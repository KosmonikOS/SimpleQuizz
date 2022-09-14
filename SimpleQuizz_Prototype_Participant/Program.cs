using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Введите код викторины , чтобы присоединиться");
var quizzId = Console.ReadLine();
Console.WriteLine("Введите свой никнейм");
var nickName = Console.ReadLine();
var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7195/quizzes")
    .WithAutomaticReconnect().Build();
var hostId = "";

await connection.StartAsync();

await connection.InvokeAsync("ConnectToQuizz", quizzId);

connection.On("GetHostId",async (string newHostId) =>
{
    hostId = newHostId;
    await connection.SendAsync("SendParticipantName", hostId, nickName);
});

connection.On("NextQuestion", async () =>
{
    if (!string.IsNullOrEmpty(hostId))
    {
        Console.Clear();
        Console.WriteLine("Выбирите вариант ответа (1-4)");
        var answer = Console.ReadLine();
        await connection.SendAsync("SendAnswer", hostId,answer);
        Console.WriteLine("Ждем нового вопроса");
    }
});

Console.WriteLine("Ждем начало викторины");

while (true) ;