using NHibernate;
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
    public class RolesController : ApiController
    {
        // GET api/Roles
        [HttpGet]
        public IList<Role> GetRoles()
        {
            using (ISession session = NHibertnateSession.OpenSession())
            {
                var roles = session.Query<Role>().ToList();
                return roles;
            }
        }

        //GET api/Roles/{id}
        [HttpGet]
        [Route("api/roles/{id}")]
        public Role GetRoleById(int id)
        {
            using (ISession session = NHibertnateSession.OpenSession())
            {
                return session.Get<Role>(id);
            }
        }

        //GET api/Roles?name=HR
        [HttpGet]
        public IList<Role> GetRoles([FromUri] String name)
        {
            using (ISession session = NHibertnateSession.OpenSession())
            {
                return session.Query<Role>().Where(x => x.Name.StartsWith(name)).ToList();
            }
        }

        // POST api/Roles
        [HttpPost]
        public IHttpActionResult PostRole([FromBody] Role role)
        {
            using (ISession session = NHibertnateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(role);
                    transaction.Commit();
                }
            }

            return StatusCode(HttpStatusCode.Created);
        }

        //PUT api/roles/{id}
        [HttpPut]
        [Route("api/roles/{id}")]
        public IHttpActionResult UpdateRole(int id, [FromBody] Role r)
        {
            using (ISession session = NHibertnateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var role = session.Get<Role>(id);
                    role.Name = r.Name;

                    session.Update(role);
                    transaction.Commit();
                }
            }
            return StatusCode(HttpStatusCode.OK);
        }

        //DELETE api/roles/{id}
        [HttpDelete]
        [Route("api/roles/{id}")]
        public void DeleteRole(int id)
        {
            using (ISession session = NHibertnateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Role role = session.Get<Role>(id);
                    IList<User> users = session.Query<User>().Where(x => x.Roles.Contains(role)).ToList();
                    foreach(User user in users)
                    {
                        user.Roles.Remove(role);
                        session.Update(user);
                    }

                    session.Delete(role);
                    transaction.Commit();
                }
            }
        }
    }
}
