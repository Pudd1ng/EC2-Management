using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EC2_Management_Tool.Models
{
    public class ScheduleModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Days { get; set; }
        public string Hours { get; set; }

        /*public ScheduleModel(int id, string name, string days, string hours)
        {
            ID = id;
            Name = name;
            Days = days;
            Hours = hours;
        }*/

        public void createSchedule(string Name, string Days, string Hours)
        {
            string connectionString = "Data Source=.;Initial Catalog=EC2ManagementTool;Integrated Security=True";
            var queryString = string.Format("INSERT INTO Schedule (Name, Days, Hours) VALUES ('{0}' , '{1}', '{2}')",Name, Days, Hours);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }       
        }
        //Maybe pass in queries and form one method here? conform to DRY
        public void deleteSchedule(string Name)
        {
            string connectionString = "Data Source=.;Initial Catalog=EC2ManagementTool;Integrated Security=True";
            var queryString = string.Format("DELETE FROM Schedule WHERE Name = '{0}'", Name);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void assignSchedule (string instanceId, string scheduleName)
        {
            string connectionString = "Data Source=.;Initial Catalog=EC2ManagementTool;Integrated Security=True";
            var queryString = string.Format("INSERT INTO ScheduledServer (ServerId, ScheduleName) VALUES ('{0}', '{1}')", instanceId, scheduleName);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

    }
}