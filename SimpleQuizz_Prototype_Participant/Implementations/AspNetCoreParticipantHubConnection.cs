using Microsoft.AspNetCore.SignalR.Client;
using SimpleQuizz_Prototype_Participant.Interfaces;

namespace SimpleQuizz_Prototype_Participant.Implementations;
internal class AspNetCoreParticipantHubConnection : IParticipantHubConnection
{
    private string connectionString = "https://localhost:7195/quizzes";
    private HubConnection connection;
    private string quizzId;
    public AspNetCoreParticipantHubConnection()
    {
        connection = new HubConnectionBuilder()
            .WithUrl(connectionString)
            .WithAutomaticReconnect().Build();
        connection.Reconnected += async (string? _) =>
        {
            await ConnectToQuizzAsync(quizzId);
        };
    }
    public async Task ConnectToHubAsync()
    {
        await connection.StartAsync();
    }
    public async Task DisconnectFromHubAsync()
    {
        await connection.StopAsync();
    }
    public async Task ConnectToQuizzAsync(string quizzId)
    {
        this.quizzId = quizzId;
        await connection.SendAsync("ConnectToQuizz", quizzId);
    }
    public async Task SendParticipantInfoAsync(string name, string participantId)
    {
        await connection.SendAsync("SendParticipantInfoToHost", quizzId, name, participantId);
    }
    public async Task SendAnswerAsync(int answer, string participantId)
    {
        await connection.SendAsync("SendAnswerToHost", quizzId, answer.ToString(), participantId);
    }
    public void SubscribeOnEvent<T>(string eventName, Func<T, Task> handler)
    {
        connection.On(eventName, handler);
    }
    public void SubscribeOnEvent(string eventName, Func<Task> handler)
    {
        connection.On(eventName, handler);
    }
    public void SubscribeOnEvent<T1, T2>(string eventName, Func<T1, T2, Task> handler)
    {
        connection.On(eventName, handler);
    }
    public void SubscribeOnEvent<T>(string eventName, Action<T> handler)
    {
        connection.On(eventName, handler);
    }
    public void SubscribeOnEvent<T1, T2>(string eventName, Action<T1, T2> handler)
    {
        connection.On(eventName, handler);
    }
}