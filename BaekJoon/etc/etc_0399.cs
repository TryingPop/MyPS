using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 30
이름 : 배성훈
내용 : 한윤정이 이탈리아에 가서 아이스크림을 사먹는데
    문제번호 : 2422번

    브루트포스 문제다
    DFS 방법으로 완전탐색을 진행했다

    3개를 셀렉트해야 하기에 삼중 포문으로 탐색하는게 더 빨라 보인다
*/
namespace BaekJoon.etc
{
    internal class etc_0399
    {

        static void Main399(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int m = ReadInt();

            bool[,] combi = new bool[n + 1, n + 1];
            int[] select = new int[3];

            for (int i = 0; i < m; i++)
            {

                int f = ReadInt();
                int b = ReadInt();

                combi[f, b] = true;
                combi[b, f] = true;
            }
            sr.Close();

            int ret = DFS();

            Console.WriteLine(ret);

            int DFS(int _depth = 0, int _s = 1)
            {

                if (_depth == 3)
                {

                    for (int i = 0; i < 3; i++)
                    {

                        for (int j = i + 1; j < 3; j++)
                        {

                            if (combi[select[i], select[j]]) return 0;
                        }
                    }

                    return 1;
                }

                int ret = 0;

                for (int i = _s; i <= n; i++)
                {

                    select[_depth] = i;
                    ret += DFS(_depth + 1, i + 1);
                }

                return ret;
            }

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
using System;
using System.Collections;
using System.Collections.Generic;
using static System.Console;

namespace C_Sharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int cnt = 0;
            bool[][] map = new bool[201][];
            for (int i = 0; i < 201; i++)
                map[i] = new bool[201];
            string[] g = Console.ReadLine().Split();
            int n = int.Parse(g[0]);
            int m = int.Parse(g[1]);

            while (m-- > 0)
            {
                g = Console.ReadLine().Split();
                int a = int.Parse(g[0]);
                int b = int.Parse(g[1]);
                map[a][b] = true;
                map[b][a] = true;
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = i + 1; j <= n; j++)
                {
                    for (int k = j + 1; k <= n; k++)
                    {
                        if (map[i][j] == true || map[i][k] == true || map[j][k] == true)
                            continue;
                        cnt++;
                    }
                }
            }

            WriteLine(cnt);
        }
    }
}

#endif
}
