using System.Collections.Generic;
using System.Linq;

namespace BeFit_TestTasks
{
    //Напишите реализацию метода, возвращающего частичные суммы ряда IEnumerable<double> GetRowSums(IEnumerable<double> row);
    //Например, для ряда 1, 2, 3, 4, ...
    //он должен вернуть 1, 3, 6, 10, ...
    //Возможность переполнения типа double при суммировании можно не учитывать. (Рекомендация: используйте LINQ).
    public static class RowSumTask
    {
        public static IEnumerable<double> GetRowSums(IEnumerable<double> row)
        {
            if (row == null || row.Count() == 0) return Enumerable.Empty<double>();

            var result = new List<double>
            {
                row.First()
            };

            var enumerator = row.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current == row.First()) continue;

                var currentRowSum = enumerator.Current + result.Last();
                result.Add(currentRowSum);
            }

            return result;
        }
    }
}
