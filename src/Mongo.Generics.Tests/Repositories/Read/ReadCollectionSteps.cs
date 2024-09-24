namespace Mongo.Generics.Tests.Repositories.Read
{
    using TechTalk.SpecFlow;

    [Binding]
    [Scope(Feature = "Read a Collection - Repository")]
    public class ReadCollectionSteps
    {
        private readonly ReadCollectionScenario scenario;

        public ReadCollectionSteps(ReadCollectionScenario scenario)
            : base()
        {
            this.scenario = scenario;
        }

        [Given("a GenericRepository of PersonEntity")]
        public void GivenAGenericRepositoryOfPersonEntity()
        {
            // The repository already exists.
            // We just make sure that the database is empty.
            this.scenario.ClearDatabase();
        }

        [Given(@"a collection of (.*) persons exist")]
        public async Task GivenAListOfPersonEntitiesAsync(int number)
        {
            await this.scenario.CreatePersonCollectionAsync(number);
        }

        [When("calling the (.*) method of the repository collection")]
        public async Task WhenMethodIsCalledAsync(string method)
        {
            await this.scenario.RunMethodAsync(method);
        }

        [Then("a list of (.*) PersonEntities is returned")]
        public void ThenTheCollectionCountEquals(int number)
        {
            this.scenario.CheckCollectionCount(number);
        }
    }
}