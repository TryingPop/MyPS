using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 5
이름 : 배성훈
내용 : 충돌의 수
    문제번호 : 24468번

    시뮬레이션 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1614
    {

        static void Main1614(string[] args)
        {

            int l, n, t;
            int[] pos;
            int[] dir;
            bool[] visit;

            Input();

            GetRet();

            void GetRet()
            {

                int CALC = l << 1;
                int ret = 0;
                visit = new bool[l + 1];
                // i공과 j공이 x좌표에서 부딪히면
                // 서로 방향이 바뀐다.
                // 각 공이 아닌 집합의 개념으로 보면 
                // 집합이 같음을 알 수 있다.
                for (int time = 0; time < t; time++)
                {

                    for (int i = 0; i < n; i++)
                    {

                        // .5에서 충돌 확인
                        if (visit[pos[i]]) ret++;
                        NextPos(i);
                        // 정수 좌표에서 충돌 확인
                        if (visit[pos[i]]) ret++;
                        else visit[pos[i]] = true;
                    }

                    for (int i = 0; i < n; i++)
                    {

                        visit[pos[i]] = false;
                    }
                }

                Console.Write(ret);

                void NextPos(int _idx)
                {

                    // 공의 다음위치
                    ref int p = ref pos[_idx];
                    ref int d = ref dir[_idx];

                    p += d;
                    if (p < 0)
                    {

                        p = -p;
                        d = 1;
                    }
                    else if (p > l)
                    {

                        p = CALC - p;
                        d = -1;
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                l = ReadInt();
                n = ReadInt();
                t = ReadInt();

                pos = new int[n];
                dir = new int[n];

                for (int i = 0; i < n; i++)
                {

                    pos[i] = ReadInt();
                    dir[i] = sr.Read() == 'R' ? 1 : -1;
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

                        while((c =sr.Read()) != -1 && c != ' ' && c != '\n')
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
