using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TryCatchStudentInfo.Models;

namespace TryCatchStudentInfo.Repositories
{
    public class StudentRepository
    {
        private readonly string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=StudentRegistrationDb;Integrated Security=True;TrustServerCertificate=True";

        public List<Students> GetStudents()
        {
            var students = new List<Students>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Students ORDER BY StudentId DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Students student = new Students();
                                student.StudentId = reader.GetInt32(0);
                                student.FirstName = reader.GetString(1);
                                student.LastName = reader.GetString(2);
                                student.MiddleInitial = reader.GetString(3);
                                student.Program = reader.GetString(4);
                                student.BirthDate = reader.GetDateTime(5).ToString("yyyy-MM-dd");
                                student.Age = reader.GetInt32(6);
                                student.Gender = reader.GetString(7);
                                student.Address = reader.GetString(8);
                                student.ContactNum = reader.GetString(9);
                                students.Add(student);
                            }
                        }
                    }
                }

            } 
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured: " + ex.ToString());
            }
            return students;
        }

        public Students GetStudent(int StudentId)
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

                                Students student = new Students();
                                student.StudentId = reader.GetInt32(0);
                                student.FirstName = reader.GetString(1);
                                student.LastName = reader.GetString(2);
                                student.MiddleInitial = reader.GetString(3);
                                student.Program = reader.GetString(4);
                                student.BirthDate = reader.GetDateTime(5).ToString("yyyy-MM-dd");
                                student.Age = reader.GetInt32(6);
                                student.Gender = reader.GetString(7);
                                student.Address = reader.GetString(8);
                                student.ContactNum = reader.GetString(9);
                                return student;

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

        public void CreateStudent(Students student)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"INSERT INTO Students (FirstName, LastName, MiddleInitial, Program, BirthDate, Age, Gender, Address, ContactNum) 
                                VALUES 
                                (@FirstName, @LastName, @MiddleInitial, @Program, @BirthDate, @Age, @Gender, @Address, @ContactNum)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", student.FirstName);
                        command.Parameters.AddWithValue("@LastName", student.LastName);
                        command.Parameters.AddWithValue("@MiddleInitial", student.MiddleInitial);
                        command.Parameters.AddWithValue("@Program", student.Program);
                        command.Parameters.AddWithValue("@BirthDate", student.BirthDate); 
                        command.Parameters.AddWithValue("@Age", student.Age);
                        command.Parameters.AddWithValue("@Gender", student.Gender);
                        command.Parameters.AddWithValue("@Address", student.Address);
                        command.Parameters.AddWithValue("@ContactNum", student.ContactNum);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        public void UpdateStudents(Students student)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Students SET FirstName=@FirstName, LastName=@LastName, MiddleInitial=@MiddleInitial, Program=@Program, BirthDate=@BirthDate, Age=@Age, Gender=@Gender, Address=@Address, ContactNum=@ContactNum WHERE StudentId=@StudentId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentId", student.StudentId);
                        command.Parameters.AddWithValue("@FirstName", student.FirstName);
                        command.Parameters.AddWithValue("@LastName", student.LastName);
                        command.Parameters.AddWithValue("@MiddleInitial", student.MiddleInitial);
                        command.Parameters.AddWithValue("@Program", student.Program);
                        command.Parameters.AddWithValue("@BirthDate", student.BirthDate);
                        command.Parameters.AddWithValue("@Age", student.Age);
                        command.Parameters.AddWithValue("@Gender", student.Gender);
                        command.Parameters.AddWithValue("@Address", student.Address);
                        command.Parameters.AddWithValue("@ContactNum", student.ContactNum);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        public void DeleteStudent(int StudentId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Students WHERE StudentId = @StudentId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentId", StudentId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

    }
}
