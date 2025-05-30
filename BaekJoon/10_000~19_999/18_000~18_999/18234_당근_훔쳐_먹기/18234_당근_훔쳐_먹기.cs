using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 30
이름 : 배성훈
내용 : 당근 훔쳐먹기
    문제번호 : 18234번

    그리디, 정렬 문제다.
    마지막 n일에만 당근을 먹는게 좋다.
    그래서 마지막 n일 이전에는 굶는다

    그리고 보너스가 작은거부터 먹는게 그리디로 최소임을 알 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1657
    {

        static void Main1657(string[] args)
        {

            int n, t;
            (int w, int p)[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                Array.Sort(arr, (x, y) =>
                {

                    int ret = y.p.CompareTo(x.p);
                    if (ret == 0) ret = x.w.CompareTo(y.w);
                    return ret;
                });

                long ret = 0;

                for (int i = 0; i < n; i++)
                {

                    ret += GetVal(i);
                }

                Console.Write(ret);

                long GetVal(int _idx)
                    => arr[_idx].w + 1L * arr[_idx].p * --t;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                t = ReadInt();

                arr = new (int w, int p)[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = (ReadInt(), ReadInt());
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

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
