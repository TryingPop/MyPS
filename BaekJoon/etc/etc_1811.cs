using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 7
이름 : 배성훈
내용 : 꽁꽁 얼어붙은 트리
    문제번호 : 34011번

    수학, 트리 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1811
    {

        static void Main1811(string[] args)
        {

            int n;
            List<int>[] edge;

            Input();

            GetRet();

            void GetRet()
            {

                int[] dep = new int[n + 1];
                int maxDep = 0;

                DFS(1, 0);

                bool[] notPrime = new bool[maxDep + 1];

                int ret = 1;
                for (int i = 2; i <= maxDep; i++)
                {

                    if (notPrime[i]) continue;
                    int cur = dep[0] + dep[i];
                    for (int j = i << 1; j <= maxDep; j += i)
                    {

                        cur += dep[j];
                        notPrime[j] = true;
                    }
                    ret = Math.Max(ret, cur);
                }

                Console.Write(ret);

                // 깊이 찾기
                void DFS(int _cur, int _dep)
                {

                    dep[_dep]++;
                    maxDep = Math.Max(maxDep, _dep);
                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        DFS(next, _dep + 1);
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                edge = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int cur = 2; cur <= n; cur++)
                {

                    int p = ReadInt();
                    edge[p].Add(cur);
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;

                        ret = c - '0';
                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
}
