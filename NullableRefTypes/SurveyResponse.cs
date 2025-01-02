namespace NullableRefTypes;

public class SurveyResponse(int id)
{
    public int Id { get; } = id;
    private Dictionary<int, string>? surveyResponses;

    private static readonly Random randomGenerator = new();

    public static SurveyResponse GetRandomId() => new(randomGenerator.Next());


    public bool AnswerSurvey(IEnumerable<SurveyQuestion> questions)
    {
        if (ConsentToSurvey())
        {
            surveyResponses = new Dictionary<int, string>();

            int index = 0;
            foreach (var question in questions)
            {
                var answer = GenerateAnswer(question);
                if (answer != null)
                {
                    surveyResponses.Add(index, answer);
                }
                index++;
            }
        }
        return surveyResponses != null;
    }

    private static bool ConsentToSurvey() => randomGenerator.Next(0, 2) == 1;

    private static string? GenerateAnswer(SurveyQuestion question)
    {
        switch (question.TypeOfQuestion)
        {
            case QuestionType.YesNo:
                int n = randomGenerator.Next(-1, 2);
                return (n == -1) ? default : (n == 0) ? "No" : "Yes";

            case QuestionType.Number:
                n = randomGenerator.Next(-30, 101);
                return (n < 0) ? default : n.ToString();

            case QuestionType.Text:
            default:
                //switch (randomGenerator.Next(0, 5))
                //{
                //    case 0:
                //        return default;
                //    case 1:
                //        return "Red";
                //    case 2:
                //        return "Green";
                //    case 3:
                //        return "Blue";
                //}
                //return "Red. No, Green. Wait.. Blue... AAARGGGGGHHH!";
                return randomGenerator.Next(0, 5) switch
                {
                    0 => default,
                    1 => "Red",
                    2 => "Green",
                    3 => "Blue",
                    _ => "Red. No, Green. Wait.. Blue... AAARGGGGGHHH!"
                };
        }
    }

    public bool AnsweredSurvey => surveyResponses != null;
    public string Answer(int index) => surveyResponses?.GetValueOrDefault(index) ?? "No answer";
}
