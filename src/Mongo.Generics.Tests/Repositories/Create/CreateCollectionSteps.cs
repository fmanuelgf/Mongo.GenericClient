namespace Mongo.Generics.Tests.Repositories.Create
{
    using TechTalk.SpecFlow;

    [Binding]
    [Scope(Feature = "Create a Collection - Repository")]
    public class CreateCollectionSteps 
    {
        private readonly CreateCollectionScenario scenario;

        public CreateCollectionSteps(CreateCollectionScenario scenario)
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

        [Given("(1) PersonEntity")]
        [Given("a list of (.*) PersonEntities")]
        public void GivenAListOfPersonEntities(int number)
        {
            this.scenario.CreatePersonEntities(number);
        }

        [When("calling the (.*) method of the repository collection")]
        public async Task WhenMethodIsCalledAsync(string method)
        {
            await this.scenario.RunMethodAsync(method);
        }

        [Then("the collection of persons is created")]
        public void ThenTheCollectionOfPersonssIsCreated()
        {
            this.scenario.CheckCollectionIsCreated();
        }

        [Then("the collection count equals (.*)")]
        public void ThenTheCollectionCountEquals(int number)
        {
            this.scenario.CheckCollectionCount(number);
        }
    }
}