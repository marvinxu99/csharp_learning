// Express your design intent more clearly with nullable and non-nullable reference types
// https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/nullable-reference-types

namespace NullableRefTypes;

internal class Program
{
    static void Main()
    {
        var surveyRun = new SurveyRun();

        surveyRun.AddQuestion(QuestionType.YesNo, "Has your code ever thrown a NullReferenceException?");
        surveyRun.AddQuestion(new SurveyQuestion(QuestionType.Number, "How many times (to the nearest 100) has that happened?"));
        surveyRun.AddQuestion(QuestionType.Text, "What is your favorite color?");

        surveyRun.PerformSurvey(50);

        foreach (var participant in surveyRun.AllParticipants)
        {
            Console.WriteLine($"Participant: {participant.Id}:");
            if (participant.AnsweredSurvey)
            {
                for (int i = 0; i < surveyRun.Questions.Count; i++)
                {
                    var answer = participant.Answer(i);
                    Console.WriteLine($"\t{surveyRun.GetQuestion(i).QuestionText} : {answer}");
                }
            }
            else
            {
                Console.WriteLine("\tNo responses");
            }
        }

    }
}
