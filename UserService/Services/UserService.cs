using System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UserService.API.Models;


namespace UserService.API.Services
{
    public class UserService : IUserService
    {
        private readonly string _connectionString;
        private readonly ILogger<UserService> _logger;

        public UserService(IConfiguration configuration, ILogger<UserService> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("Connection string is missing");
            _logger = logger;
        }

        public bool Register(string username, string phone, string login, string password)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                string checkQuery = "SELECT TOP 1 Name_employee, PhoneNumbEmpl, login, password FROM dbo.Employees WHERE login = @login";
                using (var checkCmd = new SqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@login", login);
                    object result = checkCmd.ExecuteScalar();
                    int count = Convert.ToInt32(result ?? 0);
                    if (count > 0)
                        return false;

                }

                string insertQuery = "INSERT INTO dbo.Employees (Name_employee, PhoneNumbEmpl, login, password) VALUES (@name, @phone, @login, @pass)";
                using (var command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@name", username);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@pass", password);

                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Помилка при реєстрації користувача");
                return false;
            }
        }
    }
}
