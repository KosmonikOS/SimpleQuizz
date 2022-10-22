using SimpleQuizz_Prototype_Host.Implelentations;

var host = new QuizzHost();
host.AddQuestions();
await host.StartQuizzAsync();