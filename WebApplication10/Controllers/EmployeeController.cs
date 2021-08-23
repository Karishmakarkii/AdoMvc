using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class EmployeeController : Controller
    {
        public IConfiguration Configuration { get; }

        public EmployeeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Employee()
        {
            List<Employee> employeeList = new List<Employee>();
            string connectionString = Configuration["ConnectionStrings:Default"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "Select * From employee";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Employee employee = new Employee();
                        employee.ID = Convert.ToInt32(dataReader["ID"]);
                        employee.FirstName = Convert.ToString(dataReader["FirstName"]);
                        employee.LastName = Convert.ToString(dataReader["LastName"]);


                        employeeList.Add(employee);
                    }
                }

                connection.Close();
            }

            return View("Index" , employeeList);
        }
        public IActionResult EmployeeCreate()
        {

            return View();
        }
       
        public IActionResult EmployeeUpdate()
        {

            return View();
        }
        public IActionResult EmployeeDelete()
        {

            return View();
        }
    }
}
