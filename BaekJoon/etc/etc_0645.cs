using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 28
이름 : 배성훈
내용 : N과 M (12)
    문제번호 : 15666번

    백트래킹 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0645
    {

        static void Main645(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n, m;
            int[] arr;
            int[] calc;
            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput());
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();

                Array.Sort(arr);

                int before = -1;

                for (int i = 0; i < n; i++)
                {

                    if(before != arr[i])
                    {

                        before = arr[i];
                        continue;
                    }

                    for (int j = i + 1; j < n; j++)
                    {

                        arr[j - 1] = arr[j];
                    }
                    n--;
                    i--;
                }

                calc = new int[m];
                DFS(0, 0);
                sw.Close();
            }

            void DFS(int _depth, int _s)
            {

                if (_depth == m)
                {

                    for (int i = 0; i < m; i++)
                    {

                        sw.Write($"{calc[i]} ");
                        
                    }

                    sw.Write('\n');
                    return;
                }

                for (int i = _s; i < n; i++)
                {

                    calc[_depth] = arr[i];
                    DFS(_depth + 1, i);
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
