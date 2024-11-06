using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 13
이름 : 배성훈
내용 : 포켓몬 GO
    문제번호 : 13717번

    
    달팽이? 문제이다
*/

namespace BaekJoon.etc
{
    internal class etc_0024
    {

        static void Main24(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int mon = ReadInt(sr);                  // 몬스터 수

            string maxMon = "";                     // 가장 많이 진화한 포켓몬 이름
            int max = -1;                           // 가장 많이 진화한 횟수
            int total = 0;                          // 전체 진화한 횟수

            for (int i = 0; i < mon; i++)
            {

                string name = sr.ReadLine();        // 현재 몬스터 이름
                int need = ReadInt(sr);             // 진화에 필요한 사탕 수
                int now = ReadInt(sr);              // 해당 포켓몬 진화에 필요한 보유 사탕 개수
                int curUp = 0;                      // 현재 진화 정도

                if (need <= now)
                {

                    // 처음 진화 가능?
                    now -= need;
                    curUp++;

                    // 이후에 진화는 사탕 2개를 받는다
                    // need >= 12 이다!
                    curUp += now / (need - 2);
                }

                if (curUp > max) 
                { 
                    
                    // 가장 많이 진화한 포켓몬 저장
                    max = curUp;
                    maxMon = name;
                }

                // 전체 진화횟수 추가
                total += curUp;
            }

            Console.WriteLine(total);
            Console.WriteLine(maxMon);
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret *= 10;
                ret += c - '0';
            }

            return ret;
        }
    }
}
