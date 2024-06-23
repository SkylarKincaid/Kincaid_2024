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


                    //Select employees
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

        // [HttpGet]
        // [Route("/InsertEmployee")]
        // public Response InsertEmployee(string firstName, string lastName, decimal salary, string search = "", int pageSize = 10, int pageNumber = 1, string sort = "EmployeId")
        // {
        //     Response response = new Response();

        //     try
        //     {
        //         using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //         {
        //             sqlConnection.Open();

        //             //Insert employee
        //             int rowsInserted = Employee.InsertEmployee(sqlConnection, firstName, lastName, salary);
        //             response.Message = "${rowsInserted} employees inserted. ";

        //             //Select employees after insert
        //             response.EmployeeResponse = Employee.SearchEmployees(sqlConnection, search, pageSize, pageNumber, sort);
        //             response.Message = $"{response.EmployeeResponse.Employees.Count} employees selected.";

        //             response.Result = Result.success;
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         response.Message = $"An error occurred in InsertEmployee: {ex.Message}";
        //         response.Result = Result.error;
        //     }

        //     return response;
        // }

        // [HttpGet]
        // [Route("/UpdateEmployee")]
        // public Response UpdateEmployee(int employeeId, string firstName, string lastName, decimal salary, string search = "", int pageSize = 10, int pageNumber = 1, string sort = "EmployeId")
        // {
        //     Response response = new Response();

        //     try
        //     {
        //         using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //         {
        //             sqlConnection.Open();

        //             //Update employee
        //             int rowsUpdated = Employee.UpdateEmployee(sqlConnection, employeeId, firstName, lastName, salary);
        //             response.Message = "${rowsUpdated} employees updated. ";

        //             //Select employees after update
        //             response.EmployeeResponse = Employee.SearchEmployees(sqlConnection, search, pageSize, pageNumber, sort);
        //             response.Message = $"{response.EmployeeResponse.Employees.Count} employees selected.";

        //             response.Result = Result.success;
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         response.Message = $"An error occurred in UpdateEmployee: {ex.Message}";
        //         response.Result = Result.error;
        //     }

        //     return response;
        // }

        // [HttpGet]
        // [Route("/DeleteEmployee")]
        // public Response DeleteEmployee(int employeeId, string search = "", int pageSize = 10, int pageNumber = 1, string sort = "EmployeId")
        // {
        //     Response response = new Response();

        //     try
        //     {
        //         using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //         {
        //             sqlConnection.Open();

        //             //Delete employees
        //             int rowsDeleted = Employee.DeleteEmployee(sqlConnection, employeeId);
        //             response.Message = "${rowsDeleted} employees deleted. ";

        //             //Select employees after delete
        //             response.EmployeeResponse = Employee.SearchEmployees(sqlConnection, search, pageSize, pageNumber, sort);
        //             response.Message = $"{response.EmployeeResponse.Employees.Count} employees selected.";

        //             response.Result = Result.success;
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         response.Message = $"An error occurred in DeleteEmployee: {ex.Message}";
        //         response.Result = Result.error;
        //     }

        //     return response;
        // }
    }
}