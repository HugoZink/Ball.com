using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using ExampleService.Models;
using Polly;

namespace ExampleService.Repositories
{
    class ExampleRepository : IExampleRepository
    {
        private string _connectionString;

        public ExampleRepository(string connectionString)
        {
            _connectionString = connectionString;

            // initialize database
            Policy
                .Handle<Exception>()
                .WaitAndRetry(5, r => TimeSpan.FromSeconds(5), (ex, ts) => { Console.WriteLine("Error connecting to DB. Retrying in 5 sec."); })
                .Execute(() => InitDb());
        }

        private async void InitDb()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString.Replace("Examples", "master")))
            {
                conn.Open();

                // create database
                string sqlQuery =
                    "IF DB_ID('Examples') IS NULL " +
                    "CREATE DATABASE Examples;"
                ;

                await conn.ExecuteAsync(sqlQuery);

                // create tables
                conn.ChangeDatabase("Examples");

                sqlQuery =
                    "IF OBJECT_ID('Tests') IS NULL " +
                    "CREATE TABLE Tests (" +
                    "   TestId varchar(50) NOT NULL," +
                    "   PRIMARY KEY(TestId)" +
                    ");"
                ;

                await conn.ExecuteAsync(sqlQuery);
            }
        }

        public async Task<Example> GetExampleAsynch(string exampleId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryFirstOrDefaultAsync<Example>(
                    "SELECT * FROM Tests" +
                    "WHERE TestId = " + exampleId
                );
            }
        }

        public async Task<IEnumerable<Example>> GetExamplesAsync()
        {
            List<Example> examples = new List<Example>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var examplesSelection = await conn.QueryAsync<Example>(
                    "SELECT * FROM Tests"
                );

                if (examplesSelection != null)
                {
                    examples.AddRange(examplesSelection);
                }
            }

            return examples;
        }
    }
}
