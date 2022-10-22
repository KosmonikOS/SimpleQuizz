using SimpleQuizz_Prototype_Host.Entities;
using SimpleQuizz_Prototype_Host.Interfaces;

namespace SimpleQuizz_Prototype_Host.Implelentations;
internal class QuizzHost
{
    private List<Question> questions = new List<Question>();
    private Question currentQuestion;
    private Dictionary<string, Participant> participants = new Dictionary<string, Participant>();
    private IHostHubConnection hubConnection = new AspNetCoreHostHubConnection();
    private IRender render = new ConsoleRender();
    public QuizzHost()
    {
        hubConnection.SubscribeOnEvent<string, string>("GetAnswer", HandleGetAnswer);
        hubConnection.SubscribeOnEvent<string, string>("GetParticipantName", HandleGetName);
        render.PrintStartInfo();
    }
    public async Task StartQuizzAsync()
    {
        var quizzId = GenerateQuizzId();
        await hubConnection.ConnectToHubAsync();
        await hubConnection.ConnectToQuizzAsync(quizzId);
        render.PrintQuizzInfo(quizzId);
        await hubConnection.SendHostIdToParticipantsAsync();
        await ProcessQuizzAsync();
    }
    public async Task StopQuizzAsync()
    {
        render.PrintSummary(participants.Values);
        await hubConnection.DisconnectFromHubAsync();
    }
    public void AddQuestions()
    {
        var continueAddition = true;
        while (continueAddition)
        {
            questions.Add(render.RenderQuestionCreation(out continueAddition));
        }
    }
    private async Task ProcessQuizzAsync()
    {
        foreach (var question in questions)
        {
            currentQuestion = question;
            render.PrintQuestion(question);
            await hubConnection.SendQuestionAsync();
            await Task.Delay(10000);
            render.PrintParticipantsPoints(participants.Values);
            await Task.Delay(5000);
        }
        await StopQuizzAsync();
    }
    private string GenerateQuizzId()
        => new Random().Next(0, 1000000)
        .ToString().PadLeft(7, '0');
    private void HandleGetAnswer(string participantId, string answer)
    {
        participants[participantId].AddPoints(currentQuestion
            .IsCorrectAnswer(Int32.Parse(answer)) ? 0 : currentQuestion.Points);
    }
    private void HandleGetName(string participantId, string participantName)
    {
        participants[participantId] = new Participant(participantName);
    }
}

