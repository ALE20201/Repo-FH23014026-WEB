using System;

namespace PP1
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine();

            // SumFor
            Console.WriteLine("• SumFor:");
            var (nAscFor, sumAscFor) = Finder.FindLastAscending(SumAlgorithms.SumFor);
            Console.WriteLine($"\t◦ From 1 to Max → n: {nAscFor} → sum: {sumAscFor}");
            var (nDescFor, sumDescFor) = Finder.FindFirstDescending(SumAlgorithms.SumFor);
            Console.WriteLine($"\t◦ From Max to 1 → n: {nDescFor} → sum: {sumDescFor}");

            Console.WriteLine();

            // SumIte
            Console.WriteLine("• SumIte:");

            // Ascendente
            SumAlgorithms.ResetIteCache();
            var (nAscIte, sumAscIte) = Finder.FindLastAscending(SumAlgorithms.SumIte);
            Console.WriteLine($"\t◦ From 1 to Max → n: {nAscIte} → sum: {sumAscIte}");

            // Descendente
            SumAlgorithms.InitializeIteCache(int.MaxValue, SumAlgorithms.SumFor(int.MaxValue));
            var (nDescIte, sumDescIte) = Finder.FindFirstDescending(SumAlgorithms.SumIte);
            Console.WriteLine($"\t◦ From Max to 1 → n: {nDescIte} → sum: {sumDescIte}");
        }
    }

    static class Finder
    {
        // Último n ascendente (1..Max) con sum > 0
        public static (int n, int sum) FindLastAscending(Func<int, int> sumMethod)
        {
            int lastN = 0, lastSum = 0;
            for (int n = 1; ; n++)
            {
                int s = sumMethod(n);
                if (s > 0)
                {
                    lastN = n;
                    lastSum = s;
                }
                else
                {
                    break; // el primer sum <= 0
                }

                if (n == int.MaxValue) break;
            }
            return (lastN, lastSum);
        }
        // Encuentra el primer n descendente (Max..1) tal que sumMethod(n) > 0
        public static (int n, int sum) FindFirstDescending(Func<int, int> sumMethod)
        {

            int low = 1;
            int high = int.MaxValue;
            int resultN = 0;
            int resultSum = 0;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                int s = sumMethod(mid);
                if (s > 0)
                {
                    resultN = mid;
                    resultSum = s;
                    // buscamos un n menor que siga siendo positivo
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }

            return (resultN, resultSum);
        }
    }

    static class SumAlgorithms
    {
        // Cache para SumIte (para poder elevar o disminuir n sin recalcular desde 1 cada vez)
        private static int iteCacheN = 0;
        private static int iteCacheSum = 0;

        // Reinicia el cache para empezar desde 0
        public static void ResetIteCache()
        {
            iteCacheN = 0;
            iteCacheSum = 0;
        }

        // inicia el cache en un valor arbitrario (busqueda descendente)
        public static void InitializeIteCache(int n, int sum)
        {
            iteCacheN = n;
            iteCacheSum = sum;
        }

        // SumFor: formula (con unchecked para permitir overflow
        public static int SumFor(int n)
        {
            unchecked
            {
                return n * (n + 1) / 2;
            }
        }


        public static int SumIte(int n)
        {
            unchecked
            {
                if (n >= iteCacheN)
                {
                    // incrementa cache hasta n
                    for (int i = iteCacheN + 1; i <= n; i++)
                    {
                        iteCacheSum += i;
                    }
                    iteCacheN = n;
                    return iteCacheSum;
                }
                else
                {
                    // disminuye cache desde iteCacheN hasta n 
                    for (int i = iteCacheN; i > n; i--)
                    {
                        iteCacheSum -= i;
                    }
                    iteCacheN = n;
                    return iteCacheSum;
                }
            }
        }
    }
}
