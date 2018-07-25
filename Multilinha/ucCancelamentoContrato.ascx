<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCancelamentoContrato.ascx.cs" Inherits="Multilinha.ucCancelamentoContrato" %>
  
<link href="css/jquery-ui.css" rel="stylesheet" />
<link href="css/multilinha.css" rel="stylesheet" type="text/css" media="screen" />
<link href="css/bootstrap.min.css" rel="stylesheet" />
<script src="scripts/jquery-1.12.4.min.js"></script>
<script src="scripts/jquery-ui.js"></script>
<script src="scripts/bootstrap.js"></script>

<div class="row colorbck">
            <div class="row form-group padding-row ">
                <div class="col-sm-4">
                    <asp:Label ID="bkCliente" runat="server" CssClass="col-sm-4 text-right lbl"  MaxLength="10">* Cliente: </asp:Label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtCliente" MaxLength="9" onkeypress="return isNumber(event)" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="chaveDenuncia" ID="reqCliente" ControlToValidate="txtCliente" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-4">
                    <asp:Label ID="bkClienteNome" runat="server" CssClass="col-sm-4 text-right lbl"></asp:Label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtNome" CssClass="form-control text-field" Enabled="false" MaxLength="40" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row form-group padding-row ">
                <div class="col-sm-4">
                    <asp:Label ID="bkidmultilinha" runat="server" CssClass="col-sm-4 text-right lbl" MaxLength="12">* ID Multilinha: </asp:Label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtidmultilinha" MaxLength="12" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="chaveDenuncia" ID="reqidmultilinha" ControlToValidate="txtidmultilinha" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                    </div>

                </div>
                <div class="col-sm-1 div-btns" >
                    <asp:button id="btnSearchCont" class="btns text-center" runat="server" CausesValidation="true" OnClick="btnSearchCont_Click" ValidationGroup="chaveDenuncia" Text="Consultar" ></asp:button>
                </div> 
            </div>
           <hr class="hr" />
        <div id="dpConsulta" visible="true" runat="server" >
            <div class="row form-group padding-row ">
                            <div class="col-sm-4">
                                <asp:Label ID="bkNDO" runat="server" CssClass="col-sm-4 text-right lbl" MaxLength="10">* Nº DO: </asp:Label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlncontado" ReadOnly="true" Enabled="false" MaxLength="9" CssClass="form-control text-field dtField" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqddlncontado" ControlToValidate="ddlncontado" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                                </div>
                            </div>
            </div>
            <div class="row form-group padding-row ">
                <div class="col-sm-4">
                    <asp:Label ID="bkdtiniciocontrato" runat="server" CssClass="col-sm-4 text-right lbl" MaxLength="10">* Data início de contrato: </asp:Label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtdatainiciocontrato" ReadOnly="true" Enabled="false" placeholder="0001-01-01" MaxLength="10" CssClass="form-control text-field dtField" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqdatainiciocontrato" ControlToValidate="txtdatainiciocontrato" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        <label class="requiredFieldEmpty" style="display: none;" id="reqemdatainiciocontrato" runat="server">Formato incorreto</label>
                    </div>
                </div>
                <div class="col-sm-4">
                    <asp:Label ID="bklprazocontrato" runat="server" CssClass="col-sm-4 text-right lbl">* Prazo contrato (meses):</asp:Label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtprazocontrato" MaxLength="3" ReadOnly="true" Enabled="false" CssClass="form-control text-field" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqPrazoContrato" ControlToValidate="txtprazocontrato" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-4">
                    <asp:Label ID="bkdtfimcontrato" runat="server" CssClass="col-sm-4 text-right lbl" MaxLength="10">* Data fim de contrato: </asp:Label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtdatafimcontrato" ReadOnly="true" Enabled="false" placeholder="9999-12-31" MaxLength="10" CssClass="form-control text-field dtField" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row form-group padding-row ">
                <div class="col-sm-4">
                    <asp:Label ID="bkindicadorrenovacao" runat="server"  CssClass="col-sm-4 text-right lbl">* Indicador de renovação: </asp:Label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlIndicadorRenovacao" ReadOnly="true" Enabled="false" runat="server" CssClass="form-control text-field">
                                <asp:ListItem Value="S" Text="Sim"></asp:ListItem>
                                <asp:ListItem Value=" " Text="Não"></asp:ListItem>
                            </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-4">
                    <asp:Label ID="bkprazorenovacao" runat="server" CssClass="col-sm-4 text-right lbl">* Prazo renovação (meses):</asp:Label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtprazorenovacao" MaxLength="3" Enabled="false" CssClass="form-control text-field" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtprazocontrato" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-4">
                    <asp:Label ID="bkdatarenovacao" runat="server" CssClass="col-sm-4 text-right lbl" MaxLength="10">* Data renovação: </asp:Label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtdatarenovacao" ReadOnly="true" Enabled="false" placeholder="9999-12-31" MaxLength="10" CssClass="form-control text-field dtField" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqdatarenovacao" ControlToValidate="txtdatarenovacao" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        <label class="requiredFieldEmpty" style="display: none;" id="lbemdatarenovacao" runat="server">Formato incorreto</label>
                    </div>
                </div>
            </div>
            <div class="row form-group padding-row ">
                <div class="col-sm-4">
                    <asp:Label ID="bkEstadoContrato" runat="server" CssClass="col-sm-4 text-right lbl">* Estado do Contrato: </asp:Label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlEstadoContrato" Enabled="false" CssClass="form-control text-field" DataTextField="Description" DataValueField="Code" MaxLength="30" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-4">
                    <asp:Label ID="bkndiasincumprimento" runat="server" CssClass="col-sm-4 text-right lbl">* Nº dias incumprimento:</asp:Label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtndiasincumprimento" MaxLength="3" ReadOnly="true" Enabled="false"  CssClass="form-control text-field" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqndiasincumprimento" ControlToValidate="txtndiasincumprimento" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-4">
                    <asp:Label ID="bkdataProcessamento" runat="server" CssClass="col-sm-4 text-right lbl" MaxLength="10">* Data Processamento: </asp:Label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtdataProcessamento" ReadOnly="true" Enabled="false" placeholder="9999-12-31" MaxLength="10" CssClass="form-control text-field dtField" runat="server"></asp:TextBox> 
                    </div>
                </div>
            </div>
        </div>
