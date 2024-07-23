<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="Linq.Pages.Employees" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employees</title>
    <style>
        .gridview-container {
            margin: 20px auto;
            width: 80%;
            padding: 20px;
            background-color: #ffffff;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
    </style>
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
        function editEmployee(empId) {
            window.location.href = 'EditEmployee.aspx?empId=' + empId;
        }

        function confirmDelete(empId) {
            if (confirm('Are you sure you want to delete this employee?')) {
                deleteEmployee(empId);
            }
        }

        function deleteEmployee(empId) {
            PageMethods.DeleteEmployee(empId, onDeleteSuccess, onDeleteError);
        }

        function onDeleteSuccess() {
            window.location.reload();
        }

        function onDeleteError(error) {
            console.error('Failed to delete employee: ' + error.get_message());
        }

        function BindEmployees() {
            PageMethods.BindedEmployees(onBindSuccess, onBindError);
        }

        function onBindSuccess(result) {
            var gvEmployees = document.getElementById('<%= gvEmployees.ClientID %>');
            if (gvEmployees) {
                gvEmployees.innerHTML = result;
            }
        }

        function onBindError(error) {
            console.error('Failed to rebind employees: ' + error.get_message());
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="gridview-container">
            <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" OnRowCommand="gvEmployees_RowCommand" DataKeyNames="emp_id" CssClass="table table-striped">
                <Columns>
                    <asp:BoundField DataField="emp_id" HeaderText="Employee ID" />
                    <asp:BoundField DataField="Emp_name" HeaderText="Employee Name" />
                    <asp:BoundField DataField="DOB" HeaderText="DOB" DataFormatString="{0:dd MMM yyyy}" />
                    <asp:BoundField DataField="Emp_Role" HeaderText="Employee Role" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <button type="button" class="action-button" onclick='editEmployee(<%# Eval("emp_id") %>)'>Edit</button>
                            <button type="button" class="action-button" onclick='confirmDelete(<%# Eval("emp_id") %>)'>Delete</button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="btnAddEmployee" runat="server" Text="Add Employee" OnClick="btnAddEmployee_Click" />
            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
