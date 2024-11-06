using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

/*
날짜 : 2024. 6. 30
이름 : 배성훈
내용 : 난로
    문제번호 : 15553번

    정렬, 그리디 알고리즘 문제다
    친구 오는 간격이 제일 긴텀 동안 끄고 키면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0780
    {

        static void Main780(string[] args)
        {

            StreamReader sr;

            int[] arr;
            int n, t;
            int ret;
            Solve();

            void Solve()
            {

                Input();

                Array.Sort(arr, (x, y) => y.CompareTo(x));

                for (int i = 0; i < n; i++)
                {

                    if (t-- == 0) break;
                    ret -= arr[i];
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 4);
                n = ReadInt();
                t = ReadInt();

                arr = new int[n];

                int curTime = ReadInt() + 1;
                int s = curTime;
                for (int i = 1; i < n; i++)
                {

                    int time = ReadInt();
                    arr[i] = time - curTime;
                    curTime = time + 1;
                }

                ret = curTime - s + 1;
                t--;
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
using System;
using System.Linq;

public class Program
{
    static void Main()
    {
        string[] nk = Console.ReadLine().Split(' ');
        int n = int.Parse(nk[0]), k = int.Parse(nk[1]);
        int[] array = new int[n];
        for (int i = 0; i < n; i++)
        {
            array[i] = int.Parse(Console.ReadLine());
        }
        int[] dif = new int[n - 1];
        for (int i = 0; i < n - 1; i++)
        {
            dif[i] = array[i + 1] - array[i] - 1;
        }
        Array.Sort(dif, (a, b) => b - a);
        Console.Write(array.Last() - array.First() + 1 - dif.Take(k - 1).Sum());
    }
}
#endif
}
