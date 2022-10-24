using SimpleQuizz_Prototype_Helpers.Helpers;
using SimpleQuizz_Prototype_Host.Entities;
using SimpleQuizz_Prototype_Host.Interfaces;

namespace SimpleQuizz_Prototype_Host.Implelentations;
internal class ConsoleRender : IRender
{
    public void PrintStartInfo()
    {
        Console.Clear();
        Console.WriteLine("Добро пожаловать в прототип SimpleQuizz");
        Console.WriteLine("Нажмите Enter, чтобы продолжить");
        Console.ReadLine();
    }
    public void PrintQuizzInfo(string quizzId)
    {
        Console.Clear();
        Console.WriteLine($"Код викторины {quizzId}");
        Console.WriteLine("Ожидаем участников");
        Console.WriteLine("Нажмите Enter, чтобы начать");
        Console.ReadLine();
    }
    public void PrintParticipantInfo(Participant participant)
    {
        Console.WriteLine($"Участник {participant.Name} присоединился");
    }
    public void PrintParticipantsPoints(IEnumerable<Participant> participants)
    {
        Console.Clear();
        foreach (var participant in participants)
        {
            Console.WriteLine($"{participant.Name} {participant.Points} баллов");
        }
    }
    public void PrintSummary(IEnumerable<Participant> participants)
    {
        Console.Clear();
        var winners = participants
            .OrderByDescending(x => x.Points).Take(3);
        Console.WriteLine("Победители:");
        foreach (var winner in winners)
        {
            Console.WriteLine($"{winner.Name} {winner.Points} баллов");
        }
    }
    public void PrintQuestion(Question question)
    {
        Console.Clear();
        Console.WriteLine(question.Text);
        for (int i = 0; i < question.Options.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {question.Options[i].Text}");
        }
    }
    public void RenderQuizzStart()
    {
        Console.Clear();
        Console.WriteLine("Давайте добавим вопросы в викторину");
    }
    public Question RenderQuestionCreation(out bool continueAdd)
    {
        Console.Clear();
        var question = new Question(RenderQuestionTextCreation(),
                            RenderQuestionPointsCreation(),
                            RenderOptionsCreation(),
                            RenderQuestionAnswerCreation());
        Console.WriteLine("Введите + , чтобы добавить вопрос, или - чтобы продолжить");
        continueAdd = Console.ReadLine() == "+" ? true : false;
        return question;
    }
    private string RenderQuestionTextCreation()
    {
        Console.WriteLine("Введите текст вопроса");
        var text = Console.ReadLine();
        ConsoleInputHelper.CheckInsertFailure(ref text, string.IsNullOrEmpty);
        return text;
    }
    private int RenderQuestionPointsCreation()
    {
        Console.WriteLine("Введите кол-во баллов");
        var points = Console.ReadLine();
        return ConsoleInputHelper.CheckConvertionFailure<int>(points, Int32.TryParse);
    }
    private int RenderQuestionAnswerCreation()
    {
        Console.WriteLine("Введите номер правильного ответа");
        var answer = Console.ReadLine();
        return ConsoleInputHelper.CheckConvertionFailure<int>(answer, Int32.TryParse);
    }
    private List<Option> RenderOptionsCreation()
    {
        Console.WriteLine("Сколько вариантов ответа добавить?");
        var input = Console.ReadLine();
        var quantity = ConsoleInputHelper.CheckConvertionFailure<int>(input, Int32.TryParse);
        var options = new List<Option>(quantity);
        for (int i = 0; i < quantity; i++)
        {
            Console.WriteLine($"Введите текст варианта ответа номер {i + 1}");
            var text = Console.ReadLine();
            ConsoleInputHelper.CheckInsertFailure(ref text, string.IsNullOrEmpty);
            options.Add(new Option(text));
        }
        return options;
    }
}
