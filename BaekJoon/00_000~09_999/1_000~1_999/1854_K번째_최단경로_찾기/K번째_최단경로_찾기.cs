using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 1
이름 : 배성훈
내용 : K번째 최단경로 찾기
    문제번호 : 1854번

    다익스트라, 최단 경로, 우선순위 큐 문제다
    아이디어는 다음과 같다

    우선순위 큐가 의미하는게 이른 순서로 현재 시간에서 도착한 장소가 된다
    그래서 먼저 k번 방문하면 해당 장소가 k번째 최단 거리가 된다

    그리고 k번 방문하면 다음 탐색을 중지하면 된다

    이전 노드에 한해 k + 1 번 이상 방문한 노드가 있지 않을까 생각할 수 있지만
    현재 1, 2, ... , k번째 최단 경로는 바로 직전 노드를 생각해보면
    많아야 k개 임을 확인할 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_1014
    {

        static void Main1014(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n, m, k;
            List<(int dst, int dis)>[] edge;
            int[] dis;
            int[] cnt;
            Solve();
            void Solve()
            {

                Input();

                Dijkstra();

                GetRet();
            }

            void GetRet()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                for (int i = 1; i <= n; i++)
                {

                    if (cnt[i] < k) sw.Write("-1\n");
                    else sw.Write($"{dis[i]}\n");
                }
                sw.Close();
            }

            void Dijkstra()
            {

                // 메모리 터질꺼 같다
                PriorityQueue<(int dst, int dis), int> pq = new(m * k);
                dis = new int[n + 1];
                cnt = new int[n + 1];

                pq.Enqueue((1, 0), 0);

                while (pq.Count > 0)
                {

                    (int dst, int dis) node = pq.Dequeue();
                    if (cnt[node.dst] == k) continue;
                    cnt[node.dst]++;
                    if (cnt[node.dst] == k) dis[node.dst] = node.dis;

                    for (int i = 0; i < edge[node.dst].Count; i++)
                    {

                        int next = edge[node.dst][i].dst;
                        if (cnt[next] == k) continue;
                        int nDis = node.dis + edge[node.dst][i].dis;
                        pq.Enqueue((next, nDis), nDis);
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
                k = ReadInt();

                edge = new List<(int dst, int dis)>[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    int d = ReadInt();

                    edge[f].Add((t, d));
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
public static void Cin(out int num)=>num=int.Parse(Cin());
public static void Cin(out string str)=>str=Cin();
public static void Cin(out string a,out string b,char c=' '){var r=Cin().Split(c);a=r[0];b=r[1];}
public static void Cin(out int[] numarr,char c= ' ')=>numarr=Array.ConvertAll(Cin().Split(c),int.Parse);
public static void Cin(out string[] strarr,char c=' ')=>strarr=Cin().Split(c);
public static void Cin(out double d)=>d=double.Parse(Cin());
public static void Cin(out string t,out int n){var s=Cin().Split();n=int.Parse(s[1]);t=s[0];}
public static void Cin(out int a,out int b,char c= ' '){Cin(out int[] s);(a,b)=(s[0],s[1]);}
public static void Cin(out int a,out int b,out int c,char e=' '){Cin(out int[] s);(a,b,c)=(s[0],s[1],s[2]);}
public static void Cin(out int a,out int b,out int c,out int d,char e = ' '){Cin(out int[] arr,e);(a,b,c,d)=(arr[0],arr[1],arr[2],arr[3]);}
public static void Cin(out int n,out string t) {var s=Cin().Split();n=int.Parse(s[0]);t=s[1];}
public static object? Cout{set{writer.Write(value);}}
public static object? Coutln{set{writer.WriteLine(value);}}
public static void Main() {Program.Coding();writer.Flush();}
}
class Program {
    public static void Coding() {
        Cin(out int dotcount,out int linescount, out int select);
        var lines = new LinkedList<(int dot,int time)>[dotcount];
        for(int i=0;i<dotcount;i++) lines[i]=new();
        while(linescount-->0) {
            Cin(out int a,out int b,out int t);
            lines[--a].AddLast((--b,t));
        }
        var min = new PriorityQueue<int,int>[dotcount];
        for(int i=0;i<dotcount;i++) min[i]=new();
        bool Refresh(int index,int value) {
            // 가장 큰수가 먼저 나오도록 우선순위 큐를 구성
            if (min[index].Count >= select) {
                int m = min[index].Peek();
                if (value < m) {
                    min[index].Enqueue(value,-value);
                    min[index].Dequeue();
                    return true;
                }
                return false;
            }
            min[index].Enqueue(value,-value);
            return true;
        }
        Refresh(0,0); // 시작지점에서 최단경로는 0
        PriorityQueue<(int dot,int len),int> queue = new();
        queue.Enqueue((0,0),0);
        while(queue.Count is not 0) {
            var ret = queue.Dequeue();
            int me = ret.dot;
            foreach(var other in lines[me]) {
                int len = ret.len + other.time;
                //탐색 허가
                if (Refresh(other.dot,len)) {
                    queue.Enqueue((other.dot,len),len);
                }
                //아님 불허
            }
        }
        for(int i=0;i<dotcount;i++) {
            if (min[i].Count < select) Coutln = -1;
            else Coutln = min[i].Peek();
        }
    }
}
#elif other2
// #include <iostream>
// #include <algorithm>
// #include <vector>
// #include <queue>
// #include <tuple>

using namespace std;
/*
1. k번째 최단경로 구하기
2. ElogV의 로직 사용하기
3. node는 최대 힙 배열로 만들어주기
4. node[cur]의 사이즈가 k 이상일 경우 제거하고 삽입하기
5. 각 노드의 사이즈를 체크하고 사이즈가 k인 배열만 top을 출력해주기
*/
const int INF = 0x7fffffff;



struct Edge
{
	int to, cost;
	
};
struct cmp
{
	bool operator()(const Edge &a, const Edge &b) {
		return a.cost > b.cost;
	}
};

priority_queue<int> node[1005];
vector<Edge> adj[1005];
priority_queue<Edge, vector<Edge>, cmp> pq;
int n, m, k;

int main() {
	//빠른 입출력을 위해서 사용하기
	ios_base::sync_with_stdio(false);  cin.tie(NULL); cout.tie(NULL);
	cin >> n >> m >> k;
	while (m--)
	{
		int from, to, cost;
		cin >> from >> to >> cost;
		adj[from].push_back({ to, cost });
	}
	node[1].push(0);
	pq.push({ 1, 0 });

	while (!pq.empty())
	{
		auto cur = pq.top(); pq.pop();
		if (node[cur.to].top() < cur.cost) continue;

		for (auto nxt : adj[cur.to])
		{
			if (node[nxt.to].size() < k) {
				node[nxt.to].push({ cur.cost + nxt.cost });
				pq.push({ nxt.to, cur.cost + nxt.cost });
			}
			else if (node[nxt.to].top() > cur.cost + nxt.cost) {
				node[nxt.to].pop();
				node[nxt.to].push({ cur.cost + nxt.cost });
				pq.push({ nxt.to, cur.cost + nxt.cost });
			}
		}
	}
	for (int i = 1; i <= n; i++)
	{
		if (node[i].size() == k) {
			cout << node[i].top() << '\n';
		}
		else
		{
			cout << -1 << '\n';
		}
	}
	
	
	return 0;
}
#endif
}
