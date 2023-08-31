using System;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace DapperPractData.Data
{
	public class DataAccess : IDataAccess
	{
		private readonly IConfiguration _config;

		public DataAccess(IConfiguration config)
		{
			_config = config;
		}

        // this method will retuarn a list of type T
        public async Task<IEnumerable<T>> GetData<T, P>(string query, P parameters,
			string connectionId = "DefaultConnection")
		//get the parameter T dynamically
		{
			using IDbConnection connection =
				new MySqlConnection(_config.GetConnectionString(connectionId));
			return await connection.QueryAsync<T>(query, parameters);
		}

        //This method will not return anything,
		//Used for saving data to the DB
        public async Task SaveData<P>
            (string query, P parameters, string connectionId = "DefaultConnection")
        {
            using IDbConnection connection =
                 new MySqlConnection(_config.GetConnectionString(connectionId));
            await connection.ExecuteAsync(query, parameters);

        }

    }
}

