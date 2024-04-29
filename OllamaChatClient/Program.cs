using System.Net.Http.Json;
using Newtonsoft.Json.Linq;

using HttpClient client = new();
client.BaseAddress = new Uri("http://localhost:11434");

while (true) {
    Console.Write("请输入问题：");
    var q = Console.ReadLine();

    var str = new {
        model = "llama3:8b",
        messages = new[] {
            new {
                role = "system",
                content = "你是一个全知全能的中文助手,请把问题用简短的中文回答"
            },
            new {
                role = "user",
                content = q
            }
        },
        stream = true
    };

    var response = await client.PostAsJsonAsync("http://localhost:11434/api/chat", str);

    if (response.IsSuccessStatusCode) {
        using (var stream = await response.Content.ReadAsStreamAsync())
        using (var reader = new StreamReader(stream)) {
            var content = await reader.ReadToEndAsync();
            var jsonObjects = content.Split(["}\n{"], StringSplitOptions.None);

            for (var i = 0; i < jsonObjects.Length; i++) {
                if (i != 0) {
                    jsonObjects[i] = "{" + jsonObjects[i];
                }

                if (i != jsonObjects.Length - 1) {
                    jsonObjects[i] += "}";
                }

                var obj = JObject.Parse(jsonObjects[i]);
                ProcessElement(obj);
            }
        }
    }

    Console.WriteLine();
}

void ProcessElement(JObject element) {
    // 在这里处理 JObject
    // 你可以使用 element["propertyName"] 或者 element.Property("propertyName") 来访问属性
    // 例如：var content = element["message"]?["content"]?.ToString();
    // 其中 "?" 是在属性不存在时避免空引用异常的 C# 语法糖
    // 进行你的后续处理...
    var content = element["message"]?["content"]?.ToString();
    Console.Write(content);
}

// {"model":"llama3:8b","created_at":"2024-04-29T13:33:10.8926565Z","message":{"role":"assistant","content":"😊"},"done":false}
// {"model":"llama3:8b","created_at":"2024-04-29T13:33:10.8926565Z","message":{"role":"assistant","content":"😊"},"done":false}
// {"model":"llama3:8b","created_at":"2024-04-29T13:33:10.8926565Z","message":{"role":"assistant","content":"😊"},"done":false}
// {"model":"llama3:8b","created_at":"2024-04-29T13:33:10.8926565Z","message":{"role":"assistant","content":"😊"},"done":false}