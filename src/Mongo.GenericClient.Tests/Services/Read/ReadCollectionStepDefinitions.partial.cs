namespace Mongo.GenericClient.Tests.Services.Read
{
    using Mongo.GenericClient.Models;
    using Mongo.GenericClient.Tests.Base;
    using Mongo.GenericClient.Tests.Setup.TestData;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;

    public partial class ReadCollectionStepDefinitions : StepDefinitions<PersonEntity>
    {
        private PaginationResult<PersonEntity>? pagination;
        private IList<PersonEntity> personEntities;
        private long count;
        private ObjectId objectId;
        private ArgumentException? expectedException;

        public ReadCollectionStepDefinitions()
            : base()
        {
            this.personEntities = new List<PersonEntity>();
        }

        private async Task CreatePersonCollectionAsync(int number)
        {
            var entities = DataFactory.BuildRandomPersonsList(number);
            await this.Collection.InsertManyAsync(entities);
        }

        private async Task UpdatePersonsName(int number, string name)
        {
            var entities = this.Collection
                .AsQueryable()
                .Take(number)
                .ToList();

            if (entities.Count == 1)
            {
                this.objectId = entities.First().Id;
            }

            foreach (var entity in entities)
            {
                var filter = Builders<PersonEntity>.Filter.Eq(x => x.Id, entity.Id);
                entity.Name = name;
                await this.Collection.ReplaceOneAsync(filter, entity);
            }
        }

        private async Task RunMethod(string method, string? filter = null)
        {
            switch (method)
            {
                case "AsQueryable":
                    this.count = this.ReadService.AsQueryable().Count();
                    break;
                
                case "CountDocuments":
                    this.count = filter == "name is John"
                        ? this.ReadService.CountDocuments(x => x.Name == "John")
                        : this.ReadService.CountDocuments();
                    break;

                case "GetAll":
                    this.personEntities = filter == "name is John"
                        ? this.ReadService.GetAll(x => x.Name == "John")
                        : this.ReadService.GetAll();
                    break;

                case "GetByIdAsync":
                    var person = filter == "as string"
                        ? await this.ReadService.GetByIdAsync(this.objectId.ToString())
                        : await this.ReadService.GetByIdAsync(this.objectId);
                    this.personEntities.Add(person);
                    break;

                case "GetByIdAsync foo":
                    this.expectedException = Assert.ThrowsAsync<ArgumentException>(async () =>
                        await this.ReadService.GetByIdAsync("foo")
                    );
                    break;

                default:
                    throw new ArgumentException($"Unknown method {method}");
            }
        }

        private async Task RunGetPaginatedAsyncAsync(int pageNum, int pageSize, string? filter = null)
        {
            this.pagination = filter == "name is John"
                ? await this.ReadService.GetPaginatedAsync(x => x.Name == "John", pageNum, pageSize)
                : await this.ReadService.GetPaginatedAsync(pageNum, pageSize);
        }

        private void CheckPageResultIsReturned()
        {
            Assert.That(this.pagination?.Result, Is.Not.Null);
        }

        private void CheckCount(int expectedCount)
        {
            Assert.That(this.count, Is.EqualTo(expectedCount));
        }

        private void CheckPersonEntitiesCount(int expectedCount)
        {
            Assert.That(this.personEntities.Count(), Is.EqualTo(expectedCount));
        }

        private void CheckPersonEntitiesName(int expectedCount, string name)
        {
            Assert.That(this.personEntities.Count(x => x.Name == name), Is.EqualTo(expectedCount));
        }

        private void CheckPaginationResultField(string field, int expectedValue)
        {
            switch(field)
            {
                case "Page":
                    Assert.That(this.pagination?.Page, Is.EqualTo(expectedValue));
                    break;

                case "PageSize":
                    Assert.That(this.pagination?.PageSize, Is.EqualTo(expectedValue));
                    break;

                case "TotalItems":
                    Assert.That(this.pagination?.TotalItems, Is.EqualTo(expectedValue));
                    break;

                case "Result.Count":
                    Assert.That(this.pagination?.Result?.Count(), Is.EqualTo(expectedValue));
                    break;
                
                default:
                    throw new ArgumentException($"Unknown field {field}");
            }
        }
    }
}