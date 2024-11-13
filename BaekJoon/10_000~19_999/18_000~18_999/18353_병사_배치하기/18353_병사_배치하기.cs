using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 28
이름 : 배성훈
내용 : 병사 배치하기
    문제번호 : 18353번

    가장 긴 증가하는 부분수열 LCS 문제다
    여기서는 증가가 아닌 내림이어야 한다
    처음에 내림이 아닌, 이하로 잡아서 틀렸다;

    그리고 LCS인건 바로 보였으나 풀이가 바로 안떠올랐다;
    그래서 이전에 LSC 풀이를 보고 방법을 상기시켰다
*/

namespace BaekJoon.etc
{
    internal class etc_0127
    {

        static void Main127(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);
            int[] arr = new int[len]; 
            
            for (int i = 0; i < len; i++)
            {

                arr[i] = ReadInt(sr);
            }

            sr.Close();
            int[] dp = new int[len];

            dp[0] = arr[0];
            int curLen = 1;
            for (int i = 1; i < len; i++)
            {

                int left = 0;
                int right = curLen - 1;

                while(left <= right)
                {

                    int mid = (left + right) / 2;

                    if (dp[mid] <= arr[i])
                    {

                        // dp[mid]가 작으면 right를 이동시켜야
                        // 내림차순이 유지된다
                        right = mid - 1;
                    }
                    else
                    {

                        // dp[mid]가 크면 left를 이동!
                        left = mid + 1;
                    }
                }

                // left는 dp에서 arr[i] 값보다 크거나 같은 위치를 가리킨다
                // 같은경우 left를 이동안시켰기에 가능하다!
                // 만약 같은 경우를 살리고 싶다면 위에서 이꼴을 빼면 된다
                dp[left] = arr[i];

                // 현재 기록된 최장 길이!
                if (curLen < left + 1) curLen = left + 1;
            }

            int ret = 0;
            for (int i = len - 1; i >= 0; i--)
            {

                if (dp[i] != 0) break;

                ret++;
            }

            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
