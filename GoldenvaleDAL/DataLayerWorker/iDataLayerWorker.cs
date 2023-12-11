

using static GoldenvaleDAL.Utilities;

namespace GoldenvaleDAL.DataLayerWorker
{
    public interface iDataLayerWorker
    {
        private string _connectionString => string.Empty;

        public List<iDataLayerObj> Select<iDataLayerObj>(iDataLayerObj obj);
        public void Delete<iDataLayerObj>(iDataLayerObj obj);
        public void Update<iDataLayerObj>(iDataLayerObj obj);
        public void Create<iDataLayerObj>(iDataLayerObj obj);
        public List<iDataLayerObj>ExecuteSproc(string StoredProcName, iDataLayerObj obj);
    }
}
