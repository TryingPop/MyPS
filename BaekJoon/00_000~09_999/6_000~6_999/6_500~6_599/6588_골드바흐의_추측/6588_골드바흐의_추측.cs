using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 17
이름 : 배성훈
내용 : 골드바흐의 추측
    문제번호 : 6588번

    처음에는 소수들을 찾은 뒤 N^2으로 비교하려고 했었다
    그런데 해당 방법은 소수의 개수를 보니, 1만개 이상 검색해야하는 경우 시간초과가 날거 같았다 (7 ~ 8만개)

    그래서 방법을 바꿔 둘 다 소수인 특징을 이용했다
    배열이 아닌 해시셋에 저장했고, 반대쪽이 해시셋에 있는지 탐색했었다
    (해시셋은 해시테이블을 만들어 데이터를 저장하는 방식이며 포함 유무를 확인하는 속도가 빠르다)
    해당 방법으로 처음 제출했고

    다른 사람 풀이를 보니 배열 2개로 하는방법이 보였다
    그래서 비슷하게 수정해서 제출해봤다

    464ms -> 436ms로 줄었다
*/

namespace BaekJoon.etc
{
    internal class etc_0050
    {

        static void Main50(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            int[] primes = new int[80_000];
            int cnt = 0;
            bool[] chkPrime = new bool[1_000_000];

            // HashSet<int> set = new HashSet<int>(80_000);

            for (int i = 3; i < 1_000_001; i++)
            {

                bool chk = true;
                for (int j = 2; j < i; j++)
                {

                    if (j * j > i) break;

                    if (i % j != 0) continue;

                    chk = false;
                    break;
                }

                if (chk)
                {

                    // set.Add(i);
                    primes[cnt++] = i;
                    chkPrime[i] = true;
                }
            }

            while(true)
            {

                int find = ReadInt(sr);

                if (find == 0) break;
                int min = 3;
                int max = 3 - find;

                for (int i = 0; i < cnt; i++)
                {

                    int cur = primes[i];

                    if (cur >= find) break;

                    int chk = find - primes[i];
                    if (chkPrime[chk])
                    {

                        min = cur;
                        max = chk;
                        break;
                    }
                }

                /*
                foreach (int prime in set)
                {

                    int other = find - prime;

                    if (set.Contains(other))
                    {

                        min = prime;
                        max = other;
                        break;
                    }
                }
                */

                sw.Write($"{find} = {min} + {max}\n");
            }

            sr.Close();
            sw.Close();
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
using System;
using System.IO;

using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
int input;

var primes = new int[78498];
var isNotPrime = new bool[1_000_000];
isNotPrime[1] = true;

int count = 0;
for (int i = 2; i < isNotPrime.Length; i++)
{
    if (isNotPrime[i])
        continue;

    primes[count++] = i;
    for (int j = 2; j * i < isNotPrime.Length; j++)
        isNotPrime[i * j] = true;
}

using var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
while ((input = ScanInt()) != 0)
{
    for (int i = 0; true; i++)
    {
        var a = primes[i];
        var b = input - a;
        if (!isNotPrime[b])
        {
            sw.WriteLine($"{input} = {a} + {b}");
            break;
        }
    }
}

int ScanInt()
{
    int c, ret = 0;
    while ((c = sr.Read()) != '\n' && c != ' ' && c != -1)
    {
        if (c == '\r')
        {
            sr.Read();
            break;
        }
        ret = 10 * ret + c - '0';
    }
    return ret;
}
#endif
}
