using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 5
이름 : 배성훈
내용 : 퍼즐
    문제번호 : 1525번

    BFS, 해시셋 문제다
    퍼즐 상황을 배열로 나타내고,
    배열을 숫자로 변형해 이미 방문했는지
    혹은 완성된 형태인지 비교를 통해 풀었다

    상황은 많아야 9!개가 나온다
*/

namespace BaekJoon.etc
{
    internal class etc_0864
    {

        static void Main864(string[] args)
        {

            StreamReader sr;
            HashSet<long> visit;

            long END = 87654321L;
            long[] arr;
            long[] next;
            int zero;

            Solve();
            void Solve()
            {

                Init();

                GetRet();
            }

            void GetRet()
            {

                long chk = ArrToNum(arr);
                if (chk == END)
                {

                    Console.Write(0);
                    return;
                }

                Queue<long> c = new();
                Queue<long> n = new();
                int ret = 1;
                
                c.Enqueue(chk);
                visit.Add(chk);

                while(c.Count > 0)
                {

                    while(c.Count > 0)
                    {

                        long node = c.Dequeue();
                        zero = NumToArr(node);
                        for (int i = 0; i < 4; i++)
                        {

                            chk = Move(i);
                            if (chk == 0L || visit.Contains(chk)) continue;

                            if (chk == END)
                            {

                                Console.Write(ret);
                                return;
                            }

                            visit.Add(chk);
                            n.Enqueue(chk);
                        }
                    }

                    Queue<long> temp = c;
                    c = n;
                    n = temp;

                    ret++;
                }

                Console.Write(-1);
            }

            long Move(int _dir)
            {

                // 시계방향
                if (_dir == 0)
                {

                    if (zero < 3) return 0L;

                    Copy();
                    Change(zero, zero - 3);
                }
                else if (_dir == 1)
                {

                    if (zero % 3 == 2) return 0L;

                    Copy();
                    Change(zero, zero + 1);
                }
                else if (_dir == 2)
                {

                    if (zero > 5) return 0L;

                    Copy();
                    Change(zero, zero + 3);
                }
                else
                {

                    if (zero % 3 == 0) return 0L;

                    Copy();
                    Change(zero, zero - 1);
                }

                return ArrToNum(next);
            }

            void Copy()
            {

                for (int i = 0; i < 9; i++)
                {

                    next[i] = arr[i];
                }
            }

            void Change(int _i, int _j)
            {

                long temp = next[_i];
                next[_i] = next[_j];
                next[_j] = temp;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput());

                zero = -1;
                visit = new(363_000);
                arr = new long[9];
                for (int i = 0; i < 9; i++)
                {

                    int cur = ReadInt();
                    arr[i] = cur;
                    if (cur == 0) zero = i;
                }

                next = new long[9];

                sr.Close();
            }

            int NumToArr(long _num)
            {

                int ret = -1;
                for (int i = 0; i < 9; i++)
                {

                    long cur = _num % 10;
                    arr[i] = cur;
                    _num /= 10;

                    if (cur == 0L) ret = i;
                }

                return ret;
            }

            long ArrToNum(long[] _arr)
            {

                long ret = 0L;
                for (int i = 8; i >= 0; i--)
                {

                    ret = ret * 10 + _arr[i];
                }

                return ret;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c=  sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
var sw = new StreamWriter(Console.OpenStandardOutput());
var sr = new StreamReader(Console.OpenStandardInput());

var currentArray = 0;
var puzzleHash = new HashSet<int>();
var targetHash = 123456789;

for (var i = 0; i < 3; i++)
{
    var input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
    for (var j = 0; j < 3; j++)
    {
        if (input[j] == 0)
        {
            input[j] = 9;
        }
        currentArray = currentArray * 10 + input[j];
    }
}

var max = -1;

var q = new Queue<(int current, int depth)>();
q.Enqueue((currentArray, 0));
puzzleHash.Add(currentArray);
var dx = new int[] { 0, 0, 1, -1 };
var dy = new int[] { 1, -1, 0, 0 };

while (q.Count > 0)
{
    var (current, depth) = q.Dequeue();
    if (current == targetHash)
    {
        max = depth;
        break;
    }
    
    var zeroIndex = current.ToString().IndexOf('9');
    var cx = zeroIndex / 3;
    var cy = zeroIndex % 3;

    for (var d = 0; d < 4; d++)
    {
        var nx = cx + dx[d];
        var ny = cy + dy[d];
        if (nx < 0 || nx >= 3 || ny < 0 || ny >= 3)
        {
            continue;
        }
        
        var next = current.ToString().ToCharArray();
        (next[cx * 3 + cy], next[nx * 3 + ny]) = (next[nx * 3 + ny], next[cx * 3 + cy]);
        var nextInt = int.Parse(new string(next));
        if (puzzleHash.Add(nextInt) == false)
        {
            continue;
        }

        q.Enqueue((nextInt, depth + 1));
    }
}

sw.WriteLine(max);
sw.Close();
sr.Close();
#elif other2

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public static class Program
{
    private static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var targetHash = 123456780;

        var init = new List<int>();
        for (var idx = 0; idx < 3; idx++)
            init.AddRange(sr.ReadLine().Split(' ').Select(Int32.Parse));

        var visited = new HashSet<long>();
        var q = new Queue<(long hash, int movecount)>();
        var inithash = ToHash(init.AsEnumerable().Reverse().ToArray());

        q.Enqueue((inithash, 0));
        visited.Add(inithash);

        while (q.TryDequeue(out var s))
        {
            var (hash, movecount) = s;

            if (hash == targetHash)
            {
                sw.WriteLine(movecount);
                return;
            }

            foreach (var newhash in Derive(hash))
            {
                if (visited.Add(newhash))
                    q.Enqueue((newhash, 1 + movecount));
            }
        }

        sw.WriteLine(-1);
    }

    private static long SetKth(long v, long k, long n)
    {
        var pow = FastPow(10, k);
        return v + (n - GetKth(v, k)) * pow;
    }
    private static long GetKth(long v, long k)
    {
        return (v / FastPow(10, k)) % 10;
    }
    private static IEnumerable<long> Derive(long node)
    {
        var zeropos = 0;
        for (zeropos = 0; zeropos < 9; zeropos++)
            if (GetKth(node, zeropos) == 0)
                break;

        var y = zeropos / 3;
        var x = zeropos % 3;

        foreach (var (dy, dx) in new[] { (-1, 0), (1, 0), (0, 1), (0, -1) })
        {
            var nx = x + dx;
            var ny = y + dy;

            if (nx < 0 || ny < 0 || nx >= 3 || ny >= 3)
                continue;

            var newnode = node;

            newnode = SetKth(newnode, x + 3 * y, GetKth(newnode, nx + 3 * ny));
            newnode = SetKth(newnode, nx + 3 * ny, 0);

            yield return newnode;
        }
    }

    public static long FastPow(long a, long p)
    {
        var rv = 1L;

        while (p > 0)
        {
            if (p % 2 == 1)
                rv *= a;

            a *= a;
            p /= 2;
        }

        return rv;
    }
    public static long ToHash(int[] v) => v.Select((v, idx) => v * FastPow(10, idx)).Sum();
}

#endif
}
