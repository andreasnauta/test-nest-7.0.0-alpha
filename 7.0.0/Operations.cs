using Common.Classes;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _7._0._0
{
    public class Operations
    {
        const string index = "test";
        const string elasticConnection = "connection";
        const string elasticUser = "user";
        const string elasticPassword = "password";
        readonly ElasticClient client;

        public Operations()
        {
            var connectionSettings = new ConnectionSettings(new Uri(elasticConnection)).BasicAuthentication(elasticUser, elasticPassword);

            connectionSettings.DefaultIndex(index);

            client = new ElasticClient(connectionSettings);
        }

        public void Insert()
        {
            DeleteAndCreate(client, index);

            var person = new Person
            {
                Description = "Description",
                Name = "Name"
            };

            var linkActor = new LinkActor
            {
                Actor = person
            };

            var indexResponse = client.IndexDocument(linkActor);

            if (!indexResponse.IsValid)
            {
                throw new Exception("Error while inserting document: " + indexResponse.OriginalException);
            }
        }

        public void DeleteAndCreate(ElasticClient client, string index)
        {
            if (client.IndexExists(index).Exists)
            {
                var indexDeleteResponse = client.DeleteIndex(index);

                if (!indexDeleteResponse.IsValid)
                {
                    throw new Exception("Error while deleting index: " + indexDeleteResponse.OriginalException);
                }
            }

            var indexCreateResponse = client.CreateIndex(index, c => c
           .Map<Actor>(m => m
           .AutoMap(typeof(Person))));

            if (!indexCreateResponse.IsValid)
            {
                throw new Exception("Error while creating index: " + indexCreateResponse.OriginalException);
            }
        }
    }
}
