using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 26
이름 : 배성훈
내용 : 뮤탈리스크
    문제번호 : 12869번

    DFS 를 이용해 풀었다
    처음에는 우선순위 큐를 이용해 그리디로 풀면 되는줄 알았으나,
    12, 10, 4의 예제를 본 결과 아님을 알았다

    그래서 DFS 탐색을 이용해 공격의 경우의 수를 세었다
    처음에 21만개를(60 * 60 * 60) 초기화할 이유가 없다고 판단해서 + 1 로 넣었다
    이상없이 72ms로 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0101
    {

        static void Main101(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            int[] input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int[] hp = new int[3];

            for (int i = 0; i < input.Length; i++)
            {

                hp[i] = input[i];
            }

            int[,,] dp = new int[61, 61, 61];
            
            dp[0, 0, 0] = 1;
            int[][] dmg = new int[3][];

            dmg[0] = new int[] { 9, 9, 3, 3, 1, 1 };
            dmg[1] = new int[] { 3, 1, 9, 1, 9, 3 };
            dmg[2] = new int[] { 1, 3, 1, 9, 3, 9 };

            int atk = DFS(dp, hp[0], hp[1], hp[2], dmg);
            Console.WriteLine(atk - 1);
        }

        static int DFS(int[,,] _dp, int _x, int _y, int _z, int[][] _dmg)
        {

            // 맨 밑의 값은 1로 설정
            if (_x <= 0 && _y <= 0 && _z <= 0)
            {

                return 1;
            }

            _x = _x < 0 ? 0 : _x;
            _y = _y < 0 ? 0 : _y;
            _z = _z < 0 ? 0 : _z;

            if (_dp[_x, _y, _z] != 0) return _dp[_x, _y, _z];

            // 주어진 범위의 모든 값보다 큰 값을 잡으면 된다
            int cur = 100;
            for (int i = 0; i < 6; i++)
            {

                int chk = DFS(_dp, _x - _dmg[0][i], _y - _dmg[1][i], _z - _dmg[2][i], _dmg);
                cur = cur > chk ? chk : cur;
            }

            _dp[_x, _y, _z] = cur + 1;

            return cur + 1;
        }
    }

#if other1
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public record struct State(int A, int B, int C)
{
    public State[] Derive()
    {
        return new State[]
        {
            new State(Math.Max(0, A - 9), Math.Max(0, B - 3), Math.Max(0, C - 1)),
            new State(Math.Max(0, A - 9), Math.Max(0, B - 1), Math.Max(0, C - 3)),
            new State(Math.Max(0, A - 3), Math.Max(0, B - 9), Math.Max(0, C - 1)),
            new State(Math.Max(0, A - 3), Math.Max(0, B - 1), Math.Max(0, C - 9)),
            new State(Math.Max(0, A - 1), Math.Max(0, B - 9), Math.Max(0, C - 3)),
            new State(Math.Max(0, A - 1), Math.Max(0, B - 3), Math.Max(0, C - 9)),
        }.Distinct().ToArray();
    }
}

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var s = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var init = new State(s[0], n >= 2 ? s[1] : 0, n >= 3 ? s[2] : 0);
        var visited = new HashSet<State>();
        var q = new Queue<(State s, int attack)>();

        q.Enqueue((init, 0));
        visited.Add(init);

        while (q.TryDequeue(out var state))
        {
            if (state.s.A == 0 && state.s.B == 0 && state.s.C == 0)
            {
                sw.WriteLine(state.attack);
                return;
            }

            foreach (var d in state.s.Derive())
                if (visited.Add(d))
                    q.Enqueue((d, state.attack + 1));
        }
    }
}
#elif other2
var sr = new StreamReader(Console.OpenStandardInput());
var sw = new StreamWriter(Console.OpenStandardOutput());

var N = int.Parse(sr.ReadLine());
var inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);


var A = 0;
var B = 0;
var C = 0;
for (var i = 0; i < N; i++)
{
    if (i == 0) A = inputs[0];
    if (i == 1) B = inputs[1];
    if (i == 2) C = inputs[2];
}

var dp = new int[61, 61, 61];
var board = new int[6, 3] { { 1, 3, 9 }, { 1, 9, 3 }, { 3, 1, 9 }, { 3, 9, 1 }, { 9, 3, 1 }, { 9, 1, 3 } };
var result = int.MaxValue;

void DFS(int a, int b, int c, int depth)
{
    if (a >= A && b >= B && c >= C)
    {
        result = Math.Min(result, depth);
    }

    for (var i = 0; i < 6; i++)
    {
        var nextA = a + board[i, 0];
        var nextB = b + board[i, 1];
        var nextC = c + board[i, 2];

        if (nextA >= 60) nextA = 60;
        if (nextB >= 60) nextB = 60;
        if (nextC >= 60) nextC = 60;
        if (dp[nextA, nextB, nextC] == 0) dp[nextA, nextB, nextC] = 10000;
        if (dp[nextA, nextB, nextC] <= depth + 1) continue;
        dp[nextA, nextB, nextC] = depth + 1;
        DFS(nextA, nextB, nextC, depth + 1);
    }
}

DFS(0, 0, 0, 0);

sw.WriteLine(result);
sw.Flush();
sw.Close();
sr.Close();
#endif
}
