﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LM37SimulacaoML.aspx.cs" Inherits="Multilinha.LM37SimulacaoML" %>
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
                    <asp:LinkButton CssClass="atab "  ID="lblTransaction" Enabled="false" runat="server" Text="C - Simulação ML"></asp:LinkButton>
                </li>               
                <li id="liVisualizacao" runat="server" class=" ">               
                    <asp:LinkButton CssClass="atab " ID="lblTransactionV" Enabled="false" runat="server" Text="V - Consulta de Simulações ML"></asp:LinkButton>
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
                        <asp:TextBox ID="TextBox1" CssClass="form-control text-field" ReadOnly="true" Enabled="false" MaxLength="40" runat="server"></asp:TextBox>
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
            <div class="row form-group padding-row col-sm-12 ">
                    <div class="col-sm-6">
                            <label id="lbTipoSimulacao" runat="server" class="col-sm-4 text-right lbl">* Tipo Simulação: </label>
                            <div class="col-sm-8">
                            <asp:RadioButtonList runat="server" ID="rdtipoSimulacao" RepeatDirection="Horizontal" TextAlign="Left" DataTextField="Description" DataValueField="Code"></asp:RadioButtonList>
                            </div>
                     </div>
                     <div class="col-sm-6">
                        <div class="col-sm-4 div-btns" >   
                            <asp:button id="btnSearchCont" class="btns text-center" runat="server" CausesValidation="true" OnClick="btnSearchCont_Click" ValidationGroup="chaveContrato" Text="OK" ></asp:button>
                        </div> 
                        </div>
               </div>
            <hr class="hr" id="hr1" runat="server" />        </div>
 
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
                                MaxLength="2" CssClass="form-control text-field" runat="server" DataTextField="">
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
                            <asp:TextBox ID="txtlimiteglobalmultilinhaNovo" Enabled="false" CssClass=" col-sm-4 form-control text-field" runat="server"></asp:TextBox>
                           
                        </div>
                        <div class="col-sm-1">
                             <span class="ui-icon ui-icon-check"></span>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="lblimiteglobalmultilinhaTotal" runat="server" class="col-sm-4 text-right lbl"></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtlimiteglobalmultilinhaTotal" CssClass="form-control text-field number" runat="server"></asp:TextBox>
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
                            <asp:TextBox ID="txtsublimiteriscoFinanceiroNovo" Enabled="false" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-1">
                             <span class="ui-icon ui-icon-check"></span>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="lbsublimiteriscoFinanceiroTotal" runat="server" class="col-sm-4 text-right lbl"></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtsublimiteriscoFinanceiroTotal" CssClass="form-control text-field number" runat="server"></asp:TextBox>
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
                            <asp:TextBox ID="txtsublimitriscoComercialNovo" Enabled="false" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        </div>
                         <div class="col-sm-1">
                             <span class="ui-icon ui-icon-check"></span>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="lbsublimitriscoComercialTotal" runat="server" class="col-sm-4 text-right lbl"></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtsublimitriscoComercialTotal" CssClass="form-control text-field number" runat="server"></asp:TextBox>
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
                            <asp:TextBox ID="txtsublimiteriscoAssinaturaNovo" Enabled="false" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        </div>
                         <div class="col-sm-1">
                             <span class="ui-icon ui-icon-check"></span>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="lbsublimiteriscoAssinaturaTotal" runat="server" class="col-sm-4 text-right lbl"></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtsublimiteriscoAssinaturaTotal" CssClass="form-control text-field number" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
             </div>
             <div id="tbsimulacoesFamilias" class="row form-group padding-row ">
                         <div class="col-sm-10 treViewCatalogo">
                             <asp:ListView ID="lvProdutosSimulacao" runat="server">
                                        <EmptyDataTemplate>Não existem resultados para a pesquisa efetuada.</EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="Table Grid" id="tbSublimtesFinanceiros">
                                                <div class="Heading Color">
                                                    <div class="Cell Grid col-6">
                                                        <label>Tipo Risco</label>
                                                    </div>
                                                    <div class="Cell Grid col-6">
                                                        <label>Tipologia de Produto</label>
                                                    </div>
                                                    <div class="Cell Grid col-6">
                                                        <label>Código Tipologia</label>
                                                    </div>
                                                    <div class="Cell Grid col-6">
                                                        <label>Preço</label>
                                                    </div>
                                                    <div class="Cell Grid col-6">
                                                        <label>Sublimite Comprometido Atual</label>
                                                    </div>
                                                     <div class="Cell Grid col-6">
                                                        <label>Sublimite Contratado</label>
                                                    </div>
                                                       <div class="Cell Grid col-6">
                                                        <label>Exposição Atual</label>
                                                    </div>
                                                    <div class="Cell Grid col-6">
                                                        <label>Novo Sublimite Comprometido</label>
                                                    </div>
                                                       <div class="Cell Grid col-6">
                                                        <label></label>
                                                    </div>
                                                    </div>
                                                <div class="Row" runat="server" id="itemPlaceholder">
                                                </div>
                                             </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <div class="Row <%# Container.DataItemIndex % 2 == 0 ? "Even" : "Odd" %>">
                                                    <div class="Cell Grid col-6">
                                                        <asp:Label runat="server" ID="lbTipologiaRisco" CssClass="text-center" Text='<%# Eval("TipologiaRisco") %>'></asp:Label>
                                                    </div>
                                                <div class="Cell Grid col-6">
                                                        <asp:Label runat="server" ID="lbFamiliaProduto" Text='<%# Eval("FamiliaProduto") %>' ></asp:Label>
                                                    </div>
                                                <div class="Cell Grid col-6">
                                                        <asp:Label runat="server" ID="lbCodigoTipologia" Text='<%# Eval("CodigoTipologia") %>' ></asp:Label>
                                                    </div>
                                                 <div class="Cell Grid col-6">
                                                        <asp:Label runat="server" ID="lbpreco" Text='<%# Eval("preco") %>' ></asp:Label>
                                                    </div>
                                                 <div class="Cell Grid col-6">
                                                        <asp:Label runat="server" ID="lbSublimiteComprometido" Text='<%# Eval("SublimiteComprometido") %>' ></asp:Label>
                                                    </div>
                                                 <div class="Cell Grid col-6">
                                                        <asp:Label runat="server" ID="lbSublimiteContratado" Text='<%# Eval("SublimiteContratado") %>' ></asp:Label>
                                                    </div>
                                                  <div class="Cell Grid col-6">
                                                        <asp:Label runat="server" ID="lbExposicaoAtual" Text='<%# Eval("ExposicaoAtual") %>' ></asp:Label>
                                                    </div>
                                                <div class="Cell Grid col-6">
                                                        <asp:TextBox runat="server" ID="lbSublimiteComprometidoNovo" Text='<%# Eval("SublimiteComprometidoNovo") %>' ></asp:TextBox>
                                                    </div>
                                                  <div class="Cell Grid col-6">
                                                        <span id="simulcaovalido" runat="server" class="ui-icon ui-icon-check"></span>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                              </asp:ListView>
                         </div>
                    </div>
            </div>
        </div>
   </form>
</body>