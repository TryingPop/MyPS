using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 19
이름 : 배성훈
내용 : #15164번_제보
    문제번호 : 16163번

    문자열, 매내처 문제다
    처음에 aaaaa같이 같은 문자열인 경우를 신경안써 한 번 틀렸다

    아이디어는 다음과 같다
    매내처 알고리즘을 쓰면,
    초기에 홀수항에만 문자열을 배치하는데,
    문자열이 있는 홀수항의 위치에 홀수 길이의 최대 팰린드롬을 찾아낸다
    반면 짝수 항은 짝수 길이의 최대 팰린드롬이 찾아낸다

    그래서 홀수항이 5인 경우 1, 3, 5로 3개가 존재 가능하고
    이는 (n + 1) / 2로 설정 가능하다

    짝수항은 매내처 알고리즘에 의해 짝수값만 오게되는데 6인 경우
    2, 4, 6으로 3개가 가능하다
    정수의 나눗셈은 내림연산을 이용하기에 (n + 1) / 2를 그대로 써도 된다!

    그래서 모두 누적해서 찾으니 이상없이 통과한다
    다만, 문자열이 최대 200만의 길이라 최대 경우의 수는 10^12를 넘기고,
    자료형을 long으로 해줘야한다
*/

namespace BaekJoon._56
{
    internal class _56_02
    {

        static void Main2(string[] args)
        {

            int[] str, p;
            int len;

            Solve();

            void Solve()
            {

                Init();

                Manachers();

                long ret = 0;

                for (int i = 1; i < str.Length; i++)
                {

                    ret += (p[i] + 1) / 2;
                }

                Console.Write(ret);
            }

            void Init()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                string temp = sr.ReadLine();
                sr.Close();

                len = 2 * temp.Length + 1;
                str = new int[len];

                for (int i = 0; i < temp.Length; i++)
                {

                    str[i * 2 + 1] = temp[i];
                }

                p = new int[len];
            }

            void Manachers()
            {

                int r = 0;
                int c = 0;

                for (int i = 0; i < p.Length; i++)
                {

                    if (i <= r) p[i] = Math.Min(p[2 * c - i], r - i);
                    else p[i] = 0;

                    while (i - p[i] - 1 >= 0
                        && i + p[i] + 1 < p.Length
                        && str[i - p[i] - 1] == str[i + p[i] + 1])
                    {

                        p[i]++;
                    }

                    if (r < i + p[i])
                    {

                        r = i + p[i];
                        c = i;
                    }
                }
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

        var m = Manacher(sr.ReadLine());
        sw.WriteLine(m.Sum(v => (long)(1 + v) / 2));
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

#elif other2
string text = string.Concat('#',string.Join('#',Console.ReadLine().ToArray()),'#');
long n = text.Length;

long range=0, center=0;
long[] radius = new long[text.Length];
for(long i=0; i< n;i ++) {
    if (i <= range) {
        radius[i] = Math.Min(radius[(center<<1) - i], range - i);
    }
    else {
        radius[i] = 0;
    }

    long left, right;
    while(
        (left = i - radius[i] - 1) >= 0 && (right = i + radius[i] + 1) < n //범위 체크
        && text[(int)left] == text[(int)right]
    ) {
        radius[i]++;
    }

    if (range < i + radius[i]) {
        range = i + radius[i];
        center = i;
    }
}

Console.Write(radius.Sum(x => (x+1)>>1));
#endif
}
