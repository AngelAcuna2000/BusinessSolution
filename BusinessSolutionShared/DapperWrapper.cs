﻿using Dapper;
using System.Data;

namespace BusinessSolutionShared;

public class DapperWrapper : IDapperWrapper
{
    public int Execute(
        IDbConnection? conn,
        string? sql,
        object? param = null,
        IDbTransaction? transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        ArgumentNullException.ThrowIfNull(conn);

        ArgumentNullException.ThrowIfNull(sql);

        return conn.Execute(sql, param, transaction, commandTimeout, commandType);
    }

    public IEnumerable<T> Query<T>(
        IDbConnection? conn,
        string? sql,
        object? param = null,
        IDbTransaction? transaction = null,
        bool buffered = true,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        ArgumentNullException.ThrowIfNull(conn);

        ArgumentNullException.ThrowIfNull(sql);

        return conn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
    }

    public T QuerySingle<T>(
        IDbConnection? conn,
        string? sql,
        object? param = null,
        IDbTransaction? transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        ArgumentNullException.ThrowIfNull(conn);

        ArgumentNullException.ThrowIfNull(sql);

        return conn.QuerySingle<T>(sql, param, transaction, commandTimeout, commandType);
    }
}
