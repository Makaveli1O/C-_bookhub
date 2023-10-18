namespace Infrastructure.Repository;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task Add(TEntity entity);
    void Delete(TEntity entity);
    void Update(TEntity entity);
    Task<TEntity?> GetById(long id);
    Task<IEnumerable<TEntity>> GetAll();
    void SaveChanges();
}
