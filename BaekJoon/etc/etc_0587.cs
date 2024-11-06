using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 21
이름 : 배성훈
내용 : 한조 대기 중
    문제번호 : 14433번

    이분 매칭 문제다
    아군 트롤픽으로 이분 매칭 1번 돌리고,
    상대 트롤픽으로 이분 매칭 1번 돌려
    비교해 결과를 도출하면 된다
*/
namespace BaekJoon.etc
{
    internal class etc_0587
    {

        static void Main587(string[] args)
        {

            string YES = "네 다음 힐딱이";
            string NO = "그만 알아보자";

            StreamReader sr;
            int n;
            int m;

            List<int>[] pick1;
            List<int>[] pick2;

            int[] match;
            bool[] visit;

            Solve();

            void Solve()
            {

                Input();

                int ret = 0;
                for (int i = 1; i <= n; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i, pick1)) ret--;
                }

                Array.Fill(match, 0);
                for (int i = 1; i <= n; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i, pick2)) ret++;
                }

                Console.WriteLine(ret > 0 ? YES : NO);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 8);

                n = ReadInt();
                int m = ReadInt();

                int len1 = ReadInt();
                int len2 = ReadInt();

                pick1 = new List<int>[n + 1];
                pick2 = new List<int>[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    pick1[i] = new();
                    pick2[i] = new();
                }

                visit = new bool[m + 1];
                match = new int[m + 1];

                for (int i = 0; i < len1; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    pick1[f].Add(b);
                }

                for (int i = 0; i < len2; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    pick2[f].Add(b);
                }

                sr.Close();
            }

            bool DFS(int _n, List<int>[] _pick)
            {

                for (int i = 0; i < _pick[_n].Count; i++)
                {

                    int next = _pick[_n][i];
                    if (visit[next]) continue;
                    visit[next] = true;

                    if (match[next] == 0 || DFS(match[next], _pick))
                    {

                        match[next] = _n;
                        return true;
                    }
                }
                return false;
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
