using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using NHibernate.Mapping;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Automapping;
using StateInterface.Designer.Repository.Properties;
using NHibernate.Tool.hbm2ddl;

namespace StateInterface.Designer.Repository
{
    public class SessionProvider
    {
        private static ISessionFactory _sessionFactory;
        private static ISession _session;
        private static object locker = new object();

        public static ISessionFactory SessionFactory
        {
            get
            {
                lock (locker)
                {
                    if (_sessionFactory == null)
                    {
                        _sessionFactory = buildSessionFactory();
                    }
                }

                return _sessionFactory;
            }
        }

        private static ISessionFactory buildSessionFactory()
        {
            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                .ConnectionString(Settings.Default.ConnectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<StateInterface.Designer.Repository.Mappings.FieldMap>())
                //.Mappings(m =>
                //    {
                //        m.FluentMappings.Add<StateInterface.Designer.Repository.Mappings.PermissionMap>()
                //            .Add<StateInterface.Designer.Repository.Mappings.UserMap>()
                //            .Add<StateInterface.Designer.Repository.Mappings.RoleMap>()
                //            ;
                //    })
                //.ExposeConfiguration(cfg => { new SchemaExport(cfg).Create(false, true); })
                //.ExportTo(@"C:\temp\ICA NH Mapping"))
                .BuildSessionFactory();
            

            return sessionFactory;
        }

        private SessionProvider()
        { }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static IStatelessSession OpenStatelessSession()
        {
            return SessionFactory.OpenStatelessSession();
        }

        public static ISession GetSession()
        {
            if (_session == null)
            {
                _session = OpenSession();
            }

            return _session;
        }
    }
}
