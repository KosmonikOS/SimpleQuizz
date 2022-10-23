namespace SimpleQuizz_Prototype.Services.Interfaces;
public interface IHostQuizzMapper
{
    public void MapHostToQuizz(string quizzId, string hostId);
    public string GetHostId(string quizzId);
}
