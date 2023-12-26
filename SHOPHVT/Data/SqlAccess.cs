using System;
using System.Data;
using System.Data.SqlClient;

public class SqlAccess
{
	private SqlConnection _connection;

	public SqlAccess()
	{
		_connection = new SqlConnection();

		string efConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB_HVTShopEntities"].ConnectionString;
		int connectionStringStartIndex = efConnectionString.IndexOf('"') + 1;
		int connectionStringLength = efConnectionString.Length - connectionStringStartIndex - 1;
		string connectionString = efConnectionString.Substring(connectionStringStartIndex, connectionStringLength);

		string trueConnectionString = "";
		string[] substrings = connectionString.Split(';');
		int startIndex = 0;
		int endIndex = 2;
		for (int i = startIndex; i <= endIndex; i++)
        {
			trueConnectionString += substrings[i] + ";";
		}
		_connection.ConnectionString = trueConnectionString;
		//System.Diagnostics.Debug.WriteLine("Connection String: " + connectionString);
		//System.Diagnostics.Debug.WriteLine("True Connection String: " + trueConnectionString);
	}
	
	public void OpenConnection()
	{
		if (_connection.State == ConnectionState.Closed)
		{
			_connection.Open();
		}
	}
	
	public void CloseConnection()
	{
		if (_connection.State == ConnectionState.Open)
		{
			_connection.Close();
		}
	}
	
	public DataTable GetTable(string command)
	{
		DataTable dataTable = null;
		try
		{
			OpenConnection();
			SqlDataAdapter adapter = new SqlDataAdapter(command, _connection);
			dataTable = new DataTable();
			adapter.Fill(dataTable);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}
		finally
		{
			CloseConnection();
		}
		return dataTable;
	}
	
	public DataTable GetTable(string procedureName, SqlParameter[] parameters)
	{
		DataTable dataTable = null;
		try
		{
			OpenConnection();
			dataTable = new DataTable();
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = _connection;
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.CommandText = procedureName;
			if (parameters != null)
			{
				sqlCommand.Parameters.AddRange(parameters);
			}
			SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
			adapter.Fill(dataTable);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}
		finally
		{
			CloseConnection();
		}
		return dataTable;
	}
	
	public void GetDataSet(ref DataSet dataSet, string command)
	{
		dataSet.Tables.Add(GetTable(command));
	}
	
	public void GetDataSet(ref DataSet dataSet, string procedureName, SqlParameter[] parameters)
	{
		dataSet.Tables.Add(GetTable(procedureName, parameters));
	}

	public int Execute(string command)
	{
		int rowAffectedCount = 0;
		try
		{
			OpenConnection();
			SqlCommand sqlCommand = new SqlCommand(command, _connection);
			rowAffectedCount = (int) sqlCommand.ExecuteNonQuery();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}
		finally
		{
			CloseConnection();
		}
		return rowAffectedCount;
	}
	
	public int Execute(string procedureName, SqlParameter[] parameters)
	{
		int rowAffectedCount = 0;
		try
		{
			OpenConnection();
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = _connection;
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.CommandText = procedureName;
			if (parameters != null)
			{
				sqlCommand.Parameters.AddRange(parameters);
			}
			rowAffectedCount = (int) sqlCommand.ExecuteNonQuery();
		}
		catch (SqlException ex)
		{
			System.Diagnostics.Debug.WriteLine(ex.ToString());
		}
		finally
		{
			CloseConnection();
		}
		return rowAffectedCount;
	}
}