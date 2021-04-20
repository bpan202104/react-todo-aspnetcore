using System;
using Dapper.Contrib.Extensions;

namespace todolist.api.Model
{
    [Table ("TaskLists")]
    public class EmptyTodoTaskList
    {
        public string Name { get; set; }
        [ExplicitKey]
        public Guid Id { get; set; }        
    }
}