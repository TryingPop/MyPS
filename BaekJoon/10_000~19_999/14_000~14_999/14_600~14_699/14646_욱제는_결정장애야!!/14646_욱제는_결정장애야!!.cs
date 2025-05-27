using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 18
이름 : 배성훈
내용 : 욱제는 결정장애야
    문제번호 : 14646번

    구현, 시뮬레이션 문제다
    조건대로 이상없이 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0282
    {

        static void Main282(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n = ReadInt(sr);

            bool[] memo = new bool[n + 1];

            int len = 2 * n;
            int ret = 0;
            int cur = 0;
            for (int i = 0; i < len; i++)
            {

                int num = ReadInt(sr);

                if (memo[num]) 
                { 
                    
                    // 이미 스티커 붙인 경우
                    // 스티커 제거다
                    // 한 번 먹은 음식은 다시 안나오므로 false로 수정할 필요 X
                    cur--;
                    continue;
                }

                // 스티커 붙인다
                memo[num] = true;
                cur++;
                if (ret < cur) ret = cur;
            }

            sr.Close();
            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while ((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
