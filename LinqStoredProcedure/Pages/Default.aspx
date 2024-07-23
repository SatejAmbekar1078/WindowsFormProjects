<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LinqStoredProcedure.Pages.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Master</title>
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

        $(function () {
            $('#btnSearch').click(function () {
                var searchText = $('#txtSearch').val().toLowerCase();
                $('#GridView1 tr:gt(0)').each(function () {
                    var name = $(this).find('td:eq(1)').text().toLowerCase();
                    if (name.includes(searchText)) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }
                });
            });
        });
    </script>
    <style>
        .grid {
            margin-top: 20px;
        }

        .gridview-pager {
            margin-top: 10px;
            text-align: right;
        }

            .gridview-pager a {
                display: inline-block;
                padding: 5px 10px;
                margin-right: 5px;
                border: 1px solid #ccc;
                background-color: #f0f0f0;
                color: #333;
                text-decoration: none;
                border-radius: 3px;
            }

                .gridview-pager a:hover {
                    background-color: #e0e0e0;
                }

            .gridview-pager .current {
                background-color: #007bff;
                color: #fff;
                border-color: #007bff;
            }

        #Button1 {
            margin-bottom: 10px;
            text-align: right;
        }

        h1.page-title {
            font-size: 2.5rem;
            color: #333;
            margin-bottom: 20px;
            text-align: center;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-weight: bold;
            text-transform: uppercase;
            letter-spacing: 1px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
        <div class="container">
            <h1 class="page-title">Employee Master</h1>
            <div id="AddEmployee" runat="server" visible="false">
                <div class="card mb-3">
                    <h3 class="card-header">Add New Employee</h3>
                    <div class="card-body" id="divAddEmployeeForm">
                        <asp:TextBox ID="txtAddName" runat="server" CssClass="form-control mb-3" Placeholder="Enter employee name"></asp:TextBox>
                        <asp:TextBox ID="txtAddDob" runat="server" CssClass="form-control mb-3" Placeholder="Select date of birth"></asp:TextBox>
                        <asp:DropDownList ID="ddlAddRole" runat="server" CssClass="form-control mb-3">
                            <asp:ListItem Text="Developer" Value="Developer"></asp:ListItem>
                            <asp:ListItem Text="Team Lead" Value="Team Lead"></asp:ListItem>
                            <asp:ListItem Text="Business Analyst" Value="Business Analyst"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnAddEmployee" runat="server" Text="Add Employee" OnClick="btnAddEmployee_Click" CssClass="btn btn-outline-success" />
                        <asp:Button ID="btnCancel1" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" />
                    </div>
                </div>
            </div>
            <div id="Grid" class="grid" runat="server" visible="true">
                <div class="row mb-3">
                    <div class="col-sm-6">
                        <div class="input-group">
                            <input type="text" id="txtSearch" class="form-control" placeholder="Search..." />
                            <div class="input-group-append">
                                <button id="btnSearch" class="btn btn-outline-primary" type="button">Search</button>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 text-right">
                        <asp:Button ID="Button1" runat="server" Text="Add Employee" OnClick="AddEmployeeForm" CssClass="btn btn-outline-success mb-3" />
                    </div>
                </div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="emp_id"
                    OnRowDeleting="GridView1_RowDeleting" CssClass="table table-bordered border-primary" AllowPaging="true" PageSize="5"
                    OnPageIndexChanging="GridView1_PageIndexChanging" PagerStyle-CssClass="gridview-pager">
                    <Columns>
                        <asp:BoundField DataField="emp_id" HeaderText="Employee ID" ReadOnly="True" />
                        <asp:BoundField DataField="Emp_name" HeaderText="Name" />
                        <asp:TemplateField HeaderText="Date of Birth">
                            <ItemTemplate>
                                <asp:Label ID="lblDOB" runat="server" Text='<%# Bind("DOB", "{0:dd MMM yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditDob" runat="server" CssClass="form-control" Text='<%# Bind("DOB", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
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
                                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("emp_id") %>' OnClick="btnEditForm" CssClass="btn btn-outline-info"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-outline-danger"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div id="divEditEmployee" runat="server" visible="false">
            <h3>Edit Employee</h3>
            <asp:HiddenField ID="hdnEditEmployeeId" runat="server" />
            <asp:TextBox ID="txtEditName" runat="server" CssClass="form-control mb-3" Placeholder="Enter employee name"></asp:TextBox>
            <asp:TextBox ID="txtEditDob" runat="server" CssClass="form-control mb-3" Placeholder="Select date of birth"></asp:TextBox>
            <asp:DropDownList ID="ddlEditRole" runat="server" CssClass="form-control mb-3">
                <asp:ListItem Text="Developer" Value="Developer"></asp:ListItem>
                <asp:ListItem Text="Team Lead" Value="Team Lead"></asp:ListItem>
                <asp:ListItem Text="Business Analyst" Value="Business Analyst"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnUpdate" runat="server" Text="Update Employee" OnClick="btnUpdate_Click" CssClass="btn btn-outline-primary" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" />
        </div>
    </form>
</body>
</html>
