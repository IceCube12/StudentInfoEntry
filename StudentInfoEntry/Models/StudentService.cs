using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace StudentInfoEntry.Models
{
    public class StudentService
    {
        SqlConnection ObjSqlConnection;
        SqlCommand ObjSqlCommand;

        public StudentService()
        {
            ObjSqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMSConnection"].ConnectionString);
            ObjSqlCommand = new SqlCommand();
            ObjSqlCommand.Connection = ObjSqlConnection;
            ObjSqlCommand.CommandType = CommandType.StoredProcedure;
        }

        public List<Student> GetAll()
        {
            List<Student> ObjStudentList = new List<Student>();
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_SelectAllStudents";

                ObjSqlConnection.Open();
                var ObjSqlDataReader = ObjSqlCommand.ExecuteReader();
                if (ObjSqlDataReader.HasRows) 
                {
                    Student ObjStudent = null;
                    while (ObjSqlDataReader.Read()) 
                    {
                        ObjStudent = new Student();
                        ObjStudent.Id = ObjSqlDataReader.GetInt32(0);
                        ObjStudent.Name = ObjSqlDataReader.GetString(1);
                        ObjStudent.Gradelvl = ObjSqlDataReader.GetInt32(2);
                        ObjStudent.Course = ObjSqlDataReader.GetString(3);

                        ObjStudentList.Add(ObjStudent);
                    }
                }
                ObjSqlDataReader.Close();


            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally 
            { 
                ObjSqlConnection.Close(); 
            }

            return ObjStudentList;
        }

        public bool Add(Student objNewStudent)
        {
            bool IsAdded = false;
            //grade level must be from 1 to 3
            if (objNewStudent.Gradelvl < 1 || objNewStudent.Gradelvl > 3)
                throw new ArgumentException("Invalid Grade Level");

            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_InsertStudent";
                ObjSqlCommand.Parameters.AddWithValue("@Id", objNewStudent.Id);
                ObjSqlCommand.Parameters.AddWithValue("@Name", objNewStudent.Name);
                ObjSqlCommand.Parameters.AddWithValue("@Gradelvl", objNewStudent.Gradelvl);
                ObjSqlCommand.Parameters.AddWithValue("@Course", objNewStudent.Course);

                ObjSqlConnection.Open();
                int NoOfRowsAffected = ObjSqlCommand.ExecuteNonQuery();
                IsAdded = NoOfRowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection?.Close();
            }

            return IsAdded;
        }

        public bool Update(Student objStudentToUpdate)
        {
            bool IsUpdated = false;

            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_UpdateStudent";
                ObjSqlCommand.Parameters.AddWithValue("@Id", objStudentToUpdate.Id);
                ObjSqlCommand.Parameters.AddWithValue("@Name", objStudentToUpdate.Name);
                ObjSqlCommand.Parameters.AddWithValue("@Gradelvl", objStudentToUpdate.Gradelvl);
                ObjSqlCommand.Parameters.AddWithValue("@Course", objStudentToUpdate.Course);

                ObjSqlConnection.Open();
                int NoOfRowsAffected = ObjSqlCommand.ExecuteNonQuery();
                IsUpdated = NoOfRowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection?.Close();
            }

            return IsUpdated;
        }

        public bool Delete(int id)
        {
            bool isDeleted = false;

            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_DeleteStudent";
                ObjSqlCommand.Parameters.AddWithValue("@Id", id);

                ObjSqlConnection.Open();
                int NoOfRowsAffected = ObjSqlCommand.ExecuteNonQuery();
                isDeleted = NoOfRowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection?.Close();
            }

            return isDeleted;
        }

        public Student Search(int id)
        {
            Student ObjStudent = null;

            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_SelectStudentById";
                ObjSqlCommand.Parameters.AddWithValue("@Id", id);

                ObjSqlConnection.Open();
                var ObjSqlDataReader = ObjSqlCommand.ExecuteReader();
                if (ObjSqlDataReader.HasRows)
                {
                    ObjSqlDataReader.Read();
                    ObjStudent = new Student();
                    ObjStudent.Id = ObjSqlDataReader.GetInt32(0);
                    ObjStudent.Name = ObjSqlDataReader.GetString(1);
                    ObjStudent.Gradelvl = ObjSqlDataReader.GetInt32(2);
                    ObjStudent.Course = ObjSqlDataReader.GetString(3);
                }

                ObjSqlDataReader.Close();
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }

            return ObjStudent;
        }
    }
}
