namespace Mongo.Generics.Tests.SetUp
{
    using MongoDB.Bson;

    public static class DataFactory
    {
        private static readonly Random random= new Random((int)DateTime.Today.Ticks);
        
        public static List<PersonEntity> BuildPersonEntities(int number)
        {
            var result = new List<PersonEntity>();
            while (number-- > 0)
            {
                var entity = new PersonEntity
                {
                    Id = ObjectId.GenerateNewId(),
                    Name = $"Person {number:000}",
                    Age = random.Next(1, 91),
                    CreatedAt = DateTime.UtcNow
                };

                result.Add(entity);
            }

            return result;
        }
    }
}