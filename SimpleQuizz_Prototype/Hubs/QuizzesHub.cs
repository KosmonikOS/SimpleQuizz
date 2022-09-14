using Microsoft.AspNetCore.SignalR;

namespace SimpleQuizz_Prototype.Hubs
{
    public class QuizzesHub : Hub
    {
        public async Task ConnectToQuizz(string connectionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connectionId);
        }
        public async Task SendHostId(string connectionId,string hostId)
        {
            await Clients.OthersInGroup(connectionId).SendAsync("GetHostId",hostId);
        }
        public async Task StartNewQuestion(string connectionId)
        {
            await Clients.OthersInGroup(connectionId).SendAsync("NextQuestion");
        }
        public async Task SendAnswer(string hostId,string answer)
        {
            await Clients.Client(hostId).SendAsync("GetAnswer", Context.ConnectionId, answer);
        }
    }
}
