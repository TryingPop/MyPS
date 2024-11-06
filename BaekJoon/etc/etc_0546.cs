using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 16
이름 : 배성훈
내용 : 열혈강호
    문제번호 : 11375번

    이분 매칭 문제다
    유튜브 영상을 참고해서 풀었다

    아이디어는 다음과 같다
    그리디 알고리즘이 쓰인다
    먼저 가치가 높은 것을 잇는다 (여기서는 가치가 모두 같다) 그리고, 다음 노드로 간다
    해당 노드로 간선을 이을려고 할 때, 이미 주인이 있다면
    해당 노드를 다른 노드로 이을 수 있는지 판별한다 없으면 다른 간선을 알아본다
    이렇게 각 노드별로 진행하면 최대로 이을 수 있는 경우의 수가 나온다
*/

namespace BaekJoon.etc
{
    internal class etc_0546
    {

        static void Main546(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            int n = ReadInt();
            int m = ReadInt();

            List<int>[] line = new List<int>[n + 1];
            bool[] visit = new bool[m + 1];
            int[] work = new int[m + 1];
            int ret = 0;

            Solve();
            Console.WriteLine(ret);

            sr.Close();

            void Solve()
            {

                for (int i = 1; i <= n; i++)
                {

                    int len = ReadInt();
                    line[i] = new(len);
                    for (int j = 0; j < len; j++)
                    {

                        line[i].Add(ReadInt());
                    }
                }

                for (int i = 1; i <= n; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i)) ret++;
                }
            }

            bool DFS(int _n)
            {

                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];

                    if (visit[next]) continue;
                    visit[next] = true;

                    if (work[next] == 0 || DFS(work[next]))
                    {

                        work[next] = _n;
                        return true;
                    }
                }

                return false;
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
// #include <unistd.h>
// #include <sys/stat.h>
// #include <sys/mman.h>

int buf[36];
char* s = (char*) mmap(0, buf[12], 3, 2, 0, fstat(0, (struct stat*)buf)), *q = s;
inline int rI() {
  int x=0; bool e;
  q += e = *q == '-';
  while(*q >= '0') x = x*10 + *q++ - '0'; q++;
  return e ? -x : x;
}

// #include <iostream>
// #include <stack>
// #include <algorithm>
using namespace std;

int n,m,t,ans,p;
bool w[1000][1000];
int r[1000],c[1000];
stack<int> st;

int main(){
    n=rI(),m=rI();
    for (int i=0; i<n; i++){
    	r[i]=rI();
    	for (int j=0; j<r[i]; j++){
    		t=rI(); t--;
    		w[i][t]=true; c[t]++;
		}	
	}
	for (int i=0; i<n; i++)
	{if (r[i]==1) {st.push(i);} }
	for (int i=0; i<m; i++)
	{if (c[i]==1) {st.push(i+1000);} }
	while (!st.empty()){
		t=st.top(); st.pop();
		if (t<1000){
			if (!r[t]) {continue;}
			for (p=0; !w[t][p]; p++);
			ans++; c[p]=0;
			for (int i=0; i<n; i++){
				if (w[i][p]){
					r[i]--; w[i][p]=false;
					if (r[i]==1) {st.push(i);}
				}
			}
			continue;
		}
		t-=1000;
		if (!c[t]) {continue;}
		for (p=0; !w[p][t]; p++);
		r[p]=0; ans++;
		for (int i=0; i<n; i++){
			if (w[p][i]){
				c[i]--; w[p][i]=false;
				if (c[i]==1) {st.push(i+1000);}
			}
		}
	}
	cout << ans+min(n-count(r,r+n,0),m-count(c,c+m,0));
    return 0;
}
#elif other2
public static class EnumExt
{
    public static void Deconstruct<T>(this IEnumerable<T> src, out T a0, out T a1)
    {
        if (src == null) throw new ArgumentNullException(nameof(src));
        var enumerator = src.GetEnumerator();
        enumerator.MoveNext();
        a0 = enumerator.Current;
        enumerator.MoveNext();
        a1 = enumerator.Current;
        enumerator.Dispose();
    }
}

internal class Program
{

    private static void Main(string[] args)
    {
        var (n, m) = Console.ReadLine()!.Split(' ').Select(int.Parse);
        var graph = new List<int>[n + 1];
        for (var i = 1; i <= n; i++)
        {
            graph[i] = new();
            var enumerator = Console.ReadLine()!.Split(' ').Select(int.Parse).GetEnumerator();
            enumerator.MoveNext();
            var k = enumerator.Current;
            for (var j = 0; j < k; j++)
            {
                enumerator.MoveNext();
                graph[i].Add(enumerator.Current);
            }
        }
        var visited = new bool[m + 1];
        var d = new int[m + 1];
        bool dfs(int cur)
        {
            foreach (int t in graph![cur])
            {
                if (visited![t]) continue;
                visited![t] = true;
                if (d![t] == 0 || dfs(d[t]))
                {
                    d![t] = cur;
                    return true;
                }
            }
            return false;
        }
        var cnt = 0;
        for (var i = 1; i <= n; i++)
        {
            Array.Fill(visited, false);
            if (dfs(i)) cnt++;
        }
        Console.WriteLine(cnt);
    }
}
#elif other3
#endif
}
