using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 19
이름 : 배성훈
내용 : 칵테일
    문제번호 : 1033번

    수학, 정수론, 유클리드 호제법, 그래프 이론 문제다
    조합식을 간선으로 생각하면 트리의 형태가 된다

    그래서 임의 노드로 0을 루트로 잡고 깊이를 구했다
    깊이가 깊은 쪽(리프와 가까운 쪽)에서 자식들을 탐색해 곱해주고
    깊이가 낮은 쪽(루트와 가까운 쪽)은 이외 노드들을 곱해줬다

    그리고 마지막에 모든 값의 gcd로 나눠서 제출하니 이상없이 통과한다
    9^10까지 갈 수 있다 이는 long 범위를 초과하므로 long으로 자료형을 잡았다

    현재는 N^2으로 탐색한다
    하지만 모아서 한다면 N에 탐색가능할거 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0890
    {

        static void Main890(string[] args)
        {

            StreamReader sr;

            int n;
            long[] val;
            int[] depth;
            bool[] visit;
            List<int>[] line;
            (int a, int b, long p, long q)[] composit;

            Solve();
            void Solve()
            {

                Init();

                SetVal();

                GetRet();
            }

            void SetVal()
            {

                // 깊이 설정
                depth = new int[n];
                SetDepth(0, 0);


                visit = new bool[n];
                for (int i = 0; i < n - 1; i++)
                {

                    int a = composit[i].a;
                    int b = composit[i].b;

                    if (depth[a] < depth[b]) Func(a, b, composit[i].p, composit[i].q);
                    else Func(b, a, composit[i].q, composit[i].p);
                }
            }

            void Func(int _a, int _b, long _p, long _q)
            {

                // 루트와 가까운게 _a

                // _b에서 _a로 못오게 방문 처리
                visit[_a] = true;

                // _b와 자식들에 _q를 곱해준다
                DFS(_b, _q);

                // _a 방문 해제하고 _a쪽에 _p를 곱해준다
                visit[_a] = false;
                for (int i = 0; i < n; i++)
                {

                    if (visit[i])
                    {

                        visit[i] = false;
                        continue;
                    }

                    val[i] *= _p;
                }
            }

            void DFS(int _n, long _mul)
            {

                visit[_n] = true;
                val[_n] *= _mul;

                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];
                    if (visit[next]) continue;
                    DFS(next, _mul);
                }
            }

            void SetDepth(int _n, int _prev)
            {
                
                // 깊이 설정
                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];

                    if (next == _prev) continue;
                    depth[next] = depth[_n] + 1;
                    SetDepth(next, _n);
                }
            }

            void GetRet()
            {

                long gcd = val[0];
                for (int i = 1; i < n; i++)
                {

                    gcd = GetGCD(gcd, val[i]);
                }

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    for (int i = 0; i < n; i++)
                    {

                        sw.Write($"{val[i] / gcd} ");
                    }
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                val = new long[n];
                line = new List<int>[n];

                for (int i = 0; i < n; i++)
                {

                    val[i] = 1;
                    line[i] = new();
                }

                composit = new (int a, int b, long p, long q)[n - 1];

                for (int i = 1; i < n; i++)
                {

                    int a = ReadInt();
                    int b = ReadInt();

                    long p = ReadInt();
                    long q = ReadInt();

                    long gcd = GetGCD(p, q);
                    p /= gcd;
                    q /= gcd;

                    line[a].Add(b);
                    line[b].Add(a);

                    composit[i - 1] = (a, b, p, q);
                }

                sr.Close();
            }

            long GetGCD(long _a, long _b)
            {

                while(_b > 0)
                {

                    long temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
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
using static IO;
public class IO{
public static string? Cin()=>reader.ReadLine();
public static StreamReader reader=new(Console.OpenStandardInput());
public static StreamWriter writer=new(Console.OpenStandardOutput());
public static void Cin(out long num)=>num=long.Parse(Cin());
public static void Cin(out string str)=>str=Cin();
public static void Cin(out string a,out string b,char c=' '){var r=Cin().Split(c);a=r[0];b=r[1];}
public static void Cin(out long[] numarr,char c= ' ')=>numarr=Array.ConvertAll(Cin().Split(c),long.Parse);
public static void Cin(out string[] strarr,char c=' ')=>strarr=Cin().Split(c);
public static void Cin(out double d)=>d=double.Parse(Cin());
public static void Cin(out string t,out long n){var s=Cin().Split();n=long.Parse(s[1]);t=s[0];}
public static void Cin(out long a,out long b,char c= ' '){Cin(out long[] s);(a,b)=(s[0],s[1]);}
public static void Cin(out long a,out long b,out long c,char e=' '){Cin(out long[] s);(a,b,c)=(s[0],s[1],s[2]);}
public static void Cin(out long a,out long b,out long c,out long d,char e = ' '){Cin(out long[] arr,e);(a,b,c,d)=(arr[0],arr[1],arr[2],arr[3]);}
public static void Cin(out long n,out string t) {var s=Cin().Split();n=long.Parse(s[0]);t=s[1];}
public static void Cin<T>(Func<string,T> c,out T a,out T b) {var s=Cin().Split();a=c(s[0]);b=c(s[1]);}
public static void Cin<T>(Func<string,T> c,out T[] a){a=Array.ConvertAll(Cin().Split(),x=>c(x));}
public static object? Cout{set{writer.Write(value);}}
public static object? Coutln{set{writer.WriteLine(value);}}
public static void Main() {Program.Coding();writer.Flush();}
}
class Program {
    record LineInfo(long dot,long mul,long div);
    static long gcd(long a,long b) => b is 0 ? a : gcd(b,a%b);
    public static void Coding() {
        Cin(out long count);
        LinkedList<LineInfo>[] lines = new LinkedList<LineInfo>[count];
        for(long i=0;i<count;i++)lines[i]=new();
        long mm = 1; // 최소 공배수
        for(long i=1;i<count;i++) {
            Cin(out long a,out long b,out long p,out long q);
            mm *= p * q / gcd(p,q);
            lines[a].AddLast(new LineInfo(b,q,p));
            lines[b].AddLast(new LineInfo(a,p,q));
        }
        long[] value = new long[count];
        value[0] = mm;
        void dfs(long me=0,long par=0) {
            long mv = value[me];
            foreach(var other in lines[me]) {
                if (other.dot == par) continue;
                value[other.dot] = mv * other.mul / other.div;
                dfs(other.dot,me);
            }
        }
        dfs();
        //최대 공약수
        long mp = mm;
        for(long x=1;x<count;x++) {
            mp = gcd(mp,value[x]);
        }
        foreach(var n in value) {
            Cout = n/mp;
            Cout = ' ';
        }
    }
}
#elif other2
// #include<cstdio>
typedef long long ll;
ll gcd(ll x,ll y){return y?gcd(y,x%y):x;}
int main(){
	int N;
	ll res[11]={0},d[11][11]={0};
	scanf("%d",&N);
	for(int i=1;i<N;i++){
		int a,b,p,q;
		scanf("%d%d%d%d",&a,&b,&p,&q);
		d[a][b]=q;
		d[b][a]=p;
	}
	for(int i=0;i<N;i++)d[i][i]=1;
	for(int k=0;k<N;k++)
		for(int i=0;i<N;i++)
			for(int j=0;j<N;j++)
				if(!d[i][j]&&d[k][j]&&d[i][k])
					d[i][j]=d[i][k]*d[k][j];
	ll x=1;
	for(int i=0;i<N;i++)
		x*=d[i][0]/gcd(x,d[i][0]);
	for(int i=0;i<N;i++)
		res[i]=x*d[0][i]/d[i][0];
	int g=res[0]; 
	for(int i=1;i<N;i++)g=gcd(g,res[i]);
	for(int i=0;i<N;i++)printf("%lld ",res[i]/g);
	return 0;
}
#elif other3
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());

        var graph = new (int p, int q)?[n, n];
        for (var idx = 0; idx < n - 1; idx++)
        {
            var abpq = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var a = abpq[0];
            var b = abpq[1];
            var p = abpq[2];
            var q = abpq[3];

            graph[a, b] = (q, p);
            graph[b, a] = (p, q);
        }

        var pqs = new (long p, long q)?[n];
        pqs[0] = (1, 1);

        var queue = new Queue<int>();
        queue.Enqueue(0);

        while (queue.TryDequeue(out var src))
        {
            for (var dst = 0; dst < n; dst++)
                if (graph[src, dst].HasValue && !pqs[dst].HasValue)
                {
                    var p = pqs[src].Value.p * graph[src, dst].Value.p;
                    var q = pqs[src].Value.q * graph[src, dst].Value.q;
                    var g = GCD(p, q);

                    pqs[dst] = (p / g, q / g);
                    queue.Enqueue(dst);
                }
        }

        var lcm = pqs.Select(v => v.Value.q).Aggregate((l, r) => l / GCD(l, r) * r);
        foreach (var pq in pqs)
        {
            sw.Write(pq.Value.p * lcm / pq.Value.q);
            sw.Write(' ');
        }
    }

    private static long GCD(long x, long y)
    {
        while (x != 0 && y != 0)
            if (x > y)
                x %= y;
            else
                y %= x;

        return Math.Max(x, y);

    }
}
#endif
}
