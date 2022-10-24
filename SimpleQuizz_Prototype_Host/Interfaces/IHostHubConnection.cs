namespace SimpleQuizz_Prototype_Host.Interfaces;
internal interface IHostHubConnection
{
    public Task ConnectToHubAsync();
    public Task DisconnectFromHubAsync();
    public Task ConnectToQuizzAsync(string quizzId);
    public Task SendQuestionAsync();
    public void SubscribeOnEvent<T>(string eventName, Func<T, Task> handler);
    public void SubscribeOnEvent(string eventName, Func<Task> handler);
    public void SubscribeOnEvent<T1, T2>(string eventName, Func<T1, T2, Task> handler);
    public void SubscribeOnEvent<T>(string eventName, Action<T> handler);
    public void SubscribeOnEvent<T1, T2>(string eventName, Action<T1, T2> handler);
}

