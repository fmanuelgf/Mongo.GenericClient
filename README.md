# Mongo.GenericClient

Generic library to manage a [MongoDB](https://www.mongodb.com) database.

## Required environment variables

- `"MONGODB_CONNECTION_STRING"` (e.g., "mongodb://root:root@localhost:27017")

- `"MONGODB_DATABASE_NAME"` (e.g., "my_mongodb")

## Interfaces

Services

```csharp
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

        Task<PaginationResult<TEntity>> GetPaginatedAsync(
            int pageNum,
            int pageSize);
        
        Task<PaginationResult<TEntity>> GetPaginatedAsync(
            Expression<Func<TEntity, bool>> filter,
            int pageNum,
            int pageSize);

        Task<TEntity> GetByIdAsync(ObjectId id);

        Task<TEntity> GetByIdAsync(string id);

        IQueryable<TEntity> AsQueryable();

        long CountDocuments(Expression<Func<TEntity, bool>>? filter = null);
    }
}
```

```csharp
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

        Task<bool> UpdateAsync(ObjectId id, Dictionary<string, object> data);

        Task<bool> UpdateAsync(string id, Dictionary<string, object> data);

        Task DeleteAsync(ObjectId id);
        
        Task DeleteAsync(string id);

        Task DeleteAsync(ObjectId[] ids);
        
        Task DeleteAsync(IList<ObjectId> ids);
        
        Task DeleteAsync(string[] ids);
        
        Task DeleteAsync(IList<string> ids);
    }
}
```

IEntity

```csharp
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

```csharp
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

Auxiliary helper that provides access to the MongoClient and the Database directly, **if needed**.

```csharp
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

```csharp
using Mongo.GenericClient.DependencyInjection;

// IMongoContext, using the default settings (see `Required environment variables`)
services.RegisterMongoContext(RegisterMode.Singleton);

// IMongoContext, using more configuration settings
var settings = AppConfig.DefaultMongoClientSettings;
settings.ReadConcern = ReadConcern.Majority;
settings.WriteConcern = WriteConcern.WMajority;

services.RegisterMongoContext(RegisterMode.Singleton, settings);

// Services (specifying the entities)
services.RegisterGenericServices<ExampleEntity>(RegisterMode.Singleton);
services.RegisterGenericServices<AnotherEntity>(RegisterMode.Scoped);

// Services (for all entities at the same time)
services.RegisterAllGenericServices(RegisterMode.Scoped);

```

Defining an entity and its collection name

**Note:** The class must implement `IEntity` and have the attribute `CollectionName`.

```csharp
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

```csharp
// Using IMongoContext
await this.mongoContext.GetCollection<PersonEntity>().InsertOneAsync(entity);

// Using the service
await this.writeService.CreateAsync(entity);
```

Getting a list of documents as entities

```csharp
var allEntities = this.readService.GetAll();
var filteredEntities = this.readService.GetAll(x => x.Age == 30);
````

Updating an entity

```csharp
// Given a `person` entity, we can:

// Apply changes to the entity and then call `UpdateAsync`...
this.writeService.UpdateAsync(person);

// ... Or simply call `UpdateAsync` with the ID of the entity to be updated and the field-value pairs to be updated.
var data = new Dictionary<string, object>
{
    ["Age"] = 30
}
this.writeService.UpdateAsync(personId, data);

````

## Note

- In order to run the tests, you must first run `docker compose up -d`.

- You can then manage the database at [http://localhost:8081](http://localhost:8081), with:
  - User: user
  - Password: pwd
