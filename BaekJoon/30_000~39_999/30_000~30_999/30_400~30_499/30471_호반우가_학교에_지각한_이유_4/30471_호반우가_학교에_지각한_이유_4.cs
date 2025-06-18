using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 1
이름 : 배성훈
내용 : 호반우가 학교에 지각한 이유 4
    문제번호 : 30471번

    그리디, 분리집합 문제다
    아이디어는 다음과 같다
    큰 수를 찾아야 하는데 그리디로 큰 집합을 기준으로 계속해서 합치는게 최대이다
    n개의 인원을 가진 그룹의 최대 경우의 수를 세어보면 
    1 + 2 + ... n = (n * (n - 1)) / 2개 이다
*/

namespace BaekJoon.etc
{
    internal class etc_1011
    {

        static void Main1011(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int[] group, cnt, s;
            int n, m;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            int Find(int _chk)
            {

                int len = 0;
                while (_chk != group[_chk])
                {

                    s[len++] = _chk;
                    _chk = group[_chk];
                }

                while (len > 0)
                {

                    group[s[--len]] = _chk;
                }

                return _chk;
            }

            void Union(int _f, int _b)
            {

                if (_b < _f)
                {

                    int temp = _f;
                    _f = _b;
                    _b = temp;
                }

                group[_b] = _f;
                cnt[_f] += cnt[_b];
            }

            void GetRet()
            {

                long ret = n;
                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    f = Find(f);
                    b = Find(b);
                    if (f == b) sw.Write($"{ret}\n");
                    else
                    {

                        int cntF = cnt[f];
                        int cntB = cnt[b];
                        ret = ret - GetMaxNum(cntF) - GetMaxNum(cntB) + GetMaxNum(cntF + cntB);
                        Union(f, b);

                        sw.Write($"{ret}\n");
                    }
                }

                sr.Close();
                sw.Close();

                long GetMaxNum(int _n)
                {

                    return (1L * _n * (_n - 1)) / 2;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 8);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 8);

                n = ReadInt();
                m = ReadInt();

                group = new int[n + 1];
                cnt = new int[n + 1];
                s = new int[n];
                for (int i = 1; i <= n; i++)
                {

                    group[i] = i;
                    cnt[i] = 1;
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
                    if (c == -1 || c == ' ' || c == '\n') return true;
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
        string[] nm = Console.ReadLine().Split(' ');
        int n = int.Parse(nm[0]), m = int.Parse(nm[1]);
        Init(n);
        long mini = 0;
        StringBuilder sb = new();
        for (int i = 0; i < m; i++)
        {
            string[] ab = Console.ReadLine().Split(' ');
            int a = int.Parse(ab[0]), b = int.Parse(ab[1]);
            a = Find(a); b = Find(b);
            if (a != b)
            {
                mini = mini - Mini(-parent[a]) - Mini(-parent[b]) + Mini(-parent[a] + -parent[b]);
                Union(a, b);
            }
            sb.Append(n + mini);
            if (i + 1 < m)
                sb.Append('\n');
        }
        Console.Write(sb.ToString());
    }
    static long Mini(long x) => (x - 1) * x / 2;
    static int[] parent;
    static void Init(int n)
    {
        parent = new int[n + 1];
        for (int i = 1; i <= n; i++)
        {
            parent[i] = -1;
        }
    }
    static int Find(int x)
    {
        if (parent[x] < 0)
            return x;
        return parent[x] = Find(parent[x]);
    }
    static void Union(int x, int y)
    {
        parent[y] += parent[x];
        parent[x] = y;
    }
}
#endif
}
