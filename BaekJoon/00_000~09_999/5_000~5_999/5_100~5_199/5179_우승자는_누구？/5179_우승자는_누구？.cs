using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 27
이름 : 배성훈
내용 : 우승자는 누구?
    문제번호 : 5179번

    구현, 정렬 문제다.
    struct를 이용해 구현하면 성능과 코드가 더 깔끔할 것이다.
    그런데, 여기서는 그냥 C# 에서 제공하는 구조체을 이용했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1364
    {

        static void Main1364(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int k = ReadInt();
            int n, m, p;

            var comp = Comparer<(int[] prob, int solve, int score)>.Create((x, y) =>
            {

                if (x.solve != y.solve) return y.solve.CompareTo(x.solve);
                else return x.score.CompareTo(y.score);
            });
            
            (int[] prob, int solve, int score)[] arr;
            SetArr();
            
            for (int i = 1; i <= k; i++)
            {

                sw.Write($"Data Set {i}:\n");

                Input();

                GetRet();
            }

            void SetArr()
            {

                arr = new (int[] prob, int solve, int score)[501];

                for (int i = 0; i <= 500; i++)
                {

                    arr[i].prob = new int[26];
                }
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();
                p = ReadInt();

                for (int i = 0; i < p; i++)
                {

                    arr[i].solve = 0;
                    arr[i].score = 0;
                    for (int j = 0; j < 26; j++)
                    {

                        arr[i].prob[j] = 0;
                    }
                }
            }

            void GetRet()
            {

                int SOLVE = -1;
                for (int i = 0; i < m; i++)
                {

                    int who = ReadInt() - 1;
                    int prob = ReadInt() + '0' - 'A';
                    int time = ReadInt();
                    bool chkSolve = ReadInt() == 1;

                    if (arr[who].prob[prob] == SOLVE) continue;

                    if (chkSolve)
                    {

                        int add = time + arr[who].prob[prob] * 20;
                        arr[who].prob[prob] = SOLVE;

                        arr[who].score += add;
                        arr[who].solve++;
                    }
                    else
                        arr[who].prob[prob]++;
                }

                for (int i = 0; i < p; i++)
                {

                    arr[i].prob[0] = i + 1;
                }

                Array.Sort(arr, 0, p, comp);
                for (int i = 0; i < p; i++)
                {

                    sw.Write($"{arr[i].prob[0]} {arr[i].solve} {arr[i].score}\n");
                }

                sw.Write('\n');
            }

            int ReadInt()
            {

                int ret = 0;
                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n') return true;
                    ret = c - '0';

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
}
