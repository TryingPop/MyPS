using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 28
이름 : 배성훈
내용 : 입국심사
    문제번호 : 3079번

    이분탐색 문제다
    예상치 못한 곳에서 발생한 오버플로우로 한참을 틀렸다

    아이디어는 결과 시간을 이분탐색으로 찾는다
    결과 시간을 정하고, 심사된 인원이 조건을 만족하는 경우 시간을 줄여본다
    반면 인원이 조건을 만족하지 않으면 시간을 늘린다
    인원 확인은 해당 시간에 보든 심사위원이 최대 몇명을 입국심사 확인했는지 더한다(내림 나눗셈 연산)

    여기서 for문을 돌리는데, 오버플로우가 발생했다;
    해당 부분을 캐치 못해서 문제 입력이 잘못되었는가 long 연산이니 컴파일러 에러인가 ?
    이상한 곳에서 자꾸 수정을 가했고 30% 이하에서 계속 틀렸다
    캐치한 이후, 10^18을 넘기면 그냥 for문을 탈출하게했다

    어차피 가질 수 있는 최대 값은 10^18이다
    심사받는 인원 10^9명에, 심사위원이 1명이고 1명 심사당 10^9시간이 걸릴 때
    최대값인 10^18 이된다
*/

namespace BaekJoon.etc
{
    internal class etc_0375
    {

        static void Main375(string[] args)
        {

            long MAX = 1_000_000_000_000_000_000;
            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int m = ReadInt();

            int[] time = new int[n];
            for (int i = 0; i < n; i++)
            {

                time[i] = ReadInt();
            }

            sr.Close();

            long l = 1;
            long r = MAX;

            while (l <= r)
            {

                long mid = (l + r) / 2;
                long curTime = 0;
                for (int i = 0; i < n; i++)
                {

                    curTime += mid / time[i];
                    if (curTime >= MAX) break;
                }

                if (curTime < m) l = mid + 1;
                else r = mid - 1;
            }

            Console.WriteLine(r + 1);

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
