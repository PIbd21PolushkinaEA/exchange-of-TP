<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormMainClient.aspx.cs" Inherits="InternetShopWeb.FormMainClient" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        #form1 {
            height: 666px;
            width: 1067px;
        }
    </style>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="ButtonProduct" runat="server" Text="Доступные товары" OnClick="ButtonProduct_Click" />
        <asp:Button ID="ButtonReport" runat="server" Text="Отчет" OnClick="ButtonReport_Click" />&nbsp;<br />
        <asp:Button ID="ButtonCreateBuy" runat="server" Text="Выбрать покупку" OnClick="ButtonCreateBuy_Click" />
        <asp:Button ID="ButtonUpdBuy" runat="server" Text="Изменить покупку" OnClick="ButtonUpdBuy_Click" />
        <asp:Button ID="ButtonMakeReservation" runat="server" Text="Зарезервировать" OnClick="ButtonMakeReservation_Click" />
        <asp:Button ID="ButtonUpd" runat="server" Text="Обновить список" OnClick="ButtonUpd_Click" />
        <asp:GridView ID="dataGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:CommandField ShowSelectButton="true" SelectText=">>" />
                <asp:BoundField DataField="NameBuy" HeaderText="NameBuy" SortExpression="NameBuy" />
                <asp:BoundField DataField="SumOfChoosedProducts" HeaderText="Sum" SortExpression="SumOfChoosedProducts" />
                <asp:BoundField DataField="IsReserved" HeaderText="IsReserved" SortExpression="IsReserved" />
            </Columns>
            <SelectedRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="MakeReservation" InsertMethod="CreateBuy" SelectMethod="GetList" TypeName="InternetShopImplementations.Implementations.MainClientServiceDB" UpdateMethod="UpdBuy">
            <DeleteParameters>
                <asp:Parameter Name="id" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
