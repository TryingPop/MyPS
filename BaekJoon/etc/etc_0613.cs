using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 25
이름 : 배성훈
내용 : 달팽이
    문제번호 : 1913번

    구현 문제다
    1씩 줄여가며 재자리를 찾아갔다
*/

namespace BaekJoon.etc
{
    internal class etc_0613
    {

        static void Main613(string[] args)
        {

            StreamWriter sw;

            int n;
            int find;

            int[,] ret;

            Solve();

            void Solve()
            {

                n = int.Parse(Console.ReadLine());
                find = int.Parse(Console.ReadLine());

                ret = new int[n, n];
                int input = n * n;
                for (int i = 0; i <= n / 2; i++)
                {

                    
                    ret[i, i] = input--;
                    for (int j = i + 1; j < n - i; j++)
                    {

                        ret[j, i] = input--;
                    }

                    for (int j = i + 1; j < n - i; j++)
                    {

                        ret[n - i - 1, j] = input--;
                    }

                    for (int j = n - 2 - i; j >= i; j--)
                    {

                        ret[j, n - 1 - i] = input--;
                    }

                    for (int j = n - 2 - i; j > i; j--)
                    {

                        ret[i, j] = input--;
                    }
                }

                sw = new(Console.OpenStandardOutput());

                (int x, int y) ret2 = (0, 0);
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        sw.Write($"{ret[i, j]} ");
                        if (ret[i, j] == find)
                        {

                            ret2 = (i + 1, j + 1);
                        }
                    }

                    sw.Write('\n');
                }

                sw.Write($"{ret2.x} {ret2.y}");
                sw.Close();
            }

        }
    }

#if other
int n = int.Parse(Console.ReadLine());
int v = int.Parse(Console.ReadLine());

var snail = new int[n, n];
int num = n * n;
int mid = n / 2;
var (x, y) = (0, 0);
var (vx, vy) = (-1, -1);
int dir = 0;

while (true)
{
    if (num == v)
        (vx, vy) = (x + 1, y + 1);

    snail[x, y] = num--;

    if ((x, y) == (mid, mid))
        break;

    switch (dir)
    {
        case 0: if (x + 1 >= n || snail[x + 1, y] != 0) {dir++; y++;} else x++; break;
        case 1: if (y + 1 >= n || snail[x, y + 1] != 0) {dir++; x--;} else y++; break;
        case 2: if (x - 1 < 0 || snail[x - 1, y] != 0) {dir++; y--;} else x--; break;
        case 3: if (y - 1 < 0 || snail[x, y - 1] != 0) {dir = 0; x++;} else y--; break;
    }
}

using (var w = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
{
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
            w.Write(snail[i, j] + " ");

        w.WriteLine();
    }

    w.WriteLine($"{vx} {vy}");
}

#elif other2
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static StringBuilder sb = new StringBuilder();
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));           
            int n = int.Parse(input.ReadLine());
            int target = int.Parse(input.ReadLine());
            int[,] map = new int[n, n];
            make(map, n - 1);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sb.Append($"{map[i,j]} ");
                }
                sb.Append('\n');
            }
            find(n, map, target);
            output.WriteLine(sb);

            input.Close();
            output.Close();
        }
        static void make(int[,] map, int n)
        {
            int start = 0;
            int num = (n+1) * (n+1);
            while (num > 1)
            {

                for (int i = start; i < n; i++)
                    map[i, start] = num--;
                for (int i = start; i < n; i++)
                    map[n, i] = num--;
                for (int i = n; i > start; i--)
                    map[i, n] = num--;
                for (int i = n; i > start; i--)
                    map[start, i] = num--;                
                n--;                
                start++;
            }
            map[start, n] = 1;
        }
        static void find(int n,int[,] map,int target)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (map[i, j] == target)
                    {
                        sb.Append($"{i+1} {j+1}");
                        return;
                    }
                }
            }
        }
    }
}
#endif
}
