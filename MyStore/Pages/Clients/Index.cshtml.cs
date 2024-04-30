using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()//acess to page when http req
        {

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Employee;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM employee";//read data
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                ClientInfo clientInfo = new ClientInfo();
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


                                listClients.Add(clientInfo);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class ClientInfo
    {
        public String Emp_ID; //variables..data from client
        public String Emp_Name;
        public String Email;
        public String Phone;
        public String Address;
        public String Gender;
        public String DOB;
        public String Dept_Name;
        public String Job_Title;
        public String Emergency_Contact;
        public String Date_of_Join;
        public String Created_At;


    }
}