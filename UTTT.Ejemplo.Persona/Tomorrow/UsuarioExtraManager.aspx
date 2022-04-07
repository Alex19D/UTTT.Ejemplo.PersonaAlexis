<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuarioExtraManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.UsuarioExtraManager" %>
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


    <section class="container">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
             </asp:ScriptManager>
            <div class="row">
                <div>
                    <p></p>
                </div>
                <div class="col-6">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True"></asp:Label>
                    <br />
                    <p></p>
                </div>
                <div class="col col-6"></div>
            </div>
            <div class="container">
                <div class="row">


                    <div class="col-2">Usuario:</div>
                    <div class="col-2">
                        <asp:TextBox ID="txtColonia" runat="server" Width="210px" ViewStateMode="Disabled"
                            onkeypress="return validaCurp(event);"></asp:TextBox>
                    </div>
                    <div class="col-12 col-xl-8">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RegularExpressionValidator
                             ID="revNombre" runat="server" ControlToValidate="txtColonia" ErrorMessage="*Usuario debe comenzar en Mayusc seguido de Mayusc/Minusc o numeros con 
                            un rango de 5-10"
                             ValidationExpression="(([(Á,É,Í,Ó,Ú)(A-Z)])(([(a-z)(á,é,í,ó,ú)(A-Z)(0-9)]){4,9}))">
                         </asp:RegularExpressionValidator>
                    </div>


                    <div class="col-2">Password:</div>
                    <div class="col-2">
                        <asp:TextBox ID="txtCalle" type="password" runat="server" Width="210px" ViewStateMode="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-12 col-xl-8">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RegularExpressionValidator
                             ID="revPass1" runat="server" ControlToValidate="txtCalle" ErrorMessage="*Password debe tener de 8-16 caracteres,con almenos un 
                            (num, Mayus/minusc, caracter Especial)"
                             ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,16})">
                         </asp:RegularExpressionValidator>
                    </div>


                    <div class="col-2">Confirmar Password:</div>
                    <div class="col-2">
                        <asp:TextBox ID="txtConfirm" type="password" runat="server" Width="210px" ViewStateMode="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-12 col-xl-8">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RegularExpressionValidator
                             ID="revPass2" runat="server" ControlToValidate="txtConfirm" ErrorMessage="*Password debe tener de 8-16 caracteres,con almenos un 
                            (num, Mayus/minusc, caracter Especial)"
                             ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,16})">
                         </asp:RegularExpressionValidator>
                    </div>

                    <div class="col-2">Perfil:</div>
                    <div class="col-2">
                        <asp:UpdatePanel ID="UP" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList
                                    ID="ddlSexo" class="btn btn-outline-dark" runat="server"
                                    Width="210px"
                                    OnSelectedIndexChanged="ddlSexo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlSexo" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-12 col-xl-8">
                        <p></p>
                    </div>


                    <div class="col-2">Status:</div>
                    <div class="col-2">
                        <asp:UpdatePanel ID="UP1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList
                                    ID="ddlPuesto" class="btn btn-outline-dark" runat="server"
                                    Width="210px"
                                    OnSelectedIndexChanged="ddlPuesto_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlPuesto" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-8"></div>


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
