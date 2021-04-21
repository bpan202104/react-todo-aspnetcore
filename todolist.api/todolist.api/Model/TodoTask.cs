using System;
using Dapper.Contrib.Extensions;

namespace todolist.api.Model
{
    
    [Table ("Tasks")]
    public class TodoTask
    {
        public string Name { get; set; }
        [ExplicitKey]
        public Guid Id { get; set; }

        public Guid? TaskListId { get; set; }
    }
}