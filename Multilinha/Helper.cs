using MultilinhaObjects;
using MultilinhasObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Multilinha
{
    public static class Helper
    {

        public static void AddRemoveHidden(bool addHidden, params System.Web.UI.HtmlControls.HtmlControl[] ctrls)
        {
            AddRemoveCssClass(addHidden, "hidden", ctrls);
        }

        public static void AddRemoveActive(bool add, params System.Web.UI.HtmlControls.HtmlControl[] ctrls)
        {
            if(add)
                AddRemoveCssClass(add, "active", ctrls);
            else
                AddRemoveCssClass(false, "desactive", ctrls);
        }

        public static void AddRemoveCssClass(bool addRemoveClass, string cssClass, params System.Web.UI.HtmlControls.HtmlControl[] ctrls)
        {
            string className = "class", currCss;

            if (addRemoveClass) //Adiciona Class
                foreach (var ctrl in ctrls)
                {
                    currCss = ctrl.Attributes[className];
                    ctrl.Attributes.Remove(className);
                    if (currCss != null && currCss.Contains(cssClass))
                        ctrl.Attributes.Add(className, currCss);
                    else if (currCss != null)
                        ctrl.Attributes.Add(className, String.Join(" ", currCss, cssClass));
                    else
                        ctrl.Attributes.Add(className, cssClass);
                }
            else //Remove
            {
                foreach (var ctrl in ctrls)
                {
                    currCss = ctrl.Attributes[className];
                    ctrl.Attributes.Remove(className);
                    if (currCss != null)
                        ctrl.Attributes.Add(className, currCss.Replace(cssClass, string.Empty));
                }
            }
        }

        public static void EnableDisableCtrl(bool enabled, params System.Web.UI.Control[] ctrls)
        {
                EnableDisableCtrlEnum(enabled, ctrls);
        }

        internal static void SetEnableControler(this Control control, bool toEnable)
        {   //Função recursiva
            Type type = control.GetType();
            PropertyInfo needCHange = type.GetProperty("noChange");

            if ((control is WebControl) && (control as WebControl).Attributes["noChange"] == null)
            {
                PropertyInfo prop = type.GetProperty("Enabled");
                if (prop != null)
                {
                    prop.SetValue(control, toEnable, null);
                }
            }

            if (control.Controls.Count > 0)
            {
                foreach (Control cont in control.Controls)
                {
                    SetEnableControler(cont, toEnable);
                }
            }

        }

        internal static void SetInvisibleControler(this Control control, bool toVisible)
        {   //Função recursiva
            Type type = control.GetType();
            PropertyInfo needCHange = type.GetProperty("noChange");

            if ((control is WebControl) && (control as WebControl).Attributes["noChange"] == null)
            {
                PropertyInfo prop = type.GetProperty("Visible");
                if (prop != null)
                {
                    prop.SetValue(control, toVisible, null);
                }
            }

            if (control.Controls.Count > 0)
            {
                foreach (Control cont in control.Controls)
                {
                    SetInvisibleControler(cont, toVisible);
                }
            }

        }

        public static void EnableDisableCtrlEnum(bool enabled, IEnumerable ctrls)
        {
            System.Web.UI.WebControls.TextBox txt = null;
            System.Web.UI.HtmlControls.HtmlSelect cmb = null;
            System.Web.UI.HtmlControls.HtmlButton btn = null;

            if (!enabled) //Disable
                foreach (var ctrl in ctrls)
                {
                    if (ctrl is System.Web.UI.WebControls.TextBox)
                    {
                        txt = (System.Web.UI.WebControls.TextBox)ctrl;
                        //AddAttributeWeb("readonly", "true", txt);
                        txt.ReadOnly = true;

                    }
                    else if (ctrl is System.Web.UI.HtmlControls.HtmlSelect)
                    {
                        cmb = (System.Web.UI.HtmlControls.HtmlSelect)ctrl;
                        //AddAttribute("disabled", "true", cmb);
                        cmb.Disabled = true;
                    }
                    else if(ctrl is System.Web.UI.HtmlControls.HtmlButton)
                    {
                        btn = (System.Web.UI.HtmlControls.HtmlButton)ctrl;
                        //AddAttribute("disabled", "true", cmb);
                        btn.Disabled = true;
                    }
                }
            else //Enable
                foreach (var ctrl in ctrls)
                {
                    if (ctrl is System.Web.UI.WebControls.TextBox)
                    {
                        txt = (System.Web.UI.WebControls.TextBox)ctrl;
                        txt.ReadOnly = false;
                        txt.Enabled = true;
                    }
                    else if (ctrl is System.Web.UI.HtmlControls.HtmlSelect)
                    {
                        cmb = (System.Web.UI.HtmlControls.HtmlSelect)ctrl;
                        RemoveAttribute("disabled", cmb);
                        cmb.Disabled = false;
                    }
                }
        }

        public static void RemoveAttribute(string attKey, System.Web.UI.HtmlControls.HtmlControl ctrl)
        {
            RemoveAttributeInCol(attKey, ctrl.Attributes);
        }

        private static void RemoveAttributeInCol(string attKey, System.Web.UI.AttributeCollection attCol)
        {
            if (attCol[attKey] != null)
                attCol.Remove(attKey);
        }

        public static void Transfer(this Page page, string url, IDictionary<string, object> parametersToContext)
        {
            foreach (var a in parametersToContext)
            {
                HttpContext context = HttpContext.Current;
                context.Items[a.Key] = a.Value;
            }
            page.Server.Transfer(url);
        }

        internal static void CopyPropertiesTo<TU>(this Control controls, TU dest)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            var destProps = typeof(TU).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();
            if (!(controls is RequiredFieldValidator))
            {
                if (controls is Label)
                {
                    string controlId = controls.ID;
                    PropertyInfo result = null;
                    if (!String.IsNullOrEmpty(controlId))
                        result = destProps.Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                    if (result != null)
                    {
                        if (result.PropertyType == typeof(DateTime))
                        {
                            DateTime outValue;
                            if (DateTime.TryParse((controls as Label).Text, out outValue))
                                result.SetValue(dest, outValue, null);
                        }
                        else if (result.PropertyType == typeof(Int32))
                            result.SetValue(dest, Int32.Parse((controls as Label).Text), null);
                        else if (result.PropertyType == typeof(Double))
                            result.SetValue(dest, Double.Parse((controls as Label).Text), null);
                        else if (result.PropertyType == typeof(ComboBox))
                        {
                            if (result.GetValue(dest, null) == null)
                            {
                                var cb = new ComboBox();
                                CopyPropertiesTo(controls, cb);
                                result.SetValue(dest, cb, null);
                            }
                            else
                            {
                                var target = result.GetValue(dest, null);
                                var propertyTarget = target.GetType().GetProperties().Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                                if (propertyTarget != null)
                                {
                                    if (propertyTarget.PropertyType == typeof(Int32))
                                        propertyTarget.SetValue(target, Int32.Parse((controls as Label).Text), null);
                                    else
                                        propertyTarget.SetValue(target, (controls as Label).Text, null);
                                }
                            }
                        }
                        else
                        {
                            string typeName = result.PropertyType.FullName;
                            if (typeName == "System.Decimal")
                            {
                                decimal decValue = 0M;
                                string sValue = (controls as Label).Text.Replace(",", ".");
                                decValue = Convert.ToDecimal(sValue, provider);
                                result.SetValue(dest, decValue, null);
                            }
                            else
                                result.SetValue(dest, (controls as Label).Text, null);
                        }
                    }
                }
                else if (controls is HiddenField)
                {
                    string controlId = controls.ID;
                    Type controlType = controls.GetType();
                    var result = destProps.Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                    if (result != null)
                    {
                        if (result.PropertyType == typeof(DateTime))
                        {
                            DateTime outValue;
                            if (DateTime.TryParse((controls as HiddenField).Value, out outValue))
                                result.SetValue(dest, outValue, null);
                        }
                        else if (result.PropertyType == typeof(Int32))
                            result.SetValue(dest, Int32.Parse((controls as HiddenField).Value), null);
                        else if (result.PropertyType == typeof(Int64))
                            result.SetValue(dest, Int64.Parse((controls as HiddenField).Value), null);
                        //else if (result.PropertyType == typeof(List<EscalaoPorMontante>))
                        //  result.SetValue(dest, ((controls as HiddenField).Value), null);
                        else if (result.PropertyType == typeof(Double))
                            result.SetValue(dest, Double.Parse((controls as HiddenField).Value), null);
                        else if (result.PropertyType == typeof(ComboBox))
                        {
                            if (result.GetValue(dest, null) == null)
                            {
                                var cb = new ComboBox();
                                CopyPropertiesTo(controls, cb);
                                result.SetValue(dest, cb, null);
                            }
                            else
                            {
                                var target = result.GetValue(dest, null);
                                var propertyTarget = target.GetType().GetProperties().Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                                if (propertyTarget != null)
                                {
                                    if (propertyTarget.PropertyType == typeof(Int32))
                                        propertyTarget.SetValue(target, Int32.Parse((controls as HiddenField).Value), null);
                                    else
                                        propertyTarget.SetValue(target, (controls as HiddenField).Value, null);
                                }
                            }
                        }
                        else
                        {
                            string typeName = result.PropertyType.FullName;
                            if (typeName == "System.Decimal")
                            {
                                decimal decValue = 0M;
                                string sValue = (controls as HiddenField).Value.Replace(",", ".");
                                decValue = Convert.ToDecimal(sValue, provider);
                                result.SetValue(dest, decValue, null);
                            }
                            else
                                result.SetValue(dest, (controls as HiddenField).Value, null);
                        }
                    }
                }
                else if (controls is TextBox)
                {
                    string controlId = controls.ID;
                    Type controlType = controls.GetType();
                    var result = destProps.Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                    if (result != null)
                    {
                        if (result.PropertyType == typeof(DateTime))
                        {
                            DateTime outValue;
                            if (DateTime.TryParse((controls as TextBox).Text, out outValue))
                                result.SetValue(dest, outValue, null);
                        }

                        else if (result.PropertyType == typeof(Int32))
                        {
                            int val = 0;
                            if (string.IsNullOrEmpty((controls as TextBox).Text))
                            {
                                val = 0;
                            }
                            else
                            {
                                val = Int32.Parse((controls as TextBox).Text);
                            }
                            result.SetValue(dest, val, null);
                        }

                        else if (result.PropertyType == typeof(Double))
                        {
                            Double outValue;
                            if (Double.TryParse((controls as TextBox).Text, out outValue))
                                result.SetValue(dest, outValue, null);
                        }
                        else if (result.PropertyType == typeof(ComboBox))
                        {
                            if (result.GetValue(dest, null) == null)
                            {
                                var cb = new ComboBox();
                                CopyPropertiesTo(controls, cb);
                                result.SetValue(dest, cb, null);
                            }
                            else
                            {
                                var target = result.GetValue(dest, null);
                                var propertyTarget = target.GetType().GetProperties().Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                                if (propertyTarget != null)
                                {
                                    if (propertyTarget.PropertyType == typeof(Int32))
                                        propertyTarget.SetValue(target, Int32.Parse((controls as TextBox).Text), null);
                                    else
                                        propertyTarget.SetValue(target, (controls as TextBox).Text, null);
                                }
                            }
                        }
                        else
                        {
                            string typeName = result.PropertyType.FullName;
                            string sValue = (controls as TextBox).Text;
                            if (typeName == "System.Decimal")
                            {
                                sValue = (controls as TextBox).Text.Replace(",", ".");
                                decimal decValue = 0M;

                                if (string.IsNullOrEmpty(sValue))
                                    sValue = "0";

                                decValue = Convert.ToDecimal(sValue, provider);
                                result.SetValue(dest, decValue, null);
                            }
                            else
                                result.SetValue(dest, sValue, null);
                        }
                    }
                }
                else if (controls is DropDownList)
                {
                    string controlId = controls.ID;
                    Type controlType = controls.GetType();
                    var result = destProps.Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                    if (result != null)
                    {
                        if (result.PropertyType == typeof(ComboBox))
                        {
                            result.SetValue(dest, (ComboBox)(controls as DropDownList), null);
                        }
                        if (result.PropertyType == typeof(string))
                        {
                            if ((controls as DropDownList).SelectedItem != null)
                            {
                                result.SetValue(dest, (string)(controls as DropDownList).SelectedItem.Value, null);
                            }
                        }
                    }
                }

                else if (controls is CheckBoxList)
                {
                    string controlId = controls.ID;
                    Type controlType = controls.GetType();
                    var result = destProps.Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                    if (result != null)
                    {

                        if (result.PropertyType == typeof(string))
                        {
                            string val = (controls as CheckBoxList).SelectedItem == null ? "" : (controls as CheckBoxList).SelectedItem.Text;
                            result.SetValue(dest, val, null);
                        }
                    }
                }

                else if (controls is CheckBox)
                {
                    string controlId = controls.ID;
                    Type controlType = controls.GetType();
                    var result = destProps.Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                    if (result != null)
                    {

                        if (result.PropertyType == typeof(string))
                        {
                            string val = (controls as CheckBox).Checked.ToString();
                            result.SetValue(dest, val, null);
                        }

                        if (result.PropertyType == typeof(Boolean))
                        {
                            bool val = (controls as CheckBox).Checked;
                            result.SetValue(dest, val, null);
                        }
                    }
                }

                else if (controls is RadioButtonList)
                {
                    string controlId = controls.ID;
                    Type controlType = controls.GetType();
                    var result = destProps.Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                    if (result != null)
                    {

                        if (result.PropertyType == typeof(string))
                        {
                            string val = (controls as RadioButtonList).SelectedItem == null ? "" : (controls as RadioButtonList).SelectedItem.Value;
                            result.SetValue(dest, val, null);
                        }
                    }
                }
                else if (controls is ListView)
                {
                    string controlId = controls.ID;
                    Type controlType = controls.GetType();
                    var result = destProps.Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                    if (result != null)
                    {
                        CopyObjectToControls(controls, result);
                    }
                }

            }

            if (controls.Controls.Count > 0)
            {
                foreach (Control cont in controls.Controls)
                {
                    CopyPropertiesTo(cont, dest);
                }
            }
        }

        internal static void CopyObjectToControls<TU>(this Control controls, TU dest) //
        {
            var props = typeof(TU).GetProperties()
                    .Where(x => x.CanRead)
                    .ToList();

            if (controls is Label && !(controls is RequiredFieldValidator))
            {
                string controlId = controls.ID;
                PropertyInfo result = null;
                if (!String.IsNullOrEmpty(controlId))
                    result = props.Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                if (result != null)
                {
                    if (result.PropertyType == typeof(ComboBox))
                    {
                        if (result.GetValue(dest, null) != null)
                        {
                            var target = result.GetValue(dest, null);
                            var propertyTarget = target.GetType().GetProperties().Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                            var valueSelected = propertyTarget.GetValue(target, null);
                            if (valueSelected != null)
                                (controls as Label).Text = valueSelected.ToString();
                        }
                    }
                    else
                    {
                        var valueSelected = result.GetValue(dest, null);
                        if (valueSelected != null)
                            (controls as Label).Text = valueSelected.ToString();
                    }
                }
            }
            else if (controls is TextBox)
            {
                string controlId = controls.ID;
                var result = props.Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                if (result != null)
                {
                    if (result.PropertyType == typeof(ComboBox))
                    {
                        if (result.GetValue(dest, null) != null)
                        {
                            var target = result.GetValue(dest, null);
                            var propertyTarget = target.GetType().GetProperties().Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                            var valueSelected = propertyTarget.GetValue(target, null);
                            if (valueSelected != null)
                                (controls as TextBox).Text = valueSelected.ToString();
                        }
                    }
                    else
                    {
                        var valueSelected = result.GetValue(dest, null);
                        if (valueSelected != null)
                        {
                            if (result.PropertyType == typeof(DateTime))
                                (controls as TextBox).Text = ((DateTime)valueSelected) == new DateTime() ? "" : ((DateTime)valueSelected).ToString("yyyy-MM-dd");
                            else
                                (controls as TextBox).Text = valueSelected.ToString();
                        }
                    }
                }
            }
            else if (controls is DropDownList)
            {
                string controlId = controls.ID;
                var result = props.Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                if (result != null)
                {
                    if (result.PropertyType == typeof(string))
                    {
                        if (result.GetValue(dest, null) != null)
                        {
                            var target = result.GetValue(dest, null);

                            ListItem it = (controls as DropDownList).Items.FindByText(target.ToString()); //Procura por texto
                            if (it == null)
                            {
                                it = (controls as DropDownList).Items.FindByValue(target.ToString()); //Procura por valor
                                if(it == null)
                                {

                                    (controls as DropDownList).Items.Add(new ListItem() { Value = target.ToString(), Text = target.ToString() }); //Adiciona Valor
                                    it = (controls as DropDownList).Items.FindByValue(target.ToString());
                                }
                            }
                            if (it != null)
                            {
                                (controls as DropDownList).SelectedValue = it.Value;
                                controls.DataBind();
                            }

                        }
                    }
                    if (result.PropertyType == typeof(ComboBox))
                    {
                        if (result.GetValue(dest, null) != null)
                        {
                            var target = result.GetValue(dest, null);
                            ListItem it = (controls as DropDownList).Items.FindByText((target as ComboBox).Code.ToString()); //Procura por texto
                            if (it == null)
                            {
                                it = (controls as DropDownList).Items.FindByValue((target as ComboBox).Code.ToString()); //Procura por valor
                            }
                            if (it != null)
                            {
                                (controls as DropDownList).SelectedValue = it.Value;
                                controls.DataBind();
                            }
                        }
                    }
                }
            }

            else if (controls is CheckBoxList)
            {
                string controlId = controls.ID;
                var result = props.Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                if (result != null)
                {
                    if (result.PropertyType == typeof(string))
                    {
                        if (result.GetValue(dest, null) != null)
                        {
                            var target = result.GetValue(dest, null);

                            ListItem it = (controls as CheckBoxList).Items.FindByText(target.ToString()); //Procura por descritivo
                            if (it == null)
                            {
                                it = (controls as CheckBoxList).Items.FindByValue(target.ToString()); //Procura por valor
                            }
                            if (it != null)
                            {
                                (controls as CheckBoxList).SelectedValue = it.Value;
                                controls.DataBind();
                            }
                        }
                    }
                }
            }

            else if (controls is CheckBox)
            {
                string controlId = controls.ID;
                var result = props.Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                if (result != null)
                {
                    if (result.PropertyType == typeof(Boolean))
                    {
                        if (result.GetValue(dest, null) != null)
                        {
                            var target = result.GetValue(dest, null);


                            (controls as CheckBox).Checked = target.ToString() == "True";
                            controls.DataBind();
                        }
                    }
                }
            }

            else if (controls is RadioButtonList)
            {
                string controlId = controls.ID;
                var result = props.Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                if (result != null)
                {
                    if (result.PropertyType == typeof(string))
                    {
                        if (result.GetValue(dest, null) != null)
                        {
                            var target = result.GetValue(dest, null);

                            ListItem it = (controls as RadioButtonList).Items.FindByText(target.ToString()); //Procura por descritivo
                            if (it == null)
                            {
                                it = (controls as RadioButtonList).Items.FindByValue(target.ToString()); //Procura por valor
                            }
                            if (it != null)
                            {
                                (controls as RadioButtonList).SelectedValue = it.Value;
                                controls.DataBind();
                            }
                        }
                    }
                }
            }
            else if (controls is HiddenField)
            {
                string controlId = controls.ID;
                var result = props.Where(x => controlId.Contains(x.Name)).FirstOrDefault();
                if (result != null)
                {
                    if (result.PropertyType == typeof(string))
                    {
                        var target = result.GetValue(dest, null);
                        if (target != null)
                        {
                            (controls as HiddenField).Value = target.ToString();
                            controls.DataBind();
                        }
                    }
                }
            }

            if (controls.Controls.Count > 0)
            {
                foreach (Control cont in controls.Controls)
                {
                    CopyObjectToControls(cont, dest);
                }
            }
        }

        public static string getTransactionMode(HttpContext pagecontext, HttpRequest request)
        {
            string op = "";

            //Lê contexto da transação anterior (C,V,M)
            op = pagecontext.Items["Op"] as string;
            op = string.IsNullOrEmpty(op) ? "FF" : op;
            //Se não tiver contexto lê querystring
            if (op == "FF")
            {
                op = request.QueryString["OP"] ?? "FF";
            }
            return op;
        }

        /// <summary>
        /// Método para obter o control que corresponde à alteração efetuada - Regra de Negócio
        /// </summary>
        /// <param name="descAlteracao"></param>
        /// <returns>webcontrol</returns>
        public static string getControltoHighLight(string descAlteracao)
        {
            string control = "";
            if(descAlteracao.ToUpper().Contains("CONTA DO"))
            {
                return "ddlncontado";
            }
            else if (descAlteracao.ToUpper().Contains("ESTADO"))
            {
                return "ddlEstadoContrato";
            }
            else if (descAlteracao.ToUpper().Contains("PRAZO"))
            {
                return "txtprazocontrato";
            }
            else if (descAlteracao.ToUpper().Contains("RENOVAÇÃO"))
            {
                return "ddlIndRenovacao";
            }
            else if (descAlteracao.ToUpper().Contains("CONDIÇÕES DE CONTRATO"))
            {
                return "txtNDiasIncumprimento";
            }
            else if (descAlteracao.ToUpper().Contains("DENUNCIA"))
            {
                return "ddlncontado";
            }
            else if (descAlteracao.ToUpper().Contains("CONDIÇÕES DE DENUNCIA"))
            {
                return "ddlContratoDenunciado";
            }
            else if (descAlteracao.ToUpper().Contains("LIMITE MÁXIMO"))
            {
                return "txtlimiteglobalmultilinha";
            }
            else if (descAlteracao.ToUpper().Contains("LIMITE FINANCEIRO"))
            {
                return "txtsublimiteriscoFinanceiro";
            }
            else if (descAlteracao.ToUpper().Contains("LIMITE COMERCIAL"))
            {
                return "txtsublimitriscoComercial";
            }
            else if (descAlteracao.ToUpper().Contains("LIMITE ASSINATURA"))
            {
                return "txtsublimiteriscoAssinatura";
            }
            else if (descAlteracao.ToUpper().Contains("CONTRATO PRODUTO"))
            {
                return "divRiscoFinanceiro";
            }
            else if (descAlteracao.ToUpper().Contains("SUBLIMITE PRODUTO"))
            {
                return "lbsublimiteComprometido";
            }

            return control;

        }

        public static void AddHightLight(this Control control, bool highlight)
        {
            if (highlight)
            {
                if (control is WebControl)
                {
                    var webControl = (WebControl)control;
                    webControl.CssClass = "highLight " + webControl.CssClass;
                }
            }
        }
        
    }
}