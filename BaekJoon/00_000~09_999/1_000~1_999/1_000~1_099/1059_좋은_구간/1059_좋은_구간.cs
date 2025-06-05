using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 29
이름 : 배성훈
내용 : 좋은 구간
    문제번호 : 1059번

    수학, 정렬, 브루트포스 문제다
    n의 최소 조건을 잘못봐서 1번 틀렸다

    아이디어는 다음과 같다
    집합 S를 정렬하고 k가 포함된 구간을 찾아 양끝을 설정한다
    만약 k가 S에 포함되면 0으로 한다
    양쪽 끝값이 정해지면 조건에 맞는 구간을 이제 카운팅했다
*/

namespace BaekJoon.etc
{
    internal class etc_0383
    {

        static void Main383(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();

            int[] arr = new int[n];

            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }

            int k = ReadInt();
            sr.Close();

            Array.Sort(arr);

            int l = 0;
            int r = n - 1;

            while(l <= r)
            {

                int mid = (l + r) / 2;

                if (arr[mid] <= k) l = mid + 1;
                else r = mid - 1;
            }

            int find = l - 1;
            if (find > -1 && arr[find] == k)
            {

                Console.WriteLine(0);
                return;
            }
            int minL;
            if (find > -1) minL = arr[find] + 1;
            else minL = 1;
            int maxR = arr[find + 1] - 1;

            int ret = 0;
            while(minL < k)
            {

                ret += maxR - k + 1;
                minL++;
            }

            ret += maxR - k;
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
