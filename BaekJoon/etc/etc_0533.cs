using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 14
이름 : 배성훈
내용 : 접미사 배열
    문제번호 : 11656번

    문자열, 정렬 문제다
    내장 메서드를 이용해서 정렬과 부분 문자열을 구했다
*/

namespace BaekJoon.etc
{
    internal class etc_0533
    {

        static void Main533(string[] args)
        {

            string str = Console.ReadLine();

            string[] arr = new string[str.Length];
            for (int i = 0; i < str.Length; i++)
            {

                arr[i] = str.Substring(i);
            }

            Array.Sort(arr);
            using (StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 65536))
            {

                for (int i = 0; i < arr.Length; i++)
                {

                    sw.WriteLine(arr[i]);
                }
            }
        }
    }

#if other
public static class PS
{
    private class StringComparer : IComparer<string>
    {
        int IComparer<string>.Compare(string x, string y)
        {
            if (x[0] == y[0])
            {
                int len = Math.Min(x.Length, y.Length);

                for (int i = 1; i < len; i++)
                {
                    if (x[i] != y[i])
                        return x[i] - y[i];
                }

                return x.Length - y.Length;
            }
            else
            {
                return x[0] - y[0];
            }
        }
    }

    private static string s;
    private static string[] suffix;

    static PS()
    {
        s = Console.ReadLine();
        suffix = new string[s.Length];

        for (int i = 0; i < s.Length; i++)
        {
            suffix[i] = s[i..];
        }
    }

    public static void Main()
    {
        Array.Sort(suffix, new StringComparer());

        using (StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput())))
        {
            sw.Write(string.Join('\n', suffix));
        }
    }
}
#endif
}
