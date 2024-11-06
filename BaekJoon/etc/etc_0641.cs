using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 28
이름 : 배성훈
내용 : 복수전공
    문제번호 : 12843번

    이분 매칭 문제다
    두 학부 간의 강의 중 겹치는 내용이 존재한 관계 번호가 주어지기에
    이분 매칭으로 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0641
    {

        static void Main641(string[] args)
        {

            StreamReader sr;

            int[] match;
            bool[] visit;

            int n, m;
            List<int>[] line;

            Solve();

            void Solve()
            {

                Input();

                int ret = n;
                for (int i = 1; i <= n; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i)) ret--;
                }

                Console.WriteLine(ret);
            }

            bool DFS(int _n)
            {

                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];
                    if (visit[next]) continue;
                    visit[next] = true;

                    if (match[next] == 0 || DFS(match[next]))
                    {

                        match[next] = _n;
                        return true;
                    }
                }
                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                line = new List<int>[n + 1];
                bool[] isC = new bool[n + 1];
                for (int i = 0; i < n; i++)
                {

                    int num = ReadInt();
                    int type = ReadInt();
                    isC[num] = 'c' - '0' == type;
                    line[i + 1] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    if (isC[f]) line[f].Add(b);
                    else line[b].Add(f);
                }

                match = new int[n + 1];
                visit = new bool[n + 1];
                sr.Close();
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
