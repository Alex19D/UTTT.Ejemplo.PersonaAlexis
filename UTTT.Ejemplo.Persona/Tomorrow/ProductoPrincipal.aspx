<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductoPrincipal.aspx.cs" Inherits="UTTT.Ejemplo.Persona.Tomorrow.ProductoPrincipal" Debug="false" %>
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
                                <asp:Label ID="lblPerfil" runat="server" Text="..." Visible="False" ForeColor="White"></asp:Label>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link">Productos</a>
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
            <div class="col col-12">
                <p>
                </p>
                <br />
                <p>
                    Nombre:&nbsp;&nbsp;&nbsp;&nbsp;

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
        <asp:Button class="btn btn-outline-info" ID="btnRegresar" runat="server"  Text="Regresar"
            ViewStateMode="Disabled" OnClick="btnRegresar_Click" />
                </p>
            </div>
                <p>
                    <br />
                    <asp:Button class="btn btn-outline-dark" ID="btnAgregar" runat="server"  Text="Agregar"
                        OnClick="btnAgregar_Click" ViewStateMode="Disabled" />
                </p>
            <div style="font-weight: bold">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Detalle<p></p>
            </div>
            <div>
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
                                
                                <asp:BoundField DataField="strClaveProducto" HeaderText="ClaveProducto" ReadOnly="True"
                                    SortExpression="strClaveProducto" />
                                <asp:BoundField DataField="strNombre" HeaderText="Nombre" ReadOnly="True"
                                    SortExpression="strNombre" />
                                <asp:BoundField DataField="Precio" HeaderText="$ Precio $" ReadOnly="True"
                                    SortExpression="Precio" />

                                <asp:BoundField DataField="CatTipo" HeaderText="Tipo"
                                    SortExpression="CatTipo" />
                                
                                <asp:BoundField DataField="strDescripcion" HeaderText="Descripcion" ReadOnly="True"
                                    SortExpression="strDescripcion" />
                                <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgEditar" CommandName="Editar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/editrecord_16x16.png" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar" Visible="True">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgEliminar" CommandName="Eliminar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/delrecord_16x16.png" OnClientClick="javascript:return confirm('¿Está seguro de querer eliminar el registro seleccionado?', 'Mensaje de sistema')" />
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
                Select="new (strClaveProducto, strNombre, Precio,  CatTipo,id,  strDescripcion)"
                TableName="Persona" EntityTypeName="">
            </asp:LinqDataSource>
        </form>
    </section>
</body>
</html>
