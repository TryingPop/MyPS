using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 21
이름 : 배성훈
내용 : 과자 나눠주기
    문제번호 : 16401번

    이분탐색 문제다
    과자 길이를 갖고 해당 갯수가 나오는지 이분탐색을 하면 된다

    처음에는 과자 수가 많을 때, 그냥 뒤에 n개 길이를 나눠주면 가장 큰게 아닐까?
    해서 한 번 틀렸다
        -> 반례 
            2명, 2개의 과자 1, 1_000_000_000
            500_000_000 길이로 나눠줄 수 있다!
    그리고 다음은 이분 탐색 시작 r을 잘못 설정해서 1번 더 틀렸다
    arr[0]; -> arr[m - 1]로 바꾸니 이상없이 통과했다

    336ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0589
    {

        static void Main589(string[] args)
        {

            StreamReader sr;
            int n;
            int m;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);

                n = ReadInt();
                m = ReadInt();
                int ret;

                int[] arr = new int[m];

                for (int i = 0; i < m; i++)
                {

                    arr[i] = ReadInt();
                }

                Array.Sort(arr);

                int l = 1;
                int r = arr[m - 1];

                while (l <= r)
                {

                    int mid = (l + r) / 2;

                    int get = 0;
                    for (int i = 0; i < m; i++)
                    {

                        get += arr[i] / mid;
                    }

                    if (n <= get) l = mid + 1;
                    else r = mid - 1;
                }

                sr.Close();

                ret = l - 1;
                Console.WriteLine(ret);
            }

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

#if other
public static class PS
{
    private static int n, m;
    private static int[] num;

    static PS()
    {
        string[] input = Console.ReadLine().Split();
        n = int.Parse(input[0]);
        m = int.Parse(input[1]);
        num = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
    }

    public static void Main()
    {
        int left = 1;
        int right = 1000000000;
        int mid;

        while (left <= right)
        {
            mid = (left + right) / 2;

            if (Check(mid))
                left = mid + 1;
            else
                right = mid - 1;
        }

        Console.Write(right);
    }

    private static bool Check(int p)
    {
        long cnt = 0;

        for (int i = 0; i < m; i++)
        {
            cnt += num[i] / p;
        }

        return cnt >= n;
    }
}
#endif
}
