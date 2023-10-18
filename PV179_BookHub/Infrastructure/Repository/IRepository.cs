namespace Infrastructure.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    void Delete(TEntity entity);
    void Update(TEntity entity);
    TEntity? GetById(long id);
    IEnumerable<TEntity> GetAll();
    void SaveChanges();
}
