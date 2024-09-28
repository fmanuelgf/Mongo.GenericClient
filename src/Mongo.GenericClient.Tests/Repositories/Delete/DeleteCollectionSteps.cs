namespace Mongo.GenericClient.Tests.Repositories.Delete
{
    using TechTalk.SpecFlow;

    [Binding]
    [Scope(Feature = "Delete from a Collection - Repository")]
    public class DeleteCollectionSteps
    {
        private readonly DeleteCollectionScenario scenario;

        public DeleteCollectionSteps(DeleteCollectionScenario scenario)
        {
            this.scenario = scenario;
        }

        [Given("a GenericRepository of PersonEntity")]
        public void GivenAGenericRepositoryOfPersonEntity()
        {
            // The repository alDeletey exists.
            // We just make sure that the database is empty.
            this.scenario.ClearDatabase();
        }

        [Given(@"a collection of (.*) persons exist")]
        public async Task GivenAPersonsCollectionAsync(int number)
        {
            await this.scenario.CreatePersonCollectionAsync(number);
        }

        [When("calling the (.*) method of the repository collection")]
        public async Task WhenMethodIsCalledAsync(string method)
        {
            await this.scenario.RunMethodAsync(method);
        }

        [Then("the count of the collection of persons equals (.*)")]
        public void ThenTheCollectionCountEquals(int number)
        {
            this.scenario.CheckCollectionCount(number);
        }
    }
}