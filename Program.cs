using Microsoft.Azure.Cosmos;

CosmosClient client = new(
    accountEndpoint: "<azure-cosmos-db-nosql-endpoint-uri>",
    authKeyOrResourceToken: "<azure-cosmos-db-nosql-primary-key>"
);

Database database = await client.CreateDatabaseIfNotExistsAsync(
    id: "cosmicworks"
);
Console.WriteLine($"Database:\t{database.Id}");

Container container = await database.CreateContainerIfNotExistsAsync(
    id: "products",
    partitionKeyPath: "/categoryId",
    throughput: 400
);
Console.WriteLine($"Container:\t{container.Id}");

var item = new {
    id = "ddd6434a-0a4a-4c24-9387-67eda2f1d8dd",
    categoryId = "e6877436-bddc-4c2d-bc62-857f52c6cd20",
    categoryName = "Climbing Shoes",
    name = "Filiance Climbing Shoes",
    price = 115.00,
    quantity = 21,
    tags = new[]
    {
        "color-yellow",
        "gear-climbing-shoes"
    }
};

var response = await container.UpsertItemAsync(item);
Console.WriteLine($"Item Upserted:\t{item.id}");

Console.WriteLine($"Request Charge:\t{response.RequestCharge} RUs");
