[[_TOC_]]

# Sql Server Connector for UiPath

In this package, there are 3 activities:
+ Open sql server activity: Open a connection impersonated or not, with sql server
+ Execute query sql server activity: A basic operation with that connection
+ Close sql server activity: Closes the connection

## Open Sql Server Activity

### Objective
If the value of Impersonate is set to true, it will use the values of User and Password to perform the impersonation.
If the value of Impersonate is set to false, it will default to the credentials of the user logged into the system.

The ultimate goal is to use the impersonation functionality to enable access in cases where a specific user has access to a connection string but lacks local permissions.

### Input
[string] User
[string] Password
[string] ConnectionString
[bool] Impersonate

### Output
[SqlConnection] Connection

## Execute query sql server activity

### Objective
A basic execution of the sql server's query. May be text, stored procedure... you choose.

### Input
[Dictionary<string,object>] CommandParameters
[string] CommandText
[Enum : CommandType] CommandType
[SqlConnection] Connection

### Output
[DataSet] Result

## Close sql server activity

### Objective
Closes the connection's instance

### Input
[SqlConnection] Connection

### Output
N/A

## References
### Standard Security

```csharp
Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;
```

### Trusted Connection
```csharp
Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;
```

### Connection to a SQL Server instance
The server/instance name syntax used in the server option is the same for all SQL Server connection strings.

```csharp
Server=myServerName\myInstanceName;Database=myDataBase;User Id=myUsername;Password=myPassword;
```

### Using a non-standard port
If your SQL Server listens on a non-default port you can specify that using the servername,xxxx syntax (note the comma, it's not a colon).

```csharp
Server=myServerName,myPortNumber;Database=myDataBase;User Id=myUsername;Password=myPassword;
```
