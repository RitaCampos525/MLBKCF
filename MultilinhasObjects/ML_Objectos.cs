using MultilinhaObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilinhasObjects
{
    public class ML_Objectos
    {
        public static List<MultilinhaObjects.ComboBox> GetTipologias()
        {
            return new List<ComboBox>(){
                 new ComboBox()
                {
                    Code = "MLB",
                    Description = "ML Base",
                },
                  new ComboBox()
                {
                    Code = "MLA",
                    Description = "ML Avançado",
                },
            };
        }

        public static List<MultilinhaObjects.ComboBox> GetPeriocidade()
        {
            return new List<ComboBox>(){
                 new ComboBox()
                {
                    Code = "M01",
                    Description = "MENSAL",
                },
                  new ComboBox()
                {
                    Code = "M02",
                    Description = "TRIMESTRAL",
                },
                  new ComboBox()
                {
                    Code = "M03",
                    Description = "SIMESTRAL",
                },
                  new ComboBox()
                {
                    Code = "M12",
                    Description = "ANUAL",
                },
                   new ComboBox()
                {
                    Code = "M24",
                    Description = "BIANUAL",
                },
            };
        }

        public static List<MultilinhaObjects.ComboBox> GetIndicadorRenovacao()
        {
            return new List<ComboBox>(){
                 new ComboBox()
                {
                    Code = "S",
                    Description = "SIM",
                },
                  new ComboBox()
                {
                    Code = "N",
                    Description = "NÃO",
                },
        };
        }
    }
}
