using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 29
이름 : 배성훈
내용 : 도피
    문제번호 : 24466번

    수학, dp, 확률론 문제다
    모든 간선을 통해 확률을 이전 시킬 경우
    경우의 수를 세어보니 9일 x (M + N) <= 100_000 이 나온다
    그래서 시뮬레이션 돌려 해결했다
    아이디어는 다음과 같다
    우선 시작 자리에 10^18을 채운다
    이후 확률만큼 이동시킨다
    여기서 먼저 확률을 곱하면 오버플로우가 발생하고,
    바로 100을 나누면 100 이하의 수가 문제를 일으키기에 
    100 이하 부분을 살리고 나눠줬다
    분배법칙을 이용해 100 이하 부분과 나머지 부분을 나눠 연산을 하고 더해서
    19자리 이하인 확률을 내림 연산이 되게 했다
    다만 출력 부분에서 생각없이 제출하다가 1번 틀렸다
*/
namespace BaekJoon.etc
{
    internal class etc_1086
    {

        static void Main1086(string[] args)
        {

            StreamReader sr;
            int n, m;
            long[][] city;
            (int f, int t, int w)[] edge;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                city[0][0] = 1_000_000_000_000_000_000;
                for (int day = 0; day < 9; day++)
                {

                    for (int i = 0; i < m; i++)
                    {

                        city[1][edge[i].t] += Calc(city[0][edge[i].f], edge[i].w);
                    }

                    for (int i = 0; i < n; i++)
                    {

                        city[0][i] = city[1][i];
                        city[1][i] = 0;
                    }
                }

                long max = 0;
                for (int i = 0; i < n; i++)
                {

                    max = Math.Max(max, city[0][i]);
                }

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    for (int i = 0; i < n; i++)
                    {

                        if (city[0][i] != max) continue;
                        sw.Write($"{i} ");
                    }

                    string str = max.ToString("D19");
                    sw.Write($"\n{str[0]}.{str.Substring(1)}");
                }

                long Calc(long _val, long _w)
                {

                    long f = _val / 100;
                    long b = _val % 100;

                    return f * _w + (b * _w) / 100;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
                city = new long[2][];
                city[0] = new long[n];
                city[1] = new long[n];
                edge = new (int f, int t, int w)[m];

                for (int i = 0; i < m; i++)
                {

                    edge[i] = (ReadInt(), ReadInt(), ReadInt());
                }

                sr.Close();

                int ReadInt()
                {

                    int c, ret = 0;
                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return ret;
                }
            }
        }
    }
}
