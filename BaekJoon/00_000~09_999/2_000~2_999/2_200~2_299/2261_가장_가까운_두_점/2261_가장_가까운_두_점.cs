using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 12
이름 : 배성훈
내용 : 가장 가까운 두 점
    문제번호 : 2261번

    스위핑, 분할 정복, 기하학 문제다.
    아이디어는 다음과 같다.

    가장 작은 점을 찾는데,
    모든 점의 거리를 조사하면 N^2번 조사를 하게 된다.

    그런데, x의 범위로 정렬한 뒤 구간을 반으로 나눠 최솟값을 찾는다.
    x로 정렬했으니 중앙에 있는 점으로 나누던, x의 중앙이던 상관없다.
    해당 구간에서의 최솟값은 최솟값의 후보가 된다.

    왜냐하면, 서로 다른 구간에서 최솟값이 나올 수 있기 때문이다.
    이 경우의 최솟값은 그리디로 중앙에 있는 점과 x거리가 양 구간의 최솟값 이하인 경우의 점들만 해당된다.
    (귀류법으로 접근하면 된다.)

    이렇게 해당 구간의 점들의 거리를 조사해 최솟값을 찾아주면 된다.
    찾는건 전 구간이므로 최솟값을 전역변수 min로 빼서,
    양 구간의 최솟값을 찾으면 min과 비교해 갱신하고
    양쪽 구간의 경우 min과 비교해 최소 거리를 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1391
    {

        static void Main1391(string[] args)
        {

            int n;
            (int x, int y)[] pos;
            int[] calc;

            Input();

            GetRet();

            void GetRet()
            {

                int INF = 1_000_000_000;
                int min = INF;


                Array.Sort(pos, (x, y) => x.x.CompareTo(y.x));
                var comp = Comparer<int>.Create((x, y) => pos[x].y.CompareTo(pos[y].y));

                calc = new int[n];

                Console.Write(DnC(0, n));

                int DnC(int _s, int _n)
                {

                    if (_n == 1) return INF;
                    else if (_n == 2) return Dis(_s, _s + 1);

                    int mid = _n >> 1;
                    min = Math.Min(DnC(_s, mid), min);
                    min = Math.Min(DnC(_s + mid, _n - mid), min);

                    int len = 0;
                    for (int i = 0; i < _n; i++)
                    {

                        if (Pow(pos[_s + i].x - pos[_s + mid].x) < min)
                            calc[len++] = _s + i;
                    }

                    Array.Sort(calc, 0, len, comp);
                    for (int i = 0; i < len; i++)
                    {

                        for (int j = i + 1; j < len; j++)
                        {

                            if (Pow(pos[calc[i]].y - pos[calc[j]].y) < min)
                                min = Math.Min(min, Dis(calc[i], calc[j]));
                            else break;
                        }
                    }

                    return min;
                }

                int Pow(int _n)
                    => _n * _n;

                int Dis(int _i, int _j)
                    => Pow(pos[_i].x - pos[_j].x) + Pow(pos[_i].y - pos[_j].y);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                pos = new (int x, int y)[n];
                for (int i = 0; i < n; i++)
                {

                    pos[i] = (ReadInt(), ReadInt());
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
                        if (c == ' ' || c == '\n') return true;

                        bool positive = c != '-';
                        ret = positive ? c - '0' : 0;

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        ret = positive ? ret : -ret;
                        return false;
                    }
                }
            }
        }
    }

#if other
List<(int x, int y)> dots = new();

int inputCount = int.Parse(Console.ReadLine());
for (int i = 0; i < inputCount; i++) {
    var input = Array.ConvertAll(Console.ReadLine()!.Split(), int.Parse);
    dots.Add((input[0], input[1]));
}

dots.Sort((pos1, pos2) => pos1.x - pos2.x);
var ret = GetMinDistRecursive(0, inputCount - 1);

Console.WriteLine(ret);

int GetPoweredDist((int x, int y) pos1, (int x, int y) pos2) {
    int diffX = pos2.x - pos1.x;
    int diffY = pos2.y - pos1.y;
    return (diffX * diffX) + (diffY * diffY);
}

