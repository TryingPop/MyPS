using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 25
이름 : 배성훈
내용 : 유니의 편지 쓰기
    문제번호 : 28070번

    문자열, 누적합, 스위핑, 파싱 문제다
    끝나는날 조정 때문에 인덱스 에러로 한 번 틀렸다
    종료 날짜가 9999년 12월 입력이 존재하는거 같다

    풀이 아이디어는 다음과 같다
    dp를 인덱스는 년, 월을 나타내고 값은 
    입영하면 1 추가, 전역하면 다음달에 1 감소로해서 누적해서 저장해간다 
    그리고 저장이 끝나면 처음부터 읽으면서 복무 중인 인원을 확인해간다
    그리고 매번 복무한 인원을 확인하면서 최대 값이 갱신될 때 시간을 기록한다

    이렇게 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0350
    {

        static void Main350(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt();

            int[,] dp = new int[8_001, 13];

            for (int i = 0; i < len; i++)
            {

                int sy = ReadInt() - 2_000;
                int sm = ReadInt();

                dp[sy, sm]++;

                int ey = ReadInt() - 2_000;
                int em = ReadInt() + 1;

                if (em == 13)
                {

                    em = 1;
                    ey++;
                }

                dp[ey, em]--;
            }

            sr.Close();

            int max = 0;
            int yyyy = 0;
            int mm = 0;
            int cur = 0;
            for (int i = 0; i < 8_001; i++)
            {


                for (int j = 1; j <= 12; j++)
                {

                    cur += dp[i, j];
                    if (max < cur)
                    {

                        yyyy = i;
                        mm = j;
                        max = cur;
                    }
                }
            }

            Console.WriteLine($"{yyyy + 2_000}-{mm:D2}");

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n' && c != '-')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
