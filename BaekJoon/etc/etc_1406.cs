using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaekJoon.etc
{
    internal class etc_1406
    {

        static void Main1406(string[] args)
        {

            // 2912번
            // 현재 CPP로는 통과하는데 C#은 통과 X
            // 해당 사이트 머지소트트리 방법 참고해볼 예정
            // https://jeongboclass.tistory.com/89
            int n, c, m, k;
            int[] arr;
            (int s, int e, int idx)[] queries;

            Input();

            GetRet();

            void GetRet()
            {

                k = (int)Math.Ceiling(Math.Sqrt(n) + 1e-9);

                Array.Sort(queries, (x, y) =>
                {

                    int ret = (x.s / k).CompareTo(y.s / k);
                    if (ret == 0) (x.e / k).CompareTo(y.e / k);
                    return ret;
                });

                int[] cnt = new int[c + 1];
                int l = queries[0].s;
                int r = queries[0].e;
                for (int i = l; i <= r; i++)
                {

                    cnt[arr[i]]++;
                }

                int[] ret = new int[m];
                ret[queries[0].idx] = Chk(r - l + 1);

                for (int i = 1; i < m; i++)
                {

                    while (l < queries[i].s)
                        cnt[arr[l++]]--;

                    while (queries[i].s < l)
                        cnt[arr[--l]]++;

                    while (queries[i].e < r)
                        cnt[arr[r--]]--;

                    while (r < queries[i].e)
                        cnt[arr[++r]]++;

                    ret[queries[i].idx] = Chk(r - l + 1);
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < m; i++)
                {

                    if (ret[i] > 0) sw.Write($"yes {ret[i]}\n");
                    else sw.Write("no\n");
                }

                int Chk(int _len)
                {

                    int half = _len >> 1;
                    for (int i = 1; i <= c; i++)
                    {

                        if (cnt[i] <= half) continue;
                        return i;
                    }

                    return 0;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                c = ReadInt();

                arr = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                }

                m = ReadInt();
                queries = new (int s, int e, int idx)[m];
                for (int i = 0; i < m; i++)
                {

                    queries[i] = (ReadInt(), ReadInt(), i);
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
    }

#if other
using static IO;
public class IO{
public static IO Cin=new();
public static StreamReader reader=new(Console.OpenStandardInput());
public static StreamWriter writer=new(Console.OpenStandardOutput());
public static implicit operator string(IO _)=>reader.ReadLine();
public static implicit operator char[](IO _)=>reader.ReadLine().ToArray();
public static implicit operator int(IO _)=>int.Parse(reader.ReadLine());
public static implicit operator double(IO _)=>double.Parse(reader.ReadLine());
public static implicit operator string[](IO _)=>reader.ReadLine().Split();
public static implicit operator int[](IO _)=>Array.ConvertAll(reader.ReadLine().Split(),int.Parse);
public static implicit operator (int,int)(IO _){int[] a=Cin;return(a[0],a[1]);}
public static implicit operator (int,int,int)(IO _){int[] a=Cin;return(a[0],a[1],a[2]);}
public void Deconstruct(out int a,out int b){(int,int) r=Cin;(a,b)=r;}
public void Deconstruct(out int a,out int b,out int c){(int,int,int) r=Cin;(a,b,c)=r;}
public static void Loop(int end,Action<int> action,int start = 0,in int add = 1){for(;start<end;start+=add)action(start);}
public static object? Cout{set{writer.Write(value);}}
public static object? Coutln{set{writer.WriteLine(value);}}
public static void Main() {Program.Coding();writer.Flush();}
}
class Program {
    public static void Coding() {
        (int _,int _) = Cin;
        Mos mos = new(Cin);
        Loop(Cin, _ => {
            (int s,int e) = Cin;
            mos.AddQuery(--s,--e);
        });
        foreach(var ret in mos.GetResult()) {
            Coutln = ret == null ? "no" : $"yes {ret}";
        }
    }
}

class Mos {
    readonly int[] array;
    readonly int sqrtN;
    public Mos(int[] arr) {
        this.array = arr;
        this.sqrtN = (int)Math.Sqrt(arr.Length);
    }
    record Query(int Start,int End,int Index) { public int Count => End - Start + 1; }
    PriorityQueue<Query, (int s,int e)> queue = new();
    public void AddQuery(int start,int end) {
        Query q = new(start,end,queue.Count);
        queue.Enqueue(q, (start/sqrtN,end));
    }
    Counter counter = new();
    int start,end;
    void Push(in Query next) {
        while(next.Start < start) {
            counter.Add(array[--start]);
        }
        while(end < next.End) {
            counter.Add(array[++end]);
        }
    }
    void Pop(in Query next) {
        while(start < next.Start) {
            counter.Remove(array[start++]);
        }
        while(next.End < end) {
            counter.Remove(array[end--]);
        }
    }
    int? Setup(in Query first) {
        (start,end,_) = first;
        for(int x=start;x<=end;x++) counter.Add(array[x]);
        return counter.Query(first.Count/2);
    } 
    public int?[] GetResult() {
        int?[] result = new int?[queue.Count];

        var ret = queue.Dequeue();
        result[ret.Index] = Setup(ret);

        while(queue.Count > 0) {
            ret = queue.Dequeue();
            Push(ret);
            Pop(ret);
            result[ret.Index] = counter.Query(ret.Count/2);
        }

        return result;
    }
}

class Counter {
    int[] counting = new int[10001];
    public void Add(int x) => counting[x]++;
    public void Remove(int x) => counting[x]--;
    public int? Query(int half) {
        for(int i=1;i<=10000;i++) {
            if (counting[i] > half) return i;
        }
        return null;
    }
}
#elif CPP
// #include <iostream>
// #include <vector>
// #include <cmath>
// #include <algorithm>

// #define FAST_IO cin.tie(NULL);	\
				cout.tie(NULL);	\
				ios_base::sync_with_stdio(false)

// #define endl '\n'

using namespace std;

int n, c, m, k;

struct Query
{

	int s, e, idx;

	bool operator < (const Query& _other)
	{

		if (s / k == _other.s / k)
			return e / k < _other.e / k;
		else
			return s / k < _other.s / k;
	}
};

int main()
{

	FAST_IO;

	cin >> n >> c;
	vector<int> arr(n + 1);
	for (int i = 1; i <= n; i++)
		cin >> arr[i];

	cin >> m;
	vector<Query> queries(m);
	for (int i = 0; i < m; i++)
	{

		cin >> queries[i].s >> queries[i].e;
		queries[i].idx = i;
	}

	k = sqrt(n);
	sort(queries.begin(), queries.end());

	vector<int> cnt(c + 1, 0);
	int l = queries[0].s;
	int r = queries[0].e;

	for (int i = l; i <= r; i++)
		cnt[arr[i]]++;

	vector<int> ret(n, 0);
	
	int half = (r - l + 1) >> 1;

	for (int i = 1; i <= c; i++)
	{

		if (cnt[i] <= half) continue;
		int idx = queries[0].idx;
		ret[idx] = i;
		break;
	}

	for (int i = 1; i < m; i++)
	{

		while (l < queries[i].s)
			cnt[arr[l++]]--;

		while (queries[i].s < l)
			cnt[arr[--l]]++;

		while (queries[i].e < r)
			cnt[arr[r--]]--;

		while (r < queries[i].e)
			cnt[arr[++r]]++;

		half = (r - l + 1) >> 1;
		for (int j = 1; j <= c; j++)
		{

			if (cnt[j] <= half) continue;
			ret[queries[i].idx] = j;
			break;
		}
	}

	string y = "yes ";
	string n = "no";
	for (int i = 0; i < m; i++)
	{

		if (ret[i] > 0) cout << y << ret[i];
		else cout << n;
		cout << endl;
	}

	return 0;
}
#endif
}
