using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 29
이름 : 배성훈
내용 : Fibonacci Game, 수학 게임
    문제번호 : 2373, 2862번

    수학, dp, 게임이론 문제다
    1 ~ 50까지 경우의 수를 탐색하고 -> 규칙을 찾아 풀었다

    아이디어는 다음과 같다
    1, 2, 3, 1, 5, 1, 2, 3, 1, 8, 1, 2, 3, 1, 13, 1, 2, 3, 1, 5, 1, 2, 21, ...
    결과를 확인해보면, 자기보다 작은 수 중에 가장 큰 피보나치 수를 계속해서 빼간다
    만약 0이되면 직전의 피보나치 수가 정답인거 처럼 보였다

    그래서 이분 탐색으로 확인하면서 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0652
    {

        static void Main652(string[] args)
        {

            int MAX = 80;
            long n = long.Parse(Console.ReadLine());

            long[] fibo = new long[MAX + 1];

            fibo[0] = 1;
            fibo[1] = 1;

            for (int i = 2; i <= MAX; i++)
            {

                fibo[i] = fibo[i - 1] + fibo[i - 2];
            }

            long ret = -1;

            // 피보나치 게임 문제는 초기값 확인해야한다
            // 수학 게임만 놓고보면 calc 변수 없이 바로 n으로 확인해도 된다
            long calc = n;

            while (ret == -1)
            {

                int l = 0;
                int r = MAX;

                while (l <= r)
                {

                    int mid = (l + r) / 2;

                    if (fibo[mid] < calc) l = mid + 1;
                    else if (fibo[mid] > calc) r = mid - 1;
                    else
                    {

                        ret = fibo[mid];
                        break;
                    }
                }

                calc -= fibo[r];
            }

            // 피보나치 게임에서는 n개를 못들고 가기에 주석과 같은 필터를 걸쳐야한다
            // if (n == ret) ret = -1;

            Console.WriteLine(ret);
#if MY_DEBUG
            int[] dp = new int[101];

            for (int i = 1; i <= 50; i++)
            {

                for (int j = 1; j <= i; j++)
                {

                    if (DFS(i, j, true) != 1) continue;
                    dp[i] = j;
                    break;
                }
            }

            Console.ReadKey();

            int DFS(int _r, int _n, bool _p)
            {

                if (_r <= _n) return _p ? 1 : -1;

                for (int i = 1; i <= _n; i++)
                {

                    int next = _r - i;
                    int chk = DFS(next, 2 * i, !_p);
                    if (chk == (_p ? -1 : 1)) continue;

                    return _p ? 1 : -1;
                }

                return _p ? -1 : 1;
            }
#endif
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            sw.AutoFlush = true;

            long N = Convert.ToInt64(sr.ReadLine());

            long[] dp = new long[29 + 1];
            dp[0] = 1;
            dp[1] = 2;

            if (N == 2 || N == 3)
            {
                sw.Write(-1);
                return;
            }

            int idx = 0;
            for (int i = 2; i < dp.Length; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2];

                if (N == dp[i])
                {
                    sw.Write(-1);
                    return;
                }

                if (dp[i - 1] < N && N < dp[i])
                {
                    idx = i;
                    break;
                }
            }

            while (dp[idx - 1] < N && N < dp[idx])
            {
                N -= dp[idx - 1];

                for (int i = 0; i < dp.Length; i++)
                {
                    if (N == 1 || N == 2 || N == 3)
                    {
                        sw.Write(N);
                        break;
                    }
                    else if (N == dp[i])
                    {
                        sw.Write(N);
                        break;
                    }
                    else if (N < dp[i])
                    {
                        idx = i;
                        break;
                    }
                }
            }
        }
    }
}
#elif other2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;
using System.Data;

namespace Algorithm
{ 
    class Program2
    {
        static void Main()
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            StringBuilder sb = new StringBuilder();
            
            long n = long.Parse(sr.ReadLine());
            List<long> pivo = new List<long>{1,1};
            for(int i = 0; i < 100; i++)
            {
                pivo.Add(pivo[i]+pivo[i+1]);
                if(pivo[i+2] > 10000000000000000)
                    break;
            }
            if(pivo.Contains(n))
            {
                Console.WriteLine(n);
                return;
            }
            
            pivo.Reverse();
            int cnt = 0;

            foreach (var i in pivo)
            {
                if(i < n)
                {
                    n-=i;
                    if(!pivo.Contains(n))
                        continue;
                    break;
                }
            }
            sw.WriteLine(n);

           
            sr.Close();
            sw.Close(); 
        }
        static int LIS(List<int> seq)
        {
            List<double> lis = new List<double>();
            for(int i = 0; i< seq.Count(); i++)
            {
                int index = lis.BinarySearch(seq[i]);
                if(index < 0)
                    index = Math.Abs(index)-1;
                /*
                else
                {
                    lis[index]-=0.5f;
                    index++;
                }
                */
                if(index >= lis.Count())
                    lis.Add(seq[i]);
                else
                    lis[index] = seq[i];
            }
            return lis.Count();
        }
    }
}
#endif
}
