namespace Mongo.Generics.Tests.Services.Read
{
    using Mongo.Generics.Models;
    using Mongo.Generics.Tests.Base;
    using Mongo.Generics.Tests.SetUp;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;

    public class ReadCollectionScenario : ScenarioBase<PersonEntity>
    {
        private PaginationResult<PersonEntity>? pagination;
        private IMongoQueryable<PersonEntity>? query;

        public ReadCollectionScenario()
            : base()
        {
        }

        public async Task CreatePersonCollectionAsync(int number)
        {
            var entities = DataFactory.BuildRandomPersonsList(number);
            await this.Repository.Collection.InsertManyAsync(entities);
        }

        public void RunMethod(string method)
        {
            switch (method)
            {
                case "AsQueryable":
                    this.query = this.ReadService.AsQueryable();
                    break;

                default:
                    throw new ArgumentException($"Unknown method {method}");
            }
        }

        public async Task RunGetPaginatedAsyncAsync(int pageNum, int pageSize)
        {
            this.pagination = await this.ReadService.GetPaginatedAsync(pageNum, pageSize);
        }

        public void CheckPageResultIsReturned()
        {
            Assert.That(this.pagination?.Result, Is.Not.Null);
        }

        public void CheckCount(int expectedCount)
        {
            Assert.That(this.query?.Count(), Is.EqualTo(expectedCount));
        }

        public void CheckPaginationResultField(string field, int expectedValue)
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