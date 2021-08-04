using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EF.SPExecuter
{
    public class DBConnection<T> where T : class, new()
    {
        private readonly DatabaseContext _db;
        private readonly DbSet<T> _dbSet;

        public DBConnection(DatabaseContext context)
        {
            _db = context;
            _dbSet = _db.Set<T>();
        }

        public List<T> executeStoreProcedure(string storeProcedureName,string parameterName, object value)
        {
            try
            {
                var list = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>(parameterName, value)
                };

                return executeStoreProcedure(storeProcedureName, list);
            }
            catch (DBConnectionException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<T> executeStoreProcedure(string storeProcedureName, List<KeyValuePair<String, object>> parameters = null)
        {
            try
            {
                List<SqlParameter> parms = new List<SqlParameter>();

                string listadoParametro = string.Empty;

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (KeyValuePair<String, object> item in parameters)
                    {
                        string nombreParametro = "@" + item.Key;
                        parms.Add(new SqlParameter(nombreParametro, item.Value));

                        listadoParametro += nombreParametro + ',';
                    }

                    listadoParametro = listadoParametro.Substring(0, listadoParametro.Length - 1);
                }

                listadoParametro = string.Format("exec {0} {1}", storeProcedureName, listadoParametro);

                return _dbSet.FromSqlRaw(listadoParametro, parms.ToArray()).ToList();
            }
            catch (DBConnectionException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void execStoreProcedure(string storeProcedureName, string parameterName, object value)
        {
            try
            {
                var list = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>(parameterName, value)
                };

                execStoreProcedure(storeProcedureName, list);
            }
            catch (DBConnectionException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void execStoreProcedure(string storeProcedureName, List<KeyValuePair<String, object>> parameters = null)
        {
            try
            {
                List<SqlParameter> parms = new List<SqlParameter>();

                string listadoParametro = string.Empty;

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (KeyValuePair<String, object> item in parameters)
                    {
                        string nombreParametro = "@" + item.Key;
                        parms.Add(new SqlParameter(nombreParametro, item.Value));

                        listadoParametro += nombreParametro + ',';
                    }

                    listadoParametro = listadoParametro.Substring(0, listadoParametro.Length - 1);
                }

                listadoParametro = string.Format("exec {0} {1}", storeProcedureName, listadoParametro);

                _db.Database.ExecuteSqlRaw(listadoParametro, parms.ToArray());
            }
            catch (DBConnectionException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public class DBConnectionException : Exception {
            public DBConnectionException()
            { }

            public DBConnectionException(string message)
                : base(message)
            { }

            public DBConnectionException(string message, Exception innerException)
                : base(message, innerException)
            { }
        }
    }
}
