using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace AuthService.API.Services
{
    public interface IAuthService : IDisposable
    {
        bool Authenticate(string login, string password);
        void RegisterObserver(IAuthObserver observer);
        void UnregisterObserver(IAuthObserver observer);
        void Logout();
    }

    public interface IAuthObserver
    {
        void OnLogin(string username);
        void OnLogout();
    }
    public class AuthService : IAuthService
    {
        private readonly List<IAuthObserver> observers = new();
        private string currentUser;
        private readonly ILogger<AuthService> logger;
        private readonly string _connectionString;

        public AuthService(IConfiguration configuration, ILogger<AuthService> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(_connectionString), "Connection string is missing");
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool Authenticate(string login, string password)
        {
            try
            {
                string query = "SELECT login FROM employees WHERE login = @login AND password = @password";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@login", SqlDbType.VarChar).Value = login;
                        command.Parameters.Add("@password", SqlDbType.VarChar).Value = password;

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            currentUser = result.ToString();
                            NotifyLogin(currentUser);
                            return true;
                        }

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error connecting to DB during authentication");
                return false;
            }
        }

        public void Logout()
        {
            currentUser = null;
            NotifyLogout();
        }

        public void RegisterObserver(IAuthObserver observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
        }

        public void UnregisterObserver(IAuthObserver observer)
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }

        private void NotifyLogin(string username)
        {
            foreach (var observer in observers)
                observer.OnLogin(username);
        }

        private void NotifyLogout()
        {
            foreach (var observer in observers)
                observer.OnLogout();
        }

        public void Dispose()
        {
        }
    }
}
