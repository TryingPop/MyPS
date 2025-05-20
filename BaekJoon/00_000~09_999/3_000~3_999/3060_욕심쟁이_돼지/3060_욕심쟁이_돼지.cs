using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 14
이름 : 배성훈
내용 : 욕심쟁이 돼지
    문제번호 : 3060번

    시뮬레이션 문제다.
    날이 지날 때마다 선택되는 이전날의 사료의 갯수는 4개이다. 
    그래서 총합은 4배씩 늘어난다.
    그래서 시뮬레이션 돌려도 log 20억으로 충분히 시도할만하다.
    
*/

namespace BaekJoon.etc
{
    internal class etc_1627
    {

        static void Main1627(string[] args)
        {

            // log N 의 시간
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = ReadInt();

            while (t-- > 0)
            {

                int n = ReadInt();
                int sum = 0;

                for (int i = 0; i < 6; i++)
                {

                    sum += ReadInt();
                }

                int ret = 1;
                while (sum <= n)
                {

                    ret++;
                    sum <<= 2;
                }

                sw.Write($"{ret}\n");
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
