using EFSP.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EFSP.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        private EmpEntities db = new EmpEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEmployeesGrid();
            }
        }

        private void BindEmployeesGrid()
        {
            try
            {
                gvEmployees.DataSource = GetEmployeesFromDatabase();
                gvEmployees.DataBind();
                lblErrorMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "An error occurred while loading employee data. Please try again.";
            }
        }

        private List<employee> GetEmployeesFromDatabase()
        {
            return db.Database.SqlQuery<employee>("EXEC GetEmployees").ToList();
        }


        protected void gvEmployees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                if (e.NewEditIndex >= 0 && e.NewEditIndex < gvEmployees.Rows.Count)
                {
                    gvEmployees.EditIndex = e.NewEditIndex;
                    BindEmployeesGrid();

                    int empId = Convert.ToInt32(gvEmployees.DataKeys[e.NewEditIndex].Value);
                    var employee = db.employees.Find(empId);
                    if (employee != null)
                    {
                        hdnEditEmployeeId.Value = empId.ToString();
                        txtEditName.Text = employee.Emp_name;
                        txtEditDob.Text = employee.DOB.ToString("yyyy-MM-dd");
                        ddlEditRole.SelectedValue = employee.Emp_Role;
                    }

                    divEditEmployee.Visible = true;
                    AddEmployee.Visible = false;
                    Grid.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "An error occurred while editing the employee. Please try again.";
            }
        }


        protected void gvEmployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int empId = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Value);

                db.Database.ExecuteSqlCommand("EXEC DeleteEmployee @emp_id",
                    new SqlParameter("emp_id", empId));

                BindEmployeesGrid();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "An error occurred while deleting the employee. Please try again.";
            }
        }


        protected void gvEmployees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvEmployees.EditIndex = -1;
                BindEmployeesGrid();
                divEditEmployee.Visible = false;
                AddEmployee.Visible = true;
                Grid.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "An error occurred while canceling the edit. Please try again.";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int empId = Convert.ToInt32(hdnEditEmployeeId.Value);

                db.Database.ExecuteSqlCommand("EXEC UpdateEmployee @emp_id, @Emp_name, @DOB, @Emp_Role",
                    new SqlParameter("emp_id", empId),
                    new SqlParameter("Emp_name", txtEditName.Text),
                    new SqlParameter("DOB", DateTime.ParseExact(txtEditDob.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)),
                    new SqlParameter("Emp_Role", ddlEditRole.SelectedValue));

                gvEmployees.EditIndex = -1;
                BindEmployeesGrid();
                divEditEmployee.Visible = false;
                AddEmployee.Visible = true;
                Grid.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "An error occurred while updating the employee. Please try again.";
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                gvEmployees.EditIndex = -1;
                BindEmployeesGrid();
                divEditEmployee.Visible = false;
                AddEmployee.Visible = true;
                Grid.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "An error occurred while canceling the edit. Please try again.";
            }
        }

        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                db.Database.ExecuteSqlCommand("EXEC AddEmployee @Emp_name, @DOB, @Emp_Role",
                    new SqlParameter("Emp_name", txtAddName.Text),
                    new SqlParameter("DOB", DateTime.ParseExact(txtAddDob.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)),
                    new SqlParameter("Emp_Role", ddlAddRole.SelectedValue));

                txtAddName.Text = string.Empty;
                txtAddDob.Text = string.Empty;
                ddlAddRole.SelectedIndex = 0;

                BindEmployeesGrid();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "An error occurred while adding the employee. Please try again.";
            }
        }

    }
}
