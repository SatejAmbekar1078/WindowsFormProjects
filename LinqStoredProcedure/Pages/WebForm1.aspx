<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="LinqStoredProcedure.Pages.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True"></asp:CommandField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DotNetConnectionString %>" DeleteCommand="DeleteEmployee" DeleteCommandType="StoredProcedure" InsertCommand="AddEmployee" InsertCommandType="StoredProcedure" ProviderName="<%$ ConnectionStrings:DotNetConnectionString.ProviderName %>" SelectCommand="GetEmployees" SelectCommandType="StoredProcedure" UpdateCommand="UpdateEmployee" UpdateCommandType="StoredProcedure">
                <DeleteParameters>
                    <asp:Parameter Name="emp_id" Type="Int32"></asp:Parameter>
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Emp_name" Type="String"></asp:Parameter>
                    <asp:Parameter DbType="Date" Name="DOB"></asp:Parameter>
                    <asp:Parameter Name="Emp_Role" Type="String"></asp:Parameter>
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="emp_id" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="Emp_name" Type="String"></asp:Parameter>
                    <asp:Parameter DbType="Date" Name="DOB"></asp:Parameter>
                    <asp:Parameter Name="Emp_Role" Type="String"></asp:Parameter>
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
        <div>&nbsp;</div>
    </form>
</body>
</html>
