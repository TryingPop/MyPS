using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 28
이름 : 배성훈
내용 : 구슬 찾기
    문제번호 : 2617번

    플로이드 워셜, 최단 경로, DFS 문제다
    아이디어가 안떠올라 힌트를 봤다
    플로이드 워셜을 보고 풀이를 떠올렸다

    아이디어는 다음과 같다 자신보다 무겁고, 가벼운 구슬의 개수를 세어주었다
    무게 순서를 경로로 생각해서 앞에 있는 구슬들이 몇개 있는지 찾는데 플로이드 워셜을 사용했다

    그리고 중앙에 못오는 경우는 (n + 1) / 2번째가 안되는 경우다
    이는 앞에 (n + 1) / 2개 이상의 구슬이 있거나,
    뒤에 (n / 2) + 1개 이상의 구슬이 있는 경우다
    둘 중 어느 한 경우에 포함되면 중앙에 있을 수 없다고 판단했고
    해당 구슬의 개수를 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0376
    {

        static void Main376(string[] args)
        {

            int MAX = 1_000;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int[,] front = new int[n, n];
            int[,] back = new int[n, n];

            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < n; j++)
                {

                    if (i == j) continue;
                    front[i, j] = MAX;
                    back[i, j] = MAX;
                }
            }

            int m = ReadInt();

            for (int i = 0; i < m; i++)
            {

                int f = ReadInt() - 1;
                int b = ReadInt() - 1;

                front[b, f] = 1;
                back[f, b] = 1;
            }

            for (int mid = 0; mid < n; mid++)
            {

                for (int start = 0; start < n; start++)
                {

                    if (front[start, mid] == MAX) continue;
                    for (int end = 0; end < n; end++)
                    {

                        if (front[mid, end] == MAX) continue;
                        int chk = front[start, mid] + front[mid, end];
                        if (chk < front[start, end]) front[start, end] = chk;
                    }
                }
            }
            bool[] impo = new bool[n];
            int[] calc = new int[n];
            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < n; j++)
                {

                    if (i == j || front[i, j] == MAX) continue;
                    calc[i]++;
                }
            }

            int chkF = (n - 1) / 2;
            for (int i = 0; i < n; i++)
            {

                if (calc[i] > chkF) impo[i] = true;

                calc[i] = 0;
            }

            for (int mid = 0; mid < n; mid++)
            {

                for (int start = 0; start < n; start++)
                {

                    if (back[start, mid] == MAX) continue;
                    for (int end = 0; end < n; end++)
                    {

                        if (back[mid, end] == MAX) continue;
                        int chk = back[start, mid] + back[mid, end];
                        if (chk < back[start, end]) back[start, end] = chk;
                    }
                }
            }

            int chkB = n / 2;
            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < n; j++)
                {

                    if (i == j || back[i, j] == MAX) continue;
                    calc[i]++;
                }
            }

            for (int i = 0; i < n; i++)
            {

                if (calc[i] > chkB) impo[i] = true;
                calc[i] = 0; 
            }

            int ret = 0;
            for (int i = 0; i < n; i++)
            {

                if (impo[i]) ret++;
            }

            Console.WriteLine(ret);
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
var sr = new StreamReader(Console.OpenStandardInput());
var sw = new StreamWriter(Console.OpenStandardOutput());

var inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
var N = inputs[0];
var M = inputs[1];

var board = new int[N + 1, N + 1];
var half = N / 2;

for (var m = 0; m < M; m++)
{
    inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
    var left = inputs[0];
    var right = inputs[1];

    board[left, right] = 1;
}

for (var visited = 1; visited <= N; visited++)
{
    for (var left = 1; left <= N; left++)
    {
        for (var right = 1; right <= N; right++)
        {
            if (left == right) continue;
            if (board[left, visited] == 1 && board[visited, right] == 1) board[left, right] = 1;
        }
    }
}

var answer = 0;

for (var left = 1; left <= N; left++)
{
    var bigCount = 0;
    var smallCount = 0;
    for (var right = 1; right <= N; right++)
    {
        if (left == right) continue;
        else if (board[left, right] == 1) bigCount += 1;
        else if (board[right, left] == 1) smallCount += 1;
    }

    if (bigCount > half || smallCount > half) answer++;
}

sw.WriteLine(answer);

sw.Flush();
sw.Close();
sr.Close();
#endif
}
