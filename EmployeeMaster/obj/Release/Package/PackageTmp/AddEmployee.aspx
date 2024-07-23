<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEmployee.aspx.cs" Inherits="EmployeeMaster.AddEmployee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Employee</title>
    <link rel="stylesheet" href="~/Styles/styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Add Employee</h2>
            <div>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter Name"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                    ErrorMessage="Name is required." Text="*" ForeColor="Red" ValidationGroup="AddEmployeeValidation" />
            </div>
            <div>
                <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" placeholder="Enter Date of Birth (DD/MM/YYYY)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDOB" runat="server" ControlToValidate="txtDOB"
                    ErrorMessage="DOB is required." Text="*" ForeColor="Red" ValidationGroup="AddEmployeeValidation" />
                <asp:RegularExpressionValidator ID="revDOB" runat="server" ControlToValidate="txtDOB"
                    ErrorMessage="Invalid date format. Please use DD/MM/YYYY." ValidationExpression="\d{2}/\d{2}/\d{4}"
                    ForeColor="Red" ValidationGroup="AddEmployeeValidation" />
            </div>
            <div>
                <asp:TextBox ID="txtRole" runat="server" CssClass="form-control" placeholder="Enter Role"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnAdd" runat="server" Text="Add Employee" OnClick="btnAdd_Click" CssClass="btn btn-primary" ValidationGroup="AddEmployeeValidation" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-default" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" CausesValidation="false" />
            </div>
        </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="AddEmployeeValidation" ForeColor="Red" />
    </form>
</body>
</html>
