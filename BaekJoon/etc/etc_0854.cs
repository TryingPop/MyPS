using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 31
이름 : 배성훈
내용 : 회전 초밥
    문제번호 : 2531번

    두 포인터, 브루트포스, 슬라이딩 윈도우 문제다
    쿠폰 초밥을 먼저 먹고 시작한다
    그리고 1 ~ k개를 슬라이딩 윈도우로 1칸씩 이동하며 최대 종류를 찾는다
*/

namespace BaekJoon.etc
{
    internal class etc_0854
    {

        static void Main854(string[] args)
        {

            StreamReader sr;
            int[] cnt;
            int n, d, k, c;
            int ret;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                ret = 1;
                cnt[c]++;
                for (int i = 0; i < k; i++)
                {

                    int cur = arr[i];
                    arr[i + n] = cur;
                    if (cnt[cur] == 0) ret++;
                    cnt[cur]++;
                }

                int max = ret;

                for (int i = 0; i < n; i++)
                {

                    cnt[arr[i]]--;
                    if (cnt[arr[i]] == 0) ret--;

                    int cur = arr[i + k];
                    if (cnt[cur] == 0) ret++;
                    cnt[cur]++;

                    if (max < ret) max = ret;
                }

                Console.Write(max);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                d = ReadInt();
                k = ReadInt();
                c = ReadInt();

                arr = new int[n + k];
                cnt = new int[d + 1];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }



                sr.Close();
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
namespace Baekjoon;

using System;
using System.Text;

public class Program
{
    private static void Main(string[] args)
    {
        using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        int plateCount = ScanInt(sr), sushiKind = ScanInt(sr), requiredSushi = ScanInt(sr), coupon = ScanInt(sr) - 1;
        var sushi = new int[plateCount];
        for (int i = 0; i < plateCount; i++)
            sushi[i] = ScanInt(sr) - 1;
        var sushiSelected = new int[sushiKind];
        var contained = new HashSet<int>();
        sushiSelected[coupon]++;
        contained.Add(coupon);
        for (int i = 0; i < requiredSushi; i++)
        {
            var curSushi = sushi[i];
            contained.Add(curSushi);
            sushiSelected[curSushi]++;
        }

        var ret = contained.Count;
        for (int i = 0; i < plateCount - 1; i++)
        {
            var removedSushi = sushi[i];
            if (--sushiSelected[removedSushi] == 0)
                contained.Remove(removedSushi);
            var addedSushi = sushi[(i + requiredSushi) % plateCount];
            contained.Add(addedSushi);
            sushiSelected[addedSushi]++;
            ret = Math.Max(contained.Count, ret);
        }
        Console.Write(ret);

    }

    static int ScanInt(StreamReader sr)
    {
        int c, n = 0;
        while (!((c = sr.Read()) is ' ' or '\n' or -1))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n + c - '0';
        }
        return n;
    }
}
#endif
}
