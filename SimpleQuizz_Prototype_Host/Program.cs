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
    question.Text = Console.ReadLine();
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
    var answer = Int32.Parse(Console.ReadLine() ?? "1");
    question.Answer = answer > 0 && answer <= 4 ? answer : 1;
    questions.Add(question);
    Console.WriteLine("Введите + , чтобы добавить вопрос, или - чтобы продолжить");
}
Console.Clear();

var quizzId = new Random().Next(0, 1000000).ToString().PadLeft(7, '0');
var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7195/quizzes")
    .WithAutomaticReconnect().Build();

Console.WriteLine($"Код викторины {quizzId}");

Console.WriteLine("Ожидаем участников");

await connection.StartAsync();

await connection.InvokeAsync("ConnectToQuizz", quizzId);

Console.WriteLine("Нажмите любую клавишу, чтобы начать");

Console.ReadLine();

var participants = new Dictionary<string, Participant>();

var currentQuestion = questions[0];

connection.On("GetAnswer", (string participantId, string answer) =>
{
    var points = currentQuestion.Answer != Int32.Parse(answer)
                ? 0 : currentQuestion.Points;
    participants[participantId].Points += points;
});
connection.On("GetParticipantName", (string participantId, string participantName) =>
{
    participants[participantId] = new Participant()
    {
        Name = participantName,
        Points = 0
    };
});

await connection.InvokeAsync("SendHostId", quizzId, connection.ConnectionId);

Console.Clear();

for (int i = 0; i < questions.Count; i++)
{
    currentQuestion = questions[i];
    Console.WriteLine(currentQuestion.Text);
    Console.WriteLine($"1) {currentQuestion.Options[0].Text}");
    Console.WriteLine($"2) {currentQuestion.Options[1].Text}");
    Console.WriteLine($"3) {currentQuestion.Options[2].Text}");
    Console.WriteLine($"4) {currentQuestion.Options[3].Text}");
    await connection.SendAsync("StartNewQuestion", quizzId);
    await Task.Delay(10000);
    Console.Clear();
}
Console.WriteLine("Итоги:");
var place = 1;
foreach (var kvp in participants.OrderBy(x => x.Value.Points))
{
    Console.WriteLine($"{place}) {kvp.Value.Name} {kvp.Value.Points}");
    place++;
}
Console.ReadLine();