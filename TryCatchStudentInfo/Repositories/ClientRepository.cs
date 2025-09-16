using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatchStudentInfo.Models;

namespace TryCatchStudentInfo.Repositories
{
    public class ClientRepository
    {
        private readonly string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=StudentRegistrationDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        
        public List<Client> GetClients()
        {
            var clients = new List<Client>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM clients by StudentId DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Client client = new Client();
                                client.StudentId = reader.GetInt32(0);
                                client.FirstName = reader.GetString(1);
                                client.LastName = reader.GetString(2);
                                client.MiddleInitial = reader.GetString(3);
                                client.Program = reader.GetString(4);
                                client.BirthDate = reader.GetString(5);
                                client.Age = reader.GetInt32(6);
                                client.Gender = reader.GetString(7);
                                client.Address = reader.GetString(8);   
                                client.ContactNum = reader.GetString(9);

                                clients.Add(client);
                            }
                        }
                    }
                }

            } 
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured: " + ex.ToString());
            }
            return clients;
        }

        public Client GetClient(int StudentId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Students WHERE StudentId = @StudentId";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@StudentId", StudentId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                Client client = new Client();
                                client.StudentId = reader.GetInt32(0);
                                client.FirstName = reader.GetString(1);
                                client.LastName = reader.GetString(2);
                                client.MiddleInitial = reader.GetString(3);
                                client.Program = reader.GetString(4);
                                client.BirthDate = reader.GetString(5);
                                client.Age = reader.GetInt32(6);
                                client.Gender = reader.GetString(7);
                                client.Address = reader.GetString(8);
                                client.ContactNum = reader.GetString(9);
                                return client;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            return null;
        }
    }
}
