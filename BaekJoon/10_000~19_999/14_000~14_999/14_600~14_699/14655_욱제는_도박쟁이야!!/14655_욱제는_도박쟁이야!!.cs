using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 28
이름 : 배성훈
내용 : 욱제는 도박쟁이야!!
    문제번호 : 14655번

    문제 해석 문제다!
    양끝만 뒤집을 수 있기에 하나씩 원하는 값으로 만들 수 있다
    그리고 두 번째 뒤집기는 같은 동전을 이용한다 (앞 뒷면이 바뀔 수는 있다)
    그래서 첫 번째 뒤집기에서는 최대합을, 두 번째 뒤집기에서는 최소합을 만들어 둘 빼면 점수가 된다
    이 말은 최대합의 2배가 정답인 소리다
    그래서 최대합만 구하고 2배했다
*/

namespace BaekJoon.etc
{
    internal class etc_0119
    {

        static void Main119(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadAbsInt(sr);
            // 절댓값은 1만이하고, 동전의 수도 1만 이하 거기에 2배하면 최대 2억까지 가지므로 int형으로 설정
            int total = 0;
            for (int i = 0; i < len; i++)
            {

                // 절대값 합을 구한다
                total += ReadAbsInt(sr);
            }

            sr.Close();

            // 결과는 2배
            total *= 2;
            Console.WriteLine(total);
        }

        static int ReadAbsInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r' || c == '-') continue;
                
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
