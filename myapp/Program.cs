using System;
using Microsoft.Data.SqlClient;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectNW();
            Console.WriteLine("Done!");
        }

        public static void ConnectNW()
        {            
            string connectionString = "Server=tcp:yourservername.database.windows.net,1433;Initial Catalog=SSISDB;Persist Security Info=False;User ID=username;Password=password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            string queryString = "SELECT * FROM Products";

            SqlConnection connection = new SqlConnection(connectionString);
    
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                Console.WriteLine(dr[1].ToString());
            }    
        }
    }
}