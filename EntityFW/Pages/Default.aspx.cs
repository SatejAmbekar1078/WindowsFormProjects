using EntityFW.Data;
using System;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;

namespace EntityFW.Pages
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
                gvEmployees.DataSource = db.employees.ToList();
                gvEmployees.DataBind();
                lblErrorMessage.Text = "";
            }
            catch (Exception ex)
            {
               lblErrorMessage.Text = "An error occurred while loading employee data. Please try again.";
            }
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
                var employee = db.employees.Find(empId);
                if (employee != null)
                {
                    db.employees.Remove(employee);
                    db.SaveChanges();
                    BindEmployeesGrid();
                }
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
                var employee = db.employees.Find(empId);
                if (employee != null)
                {
                    employee.Emp_name = txtEditName.Text;
                    employee.DOB = DateTime.ParseExact(txtEditDob.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    employee.Emp_Role = ddlEditRole.SelectedValue;
                    db.SaveChanges();
                    gvEmployees.EditIndex = -1;
                    BindEmployeesGrid();
                    divEditEmployee.Visible = false;
                    AddEmployee.Visible = true;
                    Grid.Visible = true;
                }
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
                employee newEmployee = new employee
                {
                    Emp_name = txtAddName.Text,
                    DOB = DateTime.ParseExact(txtAddDob.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Emp_Role = ddlAddRole.SelectedValue
                };

                db.employees.Add(newEmployee);
                db.SaveChanges();

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
