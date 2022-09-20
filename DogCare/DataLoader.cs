using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DogCare
{
    public class DataLoader
    {
        static string conString;
        static DataLoader()
        {
            try
            {
                conString = System.Configuration.ConfigurationManager.ConnectionStrings["DogCare"].ConnectionString;
            }
            catch(Exception e)
            {
                conString = "Data Source=localhost;Initial Catalog=DogCare;Integrated Security=True";
            }
        }  
        
        public static DataTable LoadDetails(int custId = 0)
        {
            SqlDataAdapter cmdSql = new SqlDataAdapter();
            DataTable ds = new DataTable();

            using (SqlCommand myCommand = new SqlCommand("dbo.GetGustomerAppointDetails"))
            {
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    myCommand.Connection = conn;
                    myCommand.CommandType = CommandType.StoredProcedure;
                    if(custId > 0)
                        myCommand.Parameters.AddWithValue("@customerId", custId);
                    else
                        myCommand.Parameters.AddWithValue("@customerId", DBNull.Value);
                    
                    conn.Open();
                    try
                    {

                        cmdSql.SelectCommand = myCommand;
                        cmdSql.Fill(ds);
                       
                        conn.Close();
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            return ds;
        }

        public static bool RegisterNewCustomer(string name, string userName, string password)
        {

            SqlDataAdapter cmdSql = new SqlDataAdapter();
            DataTable ds = new DataTable();

            using (SqlCommand myCommand = new SqlCommand("dbo.CreateNewCustomer"))
            {
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    myCommand.Connection = conn;
                    myCommand.CommandType = CommandType.StoredProcedure;
                    
                    myCommand.Parameters.AddWithValue("@firstName", name);
                    myCommand.Parameters.AddWithValue("@userName", userName);
                    myCommand.Parameters.AddWithValue("@pwrd", password);

                    conn.Open();
                    try
                    {
                        cmdSql.SelectCommand = myCommand;
                        cmdSql.Fill(ds);

                        conn.Close();
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static DataTable GetAppointById(int id)
        {
            SqlDataAdapter cmdSql = new SqlDataAdapter();
            DataTable ds = new DataTable();

            using (SqlCommand myCommand = new SqlCommand("dbo.GetVisitById"))
            {
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    myCommand.Connection = conn;
                    myCommand.CommandType = CommandType.StoredProcedure;

                    myCommand.Parameters.AddWithValue("@Id", id);                   

                    conn.Open();
                    try
                    {
                        cmdSql.SelectCommand = myCommand;
                        cmdSql.Fill(ds);                      

                        conn.Close();                        
                    }
                    catch (Exception e)
                    {                        
                    }
                }
            }
            return ds;
        }

        public static bool LoginCheck(string userName, string password)
        {

            SqlDataAdapter cmdSql = new SqlDataAdapter();
            DataTable ds = new DataTable();

            using (SqlCommand myCommand = new SqlCommand("dbo.SystemLogin"))
            {
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    myCommand.Connection = conn;
                    myCommand.CommandType = CommandType.StoredProcedure;

                    myCommand.Parameters.AddWithValue("@userName", userName);
                    myCommand.Parameters.AddWithValue("@pswrd", password);

                    conn.Open();
                    try
                    {
                        cmdSql.SelectCommand = myCommand;
                        cmdSql.Fill(ds);

                        int res = (int)ds.Rows[0]["Result"];

                        conn.Close();

                        return res > 0 ? true : false;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }
        }

        public static bool NewAppointment(int customerId, DateTime appointDate)
        {
            SqlDataAdapter cmdSql = new SqlDataAdapter();
            DataTable ds = new DataTable();
            DateTime now = DateTime.Now;

            using (SqlCommand myCommand = new SqlCommand("dbo.MakeAppointment"))
            {
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    myCommand.Connection = conn;
                    myCommand.CommandType = CommandType.StoredProcedure;

                    myCommand.Parameters.AddWithValue("@customerId", customerId);
                    myCommand.Parameters.AddWithValue("@apointDate", appointDate);
                    myCommand.Parameters.AddWithValue("@creationDate", now);

                    conn.Open();
                    try
                    {
                        cmdSql.SelectCommand = myCommand;
                        cmdSql.Fill(ds);

                        conn.Close();
                    
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool EditCustomerAppointment(int appId, int customerId, string firstName, DateTime appointDate)
        {
            SqlDataAdapter cmdSql = new SqlDataAdapter();
            DataTable ds = new DataTable();
            DateTime now = DateTime.Now;

            using (SqlCommand myCommand = new SqlCommand("dbo.EditCustomerDetails"))
            {
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    myCommand.Connection = conn;
                    myCommand.CommandType = CommandType.StoredProcedure;

                    myCommand.Parameters.AddWithValue("@VisitId", appId);
                    myCommand.Parameters.AddWithValue("@customerId", customerId);
                    myCommand.Parameters.AddWithValue("@firstName", firstName);
                    myCommand.Parameters.AddWithValue("@newVisitDate", appointDate);
                    myCommand.Parameters.AddWithValue("@newCreationDate", now);

                    conn.Open();
                    try
                    {
                        cmdSql.SelectCommand = myCommand;
                        cmdSql.Fill(ds);

                        conn.Close();
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }
            return true;
        }       

        public static void DeletAppoiintment(int id)
        {
            SqlDataAdapter cmdSql = new SqlDataAdapter();
            DataTable ds = new DataTable();

            using (SqlCommand myCommand = new SqlCommand("dbo.DeleteVisit"))
            {
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    myCommand.Connection = conn;
                    myCommand.CommandType = CommandType.StoredProcedure;

                    myCommand.Parameters.AddWithValue("@Id", id);

                    conn.Open();
                    try
                    {
                        cmdSql.SelectCommand = myCommand;
                        cmdSql.Fill(ds);

                        conn.Close();
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
          
        }
    }
}