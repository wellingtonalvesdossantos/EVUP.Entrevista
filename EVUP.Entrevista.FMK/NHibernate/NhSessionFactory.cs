using NHibernate;
using NHibernate.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVUP.Entrevista.FMK
{
    public class NhSessionFactory
    {
        #region Singleton

        /// <summary>
        /// Implementação do pattern Singleton
        /// </summary>

        private static NhSessionFactory instance = null;
        private static readonly object padlock = new object();

        public static NhSessionFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new NhSessionFactory();
                        }

                    }
                }
                return instance;
            }
        }

        #endregion

        private ISessionFactory sessionFactory;

        public ISession GetCurrentSession()
        {
            ISessionFactory factory = GetSessionFactory();

            if (!CurrentSessionContext.HasBind(factory))
            {
                ISession session = NewSession(factory);

                CurrentSessionContext.Bind(session);

                return session;
            }
            else
            {
                return factory.GetCurrentSession();
            }
        }

        public void CloseCurrentSession()
        {
            ISessionFactory factory = GetSessionFactory();

            if (factory == null) return;

            if (CurrentSessionContext.HasBind(factory))
            {
                factory.GetCurrentSession().Clear();
            }
        }

        ISessionFactory GetSessionFactory()
        {
            if (sessionFactory == null)
            {
                lock (padlock)
                {
                    if (sessionFactory == null)
                    {
                        sessionFactory = NhConfiguration.Instance.BuildSessionFactory();
                    }
                }
            }
            
            return sessionFactory;
        }

        ISession NewSession(ISessionFactory factory = null)
        {
            if (factory == null) factory = GetSessionFactory();

            ISession session = factory.OpenSession();

            session.FlushMode = FlushMode.Commit;

            return session;
        }
    }
}
