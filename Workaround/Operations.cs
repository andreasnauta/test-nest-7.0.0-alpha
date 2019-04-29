using Common.Classes;
using Elasticsearch.Net;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;

namespace Workaround
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
            var pool = new SingleNodeConnectionPool(new Uri(elasticConnection));

            var connectionSettings = new ConnectionSettings(pool, JsonNetSerializer.Default).BasicAuthentication(elasticUser, elasticPassword);

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

            var actorAsJsonString = JsonConvert.SerializeObject(linkActor, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var indexResponse = client.IndexDocument(JObject.Parse(actorAsJsonString));

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

            var indexCreateResponse = client.CreateIndex(index);

            if (!indexCreateResponse.IsValid)
            {
                throw new Exception("Error while creating index: " + indexCreateResponse.OriginalException);
            }
        }
    }
}
