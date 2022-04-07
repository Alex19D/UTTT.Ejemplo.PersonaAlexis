<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="UTTT.Ejemplo.Persona.LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../css/LogIn.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body background="../Images/Fondo4.jpg">
    <div class="login-box">
        <img src="../Images/logo.png" class="avatar" alt="Avatar Image" />
        <h1>Iniciar Sesión</h1>
        <form id="form1" runat="server">

            <!-- nombre de usuario -->
            <label for="username">Usuario:</label>
            <asp:TextBox ID="txtUsuario" runat="server" placeholder="Ingresa tu usuario"></asp:TextBox>
            <!-- contraseña-->
            <label for="password">Contraseña:</label>
            <asp:TextBox ID="txtPassword" runat="server" type="password" placeholder="Ingresa tu password"></asp:TextBox>
            <!-- button -->
            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
        </form>
         <a href="RecuperarPrincipal.aspx">¿Perdieste tu Contraseña? ¡¡¡Recuperala ahora!!!</a><br/><br/>
        <a><asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Text="* Aqui va el error" Visible="false"></asp:Label></a>
    </div>
</body>
</html>