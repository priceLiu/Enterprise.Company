using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace CN100.EnterprisePlatform.ORM
{
	public interface IDb : IDisposable
	{
		bool IsClosed { get; }
        bool IsAutoCommit { get; set; }
        bool IsAutoClose { get; set; }
        IDbConnection Connection { get; set; }
		IDbTransaction Transaction { get; }
		
		void Begin();
		void Commit();
		void Rollback();
		
		int Insert(Type type, ICollection items);
		int Insert<T>(ICollection<T> items);
		int Insert(object item);
		
		int Update(Type type, ICollection items);
		int Update<T>(ICollection<T> items);
		int Update(object item);

		
		int Delete(Type type, ICollection items);
		int Delete<T>(ICollection<T> items);
		int Delete(object item);
		int Delete(Type type, IQuery query);
		int Delete<T>(IQuery query);
		
		IList Select(Type type, IQuery query);
		IList<T> Select<T>(IQuery query);

        IList<T> Select<T>(string sql);
        IList<T> Select<T>(string sql, string[] parameterName, Object[] parameters);
        IList<T> Select<T>(string sql, IDbTransaction transaction);
        IList<T> Select<T>(string sql, string[] parameterName, Object[] parameters, IDbTransaction transaction);
        IList<T> SelectByStoreProc<T>(string storeProc);
        IList<T> SelectByStoreProc<T>(string storeProc, string[] parameterName, Object[] parameters);
        IList<T> SelectByStoreProc<T>(string storeProc, IDbTransaction transaction);
        IList<T> SelectByStoreProc<T>(string storeProc, string[] parameterName, Object[] parameters, IDbTransaction transaction);
        IList<T> Select<T>(string sql, string[] parameterName, Object[] parameters, IDbTransaction transaction, CommandType commType);
        IList<T> Select<T>(string procName, string[] parameterName, object[] parameters, string[] outputParameterName, int?[] outputParameterSize, DbType[] outputParameterType,
            ParameterDirection[] outputParameterDirection, out object[] outParameterResult);
		
		object Find(Type type, object key);
		T Find<T>(object key);
		
		object Call(string funcName, object[] parameters);
		
		IList Exec(Type type, string procName, object[] parameters);
        IList Exec(Type type, string procName, string[] parameterName, object[] parameters);
		IResultSet Exec(string procName, object[] parameters);
		IResultSet Exec(string procName, object[] parameters, int[] outputs);
        IResultSet Exec(string procName, string[] parameterName, object[] parameters, string[] outputParameterName, int?[] outputParameterSize, DbType[] outputParameterType,
            ParameterDirection[] outputParameterDirection, out object[] outParameterResult);
		
		IQuery Query();
		ITable GetTable(Type type);

        #region GetDateSet Interface

        DataSet GetDataSet(string sql, string table);

        DataSet GetDataSet(string sql, string table, string[] parameterName, Object[] parameters);

        DataSet GetDataSet(string sql, string table, IDbTransaction transaction);

        DataSet GetDataSet(string sql, string table, string[] parameterName, Object[] parameters, IDbTransaction transaction);

        DataSet GetDataSetByStoreProc(string storeProc, string table);

        DataSet GetDataSetByStoreProc(string storeProc, string table, string[] parameterName, Object[] parameters);

        DataSet GetDataSetByStoreProc(string storeProc, string table, IDbTransaction transaction);

        DataSet GetDataSetByStoreProc(string storeProc, string table, string[] parameterName, Object[] parameters, IDbTransaction transaction);

        #endregion

        #region ExecuteNonQuery

        int ExecuteNonQuery(string sql);

        int ExecuteNonQuery(string sql, string[] parameterName, Object[] parameters);

        int ExecuteNonQuery(string sql, IDbTransaction transaction);

        int ExecuteNonQuery(string sql, string[] parameterName, Object[] parameters, IDbTransaction transaction);

        #endregion

        #region ExecuteScalar

        object ExecuteScalar(string sql);

        object ExecuteScalar(string sql, string[] parameterName, Object[] parameters);

        object ExecuteScalar(string sql, IDbTransaction transaction);

        object ExecuteScalar(string sql, string[] parameterName, Object[] parameters, IDbTransaction transaction);

        #endregion

        #region ExecuteStoreProcedure

        object[] ExecuteStoreProc(string storeProc, string[] parameterName, Object[] parameters,
            string[] outputParameterName, int?[] outputParameterSize, DbType[] outputParameterType,
            ParameterDirection[] outputParameterDirection);

        object[] ExecuteStoreProc(string storeProc, string[] parameterName, Object[] parameters,
            string[] outputParameterName, int?[] outputParameterSize, DbType[] outputParameterType,
            ParameterDirection[] outputParameterDirection, IDbTransaction transaction);

        #endregion

        #region PrivateFunction

        DataSet GetDataSet(string sql, string table, string[] parameterName, Object[] parameters, IDbTransaction transaction, CommandType commType);

        void AddParameters(IDbCommand command, string[] parameterName, Object[] parameters, IDbTransaction transaction);

        DbParameter[] AddOutputReturnParameters(IDbCommand command, string[] outputParameterName,
            int?[] outputParameterSize, DbType[] outputParameterType, ParameterDirection[] outputParameterDirection,
            IDbTransaction transaction);

        #endregion
	}
}
