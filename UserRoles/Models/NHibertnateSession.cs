using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.Web;

namespace UserRoles.Models
{
    public class NHibertnateSession
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();
                return _sessionFactory;
            }
        }

        public static ISession XMLNHibernateOpenSession()
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

        private static void InitializeSessionFactory()
        {
            _sessionFactory = Fluently.Configure()
                .Database(
                    MySQLConfiguration.Standard
                        .ConnectionString(
                            cs => cs.Server("localhost").Database("userroles").Username("root").Password("password")
                        ).ShowSql()
                )
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<User>()
                    .AddFromAssemblyOf<Role>()
                    .AddFromAssemblyOf<Task>())
                .ExposeConfiguration(config => {
                    new SchemaUpdate(config).Execute(true, true);
                    })
                .BuildSessionFactory();
        }

        public static ISession FluentNHibernateOpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static ISession OpenSession()
        {
            return FluentNHibernateOpenSession();
        }
    }
}