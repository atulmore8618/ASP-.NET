using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace CRUD
{
	public partial class CRUD : System.Web.UI.Page
	{
        string cs = "Data Source=DESKTOP-J20S09T;Initial Catalog=Test;Integrated Security=true";

		public void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack) 
            {
                BindGender();
                BindDepartMent();
                BindGrid();
            }
		}

        public void BindGender()
        {
            try 
            {
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Gender", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GenderDropdown.DataSource = dt;
                GenderDropdown.DataTextField = "gender_name";
                GenderDropdown.DataValueField = "gender_id";
                GenderDropdown.DataBind();
                GenderDropdown.Items.Insert(0, new ListItem("--Select Gender--","0"));
            }
            catch( Exception ex)
            { 
                Console.WriteLine(ex.Message);
            }

            
        }

        public void BindDepartMent()
        {
            try
            {
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Department", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DepartmentDropdown.DataSource = dt;
                DepartmentDropdown.DataTextField = "dept_name";
                DepartmentDropdown.DataValueField = "dept_id";
                DepartmentDropdown.DataBind();
                DepartmentDropdown.Items.Insert(0,new ListItem("--Select Department--","0"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(cs);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_InsertEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name",txtEmpName.Text);
                cmd.Parameters.AddWithValue("@designation",txtDesignation.Text);
                cmd.Parameters.AddWithValue("@age",Convert.ToInt32(GenderDropdown.Text));
                cmd.Parameters.AddWithValue("@gender",GenderDropdown.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDecimal(txtSalary.Text));
                cmd.Parameters.AddWithValue("@department",DepartmentDropdown.SelectedItem.Text);

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    Response.Write("<script>alert('Data inserted sucessfully');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Data cannot be inserted');</script>");
                }

                
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('Error:'" +ex.Message +"');</script>");
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtEmpName.Text = "";
            txtEmpCode.Text = "";
            txtDesignation.Text = "";
            txtAge.Text = "";
            GenderDropdown.SelectedIndex = 0;
            txtSalary.Text = "";
            DepartmentDropdown.SelectedIndex = 0;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(cs);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_UpdateEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@emp_code",Convert.ToInt32(txtEmpCode.Text));
                cmd.Parameters.AddWithValue("@name", txtEmpName.Text);
                cmd.Parameters.AddWithValue("@designation", txtDesignation.Text);
                cmd.Parameters.AddWithValue("@age", Convert.ToInt32(GenderDropdown.Text));
                cmd.Parameters.AddWithValue("@gender", GenderDropdown.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDecimal(txtSalary.Text));
                cmd.Parameters.AddWithValue("@department", DepartmentDropdown.SelectedItem.Text);

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    Response.Write("<script>alert('Data updated sucessfully');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Data cannot be inserted');</script>");
                }


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error:'" + ex.Message + "');</script>");
            }

          
        }
        public void BindGrid()
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Employees", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            GridEmployee.DataSource = dt;
            GridEmployee.DataBind();


        }

        protected void GridEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
             if (e.CommandName == "EditRow") // Edit operation
            {
                int empId = Convert.ToInt32(e.CommandArgument);
                LoadEmployeeDetails(empId);
            }
            else if(e.CommandName == "DeleteRow") // Delete operation
            {
                int empId = Convert.ToInt32(e.CommandArgument);
                DeleteEmployee(empId);
            }
        }

        private void DeleteEmployee(int empId)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_DeleteEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@emp_code", empId);

                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    Response.Write("<script>alert('Employee deleted successfully');</script>");
                    BindGrid(); // Refresh Grid
                }
                else
                {
                    Response.Write("<script>alert('Error deleting employee');</script>");
                }
            }
        }
        private void LoadEmployeeDetails(int empId)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employees WHERE emp_code = @emp_code", conn);
                cmd.Parameters.AddWithValue("@emp_code", empId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtEmpCode.Text = reader["emp_code"].ToString();
                    txtEmpName.Text = reader["name"].ToString();
                    txtDesignation.Text = reader["designation"].ToString();
                    txtAge.Text = reader["age"].ToString();
                    txtSalary.Text = reader["salary"].ToString();

                    // Ensure gender dropdown is bound before setting selected value
                    BindGender();
                    string genderValue = reader["gender"].ToString();
                    if (GenderDropdown.Items.FindByText(genderValue) != null)
                    {
                        GenderDropdown.SelectedValue = GenderDropdown.Items.FindByText(genderValue).Value;
                    }

                    // Ensure department dropdown is bound before setting selected value
                    BindDepartMent();
                    string deptValue = reader["department"].ToString();
                    if (DepartmentDropdown.Items.FindByText(deptValue) != null)
                    {
                        DepartmentDropdown.SelectedValue = DepartmentDropdown.Items.FindByText(deptValue).Value;
                    }
                }
            }
        }




        
    }
}