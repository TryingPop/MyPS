using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 1
이름 : 배성훈
내용 : 최대최소
    문제번호 : 1999번

    dp, 자료구조 문제다.
    아이디어는 다음과 같다.

    2 x 2의 최댓값은 1 x 1 사각형의 인접한 4개의 최댓값과 같다.
    3 x 3의 최댓값은 2 x 2 사각형의 인접한 4개의 최댓값과 같다.
    이를 이용하면 4 x B^3의 시간에 해결 가능하다.

    반면 단순히 B범위의 모든 사각형을 조사하면 B^4이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1507
    {

        static void Main1507(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n, b, k;
            int[][] board, min, max;

            Input();

            SetMinMax();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < k; i++)
                {

                    int f = ReadInt() - 1;
                    int t = ReadInt() - 1;

                    sw.Write($"{max[f][t] - min[f][t]}\n");
                }
            }

            void Input()
            {

                n = ReadInt();
                b = ReadInt();
                k = ReadInt();

                board = new int[n][];

                for (int i = 0; i < n; i++)
                {

                    board[i] = new int[n];
                    for (int j = 0; j < n; j++)
                    {

                        board[i][j] = ReadInt();
                    }
                }
            }

            void SetMinMax()
            {

                min = new int[n][];
                max = new int[n][];

                for (int i = 0; i < n; i++)
                {

                    min[i] = new int[n];
                    max[i] = new int[n];
                    for (int j = 0; j < n; j++)
                    {

                        min[i][j] = board[i][j];
                        max[i][j] = board[i][j];
                    }
                }

                int[][] calc = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    calc[i] = new int[n];
                }

                for (int size = 1; size < b; size++)
                {

                    for (int i = 0; i < n - size; i++)
                    {

                        for (int j = 0; j < n - size; j++)
                        {

                            int chk1 = Math.Min(min[i][j], min[i][j + 1]);
                            int chk2 = Math.Min(min[i + 1][j], min[i + 1][j + 1]);
                            calc[i][j] = Math.Min(chk1, chk2);
                        }
                    }

                    int[][] temp = min;
                    min = calc;
                    calc = temp;
                }

                for (int size = 1; size < b; size++)
                {

                    for (int i = 0; i < n - size; i++)
                    {

                        for (int j = 0; j < n - size; j++)
                        {

                            int chk1 = Math.Max(max[i][j], max[i][j + 1]);
                            int chk2 = Math.Max(max[i + 1][j], max[i + 1][j + 1]);
                            calc[i][j] = Math.Max(chk1, chk2);
                        }
                    }

                    int[][] temp = max;
                    max = calc;
                    calc = temp;
                }
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;

                    ret = c - '0';

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }

#if other
using System;
using System.Text;

public class Program
{
    static void Main()
    {
        int[] nbk = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        int n = nbk[0], b = nbk[1], k = nbk[2];
        byte[,] array = new byte[n + 1, n + 1];
        for (int i = 1; i <= n; i++)
        {
            byte[] row = Array.ConvertAll(Console.ReadLine().Split(' '), byte.Parse);
            for (int j = 1; j <= n; j++)
            {
                array[i, j] = row[j - 1];
            }
        }
        byte[,,] rightMin = new byte[n + 1, n + 1, b],
                 rightMax = new byte[n + 1, n + 1, b],
                 downMin = new byte[n + 1, n + 1, b],
                 downMax = new byte[n + 1, n + 1, b];
        for (int i = 1; i <= n; i++)
        {
            for (int j = n; j >= 1; j--)
            {
                rightMin[i, j, 0] = rightMax[i, j, 0] = array[i, j];
                for (int l = 1; j + l <= n && l < b; l++)
                {
                    rightMin[i, j, l] = Math.Min(array[i, j], rightMin[i, j + 1, l - 1]);
                    rightMax[i, j, l] = Math.Max(array[i, j], rightMax[i, j + 1, l - 1]);
                }
            }
        }
        for (int i = 1; i <= n; i++)
        {
            for (int j = n; j >= 1; j--)
            {
                downMin[j, i, 0] = downMax[j, i, 0] = array[j, i];
                for (int l = 1; j + l <= n && l < b; l++)
                {
                    downMin[j, i, l] = Math.Min(array[j, i], downMin[j + 1, i, l - 1]);
                    downMax[j, i, l] = Math.Max(array[j, i], downMax[j + 1, i, l - 1]);
                }
            }
        }
        byte[,,] squareMin = new byte[n + 1, n + 1, b],
                 squareMax = new byte[n + 1, n + 1, b];
        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                for (int l = 0; l < b; l++)
                {
                    squareMin[i, j, l] = squareMax[i, j, l] = 255;
                }
            }
        }
        byte Min(int r, int c, int size)
        {
            if (size == 0)
                return array[r, c];
            if (squareMin[r, c, size] != 255)
                return squareMin[r, c, size];
            return Math.Min(Math.Min(rightMin[r, c, size], downMin[r, c, size]), Min(r + 1, c + 1, size - 1));
        }
        byte Max(int r, int c, int size)
        {
            if (size == 0)
                return array[r, c];
            if (squareMax[r, c, size] != 255)
                return squareMax[r, c, size];
            return Math.Max(Math.Max(rightMax[r, c, size], downMax[r, c, size]), Max(r + 1, c + 1, size - 1));
        }
        StringBuilder sb = new();
        for (int i = 0; i < k; i++)
        {
            string[] rc = Console.ReadLine().Split(' ');
            int r = int.Parse(rc[0]), c = int.Parse(rc[1]);
            sb.Append(Max(r, c, b - 1) - Min(r, c, b - 1));
            if (i + 1 < k)
                sb.Append('\n');
        }
        Console.Write(sb.ToString());
    }
}
#elif other2
// Baekjoon01999.cpp
// #include <iostream>

using namespace std;

using pii = pair<int, int>;

int main(void) {
    ios_base::sync_with_stdio(false);
    cin.tie(0); cout.tie(0);

    int n, b, k;
    cin >> n >> b >> k;

    int mat[250][250];
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            cin >> mat[i][j];

    int ans[250][250];
    for (int i = 0; i < n - b + 1; i++) {
        int table[251] = { 0, };
        for (int j = 0; j < n ; j++) {
            if (j >= b)
                for (int k = 0; k < b; k++)
                    table[mat[i + k][j - b]]--;

            for (int k = 0; k < b; k++)
                table[mat[i + k][j]]++;

            if (j >= (b - 1)) {
                pii result = { 250, 0 }; // min, max
                for (int k = 0; k <= 250; k++) {
                    if (table[k]) {
                        result.first = min(result.first, k);
                        result.second = max(result.second, k);
                    }
                }

                ans[i][j - b + 1] = result.second - result.first;
            }
        }
    }

    while (k--) {
        int i, j;
        cin >> i >> j;

        cout << ans[i - 1][j - 1] << '\n';
    }

    return 0;
}

#endif
}
