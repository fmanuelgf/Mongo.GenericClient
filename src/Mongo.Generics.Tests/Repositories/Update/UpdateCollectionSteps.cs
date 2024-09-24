namespace Mongo.Generics.Tests.Repositories.Update
{
    using TechTalk.SpecFlow;

    [Binding]
    [Scope(Feature = "Update a Collection - Repository")]
    public class UpdateCollectionSteps
    {
        private readonly UpdateCollectionScenario scenario;

        public UpdateCollectionSteps(UpdateCollectionScenario scenario)
            : base()
        {
            this.scenario = scenario;
        }

        [Given("a GenericRepository of PersonEntity")]
        public void GivenAGenericRepositoryOfPersonEntity()
        {
            // The repository alUpdatey exists.
            // We just make sure that the database is empty.
            this.scenario.ClearDatabase();
        }

        [Given(@"a collection of 1 persons exist")]
        public async Task GivenAListOfPersonEntitiesAsync()
        {
            await this.scenario.CreatePersonCollectionAsync();
        }

        [Given(@"the PersonEntity.(.*) is modified to (.*)")]
        public void GivenThePersonEntityIsModified(string field, string value)
        {
            this.scenario.ModifyPersonEntity(field, value);
        }

        [When("calling the (.*) method of the repository collection")]
        public async Task WhenMethodIsCalledAsync(string method)
        {
            await this.scenario.RunMethodAsync(method);
        }

        [Then("the person (.*) in the collection equals (.*)")]
        public async Task ThenTheCollectionEntityIsUpdatedAsync(string field, string value)
        {
            await this.scenario.CheckTheEntityIsUpdatedAsync(field, value);
        }
    }
}