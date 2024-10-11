namespace Mongo.GenericClient.Tests.Services.Delete
{
    using System.Collections.Generic;
    using Mongo.GenericClient.Tests.Base;
    using Mongo.GenericClient.Tests.Setup.TestData;
    using MongoDB.Driver;

    public class DeleteCollectionScenario : ScenarioBase<PersonEntity>
    {
        private List<PersonEntity> personEntities;
        
        public ArgumentException? ExpectedExcepion { get; set; }

        public DeleteCollectionScenario()
            : base()
        {
            this.personEntities = new List<PersonEntity>();
        }

        public async Task CreatePersonCollectionAsync(int number)
        {
            this.personEntities = DataFactory.BuildRandomPersonsList(number);
            await this.Collection.InsertManyAsync(this.personEntities);
        }

        public async Task RunDeleteAsyncMethodAsync(string asType)
        {
            switch (asType)
            {
                case "ObjectId":
                    await this.WriteService.DeleteAsync(this.personEntities.Last().Id);
                    break;

                case "string":
                    await this.WriteService.DeleteAsync(this.personEntities.Last().Id.ToString());
                    break;

                default:
                    throw new ArgumentException($"Unknown type {asType}");
            }
        }

        public void CheckCollectionCount(int expectedCount)
        {
            Assert.That(
                this.Collection.CountDocuments(Builders<PersonEntity>.Filter.Empty),
                Is.EqualTo(expectedCount)
            );
        }
    }
}