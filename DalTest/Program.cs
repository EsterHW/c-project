using DalApi;
using DO;
using DalList = Dal.DalList;

namespace DalTest;


internal class Program
{
    private static readonly IDal s_dal = new DalList();

 
    private static void Main()
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

    private static void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("Main menu - choose an entity to test:");
            Console.WriteLine("1. Tests");
            Console.WriteLine("2. Questions");
            Console.WriteLine("3. Answers");
            Console.WriteLine("4. Students");
            Console.WriteLine("5. Questions for student");
            Console.WriteLine("0. Exit");

            int choice = ReadInt("Your choice: ");
            switch (choice)
            {
                case 1: EntityMenu(s_dal.Test, "Test"); break;
                case 2: EntityMenu(s_dal.Question, "Question"); break;
                case 3: EntityMenu(s_dal.Answer, "Answer"); break;
                case 4: EntityMenu(s_dal.Student, "Student"); break;
                case 5: EntityMenu(s_dal.QuestionForStudent, "QuestionForStudent"); break;
                case 0: return;
                default: Console.WriteLine("Wrong choice, try again"); break;
            }
        }
    }

    /// <summary>
    /// מציג את תת-התפריט של פעולות ה-CRUD עבור ישות אחת ומזמן את הפעולה שנבחרה.
    /// </summary>
    /// <typeparam name="T">טיפוס הישות הנבדקת.</typeparam>
    /// <param name="crud">אובייקט הגישה של הישות.</param>
    /// <param name="entityName">שם הישות, לצורך התצוגה.</param>
    private static void EntityMenu<T>(ICrud<T> crud, string entityName)
    {
        while (true)
        {
            Console.WriteLine($"Sub menu - {entityName}:");
            Console.WriteLine("1. Create");
            Console.WriteLine("2. Read");
            Console.WriteLine("3. ReadAll");
            Console.WriteLine("4. Update");
            Console.WriteLine("5. Delete");
            Console.WriteLine("0. Back to main menu");

            int choice = ReadInt("Your choice: ");
            switch (choice)
            {
                case 1: Create(crud, entityName); break;
                case 2: Read(crud, entityName); break;
                case 3: ReadAll(crud); break;
                case 4: Update(crud, entityName); break;
                case 5: Delete(crud); break;
                case 0: return;
                default: Console.WriteLine("Wrong choice, try again"); break;
            }
        }
    }

    #endregion

    #region CRUD operations

    /// <summary>
    /// קורא מהמשתמש את ערכי ישות חדשה ומוסיף אותה לבסיס הנתונים.
    /// </summary>
    /// <typeparam name="T">טיפוס הישות.</typeparam>
    /// <param name="crud">אובייקט הגישה של הישות.</param>
    /// <param name="entityName">שם הישות, שקובע אילו שדות לקרוא.</param>
    private static void Create<T>(ICrud<T> crud, string entityName)
    {
        T? item = entityName switch
        {
            "Test" => (T)(object)ReadTest(),
            "Question" => (T)(object)ReadQuestion(),
            "Answer" => (T)(object)ReadAnswer(),
            "Student" => (T)(object)ReadStudent(),
            _ => (T)(object)ReadQuestionForStudent()
        };

        int id = crud.Create(item);
        Console.WriteLine($"Created with ID {id}");
    }

    /// <summary>
    /// קורא מהמשתמש מזהה ומדפיס את הישות המתאימה.
    /// </summary>
    /// <typeparam name="T">טיפוס הישות.</typeparam>
    /// <param name="crud">אובייקט הגישה של הישות.</param>
    /// <param name="entityName">שם הישות, לצורך הנחיית הקלט.</param>
    private static void Read<T>(ICrud<T> crud, string entityName)
    {
        int id = ReadInt($"Enter {entityName} ID: ");
        T? item = crud.Read(id);
        Console.WriteLine(item is null ? "Not found" : item);
    }

    /// <summary>
    /// מדפיס את כל האובייקטים מטיפוס הישות שהתקבלה.
    /// </summary>
    /// <typeparam name="T">טיפוס הישות.</typeparam>
    /// <param name="crud">אובייקט הגישה של הישות.</param>
    private static void ReadAll<T>(ICrud<T> crud)
    {
        List<T> items = crud.ReadAll();
        if (items.Count == 0)
            Console.WriteLine("The list is empty");

        foreach (T item in items)
            Console.WriteLine(item);
    }

    /// <summary>
    /// קורא מהמשתמש את כל ערכי ישות קיימת (כולל המזהה שלה) ומעדכן אותה.
    /// </summary>
    /// <typeparam name="T">טיפוס הישות.</typeparam>
    /// <param name="crud">אובייקט הגישה של הישות.</param>
    /// <param name="entityName">שם הישות, שקובע אילו שדות לקרוא.</param>
    private static void Update<T>(ICrud<T> crud, string entityName)
    {
        T? item = entityName switch
        {
            "Test" => (T)(object)ReadTest(),
            "Question" => (T)(object)ReadQuestion(),
            "Answer" => (T)(object)ReadAnswer(),
            "Student" => (T)(object)ReadStudent(),
            _ => (T)(object)ReadQuestionForStudent()
        };

        crud.Update(item);
        Console.WriteLine("Updated");
    }

    /// <summary>
    /// קורא מהמשתמש מזהה ומוחק את הישות המתאימה.
    /// </summary>
    /// <typeparam name="T">טיפוס הישות.</typeparam>
    /// <param name="crud">אובייקט הגישה של הישות.</param>
    private static void Delete<T>(ICrud<T> crud)
    {
        int id = ReadInt("Enter ID to delete: ");
        crud.Delete(id);
        Console.WriteLine("Deleted");
    }

    #endregion

    #region Entity input


    private static Test ReadTest() =>
        new(ReadInt("Test ID: "),
            (Subject)ReadInt($"Subject (0-{Enum.GetValues<Subject>().Length - 1}): "),
            ReadDate("Test date: "),
            ReadInt("Number of questions: "));


    private static Question ReadQuestion() =>
        new(ReadInt("Question ID: "),
            (Subject)ReadInt($"Subject (0-{Enum.GetValues<Subject>().Length - 1}): "),
            ReadLine("Question content: "),
            ReadInt("Correct answer ID: "));

    private static Answer ReadAnswer() =>
        new(ReadInt("Answer ID: "),
            ReadInt("Question ID: "),
            ReadLine("Answer content: "));

    
    private static Student ReadStudent() =>
        new(ReadInt("Student ID: "),
            ReadLine("Student name: "),
            ReadInt("Test ID: "),
            ReadInt("Mark: "));

    /// <summary>
    /// קורא מהמשתמש את כל השדות של שאלה לנבחן.
    /// </summary>
    private static QuestionForStudent ReadQuestionForStudent() =>
        new(ReadInt("Student ID: "),
            ReadInt("Test ID: "),
            ReadInt("Question ID: "),
            ReadInt("Selected answer ID: "));

    #endregion

    #region Input helpers

    /// <summary>
    /// קורא מהמשתמש מספר שלם, וחוזר על הקריאה עד שמתקבל מספר תקין.
    /// </summary>
    /// <param name="prompt">הטקסט שיוצג לפני הקריאה.</param>
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
    /// קורא מהמשתמש שורת טקסט.
    /// </summary>
    /// <param name="prompt">הטקסט שיוצג לפני הקריאה.</param>
    private static string ReadLine(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    #endregion
}
