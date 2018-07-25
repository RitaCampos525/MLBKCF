using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultilinhasObjects
{
    public class ABUtil
    {
        [Serializable()]
        public class ABCommandArgs
        {
            public string AB_ADDRESS;
            public string HOSTNAME;
            public string HOSTIP;
            public string FQDN_HOSTNAME;
            public string SN_HOSTNAME;
            public int PORT;
            public string AB_SESSION_ID;
            public string CUTILIZA;
            public string CODBALCAO;
            public string CTERM;

            public string CPERFIL;

            public string USERNT;
            public string DOMAIN;

            public string ZCLIENTE;
            public string TASKID;
        }

    }
}
