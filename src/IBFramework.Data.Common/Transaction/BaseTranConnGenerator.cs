﻿using IBFramework.Core.Data;
using System;
using System.Data;

namespace IBFramework.Data.Common.Transaction
{
    public abstract class BaseTranConnGenerator : ITranConnGenerator
    {
        public ITranConn GenerateTranConn(string connectionString, IsolationLevel isolation = IsolationLevel.ReadUncommitted)
        {
            IDbConnection conn = InternalGetDbConnection(connectionString);

            if (connectionString == null)
            {
                throw new Exception("Your service has not been initialized! The connection is going to fail!");
            }

            //Seems this is required
            conn.Open();

            IDbTransaction tran = conn.BeginTransaction(isolation);
            return new TranConn(conn, tran);
        }

        protected abstract IDbConnection InternalGetDbConnection(string connectionString);
    }
}
