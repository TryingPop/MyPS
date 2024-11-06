/*
날짜 : 2024. 9. 6
이름 : 배성훈
내용 : 교수님은 기다리지 않는다
    문제번호 : 3830번

    자료구조, 분리집합 문제다
    두 합성물을 연성했는지 확인하기 위해 유니온 파인드 알고리즘을 이용한다

    그룹으로 엮이면 그룹의 대표 노드 즉, 
    그룹 번호와 같은 노드와의 거리를 기록한다
    
    만약 두 그룹이 합쳐지면
    대표 노드에 거리차를 저장한다
    그리고 해당 노드를 방문하게되면 거리를 누적해 전달한다

    이렇게 대표노드와의 거리를 구하면
    실제 두 노드 사이의 거리는 차로 구할 수 있게된다
*/

namespace BaekJoon.etc
{
    internal class etc_0948
    {

        static void Main948(string[] args)
        {

            string NO = "UNKNOWN\n";

            StreamReader sr;
            StreamWriter sw;

            int[] group;
            int[] weights;
            int[] stack;

            int n, m;

            Solve();
            void Solve()
            {

                Init();

                while(Input())
                {

                    for (int i = 0; i < m; i++)
                    {

                        int op = ReadOp();

                        int f = TryReadInt();
                        int b = TryReadInt();

                        if (op == 1)
                        {

                            int w = TryReadInt();
                            Union(f, b, w);
                        }
                        else
                        {

                            int fIdx = Find(f);
                            int bIdx = Find(b);

                            if (fIdx == bIdx) sw.Write($"{weights[b] - weights[f]}\n");
                            else sw.Write(NO);
                        }
                    }
                }

                sr.Close();
                sw.Close();
            }

            int ReadOp()
            {

                int op = TryReadInt();

                if (op == '!' - '0') return 1;
                else return 2;
            }

            bool Input()
            {

                n = TryReadInt();
                m = TryReadInt();

                for (int i = 1; i <= n; i++)
                {

                    group[i] = i;
                    weights[i] = 0;
                }

                return n != 0 || m != 0;
            }

            void Union(int _f, int _b, int _w)
            {

                int f = Find(_f);
                int b = Find(_b);

                if (f == b) return;

                weights[b] = weights[_f] - weights[_b] + _w;
                group[b] = f;
            }

            int Find(int _chk)
            {

                int len = 0;
                while (_chk != group[_chk])
                {

                    stack[len++] = _chk;
                    _chk = group[_chk];
                }
                stack[len] = _chk;
                while (len > 0) 
                {

                    weights[stack[len - 1]] += weights[stack[len]];
                    group[stack[--len]] = _chk;
                }

                return _chk;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                group = new int[100_001];

                weights = new int[100_001];
                stack = new int[100_001];
            }
            
            int TryReadInt()
            {

                int ret;
                while (ReadInt(out ret)) { }

                return ret;
            }

            bool ReadInt(out int _ret)
            {

                int c = sr.Read();
                _ret = 0;
                if (c == '\r') c = sr.Read();
                if (c == -1 || c == ' ' || c == '\n') return true;

                _ret = c - '0';
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    _ret = _ret * 10 + c - '0';
                }

                return false;
            }
        }

    }
#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public class UnionFind
{
    private int[] _set;

    /// <summary>
    /// weight(idx) + _selfToRoot(idx) = weight(root)
    /// </summary>
    private long[] _selfToRoot;

    private List<int> _buffer;

    public UnionFind(int n)
    {
        _set = new int[n];
        _selfToRoot = new long[n];
        _buffer = new List<int>(n);

        for (var idx = 0; idx < n; idx++)
            _set[idx] = idx;
    }

    public int Find(int v)
    {
        var root = v;
        if (_set[root] == _set[_set[root]])
            return _set[root];

        _buffer.Clear();
        do
        {
            _buffer.Add(root);
            root = _set[root];
        }
        while (root != _set[root]);

        foreach (var val in _buffer.AsEnumerable().Reverse())
        {
            if (_set[_set[val]] != root)
                throw new NotImplementedException();

            _selfToRoot[val] += _selfToRoot[_set[val]];
            _set[val] = root;
        }

        return root;
    }

    public long? GetWeightDiff(int b, int a)
    {
        var broot = Find(b);
        var aroot = Find(a);

        if (broot != aroot)
            return null;

        return _selfToRoot[a] - _selfToRoot[b];
    }

    // weight(b) = weight(a) + diff
    public void Union(int b, int a, int diff)
    {
        var broot = Find(b);
        var aroot = Find(a);

        if (broot == aroot)
            return;

        _set[broot] = _set[aroot];
        _selfToRoot[broot] = -diff + _selfToRoot[a] - _selfToRoot[b];
    }
}

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        while (true)
        {
            var nm = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var (n, m) = (nm[0], nm[1]);

            if (n == 0 && m == 0)
                break;

            var uf = new UnionFind(1 + n);

            while (m-- > 0)
            {
                var q = sr.ReadLine().Split(' ');

                if (q[0] == "!")
                {
                    var (a, b, w) = (Int32.Parse(q[1]), Int32.Parse(q[2]), Int32.Parse(q[3]));
                    uf.Union(b, a, w);
                }
                else
                {
                    var (a, b) = (Int32.Parse(q[1]), Int32.Parse(q[2]));
                    sw.WriteLine(uf.GetWeightDiff(b, a)?.ToString() ?? "UNKNOWN");
                }
            }
        }
    }
}

