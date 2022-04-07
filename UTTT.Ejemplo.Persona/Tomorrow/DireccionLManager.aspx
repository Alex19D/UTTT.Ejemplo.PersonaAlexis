<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DireccionLManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.DireccionLManager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <header>
    <div class="col-100%">
        <nav class="navbar navbar-dark bg-dark">
            <div class="container-fluid">
                <span class="navbar-brand mb-0 h1" lang="bal">Dirección</span>
                
            </div>
        </nav>
    </div>
    <header>
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
                            <asp:Label ID="lblPersona" runat="server" Text="Local con clave: " Font-Bold="True"></asp:Label>
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
                                <asp:BoundField DataField="strColonia" HeaderText="Colonia" ReadOnly="True"
                                    SortExpression="strColonia" />
                                <asp:BoundField DataField="strCalle" HeaderText="Calle" ReadOnly="True"
                                    SortExpression="strCalle" />
                                <asp:BoundField DataField="strNumero" HeaderText="Número" ReadOnly="True"
                                    SortExpression="strNumero" />
                                <asp:BoundField DataField="strCP" HeaderText="Codigo Postal" ReadOnly="True"
                                    SortExpression="strCP" />
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
                EntityTypeName="" Select="new (id, strCalle, strNumero, strColonia, strCP)" 
                TableName="Direccion" onselecting="LinqDataSourceDireccion_Selecting">
            </asp:LinqDataSource>
        </form>
    </section>
</body>
</html>
