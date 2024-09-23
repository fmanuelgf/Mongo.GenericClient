namespace Mongo.Generics.Tests.Repositories.Create
{
    using Mongo.Generics.Core.Repositories;
    using Mongo.Generics.Repositories;
    using Mongo.Generics.Tests.Base;
    using Mongo.Generics.Tests.SetUp;
    using MongoDB.Driver;

    public class CreateCollectionScenario : ScenarioBase
    {
        private readonly IGenericRepository<PersonEntity> personsRepository;
        private List<PersonEntity> personEntities;

        public CreateCollectionScenario()
            : base()
        {
            TestConfig.Configure();
            this.personsRepository = new GenericRepository<PersonEntity>();
            this.personEntities = new List<PersonEntity>();
        }

        public void CreatePersonEntities(int number)
        {
            this.personEntities = DataFactory.BuildPersonEntities(number);
        }

        public async Task RunMethodAsync(string method)
        {
            switch (method)
            {
                case "InsertOneAsync":
                    await this.personsRepository.Collection.InsertOneAsync(this.personEntities.First());
                    break;

                case "InsertManyAsync":
                    await this.personsRepository.Collection.InsertManyAsync(this.personEntities);
                    break;

                default:
                    throw new ArgumentException($"Unknown method {method}");
            }
        }

        public void CheckCollectionIsCreated()
        {
            Assert.That(
                this.personsRepository.Collection.CountDocuments(Builders<PersonEntity>.Filter.Empty),
                Is.GreaterThan(0)
            );
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