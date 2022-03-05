using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using System.Threading.Tasks;

namespace DapperAPI.DataServices
{
    public class Db2Service<TEntity> : IDisposable, IDb2Service<TEntity> where TEntity : class
    {
        private readonly IConfiguration _config;
        private readonly IDbConnection _dbConnection;

        public Db2Service(IConfiguration config)
        {
            _config = config;
            _dbConnection = new OleDbConnection(_config.GetConnectionString("DefaultConnection"));
        }

        public void Dispose()
        {

        }

        private static string DataDbFormat(PropertyInfo propModel, string dataModel)
        {
            string dataDb = (propModel.PropertyType.Name.ToLower()) switch
            {
                "string" or "int16" or "int32" or "int64" or "bit" => dataModel,
                "decimal" or "float" or "double" => dataModel.Replace(",", "."),
                "datetime" => Convert.ToDateTime(dataModel).ToString("yyyy-MM-dd"),
                _ => throw new Exception($"Invalid data type {propModel.PropertyType.Name}"),
            };
            return dataDb;
        }

        public async Task<TEntity> GetById(int id)
        {
            return _dbConnection.QuerySingle<TEntity>($"SELECT * FROM {typeof(TEntity).Name} WHERE ID = {id}");
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return _dbConnection.Query<TEntity>($"SELECT * FROM {typeof(TEntity).Name}");
        }

        public async Task<TEntity> Insert(TEntity obj)
        {
            var propertiesObj = obj.GetType().GetProperties();

            string fieldsTable = "";
            string valuesInsert = "";
            for (int i = 0; i < propertiesObj.Length; i++)
            {
                var valField = DataDbFormat(propertiesObj[i], propertiesObj[i].GetValue(obj).ToString());

                var key = false;

                foreach (var atrib in propertiesObj[i].CustomAttributes)
                    if (atrib.AttributeType.Name == "KeyAttribute") key = true;

                if (!key)
                {
                    fieldsTable += (fieldsTable != "") ? $", {propertiesObj[i].Name}" : propertiesObj[i].Name;
                    valuesInsert += (valuesInsert != "") ? $", '{valField}'" : $"'{valField}'";
                }
            }

            var insertSql = $"INSERT INTO {typeof(TEntity).Name} ({fieldsTable}) VALUES ({valuesInsert})";

            _dbConnection.Execute(insertSql);
            var objInserted = _dbConnection.QuerySingle<TEntity>($"SELECT * FROM {typeof(TEntity).Name} WHERE ID = IDENTITY_VAL_LOCAL ( )");

            return objInserted;

        }

        public async Task Delete(int id)
        {
            _dbConnection.Execute($"DELETE {typeof(TEntity).Name} WHERE ID = {id}");
        }

        public async Task Update(TEntity obj)
        {
            var propertiesObj = obj.GetType().GetProperties();

            string updateWhere = "";
            string updateSet = "";

            for (int i = 0; i < propertiesObj.Length; i++)
            {
                var valField = DataDbFormat(propertiesObj[i], propertiesObj[i].GetValue(obj).ToString());

                var key = false;

                foreach (var atrib in propertiesObj[i].CustomAttributes)
                    if (atrib.AttributeType.Name == "KeyAttribute") key = true;

                if (key)
                    updateWhere = $"WHERE ID = {valField}";
                else
                    updateSet += (updateSet == "") ? $"SET {propertiesObj[i].Name}='{valField}'" : $", {propertiesObj[i].Name}='{valField}'";
            }

            var updateSql = $"UPDATE {typeof(TEntity).Name} {updateSet} {updateWhere}";

            _dbConnection.Execute(updateSql);

        }
    }
}
