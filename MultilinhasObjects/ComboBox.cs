using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace MultilinhaObjects
{
    [Serializable]
    public class ComboBox
    {
        public String Description { get; set; }
        public String Code { get; set; }


        public static explicit operator ComboBox(DropDownList dp)
        {
            return new ComboBox()
            {
                Code = dp.SelectedItem.Value,
                Description = dp.SelectedItem.Text,
            };
        }
    }


    
}
