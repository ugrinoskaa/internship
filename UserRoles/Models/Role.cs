using Newtonsoft.Json;
using FluentNHibernate.Mapping;

namespace UserRoles.Models
{
    public class Role
    {
        [JsonProperty(PropertyName = "id")]
        public virtual int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public virtual string Name { get; set; }
    }

    class RoleMap: ClassMap<Role>
    {
        public RoleMap()
        {
            Id(role => role.Id).Column("id").GeneratedBy.Native();
            Map(role => role.Name).Column("name");
            Table("roles");
        }
    }
}