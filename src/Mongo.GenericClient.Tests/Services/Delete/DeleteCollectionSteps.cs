namespace Mongo.GenericClient.Tests.Services.Delete
{
    using TechTalk.SpecFlow;

    [Binding]
    [Scope(Feature = "Delete from a Collection - Service")]
    public class DeleteCollectionSteps
    {
        private readonly DeleteCollectionScenario scenario;

        public DeleteCollectionSteps(DeleteCollectionScenario scenario)
        {
            this.scenario = scenario;
        }

        [Given("a WriteService of PersonEntity")]
        public void GivenAWriteServiceOfPersonEntity()
        {
            // The service already exists.
            // We just make sure that the database is empty.
            this.scenario.ClearDatabase();
        }

        [Given("a collection of (.*) persons exist")]
        public async Task GivenAPersonsCollectionAsync(int number)
        {
            await this.scenario.CreatePersonCollectionAsync(number);
        }

        [Given("1 person has the Id xyz")]
        public void GivenAPersonsWithId()
        {
            // do nothing
        }

        [When("calling the DeleteAsync xyz as (.*) method of the WriteService")]
        public async Task WhenMethodIsCalledAsync(string asType)
        {
            await this.scenario.RunDeleteAsyncMethodAsync(asType);
        }

        [Then("the count of the collection of persons equals (.*)")]
        public void ThenTheCollectionCountEquals(int number)
        {
            this.scenario.CheckCollectionCount(number);
        }
    }
}