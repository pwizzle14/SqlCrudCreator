namespace GoldenvaleDAL.ClassObjects
{
    public partial class TitanProcesserQueue : IDataLayerObj
    {
        public int TitanProcesserQueueID { get; set; }
        public int ServiceId { get; set; }
        public string? ServiceData { get; set; }
        public int ServiceStatusId { get; set; }
        public DateTime? StartTimeUTC { get; set; }
        public DateTime? EndTimeUTC { get; set; }
        public string? ProcessingGUID { get; set; }



        public string GetPrimaryKey()
        {
            return "TitanProcesserQueueID";
        }


        public string SprocNameCreate()
        {
            return "TitanProcesserQueue_Create";
        }
        public string SprocNameUpdate()
        {
            return "TitanProcesserQueue_Update";
        }
        public string SprocNameDelete()
        {
            return "TitanProcesserQueue_Delete";
        }
        public string SprocNameFetch()
        {
            return "TitanProcesserQueue_Fetch";
        }

    }
}