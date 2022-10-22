namespace SimpleQuizz_Prototype_Host.Helpers;

internal static class ConsoleInputHelper
{
    public static void CheckInsertFailure(ref string input, Func<string, bool> failureCondition)
    {
        while (failureCondition(input))
        {
            Console.WriteLine("Некорректный формат, попробуйте снова");
            input = Console.ReadLine() ?? "";
        }
    }
    public static T CheckConvertionFailure<T>(string input, ConvertionDelegate<T> convertion)
    {
        T result;
        while (!convertion(input, out result))
        {
            Console.WriteLine("Некорректный формат, попробуйте снова");
            input = Console.ReadLine() ?? "";
        }
        return result;
    }
    public delegate bool ConvertionDelegate<T>(string input, out T result);
}

