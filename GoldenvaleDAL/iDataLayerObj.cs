﻿namespace GoldenvaleDAL
{
    public interface iDataLayerObj
    {
        public string GetPrimaryKey();
        public string SprocNameCreate();
        public string SprocNameUpdate();
        public string SprocNameDelete();
        public string SprocNameFetch();
        
    }
}
