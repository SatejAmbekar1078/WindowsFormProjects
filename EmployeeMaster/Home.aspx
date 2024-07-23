<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="EmployeeMaster.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Master</title>
    <link rel="stylesheet" href="~/Styles/styles.css" />
    <style>
        /* Styles for gridview-container */
        .gridview-container {
            margin: 20px auto;
            width: 80%;
            padding: 20px;
            background-color: #ffffff;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Employee Master</h2>
            <div>
                <asp:TextBox ID="txtSearch" runat="server" placeholder="Search..." AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                <asp:Button ID="btnAddNew" runat="server" Text="Add New" OnClick="btnAddNew_Click" CssClass="btn btn-primary" />
            </div>
            <div class="gridview-container">
                <asp:GridView ID="dataGridViewEmployees" OnRowDeleting="dataGridViewEmployees_RowDeleting" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    PageSize="5" OnPageIndexChanging="dataGridViewEmployees_PageIndexChanging" OnRowCommand="dataGridViewEmployees_RowCommand" CssClass="table table-striped">
                    <Columns>
                        <asp:BoundField DataField="Emp_id" HeaderText="Emp ID" />
                        <asp:BoundField DataField="Emp_name" HeaderText="Emp Name" />
                        <asp:TemplateField HeaderText="DOB">
                            <ItemTemplate>
                                <%# Eval("DOB", "{0:dd MMM yyyy}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-success" CommandName="Edit" CommandArgument='<%# Eval("Emp_id") %>' />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-warning" CommandName="Delete" CommandArgument='<%# Eval("Emp_id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                </asp:GridView>
            </div>
            <div class="paging">
                <asp:Label ID="lblPageInfo" runat="server"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>