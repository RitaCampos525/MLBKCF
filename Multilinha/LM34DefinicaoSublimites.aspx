<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LM34DefinicaoSublimites.aspx.cs" Inherits="Multilinha.LM34DefinicaoSublimites" %>
<%@ Register Src="~/header.ascx" TagPrefix="uc1" TagName="header" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Definição Sublimites ML</title>
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
        <div class="row titleTransaction">
            <ul class="list-inline">
                <li>
                    <asp:LinkButton CssClass="atab atabD"  ID="lblTransaction" Enabled="false" runat="server" Text="Definição de Sublimites"></asp:LinkButton>
                </li>               
                <li >               
                    <asp:LinkButton CssClass="atab atabD" ID="lblTransactionV" Enabled="false" runat="server" Text="Consulta de Sublimites"></asp:LinkButton>
               </li>                
               <li >                
                    <asp:LinkButton CssClass="atab atabD" ID="lblTransactionAp" Enabled="false" runat="server" Text="Lista de contratos para aprovação"></asp:LinkButton>
               </li>                                
               <li >                
                    <asp:LinkButton CssClass="atab atabD" ID="lblTransactionM" Enabled="false" runat="server" Text="Modificação de Sublimites"></asp:LinkButton>
               </li>
            </ul>
        </div>
        <div id="ml04_criar" runat="server" >
        <div class="row colorbck">
            <div id="camposChaveSubLim" runat="server" >
            <div class="row form-group padding-row " >
                <div class="col-sm-4">
                    <label id="bkCliente" runat="server" class="col-sm-4 text-right lbl">* Cliente: </label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtCliente" MaxLength="7" Enabled="false" ReadOnly="true" onkeypress="return isNumber(event)" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="chaveContrato" ID="reqCliente" ControlToValidate="txtCliente" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-4">
                    <Label id="bkClienteNome" runat="server" class="col-sm-4 text-right lbl"></Label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtNome" CssClass="form-control text-field" Enabled="false" MaxLength="40" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row form-group padding-row ">
                <div class="col-sm-4">
                    <label id="bkidmultilinha" runat="server" class="col-sm-4 text-right lbl">* ID Multilinha: </label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtidmultilinha" MaxLength="12" Enabled="false" ReadOnly="true" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="chaveContrato" ID="reqidmultilinha" ControlToValidate="txtidmultilinha" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                    </div>

                </div>
                <div class="col-sm-2 div-btns" >   
                    <asp:button id="btnSearchCont" class="btns text-center" runat="server" CausesValidation="true" OnClick="btnSearchCont_Click1" ValidationGroup="chaveContrato" Text="OK" ></asp:button>
                </div> 
            </div>
                
           <hr class="hr1" id="hr1" runat="server" />
           </div>
           <div id="dpOK" visible="true" runat="server" >
            <div class="row form-group padding-row ">
                    <div class="col-sm-4">
                        <Label id="bklProduto" runat="server" class="col-sm-4 text-right lbl">* Produto: </Label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtProdutoml" MaxLength="2" Text="01" Enabled="false" ReadOnly="true" pattern="[A-Za-z0-9]{2}" title="Deve inserir o código do produto multilinha"
                                 CssClass="form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqProductCode" ValidationGroup="valChaves" ControlToValidate="txtProdutoml" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <Label id="bklSubProduto" runat="server" class="col-sm-4 text-right lbl">* Sub-Produto: </Label>
                        <div class="col-sm-6">
                            <asp:DropDownList runat="server" ID="ddSubprodutoml" Enabled="false" ReadOnly="true" CssClass="form-control text-field dtField">
                                <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                <asp:ListItem Text="02" Value="02"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqSubProdCode" ValidationGroup="valChaves" ControlToValidate="ddSubprodutoml" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <Label id="bklProdutoDesc" runat="server" class="col-sm-4 text-right lbl"></Label>
                        <div class="col-sm-6">
                            <asp:TextBox id="txtDescritivo" runat="server" Enabled="false" Text="ML BASE" CssClass="form-control text-field"></asp:TextBox>
                        </div>
                    </div>
                </div>
            <div class="row form-group padding-row ">
                    <div class="col-sm-4">
                        <Label id="bkEstadoContrato" runat="server" class="col-sm-4 text-right lbl">* Estado do Contrato: </Label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtEstadoContrato" Enabled="false" CssClass="form-control text-field" MaxLength="30" runat="server"></asp:TextBox>
                        </div>
                    </div>
               </div>
            <div class="row form-group padding-row ">
                        <div class="col-sm-4">
                            <Label id="bklimitecomprometido" runat="server" class="col-sm-4 text-right lbl">* Limite Global Comprometido: </Label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtlimiteglobalmultilinha" Enabled="false" ReadOnly="true" CssClass="form-control text-field number" MaxLength="18" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqlimitecomprometido" ControlToValidate="txtlimiteglobalmultilinha" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                </div>
        
            <hr class="hr2" id="hr2" runat="server" />
            </div>
        </div>

        <div class="row titleAccordion" id="dvtitleAcordionRFinanceiro" runat="server" onclick="fAccordionController();">
                <a id="titleRiscoFinanceiro" runat="server" class="accordion " title="Risco Financeiro">Risco Financeiro</a>
        </div>
        
        <div id="divRiscoFinanceiro" runat="server" class="row closeAccordion colorbck hidden ">
                <div class="col-sm-12">
                    <div class="row form-group padding-row ">
                        <div class="col-sm-4">
                            <label id="bklabelTipoR" runat="server" class="col-sm-4 text-right lbl">* Tipo de Risco Sublimite: </label>
                            <div class="col-sm-6">
                                <Label id="bklsublimiteriscoFinanceiro" runat="server" class="col-sm-8 text-right lbl">* Risco Financeiro: </Label>
                                <asp:TextBox ID="txtsublimiteriscoFinanceiro" Enabled="false" ReadOnly="true"  CssClass="form-control text-field number" MaxLength="18" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqsublimiteriscoFinanceiro" ControlToValidate="txtsublimiteriscoFinanceiro" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                   </div>
                    <div class="row form-group padding-row ">
                         <div class="col-sm-10 treViewCatalogo">
                             <asp:ListView ID="lvProdutosRiscoF" runat="server">
                                        <EmptyDataTemplate>Não existem resultados para a pesquisa efetuada.</EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="Table Grid" id="tbSublimtesFinanceiros">
                                                <div class="Heading Color">
                                                    <div class="Cell Grid col-6">
                                                        <label>PROD</label>
                                                    </div>
                                                    <div class="Cell Grid col-6">
                                                        <label>Família Produto</label>
                                                    </div>
                                                    <div class="Cell Grid col-6">
                                                        <label>Sublimite Comprometido</label>
                                                    </div>
                    
                                                    </div>
                                                <div class="Row" runat="server" id="itemPlaceholder">
                                                </div>
                                             </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <div class="Row <%# Container.DataItemIndex % 2 == 0 ? "Even" : "Odd" %>">
                                                    <div class="Cell Grid col-6">
                                                        <asp:Label runat="server" ID="lbProduto" CssClass="text-center" Text='<%# Eval("codfamiliaproduto") %>'></asp:Label>
                                                    </div>
                                                <div class="Cell Grid col-6">
                                                        <asp:Label runat="server" ID="lbsubproduto" Text='<%# Eval("familiaProduto") %>' ></asp:Label>
                                                    </div>
                                                <div class="Cell Grid col-6">
                                                        <asp:Textbox runat="server" ID="lbsublimiteComprometido" placeholder="0,00" CssClass="number form-control text-field text-center" MaxLength="18" Text='<%# Eval("sublimitecompremetido") %>'></asp:Textbox>
                                                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" InitialValue="0" ID="reqsublimiteComprometido" ValidationGroup="sublimitesValGrp" ControlToValidate="lbsublimiteComprometido" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                              </asp:ListView>
                         </div>
                    </div>
            </div>
    </div>
        
        <div class="row titleAccordion" id="dvtitleAcordionRComercial" runat="server" onclick="fAccordionController();">
                <a id="titleRiscoComercial" runat="server" class="accordion " title="Risco Comercial">Risco Comercial</a>
        </div>

        <div id="divRiscoComercial" runat="server" class="row closeAccordion colorbck hidden ">
            <div class="col-sm-12">
                <div class="row form-group padding-row ">
                    <div class="col-sm-4">
                        <label id="bkRiscoSublimiteCom" runat="server" class="col-sm-4 text-right lbl">* Tipo de Risco Sublimite: </label>
                        <div class="col-sm-6">
                            <Label id="bkRiscoCom" runat="server" class="col-sm-8 text-right lbl">* Risco Comercial: </Label>
                            <asp:TextBox ID="txtsublimitriscoComercial" Enabled="false" ReadOnly="true"  CssClass="form-control text-field number" MaxLength="18" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqsublimitriscoComercial" ControlToValidate="txtsublimitriscoComercial" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    </div>
                <div class="row form-group padding-row ">
                    <div class="col-sm-10 treViewCatalogo">
                        <asp:ListView ID="lvProdutosRiscoC" runat="server">
                            <EmptyDataTemplate>Não existem resultados para a pesquisa efetuada.</EmptyDataTemplate>
                            <LayoutTemplate>
                                <div class="Table Grid" id="tbSublimtesComerciais">
                                    <div class="Heading Color">
                                        <div class="Cell Grid col-6">
                                            <label>PROD</label>
                                        </div>
                                        <div class="Cell Grid col-6">
                                            <label>Família Produto</label>
                                        </div>
                                        <div class="Cell Grid col-6">
                                            <label>Sublimite Comprometido</label>
                                        </div>
                    
                                    </div>
                                    <div class="Row" runat="server" id="itemPlaceholder">
                                    </div>
                                 </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div class="Row <%# Container.DataItemIndex % 2 == 0 ? "Even" : "Odd" %>">
                                        <div class="Cell Grid col-6">
                                            <asp:Label runat="server" CssClass="text-center " ID="lbProduto" Text='<%# Eval("codfamiliaproduto") %>'></asp:Label>
                                        </div>
                                    <div class="Cell Grid col-6">
                                            <asp:Label runat="server" ID="lbsubproduto" Text='<%# Eval("familiaProduto") %>' ></asp:Label>
                                        </div>
                                    <div class="Cell Grid col-6">
                                            <asp:Textbox runat="server" ID="lbsublimiteComprometido" placeholder="0,00" CssClass="number text-center form-control text-field " MaxLength="18" Text='<%# Eval("sublimitecompremetido") %>'></asp:Textbox>
                                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqsublimiteComprometido" InitialValue="0" ValidationGroup="sublimitesValGrp" ControlToValidate="lbsublimiteComprometido" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
        </div>

        <div class="row titleAccordion" id="dvtitleAcordionRAssinatura" runat="server" onclick="fAccordionController();">
                <a id="A1" runat="server" class="accordion " title="Risco Comercial">Risco Assinatura</a>
        </div>

        <div id="div3" runat="server" class="row closeAccordion colorbck hidden ">
            <div class="col-sm-12">
                <div class="row form-group padding-row ">
                    <div class="col-sm-4">
                        <label id="bkRiscoSublimiteAss" runat="server" class="col-sm-4 text-right lbl">* Tipo de Risco Sublimite: </label>
                        <div class="col-sm-6">
                            <Label id="lbRiscoSublimiteAssinatura" runat="server" class="col-sm-8 text-right lbl">* Risco Assinatura: </Label>
                            <asp:TextBox ID="txtsublimiteriscoAssinatura" Enabled="false" ReadOnly="true"  CssClass="form-control text-field number text-center" MaxLength="18" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqsublimiteriscoAssinatura" ControlToValidate="txtsublimiteriscoAssinatura" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row form-group padding-row ">
                    <div class="col-sm-10 treViewCatalogo">
                        <asp:ListView ID="lvProdutosRiscoA" runat="server">
                            <EmptyDataTemplate>Não existem resultados para a pesquisa efetuada.</EmptyDataTemplate>
                            <LayoutTemplate>
                                <div class="Table Grid" id="tbSublimtesAssinatura">
                                    <div class="Heading Color">
                                        <div class="Cell Grid col-6">
                                            <label>PROD</label>
                                        </div>
                                        <div class="Cell Grid col-6">
                                            <label>Família Produto</label>
                                        </div>
                                        <div class="Cell Grid col-6">
                                            <label>Sublimite Comprometido</label>
                                        </div>
                    
                                        </div>
                                    <div class="Row" runat="server" id="itemPlaceholder">
                                    </div>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div class="Row <%# Container.DataItemIndex % 2 == 0 ? "Even" : "Odd" %>">
                                        <div class="Cell Grid col-6">
                                            <asp:Label runat="server" ID="lbProduto" CssClass="text-center" Text='<%# Eval("codfamiliaproduto") %>'></asp:Label>
                                        </div>
                                    <div class="Cell Grid col-6">
                                            <asp:Label runat="server" ID="lbsubproduto" Text='<%# Eval("familiaProduto") %>' ></asp:Label>
                                        </div>
                                    <div class="Cell Grid col-6">
                                            <asp:Textbox runat="server" ID="lbsublimiteComprometido" placeholder="0,00" CssClass="number text-center form-control text-field " MaxLength="18" Text='<%# Eval("sublimitecompremetido") %>'></asp:Textbox>
                                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqsublimiteComprometido" ValidationGroup="sublimitesValGrp" InitialValue="0" ControlToValidate="lbsublimiteComprometido" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
        </div>

        <div id="accoesfinais_criarlm24" runat="server">
                <div class="row text-right div-btns">
                    <asp:button id="btnCriar" CssClass="btns" runat="server" Text="Criar Sublimite" OnClick="btnCriar_Click" causesvalidation="true" ValidationGroup="sublimitesValGrp"></asp:button>
                    <asp:button id="btnSeguinte" CssClass="btns" runat="server" Text="Seguinte" Enabled="false" OnClick="btnSeguinte_Click" causesvalidation="true"></asp:button>
                </div>
        </div>
        </div>

    </form>
    <script src="scripts/multilinha.js"></script>
</body>
</html>
