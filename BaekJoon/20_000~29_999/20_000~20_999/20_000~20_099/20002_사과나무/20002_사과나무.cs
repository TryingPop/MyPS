using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 26
이름 : 배성훈
내용 : 사과나무
    문제번호 : 20002번

    브루트포스, 누적합 문제다
    다른 사람 풀이를 보니, 누적합 배열을 사각형 범위의 합으로 했다
    그래서, 왼쪽, 위쪽을 빼고 그리고 겹치는 구간을 더 했다
    해당 방법으로 푸는게 깔끔해 보인다

    열 합과, 행 합을 따로 뒀는데,
    배열을 2개쓰고 코드 짜는것도 복잡하다
*/

namespace BaekJoon.etc
{
    internal class etc_0351
    {

        static void Main351(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();

            int[,] board = new int[n, n];

            for (int r = 0; r < n; r++)
            {

                for (int c = 0; c < n; c++)
                {

                    board[r, c] = ReadInt();
                }
            }

            sr.Close();

            // 세로 방향 누적합
            int[,] cSum = new int[n + 1, n + 1];
            // 가로 방향 누적합
            int[,] rSum = new int[n + 1, n + 1];

            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < n; j++)
                {

                    rSum[i + 1, j + 1] = board[i, j];
                    rSum[i + 1, j + 1] += rSum[i + 1, j];

                    cSum[j + 1, i + 1] = board[j, i];
                    cSum[j + 1, i + 1] += cSum[j, i + 1];
                }
            }

            int ret = -1_000;

            for (int i = 1; i <= n; i++)
            {

                int first = 0;
                for (int j = 1; j < i; j++)
                {

                    // 처음 칸 설정
                    first += rSum[j, i];
                }

                for (int r = 0; r <= n - i; r++)
                {

                    // 다음칸 행만 밑에 추가
                    // 그리고 위에 행 뺀다
                    first += rSum[r + i, i];
                    first -= rSum[r, i];

                    int calc = first;

                    if (ret < calc) ret = calc;
                    for (int c = 1; c <= n - i; c++)
                    {

                        // 오른쪽 이동은 왼쪽 열을 빼고, 오른쪽 열을 더한다
                        calc += cSum[r + i, c + i] - cSum[r, c + i];
                        calc -= cSum[r + i, c] - cSum[r, c];

                        if (ret < calc) ret = calc;
                    }
                }
            }

            Console.WriteLine(ret);


            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }

                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace no20002try1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 소작농인 형곤이는 오늘도 악독한 신영이때문에 웁니다.

            int size = int.Parse(Console.ReadLine());
            int[,] sums = new int[size + 1, size + 1];

            for (int y = 0; y < size; y++)
            {
                string[] recvLine = Console.ReadLine().Split(' ');
                for (int x = 0; x < size; ++x)
                {
                    sums[x + 1, y + 1] = int.Parse(recvLine[x]) + sums[x + 1, y] + sums[x, y + 1] - sums[x, y];
                }
            }

            int max = int.MinValue;
            for (int x1 = 0; x1 < size; ++x1)
            {
                for (int y1 = 0; y1 < size; ++y1)
                {
                    for (int k = 1; k <= size; ++k)
                    {
                        if (x1 + k > size) break;
                        if (y1 + k > size) break;
                        int one = sums[x1 + k, y1 + k] - sums[x1 + k, y1] - sums[x1, y1 + k] + sums[x1, y1];
                        if (one > max) max = one;
                    }
                }
            }
            Console.WriteLine(max);
        }
    }
}
#elif other2
using System;
using System.IO;
using System.Linq;

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());

        var sum = new int[1 + n][];
        sum[0] = new int[1 + n];

        for (var idx = 1; idx <= n; idx++)
            sum[idx] = sr.ReadLine().Split(' ').Select(Int32.Parse).Prepend(0).ToArray();

        for (var x = 1; x <= n; x++)
            for (var y = 1; y <= n; y++)
                sum[y][x] += sum[y][x - 1];

        for (var y = 1; y <= n; y++)
            for (var x = 1; x <= n; x++)
                sum[y][x] += sum[y - 1][x];

        var maxsum = sum[1][1];

        for (var k = 1; k <= n; k++)
            for (var tox = k; tox <= n; tox++)
                for (var toy = k; toy <= n; toy++)
                {
                    var fromx = tox - k;
                    var fromy = toy - k;

                    maxsum = Math.Max(
                        maxsum,
                        sum[toy][tox] - sum[toy][fromx] - sum[fromy][tox] + sum[fromy][fromx]);
                }

        sw.WriteLine(maxsum);
    }
}
#endif
}
