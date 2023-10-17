

namespace Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    void Commit();
    void Rollback();
}

