using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 30
이름 : 배성훈
내용 : 팰린드롬??
    문제번호 : 11046번

    매내처 문제다
    매내처 알고리즘으로 해당 좌표를 중심으로하는 가장 긴 팰린드롬의 길이를 저장한다.
    그리고 중심으로 하는 길이가 처음과 끝을 포함하면 1, 아니면 0을 반환하게 하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1228
    {

        static void Main1228(string[] args)
        {

            string YES = "1\n";
            string NO = "0\n";

            StreamReader sr;
            StreamWriter sw;

            int[] arr, chk;
            int n;

            Solve();
            void Solve()
            {

                Init();

                Manacher();

                GetRet();
            }

            void GetRet()
            {

                int len = ReadInt();

                for (int i = 0; i < len; i++)
                {

                    int s = ReadInt() - 1;
                    int e = ReadInt() - 1;

                    // 중심으로 하는 팰린드롬의 길이 확인
                    if (chk[s + e + 1] >= e - s + 1) sw.Write(YES);
                    else sw.Write(NO);
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n * 2 + 1];
                chk = new int[n * 2 + 1];

                for (int i = 0; i < n; i++)
                {

                    arr[(i << 1) + 1] = ReadInt();
                }

                n = (n << 1) + 1;
            }

            void Manacher()
            {

                int r = 0, p = 0;
                for (int i = 0; i < n; i++)
                {

                    if (i <= r) chk[i] = Math.Min(r - i, chk[2 * p - i]);
                    else chk[i] = 0;

                    while (i - chk[i] - 1 >= 0
                        && i + chk[i] + 1 <= n - 1
                        && arr[i - chk[i] - 1] == arr[i + chk[i] + 1]) { chk[i] += 1; }

                    if (r < i + chk[i])
                    {

                        r = i + chk[i];
                        p = i;
                    }
                }
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;

                    ret = c - '0';
                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }

#if other
using System;
using System.IO;
using System.Linq;

// #nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var numbers = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var manacher = Manacher(numbers);

        var q = Int32.Parse(sr.ReadLine());
        while (q-- > 0)
        {
            var sted = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

            var l = sted[0] - 1;
            var r = sted[1] - 1;
            var manacherMid = l + r + 2;

            var len = r - l + 1;

            sw.WriteLine(manacher[manacherMid] >= len ? 1 : 0);
        }
    }

    private static int[] Manacher(int[] s)
    {
        var arr = new int[2 * s.Length + 3];
        var radius = new int[arr.Length];
        arr[0] = '$';
        arr[^1] = '@';
        arr[^2] = '#';

        for (var idx = 0; idx < s.Length; idx++)
        {
            arr[2 * idx + 1] = '#';
            arr[2 * idx + 2] = s[idx];
        }

        var center = 0;
        var right = 0;
        for (var idx = 1; idx < arr.Length - 1; idx++)
        {
            if (idx < right)
            {
                var mirrored = 2 * center - idx;
                radius[idx] = Math.Min(right - idx, radius[mirrored]);
            }

            while (arr[idx + radius[idx] + 1] == arr[idx - radius[idx] - 1])
                radius[idx]++;

            if (idx + radius[idx] > right)
            {
                center = idx;
                right = idx + radius[idx];
            }
        }

        return radius;
    }
}

#endif
}
