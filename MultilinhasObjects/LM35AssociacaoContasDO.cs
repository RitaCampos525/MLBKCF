﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilinhasObjects
{
    public class LM35AssociacaoContasDO
    {
        //campos
        //lista
        //sub lista

        public int Cliente { get; set; }

        public int idmultilinha { get; set; }

        public string Nome { get; set; }

        public string ncontado { get; set; }

        public List<listaContaDO> Lista
        {
            get;
            set;
        }

    }

    public class listaContaDO
    {
        public Boolean Associado { get; set; }

        public string NumContaDO { get; set; }

        public DateTime DataAssociada { get; set; }

    }
}
