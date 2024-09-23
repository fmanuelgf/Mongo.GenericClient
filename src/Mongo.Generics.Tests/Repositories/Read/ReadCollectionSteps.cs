namespace Mongo.Generics.Tests.Repositories.Read
{
    using Mongo.Generics.Tests.Base;
    using TechTalk.SpecFlow;

    [Binding]
    [Scope(Feature = "Read a Collection")]
    public class ReadCollectionSteps : ScenarioBase
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
            // We just make sure that the dabase is empty.
            this.scenario.ClearDatabase();
        }

        [Given(@"a collection of (.*) PersonEntities exist\(s\)")]
        public async Task GivenAListOfPersonEntities(int number)
        {
            await this.scenario.CreatePersonCollectionAsync(number);
        }

        [When("calling the (.*) method of the repository collection")]
        public async Task WhenRepositoryMethodIsCalledAsync(string method)
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