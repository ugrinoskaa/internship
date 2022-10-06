using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserRoles.Models
{
    public class Role
    {
        [JsonProperty(PropertyName = "id")]
        public virtual int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public virtual string Name { get; set; }
    }
}