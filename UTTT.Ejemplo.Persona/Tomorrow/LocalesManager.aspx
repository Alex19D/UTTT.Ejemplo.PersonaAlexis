﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocalesManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.LocalesManager" Debug="false" %>
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
                <span class="navbar-brand mb-0 h1">Persona</span>
            </div>
        </nav>
    </div>
    <section class="container-fluid">
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


                    <div class="col-2">Tipo Local: </div>
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
                                <asp:AsyncPostBackTrigger ControlID="ddlSexo" EventName="SelectedIndexChanged"/>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-12 col-xl-8">
                        <p></p>
                    </div>


                    <div class="col-2">Clave Local</div>
                    <div class="col-2">
                        <asp:TextBox ID="txtClaveUnica" runat="server"
                            Width="210px" ViewStateMode="Disabled"
                            onkeypress="return validaNumeros(event);" MaxLength="3">
                        </asp:TextBox>
                    </div>
                    <div class="col-12 col-xl-8">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RangeValidator
                            ID="rvClaveUnica" runat="server" ControlToValidate="txtClaveUnica" ErrorMessage="*La clave Unica debe de estar entre 100 y 999"
                            MaximumValue="999" MinimumValue="100" Type="Integer">
                        </asp:RangeValidator>
                    </div>

                    <div class="col-2">Descripcion:</div>
                    <div class="col-2">
                        <asp:TextBox ID="txtCurp" runat="server" Width="210px"
                            ViewStateMode="Disabled"
                            onkeypress="return validaCurp(event);">
                        </asp:TextBox>
                    </div>
                    <div class="col-12 col-xl-8">
                    </div>


                    <div class="col-2">Fecha de Inauguracion: </div>
                    <div class="col-2">
                            <asp:TextBox ID="txtFechaNacimiento" runat="server" Width="210px" ></asp:TextBox>
                    </div>
                    <div class=" col-5 col-lg-3 col-xl-1">
                        <center>
                        <asp:ImageButton ID="imgFD" ImageUrl="https://img2.freepng.es/20181127/wkw/kisspng-computer-icons-calendar-date-clip-art-portable-net-annual-calendar-page-svg-png-icon-free-download-5bfdc582e1dc47.7282942115433578269251.jpg"
                            Width="30px" Height="30px"
                            ImageAlign="Bottom" runat="server" CausesValidation="false" />
                        <ajaxToolkit:CalendarExtender ID="Calendar1" runat="server" Format="MM/dd/yyyy" PopupPosition="BottomRight"
                            BehaviorID="Calendar1" PopupButtonID="imgFD" TargetControlID="txtFechaNacimiento"></ajaxToolkit:CalendarExtender>
                            </center>
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
