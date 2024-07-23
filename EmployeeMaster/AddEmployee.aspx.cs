using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace EmployeeMaster
{
    public partial class AddEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Optionally, you can add logic here if needed
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) // Check if all validations are successful
            {
                string name = txtName.Text.Trim();
                DateTime dob;
                if (!DateTime.TryParseExact(txtDOB.Text.Trim(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dob))
                {
                    // Handle invalid date format (though should be handled by RegularExpressionValidator)
                    return;
                }
                string role = txtRole.Text.Trim();

                // Insert new employee details into the database
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Employees (Emp_name, DOB) VALUES (@Name, @DOB)", con))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@DOB", dob);
                        //cmd.Parameters.AddWithValue("@Role", role);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                Response.Redirect("Home.aspx"); // Redirect to employee list after adding
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            // Clear form fields
            txtName.Text = string.Empty;
            txtDOB.Text = string.Empty;
            txtRole.Text = string.Empty;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx"); // Redirect to employee list without adding
        }
    }
}
