using Microsoft.AspNetCore.SignalR.Client;
using SimpleQuizz_Prototype_Participant.Interfaces;

namespace SimpleQuizz_Prototype_Participant.Implementations;
internal class AspNetCoreParticipantHubConnection : IParticipantHubConnection
{
    private string connectionString = "https://localhost:7195/quizzes";
    private HubConnection connection;
    private string? hostId;
    public AspNetCoreParticipantHubConnection()
    {
        connection = new HubConnectionBuilder()
            .WithUrl(connectionString)
            .WithAutomaticReconnect().Build();
        SubscribeOnEvent<string>("GetHostId", HandleGetHostId);
    }
    public string HostId { set { hostId = value; } }
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
        await connection.SendAsync("ConnectToQuizz", quizzId);
    }
    public async Task SendParticipantNameAsync(string name)
    {
        await connection.SendAsync("SendParticipantName", hostId, name);
    }
    public async Task SendAnswerAsync(int answer)
    {
        await connection.SendAsync("SendAnswerToHost", hostId, answer.ToString());
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
    private void HandleGetHostId(string hostId)
    {
        this.hostId = hostId;
    }
}