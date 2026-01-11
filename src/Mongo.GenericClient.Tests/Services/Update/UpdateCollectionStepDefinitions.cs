namespace Mongo.GenericClient.Tests.Services.Update
{
    using TechTalk.SpecFlow;

    [Binding]
    [Scope(Feature = "Update a Collection - Service")]
    public partial class UpdateCollectionStepDefinitions
    {
        [Given("a WriteService of PersonEntity")]
        public void GivenAWriteServiceOfPersonEntity()
        {
            // The service already exists.
            // We just make sure that the database is empty.
            this.ClearDatabase();
        }

        [Given(@"a collection of 1 person, with name (.*) and age (.*) exist")]
        public async Task GivenAPersonsCollectionAsync(string name, int age)
        {
            await this.CreatePersonCollectionAsync(name, age);
        }

        [Given(@"the PersonEntity.(.*) is modified to (.*)")]
        public void GivenThePersonEntityIsModified(string field, string value)
        {
            this.ModifyPersonEntity(field, value);
        }

        [Given(@"using the field-value pair (.*) with value (.*)")]
        public void GivenUsingTheFieldValuePair(string field, string value)
        {
            this.UseFieldValuePair(field, value);
        }

        [Given(@"using the person's ID as (.*)")]
        public void GivenUsingThePersonIdAs(string personIdType)
        {
            this.SetPersonIdType(personIdType);
        }

        [When("calling the (.*) method of the (WriteService|WriteService using the person's ID and the field-value pairs)")]
        public async Task WhenMethodIsCalledAsync(string method, string serviceOption)
        {
            var isUsingFieldValuePairs = serviceOption.Contains("using the person's ID and the field-value pairs");
            await this.RunMethodAsync(method, isUsingFieldValuePairs);
        }

        [Then("person.(.*) in the collection equals (.*)")]
        public async Task ThenTheCollectionEntityIsUpdatedAsync(string field, string value)
        {
            await this.CheckTheEntityIsUpdatedAsync(field, value);
        }
    }
}