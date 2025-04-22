using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 22
이름 : 배성훈
내용 : 슈퍼 소수
    문제번호 : 31216번

    수학, 정수론, 소수판정 문제다
    소수 수열로 나열 했을 때, 인덱스가 소수인 값을 슈퍼 소수라 한다
    대표적으로 3이다

    테스트 케이스는 1000개이고, 찾을 슈퍼소수 인덱스는 1 ~ 3000이다
    예제에서 최대 한도를 3000번째가 318137임을 알려준 친절한 문제이므로
    범위는 바로 정해진다
*/

namespace BaekJoon.etc
{
    internal class etc_0322
    {

        static void Main322(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt();

            bool[] isPrime = new bool[318_138];
            int[] dp = new int[3_001];

            ChkPrime();
            ChkSuperPrime();

            while(test-- > 0)
            {

                int n = ReadInt();
                sw.WriteLine(dp[n]);
            }

            sr.Close();
            sw.Close();
            void ChkSuperPrime()
            {

                int primeIdx = 0;
                int dpIdx = 1;
                for (int i = 2; i < isPrime.Length; i++)
                {

                    if (!isPrime[i]) continue;

                    primeIdx++;
                    if (!isPrime[primeIdx]) continue;
                    dp[dpIdx++] = i;
                }
            }

            void ChkPrime()
            {

                for (int i = 2; i <= 318137; i++)
                {

                    isPrime[i] = true;
                    for (int j = 2; j < i; j++)
                    {

                        if (j * j > i) break;
                        if (i % j != 0) continue;
                        isPrime[i] = false;
                        break;
                    }
                }
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
var reader = new Reader();
var t = reader.NextInt();
var eratos = new EratosSeive(500000);
var superPrimes = new (int p, int factor)[3001];
var superPrimeCount = 0;

using (var w = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
while (t-- > 0)
{
    var n = reader.NextInt();

    if (superPrimes[n].p != 0)
        w.WriteLine(superPrimes[n - 1].p);

    var (nextPrime, primeCount) = superPrimeCount == 0 ? (0, 0) : superPrimes[superPrimeCount - 1];
    while (superPrimeCount < n)
    {
        nextPrime = eratos.NextPrime(nextPrime);
        primeCount++;

        if (eratos.IsPrime(primeCount))
            superPrimes[superPrimeCount++] = (nextPrime, primeCount);
    }

    w.WriteLine(superPrimes[n - 1].p);
}

class EratosSeive
{
    bool[] seive;
    int cursor;

    public EratosSeive(int limit)
    {
        seive = new bool[limit + 1];
        seive[0] = true;
        seive[1] = true;

        for (int i = 4; i <= limit; i += 2)
            seive[i] = true;

        for (int i = 6; i <= limit; i += 3)
            seive[i] = true;

        for (int i = 5; i * i <= limit; i += 6)
        {
            for (int n = i * i; n <= limit; n += i)
                seive[n] = true;

            int j = i + 2;
            for (int n = j * j; n <= limit; n += j)
                seive[n] = true;
        }

        cursor = 0;
    }

    public bool IsPrime(int num) => !seive[num];

    public int NextPrime(int start = -1)
    {
        if (start != -1)
            cursor = start;

        while (cursor < seive.Length)
        {
            cursor++;
            if (seive[cursor] == false)
                return cursor;
        }

        return -1;
    }
}

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
}
#endif
}
