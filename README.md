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
namespace Mongo.Generics.Repositories
{
    using System;
    using Mongo.Generics.Core.Attributes;
    using Mongo.Generics.Core.Entities;
    using MongoDB.Driver;

    public class GenericRepository<TEntity>
        where TEntity : AuditableEntity, IEntity
    {
        public GenericRepository()
        {
            var client = new MongoClient(AppConfig.ConnectionString);
            var database = client.GetDatabase(AppConfig.DatabaseName);

            var collectionName =
                Attribute.GetCustomAttribute(
                    typeof(TEntity),
                    typeof(CollectionNameAttribute)) as CollectionNameAttribute;

            this.Collection = database.GetCollection<TEntity>(collectionName?.Name);
        }

        public IMongoCollection<TEntity> Collection { get; private set; }
    }
}
```

### Services

```C#
namespace Eudora.Domain.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Eudora.Domain.Core.Entities;
    using Eudora.Domain.Core.Models;
    using MongoDB.Bson;

    public interface IReadService<TEntity>
        where TEntity : AuditableEntity, IEntity
    {
        IEnumerable<TEntity> GetAll(bool includeDeleted = false);

        Task<PaginationResult<TEntity>> GetPaginatedAsync(
            int pageNum,
            int pageSize,
            string? search = null);

        Task<TEntity> GetByIdAsync(ObjectId id);
    }
}

```

```C#
namespace Eudora.Domain.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Eudora.Domain.Core.Entities;

    public interface IWriteService<TEntity>
        where TEntity : AuditableEntity, IEntity
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task CreateAsync(IEnumerable<TEntity> entities);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(TEntity entity);
    }
}
```


<hr>

## Usage

// TODO

<hr>

## Note

- In order to run the tests, you must first run `docker compose up -d`.

- You can then manage the database at [http://localhost:8081](http://localhost:8081), with:
    - User: user
    - Password: pwd