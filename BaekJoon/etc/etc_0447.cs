using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 4
이름 : 배성훈
내용 : 요요 시뮬레이션
    문제번호 : 19636번

    구현 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0447
    {

        static void Main447(string[] args)
        {

            string DEAD = "Danger Diet";
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int w = ReadInt();      // 초기 무게
            int i = ReadInt();      // 일일 에너지 섭취량, 일일 기초 대사량
            int t = ReadInt();      // 역치

            int d = ReadInt();      // 다이어트 기간
            int e = ReadInt();      // 다이어트 기간 에너지 섭취
            int a = ReadInt();      // 다이어트 기간 일일 활동 대사량
            sr.Close();

            // 변화를 무시하는 경우 
            int ret1 = w + d * (e - a - i);
            int ret2 = i;
            

            // 기초대사량이 변하는 경우 시뮬레이션 돌렸다
            bool dead = false;
            int ret3 = w;
            
            for (int j = 0; j < d; j++)
            {

                int chk = e - a - i;
                ret3 += chk;
                if (-chk > t)
                {

                    chk = (chk - 1) / 2;
                    i += chk;
                }
                else if (chk > t)
                {

                    i += chk / 2;
                }

                // 사망 여부
                if (i <= 0 || ret3 <= 0) dead = true;
            }

            int ret4 = i;

            // 출력
            if (ret1 <= 0)
            {

                Console.WriteLine(DEAD);
            }
            else
            {

                Console.WriteLine($"{ret1} {ret2}");
            }

            if (dead)
            {

                Console.WriteLine(DEAD);
            }
            else
            {

                if (ret4 < ret2) Console.WriteLine($"{ret3} {ret4} YOYO");
                else Console.WriteLine($"{ret3} {ret4} NO");
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
