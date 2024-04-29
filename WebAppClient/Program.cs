using System.Net.Http.Json;

using HttpClient client = new();
client.BaseAddress = new Uri("http://localhost:5211");

while (true) {
    Console.Write("请输入问题：");
    var q = Console.ReadLine();
    
    await foreach (var msg in client.GetFromJsonAsAsyncEnumerable<string>(
                       $"/chat?question={q}")) {
        Console.Write(msg);
    }
    Console.WriteLine();
}