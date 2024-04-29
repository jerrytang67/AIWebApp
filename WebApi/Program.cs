using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
// builder.Services.AddKernel().AddOpenAIChatCompletion("gpt-3.5-turbo", "key");
builder.Services.AddKernel().AddAzureOpenAIChatCompletion("gtp-3.5-turbo");
var app = builder.Build();

app.MapDefaultEndpoints();
app.MapGet("/chat", (string question, Kernel kernel) => kernel.InvokePromptStreamingAsync<string>(question));
app.Run();