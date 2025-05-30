using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 30
이름 : 배성훈
내용 : 시간 여행
    문제번호 : 6051번

    스택 문제다.
    연결 리스트를 이용해 과거로 돌아가는게 주된 아이디어 같다.
*/

namespace BaekJoon.etc
{
    internal class etc_1655
    {

        static void Main1655()
        {

            int a = 'a' - '0';
            int s = 's' - '0';

            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            (int prev, int val)[] ret = new (int prev, int val)[n + 1];
            ret[0] = (-1, -1);

            for (int i = 1; i <= n; i++)
            {

                int op = ReadInt();

                if (op == a)
                    ret[i] = (i - 1, ReadInt());
                else if (op == s)
                {

                    if (ret[i - 1].prev != -1) ret[i] = ret[ret[i - 1].prev];
                }
                else
                    ret[i] = ret[ReadInt() - 1];

                sw.Write($"{ret[i].val}\n");
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
using ProblemSolving.Templates.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
namespace ProblemSolving.Templates.Utility {}
namespace System {}
namespace System.Collections.Generic {}
namespace System.IO {}
namespace System.Linq {}
namespace System.Runtime.InteropServices {}

// #nullable disable

public record class State(State prev, long last);

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        Solve(sr, sw);
    }

    public static void Solve(StreamReader sr, StreamWriter sw)
    {
        var n = Int32.Parse(sr.ReadLine());
        var states = new State[1 + n];
        states[0] = new State(null, -1);

        for (var idx = 1; idx <= n; idx++)
        {
            var q = sr.ReadLine().Split(' ');
            var curr = states[idx - 1];

            if (q[0] == "a")
            {
                var k = Int64.Parse(q[1]);
                curr = new State(curr, k);
            }
            else if (q[0] == "s")
            {
                curr = curr.prev;
            }
            else if (q[0] == "t")
            {
                var k = Int64.Parse(q[1]);
                curr = states[k - 1];
            }

            sw.WriteLine(curr.last);
            states[idx] = curr;
        }
    }
}
#endif
}