#elif other2
using System;
using System.IO;

namespace Baekjoon_3830
{
    class Program
    {
        static int[] p = new int[100001];
        static long[] v = new long[100001];

        static void Main()
        {
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            int[] nm = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            while (nm[0] != 0 && nm[1] != 0)
            {
                int n1 = nm[0] + 1, m = nm[1];
                for (int i = 1; i < n1; i++)
                {
                    p[i] = i;
                    v[i] = 0;
                }
                while (m-- > 0)
                {
                    string[] w = sr.ReadLine().Split(' ');
                    int a = int.Parse(w[1]), b = int.Parse(w[2]), ra = Root(a), rb = Root(b);
                    if (w[0][0] == '!')
                    {
                        if (ra != rb)
                        {
                            p[rb] = ra;
                            int c = int.Parse(w[3]);
                            v[rb] = v[a] + c - v[b];
                        }
                    }
                    else
                    {
                        sw.WriteLine(ra == rb ? (v[b] - v[a]).ToString() : "UNKNOWN");
                    }
                }
                nm = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            }
            sw.Flush();
        }

        static int Root(int i)
        {
            if (i == p[i])
                return i;
            int t = Root(p[i]);
            v[i] += v[p[i]];
            return p[i] = t;
        }
    }
}

#elif other3
using System.Text;

StringBuilder sb = new StringBuilder();
int[] inputs, parents, dist;
int N, M;

while (true)
{
    inputs = Array.ConvertAll(Console.ReadLine()!.Split(), int.Parse);
    N = inputs[0];
    M = inputs[1];
    if (N == 0 && M == 0) break;

    parents = new int[N + 1];
    dist = new int[N + 1];
    for (int i = 1; i <= N; i++)
        parents[i] = i;

    string[] strs;
    int parentA, parentB, a, b, c;
    for (int i = 0; i < M; i++)
    {
        strs = Console.ReadLine()!.Split();
        a = int.Parse(strs[1]); b = int.Parse(strs[2]);
        parentA = FindParent(a); parentB = FindParent(b);
        if (strs[0] == "!")
        {
            c = int.Parse(strs[3]);
            if (parentA != parentB)
                Union(a, b, c);
        }
        else // 교수님의 질문
        {
            if (parentA != parentB)
                sb.AppendLine("UNKNOWN");
            else
                sb.AppendLine((dist[b] - dist[a]).ToString());
        }
    }
}

Console.WriteLine(sb);

int FindParent(int node)
{
    if (parents[node] != node)
    {
        int parent = FindParent(parents[node]);
        dist[node] += dist[parents[node]];
        parents[node] = parent;
    }
    return parents[node];
}

void Union(int nodeA, int nodeB, int dif)
{
    int parentA = FindParent(nodeA);
    int parentB = FindParent(nodeB);

    if (parentA != parentB)
    {
        parents[parentB] = parentA;
        dist[parentB] = dist[nodeA] + dif - dist[nodeB];
    }
}

#elif other4
// #include <iostream>
// #include <vector>

using namespace std;

namespace fio
{
    //read
    const int SIZE = 1 << 20;
    char arBuffer[SIZE]{};
    int nReadIndex = SIZE;

    inline char ReadChar()
    {
        if (nReadIndex == SIZE)
        {
            fread(arBuffer, 1, SIZE, stdin);
            nReadIndex = 0;
        }

        return arBuffer[nReadIndex++];
    }

