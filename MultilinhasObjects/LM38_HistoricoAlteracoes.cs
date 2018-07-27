using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilinhasObjects
{
    public class LM38_HistoricoAlteracoes
    {
        //Campos
        //Lista
        //Sublista

        public int Cliente { get; set; }
        public string Nome { get; set; }
        public int idmultilinha { get; set; }

        public class historicoAlteracoes{
            public string idAlteracao { get; set; }

            public DateTime dataProcessamento { get; set; }

            public DateTime dataValorAlteracao { get; set; }

            public string TipoAlteracao { get; set; }

            public string description { get; set; }

            public string utilizador { get; set; }

            public int zSeq { get; set; }

        }
    }
}
