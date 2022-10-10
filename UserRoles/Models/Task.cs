using FluentNHibernate.Mapping;
using System;

namespace UserRoles.Models
{
    public class Task
    {
        public virtual int Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual User Reporter { get; set; } 

        public virtual User Assigned { get; set; }  

        public virtual DateTime CreatedDate { get; set; }

        public virtual DateTime UpdatedDate { get; set; }

        public virtual TaskStatus Status { get; set; }

        public enum TaskStatus 
        {
            OPEN,
            IN_PROGRESS,
            DONE
        }
    }

    class TaskMap: ClassMap<Task>
    {
        public TaskMap()
        {
            Table("tasks");
            Id(task => task.Id).Column("id").GeneratedBy.Native();
            Map(task => task.Title).Column("title").Not.Nullable();
            Map(task => task.Description).Column("description").Not.Nullable();
            Map(task => task.CreatedDate).Column("created_date").Not.Nullable().Default("CURRENT_TIMESTAMP");
            Map(task => task.UpdatedDate).Column("updated_date").Not.Nullable().Default("CURRENT_TIMESTAMP");
            Map(task => task.Status).Column("status").Not.Nullable().CustomType<GenericEnumMapper<Task.TaskStatus>>();
            References(task => task.Reporter).Column("reporter_id");
            References(task => task.Assigned).Column("assigned_id");
        }
    }
}