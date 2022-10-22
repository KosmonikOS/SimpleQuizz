namespace SimpleQuizz_Prototype_Participant.Interfaces;
internal interface IRender
{
    public void PrintStartInfo();
    public string RenderQuizzIdInsertion();
    public string RenderParticipantNameInsertion();
    public int RenderAnswerInsertion();
    public void PrintQuestionOptions();
    public void PrintEndInfo();
    public void PrintWaitingInfo();
}

