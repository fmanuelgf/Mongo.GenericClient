namespace Mongo.Generics.Tests.Services.Read
{
    using Mongo.Generics.Models;
    using Mongo.Generics.Tests.Base;
    using Mongo.Generics.Tests.SetUp;
    using MongoDB.Driver;

    public class ReadCollectionScenario : ScenarioBase<PersonEntity>
    {
        private PaginationResult<PersonEntity>? pagination;

        public ReadCollectionScenario()
            : base()
        {
        }

        public async Task CreatePersonCollectionAsync(int number)
        {
            var entities = DataFactory.BuildRandomPersonsList(number);
            await this.Repository.Collection.InsertManyAsync(entities);
        }

        public async Task RunMethodAsync(string method, int pageNum, int pageSize)
        {
            switch (method)
            {
                case "GetPaginatedAsync":
                    this.pagination = await this.ReadService.GetPaginatedAsync(pageNum, pageSize);
                    break;

                default:
                    throw new ArgumentException($"Unknown method {method}");
            }
        }

        public void CheckPageResultIsReturned()
        {
            Assert.That(this.pagination?.Result, Is.Not.Null);
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