using System.Numerics;

namespace ConsoleApp7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int n = 3;
            double[,] a, obr=new double[n,n];
            double[] x;
            double d;
            //a = new double[n, n] {
            //    { 5.526, 0.305, 0.887, 0.037 },
            //    { 0.658, 2.453, 0.678, 0.192 },
            //    { 0.398, 0.232, 4.957, 0.567 },
            //    { 0.081, 0.521, 0.192, 4.988 }
            //};
            //x = new double[n] { .774, 0.245, 0.343, 0.263 };

            //a = new double[n, n] {
            //    { 1, 2},
            //    { 3, 4},
            //};
            //x = new double[n] { 36, 47 };
            //double[,] tmm = new double[n-1, n-1];
            ////Console.WriteLine(opr(a, n));
            //tmm=ok(a, n - 1, 0, 0);
            //print(tmm, n - 1);
            //a = new double[n, n] {
            //    { 2, 1, 4 },
            //    { 3, 2, 1 },
            //    { 1, 3, 3 },
            //};
            //x = new double[n] { 16, 10, 16 };

            a = new double[n, n] {
                { 2, 5, 7 },
                { 6, 3, 4 },
                { 5, -2, -3 },
            };
            d = opr(a, n);
            //Console.WriteLine(opr(a, n));
            if(d!=0)
            {
                for(int i=0; i<n;i++)
                {
                    for(int j=0; j<n;j++)
                    {
                        int m = n - 1;
                        double[,]tmp=new double[m,m];
                        ok(tmp, n, i, j);
                        obr[i, j] = Math.Pow(-1.0, i + j + 2) * opr(tmp, m) / d;
                    }
                }
            }

            //ok(a, n - 1, 0, 0);
            //for (int i=0;i<n;i++)
            //{
            //    for(int j=0;j<n;j++)
            //    {
            //        print(ok(a, n, i, j), n - 1);
            //        Console.WriteLine();
            //    }
            //    Console.WriteLine();
            //}
            //print(ok(a, n , 1, 1), n-1);


            //gauss(a, x, n);
            //for (int i = 0; i < n; i++)
            //    Console.WriteLine(Math.Round(x[i], 4));
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
        static void print(double[,] a, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write($"{Math.Round(a[i, j], 3)} ");
                Console.WriteLine();
            }
        }
        static double opr(double[,] a, int n)
        {
            double temp = 0;
            int k = 1;
            if (n == 1)
                temp = a[0,0];
            if (n == 2)
                temp=a[0, 0] * a[1, 1] - a[1, 0] * a[0, 1];
            else
            { 
                for (int i = 0; i < n; i++)
                {
                    int m = n - 1;
                    double[,] tmp = ok(a, n, 0, i);
                    //Console.WriteLine($"{tmp.Length}, {m}");
                    temp = temp + k * a[0, i] * opr(tmp, m);
                    k *= -1;
                    //print(tmp, m);
                }
            }
            return temp;
        }
        static double[,] ok(double[,]a, int n, int x, int y)
        {
            double[,] tmp=new double[n-1,n-1];
            int ki=0, kj;
            for(int i=0;i<n;i++)
            {
                if(i!=x)
                {
                    kj = 0;
                    for(int j=0;j<n;j++)
                    {
                        if(j!=y)
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
        }
        static void gauss(double[,] a, double[] x, int n)
        {
            int imax;
            double amax, c;
            print(a, x, n);

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
                print(a, x, n);
                for (int i = k; i < n; i++)
                {
                    c = 1 / (a[i, k]);
                    for (int j = 0; j < n; j++)
                        a[i, j] *= c;
                    x[i] *= c;
                }
                print(a, x, n);
                for (int i = k + 1; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        a[i, j] -= a[k, j];
                    }
                    x[i] -= x[k];
                }
                print(a, x, n);
                Console.WriteLine("===========");
            }
            for (int i = n - 2; i >= 0; i--)
            {
                for (int j = i + 1; j < n; j++)
                    x[i] -= a[i, j] * x[j];
            }

        }
    }

}
