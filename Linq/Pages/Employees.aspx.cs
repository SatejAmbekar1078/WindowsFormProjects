using Linq.Data;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace Linq.Pages
{
    public partial class Employees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEmployees();
            }
        }

        protected void BindEmployees()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EmpDbConnectionString"]?.ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Connection string not found or is null.");
            }

            using (EmpDataDataContext context = new EmpDataDataContext(connectionString))
            {
                var employees = context.employees.ToList();
                gvEmployees.DataSource = employees;
                gvEmployees.DataBind();
            }
        }

        protected void gvEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditEmployee")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int empId = Convert.ToInt32(gvEmployees.DataKeys[rowIndex].Value);

                Response.Redirect($"EditEmployee.aspx?empId={empId}");
            }
            else if (e.CommandName == "DeleteEmployee")
            {
                int empId = Convert.ToInt32(e.CommandArgument);

                try
                {
                    DeleteEmployee(empId);
                    
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Failed to delete employee. Please try again.";
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static void DeleteEmployee(int empId)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EmpDbConnectionString"]?.ConnectionString;

                using (EmpDataDataContext context = new EmpDataDataContext(connectionString))
                {
                    var employee = context.employees.SingleOrDefault(emp => emp.emp_id == empId);
                    if (employee != null)
                    {
                        context.employees.DeleteOnSubmit(employee);
                        context.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete employee.", ex);
            }
        }



        [System.Web.Services.WebMethod]
        public static string BindedEmployees()
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EmpDbConnectionString"]?.ConnectionString;

                using (EmpDataDataContext context = new EmpDataDataContext(connectionString))
                {
                    var employees = context.employees.ToList();
                    // Generate HTML for GridView manually
                    GridView gv = new GridView();
                    gv.DataSource = employees;
                    gv.DataBind();

                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
                    gv.RenderControl(hw);

                    return sw.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch employees.", ex);
            }
        }

        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditEmployee.aspx");
        }
    }
}
