namespace SimpleQuizz_Prototype_Host;
internal class Question
{
    public string Text { get; set; }
    public int Points { get; set; }
    public List<Option> Options { get; set; }
    public int Answer { get; set; }
}
