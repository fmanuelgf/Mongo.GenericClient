namespace Mongo.GenericClient.Tests.Services.Read
{
    using TechTalk.SpecFlow;

    [Binding]
    [Scope(Feature = "Read a Collection - Service")]
    public class ReadCollectionSteps
    {
        private readonly ReadCollectionScenario scenario;

        public ReadCollectionSteps(ReadCollectionScenario scenario)
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
        public async Task GivenAPersonsCollectionAsync(int number)
        {
            await this.scenario.CreatePersonCollectionAsync(number);
        }

        [Given(@"(.*) of the persons have the Name '(.*)'")]
        public async Task GivenSomePersonsName(int number, string name)
        {
            await this.scenario.UpdatePersonsName(number, name);
        }

        [Given(@"1 of the persons have the Name '(.*)' and Id xyz")]
        public async Task GivenSomePersonsName(string name)
        {
            await this.scenario.UpdatePersonsName(1, name);
        }

        [When(@"calling the GetPaginatedAsync\(pageNumer: (.*), pageSize: (.*)\) method of the ReadService with (.*) filter")]
        public async Task WhenMethodIsCalledAsync(int pageNum, int pageSize, string filter)
        {
            await this.scenario.RunGetPaginatedAsyncAsync(pageNum, pageSize, filter);
        }

        [When("calling the AsQueryable method of the ReadService")]
        public async Task WhenAsQueryableMethodIsCalledAsync()
        {
            await this.scenario.RunMethod("AsQueryable");
        }

        [When("calling the CountDocuments method of the ReadService with (.*) filter")]
        public async Task WhenCountDocumentsMethodIsCalledAsync(string filter)
        {
            await this.scenario.RunMethod("CountDocuments", filter);
        }

        [When("calling the GetByIdAsync xyz as (.*) method of the ReadService")]
        public async Task WhenGetByIdMethodIsCalledAsync(string asType)
        {
            await this.scenario.RunMethod("GetByIdAsync", asType);
        }

        [When("calling the GetByIdAsync foo method of the ReadService")]
        public async Task WhenGetByIdMethodIsCalledAsync()
        {
            await this.scenario.RunMethod("GetByIdAsync foo");
        }

        [When(@"calling the GetAll method of the ReadService with (.*) filter")]
        public async Task WhenMethodIsCalledAsync(string filter)
        {
            await this.scenario.RunMethod("GetAll", filter);
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

        [Then("Count (.*) equals (.*)")]
        public void ThenCountEquals(string _, int number)
        {
            this.scenario.CheckCount(number);
        }

        [Then("(1) PersonEntity is returned")]
        [Then("(.*) PersonEntities are returned")]
        public void ThenPersonEntitiesAreReturned(int number)
        {
            this.scenario.CheckPersonEntitiesCount(number);
        }

        [Then("(.*) of the PersonEntities have the Name '(.*)'")]
        [Then("(the) PersonEntity has the Name '(.*)'")]
        public void ThenPersonEntitiesNames(string howMany, string name)
        {
            var number = howMany == "the" ? 1 : int.Parse(howMany);
            this.scenario.CheckPersonEntitiesName(number, name);
        }

        [Then("an Exception is thrown")]
        public void ThenTheExpectedErrorIsThrown()
        {
            Assert.That(this.scenario.ExpectedExcepion, Is.Not.Null);
        }

        [Then("the Exception message is 'foo is not a valid ObjectId'")]
        public void ThenTheExpectedErrorMessageIs()
        {
            Assert.That(
                this.scenario.ExpectedExcepion?.Message,
                Is.EqualTo("'foo' is not a valid ObjectId")
            );
        }
    }
}