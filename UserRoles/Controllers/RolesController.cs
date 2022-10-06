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
        // GET api/roles
        public IList<Role> GetRoles()
        {
            using (ISession session = NHibertnateSession.OpenSession())
            {
                var roles = session.Query<Role>().ToList();
                return roles;
            }
        }

        // POST api/roles
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


    }
}
