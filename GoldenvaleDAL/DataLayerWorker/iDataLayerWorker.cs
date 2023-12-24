

namespace GoldenvaleDAL.DataLayerWorker
{
    public interface IDataLayerWorker
    {
        private string _connectionString => string.Empty;

        public Task<IDataLayerObj>? SelectById<IDataLayerObj>(int id, IDataLayerObj obj);
        public Task DeleteById(int id, IDataLayerObj obj);
        public Task Update<IDataLayerObj>(IDataLayerObj obj);
        public Task<IDataLayerObj> Create<IDataLayerObj>(IDataLayerObj obj);
        public Task<List<IDataLayerObj>> ExecuteSproc<IDataLayerObj>(string storedProcName, Dictionary<string, object> parms);
    }
}
