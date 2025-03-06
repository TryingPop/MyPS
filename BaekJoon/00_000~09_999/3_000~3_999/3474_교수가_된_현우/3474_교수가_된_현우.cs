using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 12
이름 : 배성훈
내용 : 교수가 된 현우
    문제번호 : 3474번

    뒤에 0의 개수는 10으로 나눠떨어지는 갯수와 같다
    그래서 소인 수 중 2와 5의 소인 수 중 작은게 정답이 된다
    그런데 팩토리얼 정의 상 2의 개수는 항상 5의 개수보다 많기에!
    5의 개수만 세어 결론을 도출했다
    이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0206
    {

        static void Main206(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            int test = ReadInt(sr);

            while(test-- > 0)
            {

                int find = ReadInt(sr);

                int ret = 0;
                while(find > 0)
                {

                    find /= 5;
                    ret += find;
                }

                sw.WriteLine(ret);
            }

            sw.Close();
            sr.Close();
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
