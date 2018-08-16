<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LM38HistoricoML.aspx.cs" Inherits="Multilinha.LM38HistoricoML" %>

<%@ Register Src="~/header.ascx" TagPrefix="uc1" TagName="header" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Histórico ML</title>
    <meta http-equiv="X-UA-Compatible" content="IE=11" />
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <link href="css/multilinha.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/jquery-1.12.4.min.js"></script>
    <script src="scripts/jquery-ui.js"></script>
    <script src="scripts/bootstrap.js"></script>
</head>
<uc1:header runat="server" ID="header" />
<body>
    <form id="form1" class="content container-fluid form-horizontal" runat="server">
        <div id="dvError" runat="server">
            <asp:Label runat="server" ID="lberror" CssClass="col-md-12 col-sm-12 lbl" Visible="false ">Occur an error</asp:Label>
        </div>
        <div class="clear"></div>
        <br />
        <div class="row titleTransaction menu">
            <ul>           
                <li id="liVisualizacao" runat="server" class=" ">               
                    <asp:LinkButton CssClass="atab " ID="lblTransactionV" Enabled="false" runat="server" Text="V - Histórico ML"></asp:LinkButton>
               </li>                                  
            </ul>
        </div>
        <div id="lm38_cons" runat="server" >
        <div class="row colorbck">
            <div id="camposChaveHis" runat="server" >
                <div class="row form-group padding-row ">
                    <div class="col-sm-6">
                        <label id="bkCliente" runat="server" class="col-sm-4 text-right lbl">* Cliente: </label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtCliente" MaxLength="7" onkeypress="return isNumber(event)" OnTextChanged="txtCliente_TextChanged" AutoPostBack="true" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="chaveContrato" ID="reqCliente" ControlToValidate="txtCliente" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                         <div class="col-sm-4">
                            <asp:TextBox ID="txtNome" CssClass="form-control text-field" ReadOnly="true" Enabled="false" MaxLength="40" runat="server"></asp:TextBox>
                        </div>
                    </div>
                 </div>
                <div class="row form-group padding-row ">
                    <div class="col-sm-6">
                        <label id="bkidmultilinha" runat="server" class="col-sm-4 text-right lbl">* ID Multilinha: </label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtidmultilinha" MaxLength="12" AutoPostBack="true" OnTextChanged="txt_idmultilinha_TextChanged" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="chaveContrato" ID="reqidmultilinha" ControlToValidate="txtidmultilinha" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row form-group col-sm-12 ">
                    <div class="col-sm-6">
                     </div>
                     <div class="div-btns">
                        <div class="col-sm-4" >   
                            <asp:button id="btnSearchCont" class="btns text-center" runat="server" CausesValidation="true" OnClick="btnSearchCont_Click" ValidationGroup="chaveContrato" Text="OK" ></asp:button>
                        </div> 
                     </div>
               </div>
            <hr class="hr" id="hr1" runat="server" /> 
            </div>
            <div id="tbHistorico" class="row form-group col-sm-12" style="padding-left:40px" runat="server">
             <asp:ListView ID="lvhistoricoAlteracoes" runat="server" class="row form-group padding-row">
                <EmptyDataTemplate>Não foram encontrados resultados</EmptyDataTemplate>
                <LayoutTemplate>
                    <div class="Table Grid col-sm-12" id="tbHistoricoAlt">
                        <div class="Heading Color">
                            <div class="Cell Grid">
                                <label></label>
                            </div>
                            <div class="Cell Grid">
                                <label>ID Simulação</label>
                            </div>
                            <div class="Cell Grid">
                                <label>Data Processamento</label>
                            </div>
                            <div class="Cell Grid">
                                <label>Data Alteração</label>
                            </div>
                              <div class="Cell Grid">
                                <label>Nº Contrato Produto</label>
                            </div>
                              <div class="Cell Grid">
                                <label>Tipo</label>
                            </div>
                              <div class="Cell Grid">
                                <label>Valor Anterior</label>
                            </div>
                            <div class="Cell Grid">
                                <label>Valor Posterior</label>
                            </div>
                            <div class="Cell Grid">
                                <label>Alteração</label>
                            </div>
                           <div class="Cell Grid">
                                <label>Utilizador</label>
                            </div>
                            </div>
                        <div class="Row" runat="server" id="itemPlaceholder">
                        </div>
                        </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="Row <%# Container.DataItemIndex % 2 == 0 ? "Even" : "Odd" %>">
                            <div class="Cell Grid">
                                <asp:CheckBox runat="server" ID="cbSelected" onclick="toggle(this)" CssClass="text-center bklabel" ></asp:CheckBox>
                            </div>
                         <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbidAlteracao" CssClass="text-center bklabel" Text='<%# Eval("idAlteracao") %>'></asp:Label>
                            </div>
                         <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbdataProcessamento" CssClass="text-center bklabel" Text='<%# Eval("dataProcessamento", "{0:yyyy-MM-dd}") %>'></asp:Label>
                            </div>
                         <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbdataValorAlteracao"  CssClass="text-center bklabel" Text='<%# Eval("dataValorAlteracao", "{0:yyyy-MM-dd}") %>'></asp:Label>
                            </div>
                         <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbnContratoProduto" CssClass="text-center bklabel" Text='<%# Eval("nContratoProduto") %>'></asp:Label>
                            </div>
                         <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbTipoAlteracao" CssClass="text-center bklabel" Text='<%# Eval("TipoAlteracao") %>'></asp:Label>
                            </div>
                         <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbvalorAnterior" CssClass="bklabel" Text='<%# Eval("valorAnterior") %>'></asp:Label>
                            </div>
                           <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbvalorPosterior" CssClass="bklabel" Text='<%# Eval("valorPosterior ") %>'></asp:Label>
                            </div>
                           <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbAlteracao" CssClass="text-center bklabel" Text='<%# Eval("Alteracao") %>'></asp:Label>
                            </div>
                           <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbutilizador" CssClass="text-center bklabel" Text='<%# Eval("utilizador") %>'></asp:Label>
                            </div>
                        <asp:HiddenField runat="server" ID="campoAlterado" Value='<%# Eval("campoAlterado") %>'/>
                        </div>
                </ItemTemplate>
            </asp:ListView>
            </div>
            <br />
            <div id="divBtnConsultar" runat="server" class="row form-group padding-row col-sm-12 text-right">
                <div class="row div-btns">
                     <asp:Button CssClass="normalButton col-4 btns" runat="server" ID="btnConsultarHis" OnClick="blba_Click1" Enabled="false" CausesValidation="false" Text="Consultar" />
                </div>
            </div>
             <hr class="hr" id="hr2" runat="server" />  
        </div>
       </div>
       </form>
         <script type='text/javascript'>
            var dtfechas = "<%=this.dtfechas %>";
        </script>
        <script src="scripts/multilinha.js"></script>
        <script>
            function toggle(source) {
                var checkedValue = -1;
                checkboxes = $('[type=checkbox]');
                for (var i = 0, n = checkboxes.length; i < n; i++) {
                    if (checkboxes[i].id == source.id) {
                        checkboxes[i].checked = source.checked;
                        if (source.checked)
                            checkedValue = i;
                    }
                    else
                        checkboxes[i].checked = false;
                }

                $("#btnConsultarHis").prop('disabled', !source.checked);
            }
        </script>
   </body>
</html>
