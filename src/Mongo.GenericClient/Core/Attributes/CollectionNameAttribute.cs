namespace Mongo.GenericClient.Core.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public class CollectionNameAttribute : Attribute
    {
        private string name;  
    
        public CollectionNameAttribute(string name)  
        {  
            this.name = name;  
        }

        public string Name => this.name;
    }
}