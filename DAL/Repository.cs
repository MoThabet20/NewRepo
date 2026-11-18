using DAL;
using Domains;
using Utilities;

namespace DAL
{
    public class Repository<T> :IRepository<T> where T : BaseEntity
    {
        //const string fileName = "data.json";
        IConverter<T> _serializer;

        public Repository()
        {
            
        }
        public Repository(IConverter<T> jsonSerializer)
        {
            _serializer = jsonSerializer;
        }

        public async Task<List<T>> GetAll()
        {
            try
            {
                JsonConverter<T> FFConvert = new JsonConverter<T>();
                var items = FFConvert.ConvertBack(await FileHelper.ReadAll(GetFileName()));
                return items;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An unexpected error occurred while reading data from file", ex, "list is null");
            }

        }

        public async Task<T> GetById(int id)
        {
            try
            {
                JsonConverter<T> FFConvert = new JsonConverter<T>();
                var items = FFConvert.ConvertBack(await FileHelper.ReadAll(GetFileName()));
                var item = items.Where(a => a.Id == id).FirstOrDefault();
                if (item == null)
                    throw new DataAccessException("item is null");
                else
                    return item;
            }

            catch (Exception ex)
            {
                throw new DataAccessException($"An unexpected error occurred while reading{id} of item", ex, "item is now null");
            }

        }

        public async Task AddItem(T oItem)
        {
            try
            {
                var newList = await GetAll();
                newList.Add(oItem);

                var content = _serializer.Convert(newList);
                //call Create() method to add the list with new obj content to the json file
                await FileHelper.Create(GetFileName(), content);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An unexpected error occurred while adding items in file", ex, "list is null");
            }
        }

        public async Task<List<T>> ShowItems()
        {
            try
            {
                var items = await GetAll();
                return items;
            }
            catch (DataAccessException ex)
            {
                throw new DataAccessException("Error on getting data");
            }
        }

        public async Task Update(T model)
        {
            try
            {
                var items = await GetAll();
                var existedItem = items.Where(a => a.Id == model.Id).FirstOrDefault();
                if (existedItem != null)
                    items.Remove(existedItem);

                items.Add(model);
                //covert to string
                string itemsString = _serializer.Convert(items);
                //write all text
               await FileHelper.Create(GetFileName(), itemsString);
            }
            catch (DataAccessException ex)
            {
                throw new DataAccessException("Error on updating data...");
            }

        }

        public async Task Delete(int id)
        {
            try
            {
                var items = await GetAll();
                var deletedItem = items.FirstOrDefault(a => a.Id == id);
                items.Remove(deletedItem);
                //covert to string
                string itemsString = _serializer.Convert(items);
                //write all text
                FileHelper.Update(GetFileName(), itemsString);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Failed to delete {ex.Message}");
            }
        }

        private string GetFileName()
        {
            var fileName = typeof(T).Name.ToLower() + "s.json";
            return fileName;
        }

        public bool Validate (T model)
        {
            throw new NotImplementedException();
        }
    }
}
