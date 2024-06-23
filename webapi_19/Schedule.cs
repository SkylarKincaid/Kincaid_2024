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



        public static int InsertSchedule(SqlConnection sqlConnection, string title, DateTime start, DateTime endDate)
        {
            // created a variable of rowsUpdated with type int and value 0
            int rowsUpdated = 0;

            // Set the SQL statement
            string sqlStatement = "insert into Schedule (TITLE, START, END_DATE) values (@TITLE, @START, @END_DATE)";

            // Create a SqlCommand
            using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection))
            {
                // Add parameters
                sqlCommand.Parameters.AddWithValue("@TITLE", title);
                sqlCommand.Parameters.AddWithValue("@START", start);
                sqlCommand.Parameters.AddWithValue("@END_DATE", endDate);

                // Execute the SQL command (and capture number of rows updated)
                rowsUpdated = sqlCommand.ExecuteNonQuery();
            }

            return rowsUpdated;
        }

        public static int UpdateSchedule(SqlConnection sqlConnection, string title, int ID)
        {
            //creates a value for rowsUpdated
            int rowsUpdated = 0;

            // Creates a string statment for Update Schedule with placeholders
            string sqlStatement = "update Schedule set TITLE = @title Where ID = @ID";

            // Says we use some sql stuff to create stuff
            using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection))
            {
                // Adds paramaters that assigns values to the placeholders
                sqlCommand.Parameters.AddWithValue("@title", title);
                sqlCommand.Parameters.AddWithValue("@ID", ID);
                // Execute the SQL command 
                rowsUpdated = sqlCommand.ExecuteNonQuery();
            }
            // Returns number of rows updated
            return rowsUpdated;
        }

        //    Public means it can be accessed by the entire site. Then, we created a function of DeleteSchedule that returns on object of type int. 
        public static int DeleteSchedule(SqlConnection sqlConnection, int ID)
        {
            // Here we created a variable named rowsDeleted with type int and value 0. 
            int rowsDeleted = 0;

            // Here we create the variable sqlStatement with type string and the value. 
            string sqlStatement = "delete from Schedule where ID = @ID";

            // Here we told the database to connect and do some stuff
            using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection))
            {
                // Here we added the ID parameter
                sqlCommand.Parameters.AddWithValue("@ID", ID);

                // Here we executed the query to return the variable value
                rowsDeleted = sqlCommand.ExecuteNonQuery();
            }
            // Here we will return the variable value. 
            return rowsDeleted;
        }

    }
}