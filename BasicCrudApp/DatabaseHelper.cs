using System;
using MySql.Data.MySqlClient;

public class DatabaseHelper
{
    #region Proprierties

    public SqlConnection MyBdConnection { get; set; }
    public string NameStringConnection { get; set; }

    #endregion

    #region Constructors

    public DatabaseHelper()
    {
        this.NameStringConnection = "DefaultStringConnection";
        this.MyBdConnection = new SqlConnection(this.NameStringConnection);
    }

    public DatabaseHelper(string nameStringConnection)
    {
        this.NameStringConnection = nameStringConnection;
        this.MyBdConnection = new SqlConnection(this.NameStringConnection);
    }

    #endregion

    #region Private Methods

    private string GetCorrectParameterName(string parameterName)
    {
        if (parameterName[0] != '@')
        {
            parameterName = "@" + parameterName;
        }
        return parameterName;
    }

    #endregion

    #region Public Methods

    public static DatabaseHelper Create()
    {
        return new DatabaseHelper();
    }

    public static DatabaseHelper Create(string nameStringConnection)
    {
        return new DatabaseHelper(nameStringConnection);
    }

    public void OpenConnection()
    {
        if (this.MyBdConnection.State == System.Data.Connection.Closed)
        {
            this.MyBdConnection.Open();
        }
    }

    public void CloseConnection()
    {
        this.MyBdConnection.Close();
    }

    public SqlParameter BuildParameter(string name, object value, DbType type, int size)
    {
        SqlParameter parameter = new SqlParameter()
        {
            this.GetCorrectParameterName(name), value;

        parameter.DbType = type;
        parameter.Size = size;
        return parameter;
    }

    public void BuildParameter(string name, object value, DbType type, int size, List listparameters)
    {
        SqlParameter parameter = this.BuildParameter(name, value, type, size);
        listparameters.Add(parameter);
    }

    public SqlParameter BuildOutPutParameter(string name, DbType type, int size)
    {
        SqlParameter parameter = new SqlParameter();
        parameter.ParameterName = this.GetCorrectParameterName(name);
        parameter.DbType = type;
        parameter.Size = size;
        parameter.Direction = ParameterDirection.Output;
        return parameter;
    }

    public void ExecuteNonQuery(SqlCommand command)
    {
        command.ExecuteNonQuery();
    }

    public void ExecuteNonQuery(SqlCommand command, bool openConnection)
    {
        if (openConnection)
        {
            this.OpenConnection();
        }
        this.ExecuteNonQuery(command);
        if (openConnection)
        {
            this.CloseConnection();
        }
    }

    public void ExecuteNonQuery(string query, params SqlParameter[] parameters)
    {
        Exception erro = null;
        try
        {
            this.OpenConnection();
            SqlCommand command = this.MyBdConnection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddRange(parameters);
            this.ExecuteNonQuery(command);
            this.CloseConnection();
        }
        catch (Exception ex)
        {
            erro = ex;
        }
        finally
        {
            this.CloseConnection(erro);
        }
        if (erro != null)
        {
            throw erro;
        }
    }

    public void ExecuteCommands(params SqlCommand[] commands)
    {
        Exception erro = null;
        SqlTransaction trans = null;
        try
        {
            this.MyBdConnection.Open();
            trans = this.MyBdConnection.BeginTransaction();
            for (int i = 0; i < commands.Length; i++)
            {
                commands[i].Transaction = trans;
                this.ExecuteNonQuery(commands[i]);
            }
            trans.Commit();
            this.MyBdConnection.Close();
        }
        catch (Exception ex)
        {
            trans.Rollback();
            erro = ex;
        }
        finally
        {
            this.MyBdConnection.Close();
        }

        if (erro != null)
        {
            throw erro;
        }
    }
    #endregion
}