using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsAluraCSV.Domain.Interfaces.Repositories;
using TransactionsAluraCSV.Infra.Data.Contexts;

namespace TransactionsAluraCSV.Infra.Data.Repositories
{
    public class BaseRepository<TEntity>: IBaseRepository<TEntity>
        where TEntity : class
    {

        private readonly PostgreSqlContext _postgreSqlContext;

        public BaseRepository(PostgreSqlContext postgreSqlContext)
        {
            _postgreSqlContext = postgreSqlContext;
        }

        public TEntity GetById(Guid id)
        {
            return _postgreSqlContext.Set<TEntity>().Find(id);
        }

        public List<TEntity> GetAll()
        {
            return _postgreSqlContext.Set<TEntity>().ToList();
        }

        public void DeleteById(Guid id)
        {
            TEntity tEntity = _postgreSqlContext.Set<TEntity>().Find(id);
            _postgreSqlContext.Entry(tEntity).State = EntityState.Deleted;
            _postgreSqlContext.SaveChanges();
        }

        public void Insert(TEntity entity)
        {
            _postgreSqlContext.Entry(entity).State = EntityState.Added;
            _postgreSqlContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _postgreSqlContext.Entry(entity).State = EntityState.Modified;
            _postgreSqlContext.SaveChanges();
        }
    }
}
