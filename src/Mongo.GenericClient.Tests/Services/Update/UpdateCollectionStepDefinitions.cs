namespace Mongo.GenericClient.Tests.Services.Update
{
    using TechTalk.SpecFlow;

    [Binding]
    [Scope(Feature = "Update a Collection - Service")]
    public partial class UpdateCollectionStepDefinitions
    {
        [Given("a WriteService of PersonEntity")]
        public void GivenAWriteServiceOfPersonEntity()
        {
            // The service already exists.
            // We just make sure that the database is empty.
            this.ClearDatabase();
        }

        [Given(@"a collection of 1 persons exist")]
        public async Task GivenAPersonsCollectionAsync()
        {
            await this.CreatePersonCollectionAsync();
        }

        [Given(@"the PersonEntity.(.*) is modified to (.*)")]
        public void GivenThePersonEntityIsModified(string field, string value)
        {
            this.ModifyPersonEntity(field, value);
        }

        [When("calling the (.*) method of the WriteService")]
        public async Task WhenMethodIsCalledAsync(string method)
        {
            await this.RunMethodAsync(method);
        }

        [Then("person.(.*) in the collection equals (.*)")]
        public async Task ThenTheCollectionEntityIsUpdatedAsync(string field, string value)
        {
            await this.CheckTheEntityIsUpdatedAsync(field, value);
        }
    }
}