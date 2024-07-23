using LinqStoredProcedure.Data;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace LinqStoredProcedure.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void BindGrid()
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["DotNetConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("GetEmployees", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    Debug.WriteLine("In BindGrid");
                    lblErrorMessage.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Error: " + ex.Message;
                lblErrorMessage.Visible = true;
            }
        }


        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["DotNetConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("AddEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Emp_name", txtAddName.Text);
                    cmd.Parameters.AddWithValue("@DOB", DateTime.ParseExact(txtAddDob.Text, "yyyy-MM-dd", null));
                    cmd.Parameters.AddWithValue("@Emp_Role", ddlAddRole.SelectedValue);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Debug.WriteLine("Employee added succeffully");
                }
                txtAddName.Text = "";
                txtAddDob.Text = "";
                ddlAddRole.SelectedIndex = 0;
                BindGrid();
                divEditEmployee.Visible = false;
                AddEmployee.Visible = false;
                Grid.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Error: " + ex.Message; 
                lblErrorMessage.Visible = true;
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                int empId = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
                employee empl = GetEmployeeById(empId);

                if (empl != null)
                {
                    txtEditName.Text = empl.Emp_name;
                    txtEditDob.Text = empl.DOB.ToString("yyyy-MM-dd");
                    ddlEditRole.SelectedValue = empl.Emp_Role;

                    Debug.WriteLine("In Edit Form");

                }
                else
                {
                    lblErrorMessage.Text = "Employee not found.";
                    lblErrorMessage.Visible = true;
                }

                divEditEmployee.Visible = true;
                AddEmployee.Visible = false;
                Grid.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Error: " + ex.Message;
                lblErrorMessage.Visible = true;
            }
        }

        private employee GetEmployeeById(int empId)
        {
            Debug.WriteLine("In GetEmployeeById Method");
            string cs = ConfigurationManager.ConnectionStrings["DotNetConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("GetEmployeeById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@emp_id", empId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    employee emp = new employee();
                    emp.emp_id = empId;
                    emp.Emp_name = reader["Emp_name"].ToString();
                    emp.DOB = Convert.ToDateTime(reader["DOB"]);
                    emp.Emp_Role = reader["Emp_Role"].ToString();
                    return emp;
                }
                return null;
            }
        }


        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Debug.WriteLine("In RowCancelling");
            GridView1.EditIndex = -1;
            BindGrid();
            divEditEmployee.Visible = false;
            AddEmployee.Visible = true;
            Grid.Visible = true;
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Debug.WriteLine("In Row Deleting");
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["DotNetConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    int emp_id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["emp_id"]);
                    SqlCommand cmd = new SqlCommand("DeleteEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@emp_id", emp_id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                BindGrid();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Error: " + ex.Message;
                lblErrorMessage.Visible = true;
            }
        }

        protected void btnEditForm(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkEdit = (LinkButton)sender;
                int empId = Convert.ToInt32(lnkEdit.CommandArgument);
                hdnEditEmployeeId.Value = empId.ToString();
                employee empl = GetEmployeeById(empId);

                if (empl != null)
                {
                    txtEditName.Text = empl.Emp_name;
                    txtEditDob.Text = empl.DOB.ToString("yyyy-MM-dd");
                    ddlEditRole.SelectedValue = empl.Emp_Role;

                    Debug.WriteLine("In Edit Form");

                }
                else
                {
                    lblErrorMessage.Text = "Employee not found.";
                    lblErrorMessage.Visible = true;
                }

                divEditEmployee.Visible = true;
                AddEmployee.Visible = false;
                Grid.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Error: " + ex.Message;
                lblErrorMessage.Visible = true;
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Update button clicked");
            try
            {
                int empId = Convert.ToInt32(hdnEditEmployeeId.Value);
                string emp_name = txtEditName.Text;
                DateTime dob = DateTime.ParseExact(txtEditDob.Text, "yyyy-MM-dd", null);
                string emp_role = ddlEditRole.SelectedValue;
                Debug.WriteLine("Name " + emp_name + " Id - " + empId + " Date: " + dob + " Role :" + emp_role);
                string cs = ConfigurationManager.ConnectionStrings["DotNetConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("UpdateEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@emp_id", empId);
                    cmd.Parameters.AddWithValue("@Emp_name", txtEditName.Text);
                    cmd.Parameters.AddWithValue("@DOB", dob);
                    cmd.Parameters.AddWithValue("@Emp_Role", emp_role);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                GridView1.EditIndex = -1;
                BindGrid();

                divEditEmployee.Visible = false;
                AddEmployee.Visible = false;
                Grid.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Error: " + ex.Message;
                lblErrorMessage.Visible = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Cancle button clicked");
            GridView1.EditIndex = -1;
            BindGrid();
            divEditEmployee.Visible = false;
            AddEmployee.Visible = false;
            Grid.Visible = true;
        }

        protected void AddEmployeeForm(object sender, EventArgs e)
        {
            divEditEmployee.Visible = false;
            AddEmployee.Visible = true;
            Grid.Visible = false;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGrid(); 
        }

    }
}
