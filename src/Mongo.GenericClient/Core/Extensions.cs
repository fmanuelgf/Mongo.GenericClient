namespace Mongo.GenericClient.Core
{
    using MongoDB.Bson;

    public static class Extensions
    {
        public static ObjectId ToObjectId(this object id)
        {
            try
            {
                return id is ObjectId oid ? oid : ObjectId.Parse(id.ToString());
            }
            catch
            {
                throw new InvalidCastException("The provided ID is not a valid ObjectId.");
            }
        }
    }
}