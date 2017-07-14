<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Busca de DNS</title>
    <link rel="stylesheet" type="text/css" href="CSS/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="CSS/main.css" />
</head>
<body>
    <form id="formurl" runat="server" margin="auto"> 
    <div>
    <p><img src="Images/umbler-logo.png" class="imglogo"></p>
    <h1>Busca de DNS</h1>
        <p>Olá! Seja bem-vindo(a) à busca de DNS da Umbler. Coloque uma URL com http ou https e confira os nameservers e IP do link que digitou.</p>
        <asp:TextBox id="url" runat="server" value="http://" />
        <asp:Button id="enviar" PostBackUrl="~/Resultado.aspx" runat="server" text="Enviar"/>
    </div>
    </form>
</body>
</html>
