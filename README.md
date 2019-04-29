# Introduction
This repository is for testing the mapping of abstract classes to Elasticsearch. I don't understand NEST or Elastic fully (which is probably obvious), but I hope it helps. There is no Assertions, as I don't know how to search well enough in NEST (see Problems), but choose to create the project 
as Class Libraries with Unit-Test in case someone want's to add the search or I learn it myself at some point.

# Usage
Insert the relevant connection information in Operations class in the following projects:
- 6.6.0
- 7.0.0
- Workaround

# Problems:
1: I am unable to get the Search Query to work with a Type in both 6.6.0 and 7.0.0. If I search like this:

```csharp
client.Search<LinkActor>(q => q.MatchAll())
```

I get a the following results:
- 6.6.0
Could not create an instance of type Common.Classes.Actor.

- 7.0.0:
Elasticsearch.Net.UnexpectedElasticsearchClientException: generated serializer for Actor does not support deserialize.

Other Types (fx Person) or dynamic either gives empty results or results with empty documents.

2: I keep getting "System.IO.IOException: Unable to write data to the transport connection:" after some time forcing me to restart VS2019. Something is not closed correctly.

# Results
- 6.6.0: 
```
/test/_search?q=Description

{
    "_index": "test",
    "_type": "linkactor",
    "_id": "removed",
    "_score": 0.2876821,
    "_source": {
        "actor": {
            "name": "Name",
            "description": "Description"
        }
    }
}
```
- 7.0.0:
```
/test/_search?q=Description
{
    "_index": "test",
    "_type": "_doc",
    "_id": "removed",
    "_score": 0.2876821,
    "_source": {
        "actor": {
            "description": "Description"
        }
    }
}
```
- Workaround:
```
/test/_search?q=Description
{
    "_index": "test",
    "_type": "_doc",
    "_id": "removed",
    "_score": 0.2876821,
    "_source": {
        "actor": {
            "name": "Name",
            "description": "Description"
        }
    }
}
```