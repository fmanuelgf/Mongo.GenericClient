namespace Mongo.GenericClient.Tests.SetUp
{
    using MongoDB.Bson;

    public static class DataFactory
    {
        private static readonly Random random= new Random((int)DateTime.Today.Ticks);
        
        public static PersonEntity BuildRandomPerson(string name)
        {
            return new PersonEntity
            {
                Id = ObjectId.GenerateNewId(),
                Name = name,
                Age = random.Next(1, 91)
            };
        }

        public static List<PersonEntity> BuildRandomPersonsList(int number, string? name = null)
        {
            var result = new List<PersonEntity>();
            while (number-- > 0)
            {
                result.Add(BuildRandomPerson(name ?? $"Person {number}"));
            }

            return result;
        }
    }
}