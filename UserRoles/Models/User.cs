using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
}