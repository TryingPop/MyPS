using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 29
이름 : 배성훈
내용 : 다운로드
    문제번호 : 3216번

    그리디 문제다
    시작부터 확인하면서 가면 된다
    다만 접근을 잘못해서 2번 틀렸다

    처음에는 차가 누적이 아닌 그냥 계산으로
    양수면 결과에 추가하는 식으로 했다
    이 결과 두 번째 예제를 실행안해서 생긴 결과였다

    2번 틀리고 나서 양수로 바뀔때만 초기화하면 이상없음을 느끼고
    바꾸니 이상없이 통과했다

    10만 * 1000개가 
*/

namespace BaekJoon.etc
{
    internal class etc_0388
    {

        static void Main388(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536);

            int n = ReadInt();

            int before = 0;
            int ret = 0;
            int add = 0;
            for (int i = 0; i < n; i++)
            {

                int cur = ReadInt();
                int dl = ReadInt();
                add += dl - before;
                before = cur;
                if (add > 0) 
                { 
                    
                    ret += add;
                    add = 0;
                }
            }

            Console.WriteLine(ret);

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
