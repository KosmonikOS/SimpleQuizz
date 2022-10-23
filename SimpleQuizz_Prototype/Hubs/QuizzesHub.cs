using Microsoft.AspNetCore.SignalR;
using SimpleQuizz_Prototype.Services.Interfaces;

namespace SimpleQuizz_Prototype.Hubs;
public class QuizzesHub : Hub
{
    private readonly IHostQuizzMapper hqMapper;

    public QuizzesHub(IHostQuizzMapper hqMapper)
    {
        this.hqMapper = hqMapper;
    }
    public async Task ConnectToQuizz(string quizzId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, quizzId);
    }
    public void MapHostId(string quizzId)
    {
        hqMapper.MapHostToQuizz(quizzId, Context.ConnectionId);
    }
    public async Task SendQuestionToParticipants(string quizzId)
    {
        await Clients.OthersInGroup(quizzId).SendAsync("NextQuestion");
    }
    public async Task SendAnswerToHost(string quizzId,string answer)
    {
        var hostId = hqMapper.GetHostId(quizzId);
        if (hostId is not null)
            await Clients.Client(hostId).SendAsync("GetAnswer", Context.ConnectionId, answer);
    }
    public async Task SendParticipantName(string quizzId,string name)
    {
        var hostId = hqMapper.GetHostId(quizzId);
        if (hostId is not null)
            await Clients.Client(hostId).SendAsync("GetParticipantName", Context.ConnectionId, name);
    }
    public async Task SendDisconnectionMessage(string quizzId)
    {
        await Clients.OthersInGroup(quizzId).SendAsync("DisconnectFromQuizz");
    }
}