using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;

namespace IE.MobileStorage.Sqlite
{
    public class Database : IDatabase
    {
        public SQLiteConnection _connection { get; set; }
        public string _databasePath { get; set; }

        public Database(string databasePath, SQLiteConnection connection)
        {
            if (_connection == null)
                _connection = connection;
            if (string.IsNullOrWhiteSpace(_databasePath))
                _databasePath = databasePath;

            _connection.BusyTimeout = new TimeSpan(1, 0, 0);

            //Check if tables exist then create them if not
            //Parent Tables
            //if (_connection.GetTableInfo(nameof(RecentSearches)).Count == 0)
            //    _connection.CreateTable(typeof(RecentSearches));
        }

        public void BeginTransaction() => _connection.BeginTransaction();
        public void CloseDatabase() => _connection.Close();

        public void Commit() => _connection.Commit();
        public void CreateTable<T>() => _connection.CreateTable<T>();

        //Remove Items
        public void Delete(object objectToDelete) => _connection.Delete(objectToDelete);
        public void Delete<T>(Guid id) => _connection.Delete<T>(id);
        public void Delete<T>(IEnumerable<Guid> ids) => _connection.DeleteAll<T>(); //Check this logic ??
        public void DeleteAll<T>() => _connection.DeleteAll<T>();

        public void DropTable<T>() => _connection.DropTable<T>();
        public void Execute(string query, params object[] args) => _connection.CreateCommand(query, args).ExecuteNonQuery();

        IEnumerable<PersistentType> IDatabase.Get<PersistentType>(string queryStatement, params object[] queryParameter) => _connection.CreateCommand(queryStatement, queryParameter).ExecuteQuery<PersistentType>().ToList();
        Task<IEnumerable<PersistentType>> IDatabase.GetAsync<PersistentType>(string query) => _connection.CreateCommand(query).ExecuteScalar<Task<IEnumerable<PersistentType>>>();
        public IEnumerable<PersistentType> GetAll<PersistentType>(string query) where PersistentType : class, new() => _connection.CreateCommand(query).ExecuteQuery<PersistentType>().AsEnumerable();
        public IEnumerable<T> GetItems<T>(Func<T, bool> condition) where T : class, new()
        {

            return null;
        }

        public ScalarType GetScalar<ScalarType>(string query) where ScalarType : new() => _connection.ExecuteScalar<ScalarType>(query);
        public Task<ScalarType> GetScalarAsync<ScalarType>(string query) where ScalarType : new() => _connection.ExecuteScalar<Task<ScalarType>>(query);
        public T GetSingleItem<T>(Func<T, bool> condition) where T : class, new() => _connection.Get<T>(condition);

        //INSERTS
        public void Insert<T>(T objectToInsert) => _connection.Insert(objectToInsert);
        public int InsertItems<T>(IEnumerable<T> items) => _connection.InsertAll(items);
        public void InsertOrUpdate<T>(T obj) => _connection.InsertOrReplace(obj);

        //TRANSACTION MANAGEMENT
        public void Rollback() => _connection.Rollback();
        public void RunInTransaction(Action action) => _connection.RunInTransaction(() => { action.Invoke(); });
        public void SaveChanges() => _connection.Commit();
        public Task SaveChangesAsync() => Task.Run(() => { _connection.Commit(); });

        //UPDATE
        public void Update<T>(T objectToUpdate) where T : class, new() => _connection.Update(objectToUpdate);
        public void Update<T>(IEnumerable<T> objectsToUpdate) where T : class, new() => _connection.UpdateAll(objectsToUpdate);
        public int UpdateItems<T>(IEnumerable<T> models) => _connection.UpdateAll(models);

        IEnumerable<PersistentType> IDatabase.GetAll<PersistentType>()
        {
            return null;
        }

        public IEnumerable<PersistentType> GetAll<PersistentType>() where PersistentType : class, new()
        {
            throw new NotImplementedException();
        }

        public void InsertOrReplace<T>(T objectToInsert) => _connection.InsertOrReplace(objectToInsert);
        public int InsertOrReplaceItems<T>(IEnumerable<T> items) => _connection.InsertOrReplaceAll(items);
    }
}
