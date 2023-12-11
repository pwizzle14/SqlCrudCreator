using Newtonsoft.Json;

namespace GoldenvaleDAL
{
    public class Utilities
    {
        public enum SQL_FUNCTION_TYPE
        {
            CREATE = 0,
            UPDATE = 1,
            DELETE = 2,
            SELECT = 3,
        }

        public static Dictionary<string, object> ToDictionary(object obj, SQL_FUNCTION_TYPE functionType)
        {

            if(obj == null)
                return null;

            var json = JsonConvert.SerializeObject(obj);

            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            switch (functionType)
            {
                case SQL_FUNCTION_TYPE.CREATE:
                    {
                        //remove first value with would be the key on create
                        dictionary.Remove(dictionary.Keys.First());
                            break;
                    }
                case SQL_FUNCTION_TYPE.SELECT:
                case SQL_FUNCTION_TYPE.DELETE:
                    {
                        //only need the first value
                        var dicID = new Dictionary<string, object>();
                        dicID.Add(dictionary.FirstOrDefault().Key, dictionary.FirstOrDefault().Value);

                        return dicID;
                       
                    }

                default:
                    break;
            }


            return dictionary;
        }



    }
}
