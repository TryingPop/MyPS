using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 20
이름 : 배성훈
내용 : 개미
    문제번호 : 3048번

    구현, 문자열, 시뮬레이션 문제다
    문제 규칙을 찾고 연산을 통해 결과를 바로 출력되게 코드를 작성했다
*/

namespace BaekJoon.etc
{
    internal class etc_0294
    {

        static void Main294(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            string str1 = sr.ReadLine();
            string str2 = sr.ReadLine();

            int time = ReadInt(sr);
            sr.Close();

            // str의 위치
            int[] move1 = new int[n];       // str1의 결과에 있을 인덱스
            int[] move2 = new int[m];       // str2의 결과에 있을 인덱스

            for (int i = 0; i < n; i++)
            {

                // str1은 역순으로 배치
                move1[i] = n - 1 - i;
            }

            for (int i = 0; i < m; i++)
            {

                move2[i] = n + i;
            }

            int idx1 = 0;
            int idx2 = 0;
            for (int i = time; i >= 0; i--)
            {

                // 앞에서부터 time, time - 1, time - 2를 이동한다
                if (idx1 < n)
                {

                    move1[idx1] += i;
                    idx1++;
                }
                
                // 앞에서부터 time, time - 1, time -2를 이동한다
                if (idx2 < m)
                {

                    move2[idx2] -= i;
                    idx2++;
                }
            }

            char[] ret = new char[n + m];
            int s = 0;
            int e = n + m - 1;

            for (int i = 0; i < n; i++)
            {

                // 인덱스를 초과하면, 안되므로
                // 인덱스 조절
                if (move1[i] > e)
                {

                    move1[i] = e;
                    e--;
                }
                ret[move1[i]] = str1[i];
            }

            for (int i = 0; i < m; i++)
            {

                // 마찬가지 인덱스 조절
                if (move2[i] < s)
                {

                    move2[i] = s;
                    s++;
                }
                ret[move2[i]] = str2[i];
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < n + m; i++)
                {

                    sw.Write(ret[i]);
                }
            }
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
