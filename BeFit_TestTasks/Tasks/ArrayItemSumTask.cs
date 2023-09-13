using System.Collections.Generic;
using System.Linq;

namespace BeFit_TestTasks
{
    //Есть массив из целых чисел.Написать функцию, которая определяет можно ли заданное число представить суммой чисел из массива(каждое число можно использовать один раз):
    //Пример:
    //Массив: {3, 1, 8, 5, 4}
    //Число 10  -  можно представить суммой(1 + 5 + 4)
    //Число 2 - нельзя
    public static class ArrayItemSumTask
    {
        private static readonly int [] _items = new [] { 3, 1, 8, 5, 4 };

        public static bool IsArrayItemSum(int sum, out List<int> result)
        {
            result = new List<int>();

            var filtered = (from item in _items where item <= sum select item).ToArray();
            if (filtered.Count() == 0) return false;

            for (var i = 0; i < filtered.Length; i++)
            {
                result.Clear();
                result.Add(filtered[i]);

                if (result.Sum() == sum) return true;

                var index = i + 1;
                while (index < filtered.Length)
                {
                    result.Clear();
                    result.Add(filtered[i]);
                    for (var j = index; j < filtered.Length; j++)
                    {
                        if (result.Sum() + filtered[j] > sum)
                        {
                            continue;
                        }

                        result.Add(filtered[j]);
                        if (result.Sum() == sum) return true;
                    }
                    index++;
                }
            }

            return false;
        }
    }
}
