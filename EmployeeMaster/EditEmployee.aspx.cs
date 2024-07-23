using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace EmployeeMaster
{
    public partial class EditEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if EmpId query parameter is present and valid
                if (!string.IsNullOrEmpty(Request.QueryString["EmpId"]) && int.TryParse(Request.QueryString["EmpId"], out int empId))
                {
                    LoadEmployeeDetails(empId); // Load employee details for editing
                }
                else
                {
                    Response.Redirect("Home.aspx"); // Redirect if EmpId is missing or invalid
                }
            }
        }

        protected void LoadEmployeeDetails(int empId)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Emp_id, Emp_name, DOB FROM Employees WHERE Emp_id = @EmpId", con))
                {
                    cmd.Parameters.AddWithValue("@EmpId", empId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Populate textboxes with data from database
                        lblEmpId.Text = reader["Emp_id"].ToString(); // Store Emp_id in a hidden label
                        txtName.Text = reader["Emp_name"].ToString();
                        txtDOB.Text = Convert.ToDateTime(reader["DOB"]).ToString("dd/MM/yyyy"); // Format as DD/MM/YYYY
                        //txtRole.Text = reader["Role"].ToString();
                    }
                    else
                    {
                        // Handle case where no employee with given EmpId is found
                        Response.Redirect("Home.aspx");
                    }
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) // Check if all validations are successful
            {
                int empId = Convert.ToInt32(lblEmpId.Text); // Get Emp_id from hidden label
                string name = txtName.Text.Trim();
                DateTime dob;
                if (!DateTime.TryParseExact(txtDOB.Text.Trim(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dob))
                {
                    // Handle invalid date format (though should be handled by RegularExpressionValidator)
                    return;
                }
                string role = txtRole.Text.Trim();

                // Update employee details in database
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Employees SET Emp_name = @Name, DOB = @DOB WHERE Emp_id = @EmpId", con))
                    {
                        cmd.Parameters.AddWithValue("@EmpId", empId);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@DOB", dob);
                        //cmd.Parameters.AddWithValue("@Role", role);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                Response.Redirect("Home.aspx"); // Redirect to employee list after update
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            int empId = Convert.ToInt32(lblEmpId.Text); // Get Emp_id from hidden label
            LoadEmployeeDetails(empId); // Reload employee details to reset form fields
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx"); // Redirect to employee list without saving changes
        }
    }
}
