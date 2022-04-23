using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsAluraCSV.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        TEntity GetById(Guid id);
        List<TEntity> GetAll();
        void DeleteById(Guid id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
    }
}
