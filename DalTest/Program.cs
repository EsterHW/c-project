using DalApi;
using DO;
using DalList = Dal.DalList;

namespace DalTest;

/// <summary>
/// תוכנית ראשית לבדיקה ידנית של שכבת הנתונים.
/// התוכנית מבצעת קלט, פלט וזימון פונקציות הממשק בלבד, ואינה בודקת את תקינות הנתונים.
/// </summary>
public class Program
{
    private static readonly IDal s_dal = new DalList();

    private static void Main(string[] args)
    {
        try
        {
            Initialization.Initialize(s_dal);
            MainMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    #region Menus

    /// <summary>
    /// מציג את התפריט הראשי וקורא את בחירת המשתמש עד לבקשה לצאת.
    /// </summary>
    private static void MainMenu()
    {
        int choice;
        do
        {
            choice = PrintMainMenu();
            switch (choice)
            {
                case 1: TestMenu(); break;
                case 2: QuestionMenu(); break;
                case 3: AnswerMenu(); break;
                case 4: StudentMenu(); break;
                case 5: QuestionForStudentMenu(); break;
                case 0: break;
                default: Console.WriteLine("Wrong choice, try again"); break;
            }
        } while (choice != 0);
    }

    /// <summary>
    /// מציג את תת-התפריט של פעולות ה-CRUD עבור המבחן ומזמן את הפעולה שנבחרה.
    /// </summary>
    private static void TestMenu()
    {
        int choice;
        do
        {
            choice = PrintSubMenu("Test");
            switch (choice)
            {
                case 1: AddTest(); break;
                case 2: Read(s_dal.Test); break;
                case 3: ReadAll(s_dal.Test); break;
                case 4: UpdateTest(); break;
                case 5: Delete(s_dal.Test); break;
                case 0: break;
                default: Console.WriteLine("Wrong choice, try again"); break;
            }
        } while (choice != 0);
    }

    /// <summary>
    /// מציג את תת-התפריט של פעולות ה-CRUD עבור השאלה ומזמן את הפעולה שנבחרה.
    /// </summary>
    private static void QuestionMenu()
    {
        int choice;
        do
        {
            choice = PrintSubMenu("Question");
            switch (choice)
            {
                case 1: AddQuestion(); break;
                case 2: Read(s_dal.Question); break;
                case 3: ReadAll(s_dal.Question); break;
                case 4: UpdateQuestion(); break;
                case 5: Delete(s_dal.Question); break;
                case 0: break;
                default: Console.WriteLine("Wrong choice, try again"); break;
            }
        } while (choice != 0);
    }

    /// <summary>
    /// מציג את תת-התפריט של פעולות ה-CRUD עבור התשובה ומזמן את הפעולה שנבחרה.
    /// </summary>
    private static void AnswerMenu()
    {
        int choice;
        do
        {
            choice = PrintSubMenu("Answer");
            switch (choice)
            {
                case 1: AddAnswer(); break;
                case 2: Read(s_dal.Answer); break;
                case 3: ReadAll(s_dal.Answer); break;
                case 4: UpdateAnswer(); break;
                case 5: Delete(s_dal.Answer); break;
                case 0: break;
                default: Console.WriteLine("Wrong choice, try again"); break;
            }
        } while (choice != 0);
    }

    /// <summary>
    /// מציג את תת-התפריט של פעולות ה-CRUD עבור התלמיד ומזמן את הפעולה שנבחרה.
    /// </summary>
    private static void StudentMenu()
    {
        int choice;
        do
        {
            choice = PrintSubMenu("Student");
            switch (choice)
            {
                case 1: AddStudent(); break;
                case 2: Read(s_dal.Student); break;
                case 3: ReadAll(s_dal.Student); break;
                case 4: UpdateStudent(); break;
                case 5: Delete(s_dal.Student); break;
                case 0: break;
                default: Console.WriteLine("Wrong choice, try again"); break;
            }
        } while (choice != 0);
    }

    /// <summary>
    /// מציג את תת-התפריט של פעולות ה-CRUD עבור השאלה לנבחן ומזמן את הפעולה שנבחרה.
    /// </summary>
    private static void QuestionForStudentMenu()
    {
        int choice;
        do
        {
            choice = PrintSubMenu("QuestionForStudent");
            switch (choice)
            {
                case 1: AddQuestionForStudent(); break;
                case 2: Read(s_dal.QuestionForStudent); break;
                case 3: ReadAll(s_dal.QuestionForStudent); break;
                case 4: UpdateQuestionForStudent(); break;
                case 5: Delete(s_dal.QuestionForStudent); break;
                case 0: break;
                default: Console.WriteLine("Wrong choice, try again"); break;
            }
        } while (choice != 0);
    }

    /// <summary>
    /// מדפיס את התפריט הראשי ומחזיר את בחירת המשתמש.
    /// </summary>
    public static int PrintMainMenu()
    {
        Console.WriteLine("Main menu - choose an entity to test:");
        Console.WriteLine("1. Tests");
        Console.WriteLine("2. Questions");
        Console.WriteLine("3. Answers");
        Console.WriteLine("4. Students");
        Console.WriteLine("5. Questions for student");
        Console.WriteLine("0. Exit");

        return ReadInt("Your choice: ");
    }

    /// <summary>
    /// מדפיס את תת-התפריט של פעולות ה-CRUD ומחזיר את בחירת המשתמש.
    /// </summary>
    /// <param name="item">שם הישות, לצורך התצוגה.</param>
    public static int PrintSubMenu(string item)
    {
        Console.WriteLine($"--- Sub menu - {item} ---");
        Console.WriteLine("1. Create");
        Console.WriteLine("2. Read");
        Console.WriteLine("3. ReadAll");
        Console.WriteLine("4. Update");
        Console.WriteLine("5. Delete");
        Console.WriteLine("0. Back to main menu");

        return ReadInt("Your choice: ");
    }

    #endregion

    #region Entity input

    private static Test AskTest(int code = 0) =>
        new(code,
            ReadSubject("Test subject: "),
            ReadDate("Test date: "),
            ReadInt("Number of questions: "));

    private static Question AskQuestion(int code = 0) =>
        new(code,
            ReadSubject("Question subject: "),
            ReadStr("Question content: "),
            ReadInt("Correct answer ID: "));

    
    private static Answer AskAnswer(int code = 0) =>
        new(code,
            ReadInt("Question ID: "),
            ReadStr("Answer content: "));


    private static Student AskStudent(int identity = 0) =>
        new(identity,
            ReadStr("Student name: "),
            ReadInt("Test ID: "),
            ReadInt("Mark: "));

    
    private static QuestionForStudent AskQuestionForStudent(int studentId = 0) =>
        new(studentId,
            ReadInt("Test ID: "),
            ReadInt("Question ID: "),
            ReadInt("Selected answer ID: "));

    #endregion

    #region CRUD operations

    private static void AddTest() =>
        Console.WriteLine($"Created with ID {s_dal.Test.Create(AskTest())}");

    /// <summary>
    /// קורא שאלה חדשה ומוסיף אותה לבסיס הנתונים.
    /// </summary>
    private static void AddQuestion() =>
        Console.WriteLine($"Created with ID {s_dal.Question.Create(AskQuestion())}");

    /// <summary>
    /// קורא תשובה חדשה ומוסיף אותה לבסיס הנתונים.
    /// </summary>
    private static void AddAnswer() =>
        Console.WriteLine($"Created with ID {s_dal.Answer.Create(AskAnswer())}");

    /// <summary>
    /// קורא תלמיד חדש ומוסיף אותו לבסיס הנתונים.
    /// </summary>
    private static void AddStudent() =>
        Console.WriteLine($"Created with ID {s_dal.Student.Create(AskStudent())}");

    /// <summary>
    /// קורא שאלה לנבחן חדשה ומוסיף אותה לבסיס הנתונים.
    /// </summary>
    private static void AddQuestionForStudent() =>
        Console.WriteLine($"Created with ID {s_dal.QuestionForStudent.Create(AskQuestionForStudent())}");

    /// <summary>
    /// קורא מבחן קיים (כולל המזהה) ומעדכן אותו.
    /// </summary>
    private static void UpdateTest() =>
        s_dal.Test.Update(AskTest(ReadInt("Test ID: ")));

    /// <summary>
    /// קורא שאלה קיימת (כולל המזהה) ומעדכן אותה.
    /// </summary>
    private static void UpdateQuestion() =>
        s_dal.Question.Update(AskQuestion(ReadInt("Question ID: ")));

    /// <summary>
    /// קורא תשובה קיימת (כולל המזהה) ומעדכנת אותה.
    /// </summary>
    private static void UpdateAnswer() =>
        s_dal.Answer.Update(AskAnswer(ReadInt("Answer ID: ")));

    /// <summary>
    /// קורא תלמיד קיים (כולל המזהה) ומעדכן אותו.
    /// </summary>
    private static void UpdateStudent() =>
        s_dal.Student.Update(AskStudent(ReadInt("Student ID: ")));

    /// <summary>
    /// קורא שאלה לנבחן קיימת (כולל המזהה של התלמיד) ומעדכן אותה.
    /// </summary>
    private static void UpdateQuestionForStudent() =>
        s_dal.QuestionForStudent.Update(AskQuestionForStudent(ReadInt("Student ID: ")));

    /// <summary>
    /// מדפיס את כל האובייקטים מטיפוס הישות שהתקבלה.
    /// </summary>
    /// <typeparam name="T">טיפוס הישות.</typeparam>
    /// <param name="crud">אובייקט הגישה של הישות.</param>
    private static void ReadAll<T>(ICrud<T> crud)
    {
        List<T> items = crud.ReadAll();
        if (items.Count == 0)
        {
            Console.WriteLine("The list is empty");
            return;
        }

        foreach (T item in items)
            Console.WriteLine(item);
    }

    /// <summary>
    /// קורא מהמשתמש מזהה ומדפיס את הישות המתאימה.
    /// </summary>
    /// <typeparam name="T">טיפוס הישות.</typeparam>
    /// <param name="crud">אובייקט הגישה של הישות.</param>
    private static void Read<T>(ICrud<T> crud)
    {
        T? item = crud.Read(ReadInt("Enter ID: "));
        Console.WriteLine(item is null ? "Not found" : item);
    }

        private static void Delete<T>(ICrud<T> crud)
    {
        crud.Delete(ReadInt("Enter ID to delete: "));
        Console.WriteLine("Deleted");
    }

    #endregion

    #region Input helpers

    private static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value))
                return value;

