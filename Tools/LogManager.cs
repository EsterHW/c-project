using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools;

public static class LogManager
{
    private static readonly string PATH = "Log";


    // ניתוב תקיית לוג
    public static string getLogDirPath()
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PATH);
    }



    // ניתוב קובץ .log

    // ניתוב תיקיית חודש - יצירה אם לא קיים 
    public static string getPathDir()
    {
       string thisDate = Path.Combine(getLogDirPath(), $"{DateTime.Now.Month:D2}.{DateTime.Now.Year}");

        if (!Directory.Exists(thisDate))
        {
            Directory.CreateDirectory(thisDate);
        }

        return thisDate;
        // return Path.Combine(AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.Month, log);

    }
public static string getPathFile()
{
    string dayFile = Path.Combine(getPathDir(), $"{DateTime.Now.Day:D2}.log");

    // אין צורך ליצור את הקובץ — StreamWriter עם append: true יוצר אותו לבד
    return dayFile;
}

    //    }7. כתבי פונקציה סטטית לכתיבה ללוג.הפונקציה תקבל כפרמטרים את המחרוזות הנדרשות(שם הפרויקט, שם הפונקציה והודעה חופשית) ותכתוב לקובץ לפי תבנית קבועה.התבנית חייבת לכלול תאריך ושעה נוכחיים וכן את כל הפרמטרים שהתקבלו בפונקציה.
    //לדוגמא: $"{DateTime.Now}\t{project}.{funcName}:\t{message}"
    //לפני הכתיבה, יש לוודא שהתיקיה והקובץ קיימים (במקרה של תחילת חודש או תחילת יום- ייתכן שלא) ואם לא- היא תיצור אותם.

    public static void WriteToLog(string nameProject, string nameFunc, string message)
    {
        try
        {
            string thisFile = getPathFile();
            using (StreamWriter writer = new StreamWriter(thisFile, true))
            {
                writer.WriteLine($"{DateTime.Now}\t{nameProject}.{nameFunc}:\t{message}");
            }
        }
        catch
        {
            // לוג לא אמור להפיל את האפליקציה
        }
    }

    // מחיקת תיקיות לוג מלפני יותר מחודשיים
    public static void deleteLogs()
    {
        string logDir = getLogDirPath();
        if (!Directory.Exists(logDir))
            return;

        foreach (string monthDir in Directory.GetDirectories(logDir))
        {
            string name = Path.GetFileName(monthDir);          // "11.2026"
            if (!DateTime.TryParseExact(name, "MM.yyyy", null,
                    System.Globalization.DateTimeStyles.None, out DateTime monthDate))
                continue;

            if (monthDate < DateTime.Now.Date.AddMonths(-2))
            {
                try
                {
                    Directory.Delete(monthDir, recursive: true);
                }
                catch
                {
                    // תיקייה תפוסה — ננסה בפעם הבאה
                }
            }
        }
    }
}


