using System;
using System.Collections.Generic;

namespace todolist.api.Model
{
    public class TodoTaskList
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public List<TodoTask> Tasks { get; set; }
    }
}