using SimpleQuizz_Prototype_Participant.Interfaces;

namespace SimpleQuizz_Prototype_Participant.Implementations;
internal class QuizzParticipant
{
    private IParticipantHubConnection hubConnection 
        = new AspNetCoreParticipantHubConnection();
    private IRender render = new ConsoleRender();
    private CancellationTokenSource gamecancellationTokenSource 
        = new CancellationTokenSource();
    private string name;
    public QuizzParticipant()
    {
        render.PrintStartInfo();
        hubConnection.SubscribeOnEvent("GetHostId", HandleGetHostId);
        hubConnection.SubscribeOnEvent("NextQuestion", HandleNextQuestion);
        hubConnection.SubscribeOnEvent("DisconnectFromQuizz", HandleDisconnectFromQuizz);
    }
    public async Task StartQuizzAsync()
    {
        var quizzId = render.RenderQuizzIdInsertion();
        name = render.RenderParticipantNameInsertion();
        await hubConnection.ConnectToHubAsync();
        await hubConnection.ConnectToQuizzAsync(quizzId);
        render.PrintWaitingInfo();
        ProcessQuizz();
    }
    private void ProcessQuizz()
    {
        var cancellationToken = gamecancellationTokenSource.Token;
        while (true)
            if (cancellationToken.IsCancellationRequested)
                return;
    }
    private async Task HandleGetHostId()
    {
        await hubConnection.SendParticipantNameAsync(name);
    }
    private async Task HandleNextQuestion()
    {
        var answer = render.RenderAnswerInsertion();
        await hubConnection.SendAnswerAsync(answer);
    }
    private async Task HandleDisconnectFromQuizz()
    {
        await hubConnection.DisconnectFromHubAsync();
        gamecancellationTokenSource.Cancel();
    }
}



