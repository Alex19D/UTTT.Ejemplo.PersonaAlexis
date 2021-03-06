<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuarioManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.UsuarioManager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                            <a class="nav-link">Empleados</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>
            <section class="container-fluid">
                <form id="form1" runat="server">
                    <div>
                        <p>
                            <br />
                            <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True"></asp:Label>
                            <br />
                        </p>
                        <p>
                            <span>
                                <asp:Label ID="lblPersona" runat="server" Text="Empleado: " Font-Bold="True"></asp:Label>
                                <asp:Label ID="txtPersona" runat="server" Text="Persona" Font-Bold="True"></asp:Label>
                            </span>
                        </p>
                        <br />
                        <p>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAgregar" runat="server" class="btn btn-outline-dark" Text="Agregar" OnClick="btnAgregar_Click" ViewStateMode="Disabled" />
                            &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnRegresar" class="btn btn-outline-info" runat="server" OnClick="btnRegresar_Click" Text="Regresar" />
                        </p>
                    </div>

                    <div>
                    </div>

                    <div class="row justify-content">
                        <div class="table-responsive">
                            <div>

                                <asp:GridView CssClass="table table-dark table-hover table-bordered table-hover"
                                    ID="dgvDireccion" runat="server" AutoGenerateColumns="False"
                                    DataSourceID="LinqDataSourceDireccion" Width="1062px" BackColor="White"
                                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    GridLines="Horizontal" OnRowCommand="dgvDireccion_RowCommand">
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True"
                                            SortExpression="id" Visible="False" />
                                        <asp:BoundField DataField="strUsuario" HeaderText="Usuario" ReadOnly="True"
                                            SortExpression="strUsuario" />
                                        <asp:BoundField DataField="CatPerfil" HeaderText="Perfil"
                                            SortExpression="CatPerfil" />

                                        <asp:BoundField DataField="CatStatus" HeaderText="Status"
                                            SortExpression="CatStatus" />
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


                    <asp:LinqDataSource ID="LinqDataSourceDireccion" runat="server"
                        ContextTypeName="UTTT.Ejemplo.Linq.Data.Entity.DcGeneralDataContext"
                        EntityTypeName="" Select="new (id, strUsuario, CatPerfil, CatStatus)"
                        TableName="Direccion" OnSelecting="LinqDataSourceDireccion_Selecting">
                    </asp:LinqDataSource>
                </form>
            </section>
</body>
</html>
