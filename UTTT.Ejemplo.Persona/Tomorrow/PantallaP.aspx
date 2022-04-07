<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PantallaP.aspx.cs" Inherits="UTTT.Ejemplo.Persona.Tomorrow.PantallaP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <title></title>
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
                            <a class="nav-link">Pantalla Principal</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="LogIn.aspx">Cerrar Sesion</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>

    <div>
        <form id="form1" runat="server">
        <div>
            <br />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnProductos" type="button" class="btn btn-outline-dark" runat="server" OnClick="btnProductos_Click" Text="Productos" Visible="False" />
            <asp:Button ID="btnEmpleados" type="button" class="btn btn-outline-dark" runat="server" Text="Empleados" Visible="False" OnClick="btnEmpleados_Click" />
            <asp:Button ID="btnLocales" type="button" class="btn btn-outline-dark" runat="server" Text="Locales" Visible="False" OnClick="btnLocales_Click" />
            <br />
            <br />
        </div>
    </form>
    </div>
</body>
</html>
