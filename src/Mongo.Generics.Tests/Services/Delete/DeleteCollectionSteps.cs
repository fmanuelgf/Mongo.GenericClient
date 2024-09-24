namespace Mongo.Generics.Tests.Services.Delete
{
    using TechTalk.SpecFlow;

    [Binding]
    [Scope(Feature = "Delete a Collection - Service")]
    public class DeleteCollectionSteps
    {
        private readonly DeleteCollectionScenario scenario;

        public DeleteCollectionSteps(DeleteCollectionScenario scenario)
            : base()
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

        [Given(@"a collection of (.*) persons exist")]
        public async Task GivenAListOfPersonEntitiesAsync(int number)
        {
            await this.scenario.CreatePersonCollectionAsync(number);
        }

        [When("calling the (.*) method of the WriteService")]
        public async Task WhenMethodIsCalledAsync(string method)
        {
            await this.scenario.RunMethodAsync(method);
        }

        [Then("the collection of persons count equals (.*)")]
        public void ThenTheCollectionCountEquals(int number)
        {
            this.scenario.CheckCollectionCount(number);
        }
    }
}