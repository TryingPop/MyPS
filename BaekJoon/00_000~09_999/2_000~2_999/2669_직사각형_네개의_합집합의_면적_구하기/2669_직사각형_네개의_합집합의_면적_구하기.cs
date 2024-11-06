using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 9
이름 : 배성훈
내용 : 직사각형 네개의 합집합의 면적 구하기
    문제번호 : 2669번

    직사각형 문제를 풀기전에 먼저 풀어본다
    일단 스위핑과 구분구적법 아이디어로 풀었다

    좌표압축을 하면, 1 ~ 100까지 도는걸 많아야 8번 하게 줄일 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0009
    {

        static void Main9(string[] args)
        {

            int[][] squares = new int[4][];

            for (int i = 0; i < 4; i++)
            {

                squares[i] = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            }

            // 구분 구적법 아이디어 사용!
            // 과정에서 스위핑 알고리즘을 썼다
            // 일단 y축 정렬을 한다!
            // 이제 고정된 x좌표에 한해서 정렬 실시!
            Array.Sort(squares, (x, y) =>
            {

                int ret = x[1] - y[1];
                if (ret == 0) ret = y[3] - x[3];

                return ret;
            });

            int sum = 0;
            for (int i = 1; i < 101; i++)
            {

                int len = 0;
                int s = -1;
                int e = -1;
                // y축 길이 재는데에 스위핑이 쓰인다
                for (int j = 0; j < 4; j++)
                {

                    if (squares[j][0] >= i || squares[j][2] < i) continue;

                    if (s == -1) 
                    { 
                        
                        s = squares[j][1];
                        e = squares[j][3];
                        len += e - s;
                    }
                    else if (e < squares[j][1])
                    {

                        s = squares[j][1];
                        e = squares[j][3];
                        len += e - s;
                    }
                    else if (e < squares[j][3])
                    {

                        len += squares[j][3] - e;
                        e = squares[j][3];
                    }
                }

                sum += len;
            }

            Console.WriteLine(sum);
        }
    }

#if other

internal class Program
{
    private static void Main(string[] args)
    {
        var visited = new bool[100, 100];
        var count = 0;
        using (var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
            for (int i = 0; i < 4; i++)
            {
                int lx = ScanInt(sr), ly = ScanInt(sr), rx = ScanInt(sr), ry = ScanInt(sr);
                for (int j = lx; j < rx; j++)
                    for (int k = ly; k < ry; k++)
                        if (!visited[j, k])
                        {
                            visited[j, k] = true;
                            count++;
                        }
            }
        Console.Write(count);
    }

    static int ScanInt(StreamReader sr)
    {
        int c, n = 0;
        while (!((c = sr.Read()) is ' ' or '\n' or -1))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n + c - '0';
        }
        return n - 1;
    }
}
#endif
}
