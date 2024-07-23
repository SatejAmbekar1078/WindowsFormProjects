using Linq.Data;
using System;
using System.Linq;

namespace Linq.Pages
{
    public partial class EditEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["empId"]))
                {
                    int empId = Convert.ToInt32(Request.QueryString["empId"]);
                    BindEmployee(empId);
                    litFormTitle.Text = "Edit Employee Details"; 
                    btnUpdate.Visible = true;
                    btnCancel.Visible = true;
                }
                else
                {
                    litFormTitle.Text = "Add Employee";
                    btnSave.Visible = true; 
                    btnCancel.Visible = true; 
                }
            }
        }

        protected void BindEmployee(int empId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EmpDbConnectionString"]?.ConnectionString;

            using (EmpDataDataContext context = new EmpDataDataContext(connectionString))
            {
                var employee = context.employees.SingleOrDefault(emp => emp.emp_id == empId);
                if (employee != null)
                {
                    hfEmpId.Value = employee.emp_id.ToString();
                    tbEmpId.Text = employee.emp_id.ToString();
                    tbEmpName.Text = employee.Emp_name;
                    tbDOB.Text = employee.DOB.ToString("yyyy-MM-dd");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EmpDbConnectionString"]?.ConnectionString;

                using (EmpDataDataContext context = new EmpDataDataContext(connectionString))
                {
                    
                    employee newEmployee = new employee
                    {
                        Emp_name = tbEmpName.Text,
                        DOB = Convert.ToDateTime(tbDOB.Text),
                        Emp_Role = ddlEmpRole.SelectedValue
                    };

                   
                    context.employees.InsertOnSubmit(newEmployee);
                    context.SubmitChanges();
                }

                lblMessage.Text = "Employee added successfully."; 
                ClearFields(); 
                Response.Redirect("Employees.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Failed to save employee. Please try again."; 
                Console.WriteLine(ex.Message); 
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EmpDbConnectionString"]?.ConnectionString;

                using (EmpDataDataContext context = new EmpDataDataContext(connectionString))
                {
                    int empId = Convert.ToInt32(hfEmpId.Value);
                    var employee = context.employees.SingleOrDefault(emp => emp.emp_id == empId);
                    if (employee != null)
                    {
                        employee.Emp_name = tbEmpName.Text;
                        employee.DOB = Convert.ToDateTime(tbDOB.Text);
                        employee.Emp_Role = ddlEmpRole.SelectedValue;
                        context.SubmitChanges();
                    }
                }

                Response.Redirect("Employees.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Failed to update employee. Please try again.";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Employees.aspx");
        }

        private void ClearFields()
        {
            tbEmpName.Text = string.Empty;
            tbDOB.Text = string.Empty;
        }
    }
}
