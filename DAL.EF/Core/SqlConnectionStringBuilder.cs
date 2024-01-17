namespace DAL.EF.Core;

using Microsoft.Extensions.Configuration;

public class SqlConnectionStringBuilder
{
    private readonly IConfiguration _configuration;

    public SqlConnectionStringBuilder(IConfiguration configuration, string connectionStringName)
    {
        _configuration = configuration;

        string connectionString = configuration.GetConnectionString(connectionStringName)!;

        string[] parts = connectionString.Split(';');
        var dictionary = parts.Select(part => part.Split('=')).ToDictionary(strings => strings[0].Trim().ToLower(),
            strings => strings[1].Trim());

        Database = GetStrValue(dictionary["database"], "DATABASE");
        Password = GetStrValue(dictionary["password"], "DATABASE_PASSWORD");
        Port = int.Parse(GetStrValue(dictionary["port"], "SQL_SERVER_PORT"));
        Server = GetStrValue(dictionary["server"], "SQL_SERVER_HOST");
        User = GetStrValue(dictionary["uid"], "DATABASE_USER");
    }

    public string Database { get; set; }
    public string Password { get; set; }
    public int Port { get; set; }
    public string Server { get; set; }
    public string User { get; set; }

    private string GetStrValue(string initialValue, string environmentVariableName)
    {
        return _configuration[environmentVariableName] ?? initialValue;
    }

    public override string ToString()
    {
        return $"Server={Server}; port={Port}; Database={Database}; uid={User}; Password={Password}";
    }
}