using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 1
이름 : 배성훈
내용 : 백신 개발
    문제번호 : 30090번

    브루트포스, 백트래킹 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1663
    {

        static void Main1663(string[] args)
        {

            int n;
            string[] str;

            Input();

            GetRet();

            void GetRet()
            {

                int INF = 101;
                int[] calcStr = new int[100];

                int ret = DFS();

                Console.Write(ret);

                int DFS(int _dep = 0, int _state = 0, int _len = 0)
                {

                    if (_dep == n)
                        return _len;

                    int ret = INF;
                    for (int i = 0; i < n; i++)
                    {

                        if ((_state & (1 << i)) != 0) continue;

                        int s = TryAdd(i);
                        if (s == -1) continue;

                        Add(i, s);
                        int nLen = s + str[i].Length;
                        ret = Math.Min(ret, DFS(_dep + 1, _state | (1 << i), nLen));
                    }

                    return ret;

                    void Add(int _idx, int _s)
                    {

                        for (int i = 0; i < str[_idx].Length; i++)
                        {

                            calcStr[_s + i] = str[_idx][i];
                        }
                    }

                    int TryAdd(int _idx)
                    {

                        if (_len == 0) return 0;

                        int s = Math.Max(0, _len - str[_idx].Length);

                        for (int i = s; i < _len; i++)
                        {

                            if (str[_idx][0] == calcStr[i])
                            {

                                bool flag = true;
                                for (int j = 1; i + j < _len; j++)
                                {

                                    if (str[_idx][j] == calcStr[i + j]) continue;
                                    flag = false;
                                    break;
                                }

                                if (flag) return i;
                            }
                        }

                        return -1;
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());

                str = new string[n];
                for (int i = 0; i < n; i++)
                {

                    str[i] = sr.ReadLine();
                }
            }
        }
    }
}
