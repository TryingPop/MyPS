using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 19
이름 : 배성훈
내용 : 가장 긴 팰린드롬 부분 문자열
    문제번호 : 13275번

    문자열, 매내처 문제다
    매내처 알고리즘은 해당 사이트를 참고해서 작성했다
    이전값을 이용할게 없다면 현재 길이 1씩 늘려가면서 재고,
    팰린드롬이 발견되어 이전값을 이용할 수 있다면, 이용해서 현재 팰린드롬 찾는데 영향을 준다
    https://m.blog.naver.com/jqkt15/222108415284
*/

namespace BaekJoon._56
{
    internal class _56_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr;
            int[] str, p;

            Solve();

            void Solve()
            {

                Init();

                Manachers();

                int ret = 0;
                for (int i = 0; i < p.Length; i++)
                {

                    ret = Math.Max(ret, p[i]);
                }

                Console.Write(ret);
            }

            void Manachers()
            {

                int r = 0;  // 오른쪽 끝 지점
                int c = 0;  // 중심

                for (int i = 0; i < p.Length; i++)
                {

                    if (i <= r) p[i] = Math.Min(p[2 * c - i], r - i);
                    else p[i] = 0;

                    while (i - p[i] - 1 >= 0
                        && i + p[i] + 1 < p.Length
                        && str[i - p[i] - 1] == str[i + p[i] + 1])
                    {

                        // 팰린드롬 갯수++
                        p[i]++;
                    }

                    if (r < i + p[i])
                    {

                        r = i + p[i];
                        c = i;
                    }
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 2);
                string temp = sr.ReadLine();
                int len = temp.Length * 2 + 1;
                str = new int[len];

                for (int i = 0; i < temp.Length; i++)
                {

                    str[i * 2 + 1] = temp[i];
                }
                p = new int[len];
                sr.Close();
            }
        }
    }

#if other
using System;
using System.IO;
using System.Linq;

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var s = sr.ReadLine();
        var lens = Manacher(s);

        sw.WriteLine(lens.Max());
    }

    private static int[] Manacher(string s)
    {
        var arr = new Char[2 * s.Length + 3];
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
