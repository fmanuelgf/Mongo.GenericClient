namespace Mongo.Generics.Tests.Repositories
{
    using Mongo.Generics.Core.Repositories;
    using Mongo.Generics.Repositories;
    using Mongo.Generics.Tests.Base;
    using Mongo.Generics.Tests.SetUp;
    using MongoDB.Driver;

    public class GenericRepositoryTests : TestBase
    {
        private readonly IGenericRepository<PersonEntity> personsRepository;

        public GenericRepositoryTests()
        {
            TestConfig.Configure();
            
            this.personsRepository = new GenericRepository<PersonEntity>();
        }

        [Test]
        public async Task CanCreateACollectionAsync()
        {
            // Arrange
            var entities = DataFactory.BuildPersonEntities(100);
            
            // Act
            await this.personsRepository.Collection.InsertManyAsync(entities);

            // Assert
            Assert.That(
                this.personsRepository.Collection.CountDocuments(Builders<PersonEntity>.Filter.Empty),
                Is.EqualTo(100)
            );
        }
    }
}