using Microsoft.Ajax.Utilities;
using MySqlX.XDevAPI;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using UserRoles.Models;
using HttpDeleteAttribute = System.Web.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace UserRoles.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/Users
        [HttpGet]
        public IList<User> GetUsers()
        {
            using (ISession session = NHibertnateSession.OpenSession())
            {
                    return session.Query<User>().ToList();
            }
        }

        //GET api/users?name=Aneta
        [HttpGet]
        public IList<User> GetUsers([FromUri] String name)
        {
            using (ISession session = NHibertnateSession.OpenSession())
            {
                return session.Query<User>().Where(x => x.FirstName.StartsWith(name)).ToList();
            }
        }

        //GET api/Users/{id}
        [HttpGet]
        [Route("api/users/{id}")]
        public User GetUserById(int id)
        {
            using (ISession session = NHibertnateSession.OpenSession())
            {
                return session.Get<User>(id);
            }
        }

        // POST api/Users
        [HttpPost]
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

        //PUT api/Users/{id}
        [HttpPut]
        [Route("api/users/{id}")]
        public IHttpActionResult UpdateUser(int id, [FromBody] User u)
        {
            using (ISession session = NHibertnateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var user = session.Get<User>(id);
                    user.FirstName = u.FirstName;
                    user.LastName = u.LastName;

                    session.Update(user);
                    transaction.Commit();
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

        // PUT api/Users/{user_id}/Roles/{role_id}
        [HttpPut]
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

        //DELETE api/Users/{id}
        [HttpDelete]
        [Route("api/users/{id}")]
        public void DeleteUser(int id)
        {
            using (ISession session = NHibertnateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var user = session.Get<User>(id);

                    user.Roles.Clear();

                    session.Save(user);

                    session.Delete(user);
                    transaction.Commit();
                }
            }
        }
    }
}
