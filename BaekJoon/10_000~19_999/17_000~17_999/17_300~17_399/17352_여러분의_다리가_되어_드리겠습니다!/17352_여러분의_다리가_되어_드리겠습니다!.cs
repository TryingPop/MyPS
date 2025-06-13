using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 13
이름 : 배성훈
내용 : 여러분의 다리가 되어 드리겠습니다!
    문제번호 : 17352번

    분리 집합 문제다.
    마지막에 최신화를 안해줘서 1번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1699
    {

        static void Main1699(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = ReadInt();
            int[] group = new int[n + 1];
            int[] stk = new int[n];

            for (int i = 1; i <= n; i++)
            {

                group[i] = i;
            }

            for (int i = 2; i < n; i++)
            {

                int f = ReadInt();
                int t = ReadInt();
                Union(f, t);
            }

            for (int i = 2; i <= n; i++)
            {

                Find(i);
                if (group[i] == group[1]) continue;

                Console.Write($"{1} {i}");
                break;
            }

            void Union(int _f, int _t)
            {

                int f = Find(_f);
                int t = Find(_t);

                if (f == t) return;

                if (t < f)
                {

                    int temp = f;
                    f = t;
                    t = temp;
                }

                group[t] = f;
            }

            int Find(int _chk)
            {

                int len = 0;

                while (_chk != group[_chk])
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
