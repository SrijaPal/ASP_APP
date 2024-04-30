using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Retrieve form data
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

            // Validate form data
            if (clientInfo.Emp_Name.Length == 0 || clientInfo.Email.Length == 0 ||
                clientInfo.Phone.Length == 0 || clientInfo.Address.Length == 0 ||
                clientInfo.Gender.Length == 0 || clientInfo.DOB.Length == 0 ||
                clientInfo.Dept_Name.Length == 0 || clientInfo.Job_Title.Length == 0 ||
                clientInfo.Emergency_Contact.Length == 0 || clientInfo.Date_of_Join.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            // Additional constraints
            if (!IsValidPhoneNumber(clientInfo.Phone))
            {
                errorMessage = "Phone number must be 10 digits";
                return;
            }
            if (!IsValidPhoneNumber(clientInfo.Emergency_Contact))
            {
                errorMessage = "Emergency contact number must be 10 digits";
                return;
            }

            // Save data to database
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Employee;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO employee" +
                        "(Emp_Name,Email,Phone,Address,Gender,DOB,Dept_Name,Job_Title,Emergency_Contact,Date_of_Join) VALUES" +
                        "(@Emp_Name, @Email, @Phone, @Address, @Gender, @DOB, @Dept_Name, @Job_Title, @Emergency_Contact, @Date_of_Join);";

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
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Reset form data and show success message
            clientInfo = new ClientInfo();
            successMessage = "New employee added successfully";
        }

        // Method to validate phone number format
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length == 10 && long.TryParse(phoneNumber, out _);
        }
    }
}
