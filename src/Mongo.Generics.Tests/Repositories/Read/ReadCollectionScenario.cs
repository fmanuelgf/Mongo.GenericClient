namespace Mongo.Generics.Tests.Repositories.Read
{
    using Mongo.Generics.Core.Repositories;
    using Mongo.Generics.Repositories;
    using Mongo.Generics.Tests.Base;
    using Mongo.Generics.Tests.SetUp;
    using MongoDB.Driver;

    public class ReadCollectionScenario : ScenarioBase
    {
        private readonly IGenericRepository<PersonEntity> personsRepository;
        private List<PersonEntity> personEntities;

        public ReadCollectionScenario()
            : base()
        {
            TestConfig.Configure();
            this.personsRepository = new GenericRepository<PersonEntity>();
            this.personEntities = new List<PersonEntity>();
        }

        public async Task CreatePersonCollectionAsync(int number)
        {
            var entities = DataFactory.BuildPersonEntities(number);
            await this.personsRepository.Collection.InsertManyAsync(entities);
        }

        public async Task RunMethodAsync(string method)
        {
            switch (method)
            {
                case "Find":
                    this.personEntities = await this.personsRepository.Collection
                        .Find(Builders<PersonEntity>.Filter.Empty)
                        .ToListAsync();
                    break;

                default:
                    throw new ArgumentException($"Unknown method {method}");
            }
        }

        public void CheckCollectionCount(int expectedCount)
        {
            Assert.That(
                this.personsRepository.Collection.CountDocuments(Builders<PersonEntity>.Filter.Empty),
                Is.EqualTo(expectedCount)
            );
        }
    }
}