namespace MCC75NET.Repositories.Interface;

interface IRepository<Key, Entity> where Entity: class
{
    // GET
    List<Entity> GetAll();
    // GetByID
    Entity GetById(Key key);
    // Create
    int Insert(Entity entity);
    // Update
    int Update(Entity entity);
    // Delete
    int Delete(Key key);
}
