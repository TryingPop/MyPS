using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 1
이름 : 배성훈
내용 : 두 개의 탑
    문제번호 : 2118번

    30분안에 투포인트를 바로 못떠올렸다
    ... 아직 많이 부족하다
    풀이까지 꽤 많은 시간을 소모했다(1시간 가까이;)

    먼저 접근한 방법은 이분 탐색이다
    mid를 옮기는 조건문 만들기가 힘들어 넘겼다

    그리고 차례로라는 조건 초점을 맞춰서 
    투포인트로 완전탐색을 진행했다

    투포인트 탐색방법은 다음과 같다
    우선 부분합 sum은 left에서 right까지이다!
    0, 0에서 시작한다
    right는 반값이 안되는 경우 + 1 이동시킨다
    left는 반값을 넘기는 경우 + 1이동시켜 반값을 못넘기게 한다
    만약 left == right에서 left를 이동시켜야 하는 경우면(양수만 입력되는 여기는 없지만!)
    right를 이동시키게 코드를 짰다

    N의 크기가 5만이라 N^2인 2중 포문 방법은 시도조차 안했다
*/

namespace BaekJoon.etc
{
    internal class etc_0138
    {

        static void Main138(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);
            int total = 0;
            int[] arr = new int[len + 1];

            for (int i = 0; i < len; i++)
            {

                arr[i + 1] = ReadInt(sr);
                // arr[i + 1] += arr[i];
                total += arr[i + 1];
            }

            sr.Close();

            int left = 0;
            int right = 0;
            int ret = 0;
            int cur = arr[0];
            int half = total / 2;
            while(left <= len && right < len)
            {

                if (cur > half && left < right)
                {

                    cur -= arr[left++];
                }
                else cur += arr[++right];

                // 여기서 체크 ?
                int calc = total - cur;
                if (calc > cur)
                {

                    if (ret < cur) ret = cur;
                }
                else
                {

                    if (ret < calc) ret = calc;
                }
            }

            // 남은 부분
            while(left < len)
            {

                cur -= arr[left++];

                int calc = total - cur;
                if (calc > cur)
                {

                    if (ret < cur) ret = cur;
                }
                else
                {

                    if (ret < calc) ret = calc;
                }
            }

            Console.WriteLine(ret);
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

#if other
#nullable disable

using System;
using System.IO;

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());

        var len = new int[1 + n];
        for (var idx = 0; idx < n; idx++)
            len[1 + idx] = len[idx] + Int32.Parse(sr.ReadLine());

        var lensum = len[n];
        var max = 0;

        for (var e = 1; e <= n; e++)
            for (var s = 0; s < e; s++)
            {
                var dist = Math.Min(len[e] - len[s], lensum - (len[e] - len[s]));
                max = Math.Max(max, dist);
            }

        sw.WriteLine(max);
    }
}

#endif
}
