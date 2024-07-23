<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EFSP.Pages.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Management</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-datepicker@1.9.0/dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap-datepicker@1.9.0/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#<%= txtEditDob.ClientID %>').datepicker({
                format: 'yyyy-mm-dd',
                autoclose: true,
                todayHighlight: true
            });
            $('#<%= txtAddDob.ClientID %>').datepicker({
                format: 'yyyy-mm-dd',
                autoclose: true,
                todayHighlight: true
            });

            $('#btnShowAddForm').click(function () {
                $('#divAddEmployeeForm').toggle();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
        <div class="container">
            <h1>Employee Management</h1>
            <div id="AddEmployee" runat="server" visible="true">
                <div class="card mb-3">
                    <h3 class="card-header">Add New Employee</h3>
                    <div class="card-body" id="divAddEmployeeForm">
                        <asp:TextBox ID="txtAddName" runat="server" CssClass="form-control mb-3" placeholder="Enter employee name" />
                        <asp:TextBox ID="txtAddDob" runat="server" CssClass="form-control mb-3" placeholder="Select date of birth" />
                        <asp:DropDownList ID="ddlAddRole" runat="server" CssClass="form-control mb-3">
                            <asp:ListItem Text="Developer" Value="Developer"></asp:ListItem>
                            <asp:ListItem Text="Team Lead" Value="Team Lead"></asp:ListItem>
                            <asp:ListItem Text="Business Analyst" Value="Business Analyst"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnAddEmployee" runat="server" Text="Add Employee" OnClick="btnAddEmployee_Click" CssClass="btn btn-success" />
                    </div>
                </div>
            </div>
            <div id="Grid" runat="server" visible="true">
                <asp:GridView ID="gvEmployees" runat="server" DataKeyNames="emp_id" AutoGenerateColumns="False"
                    OnRowEditing="gvEmployees_RowEditing" OnRowDeleting="gvEmployees_RowDeleting"
                    OnRowCancelingEdit="gvEmployees_RowCancelingEdit" CssClass="table table-striped" AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="emp_id" HeaderText="Employee ID" ReadOnly="True" />
                        <asp:BoundField DataField="Emp_name" HeaderText="Name" />
                        <asp:TemplateField HeaderText="Date of Birth">
                            <ItemTemplate>
                                <asp:Label ID="lblDOB" runat="server" Text='<%# Bind("DOB", "{0:dd MMM yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditDob" runat="server" CssClass="form-control" Text='<%# Bind("DOB", "{0:yyyy-MM-dd}") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Role">
                            <ItemTemplate>
                                <asp:Label ID="lblRole" runat="server" Text='<%# Bind("Emp_Role") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlEditRole" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Developer" Value="Developer"></asp:ListItem>
                                    <asp:ListItem Text="Team Lead" Value="Team Lead"></asp:ListItem>
                                    <asp:ListItem Text="Business Analyst" Value="Business Analyst"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Text="Edit" class="btn btn-info" />
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" Text="Delete" class="btn btn-danger" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div id="divEditEmployee" runat="server" visible="false">
                <h3>Edit Employee</h3>
                <asp:HiddenField ID="hdnEditEmployeeId" runat="server" />
                <asp:TextBox ID="txtEditName" runat="server" CssClass="form-control mb-3" placeholder="Enter employee name" />
                <asp:TextBox ID="txtEditDob" runat="server" CssClass="form-control mb-3" placeholder="Select date of birth" />
                <asp:DropDownList ID="ddlEditRole" runat="server" CssClass="form-control mb-3">
                    <asp:ListItem Text="Developer" Value="Developer"></asp:ListItem>
                    <asp:ListItem Text="Team Lead" Value="Team Lead"></asp:ListItem>
                    <asp:ListItem Text="Business Analyst" Value="Business Analyst"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnUpdate" runat="server" Text="Update Employee" OnClick="btnUpdate_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" />
            </div>
        </div>
    </form>
</body>
</html>

