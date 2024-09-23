namespace Mongo.Generics.Tests.Repositories.Read
{
    using Mongo.Generics.Tests.Base;
    using Mongo.Generics.Tests.SetUp;
    using MongoDB.Driver;

    public class ReadCollectionScenario : ScenarioBase<PersonEntity>
    {
        private List<PersonEntity> personEntities;

        public ReadCollectionScenario()
            : base()
        {
            this.personEntities = new List<PersonEntity>();
        }

        public async Task CreatePersonCollectionAsync(int number)
        {
            var entities = DataFactory.BuildPersonEntities(number);
            await this.Repository.Collection.InsertManyAsync(entities);
        }

        public async Task RunMethodAsync(string method)
        {
            switch (method)
            {
                case "Find":
                    this.personEntities = await this.Repository.Collection
                        .Find(Builders<PersonEntity>.Filter.Empty)
                        .ToListAsync();
                    break;

                default:
                    throw new ArgumentException($"Unknown method {method}");
            }
        }

        public void CheckCollectionCount(int expectedCount)
        {
            Assert.That(this.personEntities.Count, Is.EqualTo(expectedCount));
        }
    }
}