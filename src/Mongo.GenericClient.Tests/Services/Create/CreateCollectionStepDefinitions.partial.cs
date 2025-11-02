namespace Mongo.GenericClient.Tests.Services.Create
{
    using Mongo.GenericClient.Tests.Base;
    using Mongo.GenericClient.Tests.Setup.TestData;
    using MongoDB.Driver;

    public partial class CreateCollectionStepDefinitions : StepDefinitions<PersonEntity>
    {
        private List<PersonEntity> personEntities;

        public CreateCollectionStepDefinitions()
            : base()
        {
            this.personEntities = [];
        }

        private void CreatePersonEntities(int number)
        {
            this.personEntities = DataFactory.BuildRandomPersonsList(number);
        }

        private async Task RunMethodAsync(string method)
        {
            switch (method)
            {
                case "CreateAsync":
                    await this.WriteService.CreateAsync(this.personEntities.First());
                    break;

                default:
                    throw new ArgumentException($"Unknown method {method}");
            }
        }

        private void CheckCollectionIsCreated()
        {
            Assert.That(
                this.ReadService.CountDocuments(),
                Is.GreaterThan(0)
            );
        }

        private void CheckCollectionCount(int expectedCount)
        {
            Assert.That(
                this.ReadService.CountDocuments(),
                Is.EqualTo(expectedCount)
            );
        }
    }
}