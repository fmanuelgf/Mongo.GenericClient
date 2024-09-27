namespace Mongo.GenericClient.Tests.SetUp
{
    using MongoDB.Bson;

    public static class DataFactory
    {
        private static readonly Random random= new Random((int)DateTime.Today.Ticks);
        
        public static PersonEntity BuildRandomPerson()
        {
            return new PersonEntity
            {
                Id = ObjectId.GenerateNewId(),
                Name = $"Person {random.Next(1, 1000):000}",
                Age = random.Next(1, 91)
            };
        }

        public static List<PersonEntity> BuildRandomPersonsList(int number)
        {
            var result = new List<PersonEntity>();
            while (number-- > 0)
            {
                result.Add(BuildRandomPerson());
            }

            return result;
        }
    }
}