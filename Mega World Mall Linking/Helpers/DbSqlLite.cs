using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Forms;

public class DbSQLite
{
    private string DatabasePath { get; set; }
    private string DatabaseName { get; set; }

    public DbSQLite(string databasePath, string databaseName)
    {
        if (string.IsNullOrEmpty(databasePath))
        {
            throw new ArgumentNullException(nameof(databasePath), "Database path is empty");
        }
        if (string.IsNullOrEmpty(databaseName))
        {
            throw new ArgumentNullException(nameof(databaseName), "Database name is empty");
        }

        DatabaseName = databaseName;
        DatabasePath = Path.Combine(databasePath, databaseName);

        if (!string.IsNullOrEmpty(databasePath) && !Directory.Exists(databasePath))
        {
            Directory.CreateDirectory(databasePath);
        }

        if (!File.Exists(DatabasePath))
        {
            SQLiteConnection.CreateFile(DatabasePath);
        }
        EnsureTablesExist();
    }


    //Kim - 09042026 
    public string GetDatabasePath()
    {
        return DatabasePath;
    }


    public string GetConnectionString()
    {
        return $"Data Source={DatabasePath};Version=3;";
    }

    public SQLiteConnection GetConnection()
    {
        var conn = new SQLiteConnection(GetConnectionString());
        conn.Open();
        return conn;
    }

    public int ExecuteNonQuery(string sql)
    {
        using (var conn = GetConnection())
        using (var cmd = new SQLiteCommand(sql, conn))
        {
            return cmd.ExecuteNonQuery();
        }
    }

    public string ExecuteScalar(string sql)
    {
        using (var conn = GetConnection())
        using (var cmd = new SQLiteCommand(sql, conn))
        {
            var result = cmd.ExecuteScalar();
            return result?.ToString();
        }
    }

    public bool CreateTable(string tableName, Dictionary<string, string> data)
    {
        if (IsTableExist(tableName)) return false;

        var columns = string.Join(", ", data.Select(kv => $"{kv.Key} {kv.Value}"));
        var sql = $"CREATE TABLE {tableName} ({columns});";
        ExecuteNonQuery(sql);
        return true;
    }

    public bool DropTable(string tableName)
    {
        var sql = $"DROP TABLE IF EXISTS {tableName};";
        ExecuteNonQuery(sql);
        return true;
    }

    public bool Update(string tableName, Dictionary<string, string> data, string where)
    {
        var setClause = string.Join(", ", data.Select(kv => $"{kv.Key} = {kv.Value}"));
        var sql = $"UPDATE {tableName} SET {setClause} WHERE {where};";
        ExecuteNonQuery(sql);
        return true;
    }

    public bool Delete(string tableName, string where)
    {
        var sql = $"DELETE FROM {tableName} WHERE {where};";
        ExecuteNonQuery(sql);
        return true;
    }

    public bool Insert(string tableName, Dictionary<string, string> data)
    {
        var columns = string.Join(", ", data.Keys);
        var values = string.Join(", ", data.Values);
        var sql = $"INSERT INTO {tableName} ({columns}) VALUES ({values});";
        ExecuteNonQuery(sql);
        return true;
    }

    public bool AddColumn(string tableName, string columnName, string dataType)
    {
        var sql = $"ALTER TABLE {tableName} ADD COLUMN {columnName} {dataType};";
        ExecuteNonQuery(sql);
        return true;
    }

    public bool IsTableExist(string tableName)
    {
        using (var conn = GetConnection())
        using (var cmd = new SQLiteCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}';", conn))
        using (var reader = cmd.ExecuteReader())
        {
            return reader.HasRows;
        }
    }

    public DataTable GetDataTable(string sql)
    {
        var dt = new DataTable();
        using (var conn = GetConnection())
        using (var cmd = new SQLiteCommand(sql, conn))
        using (var reader = cmd.ExecuteReader())
        {
            dt.Load(reader);
        }
        return dt;
    }

    public List<string> GetTables()
    {
        var tables = new List<string>();
        using (var conn = GetConnection())
        using (var cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table';", conn))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                tables.Add(reader.GetString(0));
            }
        }
        return tables;
    }

    public string GetColumnValue(string tableName, string columnName, string condition)
    {
        var sql = $"SELECT {columnName} FROM {tableName} WHERE {condition} LIMIT 1;";
        var dt = GetDataTable(sql);
        return dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : string.Empty;
    }

    public List<string> GetColumns(string tableName)
    {
        var columns = new List<string>();
        using (var conn = GetConnection())
        using (var cmd = new SQLiteCommand($"PRAGMA table_info({tableName});", conn))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                columns.Add(reader[1].ToString());
            }
        }
        return columns;
    }

    public int GetStatusCount(string tableName, string condition = "1=1")
    {
        var sql = $"SELECT COUNT(*) FROM {tableName} WHERE {condition};";
        return Convert.ToInt32(ExecuteScalar(sql));
    }

    public bool IsColumnExist(string columnName, string tableName)
    {
        return GetColumns(tableName).Contains(columnName);
    }

    public bool IsExist(string tableName, string condition)
    {
        return GetStatusCount(tableName, condition) > 0;
    }

    public bool IsValueExist(string tableName, string column, string value)
    {
        string safeVal = (value ?? string.Empty).Replace("'", "''");
        var sql = $"SELECT 1 FROM {tableName} WHERE {column} = '{safeVal}' LIMIT 1;";
        var dt = GetDataTable(sql);
        return dt.Rows.Count > 0;
    }

    public void EnsureTablesExist()
    {
        ExecuteNonQuery(@"
            CREATE TABLE IF NOT EXISTS orderdata (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                OrderNo TEXT,
                AccDate TEXT,
                Time TEXT,
                Time_Now TEXT,
                TableNo TEXT,
                Total DECIMAL(18,2) DEFAULT 0,
                TaxTotal DECIMAL(18,2) DEFAULT 0,
                ServiceCharge DECIMAL(18,2) DEFAULT 0,
                Posted INTEGER DEFAULT 0,
                Void INTEGER DEFAULT 0
            );
            CREATE TABLE IF NOT EXISTS discountdata (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                OrderNo TEXT,
                Type TEXT,
                Amount DECIMAL(18,2) DEFAULT 0
            );
            CREATE TABLE IF NOT EXISTS voiddata (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                OrderID TEXT,
                Total DECIMAL(18,2) DEFAULT 0
            );
            CREATE TABLE IF NOT EXISTS paymentdata (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                OrderNo TEXT,
                Name TEXT,
                Amount DECIMAL(18,2) DEFAULT 0
            );
            CREATE TABLE IF NOT EXISTS itemdata (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                OrderNo TEXT,
                ItemCode TEXT,
                ItemName TEXT,
                Qty DECIMAL(18,2) DEFAULT 0,
                Amount DECIMAL(18,2) DEFAULT 0
            );
        ");
    }

    public bool IsEmpty(string tableName)
    {
        return GetStatusCount(tableName) == 0;
    }

    public int GetLastID(string tableName)
    {
        var sql = $"SELECT ID FROM {tableName} ORDER BY ID DESC LIMIT 1;";
        var dt = GetDataTable(sql);
        return dt.Rows.Count > 0 && int.TryParse(dt.Rows[0][0].ToString(), out var id) ? id : 0;
    }
}
