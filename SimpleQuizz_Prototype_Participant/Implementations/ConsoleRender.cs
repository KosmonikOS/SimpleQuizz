using SimpleQuizz_Prototype_Helpers.Helpers;
using SimpleQuizz_Prototype_Participant.Interfaces;

namespace SimpleQuizz_Prototype_Participant.Implementations;
internal class ConsoleRender : IRender
{
    public void PrintStartInfo()
    {
        Console.Clear();
        Console.WriteLine("Добро пожаловать в прототип SimpleQuizz");
        Console.WriteLine("Нажмите Enter, чтобы продолжить");
        Console.ReadLine();
    }
    public string RenderQuizzIdInsertion()
    {
        Console.Clear();
        Console.WriteLine("Введите код викторины, к которой хотите подключиться");
        var quizzId = Console.ReadLine();
        ConsoleInputHelper.CheckInsertFailure(ref quizzId, x =>
                x.Length != 7 || string.IsNullOrEmpty(x));
        return quizzId;
    }
    public string RenderParticipantNameInsertion()
    {
        Console.Clear();
        Console.WriteLine("Придумайте имя участника");
        var name = Console.ReadLine();
        ConsoleInputHelper.CheckInsertFailure(ref name, string.IsNullOrEmpty);
        return name;
    }
    public int RenderAnswerInsertion()
    {
        Console.Clear();
        Console.WriteLine("Введите свой вариант ответа");
        var answer = Console.ReadLine();
        return ConsoleInputHelper.CheckConvertionFailure<int>(answer,Int32.TryParse);
    }
    public void PrintQuestionOptions()
    {
        Console.Clear();
        Console.WriteLine("Выбирите вариант ответа из списка");
    }
    public void PrintEndInfo()
    {
        Console.Clear();
        Console.WriteLine("Спасибо за участие в викторине");
    }

    public void PrintWaitingInfo()
    {
        Console.Clear();
        Console.WriteLine("Ожидаем начало викторины");
    }
}
