using API.Interfaces;

namespace API.Data.Models;

public class BaseEntity<T> : IAggregateRoot
    where T : struct
{
    public T Id { get; init; }
}
