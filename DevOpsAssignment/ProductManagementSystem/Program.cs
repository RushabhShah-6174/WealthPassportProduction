using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using ProductManagementSystem.Interfaces;
using ProductManagementSystem.Application;
using ProductManagementSystem.Infrastructure;
using ProductManagementSystem.Presentation.Middleware;
using Amazon.Runtime;
using Amazon.DynamoDBv2.Model;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();
builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
{
    string dynamoDbEndpoint = Environment.GetEnvironmentVariable("DYNAMODB_ENDPOINT") ?? builder.Configuration["DynamoDB:Endpoint"] ?? "http://localhost:8000";
    string awsRegion = Environment.GetEnvironmentVariable("AWS_REGION") ?? builder.Configuration["AWS:Region"] ?? "us-east-1";
    AmazonDynamoDBConfig clientConfig = new()
    {
        RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(awsRegion)
    };

    if (builder.Environment.IsDevelopment())
    {
        clientConfig.ServiceURL = dynamoDbEndpoint;
        clientConfig.UseHttp = true;

        return new AmazonDynamoDBClient(new BasicAWSCredentials("fakeMyKeyId", "fakeSecretAccessKey"), clientConfig);
    }
    else
        return new AmazonDynamoDBClient(clientConfig);
});

var tableName = builder.Configuration["DynamoDB:TableName"] ?? "Products";

builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddHealthChecks();
builder.Services.AddProblemDetails();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    var dynamoDb = app.Services.GetRequiredService<IAmazonDynamoDB>();
    var existingTables = await dynamoDb.ListTablesAsync();

    if (!existingTables.TableNames.Contains(tableName))
    {
        var createRequest = new CreateTableRequest
        {
            TableName = tableName,
            AttributeDefinitions = new List<AttributeDefinition>
            {
                new AttributeDefinition("Id", ScalarAttributeType.S)
            },
            KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement("Id", KeyType.HASH)
            },
            ProvisionedThroughput = new ProvisionedThroughput(5, 5)
        };

        await dynamoDb.CreateTableAsync(createRequest);
    }

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();
app.MapHealthChecks("/health");
app.Run();// "http://0.0.0.0:80");
