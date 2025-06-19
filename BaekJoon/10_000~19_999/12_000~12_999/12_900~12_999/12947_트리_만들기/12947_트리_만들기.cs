using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 19
이름 : 배성훈
내용 : 트리 만들기
    문제번호 : 12947번

    트리, 트리의 지름 문제다.
    임의의 점에서 가장 먼 거리에 있는 점을 찾는다.
    
    그리고 해당 점에서 가장 먼 거리에 있는 점이 트리의 지름이 된다.


    이제 거리 계산을 한다.

    만약 거리 i에서 노드의 갯수가 1이라면 i + 1에 대해서는 i를 경유해야 한다.
    만약 cnt[i] = 1이고 모든 i < k ≤ j에 대해 cnt[k] != 1이라면 j는 i를 경유해서 간다.
    그리고 j, k의 거리는 k - i + j - i이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1713
    {

        static void Main1713(string[] args)
        {

            int d;
            int[] cnt;

            Input();

            GetRet();

            void GetRet()
            {

                int ret = d;
                cnt[0] = 1;

                int prev = 0;
                for (int i = 1; i <= d; i++)
                {

                    if (cnt[i] == 1)
                    {

                        prev = i;
                        ret = Math.Max(ret, d - i);
                    }
                    else
                        ret = Math.Max(ret, (d - prev + i - prev));
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                d = ReadInt();
                cnt = new int[d + 1];
                for (int i = 1; i <= d; i++)
                {

                    cnt[i] = ReadInt();
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
