using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 14
이름 : 배성훈
내용 : 블로그
    문제번호 : 21921번

    하나씩 읽으면서 연산했다
*/

namespace BaekJoon.etc
{
    internal class etc_0030
    {

        static void Main30(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);
            int range = ReadInt(sr);

            // 250_000 * 8_000 = 2_000_000_000
            // 이므로 총합을 int로 설정
            // range간 입장인원
            int curTotal = 0;
            // 입장 유저수
            int[] user = new int[len];

            for (int i = 0; i < range; i++)
            {

                // 입력 받고 초기 total을 계산
                user[i] = ReadInt(sr);
                curTotal += user[i];
            }

            // 초기 인원을 max로 설정
            int max = curTotal;
            // 구간 1개
            int cnt = 1;

            for (int i = range; i < len; i++)
            {

                user[i] = ReadInt(sr);

                // range간 입장인원 갱신
                curTotal += user[i] - user[i - range];
                // 최대보다 많은 경우
                if (max < curTotal)
                {

                    // 최대로 설정하고, 구간 1개
                    max = curTotal;
                    cnt = 1;
                }
                // 최대와 같은 경우 구간 1개 추가
                else if (max == curTotal) cnt++;
            }

            sr.Close();

            // 출력
            if (max == 0) Console.WriteLine("SAD");
            else
            {

                Console.WriteLine(max);
                Console.WriteLine(cnt);
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while((c = _sr.Read()) != ' ' && c != '\n' && c != -1)
            {

                if (c == '\r') continue;

                ret *= 10;
                ret += c - '0';
            }

            return ret;
        }
    }
}
