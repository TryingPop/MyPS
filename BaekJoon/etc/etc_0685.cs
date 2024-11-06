using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 10
이름 : 배성훈
내용 : 비밀번호 찾기
    문제번호 : 17219번

    해시 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0685
    {

        static void Main685(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
            StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 4);

            int[] info = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            Dictionary<string, string> ret = new(info[0]);

            for(int i = 0; i < info[0]; i++)
            {

                string[] temp = sr.ReadLine().Split();
                ret[temp[0]] = temp[1];
            }

            for (int i = 0; i < info[1]; i++)
            {

                string temp = sr.ReadLine();
                sw.Write($"{ret[temp]}\n");
                if (i == 1_000)
                {

                    i -= 1_000;
                    info[1] -= 1_000;

                    sw.Flush();
                }
            }

            sr.Close();
            sw.Close();
        }
    }

#if other
#nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nm = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var n = nm[0];
        var m = nm[1];

        var map = new Dictionary<string, string>();

        while (n-- > 0)
        {
            var l = sr.ReadLine().Split(' ');
            map.Add(l[0], l[1]);
        }

        while (m-- > 0)
            sw.WriteLine(map[sr.ReadLine()]);
    }
}

#endif
}
