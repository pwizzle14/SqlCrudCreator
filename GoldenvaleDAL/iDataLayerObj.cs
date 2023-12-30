namespace GoldenvaleDAL
{
    public interface IDataLayerObj
    {
        public string GetPrimaryKey();
        public string SprocNameCreate();
        public string SprocNameUpdate();
        public string SprocNameDelete();
        public string SprocNameFetch();
        
    }
}
