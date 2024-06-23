using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace webapi_02.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : ControllerBase
    {
        //Logger
        private readonly ILogger<ScheduleController> _logger;

        public ScheduleController(ILogger<ScheduleController> logger)
        {
            _logger = logger;
        }

        //Connection string
        private static string serverName = @"LAPTOP-GB7AKJI3\SQLEXPRESS01"; //Change to the "Server Name" you see when you launch SQL Server Management Studio.
        private static string databaseName = "Theropy Project"; //Change to the database where you created your Employee table.
        private static string connectionString = $"data source={serverName}; database={databaseName}; Integrated Security=true;Encrypt=true;TrustServerCertificate=true;";


        [HttpGet]
        [Route("/Schedule")]
        public Response GetSchedule()
        {
            Response response = new Response();

            try

            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();


                    response.ScheduleResponse = Schedule.GetSchedule(sqlConnection);
                    response.Message = $"{response.ScheduleResponse.TherapyEvents.Count} events returned.";

                    response.Result = Result.success;
                }
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred in GetSchedule: {ex.Message}";
                response.Result = Result.error;
            }

            return response;
        }

        [HttpGet]
        [Route("/InsertSchedule")]
        public Response InsertSchedule(string title, string start, string endDate)
        {
            Response response = new Response();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    //Insert employee
                    int rowsInserted = Schedule.InsertSchedule(sqlConnection, title, DateTime.Parse(start), DateTime.Parse(endDate));
                    response.Message = $"{rowsInserted} schedule inserted. ";

                    response.Result = Result.success;
                }
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred in InsertSchedule: {ex.Message}";
                response.Result = Result.error;
            }

            return response;
        }

        [HttpGet]
        [Route("/UpdateSchedule")]
        // Public means the entire site can access it. The function returns a response with the name Updated Schedule. Title and ID are the paramaters. 
        public Response UpdateSchedule(string title, int ID)
        {
            // Creating a variable of response with type Response and value of new Response - or an empty response object
            Response response = new Response();

            Console.WriteLine(response);

            try
            {
                // creating connection
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    //This is a variable named rowsUpdated of type int. Its value is what is returned from the UpdateSchedule funciton in the Schedule class. 
                    int rowsUpdated = Schedule.UpdateSchedule(sqlConnection, title, ID);

                    // This sets the message parameter of the response varaiable to the string. 
                    response.Message = $"{rowsUpdated} schedule updated. ";


                    Console.WriteLine(response);

                    // This sets the result parameter of the response variable to the enum. 
                    response.Result = Result.success;
                    Console.WriteLine(response);
                }
            }
            // catches errors
            catch (Exception ex)
            {
                response.Message = $"An error occurred in UpdateSchedule: {ex.Message}";
                response.Result = Result.error;
            }
            //  returns variable response
            return response;
        }

        [HttpGet]
        [Route("/DeleteSchedule")]
        // Public means the entire site can access this. Here the function returns response and is named Delete Schedule. ID is a parameter.
        public Response DeleteSchedule(int ID)
        {
            // We crated a variable response with type Response and value new Response. 
            Response response = new Response();

            try
            {
                // Here we connect to the database and do stuff
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    //Here we create variable rowsDeleted with type int and set a value for what class schedule and function DeleteSchedule returns. 
                    int rowsDeleted = Schedule.DeleteSchedule(sqlConnection, ID);
                    // This sets the message parameter of the response to the string. 
                    response.Message = "${rowsDeleted} employees deleted. ";
                    // This sets the result parameter of the response variable to the enum.
                    response.Result = Result.success;
                }
            }
            // This catches errors
            catch (Exception ex)
            {
                response.Message = $"An error occurred in DeleteSchedule: {ex.Message}";
                response.Result = Result.error;
            }
            // This returns the response variable
            return response;
        }
    }
}