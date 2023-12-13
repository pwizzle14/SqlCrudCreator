

using static GoldenvaleDAL.Utilities;

namespace GoldenvaleDAL.DataLayerWorker
{
    public interface iDataLayerWorker
    {
        private string _connectionString => string.Empty;

        public iDataLayerObj? SelectById<iDataLayerObj>(int id, iDataLayerObj obj);
        public List<iDataLayerObj> Select<iDataLayerObj>(iDataLayerObj obj);
        public void Delete<iDataLayerObj>(iDataLayerObj obj);
        public void DeleteById(int id, iDataLayerObj obj);
        public void Update<iDataLayerObj>(iDataLayerObj obj);
        public iDataLayerObj Create<iDataLayerObj>(iDataLayerObj obj);
        public List<iDataLayerObj>ExecuteSproc(string StoredProcName, iDataLayerObj obj);
    }
}
