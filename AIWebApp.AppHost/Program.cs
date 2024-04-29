var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WebApi>("webapi");
builder.AddProject<Projects.OllamaChatClient>("OllamaChatClient");
builder.AddProject<Projects.WebAppClient>("WebAppClient");

builder.Build().Run();
