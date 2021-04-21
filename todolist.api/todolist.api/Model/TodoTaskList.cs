using System;
using System.Collections;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace todolist.api.Model
{
    [Table ("TaskLists")]
    public class TodoTaskList
    {
        public string Name { get; set; }
        [ExplicitKey]
        public Guid Id { get; set; }

        public List<TodoTask> Tasks { get; set; }
    }
    
    [Table ("TaskLists")]
    public class TodoTaskListHeader
    {
        public string Name { get; set; }
        [ExplicitKey]
        public Guid Id { get; set; }
    }    
}