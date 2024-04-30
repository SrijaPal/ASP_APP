using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace MyStore.Pages.Clients
{
    public class EditModel : PageModel
    {

        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String Emp_ID = Request.Query["Emp_ID"];
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Employee;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM employee WHERE Emp_ID=@Emp_ID";//read data
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Emp_ID", Emp_ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                clientInfo.Emp_ID = "" + reader.GetInt32(0);//convert int to str..so "" in start
                                clientInfo.Emp_Name = reader.GetString(1);
                                clientInfo.Email = reader.GetString(2);
                                clientInfo.Phone = reader.GetString(3);
                                clientInfo.Address = reader.GetString(4);
                                clientInfo.Gender = reader.GetString(5);
                                clientInfo.DOB = reader.GetString(6);
                                clientInfo.Dept_Name = reader.GetString(7);
                                clientInfo.Job_Title = reader.GetString(8);
                                clientInfo.Emergency_Contact = reader.GetString(9);
                                clientInfo.Date_of_Join = reader.GetString(10);
                                clientInfo.Created_At = reader.GetDateTime(11).ToString();

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            clientInfo.Emp_ID = Request.Form["Emp_ID"];
            clientInfo.Emp_Name = Request.Form["Emp_Name"];
            clientInfo.Email = Request.Form["Email"];
            clientInfo.Phone = Request.Form["Phone"];
            clientInfo.Address = Request.Form["Address"];
            clientInfo.Gender = Request.Form["Gender"];
            clientInfo.DOB = Request.Form["DOB"];
            clientInfo.Dept_Name = Request.Form["Dept_Name"];
            clientInfo.Job_Title = Request.Form["Job_Title"];
            clientInfo.Emergency_Contact = Request.Form["Emergency_Contact"];
            clientInfo.Date_of_Join = Request.Form["Date_of_Join"];


            if (clientInfo.Emp_Name.Length == 0 || clientInfo.Email.Length == 0 ||
                clientInfo.Phone.Length == 0 || clientInfo.Address.Length == 0 ||
                clientInfo.Gender.Length == 0 || clientInfo.DOB.Length == 0 ||
                clientInfo.Dept_Name.Length == 0 || clientInfo.Job_Title.Length == 0 ||
                clientInfo.Emergency_Contact.Length == 0 || clientInfo.Date_of_Join.Length == 0)
            {
                errorMessage = "All the fields are required";
                    return;
                }

                try
                {
                    String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Employee;Integrated Security=True;Encrypt=False";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sql = "UPDATE employee " +
                            "SET Emp_Name=@Emp_Name, Email=@Email, Phone=@Phone, Address=@Address, Gender=@Gender, DOB=@DOB, Dept_Name=@Dept_Name, Job_Title=@Job_Title, Emergency_Contact=@Emergency_Contact, Date_of_Join=@Date_of_Join " +
                            "WHERE Emp_ID = @Emp_ID;";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@Emp_Name", clientInfo.Emp_Name);
                            command.Parameters.AddWithValue("@Email", clientInfo.Email);
                            command.Parameters.AddWithValue("@Phone", clientInfo.Phone);
                            command.Parameters.AddWithValue("@Address", clientInfo.Address);
                            command.Parameters.AddWithValue("@Gender", clientInfo.Gender);
                            command.Parameters.AddWithValue("@DOB", clientInfo.DOB);
                            command.Parameters.AddWithValue("@Dept_Name", clientInfo.Dept_Name);
                            command.Parameters.AddWithValue("@Job_Title", clientInfo.Job_Title);
                            command.Parameters.AddWithValue("@Emergency_Contact", clientInfo.Emergency_Contact);
                            command.Parameters.AddWithValue("@Date_of_Join", clientInfo.Date_of_Join);
                            command.Parameters.AddWithValue("@Emp_ID", clientInfo.Emp_ID);
                            command.ExecuteNonQuery();

                        }


                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return;
                }

                Response.Redirect("/Clients/Index");



            }


        }
}
