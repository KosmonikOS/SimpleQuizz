using SimpleQuizz_Prototype_Host.Entities;

namespace SimpleQuizz_Prototype_Host.Interfaces;
internal interface IRender
{
    public void PrintStartInfo();
    public void PrintQuizzInfo(string QuizzId);
    public void PrintParticipantInfo(Participant participant);
    public void PrintParticipantsPoints(IEnumerable<Participant> participants);
    public void PrintSummary(IEnumerable<Participant> participants);
    public void PrintQuestion(Question question);
    public void RenderQuizzStart();
    public Question RenderQuestionCreation(out bool continueAdd);
}