            Console.WriteLine("Invalid number, try again");
        }
    }

    /// <summary>
    /// קורא מהמשתמש תאריך, וחוזר על הקריאה עד שמתקבל תאריך תקין.
    /// </summary>
    /// <param name="prompt">הטקסט שיוצג לפני הקריאה.</param>
    private static DateTime ReadDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (DateTime.TryParse(Console.ReadLine(), out DateTime value))
                return value;

            Console.WriteLine("Invalid date, try again");
        }
    }

    /// <summary>
    /// קורא מהמשתמש ערך של נושא, וחוזר על הקריאה עד שמתקבל ערך קיים ב-enum.
    /// </summary>
    /// <param name="prompt">הטקסט שיוצג לפני הקריאה.</param>
    private static Subject ReadSubject(string prompt)
    {
        Console.WriteLine($"Subjects: {string.Join(", ", Enum.GetValues<Subject>())}");
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int value) && Enum.IsDefined((Subject)value))
                return (Subject)value;

            if (Enum.TryParse(input, ignoreCase: true, out Subject subject) && Enum.IsDefined(subject))
                return subject;

            Console.WriteLine("Invalid subject, try again");
        }
    }

    /// <summary>
    /// קורא מהמשתמש שורת טקסט.
    /// </summary>
    /// <param name="prompt">הטקסט שיוצג לפני הקריאה.</param>
    private static string ReadStr(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    #endregion
}
