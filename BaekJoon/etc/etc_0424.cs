using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 1
이름 : 배성훈
내용 : 멀티버스 Ⅱ
    문제번호 : 18869번

    정렬, 좌표압축 문제다
    크기가 같은 경우를 고려안해 한 번 틀렸다

    아이디어는 다음과 같다
    먼저 행성 크기로 정렬하고 크기 랭크?에 따라 값을 변경한다
    같은 경우 같은 랭크를 갖게 해야한다(이걸 안해서 한 번 틀렸다!)
        O(N * M * logM)
    이후에 값들을 비교하며 같은 것의 개수를 찾고 2개의 쌍의 개수를 찾는다
        O(N * N * M / 2)

    이렇게 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0424
    {

        static void Main424(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int m = ReadInt();

            int[][] arr = new int[n][];
            for (int i = 0; i < n; i++)
            {

                arr[i] = new int[m];
                for (int j = 0; j < m; j++)
                {

                    arr[i][j] = ReadInt();
                }
            }

            sr.Close();

            SortArrs();
            int ret = GetRet();

            Console.WriteLine(ret);

            int GetRet()
            {

                bool[] use = new bool[n];
                int ret = 0;

                for (int i = 0; i < n; i++)
                {

                    if (use[i]) continue;
                    use[i] = true;

                    int same = 1;
                    for (int j = i + 1; j < n; j++)
                    {

                        if (use[j]) continue;
                        if (IsSameArr(arr[i], arr[j]))
                        {

                            use[j] = true;
                            same++;
                        }
                    }

                    same *= (same - 1);
                    same /= 2;
                    ret += same;
                }

                return ret;
            }

            bool IsSameArr(int[] _arr1, int[] _arr2)
            {

                for (int i = 0; i < m; i++)
                {

                    if (_arr1[i] == _arr2[i]) continue;
                    return false;
                }

                return true;
            }

            void SortArrs()
            {

                (int n, int idx)[] sort = new (int n, int idx)[m];
                
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < m; j++)
                    {

                        sort[j].n = arr[i][j];
                        sort[j].idx = j;
                    }

                    Array.Sort(sort, (x, y) => x.n.CompareTo(y.n));

                    int before = -1;
                    int change = 0;

                    for (int j = 0; j < m; j++)
                    {

                        if (before != sort[j].n)
                        {

                            before = sort[j].n;
                            change++;
                        }
                        arr[i][sort[j].idx] = change;
                    }
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public class Univ : IEquatable<Univ>
{
    private int[] _compressed;

    public Univ(int[] compressed)
    {
        _compressed = compressed;
    }

    public bool Equals(Univ other)
    {
        return _compressed.SequenceEqual(other._compressed);
    }
    public override bool Equals(object obj)
    {
        return obj is Univ u && Equals(u);
    }
    public override int GetHashCode()
    {
        return _compressed.Aggregate((l, r) => 3 * l + r);
    }

    public override string ToString()
    {
        return String.Join(", ", _compressed);
    }
}

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var mn = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var m = mn[0];
        var n = mn[1];

        var universes = new Dictionary<Univ, int>();
        for (var idx = 0; idx < m; idx++)
        {
            var comp = Compress(sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray());
            var univ = new Univ(comp);

            universes[univ] = 1 + universes.GetValueOrDefault(univ);
        }

        var sum = 0L;
        foreach (var count in universes.Values)
            sum += count * (count - 1) / 2;

        sw.WriteLine(sum);
    }

    private static int[] Compress(int[] values)
    {
        var rank = values
            .Distinct()
            .OrderBy(v => v)
            .Select((v, idx) => (v, idx))
            .ToDictionary(p => p.v, p => p.idx);

        return values.Select(v => rank[v]).ToArray();
    }
}

#endif
}
