<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DireccionExtraLManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.DireccionExtraLManager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
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
            letras = "áéíóúabcdefghijklmnñopqrstuvwxyz ";
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
            letras = "abcdefghijklmnñopqrstuvwxyz012345689 ";
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
                <span class="navbar-brand mb-0 h1">Dirección</span>
            </div>
        </nav>
    </div>
    <section class="container">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
             </asp:ScriptManager>
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



                    <div class="col-2"> Colonia: </div>
                    <div class="col-2">
                        <asp:TextBox ID="txtColonia" onkeypress="return validaLetras(event);" runat="server" Width="210px" ViewStateMode="Disabled" />
                    </div>
                    <div class="col-12 col-xl-8">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RegularExpressionValidator
                             ID="revNombre" runat="server" ControlToValidate="txtColonia" ErrorMessage="*Colonia Empieza con mayusculas, 
                            rango (3-15 caracteres)"
                             ValidationExpression="(([(Á,É,Í,Ó,Ú)(A-Z)])(([(a-z)(á,é,í, ,ó,ú)(A-Z)]){2,14}))">
                         </asp:RegularExpressionValidator>
                    </div>


                    <div class="col-2">Calle: </div>
                    <div class="col-2">
                        <asp:TextBox ID="txtCalle" onkeypress="return validaCurp(event);" runat="server" Width="210px" ViewStateMode="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-8">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:RegularExpressionValidator
                             ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCalle" ErrorMessage="*Colonia Empieza con mayusculas o numero, 
                            rango (3-15 caracteres)"
                             ValidationExpression="(([(0-9)(A-Z)])(([(0-9)(a-z)(A-Z)(á,é,í, ,ó,ú)]){2,14}))">
                         </asp:RegularExpressionValidator>
                    </div>


                    <div class="col-2">Número:</div>
                    <div class="col-2">
                        <asp:TextBox ID="txtNumero" onkeypress="return validaNumeros(event);" runat="server" Width="210px" ViewStateMode="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-8">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       <asp:RegularExpressionValidator
                             ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNumero" ErrorMessage="*Solo numeros, 
                            rango (1-99)"
                             ValidationExpression="(([(0-9)]){1,2})">
                         </asp:RegularExpressionValidator>
                    </div>


                    <div class="col-2">Codigo Postal: </div>
                    <div class="col-2">
                        <asp:TextBox ID="txtCP" onkeypress="return validaNumeros(event);" runat="server" Width="210px" ViewStateMode="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-8">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RegularExpressionValidator
                             ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtCP" ErrorMessage="*Solo numeros con rango 5 digitos"
                             ValidationExpression="(([(0-9)]){5})">
                         </asp:RegularExpressionValidator>
                    </div>


                  
                </div>
                <p>
                    <br />
                </p>
                <div class="row">
                    <div class="col-4">
                        <center>
                            <asp:Button ID="btnAceptar" class="btn btn-outline-success" runat="server" Text="Aceptar"
                                OnClick="btnAceptar_Click" ViewStateMode="Disabled" />

                            <asp:Button ID="btnCancelar" class="btn btn-outline-warning" runat="server" Text="Cancelar"
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
