using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 20
이름 : 배성훈
내용 : 소인수 분해
    문제번호 : 4355번

    오일러 피 함수 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0710
    {

        static void Main710(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            Solve();

            void Solve()
            {

                Init();

                while(true)
                {

                    int n = ReadInt();
                    if (n == 0) break;
                    sw.Write($"{GetPhi(n)}\n");
                }

                sr.Close();
                sw.Close();
            }

            int GetPhi(int _n)
            {

                if (_n == 0) return 0;
                int ret = 1;
                for (int i = 2; i <= _n; i++)
                {

                    if (i * i > _n) break;
                    if (_n % i != 0) continue;

                    ret *= i - 1;
                    _n /= i;

                    while(_n % i == 0)
                    {

                        _n /= i;
                        ret *= i;
                    }
                }

                if (_n > 1) ret *= _n - 1;
                return ret;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
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
System.Text.StringBuilder sb = new System.Text.StringBuilder();

int las = (int)Math.Sqrt(1000000000);
HashSet<int> prime = new HashSet<int>();
Prime(las);

int N;
while ((N = int.Parse(Console.ReadLine())) != 0)
{
    if (N == 1) { sb.AppendLine("0"); continue; }
    if (prime.Contains(N)) { sb.AppendLine((N - 1).ToString()); continue; }

    long count = N;
    foreach (var item in prime)
    {
        if (N % item == 0)
        {
            count = count * (item - 1) / item;

            while (N % item == 0)
                N /= item;
        }
    }

    if (N != 1) count = count * (N - 1) / N;
    sb.AppendLine(count.ToString());
}

Console.Write(sb);
return;

void Prime(int las)
{
    prime.Add(2);
    for(int i = 3; i <= las; i+= 2)
    {
        if(isPrime(i))
        { prime.Add(i); }
    }
}

bool isPrime(int num)
{
    foreach(var item in prime)
    {
        if (num % item == 0) return false;
        if (item * item > num) break;
    }
    return true;
}

#endif
}
