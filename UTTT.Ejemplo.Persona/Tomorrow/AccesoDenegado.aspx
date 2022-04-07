<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccesoDenegado.aspx.cs" Inherits="UTTT.Ejemplo.Persona.Tomorrow.AccesoDenegado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Acceso Denegado</title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1><strong><span style="background-color:#F3F361">No tienes Permisos para ver esta pagina</span></strong></h1>
                    <asp:Button class="btn btn-outline-dark" ID="btnAgregar" runat="server"  Text="Regresar a Pantalla Principal" 
                        ViewStateMode="Disabled" OnClick="btnAgregar_Click" />
        </div>
        <div>
            <img src="https://previews.123rf.com/images/dinozzz/dinozzz1202/dinozzz120200051/12484987-acceso-denegado-grunge-sello-de-goma-ilustraci%C3%B3n-vectorial.jpg"
                width="1350" height="500"/>
        </div>
    </form>
</body>
</html>
