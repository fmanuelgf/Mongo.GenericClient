# Mongo.GenericClient

Generic library to manage a [MongoDB](https://www.mongodb.com) database.

##  Required environment variables

- `"MONGODB_CONNECTION_STRING"` (e.g., "mongodb://root:root@localhost:27017")

- `"MONGODB_DATABASE_NAME"` (e.g., "my_mongodb")


## Interfaces

Reposirory
```C#
namespace Mongo.GenericClient.Core.Repositories
{
    using Mongo.GenericClient.Core.Entities;
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
namespace Mongo.GenericClient.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Mongo.GenericClient.Core.Entities;
    using Mongo.GenericClient.Models;
    using MongoDB.Bson;

    public interface IReadService<TEntity>
        where TEntity : IEntity
    {
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null);

        IMongoQueryable<TEntity> AsQueryable();

        Task<PaginationResult<TEntity>> GetPaginatedAsync(
            int pageNum,
            int pageSize);
        
        Task<PaginationResult<TEntity>> GetPaginatedAsync(
            Expression<Func<TEntity, bool>> filter,
            int pageNum,
            int pageSize);

        Task<TEntity> GetByIdAsync(ObjectId id);

        Task<TEntity> GetByIdAsync(string id);
    }
}
```

```C#
namespace Mongo.GenericClient.Core.Services
{
    using System.Threading.Tasks;
    using Mongo.GenericClient.Core.Entities;
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
namespace Mongo.GenericClient.Core.Entities
{
    using MongoDB.Bson;

    public interface IEntity
    {
        ObjectId Id { get; set; }
    }
}
```


## Usage

__Defining an entity and its collection name:__

The class must implement `IEntity` and have the attribute `CollectionName`

_Example:_
```C#
namespace Mongo.GenericClient.Tests.SetUp
{
    using Mongo.GenericClient.Core.Attributes;
    using Mongo.GenericClient.Core.Entities;
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

__Creating a collection (or just inserting data)__

Using the repository

_Example:_
```C#
await this.repository.Collection.InsertOneAsync(entity);
```

Using the service

_Example:_
```C#
await this.writeService.CreateAsync(entity);
```

Getting a list of documents as entities

_Example:_
```C#
var allEntities = this.readService.GetAll()
var filteredEntities = this.readService.GetAll(x => x.Age == 30)
````


__Namespace `Mongo.GenericClient.DependencyInjection`__

Some extension methods have been added to facilitate the registration of repositories and services.

- Services and Repository separately:

_Example:_
```C#
services.ResisterGenericRepository<PersonEntity>(RegisterMode.Transient);
services.ResisterGenericServices<PersonEntity>(RegisterMode.Transient);
```

- Services and repository at the same time:

_Example:_
```C#
services.ResisterGenericRepositoryAndServices<PersonEntity>(RegisterMode.Transient);
````


## Note

- In order to run the tests, you must first run `docker compose up -d`.

- You can then manage the database at [http://localhost:8081](http://localhost:8081), with:
    - User: user
    - Password: pwd