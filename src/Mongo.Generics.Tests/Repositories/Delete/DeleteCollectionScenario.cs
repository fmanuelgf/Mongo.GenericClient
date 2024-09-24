namespace Mongo.Generics.Tests.Repositories.Delete
{
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
            var filter = Builders<PersonEntity>.Filter.Eq(x => x.Id, this.personEntities.Last().Id);
            switch (method)
            {
                case "DeleteOneAsync":
                    await this.Repository.Collection.DeleteOneAsync(filter);
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