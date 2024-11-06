using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 23
이름 : 배성훈
내용 : 엘리베이터, 엘리베이터 2
    문제번호 : 2593번, 15994번

    BFS, 제곱근 분할법, 확장 유클리드 호제법 문제다

    확장된 유클리드 알고리즘
        a = b * q0 + r1
        b = r1 * q1 + r2
        r1 = r2 * q2 + r3
        ... r(i - 1) = ri * qi + gcd

        r(i + 1) = r(i - 1) - q * ri 식을 얻을 수 있다
    ri = si * a + ti * b라하면
        s(i + 1) = s(i - 1) - q * si
        t(i + 1) = t(i - 1) - q * ti
        이고,
        s(i - 1) * a + t(i - 1) * b = (si * a + ti * b) * qi + gcd
        => gcd = s(i + 1) * a + t(i + 1) * b

        =>  t(i + 1) * b = gcd(mod a)
            s(i + 1) * a = gcd(mod b)

    를 얻는다 만약 gcd = 1이면 역원이 된다!
    
    중국인의 나머지 정리
        gcd(p1, p2) = 1
        x = a1 (mod p1) and x = a2 (mod p2)
        => x = p1 * inv(p1, p2) * a1 + p2 * inv(p2, p1) * a2
        여기서, x = inv(a, b) <=> a * x = 1 (mod b)

        3개면 gcd(p1, p2, p3) = 1
        x = a1 (mod p1), x = a2 (mod p2) and x = a3 (mod p3)
        => x = p1 * p2 * inv(p1 * p2, p3) + p2 * p3 * inv(p2 * p3, p1) + p3 * p1 * inv(p3 * p1, p2)

    아이디어는 다음과 같다
    엘리베이터끼리 만나는 층이 있는 경우 간선을 잇는다
    그리고, 해당 간선들을 이용해 목적지까지 최소 경로를 찾으면 된다
    간선의 거리가 1로 고정되어 있어 BFS 탐색이 다익스트라 탐색과 같다
    간선을 이을 수만 있다면 매우 쉬워진다

    엘리베이터 i와 j가 특정 층에서 만나는 것은 전체가 10만층 이하인 경우는
    i를 ni층씩 올리고, j 의 nj으로 나누어 떨어지고 sj 보다 크거나 같고 m보다 낮은지 확인해도 무방하다
    해당 방법이 떠오르지 않았고, 엘리베이터 2문제도(최대 5000만 층) 고려했기에 떠올랐어도 쓰지 않았을 것이다
    ni와 nj의 gcd인 gcd(ni, nj)가 합성수이고, gcd(ni, gcd(ni, nj)) != gcd(ni, nj)인 경우 쉽게 풀 수 있는지 고려했다
    결국 못 찾아 정수론 시간때 배운 중국인의 나머지 정리를 일일히 해서 풀었다
    이렇게 제출하니 이상없이 통과했다
    해당 gcd(ni, gcd(ni, nj)) != gcd(ni, nj)인 경우를 고려하기까지 3번 틀렸다 최소값을 안맞춰서 1번 틀려 총 4번 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_0717
    {

        static void Main717(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int m, n;
            int f, t;

            (int s, int n)[] elevator;
            List<int>[] line;

            int ret1;
            int[] ret2;
            Solve();

            void Solve()
            {

                Input();

                LinkLine();

                BFS();

                Output();
            }

            void Output()
            {

                sw = new(new BufferedStream(Console.OpenStandardOutput()));

                sw.Write($"{ret1}\n");

                for(int i = 0; i < ret1; i++)
                {

                    sw.Write($"{ret2[i]}\n");
                }

                sw.Close();
            }

            void BFS()
            {

                // Dijkstra
                Queue<int> q = new(n);
                (int cnt, int b)[] arr = new (int cnt, int b)[n + 2];

                arr[0].cnt = 1;
                arr[0].b = -1;

                q.Enqueue(0);

                while(q.Count > 0)
                {

                    int node = q.Dequeue();

                    for (int i = 0; i < line[node].Count; i++)
                    {

                        int next = line[node][i];
                        if (arr[next].cnt != 0) continue;
                        arr[next].cnt = arr[node].cnt + 1;
                        arr[next].b = node;

                        q.Enqueue(next);
                    }
                }

                ret1 = arr[n + 1].cnt - 2;
                if (ret1 == -2) 
                {

                    ret1 = -1;
                    ret2 = null;
                    return; 
                }

                ret2 = new int[ret1];
                int idx = ret1 - 1;

                for (int i = arr[n + 1].b; i != 0; i = arr[i].b)
                {

                    ret2[idx--] = i;
                }
            }

            void LinkLine()
            {

                line = new List<int>[n + 2];
                for (int i = 0; i < n + 2; i++)
                {

                    line[i] = new();
                }

                for (int i = 0; i < n; i++)
                {

                    for (int j = i + 1; j < n; j++)
                    {

                        if (IsDisConn(elevator[i], elevator[j])) continue;

                        line[i + 1].Add(j + 1);
                        line[j + 1].Add(i + 1);
                    }
                }

                for (int i = 0; i < n; i++)
                {

                    if (ChkFloor(f, elevator[i])) line[0].Add(i + 1);
                    if (ChkFloor(t, elevator[i])) line[i + 1].Add(n + 1);
                }
            }

            bool IsDisConn((int s, int n) _ele1, (int s, int n) _ele2)
            {

                int gcd = GetGCD(_ele1.n, _ele2.n);
                if ((_ele1.s - _ele2.s) % gcd != 0) return true;

                long s = GetFirstMeetFloor(_ele1, _ele2, gcd);
                if (s > m) return true;

                return false;
            }

            bool ChkFloor(int _floor, (int s, int n) _elevator)
            {

                return _floor >= _elevator.s && _floor % _elevator.n == _elevator.s % _elevator.n;
            }

            long GetFirstMeetFloor((int s, int n) _ele1, (int s, int n) _ele2, int _gcd)
            {

                long lcm = (1L * _ele1.n * _ele2.n) / _gcd;
                long LCM = lcm;
                long ret = 0;
                for (int i = 2; i * i <= lcm; i++)
                {

                    if (lcm % i != 0) continue;
                    lcm /= i;
                    long mod = i;
                    while(lcm % i == 0)
                    {

                        lcm /= i;
                        mod *= i;
                    }

                    long r = _ele1.n % mod == 0 ? _ele1.s : _ele2.s;
                    GetEuclid(LCM / mod, mod, out long inv, out long _);
                    ret += ChineseRemainder(LCM / mod, inv, r);
                }

                if (lcm > 1)
                {


                    long r = _ele1.n % lcm == 0 ? _ele1.s : _ele2.s;
                    GetEuclid(LCM / lcm, lcm, out long inv, out long _);

                    ret += ChineseRemainder(LCM / lcm, inv, r);
                }

                ret = ret % LCM;
                ret = ret < 0 ? ret + LCM : ret;

                long add = Math.Max(_ele1.s / LCM, _ele2.s / LCM) * LCM;
                ret = ret + add;
                if (ret < _ele1.s) ret += LCM;
                if (ret < _ele2.s) ret += LCM;
                return ret;
            }

            long ChineseRemainder(long _x, long _invX, long _r)
            {

                return _x * _invX * _r;
            }

            int GetGCD(int _a, int _b)
            {

                while(_b > 0)
                {

                    int temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            void GetEuclid(long _a, long _b, out long _invA, out long _invB)
            {

                _invA = 0;
                _invB = 1;

                long s1 = 1;
                long t1 = 0;

                long s2 = 0;
                long t2 = 1;

                while(_b > 0)
                {

                    long q = _a / _b;
                    long temp = _a % _b;
                    _a = _b;
                    _b = temp;

                    temp = -q * s2 + s1;
                    s1 = s2;
                    s2 = temp;

                    temp = -q * t2 + t1;
                    t1 = t2;
                    t2 = temp;
                }

                _invA = s1;
                _invB = t1;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                m = ReadInt();
                n = ReadInt();

                elevator = new (int s, int n)[n];
                for (int i = 0; i < n; i++)
                {

                    elevator[i] = (ReadInt(), ReadInt());
                }

                f = ReadInt();
                t = ReadInt();

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c!= ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// #include <cstdio>
// #include <cstring>
// #include <queue>
// #define INF 987654321

using namespace std;

int N, M, A, B;
int check[101][2];
int elevator[101][2];
bool visit[101];
queue<int> q;

void dfs(int node) {
	if (check[node][1] != node) {
		dfs(check[node][1]);
	}
	printf("%d\n", node+1);
}

bool has_floor(int f, int e) {
	if (elevator[e][0] > f) return false;
	return (f - elevator[e][0]) % elevator[e][1] == 0;
}

bool has_same_floor(int e1, int e2) {
	if (e1 == e2) return false;
	for (int i = 0; i <= N; i++) {
		if (elevator[e1][0] + i * elevator[e1][1] > N) break;
		if (has_floor(elevator[e1][0] + i * elevator[e1][1], e2)) {
			return true;
		}
	}
	return false;
}

int main() {
	memset(check, -1, sizeof(check));
	scanf("%d%d", &N, &M);
	for (int i = 0; i < M; i++) {
		int x, y;
		scanf("%d%d", &x, &y);
		elevator[i][0] = x;
		elevator[i][1] = y;
	}
	scanf("%d%d", &A, &B);
	for (int i = 0; i < M; i++) {
		if (has_floor(A, i)) {
			visit[i] = true;
			q.push(i);
			check[i][0] = 1;
			check[i][1] = i;
		}
	}
	while (!q.empty()) {
		int x = q.front();
		q.pop();
		for (int i = 0; i < M; i++) {
			if (visit[i]) continue;
			if (has_same_floor(x, i)) {
				visit[i] = true;
				q.push(i);
				check[i][0] = check[x][0] + 1;
				check[i][1] = x;
			}
		}
	}
	int min_val = INF, min_idx = -1;
	for (int i = 0; i < M; i++) {
		if (check[i][0] >= 0 && has_floor(B, i)) {
			if (check[i][0] < min_val) {
				min_val = check[i][0];
				min_idx = i;
			}
		}
	}
	if (min_val == INF) {
		printf("-1\n");
	} else {
		printf("%d\n", min_val);
		dfs(min_idx);
	}
	return 0;
}
#elif other2       
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class Program
    {
        public static int Main(string[] args)
        {
            using var io = new IoInstance();
                try
                {
                    var (N, M) = IO.GetIntTuple2();
                    var elevators = M.MakeList(_ => IO.GetLongTuple2());
                    var (A, B) = IO.GetLongTuple2();

                    Solve(N, elevators, A, B);
                }
                catch
                {
                    "Error".Dump();
                }
            return 0;
        }

        public static void Solve(long N, List<(long Base, long Term)> elevators, long A, long B)
        {
            var nodes = MakeNodes(N, elevators, A, B);

            var minLength = long.MaxValue;
            var nodeIndex = new List<long>();
            nodes.Where(x => x.IsStart).ForEach(node =>
            {
                nodes.ForEach(n => n.Clear());

                if (BFS(node, out var goalNode))
                {
                    if (minLength > goalNode.Depth)
                    {
                        minLength = goalNode.Depth;
                        nodeIndex.Clear();

                        var tNode = goalNode;
                        while (tNode != null)
                        {
                            nodeIndex.Add(tNode.Index);
                            tNode = tNode.PrevNode;
                        }
                    }
                }
            });

            if (minLength == long.MaxValue)
            {
                (-1).Dump();
            }
            else
            {
                minLength.Dump();
                nodeIndex.Reverse();
                nodeIndex.ForEach(index => index.Dump());
            }
        }

        private static bool BFS(Node start, out Node goalNode)
        {
            var queue = new Queue<Node>();

            start.Mark = true;
            start.Depth = 1;
            queue.Enqueue(start);

            goalNode = null;
            while (queue.Any())
            {
                var node = queue.Dequeue();
                if (node.IsGoal)
                {
                    goalNode = node;
                    return true;
                }

                node.Nodes.Where(target => !target.Mark)
                    .ForEach(target =>
                    {
                        target.Depth = node.Depth + 1;
                        target.Mark = true;
                        target.PrevNode = node;
                        queue.Enqueue(target);
                    });
            }

            return false;
        }

        private static List<Node> MakeNodes(long N, List<(long Base, long Term)> elevators, long A, long B)
        {
            var nodes = elevators.Select((e, i) => new Node(i + 1, e)).ToList();

            nodes.ForEach(node1 =>
            {
                nodes.Where(node2 => node2 != node1)
                    .Where(node2 => IsConnected(node1.Elevator, node2.Elevator, N))
                    .ForEach(node2 =>
                    {
                        node1.Nodes.Add(node2);
                        node2.Nodes.Add(node1);
                    });

                if (node1.Elevator.Base <= A)
                {
                    node1.IsStart = (A - node1.Elevator.Base) % node1.Elevator.Term == 0;
                }
                if (node1.Elevator.Base <= B)
                {
                    node1.IsGoal = (B - node1.Elevator.Base) % node1.Elevator.Term == 0;
                }
            });

            return nodes;
        }

        /// <summary> N 층 안에서 e1과 e2가 만나는가?  </summary>
        public static bool IsConnected((long Base, long Term) e1, (long Base, long Term) e2, long N)
        {
            if (e1.Base + e1.Term * 100 >= N || e2.Base + e2.Term * 100 >= N)
            {
                var eInfo = e1.Base + e1.Term * 100 >= N ? e1 : e2;
                var other = e1.Base + e1.Term * 100 >= N ? e2 : e1;

                var x = eInfo.Base;
                while (x <= N)
                {
                    if (x >= other.Base && (x - other.Base) % other.Term == 0)
                    {
                        return true;
                    }
                    x += eInfo.Term;
                }
                return false;
            }
            if (TryGetFirst(e1, e2, out var first))
            {
                var maxBase = Math.Max(e1.Base, e2.Base);
                if (first > N)
                    return false;
                if (first >= maxBase)
                    return true;

                var lcm = MathEx.Lcm(e1.Term, e2.Term);

                var xxx = ((maxBase - 1 - first) / lcm + 1);

                if (Math.Log10(xxx) + Math.Log10(lcm) > 10)
                    return false;

                var fixedFirst = xxx * lcm + first;

                if (fixedFirst <= N)
                    return true;

                return false;
            }
            else
            {
                return false;
            }
        }

        public static bool TryGetFirst((long Base, long Term) e1, (long Base, long Term) e2, out long first)
        {
            var m1 = e1.Term;
            var m2 = e2.Term;
            var a1 = e1.Base % m1;
            var a2 = e2.Base % m2;

            var gcd = MathEx.Gcd(m1, m2);
            var lcm = MathEx.Lcm(m1, m2);
            var aaa = Math.Abs(a1 - a2);

            first = 0;
            if (aaa % gcd != 0)
            {
                return false;
            }

            if (m1 == gcd || m2 == gcd)
            {
                first = Math.Min(a1, a2);
                return true;
            }

            var M1 = m1 / gcd;
            var M2 = m2 / gcd;
            var (found, x, y) = Ex.FindDiophantusEquation(M1, M2, 1);

            var v1 = a1 * M2 * y;
            var v2 = a2 * M1 * x;

            var minValue = (v1 + v2) % lcm;
            while (minValue < 0)
            {
                minValue += lcm;
            }
            first = minValue;
            return true;
        }

    }

    public class Node
    {
        public int Index { get; init; }
        public (long Base, long Term) Elevator { get; init; }

        public List<Node> Nodes = new();
        public bool Mark = false;

        public bool IsStart = false;
        public bool IsGoal = false;

        public int Depth = 0;
        public Node PrevNode = null;

        public Node(int index, (long Base, long term) e)
        {
            Index = index;
            Elevator = e;
        }

        public void Clear()
        {
            Mark = false;
            Depth = 0;
            PrevNode = null;
        }
    }

    public static partial class Ex
    {
    }

    public class IoInstance : IDisposable
    {
        public void Dispose()
        {
#if !DEBUG
            IO.Dispose();
#endif
        }
    }

    public static class IO
    {

#if !DEBUG
        static StreamReader _inputReader;
        static StringBuilder _outputBuffer = new();

        static IO()
        {
            _inputReader = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        }
#endif

        public static string GetLine()
        {
#if DEBUG
            return _input[_readInputCount++];
#else
            return _inputReader.ReadLine();
#endif
        }

        public static string GetString()
            => GetLine();

        public static string[] GetStringList()
            => GetLine().Split(' ');

        public static (string, string) GetStringTuple2()
        {
            var arr = GetStringList();
            return (arr[0], arr[1]);
        }

        public static (string, string, string) GetStringTuple3()
        {
            var arr = GetStringList();
            return (arr[0], arr[1], arr[2]);
        }

        public static (string, string, string, string) GetStringTuple4()
        {
            var arr = GetStringList();
            return (arr[0], arr[1], arr[2], arr[3]);
        }

        public static int[] GetIntList()
            => GetLine().Split(' ').Where(x => x.Length > 0).Select(x => x.ToInt()).ToArray();

        public static (int, int) GetIntTuple2()
        {
            var arr = GetLine().Split(' ');
            return (arr[0].ToInt(), arr[1].ToInt());
        }

        public static (int, int, int) GetIntTuple3()
        {
            var arr = GetLine().Split(' ');
            return (arr[0].ToInt(), arr[1].ToInt(), arr[2].ToInt());
        }

        public static (int, int, int, int) GetIntTuple4()
        {
            var arr = GetLine().Split(' ');
            return (arr[0].ToInt(), arr[1].ToInt(), arr[2].ToInt(), arr[3].ToInt());
        }

        public static int GetInt()
            => GetLine().ToInt();

        public static long[] GetLongList()
            => GetLine().Split(' ').Where(x => x.Length > 0).Select(x => x.ToLong()).ToArray();

        public static (long, long) GetLongTuple2()
        {
            var arr = GetLine().Split(' ');
            return (arr[0].ToLong(), arr[1].ToLong());
        }

        public static (long, long, long) GetLongTuple3()
        {
            var arr = GetLine().Split(' ');
            return (arr[0].ToLong(), arr[1].ToLong(), arr[2].ToLong());
        }

        public static (long, long, long, long) GetLongTuple4()
        {
            var arr = GetLine().Split(' ');
            return (arr[0].ToLong(), arr[1].ToLong(), arr[2].ToLong(), arr[3].ToLong());
        }

        public static long GetLong()
            => GetLine().ToLong();

        public static T Dump<T>(this T obj, string format = "")
        {
            var text = string.IsNullOrEmpty(format) ? $"{obj}" : string.Format(format, obj);
#if !DEBUG
            _outputBuffer.AppendLine(text);
#endif
            return obj;
        }

        public static List<T> Dump<T>(this List<T> list)
        {
#if !DEBUG
            _outputBuffer.AppendLine(list.StringJoin(" "));
#endif
            return list;
        }

#if !DEBUG
        public static void Dispose()
        {
            _inputReader.Close();
            using var streamWriter = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            streamWriter.Write(_outputBuffer.ToString());
        }
#endif
    }

    public enum LoopResult
    {
        Void,
        Break,
        Continue,
    }

    public static class Extensions
    {
        public static IEnumerable<long> GetPrimeList(int maximum)
        {
            if (maximum < 2)
                yield break;

            var isPrime = Enumerable.Range(0, maximum + 1).Select(x => false).ToList();

            yield return 2;
            for (var prime = 3; prime <= maximum; prime += 2)
            {
                if (isPrime[prime] == true)
                    continue;
                yield return prime;
                for (var i = prime; i <= maximum; i += prime)
                    isPrime[i] = true;
            }
        }

        public static string With(this string format, params object[] obj)
        {
            return string.Format(format, obj);
        }

        public static string StringJoin<T>(this IEnumerable<T> list, string separator = " ")
        {
            return string.Join(separator, list);
        }

        public static string Left(this string value, int length = 1)
        {
            if (value.Length < length)
                return value;
            return value.Substring(0, length);
        }

        public static string Right(this string value, int length = 1)
        {
            if (value.Length < length)
                return value;
            return value.Substring(value.Length - length, length);
        }


        public static int ToInt(this string str)
        {
            return int.Parse(str);
        }

        public static long ToLong(this string str)
        {
            return long.Parse(str);
        }


        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            var index = 0;
            foreach (var item in source)
                action(item, index++);
        }

        public static void ForEach1<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            var index = 1;
            foreach (var item in source)
                action(item, index++);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Func<T, LoopResult> action)
        {
            foreach (var item in source)
            {
                var result = action(item);
                switch (result)
                {
                    case LoopResult.Break:
                        break;
                    case LoopResult.Continue:
                        continue;
                }
            }
        }

        public static void ForEach<T>(this IEnumerable<T> source, Func<T, int, LoopResult> action)
        {
            var index = 0;
            foreach (var item in source)
            {
                var result = action(item, index++);
                switch (result)
                {
                    case LoopResult.Break:
                        break;
                    case LoopResult.Continue:
                        continue;
                }
            }
        }

        public static void ForEach1<T>(this IEnumerable<T> source, Func<T, int, LoopResult> action)
        {
            var index = 1;
            foreach (var item in source)
            {
                var result = action(item, index++);
                switch (result)
                {
                    case LoopResult.Break:
                        break;
                    case LoopResult.Continue:
                        continue;
                }
            }
        }

        public static bool ForEachBool<T>(this IEnumerable<T> source, Func<T, bool> func)
        {
            var result = true;
            foreach (var item in source)
            {
                if (!func(item))
                    result = false;
            }
            return result;
        }

        public static bool ForEachBool<T>(this IEnumerable<T> source, Func<T, int, bool> func)
        {
            var result = true;
            var index = 0;
            foreach (var item in source)
            {
                if (!func(item, index++))
                    result = false;
            }
            return result;
        }

        public static void For(this int count, Action<int> action)
        {
            for (var i = 0; i < count; i++)
            {
                action(i);
            }
        }

        public static void For1(this int count, Action<int> action)
        {
            for (var i = 1; i <= count; i++)
            {
                action(i);
            }
        }

        public static void For(this int count, Func<int, LoopResult> action)
        {
            for (var i = 0; i < count; i++)
            {
                var result = action(i);
                switch (result)
                {
                    case LoopResult.Break:
                        break;
                    case LoopResult.Continue:
                        continue;
                }
            }
        }

        public static void For1(this int count, Func<int, LoopResult> action)
        {
            for (var i = 1; i <= count; i++)
            {
                var result = action(i);
                switch (result)
                {
                    case LoopResult.Break:
                        break;
                    case LoopResult.Continue:
                        continue;
                }
            }
        }

        public static List<T> MakeList<T>(this int count, Func<int, T> func)
        {
            var result = new List<T>();
            for (var i = 0; i < count; i++)
            {
                result.Add(func(i));
            }
            return result;
        }
        public static TResult Reduce<TSource, TResult>(this IEnumerable<TSource> source, TResult initValue, Func<TResult, TSource, TResult> fn)
        {
            return Reduce(source, initValue, (value, item, index, list) => fn(value, item));
        }

        public static TResult Reduce<TSource, TResult>(this IEnumerable<TSource> source, TResult initValue, Func<TResult, TSource, int, TResult> fn)
        {
            return Reduce(source, initValue, (value, item, index, list) => fn(value, item, index));
        }

        public static TResult Reduce<TSource, TResult>(this IEnumerable<TSource> source, TResult initValue, Func<TResult, TSource, int, IEnumerable<TSource>, TResult> fn)
        {
            var value = initValue;

            var index = 0;
            foreach (var item in source)
            {
                value = fn(value, item, index++, source);
            }

            return value;
        }
    }

    public static partial class Ex
    {
        public static int Ccw(Point a, Point b, Point c)
        {
            // 출처: https://jason9319.tistory.com/358 [ACM-ICPC 상 탈 사람]
            var op = a.X * b.Y + b.X * c.Y + c.X * a.Y;
            op -= (a.Y * b.X + b.Y * c.X + c.Y * a.X);
            if (op > 0) return 1;
            else if (op == 0) return 0;
            else return -1;
        }

        public static bool IsIntersect(Line line1, Line line2)
        {
            // 출처: https://jason9319.tistory.com/358 [ACM-ICPC 상 탈 사람]
            var a = line1.P1;
            var b = line1.P2;
            var c = line2.P1;
            var d = line2.P2;
            int ab = Ccw(a, b, c) * Ccw(a, b, d);
            int cd = Ccw(c, d, a) * Ccw(c, d, b);
            if (ab == 0 && cd == 0)
            {
                if (a > b) (a, b) = Swap(a, b);
                if (c > d) (c, d) = Swap(c, d);
                return c <= b && a <= d;
            }
            return ab <= 0 && cd <= 0;
        }

        public static (T b, T a) Swap<T>(T a, T b)
        {
            return (b, a);
        }

        public static (bool found, long x, long y) FindDiophantusEquation(long a, long b, long c)
        {
            // https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=beneys&logNo=221122957338

            var initA = a;
            var initB = b;

            var list = new List<(long A, long B, long M, long R)>();
            do
            {
                var m = a / b;
                var r = a % b;
                list.Add((a, b, m, r));
                a = b;
                b = r;
            } while (a % b != 0);

            var gcd = list.Last().R;
            if (c % gcd != 0)
            {
                return (false, 0, 0);
            }

            //list.Dump();
            list.Reverse();

            var first = list.First();
            var list2 = new List<(long A, long X, long B, long Y)>
            {
                (first.A, 1, first.B, -first.M)
            };
            foreach (var (A, B, M, R) in list.Skip(1))
            {
                var prev = list2.Last();
                if (R == prev.A)
                {
                    var nextA = A;
                    var nextX = prev.X;
                    var nextB = B;
                    var nextY = prev.Y + (-M) * prev.X;
                    list2.Add((nextA, nextX, nextB, nextY));
                }
                else // if (R == prev.B)
                {
                    var nextA = B;
                    var nextX = prev.X + (-M) * prev.Y;
                    var nextB = A;
                    var nextY = prev.Y;
                    list2.Add((nextA, nextX, nextB, nextY));
                }
            }
            //list2.Dump();

            var mm = c / gcd;
            var last = list2.Last();
            var x = (initA == last.A ? last.X : last.Y) * mm;
            var y = (initA == last.A ? last.Y : last.X) * mm;

            //((initA * x + initB * y)).Dump("C: " + c);

            return (true, x, y);
        }

        public static long ChineseRemainderTheorem(List<(long A, long M)> arr)
        {
            // https://j1w2k3.tistory.com/1340
            var M = arr.Select(x => x.M).Aggregate((a, b) => a * b);
            var nList = arr.Select(x => M / x.M).ToList();

            var xxxList = arr
                .Zip(nList, (condition, N) => new
                {
                    condition.A,
                    N,
                    Dio = FindDiophantusEquation(N, condition.M, 1), // 특수해
                })
                .ToList();

            long x = 0;
            foreach (var xxx in xxxList)
            {
                x += (xxx.A * xxx.N * xxx.Dio.x) % M;
                x %= M;
            }

            return x;
        }
    }

    public class Matrix
    {
        private List<List<long>> Value;

        public int Row => Value.Count;
        public int Column => Value.First().Count;

        public Matrix(int row, int column)
        {
            Value = row.MakeList(_ => column.MakeList(_ => 0L));
        }

        public Matrix(int row, int column, params int[] values)
            : this(row, column)
        {
            var index = 0;
            row.For(r => column.For(c => Value[r][c] = values[index++]));
        }

        public Matrix(List<List<long>> value)
        {
            Value = value;
        }

        public List<long> this[int row] => Value[row];

        public static Matrix operator * (Matrix m1, Matrix m2)
        {
            var result = m1.Row.MakeList(r =>
            {
                return m2.Column.MakeList(c =>
                {
                    long sum = 0;
                    m1.Column.For(k =>
                    {
                        sum += m1[r][k] * m2[k][c];
                    });
                    return sum;
                });
            });

            return new Matrix(result);
        }

        public Matrix Pow(int N)
        {
            var 항등원 = new Matrix(Row.MakeList(r => Column.MakeList(c => r == c ? 1L : 0L)));

            var result = MathEx.Pow(this, N, 항등원, (m1, m2) => m1 * m2);

            return result;
        }

        public Matrix Pow(int N, Func<Matrix, Matrix, Matrix> fnMultifly)
        {
            var 항등원 = new Matrix(Row.MakeList(r => Column.MakeList(c => r == c ? 1L : 0L)));

            var result = MathEx.Pow(this, N, 항등원, fnMultifly);

            return result;
        }

    }

    public static class MathEx
    {

        /// <summary>
        /// pow1의 exp제곱을 구한다. \n
        /// 2^10 = 2.Pow(10, 1, (a, b) => a * b);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="base">밑</param>
        /// <param name="exp">지수</param>
        /// <param name="pow0">밑의 0제곱</param>
        /// <param name="fnMultifly">곱셈연산</param>
        /// <returns></returns>
        public static T Pow<T>(this T @base, int exp, T pow0, Func<T, T, T> fnMultifly)
        {
            // Addition-Chain exponentiation

            var basee = @base;
            var res = pow0;

            while (exp > 0)
            {
                if ((exp & 1) != 0)
                    res = fnMultifly(res, basee);
                exp >>= 1;
                basee = fnMultifly(basee, basee);
            }

            return res;
        }

        public static int Pow(this int @base, int exp)
        {
            return @base.Pow(exp, 1, (a, b) => a * b);
        }

        public static long Pow(this long @base, int exp)
        {
            return @base.Pow(exp, 1, (a, b) => a * b);
        }
        public static long Gcd(long a, long b)
        {
            if (a == b) { return a; }
            else if (a > b && a % b == 0) { return b; }
            else if (b > a && b % a == 0) { return a; }

            long _gcd = 0;
            while (b != 0)
            {
                _gcd = b;
                b = a % b;
                a = _gcd;
            }
            return _gcd;
        }

        public static long Lcm(long a, long b)
        {
            var gcd = Gcd(a, b);
            var lcm = (a / gcd) * b;
            return lcm;
        }

    }

    public class Line
    {
        public Point P1;
        public Point P2;

        public long Length => Math.Abs(P1.X - P2.X) + Math.Abs(P1.Y - P2.Y);
    }

    public record Point
    {
        public long X;
        public long Y;
        public Point() { }
        public Point(long x, long y)
        {
            X = x;
            Y = y;
        }

        public static bool operator <(Point a, Point b)
        {
            if (a.X < b.X)
                return true;
            else if (a.X == b.X && a.Y < b.Y)
                return true;
            return false;
        }

        public static bool operator <=(Point a, Point b)
        {
            if (a.X < b.X)
                return true;
            else if (a.X == b.X && a.Y <= b.Y)
                return true;
            return false;
        }

        public static bool operator >(Point a, Point b)
        {
            return !(a <= b);
        }

        public static bool operator >=(Point a, Point b)
        {
            return !(a < b);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

}

#elif other3
// #include <bits/stdc++.h>
using namespace std;

typedef long long ll;
const int mx = 105;

ll X[mx], K[mx]; //Elevator #i stops at X[i] + K[i] * a - th floor

vector<int> G[mx];
int n, m;
int S, E;

void input(){
	cin >> n >> m;
	for(int i = 1; i <= m; i++){
		cin >> X[i] >> K[i];
	}
	cin >> S >> E;
}
ll ceil(ll a, ll b){
	return (a + b - 1) / b;
}
bool cnct(int i, int j){
	if(K[i] < K[j]) swap(i,j); //key step for time O(N^0.5) complexity
	ll a = max(0LL, ceil(X[j] - X[i], K[i]));
	for(int t = 0; t <= K[j] && X[i] + K[i] * a <= n; t++, a++){
		if( (X[i] + K[i] * a - X[j]) % K[j] == 0 ){
			//printf("%d - %d is connected\n",min(i,j),max(i,j));
			//G[i].push_back(j);
			//G[j].push_back(i);
			return true;
		}
	}
	//printf("%d - %d is NOT connected\n",min(i,j),max(i,j));
    return false;
}

void build_graph(){
	for(int i = 1; i <= m; i++){
		for(int j = i + 1; j <= m; j++){
			cnct(i,j);
		}
	}
}

const int inf = 1e3;
int lev[mx];
int bck[mx];
bool isLeaf[mx];

void traceback(int x){
	cout << lev[x] << '\n';
	stack<int> TB;
	for(int now = x; now != 0; now = bck[now]){
		TB.push(now);
	}
	while(!TB.empty()){
		cout << TB.top() << '\n';
		TB.pop();
	}
}

void bfs(){
	build_graph();
	fill(lev + 1, lev + m + 1, inf);
	queue<int> q;
	for(int i = 1; i <= m; i++){
		if(S >= X[i] && (S - X[i]) % K[i] == 0){
			lev[i] = 1;
			q.push(i);
		}
		if(E >= X[i] && (E - X[i]) % K[i] == 0){
			isLeaf[i] = true;
		}
	}
	while(!q.empty()){
		int f = q.front(); q.pop();
		if(isLeaf[f]){
			traceback(f);
			return;
		}
        for(int i = 1; i <= m; i++){
            if(lev[i] > lev[f] + 1 && cnct(i, f)){
                bck[i] = f;
                lev[i] = lev[f] + 1;
                q.push(i);
            }
        }
	}
	cout << -1 << '\n';
	return;
}

int main(){
	ios::sync_with_stdio(0);cin.tie(0);
	input();
	bfs();
}
#endif
}
