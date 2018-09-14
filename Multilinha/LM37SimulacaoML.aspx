<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LM37SimulacaoML.aspx.cs" Inherits="Multilinha.LM37SimulacaoML" %>
<%@ Register Src="~/header.ascx" TagPrefix="uc1" TagName="header" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Simulação ML</title>
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
                <li id="liCriacao" runat="server" class=" ">
                    <asp:LinkButton CssClass="atab "  ID="lblTransaction" Enabled="false" runat="server" Text="Simulação ML"></asp:LinkButton>
                </li>               
                <li id="liVisualizacao" runat="server" class=" ">               
                    <asp:LinkButton CssClass="atab " ID="lblTransactionV" Enabled="false" runat="server" Text="Consulta de Simulações ML"></asp:LinkButton>
               </li>                                  
            </ul>
        </div>
        <div id="lm37_criar" runat="server" >
        <div class="row colorbck">
            <div id="camposChaveSim" runat="server" >
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
                        <asp:TextBox ID="txtidmultilinha" MaxLength="12" pattern="[A-Za-z0-9]{12}" title="Deve inserir um código alfanumérico com doze posições" oninvalid="setCustomValidity('Deve inserir um código alfanumérico com doze posições')"
                            onchange="try{setCustomValidity('')}catch(e){}" AutoPostBack="true" OnTextChanged="txt_idmultilinha_TextChanged" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="chaveContrato" ID="reqidmultilinha" ControlToValidate="txtidmultilinha" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                    </div>  
                </div>
            </div>
            <div class="row form-group padding-row col-sm-12 ">
                    <div class="col-sm-6">
                            <label id="lbTipoSimulacao" runat="server" class="col-sm-4 text-right lbl lblFixRD">* Tipo Simulação: </label>
                            <div class="col-sm-8">
                            <asp:RadioButtonList runat="server" CssClass="fixRD" ID="rdtipoSimulacao" RepeatDirection="Horizontal" TextAlign="Left" DataTextField="Description" DataValueField="Code"></asp:RadioButtonList>
                            </div>
                     </div>
                     <div class="div-btns">
                        <div class="col-sm-4" >   
                            <asp:button id="btnSearchCont" class="normalButton col-4 btns" runat="server" CausesValidation="true" OnClick="btnSearchCont_Click" ValidationGroup="chaveContrato" Text="Consultar" ></asp:button>
                        </div> 
                        </div>
            </div>
            <hr class="hr" id="hr1" runat="server" />
            </div>

            <div id="divProduto" runat="server" class="row form-group padding-row col-sm-12">
                    <div class="col-sm-4">
                        <label id="bkProduto" runat="server" class="col-sm-4 text-right lbl">* Produto: </label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtProdutoml" MaxLength="2" Enabled="false" pattern="[A-Za-z0-9]{2}" title="Deve inserir o código do produto multilinha"
                                CssClass="form-control text-field" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="valChaves" ID="reqProdutoml" ControlToValidate="txtProdutoml" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="bkSubProduto" runat="server" class="col-sm-4 text-right lbl">* Sub-Produto: </label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlSubprodutoml" Enabled="false" AutoPostBack="true" pattern="[0-9]{2}"
                                MaxLength="2" CssClass="form-control text-field" runat="server" DataTextField="" DataValueField="">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="lblProdutoDesc" runat="server"  class="col-sm-4 text-right lbl">* Descritivo:</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtDescritivo" runat="server" Enabled="false" Text="ML BASE" CssClass="form-control text-field"></asp:TextBox>
                        </div>
                    </div>
                </div> 
            <div id="divSimulacaoSublimites" runat="server" >
                <div class="row form-group padding-row col-sm-12">
                    <div class="col-sm-4">
                        <label id="lb" runat="server" class="col-sm-4 text-right lbl"></label>
                        <div class="col-sm-6">
                            <label>Atual</label>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="laabsublimiteriscoFinanceiro" runat="server" class="col-sm-4 text-right lbl"></label>
                        <div class="col-sm-6">
                            <label>Novo</label>
                        </div>
                    </div>
                 <div class="col-sm-4">
                        <label id="Label2" runat="server" class="col-sm-4 text-right lbl"></label>
                        <div class="col-sm-6">
                            <label>Total</label>
                        </div>
                    </div>
                </div>
                <div class="row form-group padding-row col-sm-12">
                    <div class="col-sm-4">
                        <label id="lblimiteglobalmultilinha" runat="server" class="col-sm-4 text-right lbl">* Limite Global Multilinha</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtlimiteglobalmultilinha" Enabled="false" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="lblimiteglobalmultilinhaNovo" runat="server" class="col-sm-4 text-right lbl"></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtlimiteglobalmultilinhaNovo" MaxLength="18" Enabled="false" CssClass="number col-sm-4 form-control text-field" runat="server"></asp:TextBox>
                           <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" InitialValue="0" runat="server" ID="reqlimiteglobalmultilinhaNovo" ValidationGroup="ValidaT2" ControlToValidate="txtlimiteglobalmultilinhaNovo" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-1">
                             <span id="lmGlobalNovo" runat="server" class=""></span>
                             <span id="textlmGlobalNovo" runat="server" class=""></span>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="lblimiteglobalmultilinhaTotal" runat="server" class="col-sm-4 text-right lbl"></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtlimiteglobalmultilinhaTotal" Enabled="false" CssClass="form-control text-field number" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row form-group padding-row col-sm-12">
                    <div class="col-sm-4">
                        <label id="lbsublimiteriscoFinanceiro" runat="server" class="col-sm-4 text-right lbl">* Sublimite Risco Financeiro</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtsublimiteriscoFinanceiro" Enabled="false" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="lbsublimiteriscoFinanceiroNovo" runat="server" class="col-sm-4 text-right lbl"></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtsublimiteriscoFinanceiroNovo" MaxLength="18" Enabled="false" CssClass="number form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" InitialValue="0" runat="server" ID="reqsublimiteriscoFinanceiroNovo" ValidationGroup="ValidaT2" ControlToValidate="txtsublimiteriscoFinanceiroNovo" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-1">
                             <span id="sbRfinNovo" runat="server" class=""></span>
                            <span id="textsbRfinNovo" runat="server" class=""></span>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="lbsublimiteriscoFinanceiroTotal" runat="server" class="col-sm-4 text-right lbl"></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtsublimiteriscoFinanceiroTotal" Enabled="false"  CssClass="form-control text-field number" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row form-group padding-row col-sm-12">
                    <div class="col-sm-4">
                        <label id="lbsublimitriscoComercial" runat="server" class="col-sm-4 text-right lbl">* Sublimite Risco Comercial</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtsublimitriscoComercial" Enabled="false" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="lbsublimitriscoComercialNovo" runat="server" class="col-sm-4 text-right lbl"></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtsublimitriscoComercialNovo" MaxLength="18"  Enabled="false" CssClass="number form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" CssClass="bklabel" ValidationGroup="ValidaT2" runat="server" ID="reqsublimitriscoComercialNovo" ControlToValidate="txtsublimitriscoComercialNovo" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                         <div class="col-sm-1">
                             <span id="sbRComNov" runat="server" class=""></span>
                             <span id="txtsbRComNov" runat="server" class=""></span>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="lbsublimitriscoComercialTotal" runat="server" class="col-sm-4 text-right lbl"></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtsublimitriscoComercialTotal" Enabled="false"  CssClass="form-control text-field number" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                        <div class="row form-group padding-row col-sm-12">
                    <div class="col-sm-4">
                        <label id="lbsublimiteriscoAssinatura" runat="server" class="col-sm-4 text-right lbl">* Sublimite Risco Assinatura</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtsublimiteriscoAssinatura" Enabled="false" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="lbsublimiteriscoAssinaturaNovo" runat="server" class="col-sm-4 text-right lbl"></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtsublimiteriscoAssinaturaNovo" MaxLength="18" Enabled="false" CssClass="number form-control text-field" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" CssClass="bklabel" ValidationGroup="ValidaT2" runat="server" ID="reqsublimiteriscoAssinaturaNovo" ControlToValidate="txtsublimiteriscoAssinaturaNovo" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                         <div class="col-sm-1">
                             <span id="sbRassNovo" runat="server" class=""></span>
                             <span id="textsbRassNovo" runat="server" class=""></span>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="lbsublimiteriscoAssinaturaTotal" runat="server" class="col-sm-4 text-right lbl"></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtsublimiteriscoAssinaturaTotal" Enabled="false"  CssClass="form-control text-field number" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
             </div>
             <div id="tbsimulacoesFamilias" class="row form-group padding-row col-sm-12">
                         <div class="col-sm-12">
                             <asp:ListView ID="lvProdutosSimulacao" runat="server">
                                        <EmptyDataTemplate>Não foram encontrados resultados</EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="Table Grid" id="tbSublimtesFinanceiros">
                                                <div class="Heading Color">
                                                    <div class="Cell Grid">
                                                        <label>Tipo Risco</label>
                                                    </div>
                                                    <div class="Cell Grid">
                                                        <label>Tip. de Produto</label>
                                                    </div>
                                                    <div class="Cell Grid">
                                                        <label>Cód. Tipologia</label>
                                                    </div>
                                                    <div class="Cell Grid">
                                                        <label>Preço</label>
                                                    </div>
                                                    <div class="Cell Grid">
                                                        <label>Subl. Comprometido</label>
                                                    </div>
                                                     <div class="Cell Grid">
                                                        <label>Subl. Contratado</label>
                                                    </div>
                                                       <div class="Cell Grid">
                                                        <label>Exposição Atual</label>
                                                    </div>
                                                    <div class="Cell Grid">
                                                        <label>Novo Subl. Comprometido</label>
                                                    </div>
                                                       <div class="Cell Grid">
                                                        <label></label>
                                                    </div>
                                                    </div>
                                                <div class="Row" runat="server" id="itemPlaceholder">
                                                </div>
                                             </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <div class="Row <%# Container.DataItemIndex % 2 == 0 ? "Even" : "Odd" %>">
                                                    <div class="Cell Grid">
                                                        <asp:Label runat="server" ID="lbTipologiaRisco" CssClass="text-center bklabel" Text='<%# Eval("TipologiaRisco") %>'></asp:Label>
                                                    </div>
                                                <div class="Cell Grid">
                                                        <asp:Label runat="server" ID="lbFamiliaProduto" CssClass="text-center bklabel" Text='<%# Eval("FamiliaProduto") %>' ></asp:Label>
                                                    </div>
                                                <div class="Cell Grid">
                                                        <asp:Label runat="server" ID="lbCodigoTipologia" CssClass="text-center bklabel" Text='<%# Eval("CodigoTipologia") %>' ></asp:Label>
                                                    </div>
                                                 <div class="Cell Grid">
                                                        <asp:Label runat="server" ID="lbpreco" CssClass="text-center bklabel" Text=' <%# (Boolean.Parse(Eval("preco").ToString())) ? "S" : "N" %>' ></asp:Label>
                                                    </div>
                                                 <div class="Cell Grid">
                                                        <asp:Label runat="server" ID="lbSublimiteComprometido" CssClass="text-right bklabel" Text='<%# Eval("SublimiteComprometido") %>' ></asp:Label>
                                                    </div>
                                                 <div class="Cell Grid">
                                                        <asp:Label runat="server" ID="lbSublimiteContratado" CssClass="text-right bklabel" Text='<%# Eval("SublimiteContratado") %>' ></asp:Label>
                                                    </div>
                                                  <div class="Cell Grid">
                                                        <asp:Label runat="server" ID="lbExposicaoAtual" CssClass="text-right bklabel" Text='<%# Eval("ExposicaoAtual") %>' ></asp:Label>
                                                    </div>
                                                <div class="Cell Grid">
                                                        <asp:TextBox runat="server" CssClass="number form-control text-right bklabel" ClientIDMode="Static" MaxLength="18" InitialValue="0" ID="txtSublimiteComprometidoNovo" AutoPostBack="true" OnTextChanged="lbSublimiteComprometidoNovo_TextChanged" Text='<%# Eval("SublimiteComprometidoNovo") %>' ></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" ValidationGroup="ValidaT2" runat="server" ID="reqlbSublimiteComprometidoNovo" ControlToValidate="txtSublimiteComprometidoNovo" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                                                     </div>
                                                  <div class="Cell Grid">
                                                        <span id="simulcaovalido" runat="server" class=""></span>
                                                        <span id="textsimulcaovalido" runat="server" class=""></span>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                              </asp:ListView>
                         </div>
                    </div>
            <div id="divSimular" runat="server" class="row form-group padding-row col-sm-12 text-right">
                <div class="row div-btns">
                     <asp:Button CssClass="normalButton col-4 btns" runat="server" ID="btnSimular" ValidationGroup="ValidaT2" OnClientClick="return ValidaMontantesNovos();" OnClick="btnSimular_Click" CausesValidation="true" Text="Simular" />
                </div>
                 <hr class="hr" id="hr2" runat="server" />      
           </div>
            <div id="dvAcoes_C" runat="server" class="row form-group padding-row ">
                <div class="col-sm-6">
                    <div class="col-sm-12 row div-btns">
                        <div class="col-sm-5">
                            <label id="lbidsimulacaoml" runat="server" class="col-sm-5 text-right lbl">* ID Simulação: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtidsimulacaoml" Enabled="false" MaxLength="10" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            </div>
                            </div>
                        <asp:Button ID="btnConsultarProdutos" noChange="" class="btns" Text="Contratos Produtos ML" OnClick="btnConsultarProdutos_Click" CausesValidation="true" ValidationGroup="manterestecampo" runat="server"></asp:Button>
                        <asp:Button ID="btnGuardarSimulacao" noChange="" class="btns " runat="server" CausesValidation="true" Enabled="false" ValidationGroup="ChaveProdutos" AutoPostBack="true" OnClick="btnGuardarSimulacao_Click" Text="Guardar Simulação"></asp:Button>
                    </div>
                </div>
            </div>
       </div>
       </div>
        <div id="lm37_vis" runat="server" class="row form-group padding-row ">
            <div class="row colorbck">
            <div id="camposchaveConsulta" runat="server" >
            <div class="row form-group padding-row ">
                <div class="col-sm-6">
                    <label id="lb1Cliente" runat="server" class="col-sm-4 text-right lbl">Cliente: </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txt_Cliente" MaxLength="7" onkeypress="return isNumber(event)" OnTextChanged="txtCliente_TextChanged" AutoPostBack="true" CssClass="form-control text-field" runat="server"></asp:TextBox>
                       <%-- <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="chaveContrato" ID="rqtxt_Cliente" ControlToValidate="txt_Cliente" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>--%>
                    </div>
                     <div class="col-sm-4">
                        <asp:TextBox ID="txt_Nome" CssClass="form-control text-field" ReadOnly="true" Enabled="false" MaxLength="40" runat="server"></asp:TextBox>
                    </div>
                </div>
             </div>
            <div class="row form-group padding-row ">
                <div class="col-sm-6">
                    <label id="lb1idmultilinha" runat="server" class="col-sm-4 text-right lbl">ID Multilinha: </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txt_idmultilinha" MaxLength="12" AutoPostBack="true" OnTextChanged="txt_idmultilinha_TextChanged" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="chaveContrato" ID="rqtxt_idmultilinha" ControlToValidate="txt_idmultilinha" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>--%>
                    </div>  
                </div>
            </div>
            <div class="row form-group padding-row">
                    <div class="col-sm-6">
                        <label id="lbnBalcao" runat="server" class="col-sm-4 text-right lbl">* Balcão: </label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtBalcao" MaxLength="3" AutoPostBack="true" OnTextChanged="txtnBalcao_TextChanged" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="chaveContratoV" ID="reqnBalcao" ControlToValidate="txtBalcao" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-4">
                              <asp:TextBox ID="txtgbalcao" MaxLength="30" Enabled="false" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        </div>
                    </div>
            </div>
            <div class="row form-group padding-row">
                <div class="div-btns">
                    <div class="col-sm-4" >   
                        <asp:button id="btnSearch" class="normalButton col-4 btns" runat="server" CausesValidation="true" OnClick="btnSearchCont_Click" ValidationGroup="chaveContratoV" Text="Consultar" ></asp:button>
                    </div> 
                    </div>
                 <hr class="hr" id="hr3" runat="server" />
            </div>
            <div id="tb_consultaSimulacoes" class="row form-group padding-row col-sm-12">
                         <div class="col-sm-12">
                             <asp:ListView ID="lvConsultaSimulacoes" runat="server">
                                        <EmptyDataTemplate>Não foram encontrados resultados</EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="Table Grid" id="tbSublimtesFinanceiros">
                                                <div class="Heading Color">
                                                     <div class="Cell Grid">
                                                        <label></label>
                                                    </div>
                                                    <div class="Cell Grid">
                                                        <label>ID Alteração</label>
                                                    </div>
                                                    <div class="Cell Grid">
                                                        <label>Balcão</label>
                                                    </div>
                                                    <div class="Cell Grid">
                                                        <label>ID Multilinha</label>
                                                    </div>
                                                    <div class="Cell Grid">
                                                        <label>ID Simulação</label>
                                                    </div>
                                                    <div class="Cell Grid">
                                                        <label>Data</label>
                                                    </div>
                                                    <div class="Cell Grid">
                                                        <label>Limite ML</label>
                                                    </div>
                                                     <div class="Cell Grid">
                                                        <label>Limite RF</label>
                                                    </div>
                                                       <div class="Cell Grid">
                                                        <label>Limite RC</label>
                                                    </div>
                                                    <div class="Cell Grid">
                                                        <label>Limite RA</label>
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
                                                        <asp:Label runat="server" ID="lbzSeq" CssClass="text-center bklabel" Text='<%# Eval("zSeq") %>'></asp:Label>
                                                    </div>
                                                    <div class="Cell Grid">
                                                        <asp:Label runat="server" ID="lbcons_Balcao" CssClass="text-center bklabel" Text='<%# Eval("cons_Balcao") %>'></asp:Label>
                                                    </div>
                                                    <div class="Cell Grid">
                                                            <asp:Label runat="server" ID="lbcons_idMultilinha" CssClass="text-center bklabel" Text='<%# Eval("cons_idMultilinha") %>' ></asp:Label>
                                                        </div>
                                                    <div class="Cell Grid">
                                                            <asp:Label runat="server" ID="lbcons_idSimulacao" CssClass="text-center bklabel" Text='<%# Eval("cons_idSimulacao") %>' ></asp:Label>
                                                        </div>
                                                     <div class="Cell Grid">
                                                            <asp:Label runat="server" ID="lbcons_DataSimulacao" CssClass="text-center bklabel" Text=' <%# Eval("cons_DataSimulacao" , "{0:yyyy-MM-dd}") %>' ></asp:Label>
                                                        </div>
                                                     <div class="Cell Grid">
                                                            <asp:Label runat="server" ID="lbcons_limiteML" CssClass="text-right bklabel" Text='<%# Eval("cons_limiteML") %>' ></asp:Label>
                                                        </div>
                                                     <div class="Cell Grid">
                                                            <asp:Label runat="server" ID="lbcons_limiteRF" CssClass="text-right bklabel" Text='<%# Eval("cons_limiteRF") %>' ></asp:Label>
                                                        </div>
                                                      <div class="Cell Grid">
                                                            <asp:Label runat="server" ID="lbcons_limiteRC" CssClass="text-right bklabel" Text='<%# Eval("cons_limiteRC") %>' ></asp:Label>
                                                        </div>
                                                     <div class="Cell Grid">
                                                            <asp:Label runat="server" ID="lbcons_limiteRA" CssClass="text-right bklabel" Text='<%# Eval("cons_limiteRA") %>' ></asp:Label>
                                                        </div>
                                                    <div class="Cell Grid">
                                                            <asp:Label runat="server" ID="lbcons_utilizador" CssClass="text-right bklabel" Text='<%# Eval("cons_utilizador") %>' ></asp:Label>
                                                     </div>
                                                </div>
                                            </ItemTemplate>
                              </asp:ListView>
                         </div>
                        <div id="dvAcoes_V" runat="server" class="row form-group padding-row ">
                            <div class="div-btns">
                                <div class="col-sm-4" >  
                                    <asp:Button ID="btnConsultarSm" CssClass="btns" Text="Consultar Simulação" Enabled="false" noChange="" OnClick="btnConsultarSm_Click" CausesValidation="true" ValidationGroup="manterestecampo" runat="server"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
            </div>
            </div>
       </div>
   </form>
     <script type='text/javascript'>
         var dtfechas = "<%=this.dtfechas %>";
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

             $("#btnConsultarSm").prop('disabled', !source.checked);
         }
    </script>
    <script src="scripts/multilinha.js"></script>
</body>
