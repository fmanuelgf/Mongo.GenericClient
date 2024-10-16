# Mongo.GenericClient

## Release 8.1.8

- Add the `RegisterAllGenericServices` extension method.

## Release 8.1.7

- Add `IMongoContext` to manage the collections.
- Add the `RegisterMongoContext` extension method.
- Mark `IGenericRepository` as `Obsolete`.
- Mark the `RegisterGenericRepository` extension method as `Obsolete`.
- Mark the `RegisterGenericRepositoryAndServices` extension method as `Obsolete`.

## Release 8.1.6

- Add static class `MongoHelper`, providing access to `MongoClient` and `IMongoDatabase`.
- Minor fixes.

## Release 8.1.5

- Add method `DeleteAsync(string id)`.

## Release 8.1.4

- Add namespace Mongo.GenericClient.DependencyInjection.

## Release 8.1.3

- Add filter parameter to `GetAllAsync` & `GetPaginated` methods.
- `GetByIdAsync` method: Id as `ObjectId` or as `string`.
