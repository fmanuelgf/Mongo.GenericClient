namespace Mongo.Generics.Tests.Services.Delete
{
    using System.Collections.Generic;
    using Mongo.Generics.Tests.Base;
    using Mongo.Generics.Tests.SetUp;
    using MongoDB.Driver;

    public class DeleteCollectionScenario : ScenarioBase<PersonEntity>
    {
        private List<PersonEntity> personEntities;

        public DeleteCollectionScenario()
            : base()
        {
            this.personEntities = new List<PersonEntity>();
        }

        public async Task CreatePersonCollectionAsync(int number)
        {
            this.personEntities = DataFactory.BuildRandomPersonsList(number);
            await this.Repository.Collection.InsertManyAsync(this.personEntities);
        }

        public async Task RunMethodAsync(string method)
        {
            switch (method)
            {
                case "DeleteAsync":
                    await this.WriteService.DeleteAsync(this.personEntities.Last().Id);
                    break;

                default:
                    throw new ArgumentException($"Unknown method {method}");
            }
        }

        public void CheckCollectionCount(int expectedCount)
        {
            Assert.That(
                this.Repository.Collection.CountDocuments(Builders<PersonEntity>.Filter.Empty),
                Is.EqualTo(expectedCount)
            );
        }
    }
}