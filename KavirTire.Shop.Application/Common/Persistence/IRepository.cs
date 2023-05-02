using Ardalis.Specification;
using KavirTire.Shop.Domain.Common.Interfaces;

namespace KavirTire.Shop.Application.Common.Persistence;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{

}