<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormClient.aspx.cs" Inherits="InternetShopWeb.FormClient" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 241px">

            <br />
            Имя&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="textBoxName" runat="server" Height="16px" Width="280px">Zhenya</asp:TextBox>
            <br />
            <br />
            Почта&nbsp;&nbsp;&nbsp; <asp:TextBox ID="textBoxEmail" runat="server" Height="16px" Width="280px">polushkina.evg@gmail.com</asp:TextBox>
            <br />
            <br />
            Пароль&nbsp;
        <asp:TextBox ID="textBoxPassword" runat="server" Height="16px" Width="280px">123</asp:TextBox>
            <br />
            <br />
            <asp:Button ID="RegistrationButton" runat="server" OnClick="RegistrationButton_Click" Text="Зарегестрироваться" />
            <asp:Button ID="SignInButton" runat="server" OnClick="SignInButton_Click" Text="Войти" />

        </div>
    </form>
</body>
</html>
