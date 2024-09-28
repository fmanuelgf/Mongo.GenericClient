namespace Mongo.GenericClient.Tests.Services.Create
{
    using TechTalk.SpecFlow;

    [Binding]
    [Scope(Feature = "Create a Collection - Service")]
    public class CreateCollectionSteps
    {
        private readonly CreateCollectionScenario scenario;

        public CreateCollectionSteps(CreateCollectionScenario scenario)
        {
            this.scenario = scenario;
        }

        [Given("a WriteService of PersonEntity")]
        public void GivenAGenericRepositoryOfPersonEntity()
        {
            // The service already exists.
            // We just make sure that the database is empty.
            this.scenario.ClearDatabase();
        }

        [Given("(1) PersonEntity")]
        [Given("a list of (.*) PersonEntities")]
        public void GivenAListOfPersonEntities(int number)
        {
            this.scenario.CreatePersonEntities(number);
        }

        [When("calling the (.*) method of the WriteService")]
        public async Task WhenMethodIsCalledAsync(string method)
        {
            await this.scenario.RunMethodAsync(method);
        }

        [Then("the collection of persons is created")]
        public void ThenTheCollectionOfPersonssIsCreated()
        {
            this.scenario.CheckCollectionIsCreated();
        }

        [Then("the count of the collection of persons equals (.*)")]
        public void ThenTheCollectionCountEquals(int number)
        {
            this.scenario.CheckCollectionCount(number);
        }
    }
}