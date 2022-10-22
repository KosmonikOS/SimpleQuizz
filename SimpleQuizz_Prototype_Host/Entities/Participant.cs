namespace SimpleQuizz_Prototype_Host.Entities;
internal class Participant
{
    private string name;
    private int points = 0;
    public Participant(string name)
    {
        this.name = name;
    }
    public string Name { get { return name; } }
    public int Points { get { return points; } }
    public void AddPoints(int points)
    {
        this.points += points;
    }
}
