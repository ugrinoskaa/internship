using Microsoft.Ajax.Utilities;
using NHibernate;
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
    public class UsersController : ApiController
    {
        // GET api/users
        public IList<User> GetUsers()
        {
            using (ISession session = NHibertnateSession.OpenSession())
            {
                var users = session.Query<User>().ToList();
                return users;
            }
        }

        // POST api/users
        public IHttpActionResult PostUser([FromBody] User user)
        {
            using (ISession session = NHibertnateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(user);
                    transaction.Commit();
                }
            }

            return StatusCode(HttpStatusCode.Created);
        }

        // PUT api/users/{user_id}/roles/{role_id}
        [Route("api/users/{user_id}/roles/{role_id}")]
        public IHttpActionResult PutUserRoles(int user_id, int role_id)
        {
            using (ISession session = NHibertnateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var user = session.Get<User>(user_id);
                    var role = session.Get<Role>(role_id);

                    user.Roles.Add(role);
                    user.Roles = user.Roles.DistinctBy(u => u.Id).ToList();
                    
                    session.Save(user);
                    transaction.Commit();
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

    }
}