    int ReadInt()
    {
        char cRead = ReadChar();

        while ((cRead < 48 || cRead > 57) && cRead != '-')
            cRead = ReadChar();

        int nRes = 0;
        bool bNeg = (cRead == '-');

        if (bNeg)
            cRead = ReadChar();

        while (cRead >= 48 && cRead <= 57)
        {
            nRes = nRes * 10 + cRead - 48;
            cRead = ReadChar();
        }

        return bNeg ? -nRes : nRes;
    }

    //write
    char arWBuffer[SIZE]{};
    int nWriteIndex;

    inline int GetUnitSize(int nWrite)
    {
        if (nWrite < 0)
            nWrite = -nWrite;

        int nIntSize = 1;

        while (nWrite >= 10)
        {
            nIntSize++;
            nWrite /= 10;
        }

        return nIntSize;
    }

    inline void Flush()
    {
        fwrite(arWBuffer, 1, nWriteIndex, stdout);
        nWriteIndex = 0;
    }

    inline void WriteChar(char cWrite)
    {
        if (nWriteIndex == SIZE)
            Flush();

        arWBuffer[nWriteIndex++] = cWrite;
    }

    inline void WriteInt(int nWrite)
    {
        int nUnitSize = GetUnitSize(nWrite);

        if (nWriteIndex + nUnitSize >= SIZE)
            Flush();

        if (nWrite < 0)
        {
            nWrite = -nWrite;
            arWBuffer[nWriteIndex++] = '-';
        }

        int nNext = nWriteIndex + nUnitSize;

        while (nUnitSize--)
        {
            arWBuffer[nWriteIndex + nUnitSize] = nWrite % 10 + 48;
            nWrite /= 10;
        }

        nWriteIndex = nNext;
        //WriteChar('\n');
    }
}

int arParent[100001]{};
int arWeight[100001]{};

bool Solve();
int FindParent(int nData);
void UnionParent(int nData1, int nData2, int w);
int GetMax(int nData1, int nData2);
int GetMin(int nData1, int nData2);
int GetAbs(int nData);

int main()
{
	ios::sync_with_stdio(false);
	cin.tie(NULL);
	cout.tie(NULL);

	Solve();
}

bool Solve()
{
	bool bReturn = false;
	int N, M, a, b, w, nParent1, nParent2;
	char cQuestion;
    char cAns[7] = { 'U', 'N', 'K', 'N', 'O', 'W', 'N' };

	do
	{
		while (true)
		{
            N = fio::ReadInt();
            M = fio::ReadInt();

			if (N == 0 && M == 0)
				break;

			for (int i = 1; i <= N; i++)
			{
				arWeight[i] = 0;
				arParent[i] = i;
			}

			while (M--)
			{
                cQuestion = fio::ReadChar();

				if (cQuestion == '!')
				{
                    a = fio::ReadInt();
                    b = fio::ReadInt();
                    w = fio::ReadInt();

					UnionParent(a, b, w);
				}
				else
				{
                    a = fio::ReadInt();
                    b = fio::ReadInt();

					nParent1 = FindParent(a);
					nParent2 = FindParent(b);
					
                    if (nParent1 != nParent2)
                    {
                        for (int i = 0; i < 7; i++)
                            fio::WriteChar(cAns[i]);
                    }
                    else
                        fio::WriteInt(arWeight[b] - arWeight[a]);

                    fio::WriteChar('\n');
				}
			}
		}

        fio::Flush();

		bReturn = true;
	} while (false);

	return bReturn;
}

int FindParent(int nData)
{
	if (nData == arParent[nData])
		return nData;

	int nParent = FindParent(arParent[nData]);
	arWeight[nData] += arWeight[arParent[nData]];
	arParent[nData] = nParent;

	return nParent;
}

void UnionParent(int nData1, int nData2, int w)
{
	int nParent1, nParent2;

	nParent1 = FindParent(nData1);
	nParent2 = FindParent(nData2);

	if (nParent1 == nParent2)
		return;

	arParent[nParent2] = nParent1;
	arWeight[nParent2] = arWeight[nData1] - arWeight[nData2] + w;
}

int GetMax(int nData1, int nData2)
{
	return nData1 < nData2 ? nData2 : nData1;
}

int GetMin(int nData1, int nData2)
{
	return nData1 > nData2 ? nData2 : nData1;
}

int GetAbs(int nData)
{
	return nData < 0 ? -nData : nData;
}

#endif
}
