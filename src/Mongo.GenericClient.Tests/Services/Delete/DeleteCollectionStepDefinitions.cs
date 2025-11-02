namespace Mongo.GenericClient.Tests.Services.Delete
{
    using TechTalk.SpecFlow;

    [Binding]
    [Scope(Feature = "Delete from a Collection - Service")]
    public partial class DeleteCollectionStepDefinitions
    {
        [Given("a WriteService of PersonEntity")]
        public void GivenAWriteServiceOfPersonEntity()
        {
            // The service already exists.
            // We just make sure that the database is empty.
            this.ClearDatabase();
        }

        [Given("a collection of (.*) persons exist")]
        public async Task GivenAPersonsCollectionAsync(int number)
        {
            await this.CreatePersonCollectionAsync(number);
        }

        [Given("1 person has the Id xyz")]
        public void GivenAPersonsWithId()
        {
            // do nothing
        }

        [When("calling the DeleteAsync xyz as (.*) method of the WriteService")]
        public async Task WhenMethodIsCalledAsync(string asType)
        {
            await this.RunDeleteAsyncMethodAsync(asType);
        }

        [When("calling the DeleteAsync (.*) of (.*) IDs as (ObjectId|string) method of the WriteService")]
        public async Task WhenMethodIsCalledAsync(string colType, int number, string asType)
        {
            await this.RunDeleteAsyncMethodAsync(colType, asType, number);
        }

        [When("calling the DeleteAsync (.*) of (.*) IDs as string method of the WriteService with an invalid ID")]
        public void WhenMethodIsCalledWithInvalidId(string colType, int number)
        {
            this.RunDeleteAsyncMethodWithInvalidId(colType, number);
        }

        [When("calling the DeleteAsync foo method of the WriteService")]
        public void WhenMethodIsCalledWithInvalidIdOrName()
        {
            this.expectedException = Assert.ThrowsAsync<ArgumentException>(async () =>
                await this.WriteService.DeleteAsync("foo")
            );
        }

        [Then("the count of the collection of persons equals (.*)")]
        public void ThenTheCollectionCountEquals(int number)
        {
            this.CheckCollectionCount(number);
        }

        [Then("an Exception is thrown")]
        public void ThenTheExpectedErrorIsThrown()
        {
            Assert.That(this.expectedException, Is.Not.Null);
        }

        [Then("the Exception message is 'foo is not a valid ObjectId'")]
        public void ThenTheExpectedErrorMessageIs()
        {
            Assert.That(
                this.expectedException?.Message,
                Is.EqualTo("'foo' is not a valid ObjectId")
            );
        }
    }
}