</div>
<div id="dvtitleAccoes" class="row titleAccordion" runat="server" onclick="fAccordionController();">
    <a id="titleRenovacao" runat="server" class="accordion " title="Renovação">Acções</a>
</div>
<div id="divAcoes" runat="server" class="row closeAccordion colorbck hidden ">
    <div class="col-sm-12">
        <div class="row colorbck">
            <div class="row form-group padding-row">
                <div class="col-sm-6">
                    <asp:Label ID="lblIndicadorRenova" runat="server" CssClass="col-sm-4 text-right">Denúncia:</asp:Label>
                    <div class="col-sm-6">
                        <asp:CheckBox ID="chDenuncia" runat="server" ></asp:CheckBox>
                    </div>
                </div>
                <div class="col-sm-6">
                    <asp:Label ID="lblPrazoRenova" runat="server" CssClass="col-sm-4 text-right">Resolução:</asp:Label>
                    <div class="col-sm-6">
                        <asp:CheckBox ID="chResolucao" runat="server" ></asp:CheckBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div id="accoesfinais_denunciarml03" runat="server">
            <hr id="hr1" runat="server" class="hr" />

        <div class="row text-right div-btns">
            <asp:button id="btnSeguinte" CssClass="btns" runat="server" Text="OK" causesvalidation="true" ></asp:button>
        </div>
</div>


<script type='text/javascript'>
    var dtfechas = "<%=this.dtfechas %>";
 </script>
<script src="scripts/multilinha.js"></script>