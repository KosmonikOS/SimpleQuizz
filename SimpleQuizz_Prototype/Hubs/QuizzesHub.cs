using Microsoft.AspNetCore.SignalR;

namespace SimpleQuizz_Prototype.Hubs
{
    public class QuizzesHub : Hub
    {
        public async Task ConnectToQuizz(string quizzId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, quizzId);
        }
        public async Task SendHostIdToParticipants(string quizzId, string hostId)
        {
            await Clients.OthersInGroup(quizzId).SendAsync("GetHostId", hostId);
        }
        public async Task SendQuestionToParticipants(string quizzId)
        {
            await Clients.OthersInGroup(quizzId).SendAsync("NextQuestion");
        }
        public async Task SendAnswerToHost(string hostId, string answer)
        {
            await Clients.Client(hostId).SendAsync("GetAnswer", Context.ConnectionId, answer);
        }
        public async Task SendParticipantName(string hostId, string name)
        {
            await Clients.Client(hostId).SendAsync("GetParticipantName", Context.ConnectionId, name);
        }
        public async Task SendDisconnectionMessage(string quizzId)
        {
            await Clients.OthersInGroup(quizzId).SendAsync("DisconnectFromQuizz");
        }
    }
}
