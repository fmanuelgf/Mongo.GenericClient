namespace Mongo.Generics.Tests.Services.Read
{
    using TechTalk.SpecFlow;

    [Binding]
    [Scope(Feature = "Read a Collection - Service")]
    public class ReadCollectionSteps
    {
        private readonly ReadCollectionScenario scenario;

        public ReadCollectionSteps(ReadCollectionScenario scenario)
            : base()
        {
            this.scenario = scenario;
        }

        [Given("a ReadService of PersonEntity")]
        public void GivenAReadServiceOfPersonEntity()
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

        [When(@"calling the (GetPaginatedAsync)\(pageNumer: (.*), pageSize: (.*)\) method of the ReadService")]
        public async Task WhenMethodIsCalledAsync(string method, int pageNum, int pageSize)
        {
            await this.scenario.RunMethodAsync(method, pageNum, pageSize);
        }

        [Then("a PaginationResult of PersonEntities is returned")]
        public void ThenThePageResultIsReturned()
        {
            this.scenario.CheckPageResultIsReturned();
        }

        [Then("the PaginationResult.(.*) equals (.*)")]
        public void ThenPaginationResultFieldEquals(string field, int number)
        {
            this.scenario.CheckPaginationResultField(field, number);
        }
    }
}