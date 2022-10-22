using Microsoft.AspNetCore.SignalR.Client;
using SimpleQuizz_Prototype_Host.Interfaces;

namespace SimpleQuizz_Prototype_Host.Implelentations;
internal class AspNetCoreHostHubConnection : IHostHubConnection
{
    private string connectionString = "https://localhost:7195/quizzes";
    private HubConnection connection;
    private string? quizzId;
    public AspNetCoreHostHubConnection()
    {
        connection = new HubConnectionBuilder()
            .WithUrl(connectionString)
            .WithAutomaticReconnect().Build();
    }
    public async Task ConnectToHubAsync()
    {
        await connection.StartAsync();
    }
    public async Task DisconnectFromHubAsync()
    {
        await SendDisconnectMessage();
        await connection.StopAsync();
    }
    public async Task ConnectToQuizzAsync(string quizzId)
    {
        this.quizzId = quizzId;
        await connection.InvokeAsync("ConnectToQuizz", quizzId);
    }
    public async Task SendHostIdToParticipantsAsync()
    {
        await connection.InvokeAsync("SendHostIdToParticipants", quizzId, connection.ConnectionId);
    }
    public async Task SendQuestionAsync()
    {
        await connection.SendAsync("SendQuestionToParticipants", quizzId);
    }
    private async Task SendDisconnectMessage()
    {
        await connection.SendAsync("SendDisconnectionMessage", quizzId);
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
