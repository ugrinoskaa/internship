using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using UserRoles.Models;

namespace UserRoles.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RolesController : ApiController
    {
        public static readonly List<Role> roles = new List<Role>()
        {
            new Role(){Id=1, Name="HR"},
            new Role(){Id=2, Name="Manager"}
        };

        // GET api/roles
        public IList<Role> GetRoles()
        {
            return roles;
        }

        // POST api/roles
        public IHttpActionResult PostRole([FromBody] Role role)
        {
            role.Id = roles.Count + 1;
            roles.Add(role);

            return StatusCode(HttpStatusCode.Created);
        }


    }
}
