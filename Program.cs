namespace ConsoleApp7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int n = 3;
            double[,] a;
            double[] x;
            //a = new double[n, n] {
            //    { 1, 2, 3, 4 },
            //    { 7, 2, 5, 1 },
            //    { 3, 8, 1, 6 },
            //    { 4, 3, 7, 2 } 
            //};
            //x = new double[n] { 30, 30, 46, 39};
            //a = new double[n, n] {
            //    { 2, 4, 1 },
            //    { 5, 2, 1 },
            //    { 2, 3, 4 },
            //};
            //x = new double[n] { 36, 47, 37 };
            a = new double[n, n] {
                { 2, 1, 4 },
                { 3, 2, 1 },
                { 1, 3, 3 },
            };
            x = new double[n] { 16, 10, 16 };
            gauss(a, x, n);
            for (int i = 0; i < n; i++)
                Console.WriteLine(Math.Round(x[i]));
        }
        static void print(double[,] a, double[] x, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write($"{Math.Round(a[i, j], 3)} ");
                Console.Write($" | {Math.Round(x[i], 3)}");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static void gauss(double[,] a, double[] x, int n)
        {
            int imax;
            double amax, c;
            //print(a, x, n);

            for (int k = 0; k < n; k++)
            {
                imax = k;
                amax = Math.Abs(a[k, k]);
                for (int i = k + 1; i < n; i++)
                {
                    if (Math.Abs(a[i, k]) > amax)
                    {
                        amax = Math.Abs(a[i, k]);
                        imax = i;
                    }
                }
                if (k != imax)
                {
                    for (int j = k; j < n; j++)
                    {
                        c = a[k, j];
                        a[k, j] = a[imax, j];
                        a[imax, j] = c;
                    }
                    c = x[k];
                    x[k] = x[imax];
                    x[imax] = c;
                }
                //print(a, x, n);
                for (int i = k; i < n; i++)
                {
                    c = 1 / (a[i, k]);
                    for (int j = 0; j < n; j++)
                        a[i, j] *= c;
                    x[i] *= c;
                }
                //print(a, x, n);
                for (int i = k + 1; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        a[i, j] -= a[k, j];
                    }
                    x[i] -= x[k];
                }
                //print(a, x, n);
                //Console.WriteLine("===========");
            }
            for (int i = n - 2; i >= 0; i--)
            {
                for (int j = i + 1; j < n; j++)
                    x[i] -= a[i, j] * x[j];
            }

        }
    }

}
