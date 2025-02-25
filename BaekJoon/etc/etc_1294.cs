using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 24
이름 : 배성훈
내용 : 초차원전쟁 이나
    문제번호 : 11579번

    선형대수학 문제다.
    먼저 이동으로 주어지는 벡터를 보면 선형 공간을 만드는 기저(base) 벡터다.
    그리고 선대에서 배웠듯이 맨 앞자리가 1이 먼저나오는 순으로 정렬하면 된다.

    효율적으로 한다면, 인덱스와 시작위치를 따로 배열에 저장해 풀면 되지만,
    차원의 크기가 500을 넘지 않으므로 일일히 비교하면서 정렬했다.

    이후 정렬하고 그리디로(가우스 소거법) 앞에서부터 하나씩 맞춰가면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1294
    {

        static void Main1294(string[] args)
        {

            string NO = "0\n";
            string YES = "1 ";

            int MAX_N = 500;

            StreamReader sr;
            StreamWriter sw;
            int n;
            long[][] arr;
            long[] cur, dst;
            Solve();
            void Solve()
            {

                Init();

                int t = ReadInt();

                while (t-- > 0)
                {

                    Input();

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void Input()
            {

                n = ReadInt();
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        arr[i][j] = ReadInt();
                    }
                }

                for (int i = 0; i < n; i++)
                {

                    dst[i] = ReadInt();
                }

                Array.Fill(cur, 0, 0, n);
                Array.Sort(arr, (x, y) =>
                {

                    for (int i = 0; i < n; i++)
                    {

                        if (x[i] == y[i]) continue;

                        return x[i] == 1 ? -1 : 1;
                    }

                    return 0;
                });
            }

            void GetRet()
            {

                long move = 0;
                for (int i = 0; i < n; i++)
                {

                    int s = -1;
                    for (int j = 0; j < n; j++)
                    {

                        if (arr[i][j] == 0) continue;
                        s = j;
                        break;
                    }

                    if (Add(i, s)) continue;

                    sw.Write(NO);
                    return;
                }

                sw.Write($"{YES}{move}\n");

                bool Add(int _idx, int _s)
                {

                    long mul = dst[_s] - cur[_s];
                    move += mul;
                    if (mul == 0) return true;
                    else if (mul < 0) return false;

                    for (int j = _s; j < n; j++)
                    {

                        cur[j] += mul * arr[_idx][j];
                    }

                    return true;
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                arr = new long[MAX_N][];
                for (int i = 0; i < MAX_N; i++)
                {

                    arr[i] = new long[MAX_N];
                }

                dst = new long[MAX_N];
                cur = new long[MAX_N];
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

                    bool positive = c != '-';
                    ret = positive ? c - '0' : 0;
                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;
                    return false;
                }
            }
        }
    }
}
