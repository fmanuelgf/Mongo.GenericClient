namespace Mongo.Generics.Tests.Services.Create
{
    using Mongo.Generics.Tests.Base;
    using TechTalk.SpecFlow;

    [Binding]
    [Scope(Feature = "Create a Collection - Service")]
    public class CreateCollectionSteps
    {
        private readonly CreateCollectionScenario scenario;

        public CreateCollectionSteps(CreateCollectionScenario scenario)
            : base()
        {
            this.scenario = scenario;
        }

        [Given("a WriteService of PersonEntity")]
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

        [When("calling the (.*) method of the WriteService")]
        public async Task WhenRepositoryMethodIsCalledAsync(string method)
        {
            await this.scenario.RunMethodAsync(method);
        }

        [Then("the collection of Persons is created")]
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