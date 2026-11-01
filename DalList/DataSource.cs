using DO;

namespace Dal;


internal static class DataSource
{
   
    internal static List<DO.Student> Students = new();
    internal static List<DO.Test> Tests = new();
    internal static List<DO.Question> Questions = new();
    internal static List<DO.Answer> Answers = new();
    internal static List<DO.QuestionForStudent> QuestionsForStudent = new();

 
    internal static class Config
    {
        // Student
        // לישות Student אין מזהה רץ: השדה Id מזהה את הסטודנט עצמו ואינו נוצר בשכבת ה-DAL.
        // לכן אין עבורו שדות Min/רץ/מאפיין ב-Config.

        // Test
        /// <summary>המזהה הקטן ביותר של מבחן.</summary>
        internal const int MinTestId = 100;

        /// <summary>המספר הרץ הבא של מבחן.</summary>
        private static int s_testId = MinTestId;

        /// <summary>מחזיר את המזהה הבא של מבחן ומקדם את המספר הרץ.</summary>
        internal static int NextTestId => s_testId++;

        // Question
        /// <summary>המזהה הקטן ביותר של שאלה.</summary>
        internal const int MinQuestionId = 100;

        /// <summary>המספר הרץ הבא של שאלה.</summary>
        private static int s_questionId = MinQuestionId;

        /// <summary>מחזיר את המזהה הבא של שאלה ומקדם את המספר הרץ.</summary>
        internal static int NextQuestionId => s_questionId++;

        // Answer
        /// <summary>המזהה הקטן ביותר של תשובה.</summary>
        internal const int MinAnswerId = 100;

        /// <summary>המספר הרץ הבא של תשובה.</summary>
        private static int s_answerId = MinAnswerId;

        /// <summary>מחזיר את המזהה הבא של תשובה ומקדם את המספר הרץ.</summary>
        internal static int NextAnswerId => s_answerId++;

        // QuestionForStudent
        // לישות QuestionForStudent אין מזהה רץ: המפתח שלה מורכב מהמזהים הקיימים
        // StudentId, TestId ו-QuestionId, ולכן אין עבורה שדות ב-Config.
    }
}
