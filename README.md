# Mongo.GenericClient

Generic library to manage a [MongoDB](https://www.mongodb.com) database.

## Required environment variables

- `"MONGODB_CONNECTION_STRING"` (e.g., "mongodb://root:root@localhost:27017")

- `"MONGODB_DATABASE_NAME"` (e.g., "my_mongodb")

## Interfaces

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
        
        Task DeleteAsync(string id);
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

IMongoContext

```C#
namespace Mongo.GenericClient.Core
{
    using Mongo.GenericClient.Core.Entities;
    using MongoDB.Driver;

    public interface IMongoContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>()
            where TEntity : IEntity;
    }
}
````

## MongoHelper

>Note: Auxiliary helper not required for general use.

```C#
namespace Mongo.GenericClient
{
    using MongoDB.Driver;

    public static class MongoHelper
    {
        public static MongoClient Client => new MongoClient(AppConfig.ConnectionString);
        
        public static IMongoDatabase Database => Client.GetDatabase(AppConfig.DatabaseName);
    }
}
```

## Usage

First, register `IMongoContext` and the services for all the entities to be used.

```C#
using Mongo.GenericClient.DependencyInjection;
...

// IMongoContext
services.RegisterMongoContext(RegisterMode.Singleton);

// Services
services.RegisterGenericServices<PersonEntity>(RegisterMode.Transient);

...
```

Defining an entity and its collection name

> The class must implement `IEntity` and have the attribute `CollectionName`

```C#
namespace Mongo.GenericClient.Tests.Setup
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

Creating a collection (or just inserting data)

> Using IMongoContext

```C#
await this.mongoContext.GetCollection<PersonEntity>().InsertOneAsync(entity);
```

> Using the service

```C#
await this.writeService.CreateAsync(entity);
```

Getting a list of documents as entities

```C#
var allEntities = this.readService.GetAll()
var filteredEntities = this.readService.GetAll(x => x.Age == 30)
````

## Note

- In order to run the tests, you must first run `docker compose up -d`.

- You can then manage the database at [http://localhost:8081](http://localhost:8081), with:
  - User: user
  - Password: pwd
