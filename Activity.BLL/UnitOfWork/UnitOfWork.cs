using Activity.BLL.Repository;
using Activity.DAL.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activity.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private ActivityDbContext db = new ActivityDbContext();

        private GenericRepository<User> userRepository;
        private GenericRepository<Category> categoryRepository;

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new GenericRepository<User>();
                }
                return userRepository;
            }
        }

        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new GenericRepository<Category>();
                }
                return categoryRepository;
            }
        }


        public void Save()
        {
            db.SaveChanges();
        }


    }
}
