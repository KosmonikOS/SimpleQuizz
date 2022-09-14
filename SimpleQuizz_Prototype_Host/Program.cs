using Microsoft.AspNetCore.SignalR.Client;
using SimpleQuizz_Prototype_Host;

var questions = new List<Question>();

Console.WriteLine("Введите + , чтобы добавить вопрос, или - чтобы продолжить");

while (Console.ReadLine() == "+")
{
    var question = new Question()
    {
        Options = new List<Option>() {
            new Option(),new Option(),
            new Option(),new Option()
        }
    };
    Console.WriteLine("Введите текст вопроса");
    question.Options[3].Text = Console.ReadLine();
    Console.WriteLine("Введите кол-во баллов");
    question.Points = Int32.Parse(Console.ReadLine() ?? "1");
    Console.WriteLine("Введите текст первого варианта ответа");
    question.Options[0].Text = Console.ReadLine() ?? "";
    Console.WriteLine("Введите текст второго варианта ответа");
    question.Options[1].Text = Console.ReadLine() ?? "";
    Console.WriteLine("Введите текст третьего варианта ответа");
    question.Options[2].Text = Console.ReadLine() ?? "";
    Console.WriteLine("Введите текст четвертого варианта ответа");
    question.Options[3].Text = Console.ReadLine() ?? "";
    Console.WriteLine("Введите номер правильного ответа (1-4)");
    var answer = Int32.Parse(Console.ReadLine() ?? "1") - 1;
    question.Answer = answer >= 0 && answer <= 3 ? answer : 0;
    questions.Add(question);
    Console.WriteLine("Введите + , чтобы добавить вопрос, или - чтобы продолжить");
}
Console.WriteLine("------------------------------------------------------------------------");

Console.Clear();

var quizzId = new Random().Next(0, 1000000).ToString().PadLeft(7,'0');
var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7195/quizzes")
    .WithAutomaticReconnect().Build();

Console.WriteLine($"Код викторины {quizzId}");

Console.WriteLine("Ожидаем участников");

await connection.StartAsync();

await connection.InvokeAsync("ConnectToQuizz", quizzId);

Console.WriteLine("Нажмите любую клавишу, чтобы начать");

Console.ReadLine();

var participants = new Dictionary<string, int>();

var currentQuestion = questions[0];

connection.On("GetAnswer", (string participantId, int answer) =>
{
    if(currentQuestion.Answer == answer)
        if (!participants.ContainsKey(participantId))
            participants[participantId] = currentQuestion.Points;
        else
            participants[participantId]+= currentQuestion.Points;
});

await connection.InvokeAsync("SendHostId", quizzId,connection.ConnectionId);

Console.Clear();

for (int i = 0; i < questions.Count; i++)
{
    currentQuestion = questions[i];
    Console.WriteLine(currentQuestion.Text);
    Console.WriteLine($"1) {currentQuestion.Options[0].Text}");
    Console.WriteLine($"2) {currentQuestion.Options[1].Text}");
    Console.WriteLine($"2) {currentQuestion.Options[2].Text}");
    Console.WriteLine($"3) {currentQuestion.Options[3].Text}");
    await connection.InvokeAsync("StartNewQuestion", quizzId);
    await Task.Delay(10000);
    Console.Clear();
}
Console.WriteLine("Итоги:");
var place = 1;
foreach(var kvp in participants.OrderBy(x => x.Value))
{
    Console.WriteLine($"{place}) {kvp.Key} {kvp.Value}");
    place++;
}