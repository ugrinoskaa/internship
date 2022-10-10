using FluentNHibernate.Mapping;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace UserRoles.Models
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public virtual int Id { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public virtual string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public virtual string LastName { get; set; }

        [JsonProperty(PropertyName = "roles")]
        public virtual IList<Role> Roles { get; set; }
    }

    class UserMap: ClassMap<User>
    {
        public UserMap()
        {
            Table("users");
            Id(user => user.Id).Column("id").GeneratedBy.Native();
            Map(user => user.FirstName).Column("first_name");
            Map(user => user.LastName).Column("last_name");
            HasManyToMany(user => user.Roles)
                .Table("users_roles")
                .ParentKeyColumn("user_id")
                .ChildKeyColumn("role_id")
                .Not.LazyLoad()
                .Cascade.All();
        }
    }
}