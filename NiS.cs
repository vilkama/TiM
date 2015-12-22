using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_4
{
    class Program
    {
        public const int N = 3;
        public const int M = 7;

        static void Enter_Matr_Sost(int[,] M_S)
        {
            int i, j;
            Console.WriteLine("Матрица Состояний");
            Console.Write(" ");
            for (i = 0; i < M-1; i++)
            {
                Console.Write(" {0}  ", i+1);
            }
            Console.WriteLine();
            for (j = 0; j < N; j++)
            {
                Console.Write(j+1);
                for (i = 0; i < M; i++)
                {
                    switch (M_S[i, j])
                    {
                        case 10: Console.Write(" s  "); break;
                        case 11: Console.Write(" s' "); break;
                        case 12: Console.Write(" j  "); break;
                        case 13: Console.Write(" j' "); break;
                        case 14: Console.Write(" ow "); break;
                        case 15: Console.Write(" or "); break;
                    }
                }
                Console.WriteLine();
            }
        }

        static void Enter_Matr_Per(int[,] M_P)
        {
            int i, j;
            Console.WriteLine();
            Console.WriteLine("Матрица Перехода");
            Console.Write(" ");
            for (i = 0; i < M - 1; i++)
            {
                Console.Write(" {0}  ", i + 1);
            }
            Console.WriteLine();
            for (j = 0; j < N; j++)
            {
                Console.Write(" ");
                for (i = 0; i < M - 1; i++)
                {
                    Console.Write(" {0}  ", M_P[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void Srav(int[,] M_S, int j, int[] Mi, ref int[,] H, int flag)
        {
            for (int i = 0; i < M; i++)
            {
                for (int n = 0; n < 2; n++)
                {
                    if (M_S[i, j] == Mi[n])
                    {
                        if (flag == 0)
                            H[i, j] = 0;
                        else if (flag == 1)
                            H[i, j] = 2;
                        else
                            H[i, j] = -1;
                    }
                }

            }
        }

        static void Postr_Matr_pereh(int[,] M_S, ref int[,] M_P)
        {
            int i, j, k, h;
            int[] M1 = new int[] { 10, 11 };
            int[] M3 = new int[] { 12, 13 };
            int[] M5 = new int[] { 10, 14 };
            int[] M6 = new int[] { 12, 15 };
            int[] M_otk = new int[] { 12, 14 };
            int[] M_vos = new int[] { 10, 15 };
            int[,] H = new int[M - 1, N]{{-1,-1,-1},
                                       {-1,-1,-1},
                                       {-1,-1,-1},
                                       {-1,-1,-1},
                                       {-1,-1,-1},
                                       {-1,-1,-1}};
            int[] SUM = new int[M - 1];
            for (i = 0; i < M; i++)
            {
                for (j = 0; j < N; j++)
                {
                    if (M_S[i, j] == 10)
                    {
                        Srav(M_S, j, M1, ref H, 0);
                        Srav(M_S, j, M_otk, ref H, 1);
                    }
                    if (M_S[i, j] == 11)
                        Srav(M_S, j, M1, ref H, 0);
                    if (M_S[i, j] == 12)
                    {
                        Srav(M_S, j, M3, ref H, 0);
                        Srav(M_S, j, M_vos, ref H, 1);
                    }
                    if (M_S[i, j] == 13)
                        Srav(M_S, j, M3, ref H, 0);
                    if (M_S[i, j] == 14)
                        Srav(M_S, j, M5, ref H, 0);
                    if (M_S[i, j] == 15)
                        Srav(M_S, j, M6, ref H, 0);
                }
                for (k = 0; k < M - 1; k++)
                {
                    for (j = 0; j < N; j++)
                    {
                        SUM[k] += H[k, j];
                    }
                }
                for (k = 0; k < M - 1; k++)
                {
                    if (SUM[k] == 2)
                        for (h = 0; h < N; h++)
                        {
                            if (H[k, h] == 2)
                                M_P[k, h] = i+1;
                        }
                }
                for (k = 0; k < M - 1; k++)
                {
                    for (j = 0; j < N; j++)
                    {
                        H[k, j] = -1;
                        SUM[k] = 0;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            int[,] M_S = new int[M, N]{{10,10,10},
                                       {12,11,11},
                                       {11,12,11},
                                       {10,10,12},
                                       {12,11,13},
                                       {11,12,13},
                                       {2,3,3}};
            int[,] M_P = new int[M - 1, N];
            Enter_Matr_Sost(M_S);
            Postr_Matr_pereh(M_S, ref M_P);
            Enter_Matr_Per(M_P);
            Console.ReadKey();
        }
    }
}
