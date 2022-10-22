namespace SimpleQuizz_Prototype_Host.Entities;
internal class Question
{
    private string text;
    private int points;
    private List<Option> options;
    private int answer;
    public Question(string text, int points, List<Option> options, int answer)
    {
        this.text = text;
        this.points = points;
        this.options = options;
        this.answer = answer;
    }
    public string Text { get { return text; } }
    public int Points { get { return points; } }
    public List<Option> Options { get { return options; } }
    public bool IsCorrectAnswer(int answer) => this.answer == answer;
}
