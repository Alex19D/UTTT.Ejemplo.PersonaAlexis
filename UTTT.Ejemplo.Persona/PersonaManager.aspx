<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonaManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.PersonaManager" Debug="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">
        function validaNumeros(evt) {
            var code = (evt.which) ? evt.which : evt.keyCode;
            if (code == 8) {
                return true;
            } else if (code >= 48 && code <= 57) {
                return true;
            } else {
                return false;
            }
        }

        function validaLetras(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "áéíóúabcdefghijklmnñopqrstuvwxyz";
            especiales = "8-37-39-46";
            tecla_especial = false;
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }
            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
        }

        function validaCurp(ex) {
            key = ex.keyCode || ex.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "abcdefghijklmnñopqrstuvwxyz012345689";
            especiales = "8-37-39-46";
            tecla_especial = false;
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }
            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
        }
    </script>
</head>
<body>
    <div class="col-md-12">
        <nav class="navbar navbar-dark bg-dark">
            <div class="container-fluid">
                <span class="navbar-brand mb-0 h1">Persona</span>
            </div>
        </nav>
    </div>
    <section class="container-fluid">
        <form id="form1" runat="server">
            <div class="row">
                <div class="col-6">
                    <center>
                        <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True"></asp:Label>
                        <p></p>
                    </center>
                </div>
                <div class="col col-6"></div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-2">Sexo:</div>
                    <div class="col-2">
                        <asp:DropDownList
                            ID="ddlSexo" class="btn btn-outline-dark" runat="server"
                            Width="210px"
                            OnSelectedIndexChanged="ddlSexo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-8"></div>


                    <div class="col-2">Clave Unica </div>
                    <div class="col-2">
                        <asp:TextBox ID="txtClaveUnica" runat="server"
                            Width="210px" ViewStateMode="Disabled"
                            onkeypress="return validaNumeros(event);" MaxLength="3">
                        </asp:TextBox>
                    </div>
                    <div class="col-8">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RangeValidator
                            ID="rvClaveUnica" runat="server" ControlToValidate="txtClaveUnica" ErrorMessage="*La clave Unica debe de estar entre 100 y 999"
                            MaximumValue="999" MinimumValue="100" Type="Integer">
                        </asp:RangeValidator>
                    </div>


                    <div class="col-2">Nombre </div>
                    <div class="col-2">
                        <asp:TextBox
                            ID="txtNombre" runat="server" Width="210px" ViewStateMode="Disabled"
                            onkeypress="return validaLetras(event);">
                        </asp:TextBox>
                    </div>
                    <div class="col-8">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:RegularExpressionValidator
                             ID="revNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="*El nombre debe comenzar en Mayusculas seguido de minusculas con 
                            un rango de 3-9 Letras"
                             ValidationExpression="(([(Á,É,Í,Ó,Ú)(A-Z)])(([(a-z)(á,é,í,ó,ú)]){2,8}))">
                         </asp:RegularExpressionValidator>
                    </div>


                    <div class="col-2">A Paterno: </div>
                    <div class="col-2">
                        <asp:TextBox
                            ID="txtAPaterno" runat="server" Width="210px" ViewStateMode="Disabled"
                            onkeypress="return validaLetras(event);">
                        </asp:TextBox>
                    </div>
                    <div class="col-8">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RegularExpressionValidator ID="revAPaterno" runat="server" ControlToValidate="txtAPaterno" ErrorMessage="*El APaterno debe comenzar 
                            en Mayusculas seguido de minusculas con un rango de 5-12 Letras"
                            ValidationExpression="(([(Á,É,Í,Ó,Ú)(A-Z)])(([(a-z)(á,é,í,ó,ú)]){4,11}))">
                        </asp:RegularExpressionValidator>
                    </div>


                    <div class="col-2">A Materno: </div>
                    <div class="col-2">
                        <asp:TextBox ID="txtAMaterno" runat="server" Width="210px"
                            ViewStateMode="Disabled"
                            onkeypress="return validaLetras(event);">
                        </asp:TextBox>
                    </div>
                    <div class="col-8">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RegularExpressionValidator ID="revAMaterno" runat="server" ControlToValidate="txtAMaterno" ErrorMessage="*El AMaterno debe comenzar en 
                            Mayusculas seguido de minusculas con un rango de 5-12 Letras"
                            ValidationExpression="(([(Á,É,Í,Ó,Ú)(A-Z)])(([(a-z)(á,é,í,ó,ú)]){4,11}))">
                        </asp:RegularExpressionValidator>
                    </div>


                    <div class="col-2">CURP: </div>
                    <div class="col-2">
                        <asp:TextBox ID="txtCurp" runat="server" Width="210px"
                            ViewStateMode="Disabled"
                            onkeypress="return validaCurp(event);">
                        </asp:TextBox>
                    </div>
                    <div class="col-8">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RegularExpressionValidator
                            ID="revCurp" runat="server" ControlToValidate="txtCurp" ErrorMessage="*La CURP es incorrecta"
                            ValidationExpression="^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$">
                        </asp:RegularExpressionValidator>
                    </div>
                </div>
                <p><br /></p>
                <div class="row">
                    <div class="col-4">
                        <center>
                            <asp:Button ID="btnAceptar" class="btn btn-outline-success" runat="server" Text="Aceptar"
                            OnClick="btnAceptar_Click" ViewStateMode="Disabled" />

                        <asp:Button ID="btnCancelar" class="btn btn-outline-warning" runat="server" Text="Cancelar" CausesValidation="false"
                            OnClick="btnCancelar_Click" ViewStateMode="Disabled" />
                        </center>
                    </div>
                    <div class="col-8">
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="#CC0000" Text="..." Visible="False"></asp:Label>
                    </div>
                </div>
            </div>
        </form>
    </section>
</body>
</html>
