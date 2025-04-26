using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 26
이름 : 배성훈
내용 : 팀배분
    문제번호 : 1953번

    이분 그래프, DFS 문제다.
    힌트에서 불가능한 경우는 없다고 한다.
    1명의 팀을 설정하고 나눠가면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1582
    {

        static void Main1582(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();

            int[][] edge = new int[n + 1][];

            for (int i = 1; i <= n; i++)
            {

                int len = ReadInt();
                edge[i] = new int[len];
                for (int j = 0; j < len; j++)
                {

                    edge[i][j] = ReadInt();
                }
            }

            int[] ret = new int[n + 1];

            for (int i = 1; i <= n; i++)
            {

                DFS(i);
            }

            int b = 0;
            for (int i = 1; i <= n; i++)
            {

                if (ret[i] == 1) b++;
            }

            sw.Write($"{b}\n");
            for (int i = 1; i <= n; i++)
            {

                if (ret[i] == 1) sw.Write($"{i} ");
            }

            sw.Write('\n');

            sw.Write($"{n - b}\n");

            for (int i = 1; i <= n; i++)
            {

                if (ret[i] != 1) sw.Write($"{i} ");
            }

            void DFS(int _cur, int _prev = 0, int _team = 1)
            {

                if (ret[_cur] != 0) return;
                ret[_cur] = _team;

                int nTeam = _team * -1;
                for (int i = 0; i < edge[_cur].Length; i++)
                {

                    int next = edge[_cur][i];
                    if (next == _prev) continue;

                    DFS(next, _cur, nTeam);
                }
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

                    while((c =sr.Read())!= -1 && c != ' ' && c != '\n')
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
