namespace BaseApp.DataAccess
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
    }
}
