using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;

namespace EVUP.Entrevista.FMK
{
    /// <summary>
    /// Repositório padrão para conexão ao banco de dados via NHibernate
    /// </summary>
    /// <typeparam name="T">Tipo da entidade a ser controlada pela classe</typeparam>
    internal class NhRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        public NhRepository()
        {
        }
        
        private ITransaction _transaction;

        public T GetById(long id)
        {
            return NhSessionFactory.Instance.GetCurrentSession().Get<T>(id);
        }

        public T LoadById(long id)
        {
            return NhSessionFactory.Instance.GetCurrentSession().Load<T>(id);
        }

        public long Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (!NhSessionFactory.Instance.GetCurrentSession().Transaction.IsActive)
                throw new Exception("Nenhuma transação ativa");

            object retObj = NhSessionFactory.Instance.GetCurrentSession().Save(entity);
            
            if (retObj is long)
            {
                return (long)retObj;
            }
            else
            {
                return 0;
            }
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (!NhSessionFactory.Instance.GetCurrentSession().Transaction.IsActive)
                throw new Exception("Nenhuma transação ativa");
            
             NhSessionFactory.Instance.GetCurrentSession().Update(entity);
        }

        public void Delete(long id)
        {
            if (!NhSessionFactory.Instance.GetCurrentSession().Transaction.IsActive)
                throw new Exception("Nenhuma transação ativa");

            T entity = NhSessionFactory.Instance.GetCurrentSession().Load<T>(id);

            NhSessionFactory.Instance.GetCurrentSession().Delete(entity);
            
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (!NhSessionFactory.Instance.GetCurrentSession().Transaction.IsActive)
                throw new Exception("Nenhuma transação ativa");
            
            NhSessionFactory.Instance.GetCurrentSession().Delete(entity);
        }
        
        public IQueryable<T> Table
        {
            get
            {
               return NhSessionFactory.Instance.GetCurrentSession().Query<T>(); 
            }
        }
        
        public object Refresh(object entity)
        {
            try
            {
                
                if (entity is BaseEntity)
                {
                    var id = (entity as BaseEntity).Id;

                    if (id != 0)
                    {
                        entity = NhSessionFactory.Instance.GetCurrentSession().Get(entity.GetType(), id);
                    }
                }
                else
                {
                    NhSessionFactory.Instance.GetCurrentSession().Refresh(entity);
                }
            }
            catch (HibernateException)
            {
                //workarround para não causar exception se tentar dar um refresh de um objeto desatachado
                try
                {
                    NhSessionFactory.Instance.GetCurrentSession().Refresh(entity);
                }
                catch (HibernateException)
                {

                }
            }

            return entity;
        }

        public IDisposable BeginTransaction()
        {
            if (NhSessionFactory.Instance.GetCurrentSession().Transaction.IsActive) return null;

            _transaction = NhSessionFactory.Instance.GetCurrentSession().BeginTransaction();

            return _transaction;
        }
        
        public void Commit()
        {
            _transaction.Commit();

            _transaction.Dispose();

            _transaction = null;
        }

        public void RollBack()
        {
            _transaction.Dispose();

            _transaction = null;
        }
        
        public IQuery CreateQuery(string hql)
        {
            return NhSessionFactory.Instance.GetCurrentSession().CreateQuery(hql);
        }

        public ISQLQuery CreateSQLQuery(string sql)
        {
            return NhSessionFactory.Instance.GetCurrentSession().CreateSQLQuery(sql);
        }

        public void Flush()
        {
            NhSessionFactory.Instance.GetCurrentSession().Flush();
        }

        public void Clear()
        {
            NhSessionFactory.Instance.GetCurrentSession().Clear();
        }

        public void Evict(object entity)
        {
            NhSessionFactory.Instance.GetCurrentSession().Evict(entity);
        }

        public bool IsInitialized(object o)
        {
            if (o == null) return false;

            return NHibernateUtil.IsInitialized(o);
        }
    }
}
