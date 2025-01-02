namespace NullableRefTypes;

public class SurveyRun
{
    private readonly List<SurveyQuestion> surveyQuestions = [];

    private List<SurveyResponse>? respondents;

    public void AddQuestion(QuestionType type, string question) =>
        AddQuestion(new SurveyQuestion(type, question));

    public void AddQuestion(SurveyQuestion surveyQuestion) => surveyQuestions.Add(surveyQuestion);

    public void PerformSurvey(int numberOfRespondents)
    {
        int respondentsConsenting = 0;
        respondents = [];
        while (respondentsConsenting < numberOfRespondents)
        {
            var respondent = SurveyResponse.GetRandomId();
            if (respondent.AnswerSurvey(surveyQuestions))
                respondentsConsenting++;
            respondents.Add(respondent);
        }
    }

    public IEnumerable<SurveyResponse> AllParticipants => (respondents ?? Enumerable.Empty<SurveyResponse>());
    public ICollection<SurveyQuestion> Questions => surveyQuestions;
    public SurveyQuestion GetQuestion(int index) => surveyQuestions[index];
}
