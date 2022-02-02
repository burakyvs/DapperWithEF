﻿using DataAccess.EntityFramework.Contexts;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public abstract class DbConnectorBase : IDbConnector
    {
        public string ConnectionString { get => _connectionString; set => _connectionString = value; }
        private string _connectionString;

        public bool IsTransactionActive { get => _isTransactionActive; set => _isTransactionActive = value; }
        private bool _isTransactionActive = false;

        public DbConnectorBase()
        {
            _connectionString = ProjectDbContext.ConnectionString;
        }

        public virtual async Task<DbConnection> OpenConnectionAsync()
        {
            try
            {
                DbConnection connection = new NpgsqlConnection(ConnectionString);
                await connection.OpenAsync();

                return connection;
            }
            catch (Exception error)
            {
                throw new Exception($"Cannot open database connection: Message: ${error.Message}, StackTrace: {error.StackTrace}");
            }
        }

        public async Task CloseConnectionAsync(DbConnection connection)
        {
            try
            {
                await connection.CloseAsync();
                await connection.DisposeAsync();
            }
            catch (Exception error)
            {
                throw new Exception($"Cannot close database connection: Message: ${error.Message}, StackTrace: {error.StackTrace}");
            }
        }

        public async Task<DbTransaction> BeginTransaction(DbConnection connection)
        {
            try
            {
                var transaction = await connection.BeginTransactionAsync();
                if (transaction != null)
                {
                    IsTransactionActive = true;
                    return transaction;
                }
                else
                {
                    IsTransactionActive = false;
                    throw new Exception($"Cannot begin transaction.");
                }
            }
            catch (Exception error)
            {
                throw new Exception($"Cannot begin transaction: Message: ${error.Message}, StackTrace: {error.StackTrace}");
            }
        }

        public async Task CloseTransaction(DbTransaction transaction, bool commit = true)
        {
            try
            {
                if (commit)
                    await transaction.CommitAsync();
                else
                {
                    await transaction.RollbackAsync();
                    await transaction.DisposeAsync();
                }

            }
            catch (Exception error)
            {
                throw new Exception($"Cannot close transaction: Message: ${error.Message}, StackTrace: {error.StackTrace}");
            }
        }

        public async Task RollbackTransaction(DbTransaction transaction, string? savePointName = null)
        {
            try
            {
                if (savePointName != null)
                    await transaction.RollbackAsync(savePointName);
                else
                {
                    await transaction.RollbackAsync();
                }

            }
            catch (Exception error)
            {
                throw new Exception($"Cannot rollback transaction: Message: ${error.Message}, StackTrace: {error.StackTrace}");
            }
        }

        public async Task SaveTransaction(DbTransaction transaction, string savePointName)
        {
            try
            {
                await transaction.SaveAsync(savePointName);
            }
            catch (Exception error)
            {
                throw new Exception($"Cannot save transaction: Message: ${error.Message}, StackTrace: {error.StackTrace}");
            }
        }

        public async Task ReleaseSavePoint(DbTransaction transaction, string savePointName)
        {
            try
            {
                await transaction.ReleaseAsync(savePointName);
            }
            catch (Exception error)
            {
                throw new Exception($"Cannot release transaction save point: Message: ${error.Message}, StackTrace: {error.StackTrace}");
            }
        }
    }
}
