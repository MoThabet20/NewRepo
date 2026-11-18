using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DAL
{
    public interface IRepository<T>
    {
        public Task<List<T>> GetAll();

        public Task<T> GetById(int id);

        public Task AddItem(T oItem);

        public Task<List<T>> ShowItems();

        public Task Update(T model);
        
        public Task Delete(int id);
    
    }
}
