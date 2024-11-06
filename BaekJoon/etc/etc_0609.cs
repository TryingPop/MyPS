using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 24
이름 : 배성훈
내용 : 또 수열 문제야
    문제번호 : 31229번

    수학, 애드 혹, 해 구성하기 문제다
    소수를 이용해 풀었다

    다른 사람의 풀이를 보니 홀수, 짝수로 푸는게 더 좋아보인다
*/

namespace BaekJoon.etc
{
    internal class etc_0609
    {

        static void Main609(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            bool[] notPrime = new bool[50_001];

            for (int i = 2; i <= 50_000; i++)
            {

                if (notPrime[i]) continue;

                for (int j = 2 * i; j <= 50_000; j += i)
                {

                    notPrime[j] = true;
                }
            }

            using (StreamWriter sw = new (Console.OpenStandardOutput(), bufferSize: 65536))
            {

                int chk = 0;
                for (int i = 2; i < 50_000; i++)
                {

                    if (notPrime[i]) continue;

                    sw.Write($"{i} ");
                    chk++;
                    if (chk == n) break;
                }
            }
        }
    }

#if other
using System;
using System.Text;

public class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        StringBuilder sb = new();
        for (int i = 0, v = 1; i < n; i++, v += 2)
        {
            sb.Append(v);
            if (i + 1 < n)
                sb.Append(' ');
        }
        Console.Write(sb.ToString());
    }
}
#elif other2
int n = int.Parse(Console.ReadLine());

using (var w = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
{
    int cnt = 0;
    for (int i = 0; cnt < n; i++)
    {
        if (IsPrime(i))
        {
            w.Write(i + " ");
            cnt++;
        }
    }
}

bool IsPrime(int v)
{
    if (v == 2 || v == 3)
        return true;

    if (v <= 1 || v % 2 == 0 || v % 3 == 0)
        return false;

    for (int i = 5; i * i <= v; i += 6)
    {
        if (v % i == 0 || v % (i + 2) == 0)
            return false;
    }

    return true;
}
#elif other3
// cs31229 - rby
// 2024-01-29 17:47:39
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace cs31229
{
    internal class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            int N = int.Parse(sr.ReadLine());

            int num = 1;
            for(int i = 0; i < N; i++)
            {
                sb.AppendFormat("{0} ", num);
                num += 2;
            }


            sw.Write(sb);
            sw.Close();
            sr.Close();
        }
    }
}

#endif
}
