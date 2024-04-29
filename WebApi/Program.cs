using Microsoft.SemanticKernel;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddKernel().AddOpenAIChatCompletion("gpt-3.5-turbo", "key");
var app = builder.Build();
app.MapGet("/chat", (string question, Kernel kernel) => kernel.InvokePromptStreamingAsync<string>(question));
app.Run();