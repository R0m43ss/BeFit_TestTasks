using System;
using System.Collections.Generic;
using System.Linq;

namespace BeFit_TestTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // just in case

            InitMenu();

            return;
        }

        private static void InitMenu()
        {
            ShowMenuOptions();
            HandleInput();
        }

        private static void HandleInput()
        {
            var input = Console.ReadLine();
            int.TryParse(input, out var taskNumber);

            switch (taskNumber)
            {
                case 1:
                    // todo
                    Console.WriteLine("Under development :)");
                    break;
                case 2:
                    Console.WriteLine("Частичные суммы ряда для ряда 1, 2, 3, 4...");

                    IEnumerable<double> row = new double[] { 1, 2, 3, 4 };
                    var resultRow = RowSumTask.GetRowSums(row);

                    Console.WriteLine($"Результат: {string.Join(", ", resultRow)}");
                    break;
                case 3:
                    Console.WriteLine("Получение справок от 4-х чиновников...");
                    var user = User.System;
                    GetDocumentTask.GetAllDocuments(user);
                    Console.WriteLine($"Справки в последовательности получения: {string.Join(", ", user.ReceivedDocuments.Select(d => d.Id))}");
                    break;
                case 4:
                    Console.WriteLine("Массив: {3, 1, 8, 5, 4}");
                    var possibleSums = new[] { 10, 2 };
                    foreach (var possibleSum in possibleSums)
                    {
                        var resultCheck = ArrayItemSumTask.IsArrayItemSum(possibleSum, out var sumItems);
                        var resultString = resultCheck
                            ? $"можно представить суммой ({string.Join(" + ", sumItems)})"
                            : "нельзя";

                        Console.WriteLine($"Число {possibleSum} - {resultString}");
                    }
                    break;
                default:
                    Console.WriteLine("Выход...");
                    return;
            }

            Console.WriteLine("");
            InitMenu();
        }

        private static void ShowMenuOptions()
        {
            Console.WriteLine("Выберите тестовое задание:");
            Console.WriteLine("- Для задачи #1 (класс асинхронной обработки задач) введите 1;");
            Console.WriteLine("- Для задачи #2 (частичные суммы ряда) введите 2;");
            Console.WriteLine("- Для задачи #3 (получения справки у чиновников) введите 3;");
            Console.WriteLine("- Для задачи #4 (сумма чисел из массива) введите 4;");
            Console.WriteLine("- Для завершения работы программы введите любой другой символ.");
        }
    }
}
