using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 16
이름 : 배성훈
내용 : 숫자의 신
    문제번호 : 1422번

    그리디 정렬 문제다
    우선 이전에 비슷한 유형을 풀어봐서 쉽게 풀었다
    거기서는 문자열을 유클리드 호제법 처럼 정렬 우선순위를 정했는데,
    여기서는 그렇게 하지 않고 유클리드 호제법과 같은 역할을 하는 연산을 진행했다

    그리고 반복해서 넣는건 자리수가 최우선이므로 자리수 연산으로 정렬 먼저하고,
    이후에 값이 커지는 걸로 연산했다
*/

namespace BaekJoon.etc
{
    internal class etc_0541
    {

        static void Main541(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            StreamWriter sw = new(Console.OpenStandardOutput());

            Solve();
            sw.Close();
            sr.Close();

            void Solve()
            {

                int[] info = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                (string str, int cnt)[] arr = new (string str, int cnt)[info[0]];
                int add = info[1] - info[0];

                for (int i = 0; i < info[0]; i++)
                {

                    arr[i] = (sr.ReadLine(), 1);
                }

                Array.Sort(arr, Comp1);

                StringBuilder sb = new StringBuilder(25);

                for (int i = 0; i < add; i++)
                {

                    arr[0].cnt++;
                }

                Array.Sort(arr, Comp2);
                for (int i = 0; i < arr.Length; i++)
                {

                    int len = arr[i].cnt;
                    for (int j = 0; j < len; j++)
                    {

                        sb.Append(arr[i].str);  
                    }
                }

                sw.Write(sb);

                int Comp1((string str, int cnt) _str1, (string str, int cnt) _str2)
                {

                    if (_str1.str.Length != _str2.str.Length) return _str2.str.Length.CompareTo(_str1.str.Length);
                    return (_str2.str + _str1.str).CompareTo(_str1.str + _str2.str);
                }

                int Comp2((string str, int cnt) _str1, (string str, int cnt) _str2)
                {

                    return (_str2.str + _str1.str).CompareTo(_str1.str + _str2.str);
                }
            }
        }
    }

#if other
using System;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        string[] kn = Console.ReadLine().Split(' ');
        int k = int.Parse(kn[0]), n = int.Parse(kn[1]);
        int max = int.MinValue;
        List<string> list = new List<string>(n);
        for (int i = 0; i < k; i++)
        {
            list.Add(Console.ReadLine());
            if (int.Parse(list[i]) > max)
                max = int.Parse(list[i]);
        }
        for (int i = 0; i < n - k; i++)
        {
            list.Add(max.ToString());
        }
        list.Sort((a, b) => string.Compare(b + a, a + b));
        Console.Write(string.Concat(list));
    }
}
#endif
}
