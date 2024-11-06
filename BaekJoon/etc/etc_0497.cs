using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 10
이름 : 배성훈
내용 : 순열의 순서
    문제번호 : 1722번

    수학, 구현, 조합론 문제다
    정렬된 경우이기에 각자리마다 몇 번째로 큰 수를 넣어줘야하는지 찾아 풀었다
    몇 번째로 큰수는 뒤의 나올 수 있는 경우의 수를 세며 찾았다
    만약 앞에서 뒤에서 4번째에 2번째로 작은 수가 들어와야한다면, 
    뒤에 4번째 자리수를 고정하면 뒤에 3개로 정렬할 수 있는 경우는 총 6가지다
    2번째로 작은 수가 들어오는 경우는 오름차순으로 정렬된 순서가 7 ~ 12 번째만 올 수 있다

    그리고 중복해서 사용하지 않기에, 이미 사용한 숫자면 제외하고 써야한다
    n의 크기가 20이하이기에 n번쨰로 큰 수를 찾는데 그냥for문으로 찾았다
    만약 n이 크다면 남아 있는 원소 중 n번째로 큰 원소를 찾아야하므로 세그먼트 트리를 이용할 것이다
*/

namespace BaekJoon.etc
{
    internal class etc_0497
    {

        static void Main497(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            long[] arr = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
            long[] cnt = new long[n];
            cnt[0] = 1L;
            for (int i = 1; i < n; i++)
            {

                cnt[i] = i * cnt[i - 1];
            }

            StringBuilder sb = new(60);
            bool[] use = new bool[n + 1];
            if (arr[0] == 1)
            {

                for (int i = 0; i < n; i++)
                {

                    int ret = 1;
                    int idx = n - 1 - i;
                    while (arr[1] > cnt[idx])
                    {

                        ret++;
                        arr[1] -= cnt[idx];
                    }

                    for (int j = 1; j <= n; j++)
                    {

                        if (use[j]) continue;
                        ret--;
                        if (ret != 0) continue;
                        use[j] = true;
                        sb.Append($"{j} ");
                        break;
                    }
                }
            }
            else
            {

                long ret = 1;
                for (int i = 1; i <= n; i++)
                {

                    int idx = n - i;
                    use[arr[i]] = true;

                    long len = arr[i];
                    int chk = 0;
                    for (int j = 1; j <= len; j++)
                    {

                        if (use[j]) continue;
                        chk++;
                    }
                    while (chk > 0)
                    {

                        chk--;
                        ret += cnt[idx];
                    }
                }

                sb.Append(ret);
            }

            Console.WriteLine(sb);
        }
    }
#if other
var n = int.Parse(Console.ReadLine()!);
var a = Array.ConvertAll(Console.ReadLine()!.Split(' '), long.Parse);
var arr = new int[n];
long p = P(n);
Console.WriteLine(a[0] == 1 ? string.Join(' ', One()) : $"{Two()}");
long P(int n) => n > 1 ? n * P(n - 1) : 1;
int[] One()
{
    var arr = new int[n];
    a[1]--;
    for (int i = 0; i < n; i++)
    {
        p /= n - i;
        arr[i] = (int)(a[1] / p) + 1;
        a[1] %= p;
    };
    int j;
    for (int i = 1; i <= n; i++)
    {
        for (j = 0; j < n; j++)
        {
            if (arr[j] == i) { j++; break; }
        }
        for (; j < n; j++)
        {
            if (arr[j] >= i) arr[j]++;
        }
    }
    return arr;
}
long Two()
{
    long sum = 0;
    for (int i = 0; i < n - 1; i++)
    {
        p /= n - i;
        sum += p * (a[i + 1] - 1);
        for (int j = i + 1; j <= n; j++)
        {
            if (a[j] > a[i + 1]) a[j]--;
        }
    }
    return sum + 1;
}
#elif other2
Solution2 s = new Solution2();
s.Test();

public class Solution2
{
    public Solution2()
    {
        dp = new List<long>();
        dp.Add(1);
    }

    List<long> dp;
    
    long Factorial(int n)
    {
        if(n < dp.Count)
            return dp[n];

        for (int i = 1; i <= n; i++)
        {
            if(i < dp.Count)
                continue;

            dp.Add(dp[i-1]*i);
        }

        return dp[n];
    }

    public void Test()
    {
        int n = int.Parse(Console.ReadLine());

        string[] input = Console.ReadLine().Split(' ');
        long[] arr = Array.ConvertAll(input, long.Parse);

        if(arr[0] == 1)
            Search1(n,arr[1]);
        else
            Search2(n,arr);
    }

    public void Search1(int n,long k)
    {
        int[] numbers = new int[n+1];

        for (int i = 1; i < numbers.Length; i++)
            numbers[i] = i;

        for (int i = 1; i < n; i++)
        {
            int targetIdx = 1;

            while (k > Factorial(n - i))
            {
                if (numbers[targetIdx] == 0)
                {
                    targetIdx++;
                    continue;
                }

                targetIdx++;

                k = k - Factorial(n - i);
            }

            if(numbers[targetIdx] == 0)
            {
                for (int j = targetIdx+1; j < numbers.Length; j++)
                {
                    if(numbers[j] != 0)
                        {
                            targetIdx = j;
                            break;
                        }
                }
            }

            System.Console.Write($"{numbers[targetIdx]} ");
            numbers[targetIdx] = 0;
        }

        for (int i = 0; i < numbers.Length; i++)
        {
            if(numbers[i] != 0)
            {
                System.Console.WriteLine(i);
                break;
            }
        }
    }

    public void Search2(int n,long[] arr)
    {
        long result = 1;
        int[] numbers = new int[n+1];

        for (int i = 1; i < numbers.Length; i++)
        {
            numbers[i] = i;
        }

        for (int i = 1; i < n; i++)
        {
            for (int j = 1; j < n; j++)
            {
                if(numbers[j] == 0)
                    continue;

                if(arr[i] == j)
                {
                    numbers[j] = 0;
                    break;
                }

                result += Factorial(n-i);
            }
        }

        System.Console.WriteLine(result);
    }
}
#endif
}
