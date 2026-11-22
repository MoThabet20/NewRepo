using DAL;
using Microsoft.VisualBasic.FileIO;
using System.Reflection;

namespace BL
{
    public class Business<T> : IBusinessLayer<T> where T : Domains.BaseEntity
    {
        private readonly IRepository<T> _repository;

        public Business(DAL.StorageType storageType)
        {
            switch(storageType)
            {
                case DAL.StorageType.InMemory:
                    //_repository = new Repository<T>(new InMemoryConverter<T>());
                    break;
                case DAL.StorageType.JsonFile:
                    _repository = new Repository<T>(new JsonConverter<T>());
                    break;
                case DAL.StorageType.Database:
                    //_repository = new Repository<T>(new DatabaseConverter<T>());
                    break;
                default:
                    throw new ArgumentException("Invalid storage type.");
            }
            
        }


        public async Task AddItem(T oItem)
        {
            try
            {
                if (!Validate(oItem))
                    throw new ValidationException("");
                //logic for adding an item to the database
                await _repository.AddItem(oItem);
            }
            catch (DataAccessException ex)
            {
                throw new BusinessLayerException("", ex);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await _repository.Delete(id);
            }
            catch (DataAccessException ex)
            {
                throw new BusinessLayerException("An error occurred while deleting the item.", ex);
            }
        }

        public async Task<List<T>> GetAll()
        {
            try
            {
                var items = await _repository.GetAll();
                if (items == null)
                    throw new NotFoundException("");
                return items;
            }
            catch (DataAccessException ex)
            {
                throw new BusinessLayerException("", ex);
            }
        }

        public async Task<T> GetById(int id)
        {
            try
            {
                if (id < 0)
                    throw new ValidationException($"Item {id} is not valid");
                return await _repository.GetById(id);
            }
            catch (DataAccessException ex)
            { throw new BusinessLayerException("", ex); }
        }

        public Task<List<T>> ShowItems()
        {
            return _repository.ShowItems();
        }

        public async Task Update(T model)
        {
            try
            {
                if (!Validate(model))
                    throw new ValidationException("Not Valid..");
                await _repository.Update(model);
            }
            catch (DataAccessException ex)
            {
                throw new BusinessLayerException("", ex);
            }
        }

        public bool Validate(T model)
        {
            IValidator<T> validator = new Validator<T>();
            return validator.Validate(model);
        }
    }
}
