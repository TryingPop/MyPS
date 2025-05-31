using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 31
이름 : 배성훈
내용 : Cow Contest
    문제번호 : 6156번

    플로이드 워셜 문제다.
    이기는 사람의 수와 지는 사람의 수의 합이 n - 1인 경우 자신의 등수를 확정지을 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1662
    {

        static void Main1662(string[] args)
        {

            int n;
            bool[][] w, l;

            Input();

            GetRet();

            void GetRet()
            {

                FW(w);
                FW(l);

                int ret = 0;
                for (int i = 1; i <= n; i++)
                {

                    int cnt = 0;
                    for (int j = 1; j <= n; j++)
                    {

                        if (w[i][j] || l[i][j]) cnt++;
                    }

                    if (cnt == n - 1) ret++;
                }

                Console.Write(ret);

                void FW(bool[][] _arr)
                {

                    for (int mid = 1; mid <= n; mid++)
                    {

                        for (int start = 1; start <= n; start++)
                        {

                            if (!_arr[start][mid]) continue;

                            for (int end = 1; end <= n; end++)
                            {

                                if (!_arr[mid][end]) continue;
                                _arr[start][end] = true;
                            }
                        }
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                int m = ReadInt();

                w = new bool[n + 1][];
                l = new bool[n + 1][];

                for (int i = 1; i <= n; i++)
                {

                    w[i] = new bool[n + 1];
                    l[i] = new bool[n + 1];
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    w[f][t] = true;
                    l[t][f] = true;
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
