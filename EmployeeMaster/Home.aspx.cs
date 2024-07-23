using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EmployeeMaster
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid(); // Initial binding of the grid
            }
        }

        protected void BindGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Emp_id, Emp_name, DOB FROM Employees", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridViewEmployees.DataSource = dt;
                        dataGridViewEmployees.DataBind();
                    }
                }
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BindGrid(); // Rebind grid on search text change
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEmployee.aspx");
        }

        protected void dataGridViewEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dataGridViewEmployees.PageIndex = e.NewPageIndex;
            BindGrid(); // Rebind grid on page index change
        }

        protected void dataGridViewEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int empId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"EditEmployee.aspx?EmpId={empId}");
            }
            else if (e.CommandName == "Delete")
            {
                int empId = Convert.ToInt32(e.CommandArgument);
                DeleteEmployee(empId);
                BindGrid(); // Rebind grid after deletion
            }
        }

        protected void dataGridViewEmployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridViewEmployees.Rows.Count)
            {
                int empId = Convert.ToInt32(dataGridViewEmployees.DataKeys[e.RowIndex].Values["Emp_id"]);
                DeleteEmployee(empId);
                BindGrid(); // Rebind grid after deletion
            }
        }

        protected void DeleteEmployee(int empId)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Employees WHERE Emp_id = @EmpId", con))
                {
                    cmd.Parameters.AddWithValue("@EmpId", empId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }

}