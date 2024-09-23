using Mongo.Generics.Tests.Base;
using TechTalk.SpecFlow;

namespace Mongo.Generics.Tests.Services.Read
{
    [Binding]
    [Scope(Feature = "Read a Collection")]
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
            // The repository already exists.
            // We just make sure that the dabase is empty.
            this.scenario.ClearDatabase();
        }

        [Given(@"a collection of (.*) PersonEntities exist\(s\)")]
        public async Task GivenAListOfPersonEntities(int number)
        {
            await this.scenario.CreatePersonCollectionAsync(number);
        }

        [When(@"calling the (GetPaginatedAsync)\(pageNumer: (.*), pageSize: (.*)\) method of the ReadService")]
        public async Task WhenRepositoryMethodIsCalledAsync(string method, int pageNum, int pageSize)
        {
            await this.scenario.RunMethodAsync(method, pageNum, pageSize);
        }

        [Then("a PaginationResult of PersonEntities is returned")]
        public void ThenThePageResultIsReturned()
        {
            this.scenario.CheckPageResultIsReturned();
        }

        [Then("the PaginationResult.(.*) is (.*)")]
        public void ThenPaginationResultFieldEquals(string field, int number)
        {
            this.scenario.CheckPaginationResultField(field, number);
        }
    }
}