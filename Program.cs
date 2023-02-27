namespace ConsoleApp7
{
    internal class Program
    {
        const int n = 4;
        static void Main(string[] args)
        {
            //const int n = 4;
            const double pogr = 0.001;
            double[,] a, obr = new double[n, n];
            double[] x;
            //double[] c=new double[n];
            double d, b, x1 = -1, tst = 0, kkk;
            a = new double[n, n] {
                { 5.526, 0.305, 0.887, 0.037 },
                { 0.658, 2.453, 0.678, 0.192 },
                { 0.398, 0.232, 4.957, 0.567 },
                { 0.081, 0.521, 0.192, 4.988 }
            };
            x = new double[n] { 0.774, 0.245, 0.343, 0.263 };
            Console.WriteLine("Решение методом Гаусса: ");
            gauss(a, x);
            a = new double[n, n] {
                { 5.526, 0.305, 0.887, 0.037 },
                { 0.658, 2.453, 0.678, 0.192 },
                { 0.398, 0.232, 4.957, 0.567 },
                { 0.081, 0.521, 0.192, 4.988 }
            };
            x = new double[n] { 0.774, 0.245, 0.343, 0.263 };
            Console.WriteLine();
            Console.WriteLine("Нахождение обратной матрицы: ");
            d = det(a, n);
            if (d != 0)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        int m = n - 1;
                        double[,] tmp = cut(a, n, i, j);
                        obr[i, j] = Math.Pow(-1.0, i + j) * det(tmp, m) / d;
                    }
                }
            }
            obr = trans(obr);
            print(obr);
            for (int i = 0; i < n; i++)
                x1 = Math.Max(x1, x[i]);
            double A = norm(a);
            double A1 = norm(obr);
            Console.WriteLine();
            Console.WriteLine("Абсолютная погрешность: " + Math.Round(A1, 1, MidpointRounding.ToPositiveInfinity) * pogr);
            Console.WriteLine("Относительная погрешность: " + Math.Round(A * A1 * pogr / x1, 4, MidpointRounding.ToPositiveInfinity));
            x1 = 0;
            for (int i = 0; i < n; i++)
            {
                double k = a[i, i];
                for (int j = 0; j < n; j++)
                    a[i, j] /= -k;
                x[i] /= k;
                a[i, i] = 0;
            }//преобразование системы к виду для МПИ
            for (int i = 0; i < n; i++)
                x1 = Math.Max(x1, x[i]);
            Console.WriteLine();
            Console.WriteLine("Преобразование системы для МПИ:");
            print(a);
            b = normb(a);//вычисляем ||b||
            tst = ((1 - b) / x1) * 0.01;
            kkk = Math.Ceiling(Math.Log(tst) / Math.Log(b));//считаем количество итераций
            Console.WriteLine("Необходимое количество итераций: " + Math.Round(Math.Log(tst) / Math.Log(b), 4));
            Console.WriteLine();
            double[] c = multiply(a, x);
            double[] prev = c;
            for (int k = 1; k < kkk; k++)
            {
                for (int i = 0; i < n; i++)
                    c[i] += x[i];
                prev = c;
                Console.WriteLine($"Итерация №{k + 1}: ");
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine(Math.Round(c[i], 4));
                }
                c = multiply(a, c);
            }//итерации
            for (int i = 0; i < n; i++) c[i] += x[i];
            Console.WriteLine("Уточненная оценка погрешности: " + b / (1 - b) * (sm(c) - sm(prev)));
            Console.ReadKey();
        }
        static double sm(double[] x)
        {
            double ok = 0;
            for (int i = 0; i < n; i++)
                ok += Math.Abs(x[i]);
            return ok;
        }
        static double norm(double[,] a)
        {
            double max = 0;
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                    sum += a[i, j];
                max = Math.Max(sum, max);
            }
            return max;
        }
        static double normb(double[,] a)
        {
            double max = 0;
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                    sum += Math.Abs(a[i, j]);
                max = Math.Max(sum, max);
            }
            return max;
        }
        static double[,] trans(double[,] a)
        {
            double[,] tmp = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    tmp[j, i] = a[i, j];
                }
            }
            return tmp;
        }
        static void print(double[,] a, double[] x)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write($"{Math.Round(a[i, j], 4)} ");
                Console.Write($" | {Math.Round(x[i], 4)}");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static void print(double[,] a)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write($"{Math.Round(a[i, j], 4)}  ");
                Console.WriteLine();
            }
        }
        static double det(double[,] a, int n)
        {
            double temp = 0;
            int k = 1;
            if (n == 1)
                temp = a[0, 0];
            if (n == 2)
                temp = a[0, 0] * a[1, 1] - a[1, 0] * a[0, 1];
            else
            {
                for (int i = 0; i < n; i++)
                {
                    int m = n - 1;
                    double[,] tmp = cut(a, n, 0, i);
                    //Console.WriteLine($"{tmp.Length}, {m}");
                    temp = temp + k * a[0, i] * det(tmp, m);
                    k *= -1;
                    //print(tmp, m);
                }
            }
            return temp;
        } //определитель
        static double[,] cut(double[,] a, int n, int x, int y)
        {
            double[,] tmp = new double[n - 1, n - 1];
            int ki = 0, kj;
            for (int i = 0; i < n; i++)
            {
                if (i != x)
                {
                    kj = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (j != y)
                        {
                            tmp[ki, kj] = a[i, j];
                            //Console.WriteLine(tmp[ki, kj]);
                            kj++;
                        }
                    }
                    ki++;
                }
            }

            return tmp;
        } //обрезать матрицу
        static void gauss(double[,] a, double[] x)
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
            for (int i = 0; i < n; i++)
                Console.WriteLine($"x[{i}]={Math.Round(x[i], 4)}");
        }
        static double[] multiply(double[,] a, double[] b)
        {
            double[] x = new double[n];
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sum += a[i, j] * b[j];
                }
                x[i] = sum;
                //Console.WriteLine(sum);
                sum = 0;
            }
            //Console.WriteLine();
            return x;
        }
    }

}
