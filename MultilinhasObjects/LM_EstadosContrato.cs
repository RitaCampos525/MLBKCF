using MultilinhaObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilinhasObjects
{
    public class LM_EstadosContrato
    {
        public static List<ComboBox> GetEstadoContratos()
        {
            return new List<ComboBox>(){
                 new ComboBox()
                {
                    Code = "01",
                    Description = "ATIVO",
                },
                new ComboBox()
                {
                    Code = "02",
                    Description = "EM RENOVACAO",
                },
                 new ComboBox()
                {
                    Code = "03",
                    Description = "DENUNCIADO",
                },
                new ComboBox()
                {
                    Code = "04",
                    Description = "DENUNCIADO - EM MORA",
                },
                 new ComboBox()
                {
                    Code = "05",
                    Description = "EM MORA",
                },
                new ComboBox()
                {
                    Code = "06",
                    Description = "EM MORA - CLAUSULA SUSPENSAO ATIVA",
                },
                new ComboBox()
                {
                    Code = "07",
                    Description = "CHARGE OFF",
                },
                new ComboBox()
                {
                    Code = "08",
                    Description = "WRITE OFF",
                },
                new ComboBox()
                {
                    Code = "09",
                    Description = "ARQUIVADO",
                },
            };
        }

    }
}
