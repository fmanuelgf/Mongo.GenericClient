# Mongo.Generics

Generic library to manage a [MongoDB](https://www.mongodb.com) database.

[MIT License](LICENSE)

<hr>

##  Required environment variables

- `"MONGODB_CONNECTION_STRING"` (e.g., "mongodb://root:root@localhost:27017")

- `"MONGODB_DATABASE_NAME"` (e.g., "my_blob_storage")

<hr>

## Interfaces

### Reposirory
```C#
namespace Mongo.Generics.Core.Repositories
{
    using Mongo.Generics.Core.Entities;
    using MongoDB.Driver;

    public interface IGenericRepository<TEntity>
        where TEntity : IEntity
    {
        IMongoCollection<TEntity> Collection { get; }
    }
}
```

### Services

```C#
namespace Mongo.Generics.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Mongo.Generics.Core.Entities;
    using Mongo.Generics.Models;
    using MongoDB.Bson;

    public interface IReadService<TEntity>
        where TEntity : IEntity
    {
        IEnumerable<TEntity> GetAll();

        Task<PaginationResult<TEntity>> GetPaginatedAsync(
            int pageNum,
            int pageSize);

        Task<TEntity> GetByIdAsync(ObjectId id);
    }
}
```

```C#
namespace Mongo.Generics.Core.Services
{
    using System.Threading.Tasks;
    using Mongo.Generics.Core.Entities;
    using MongoDB.Bson;

    public interface IWriteService<TEntity>
        where TEntity : IEntity
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        Task DeleteAsync(ObjectId id);
    }
}
```


<hr>

## Note

- In order to run the tests, you must first run `docker compose up -d`.

- You can then manage the database at [http://localhost:8081](http://localhost:8081), with:
    - User: user
    - Password: pwd