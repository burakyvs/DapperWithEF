﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IDbConnector
    {
        public string ConnectionString { get; set; }
        public bool IsTransactionActive { get; set; }
        public Task<SqlConnection> OpenConnectionAsync();
        public Task CloseConnectionAsync(SqlConnection connection);
        public Task<DbTransaction> BeginTransaction(SqlConnection connection);
        public Task CloseTransaction(DbTransaction transaction, bool commit = true);
        public Task RollbackTransaction(DbTransaction transaction, string? savePointName = null);
        public Task SaveTransaction(DbTransaction transaction, string savePointName);
        public Task ReleaseSavePoint(DbTransaction transaction, string savePointName);
    }
}