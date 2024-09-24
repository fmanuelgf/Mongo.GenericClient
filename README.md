# Mongo.Generics

Generic library to manage a [MongoDB](https://www.mongodb.com) database.

Source code: [https://github.com/fmanuelgf/Mongo.Generics](https://github.com/fmanuelgf/Mongo.Generics)

##  Required environment variables

- `"MONGODB_CONNECTION_STRING"` (e.g., "mongodb://root:root@localhost:27017")

- `"MONGODB_DATABASE_NAME"` (e.g., "my_mongodb")


## Interfaces

Reposirory

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

Services

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

IEntity

```C#
namespace Mongo.Generics.Core.Entities
{
    using MongoDB.Bson;

    public interface IEntity
    {
        ObjectId Id { get; set; }
    }
}
```

## Usage

Defining an entity and its collection name:

- The class must implement `IEntity` and have the attribute `CollectionName`
```C#
namespace Mongo.Generics.Tests.SetUp
{
    using Mongo.Generics.Core.Attributes;
    using Mongo.Generics.Core.Entities;
    using MongoDB.Bson;

    [CollectionName("persons")]
    public class PersonEntity : IEntity
    {
        public ObjectId Id { get ; set;  }

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }
    }
}
```

Creating a collection (or just inserting data)

- Using the repository
```C#
await this.repository.Collection.InsertOneAsync(entity);
```

- Using the service
```C#
await this.writeService.CreateAsync(entity);
```

## Note

- In order to run the tests, you must first run `docker compose up -d`.

- You can then manage the database at [http://localhost:8081](http://localhost:8081), with:
    - User: user
    - Password: pwd