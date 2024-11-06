using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 10
이름 : 배성훈
내용 : 아 저는 볶음밥이요
    문제번호 : 23814번

    상황을 나눠서 풀었다
    총 4가지 경우가 가능하다

    짜장만 채우는 경우, 
    짬뽕만 채우는 경우, 
    짜장, 짬뽕 둘 다 채우는 경우, 
    짜장, 짬뽕 모두 안 채우는 경우

    해당 경우가 존재가능한지 판별하고 비교했다

    calc4를 처음에 갱신해줘야하는데 
    calc3을 갱신하게 해버려서 한 번 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_0174
    {

        static void Main174(string[] args)
        {

            long n = long.Parse(Console.ReadLine());
            long[] info = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            info[0] %= n;
            info[1] %= n;

            long fill1 = n - info[0];
            long fill2 = n - info[1];

            long ret1 = 0;      // 짜장만 채우는 경우
            long ret2 = 0;      // 짬뽕만 채우는 경우
            long ret3 = 0;      // 짜장, 짬뽕 채우는 경우
            long ret4 = 0;      // 짜장, 짬뽕 둘 다 안채우는 경우

            // 군만 두 수
            long calc1 = 0;     // 짜장
            long calc2 = 0;     // 짬뽕
            long calc3 = 0;     // 짜장, 짬뽕
            long calc4 = 0;     // -

            ret1 = info[2] - fill1;
            calc1 = 1 + (info[2] - fill1) / n;

            ret2 = info[2] - fill2;
            calc2 = 1 + (info[2] - fill2) / n;

            ret3 = info[2] - fill1 - fill2;
            calc3 = 2 + ((info[2] - fill1 - fill2) / n);

            ret4 = info[2];
            calc4 = info[2] / n;

            long ret = 0;
            if (ret3 >= 0)
            {

                // ret1 && ret2 가 양수 보장!
                // calc 3이 되려면 가장 커야한다
                if (calc3 > calc1 && calc3 > calc2 && calc3 > calc4) ret = ret3;
                // calc3이 가장 큰 수가 아님이보장되면 
                else if (calc1 > calc2 && calc1 > calc4) ret = ret1;
                else if (calc2 > calc1 && calc2 > calc4) ret = ret2;
                else if (calc1 == calc2 && calc1 > calc4)
                {

                    if (ret1 > ret2) ret = ret1;
                    else ret = ret2;
                }
                else ret = ret4;
            }
            else if (ret1 >= 0 && ret2 >= 0)
            {

                // ret3 < 0 
                if (calc1 > calc2 && calc1 > calc4) ret = ret1;
                else if (calc2 > calc1 && calc2 > calc4) ret = ret2;
                else if (calc1 == calc2 && calc1 > calc4)
                {

                    if (ret1 > ret2) ret = ret1;
                    else ret = ret2;
                }
                else ret = ret4;
            }
            else if (ret1 >= 0)
            {

                if (calc1 > calc4) ret = ret1;
                else ret = ret4;
            }
            else if (ret2 >= 0)
            {

                if (calc2 > calc4) ret = ret2;
                else ret = ret4;
            }
            else ret = ret4;

            Console.WriteLine(ret);
        }
    }
}
