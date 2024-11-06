using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 11
이름 : 배성훈
내용 : 수 고르기
    문제번호 : 2230번

    정렬, 두 포인터 문제다
    이분 탐색으로 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0686
    {

        static void Main686(string[] args)
        {

            StreamReader sr;

            int[] arr;
            int n;
            long m;

            Solve();

            void Solve()
            {

                Input();

                Array.Sort(arr);

                long ret = 2_000_000_000;
                for (int i = 0; i < n; i++)
                {

                    long find = m + arr[i];
                    int idx = BinarySearch(find);
                    if (idx == n) break;

                    ret = Math.Min(ret, (long)arr[idx] - arr[i]);
                }

                Console.WriteLine(ret);
            }

            int BinarySearch(long _find)
            {

                int l = 0;
                int r = n - 1;

                while(l <= r)
                {

                    int mid = (l + r) / 2;

                    if (arr[mid] < _find) l = mid + 1;
                    else r = mid - 1;
                }

                return l;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);

                n = ReadInt();
                m = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c = sr.Read();
                if (c == '\r') c = sr.Read();
                if (c == -1 || c == ' ' || c == '\n') return 0;

                bool plus = c != '-';
                int ret;
                if (plus) ret = c - '0';
                else ret = 0;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }
#if other
namespace Baekjoon;

public class Program
{
    private static void Main(string[] args)
    {
        using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        var n = ScanInt(sr);
        var m = ScanInt(sr);
        var nums = new int[n];
        for (int i = 0; i < n; i++)
            nums[i] = ScanInt(sr);
        Array.Sort(nums);
        int l = 0, r = 1, minTerm = int.MaxValue;
        while (true)
        {
            var term = nums[r] - nums[l];
            if (term < m)
            {
                r++;
            }
            else if (term > m)
            {
                minTerm = Math.Min(minTerm, term);
                if (++l == r)
                    r++;
            }
            else
            {
                minTerm = term;
                break;
            }
            if (r == nums.Length)
                break;
        }
        Console.Write(minTerm);
    }

    static int ScanInt(StreamReader sr)
    {
        int c = sr.Read(), n = 0;
        if (c == '-')
            while (!((c = sr.Read()) is ' ' or '\n' or -1))
            {
                if (c == '\r')
                {
                    sr.Read();
                    break;
                }
                n = 10 * n - c + '0';
            }
        else
        {
            n = c - '0';
            while (!((c = sr.Read()) is ' ' or '\n' or -1))
            {
                if (c == '\r')
                {
                    sr.Read();
                    break;
                }
                n = 10 * n + c - '0';
            }
        }
        return n;
    }
}
#endif
}
