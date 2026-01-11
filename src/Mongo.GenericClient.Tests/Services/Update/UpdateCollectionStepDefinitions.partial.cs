namespace Mongo.GenericClient.Tests.Services.Update
{
    using Mongo.GenericClient.Tests.Base;
    using Mongo.GenericClient.Tests.Setup.TestData;

    public partial class UpdateCollectionStepDefinitions : StepDefinitions<PersonEntity>
    {
        private readonly PersonEntity personToUpdate;
        private readonly Dictionary<string, object> updateFields;
        private string personIdAsString;

        public UpdateCollectionStepDefinitions()
            : base()
        {
            this.personToUpdate = DataFactory.BuildRandomPerson("John");
            this.updateFields = [];
            this.personIdAsString = string.Empty;

            var a = new KeyValuePair<string, object>("Age", 30);
        }

        private async Task CreatePersonCollectionAsync(string name, int age)
        {
            this.personToUpdate.Name = name;
            this.personToUpdate.Age = age;
            await this.Collection.InsertOneAsync(this.personToUpdate);
        }

        private void SetPersonIdType(string personIdType)
        {
            switch (personIdType)
            {
                case "ObjectId":
                    // No action needed, as the ID is already an ObjectId.
                    break;

                case "string":
                    // Convert the ID to string for later use.
                    this.personIdAsString = this.personToUpdate.Id.ToString();
                    break;

                default:
                    throw new ArgumentException($"Unknown person ID type {personIdType}");
            }
        }

        private void ModifyPersonEntity(string field, string value)
        {
            switch (field)
            {
                case "Name":
                    this.personToUpdate.Name = value;
                    break;

                case "Age":
                    this.personToUpdate.Age = int.Parse(value);
                    break;

                default:
                    throw new ArgumentException($"Unknown field {field}");
            }
        }

        private void UseFieldValuePair(string field, string value)
        {
            this.updateFields[field] = value;
        }

        private async Task RunMethodAsync(string method, bool isUsingFieldValuePairs)
        {
            switch (method)
            {
                case "UpdateAsync":
                    if (isUsingFieldValuePairs)
                    {
                        _ = this.personIdAsString == string.Empty
                            ? await this.WriteService.UpdateAsync(this.personToUpdate.Id, this.updateFields)
                            : await this.WriteService.UpdateAsync(this.personIdAsString, this.updateFields);
                    }
                    else
                    {
                        await this.WriteService.UpdateAsync(this.personToUpdate);
                    }
                    break;

                default:
                    throw new ArgumentException($"Unknown method {method}");
            }
        }

        private async Task CheckTheEntityIsUpdatedAsync(string field, string value)
        {
            var entity = await this.ReadService.GetByIdAsync(this.personToUpdate.Id);
            switch (field)
            {
                case "Name":
                    Assert.That(entity.Name, Is.EqualTo(value));
                    break;

                case "Age":
                    Assert.That(entity.Age, Is.EqualTo(int.Parse(value)));
                    break;

                default:
                    throw new ArgumentException($"Unknown field {field}");
            }
        }
    }
}