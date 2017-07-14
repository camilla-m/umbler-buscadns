<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Resultado.aspx.cs" Inherits="_Default" %>
<%@ PreviousPageType VirtualPath="~/Default.aspx" %> 

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resultado da busca</title>
    <link rel="stylesheet" type="text/css" href="CSS/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="CSS/main.css" />
</head>
<body>
    <form id="resultado" runat="server" margin="auto"> 
    <div>
    <p><img src="Images/umbler-logo.png" width="200" /></p>
    <h1>Busca de DNS</h1>
    <p>Seguem os resultados de sua pesquisa. Você pode salvar os dados para futuras buscas ou tentar novamente.</p>
        <blockquote>Lembre-se que alguns IP's são criptografados e podem não aparecer da maneira correta.</blockquote>
    <p>O hostname é: <asp:Label ID="hostname" runat="server" class="label"></asp:Label>
        </p>
    <p>Os nameservers são:</p>
    <p><asp:Label ID="nameserver" runat="server"></asp:Label></p>
    <p>O IP é: <asp:Label ID="ipaddress" runat="server" class="label"></asp:Label></p>
    </div>
        <p>
    <asp:Button id="salvar" PostBackUrl="~/Default.aspx" runat="server" text="Salvar consulta" CausesValidation="False" onclick="Salvar" OnClientClick="Salvar" />
    <asp:Button id="voltar" PostBackUrl="~/Default.aspx" runat="server" text="Voltar" CausesValidation="False" OnClientClick="JavaScript:window.history.back(1);return false;" />
        </p>

    </form>
</body>
</html>
