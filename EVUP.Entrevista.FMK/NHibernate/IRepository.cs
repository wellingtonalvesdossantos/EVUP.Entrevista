using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace EVUP.Entrevista.FMK
{
    /// <summary>
    /// Classes que implementam essa interface intermedeiam a entidade na aplicação e o banco de dados.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade a ser controlada pela classe</typeparam>
    public interface IRepository<T>
        where T : BaseEntity
    {
        long Insert(T entity);
        void Update(T entity);
        void Delete(long id);
        void Delete(T entity);
        IDisposable BeginTransaction();
        void Commit();
        void RollBack();
        object Refresh(object entity);
        IQuery CreateQuery(string hql);
        ISQLQuery CreateSQLQuery(string sql);
        T GetById(long id);
        T LoadById(long id);
        IQueryable<T> Table { get; }
        void Flush();
        void Clear();
        void Evict(object entity);
        bool IsInitialized(object o);
    }
}
