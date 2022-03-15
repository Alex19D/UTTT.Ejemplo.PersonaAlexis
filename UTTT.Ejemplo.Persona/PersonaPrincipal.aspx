<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonaPrincipal.aspx.cs" Inherits="UTTT.Ejemplo.Persona.PersonaPrincipal" Debug="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AlexD1921</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <header>
    <div class="col-100%">
        <nav class="navbar navbar-dark bg-dark">
            <div class="container-fluid">
                <span class="navbar-brand mb-0 h1" lang="bal">Persona</span>
                
            </div>
        </nav>
    </div>
    <header>
    <section class="container-fluid">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
                </asp:ScriptManager>
            <div>
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
        <asp:Button class="btn btn-outline-dark" ID="btnAgregar" runat="server"  Text="Agregar"
            OnClick="btnAgregar_Click" ViewStateMode="Disabled" />
                </p>
            </div>
            <div>
                <div>
                    Sexo :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        <asp:DropDownList  class="btn btn-outline-dark" data-bs-toggle="dropdown" aria-expanded="false"
            ID="ddlSexo" runat="server" Width="177px">
        </asp:DropDownList>
                    <p></p>
                </div>

            </div>

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
                                <asp:BoundField DataField="strClaveUnica" HeaderText="Clave Unica"
                                    ReadOnly="True" SortExpression="strClaveUnica" />
                                <asp:BoundField DataField="strNombre" HeaderText="Nombre" ReadOnly="True"
                                    SortExpression="strNombre" />
                                <asp:BoundField DataField="strAPaterno" HeaderText="APaterno" ReadOnly="True"
                                    SortExpression="strAPaterno" />
                                <asp:BoundField DataField="strAMaterno" HeaderText="AMaterno" ReadOnly="True"
                                    SortExpression="strAMaterno" />
                                <asp:BoundField DataField="CatSexo" HeaderText="Sexo"
                                    SortExpression="CatSexo" />
                                <asp:BoundField DataField="strCurp" HeaderText="CURP" ReadOnly="True"
                                    SortExpression="strCurp" />
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

                                <asp:TemplateField HeaderText="Direccion">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgDireccion" CommandName="Direccion" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/editrecord_16x16.png" />
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
                Select="new (strNombre, strAPaterno, strAMaterno, CatSexo, strClaveUnica,id, strCurp)"
                TableName="Persona" EntityTypeName="">
            </asp:LinqDataSource>
        </form>
    </section>
</body>
</html>
