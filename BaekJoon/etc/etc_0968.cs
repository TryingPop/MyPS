using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 14
이름 : 배성훈
내용 : ⚾
    문제번호 : 17281번

    구현, 브루트포스 문제다
    1번 타자를 4번으로 세워야하는데,
    4번 타자를 1번으로 세워 1번 틀렸다

    그리고 큐를 이용해서 구현하니 시간초과 떴다
    해당 부분을 배열로 일일히 옮기니 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0968
    {

        static void Main968(string[] args)
        {

            StreamReader sr;
            int[][] arr;
            int n;
            bool[] home;
            int[] order;
            bool[] use;

            int ret;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                ret = 0;
                order = new int[9];
                use = new bool[9];

                order[3] = 0;
                use[3] = true;

                DFS();

                Console.Write(ret);
            }

            void DFS(int _depth = 1)
            {

                if (_depth == 9)
                {

                    ret = Math.Max(ret, GetScore(0, 0));
                    return;
                }

                for (int i = 0; i < 9; i++)
                {

                    if (use[i]) continue;
                    use[i] = true;
                    order[i] = _depth;

                    DFS(_depth + 1);

                    use[i] = false;
                }
            }

            int GetScore(int _s, int _dep)
            {

                if (_dep == n) return 0;
                int next = _s;

                int ret = Simulation(ref next, _dep);
                ret += GetScore(next, _dep + 1);

                return ret;
            }

            int Simulation(ref int _next, int _dep)
            {

                for (int i = 0; i < 3; i++)
                {

                    home[i] = false;
                }
                int o = 0;
                int ret = 0;
                while (o < 3)
                {

                    int op = arr[_dep][order[_next]];
                    _next = _next == 8 ? 0 : _next + 1;
                    if (op == 0) o++;
                    else if (op == 1)
                    {

                        if (home[2]) ret++;
                        home[2] = home[1];
                        home[1] = home[0];
                        home[0] = true;
                    }
                    else if (op == 2)
                    {

                        if (home[2]) ret++;
                        if (home[1]) ret++;
                        home[2] = home[0];
                        home[1] = true;
                        home[0] = false;
                    }
                    else if (op == 3)
                    {

                        if (home[2]) ret++;
                        if (home[1]) ret++;
                        if (home[0]) ret++;
                        home[2] = true;
                        home[1] = false;
                        home[0] = false;
                    }
                    else
                    {

                        if (home[2]) ret++;
                        if (home[1]) ret++;
                        if (home[0]) ret++;
                        ret++;
                        home[2] = false;
                        home[1] = false;
                        home[0] = false;
                    }
                }

                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = new int[9];
                    for (int j = 0; j < 9; j++)
                    {

                        arr[i][j] = ReadInt();
                    }
                }

                home = new bool[3];
                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
namespace boj_17281
{
    class Program
    {
        static int answer = 0;
        static List<int[]> lists = new List<int[]>();
        static List<List<int>> inningResult = new List<List<int>>();

        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());


            for (int i = 0; i < N; i++)
            {
                List<int> list = Array.ConvertAll(Console.ReadLine().Split(), int.Parse).ToList();
                inningResult.Add(list);
            }

            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            Perm(array, 0);

            Console.WriteLine(answer);
        }

        static void Perm(int[] array, int k)
        {
            if (k == array.Length - 1 && array[3] == 0)
            {
                baseball(array, inningResult);
            }
            else
            {
                for (int i = k; i < array.Length; i++)
                {
                    int temp = array[k];
                    array[k] = array[i];
                    array[i] = temp;

                    Perm(array, k + 1);
                    temp = array[k];
                    array[k] = array[i];
                    array[i] = temp;
                }
            }
        }

        static void baseball(int[] array, List<List<int>> lists)
        {
            int now = 0;
            int score = 0;
            foreach (List<int> list in lists)
            {
                int first = 0;
                int second = 0;
                int third = 0;
                int outs = 0;

                while (outs < 3)
                {
                    if (list[array[now]] == 0)
                        outs++;
                    else if (list[array[now]] == 1)
                    {
                        score += third;
                        third = second;
                        second = first;
                        first = 1;
                    }
                    else if (list[array[now]] == 2)
                    {
                        score += second + third;
                        third = first;
                        second = 1;
                        first = 0;
                    }
                    else if (list[array[now]] == 3)
                    {
                        score += first + second + third;
                        third = 1;
                        second = 0;
                        first = 0;
                    }
                    else if (list[array[now]] == 4)
                    {
                        score += 1 + first + second + third;
                        third = 0;
                        second = 0;
                        first = 0;
                    }

                    now = (now + 1) % 9;
                }
            }

            answer = Math.Max(answer, score);
        }
    }
}
#endif
}
