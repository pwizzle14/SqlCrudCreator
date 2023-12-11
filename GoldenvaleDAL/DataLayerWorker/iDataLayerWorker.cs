

namespace GoldenvaleDAL.DataLayerWorker
{
    public interface iDataLayerWorker
    {
        private string _connectionString => string.Empty;

        public List<iDataLayerObj> Select<iDataLayerObj>(iDataLayerObj obj);
        public bool Delete<iDataLayerObj>(iDataLayerObj obj);
        public bool Update<iDataLayerObj>(iDataLayerObj obj);
        public bool Create<iDataLayerObj>(iDataLayerObj obj);
    }
}
