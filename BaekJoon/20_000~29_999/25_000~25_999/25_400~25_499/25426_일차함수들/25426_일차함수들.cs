using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 24
이름 : 배성훈
내용 : 일차함수들
    문제번호 : 25426번

    수학, 그리디, 정렬 문제다
    xi는 i인 수열이다
    bi는 순서에 상관없이 일정한 값이므로 먼저 결과에 더했다
    그리고, bi의 값이 10억이고 10만개나 들어오므로
    오버플로우를 고려해서 long으로 결과를 설정했다
    그리고 ai는 양수이므로 큰거끼리 곱한게 더 큰수가 되므로 a만 정렬해서 곱해줬다

    이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0344
    {

        static void Main344(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            long[] arr = new long[n];
            long ret = 0;
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
                ret = ret + ReadInt();
            }

            sr.Close();
            Array.Sort(arr);

            for (int i = 1; i <= n; i++)
            {

                long add = i * arr[i - 1];
                ret += add;
            }

            Console.WriteLine(ret);

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }
                return ret;
            }
        }
    }
}
