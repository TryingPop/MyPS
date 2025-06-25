using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 25
이름 : 배성훈
내용 : 디버깅
    문제번호 : 24453번

    두 포인터 문제다.
    연속한 x줄 중 오류가 가장 적은 구간을 찾는다.
    그리고 찾은게 y개 이하면 y개를 제거해야 에디터를 사용할 수 있으므로 
    y개가 될때까지 임의로 지운다. 그리고 남은건 에디터로 지운다.
    반면 y개를 넘으면 남은건 에디터로 지우면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1729
    {

        static void Main1729(string[] args)
        {

            int n, m, x, y;
            bool[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int cnt = 0;
                for (int i = 1; i <= x; i++)
                {

                    if (arr[i]) cnt++;
                }

                // 연속된 x줄에 오류가 가장 적은 경우를 찾는다.
                int min = cnt;
                for (int s = 1, e = x + 1; e <= n; e++, s++)
                {

                    if (arr[e]) cnt++;
                    if (arr[s]) cnt--;

                    min = Math.Min(cnt, min);
                }

                if (min < y) min = y;
                Console.Write(m - min);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                arr = new bool[n + 1];
                for (int i = 0; i < m; i++)
                {

                    arr[ReadInt()] = true;
                }

                x = ReadInt();
                y = ReadInt();

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