int GetMinDistManual(int startIdx, int endIdx) {
    int minDist = int.MaxValue;
    
    for (int i = startIdx; i < endIdx; i++) {
        var first = dots[i];
        for (int k = i + 1; k <= endIdx; k++) {
            var second = dots[k];
            minDist = Math.Min(minDist, GetPoweredDist(first, second));
        }
    }
    
    return minDist;
}

int GetMinDistRecursive(int startIdx, int endIdx) {
    if (endIdx - startIdx < 3) return GetMinDistManual(startIdx, endIdx);
    
    int pivot = (startIdx + endIdx) / 2;
    int left = GetMinDistRecursive(startIdx, pivot);
    int right = GetMinDistRecursive(pivot + 1, endIdx);
    
    int minDist = Math.Min(left, right);
    CheckNearbyPivot(startIdx, pivot, endIdx, ref minDist);
    
    return minDist;
}

void CheckNearbyPivot(int start, int pivot, int end, ref int minDist) {
    List<(int x, int y)> candidates = new();
    
    for (int i = start; i <= end; i++) {
        var diffX = dots[i].x - dots[pivot].x;
        if (diffX * diffX < minDist) candidates.Add(dots[i]);
    }
    
    candidates.Sort((pos1, pos2) => pos1.y - pos2.y);
    
    for (int i = 0; i < candidates.Count; i++) {
        for (int k = i + 1; k < candidates.Count; k++) {
            var diffY = candidates[i].y - candidates[k].y;
            if (diffY * diffY >= minDist) break;
            
            var dist = GetPoweredDist(candidates[i], candidates[k]);
            if (dist < minDist) minDist = dist;
        }
    }
}
#elif other2
using System;
using System.Collections.Generic;
using System.Linq;

static class Program
{
    static void Main()
    {
        var te = Convert.ToInt32(Console.ReadLine());
        List<(int x, int y)> list = new();
        for (int i = 0; i < te; i++)
        {
            var stp = Console.ReadLine().Split().Select(int.Parse).ToArray();
            list.Add((stp[0], stp[1]));
        }

        var xOrder = list.OrderBy(x => x.x).ToList();
        var yOrder = list.OrderBy(x => x.y).ToList();

        var sol = Finding_Boundary_Coordinates(xOrder, yOrder).pair;

        Console.WriteLine(Squared_Distance(sol.Item1, sol.Item2));
    }


    static (double dist, ((int x, int y), (int x, int y)) pair) Finding_Boundary_Coordinates(List<(int x, int y)> xOrder, List<(int x, int y)> yOrder)
    {
        var n = xOrder.Count;
        if (n <= 3)
        {
            double minDist = double.MaxValue;
            ((int x, int y), (int x, int y)) bestPair = (xOrder[0], xOrder[0]);
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    var dist = Distance(xOrder[i], xOrder[j]);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        bestPair = (xOrder[i], xOrder[j]);
                    }
                }
            }

            return (minDist, bestPair);
        }

        int mid = n / 2;
        int midX = xOrder[mid].x;

        var leftX = xOrder.Take(mid).ToList();
        var rightX = xOrder.Skip(mid).ToList();

        var leftY = yOrder.Where(g => g.x <= midX).ToList();
        var rightY = yOrder.Where(g => g.x > midX).ToList();

        var leftResult = Finding_Boundary_Coordinates(leftX, leftY);
        var rightResult = Finding_Boundary_Coordinates(rightX, rightY);

        double cD = Math.Min(leftResult.dist, rightResult.dist);
        var coolPair = leftResult.dist < rightResult.dist ? leftResult.pair : rightResult.pair;

        var strip = yOrder.Where(g => Math.Abs(g.x - midX) < cD).ToList();
        for (int i = 0; i < strip.Count; i++)
        {
            for (int j = i + 1; j < strip.Count && strip[j].y - strip[i].y < cD; j++)
            {
                var dist = Distance(strip[i], strip[j]);
                if (dist < cD)
                {
                    cD = dist;
                    coolPair = (strip[i], strip[j]);
                }
            }
        }

        return (cD, coolPair);
    }

    static double Distance((int x, int y) p1, (int x, int y) p2)
    {
        return Math.Sqrt(Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2));
    }

    static double Squared_Distance((int x, int y) p1, (int x, int y) p2)
    {
        return Math.Pow(Math.Abs(p1.x - p2.x), 2) + Math.Pow(Math.Abs(p1.y - p2.y), 2);
    }
}
#endif
}
