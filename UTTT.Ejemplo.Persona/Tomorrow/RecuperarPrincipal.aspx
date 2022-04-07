<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperarPrincipal.aspx.cs" Inherits="UTTT.Ejemplo.Persona.RecuperarPrincipal" Debug="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AlexD1921</title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <header>
        <nav class="navbar navbar-expand-lg navbar-expand navbar-dark bg-dark">
            <div class="container-fluid">
                <a class="navbar-brand">El Mañana</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav">
                        <li class="nav-item">
                            <a class="nav-link active" aria-current="page">
                                Recuperar Contraseña
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="LogIn.aspx">Regresar</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>
    <section class="container-fluid">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
                </asp:ScriptManager>
            <div>
                <p>
                </p>
                <br />
                <p>
                    Usuario:&nbsp;&nbsp;&nbsp;&nbsp;

        <asp:TextBox ID="txtNombre" runat="server"  Width="177px"
            ViewStateMode="Disabled" OnTextChanged="buscarTextBox" AutoPostBack="True"></asp:TextBox>
                    <AjaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender" runat="server" CompletionInterval="100" EnableCaching="false" 
                        MinimumPrefixLength="2" ServiceMethod="txtNombre_TextChanged"
                        TargetControlID="txtNombre">
                    </AjaxToolkit:AutoCompleteExtender>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button class="btn btn-outline-primary" ID="btnBuscar" runat="server" Text="Buscar"
            OnClick="btnBuscar_Click" ViewStateMode="Disabled" />
                    &nbsp;&nbsp;&nbsp;
                </p>
                <asp:Label ID="lblInstrucc" runat="server" Text="" Visible="False"></asp:Label>
            </div>

            <div style="font-weight: bold">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                <asp:Label ID="lblEnc" runat="server" Text="" Visible="False"></asp:Label>
                <p></p>
            </div>
            

            <div class="row justify-content">
                <div class="table-responsive">
                    <div>

                        <asp:GridView CssClass="table table-dark table-hover table-bordered table-hover" 
                            ID="dgvPersonas" runat="server"
                            AllowPaging="True" AutoGenerateColumns="False" DataSourceID="DataSourcePersona"
                            Width="1067px" CellPadding="3" GridLines="Horizontal"
                            OnRowCommand="dgvPersonas_RowCommand" BackColor="red"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"
                            ViewStateMode="Disabled">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>

                                <asp:BoundField DataField="strUsuario" HeaderText="Usuario" ReadOnly="True"
                                            SortExpression="strUsuario" />

                                        <asp:BoundField DataField="CatPerfil" HeaderText="Perfil"
                                            SortExpression="CatPerfil" />

                                        <asp:BoundField DataField="CatStatus" HeaderText="Status"
                                            SortExpression="CatStatus" />


                                <asp:TemplateField HeaderText="Recuperar">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgEditar" CommandName="Editar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/pass.png" Width="20px" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />

                                </asp:TemplateField>
                                

                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" />
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle BackColor="#E7E7FF" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <SortedAscendingCellStyle BackColor="#F4F4FD" />
                            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                            <SortedDescendingCellStyle BackColor="#D8D8F0" />
                            <SortedDescendingHeaderStyle BackColor="#3E3277" />
                        </asp:GridView>

                    </div>
                </div>
            </div>


            <asp:LinqDataSource ID="DataSourcePersona" runat="server"
                ContextTypeName="UTTT.Ejemplo.Linq.Data.Entity.DcGeneralDataContext"
                OnSelecting="DataSourcePersona_Selecting"
                Select="new (id, strUsuario, CatPerfil, CatStatus)"
                TableName="Persona" EntityTypeName="">
            </asp:LinqDataSource>
        </form>
    </section>
</body>
</html>
