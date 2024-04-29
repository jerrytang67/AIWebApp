var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.MapGet("/chat", InvokePrompt);
app.Run();


static async IAsyncEnumerable<string> InvokePrompt(string question) {
    foreach (var word in question.Split(' ')) {
        await Task.Delay(250);
        yield return word + " ";
    }
}