public class InMemoryRepository<T> : IRepository<T> where T : class
{
    protected readonly Dictionary<Guid, T> _store = new();

    public IEnumerable<T> GetAll() => _store.Values;
    public T? GetById(Guid id) => _store.TryGetValue(id, out var entity) ? entity : null;
    public void Add(T entity)
    {
        var idProp = typeof(T).GetProperty("Id");
        if (idProp is null) throw new InvalidOperationException("Entity must have Id property");
        var id = (Guid)idProp.GetValue(entity)!;
        _store[id] = entity;
    }
    public void Update(T entity) => Add(entity);
    public void Delete(Guid id) => _store.Remove(id);
}