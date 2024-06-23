using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace webapi_02
{
    public class Schedule
    {
        //Instance fiels (auto-implemented properties, with defaults)
        public int Id { get; set; } = 0;
        public string Title { get; set; } = "";
        public DateTime Start { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; } = DateTime.Now;
        public string? Url { get; set; } = "";
        public int? GroupId { get; set; } = 0;

        //Instance methods
        public void ShowSchedule()
        {
            Console.WriteLine($"{Id}, {Title}, {Start}");
        }

        //Static Methods
        public static void ShowEvents(List<Schedule> scheduleList)
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("ID, Title, Start");
            Console.WriteLine("---------------------------------------");

            foreach (Schedule therapyEvent in scheduleList)
            {
                therapyEvent.ShowSchedule();
            }
        }

        public static ScheduleResponse GetSchedule(SqlConnection sqlConnection)
        {
            ScheduleResponse scheduleResponse = new ScheduleResponse();


            // Set the SQL statement
            string sqlStatement = "SELECT ID,TITLE, START, END_DATE,URL,GROUP_ID FROM SCHEDULE";

            // Create a SqlCommand
            using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection))
            {
                // Create a SqlDataReader and execute the SQL command
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    // Check if the reader has rows
                    if (sqlDataReader.HasRows)
                    {
                        int row = 1;
                        // Read each row from the data reader
                        while (sqlDataReader.Read())
                        {
                            //Create an employee object
                            Schedule TherapyEvent = new Schedule();

                            // Populate the employee object from the database row
                            TherapyEvent.Id = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("ID"));

                            TherapyEvent.Title = sqlDataReader.GetString(sqlDataReader.GetOrdinal("TITLE"));

                            TherapyEvent.Start = sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("START"));

                            int endDateOrdinal = sqlDataReader.GetOrdinal("END_DATE");
                            TherapyEvent.EndDate = sqlDataReader.IsDBNull(endDateOrdinal) ? null : sqlDataReader.GetDateTime(endDateOrdinal);

                            int UrlOrdinal = sqlDataReader.GetOrdinal("URL");
                            TherapyEvent.Url = sqlDataReader.IsDBNull(UrlOrdinal) ? null : sqlDataReader.GetString(UrlOrdinal);

                            int GroupIdOrdinal = sqlDataReader.GetOrdinal("GROUP_ID");
                            TherapyEvent.GroupId = sqlDataReader.IsDBNull(GroupIdOrdinal) ? null : sqlDataReader.GetInt32(GroupIdOrdinal);



                            // Add the current employee to a list of employees
                            scheduleResponse.TherapyEvents.Add(TherapyEvent);

                            row++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                }
            }

            return scheduleResponse;
        }



        // public static int InsertEmployee(SqlConnection sqlConnection, string firstName, string lastName, decimal salary)
        // {
        //     int rowsUpdated = 0;

        //     // Set the SQL statement
        //     string sqlStatement = "insert into Employee (FirstName, LastName, Salary) values (@FirstName, @LastName, @Salary)";

        //     // Create a SqlCommand
        //     using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection))
        //     {
        //         // Add parameters
        //         sqlCommand.Parameters.AddWithValue("@FirstName", firstName);
        //         sqlCommand.Parameters.AddWithValue("@LastName", lastName);
        //         sqlCommand.Parameters.AddWithValue("@Salary", salary);

        //         // Execute the SQL command (and capture number of rows updated)
        //         rowsUpdated = sqlCommand.ExecuteNonQuery();
        //     }

        //     return rowsUpdated;
        // }

        //     public static int UpdateEmployee(SqlConnection sqlConnection, int employeeId, string firstName, string lastName, decimal salary)
        //     {
        //         int rowsUpdated = 0;

        //         // Set the SQL statement
        //         string sqlStatement = "update Employee set FirstName = @FirstName, LastName = @LastName, Salary = @Salary where EmployeeId = @EmployeeId";

        //         // Create a SqlCommand
        //         using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection))
        //         {
        //             // Add parameters
        //             sqlCommand.Parameters.AddWithValue("@FirstName", firstName);
        //             sqlCommand.Parameters.AddWithValue("@LastName", lastName);
        //             sqlCommand.Parameters.AddWithValue("@Salary", salary);
        //             sqlCommand.Parameters.AddWithValue("@EmployeeId", employeeId);

        //             // Execute the SQL command (and capture number of rows updated)
        //             rowsUpdated = sqlCommand.ExecuteNonQuery();
        //         }

        //         return rowsUpdated;
        //     }

        //     public static int DeleteEmployee(SqlConnection sqlConnection, int employeeId)
        //     {
        //         int rowsDeleted = 0;

        //         // Set the SQL statement
        //         string sqlStatement = "delete from Employee where EmployeeId = @EmployeeId";

        //         using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection))
        //         {
        //             // Add parameters
        //             sqlCommand.Parameters.AddWithValue("@EmployeeId", employeeId);

        //             // Execute the SQL command (and capture number of rows deleted)
        //             rowsDeleted = sqlCommand.ExecuteNonQuery();
        //         }

        //         return rowsDeleted;
        //     }

    }
}