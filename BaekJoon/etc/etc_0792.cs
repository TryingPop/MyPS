using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 4
이름 : 배성훈
내용 : 1, 2, 3 더하기 2
    문제번호 : 12101번

    브루트포스, 백트래킹 문제다
    해당 자리에 작은수부터 배치하고 큰수로 늘려가는 DFS 탐색을 했다
    그러면 정렬 순서로 최대 깊이까지 탐색을 하고,
    원하는 순서에 번호가 되면, 정답이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0792
    {

        static void Main792(string[] args)
        {

            int[] arr, ret;
            int cur, ord;

            Solve();
            void Solve()
            {

                string[] temp = Console.ReadLine().Split();
                int r = int.Parse(temp[0]);
                ord = int.Parse(temp[1]);

                arr = new int[r];
                ret = new int[r];
                ret[0] = -1;
                cur = 0;

                DFS(r, 0);
                using (StreamWriter sw = new(Console.OpenStandardOutput()))
                {

                    sw.Write($"{ret[0]}");
                    for (int i = 1; i < r; i++)
                    {

                        int cur = ret[i];
                        if (cur == 0) break;
                        sw.Write($"+{cur}");
                    }
                }
            }

            void DFS(int _r, int _len)
            {

                if (_r == 0)
                {

                    cur++;
                    if (cur == ord)
                    {

                        for (int i = 0; i < _len; i++)
                        {

                            ret[i] = arr[i];
                        }
                    }

                    return;
                }

                for (int i = 1; i <= 3; i++)
                {

                    int chk = _r - i;
                    if (chk < 0) break;
                    arr[_len] = i;
                    DFS(chk, _len + 1);
                    arr[_len] = 0;
                }
            }
        }
    }

#if other
using System.Text;
using StreamWriter wt = new(Console.OpenStandardOutput());
using StreamReader rd = new(Console.OpenStandardInput());
var input = rd.ReadLine().Split().Select(int.Parse).ToArray();
int n = input[0], k = input[1], d = 0;

Sum(new StringBuilder().Append('1'), 1);
Sum(new StringBuilder().Append('2'), 2);
Sum(new StringBuilder().Append('3'), 3);
wt.Write(-1);

void Sum(StringBuilder sb, int sum)
{
    if (sum > n) return;
    if (sum == n)
    {
        d++;
        if (d == k)
        {
            wt.Write(sb);
            wt.Close();
            Environment.Exit(0);
        }
    }

    sb.Append("+1");
    Sum(sb, sum + 1);
    sb.Remove(sb.Length - 2, 2);
    sb.Append("+2");
    Sum(sb, sum + 2);
    sb.Remove(sb.Length - 2, 2);
    sb.Append("+3");
    Sum(sb, sum + 3);
    sb.Remove(sb.Length - 2, 2);
}
#elif other2
// BOJ_12101


using System.Text;

int[] inputnk = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
int n = inputnk[0];
int k = inputnk[1];
SortedSet<string> ss = new SortedSet<string>();
char[] arr = new char[15];

Dfs(0, 0);
int count = 0;
foreach (var one in ss)
{
    if(count++ == k - 1)
    {
        Console.WriteLine(one);
        return;
    }
}
Console.WriteLine(-1);



void Dfs(int sum, int length)
{
    if(sum == n)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            sb.Append(arr[i]);
            if(i != length - 1)
                sb.Append('+');
        }
        ss.Add(sb.ToString());
        return;
    }
    else if (sum > n)
        return;

    for (int i = 1; i <= 3; i++)
    {
        arr[length] = (char)(i + '0');
        Dfs(sum + i, length + 1);
    }
}
#elif other3
var s = Console.ReadLine().Split();
int n = int.Parse(s[0]), k = int.Parse(s[1]);
int reached = 0;
var result = new int[n];
Wow(new int[n], 0, 0);
Console.Write(result[0] == 0 ? -1 : string.Join('+', result.Where(x => x != 0)));

void Wow(int[] arr, int len, int sum)
{
    if (result[0] != 0)
        return;

    if (sum == n)
    {
        if (++reached == k)
            Array.Copy(arr, result, len);

        return;
    }

    if (sum > n)
        return;

    for (int i = 1; i <= 3; i++)
    {
        arr[len] = i;
        Wow(arr, len + 1, sum + i);
    }
}
#endif
}
