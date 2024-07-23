<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditEmployee.aspx.cs" Inherits="EmployeeMaster.EditEmployee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <link rel="stylesheet" href="~/Styles/styles.css" />
    <title>Edit Employee</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div>
            <h2>Edit Employee</h2>
            <div>
                <asp:Label ID="lblEmpId" runat="server" Visible="false"></asp:Label> <!-- Hidden field to store Emp_id -->
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter Name"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                    ErrorMessage="Name is required." Text="*" ForeColor="Red" ValidationGroup="EditEmployeeValidation" />
            </div>
            <div>
                <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" placeholder="Enter Date of Birth (DD/MM/YYYY)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDOB" runat="server" ControlToValidate="txtDOB"
                    ErrorMessage="DOB is required." Text="*" ForeColor="Red" ValidationGroup="EditEmployeeValidation" />
                <asp:RegularExpressionValidator ID="revDOB" runat="server" ControlToValidate="txtDOB"
                    ErrorMessage="Invalid date format. Please use DD/MM/YYYY." ValidationExpression="\d{2}/\d{2}/\d{4}"
                    ForeColor="Red" ValidationGroup="EditEmployeeValidation" />
            </div>
            <div>
                <asp:TextBox ID="txtRole" runat="server" CssClass="form-control" placeholder="Enter Role"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn btn-primary" ValidationGroup="EditEmployeeValidation" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-default" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" CausesValidation="false" />
            </div>
        </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="EditEmployeeValidation" ForeColor="Red" />
    </form>
</body>
</html>
