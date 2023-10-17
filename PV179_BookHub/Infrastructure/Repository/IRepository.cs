namespace Infrastructure.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    void Delete(TEntity entity);
    TEntity? GetById(int id);
    IEnumerable<TEntity> GetAll();
    void SaveChanges();
}
