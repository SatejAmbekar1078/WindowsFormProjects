<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditEmployee.aspx.cs" Inherits="Linq.Pages.EditEmployee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Employee</title>
    <link rel="stylesheet" href="~/Styles/styles.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <script>
        $(function () {
            $("#tbDOB").datepicker({
                dateFormat: 'yy-mm-dd',
                changeMonth: true,
                changeYear: true
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>
                <asp:Literal ID="litFormTitle" runat="server"></asp:Literal></h2>
            <asp:HiddenField ID="hfEmpId" runat="server" />
            <asp:Label ID="lblEmpId" runat="server" Text="Employee ID:" AssociatedControlID="tbEmpId" Visible="false"></asp:Label>
            <asp:TextBox ID="tbEmpId" runat="server" Visible="false"></asp:TextBox>
            <br />
            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
            <br />
            <asp:Label ID="lblEmpName" runat="server" Text="Employee Name:" AssociatedControlID="tbEmpName"></asp:Label>
            <asp:TextBox ID="tbEmpName" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblDOB" runat="server" Text="DOB:" AssociatedControlID="tbDOB"></asp:Label>
            <asp:TextBox ID="tbDOB" runat="server"></asp:TextBox>
            <br />
            <asp:DropDownList ID="ddlEmpRole" runat="server">
                <asp:ListItem Text="Developer" Value="Developer"></asp:ListItem>
                <asp:ListItem Text="Team Lead" Value="Team Lead"></asp:ListItem>
                <asp:ListItem Text="Business Analyst" Value="Business Analyst"></asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Visible="false" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" Visible="false" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" Visible="false" />
        </div>
    </form>
</body>
</html>
