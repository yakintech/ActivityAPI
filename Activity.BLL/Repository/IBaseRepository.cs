using Activity.DAL.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activity.BLL.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        public TEntity Add(TEntity entity);
        public List<TEntity> GetAll(int pageNumber, int pageSize);
        public List<TEntity> GetAll();
        public TEntity GetById(Guid id);
        public void Remove(Guid id);
        public TEntity Update(TEntity entity);
    }
}
