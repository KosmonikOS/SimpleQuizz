namespace SimpleQuizz_Prototype_Host.Entities;
internal class Option
{
    private string text;
    public Option(string text)
    {
        this.text = text;
    }
    public string Text { get { return text; } }
}
