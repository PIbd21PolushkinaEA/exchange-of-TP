<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormProducts.aspx.cs" Inherits="InternetShopWeb.FormProducts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="ButtonUpd" runat="server" Text="Обновить" OnClick="ButtonUpd_Click" />
            <asp:GridView ID="dataGridView" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                    <asp:CommandField ShowSelectButton="true" SelectText=">>" />
                    <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
                    <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                </Columns>
                <SelectedRowStyle BackColor="#CCCCCC" />
            </asp:GridView>
            <br />
            <asp:Button ID="ButtonBack" runat="server" Text="Вернуться" OnClick="ButtonBack_Click" />
            <br />
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAvailableList" TypeName="InternetShopImplementations.Implementations.ProductServiceDB"></asp:ObjectDataSource>
            <asp:DropDownList ID="DropDownListBrand" runat="server" Width="285px">
        </asp:DropDownList>
            <asp:Button ID="Buttonfilter" runat="server" Text="Применить" OnClick="Buttonfilter_Click" />
            <br />
        </div>
    </form>
</body>
</html>
