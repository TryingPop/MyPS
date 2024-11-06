using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 17
이름 : 배성훈
내용 : 에너지 모으기
    문제번호 : 16198번

    브루트포스, 백트래킹 문제다
    범위가 10이라, 하나씩 확인해서 풀어 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0559
    {

        static void Main559(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            int n = ReadInt();
            int[] arr = new int[n];
            bool[] visit = new bool[n];
            int ret = 0;

            Solve();

            sr.Close();

            void Solve()
            {

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                DFS();
                Console.WriteLine(ret);
            }

            void DFS(int _depth = 0, int _calc = 0)
            {

                if (_depth == n - 2)
                {

                    if (ret < _calc) ret = _calc;
                    return;
                }

                for (int i = 1; i < n - 1; i++)
                {

                    if (visit[i]) continue;
                    visit[i] = true;
                    int l = 0;
                    for (int j = i - 1; j >= 0; j--)
                    {

                        if (visit[j]) continue;
                        l = j;
                        break;
                    }

                    int r = n - 1;
                    for (int j = i + 1; j < n; j++)
                    {

                        if (visit[j]) continue;
                        r = j;
                        break;
                    }

                    DFS(_depth + 1, _calc + (arr[l] * arr[r]));
                    visit[i] = false;
                }
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
using StreamWriter wt = new(Console.OpenStandardOutput());
using StreamReader rd = new(Console.OpenStandardInput());
int n = int.Parse(rd.ReadLine()), max = 0;
var a = rd.ReadLine().Split().Select(int.Parse).ToList();
Max(a, 0);
wt.Write(max);

void Max(List<int> l, int sum)
{
    if (l.Count == 2)
    {
        max = Math.Max(max, sum);
        return;
    }

    for (int i = 1; i < l.Count - 1; i++)
    {
        int v = l[i];
        l.RemoveAt(i);
        Max(l, sum + l[i - 1] * l[i]);
        l.Insert(i, v);
    }
}
#endif
}
