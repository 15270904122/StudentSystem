using StudentSystem.EntityFramework.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.EntityFramework.Core
{
    public abstract class Entity<TPrimaryKey> : ISoftDelete
    {
        public TPrimaryKey Id { get; set; }

        public DateTime CreationTime { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? ModifyTime { get; set; }
    }

}
