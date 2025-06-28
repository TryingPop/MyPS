using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 18
이름 : 배성훈
내용 : 카드 문자열
    문제번호 : 13417번

    그리디, 덱 문제다
    비슷한 문제를 다뤄봤기에 쉽게 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0279
    {

        static void Main279(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            // 최대 길이 1000
            int[] ret = new int[2_001];
            int mid = 1_000;
            int test = ReadInt(sr);

            while(test-- > 0)
            {

                int len = ReadInt(sr);
                // 앞의 idx
                int front = mid;
                // 뒤의 idx
                int back = mid;
                int f = sr.Read();
                
                ret[back++] = f;
                for (int i = 1; i < len; i++)
                {

                    sr.Read();
                    int c = sr.Read();

                    // 그리디하게 이어붙이기
                    if (ret[front] >= c) ret[--front] = c;
                    else ret[back++] = c;
                }

                for (int i = front; i < back; i++)
                {

                    char s = (char)ret[i];
                    sw.Write(s);
                }
                sr.ReadLine();
                sw.Write('\n');
            }

            sw.Close();
            sr.Close();
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
