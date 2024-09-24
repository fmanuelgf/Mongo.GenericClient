namespace Mongo.Generics.Tests.Services.Update
{
    using Mongo.Generics.Tests.Base;
    using Mongo.Generics.Tests.SetUp;

    public class UpdateCollectionScenario : ScenarioBase<PersonEntity>
    {
        private PersonEntity personToUpdate;

        public UpdateCollectionScenario()
            : base()
        {
            this.personToUpdate = DataFactory.BuildRandomPerson();
        }

        public async Task CreatePersonCollectionAsync()
        {
            await this.Repository.Collection.InsertOneAsync(this.personToUpdate);
        }

        public void ModifyPersonEntity(string field, string value)
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

        public async Task RunMethodAsync(string method)
        {
            switch (method)
            {
                case "UpdateAsync":
                    await this.WriteService.UpdateAsync(this.personToUpdate);
                    break;

                default:
                    throw new ArgumentException($"Unknown method {method}");
            }
        }

        public async Task CheckTheEntityIsUpdatedAsync(string field, string value)
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