using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserRoles.Models
{
    public class NHibertnateSession
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            var configurationPath = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\hibernate.cfg.xml");
            configuration.Configure(configurationPath);
            
            var userConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\User.hbm.xml");
            configuration.AddFile(userConfigurationFile);

            var roleConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\Role.hbm.xml");
            configuration.AddFile(roleConfigurationFile);

            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}