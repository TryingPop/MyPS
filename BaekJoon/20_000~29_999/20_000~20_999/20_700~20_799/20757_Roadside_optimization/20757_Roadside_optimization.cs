using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 22
이름 : 배성훈
내용 : Roadside optimization
    문제번호 : 20757번

    유니온 파인드 문제다.
    MST가되게 간선을 제거하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1638
    {

        static void Main1638(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int[] group = new int[50_000];
            int[] stk = new int[50_000];

            int q = ReadInt();
            while (q-- > 0)
            {

                int n = ReadInt();

                Init();

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        int cur = ReadInt();
                        if (cur == 0) continue;

                        int f = Find(i);
                        int t = Find(j);
                        if (f == t) continue;
                        ret++;

                        if (t < f)
                        {

                            int temp = f;
                            f = t;
                            t = temp;
                        }

                        group[t] = f;
                    }
                }

                sw.Write($"{ret}\n");

                int Find(int _chk)
                {

                    int len = 0;
                    while (group[_chk] != _chk)
                    {

                        stk[len++] = _chk;
                        _chk = group[_chk];
                    }

                    while (len-- > 0)
                    {

                        group[stk[len]] = _chk;
                    }

                    return _chk;
                }

                void Init()
                {

                    for (int i = 0; i < n; i++)
                    {

                        group[i] = i;
                    }
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
