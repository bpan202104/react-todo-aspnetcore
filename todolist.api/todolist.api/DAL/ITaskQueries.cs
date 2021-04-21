using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using todolist.api.Model;

namespace todolist.api.DAL
{
    public interface ITaskQueries
    {
        Task<IEnumerable<TodoTask>> ListTasks();
        Task<TodoTask> GetTaskById(Guid id);
        Task CreateTask(TodoTask newTask);
        Task DeleteTaskById(Guid id);
        Task CreateTaskList(TodoTaskListHeader taskList);
        Task<TodoTaskList> GetTaskList(Guid id);
        Task<IEnumerable<TodoTaskListHeader>> ListTasksLists();
        Task DeleteTaskListById(Guid id);
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

        public async Task<TodoTask> GetTaskById(Guid id)
        {
            var sql = "Select * From Tasks Where Id = @Id";
            using (var connection = _provider.GetDbConnection())
            {
                var task = await connection.QueryFirstAsync<TodoTask>(sql,  new {Id = id});
                return task;
            }
        }

        public async Task CreateTask(TodoTask newTask)
        {
            using (var connection = _provider.GetDbConnection())
            {
                await connection.InsertAsync(newTask);
            }
        }

        public async Task DeleteTaskById(Guid id)
        {
            using (var connection = _provider.GetDbConnection())
            {
                await connection.DeleteAsync(new TodoTask{Id = id});
            }
        }

        public async Task CreateTaskList(TodoTaskListHeader newTaskList)
        {
            using (var connection = _provider.GetDbConnection())
            {
                await connection.InsertAsync(newTaskList);
            }
        }

        public async Task<TodoTaskList> GetTaskList(Guid id)
        {
            var sql = "Select * From TaskLists RIGHT JOIN Tasks ON Tasks.TaskListId = TaskLists.Id Where TaskLists.Id = @Id";
            using (var connection = _provider.GetDbConnection())
            {
                var taskList =await connection.QueryAsync<TodoTaskList, TodoTask, TodoTaskList>(sql, (list, task) =>
                    {
                        if (list.Tasks == null)
                            list.Tasks = new List<TodoTask>();
                        list.Tasks.Add(task);
                        return list;
                },  
                    new {Id = id});
                return taskList.First();
            }
        }

        public async Task<IEnumerable<TodoTaskListHeader>> ListTasksLists()
        {
            var sql = "Select * From TaskLists";
            using (var connection = _provider.GetDbConnection())
            {
                var tasksLists = await connection.QueryAsync<TodoTaskListHeader>(sql);
                return tasksLists;
            }
        }

        public async Task DeleteTaskListById(Guid id)
        {
            using (var connection = _provider.GetDbConnection())
            {
                //TODO: Make transactional
                var deleteTasksSql = "DELETE From Tasks Where TaskListId = @Id";
                await connection.ExecuteAsync(deleteTasksSql, new {Id = id});
                await connection.DeleteAsync(new TodoTaskListHeader{Id = id});
            }
        }
    }
}