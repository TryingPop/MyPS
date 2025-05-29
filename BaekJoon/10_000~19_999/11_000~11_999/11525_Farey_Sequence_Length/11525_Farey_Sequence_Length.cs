using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 28
이름 : 배성훈
내용 : Farey Sequence Length
    문제번호 : 11525번

    오일러 피 함수 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1651
    {

        static void Main1651(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int MAX = 10_000;

            int[] euler, ret;
            int t, s, n;

            Init();

            t = ReadInt();

            while (t-- > 0)
            {

                s = ReadInt();
                n = ReadInt();

                sw.Write($"{s} {ret[n]}\n");
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }

            void Init()
            {

                SetEuler();

                ret = new int[MAX + 1];

                ret[0] = 1;
                ret[1] = 1;

                for (int i = 2; i <= MAX; i++)
                {

                    ret[i] = euler[i];
                }

                for (int i = 1; i <= MAX; i++)
                {

                    ret[i] += ret[i - 1];
                }
            }

            void SetEuler()
            {

                euler = new int[MAX + 1];

                for (int i = 2; i <= MAX; i++)
                {

                    euler[i] = EulerPhi(i);
                }

                int EulerPhi(int _n)
                {

                    int ret = 1;
                    for (int i = 2; i * i <= _n; i++)
                    {

                        if (_n % i != 0) continue;
                        ret *= i - 1;
                        _n /= i;

                        while (_n % i == 0)
                        {

                            _n /= i;
                            ret *= i;
                        }
                    }

                    if (_n > 1) ret *= _n - 1; 
                    return ret;
                }
            }
        }
    }

#if other
int P = int.Parse(Console.ReadLine());

List<int> list = new List<int>();
list.Add(0);

while (P-- > 0)
{
    string[] line = Console.ReadLine().Split(' ');
    int K = int.Parse(line[1]);

    int sum = list[list.Count - 1];
    int start = list.Count;

    while (start <= K)
    {
        HashSet<int> set = new HashSet<int>();
        int number = start;
        for (int i = 2; i <= number; i++)
        {
            if (number % i == 0)
            {
                set.Add(i);
                number /= i--;
            }
        }
        number = start;
        foreach (var item in set)
        {
            number = number / item * (item - 1);
        }
        sum += number;
        start++;
        list.Add(sum);
        //Console.WriteLine("{0}: +{1}", start - 1, sum + 1);
    }
    Console.WriteLine("{0} {1}", line[0], list[K] + 1);
}
#elif other2
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

        var phi = new int[10001];
        for (var idx = 1; idx < phi.Length; idx++)
            phi[idx] = idx;

        for (var prime = 2; prime < phi.Length; prime++)
        {
            if (phi[prime] == prime)
                for (var mult = prime; mult < phi.Length; mult += prime)
                    phi[mult] = phi[mult] / prime * (prime - 1);
        }

        var phisum = new int[phi.Length];
        for (var idx = 1; idx < phi.Length; idx++)
            phisum[idx] = phisum[idx - 1] + phi[idx];

        var t = Int32.Parse(sr.ReadLine());
        while (t-- > 0)
        {
            var kn = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var k = kn[0];
            var n = kn[1];

            sw.WriteLine($"{k} {1 + phisum[n]}");
        }
    }
}
#endif
}
