using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 13
이름 : 배성훈
내용 : 팰린드롬 분할
    문제번호 : 1509번

    etc0025의 아이디어로 부분 문자열이 팰린드롬이 되는지 먼저 기록한다
    그리고 DFS 탐색을 통해 최소 값을 찾는다!
*/

namespace BaekJoon._48
{
    internal class _48_01
    {

        static void Main1(string[] args)
        {

            // 2500자 이므로 그냥 한 방에 읽는다!
            string str = Console.ReadLine();

            int len = str.Length;
            // dp[i, j]의 값은 str의 인덱스가 i부터 j까지 팰린드롬여부!
            bool[,] dp = new bool[len, len];

            // 부분 문자열 중에 팰린드롬 가능한거 찾기!
            // O(N^2)
            for (int i = 0; i < len; i++)
            {

                dp[i, i] = true;
            }

            for (int i = 0; i < len - 1; i++)
            {

                if (str[i] == str[i + 1]) dp[i, i + 1] = true;
            }

            for (int numLen = 2; numLen < len; numLen++)
            {

                for (int i = 0; i < len - numLen; i++)
                {

                    if (str[i] == str[i + numLen] && dp[i + 1, i + numLen - 1]) dp[i, i + numLen] = true;
                }
            }

            // 이제 DFS 탐색
            // 방문 여부
            int[] visit = new int[len];
            Array.Fill(visit, -1);
            // len - 1 ~ len - 1은 팰린드롬이므로 1개의 집합이 된다
            // DFS에서 검색되나, 바로 보이는 경우이므로 미리 채워넣는다
            visit[len - 1] = 1;
            // DFS 탐색 및 결과 출력
#if first
            int ret = DFS(dp, visit, 0, len);
            Console.WriteLine(ret);
#else

            DFS(dp, visit, 0, len);
            Console.WriteLine(visit[0]);
#endif
        }


#if first
        static int DFS(bool[,] _dp, int[] _visit, int _cur, int _len)
        {

            // 탐색 방법에 의해 인덱스가 len이 올 수도 있다 문자열 인덱스는 len - 1까지이므로
            // 해당 문자열 밖에 있어 0반환
            if (_cur > _len - 1) return 0;
            // 이미 탐색한 곳이면 탐색한 결과 반환
            else if (_visit[_cur] != -1) return _visit[_cur];

            // 가장 큰 문자열의 길이에서 하나하나가 팰린드롬!인 경우 가질 수 있는 최대값이다
            // 가질 수 있는 최대 값 넘는 수를 넣어준다
            int ret = 2500;
            for (int i = _len - 1; i >= _cur; i--)
            {

                // 팰린드롬이 되는 경우
                if (_dp[_cur, i])
                {

                    // 쪼개고 뒤에 조사!
                    // 뒤에 + 1은 현재 팰린드롬이 반영된 것이다
                    int chk = DFS(_dp, _visit, i + 1, _len) + 1;
                    ret = ret > chk ? chk : ret;
                }
            }

            // 최소 값 넣는다
            _visit[_cur] = ret;
            return _visit[_cur];
        }
#else
        // int 반환 안하는 경우
        static void DFS(bool[,] _dp, int[] _visit, int _cur, int _len)
        {

            if (_cur > _len - 1
                || _visit[_cur] != -1) return;

            int ret = 2500;
            for (int i = _len - 1; i >= _cur; i--)
            {

                if (_dp[_cur, i])
                {

                    DFS(_dp, _visit, i + 1, _len);

                    int chk = i < _len - 1 ? _visit[i + 1] + 1 : 1;
                    ret = ret > chk ? chk : ret;
                }
            }

            _visit[_cur] = ret;
        }
#endif



    }
#if other
var input = Console.ReadLine()!;
var isP = new bool[input.Length, input.Length + 1];
for (var i = 0; i < input.Length; i++)
    isP[i, i + 1] = true;
for (var i = 0; i < input.Length - 1; i++)
    isP[i, i + 2] = input[i] == input[i + 1];
for (var l = 3; l <= input.Length; l++)
{
    for (var i = 0; i <= input.Length - l; i++)
    {
        isP[i, i + l] =
            input[i] == input[i + l - 1] && isP[i + 1, i + l - 1];
    }
}

var f = new int[input.Length + 1];
f[1] = 1;
for (int l = 2; l <= input.Length; l++)
{
    f[l] = f[l - 1] + 1;
    var i = 0;
    while (i < l - 1)
    {
        if (isP[i, l])
            f[l] = Math.Min(f[i] + 1, f[l]);
        i++;
    }
}
Console.Write(f[input.Length]);
#elif other2
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var s = sr.ReadLine();
        var dp = new int[s.Length];
        dp[0] = 1;

        var isPalindrome = new bool[s.Length, s.Length];

        var q = new Queue<(int stIncl, int edIncl)>();
        for (var idx = 0; idx < s.Length; idx++)
            q.Enqueue((idx, idx));

        for (var idx = 0; idx < s.Length - 1; idx++)
            if (s[idx] == s[idx + 1])
                q.Enqueue((idx, idx + 1));

        while (q.TryDequeue(out var state))
        {
            var (stIncl, edIncl) = state;
            isPalindrome[stIncl, edIncl] = true;

            if (stIncl - 1 >= 0 && edIncl + 1 < s.Length
                && s[stIncl - 1] == s[edIncl + 1])
            {
                q.Enqueue((stIncl - 1, edIncl + 1));
            }
        }

        for (var edIncl = 1; edIncl < s.Length; edIncl++)
        {
            dp[edIncl] = dp[edIncl - 1] + 1;

            for (var stIncl = 0; stIncl <= edIncl; stIncl++)
            {
                var len = edIncl - stIncl + 1;
                if (isPalindrome[stIncl, edIncl])
                    dp[edIncl] = Math.Min(dp[edIncl], (stIncl == 0 ? 0 : dp[stIncl - 1]) + 1);
            }
        }

        sw.WriteLine(dp.Last());
    }
}

#endif
}
