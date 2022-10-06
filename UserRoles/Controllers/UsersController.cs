using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using UserRoles.Models;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace UserRoles.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        public static readonly List<User> users = new List<User>()
        {
            new User(){Id=1, FirstName="Joe", LastName="Doe"},
            new User(){Id=2, FirstName="John", LastName="Doe"}
        };

        public static readonly Dictionary<int, HashSet<int>> userroles = new Dictionary<int, HashSet<int>>(){};

        // GET api/users
        public IList<User> GetUsers()
        {
            return users;
        }

        // GET api/users/roles
        [Route("api/users/roles")]
        public IList<User> GetUserRoles()
        {
            foreach (var user in users)
            {
                if (userroles.ContainsKey(user.Id))
                {
                    HashSet<int> rolesIds = userroles[user.Id];
                    user.Roles = RolesController.roles.FindAll(r => rolesIds.Contains(r.Id));
                    
                }
            }
            return users;
        }

        // POST api/users
        public IHttpActionResult PostUser([FromBody] User user)
        {
            user.Id = users.Count + 1;
            users.Add(user);

            return StatusCode(HttpStatusCode.Created);
        }

        // PUT api/users/{user_id}/roles/{role_id}
        [Route("api/users/{user_id}/roles/{role_id}")]
        public IHttpActionResult PutUserRoles(int user_id, int role_id)
        {
            if (userroles.ContainsKey(user_id))
            {
                userroles[user_id].Add(role_id);
            }
            else
            {
                userroles[user_id] = new HashSet<int>();
                userroles[user_id].Add(role_id);
            }

            return StatusCode(HttpStatusCode.OK);
        }

    }
}
