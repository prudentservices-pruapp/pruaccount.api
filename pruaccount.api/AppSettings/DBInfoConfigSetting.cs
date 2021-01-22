using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pruaccount.api.AppSettings
{
 
    public class DBInfoConfigSetting
    {
        public string CoreConnection { get; set; }
        public List<Storage> StorageList { get; set; }
    }

    public class Storage
    {
        public string Product { get; set; }
        public string DataConnection { get; set; }
    }
    
}
