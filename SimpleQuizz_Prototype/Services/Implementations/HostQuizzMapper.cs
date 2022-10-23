using SimpleQuizz_Prototype.Services.Interfaces;

namespace SimpleQuizz_Prototype.Services.Implementations;
public class HostQuizzMapper : IHostQuizzMapper
{
    private Dictionary<string, string> pairs = new Dictionary<string, string>();
    public void MapHostToQuizz(string quizzId, string hostId)
    {
        pairs[quizzId] = hostId;
    }
    public string GetHostId(string quizzId)
    {
        return pairs[quizzId];
    }
}