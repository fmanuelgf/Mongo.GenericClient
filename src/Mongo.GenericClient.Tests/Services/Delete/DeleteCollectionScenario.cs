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

        public async Task RunDeleteAsyncMethodAsync(string colType, string asType, int number)
        {
            switch (asType)
            {
                case "ObjectId":
                    var objIds = this.personEntities.Take(number).Select(x => x.Id);
                    await this.WriteService.DeleteAsync(colType == "an array"
                        ? objIds.ToArray()
                        : objIds.ToList());
                    break;

                case "string":
                    var idStrings = this.personEntities.Take(number).Select(x => x.Id.ToString()).ToArray();
                    await this.WriteService.DeleteAsync(colType == "an array"
                        ? idStrings
                        : idStrings.ToList());
                    break;

                default:
                    throw new ArgumentException($"Unknown type {asType}");
            }
        }

        public void RunDeleteAsyncMethodWithInvalidId(string colType, int number)
        {
            var idStrings = this.personEntities.Take(number).Select(x => x.Id.ToString()).ToArray();
            idStrings[0] = "foo";

            this.ExpectedExcepion = Assert.ThrowsAsync<ArgumentException>(async () =>
                await this.WriteService.DeleteAsync(colType == "an array"
                    ? idStrings
                    : idStrings.ToList())
            );
        }

        public void CheckCollectionCount(int expectedCount)
        {
            Assert.That(
                this.ReadService.CountDocuments(),
                Is.EqualTo(expectedCount)
            );
        }
    }
}