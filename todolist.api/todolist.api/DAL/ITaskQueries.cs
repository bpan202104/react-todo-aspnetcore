using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using todolist.api.Model;
using todolist.api.Providers;

namespace todolist.api.Queries
{
    public interface ITaskQueries
    {
        Task<IEnumerable<TodoTask>> ListTasks();
        Task<TodoTask> GetById(Guid id);
        Task Create(TodoTask newTask);
        Task DeleteById(Guid id);
    }

    public class TaskQueriesDapper : ITaskQueries
    {
        private readonly IDbConnectionProvider _provider;
 
        public TaskQueriesDapper(IDbConnectionProvider provider)
        {
            _provider = provider;
        }
        public async Task<IEnumerable<TodoTask>> ListTasks()
        {
            var sql = "Select * From Tasks";
            using (var connection = _provider.GetDbConnection())
            {
                var tasks = await connection.QueryAsync<TodoTask>(sql);
                return tasks;
            }
        }

        public async Task<TodoTask> GetById(Guid id)
        {
            var sql = "Select * From Tasks Where Id = @Id";
            using (var connection = _provider.GetDbConnection())
            {
                var task = await connection.QueryFirstAsync<TodoTask>(sql,  new {Id = id});
                return task;
            }
        }

        public async Task Create(TodoTask newTask)
        {
            using (var connection = _provider.GetDbConnection())
            {
                await connection.InsertAsync(newTask);
            }
        }

        public async Task DeleteById(Guid id)
        {
            using (var connection = _provider.GetDbConnection())
            {
                await connection.DeleteAsync(new TodoTask{Id = id});
            }
        }
    }
}