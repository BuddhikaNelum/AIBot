using Microsoft.Extensions.AI;
using OpenAI;
using System.Text.Json;

class Program
{ 
    static async Task Main(string[] args)
    {
        IChatClient chatClient = new OpenAIClient("OPEN_AI_API_KEY").AsChatClient(modelId: "gpt-4o-mini");

        var propmt = "You're a friendly, intelligent assistant who supports people with their medical issues. Please provide correct answers and do not answer any questions outside the domain. Make sure your answer is as simple and short as possible.";

        var history = new List<ChatHistory>();

        if (history.Count == 0)
        {
            history.Add(new ChatHistory
            {
                Role = "AiBot",
                Message = propmt,
            });
        }

        Console.WriteLine("AiBot -: Hi, I am AiBot, Feel free to ask anything!");

        while (true)
        {
            Console.Write("User -: ");
            var query = Console.ReadLine();

            history.Add(new ChatHistory
            {
                Role = "User",
                Message = query
            });

            

            var response =  await chatClient.CompleteAsync(JsonSerializer.Serialize(history));
            Console.WriteLine($"AiBot -: {response.Message.Text}");

            history.Add(new ChatHistory
            {
                Role = "AiBot",
                Message = response.Message.Text
            });
        }

    }
}

public class ChatHistory
{
    public string Role { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}