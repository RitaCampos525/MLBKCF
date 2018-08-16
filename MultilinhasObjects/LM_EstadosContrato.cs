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
                    Description = "PENDENTE",
                },
                new ComboBox()
                {
                    Code = "03",
                    Description = "EM APROVAÇÃO",
                },
                 new ComboBox()
                {
                    Code = "04",
                    Description = "EM RENOVAÇÃO",
                },
                new ComboBox()
                {
                    Code = "05",
                    Description = "DENUNCIADO",
                },
                 new ComboBox()
                {
                    Code = "06",
                    Description = "DENUNCIADO - LIQUIDADO",
                },
                new ComboBox()
                {
                    Code = "07",
                    Description = "DENUNCIADO - EM MORA",
                },
                new ComboBox()
                {
                    Code = "07",
                    Description = "CHARGE OFF",
                },
                new ComboBox()
                {
                    Code = "08",
                    Description = "EM MORA",
                },
                new ComboBox()
                {
                    Code = "09",
                    Description = "EM MORA - CLÁUSULA SUSPENSÃO ATIVA",
                },
                new ComboBox()
                {
                    Code = "10",
                    Description = "CHARGE OFF",
                },
                new ComboBox()
                {
                    Code = "11",
                    Description = "WRITE OFF",
                },
                new ComboBox()
                {
                    Code = "12",
                    Description = "ARQUIVADO",
                },
                new ComboBox()
                {
                    Code = "13",
                    Description = "VENCIDO LIQUIDADO",
                },
            };
        }

        public static List<ComboBox> GetEstadoContratos_PMODIFICAO()
        {
            return new List<ComboBox>(){
                new ComboBox()
                {
                    Code = "10",
                    Description = "CHARGE OFF",
                },
                new ComboBox()
                {
                    Code = "11",
                    Description = "WRITE OFF",
                },
                new ComboBox()
                {
                    Code = "12",
                    Description = "ARQUIVADO",
                },
            };
        }
    }
}
