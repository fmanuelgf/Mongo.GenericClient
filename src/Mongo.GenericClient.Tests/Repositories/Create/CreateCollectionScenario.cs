namespace Mongo.GenericClient.Tests.Repositories.Create
{
    using Mongo.GenericClient.Tests.Base;
    using Mongo.GenericClient.Tests.SetUp;
    using MongoDB.Driver;

    public class CreateCollectionScenario : ScenarioBase<PersonEntity>
    {
        private List<PersonEntity> personEntities;

        public CreateCollectionScenario()
            : base()
        {
            this.personEntities = new List<PersonEntity>();
        }

        public void CreatePersonEntities(int number)
        {
            this.personEntities = DataFactory.BuildRandomPersonsList(number);
        }

        public async Task RunMethodAsync(string method)
        {
            switch (method)
            {
                case "InsertOneAsync":
                    await this.Repository.Collection.InsertOneAsync(this.personEntities.First());
                    break;

                case "InsertManyAsync":
                    await this.Repository.Collection.InsertManyAsync(this.personEntities);
                    break;

                default:
                    throw new ArgumentException($"Unknown method {method}");
            }
        }

        public void CheckCollectionIsCreated()
        {
            Assert.That(
                this.Repository.Collection.CountDocuments(Builders<PersonEntity>.Filter.Empty),
                Is.GreaterThan(0)
            );
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