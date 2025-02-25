using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 18
이름 : 배성훈
내용 : 단절점
    문제번호 : 11266번

    그래프 이론, 단절점과 단절선 문제다.
    단절점은 해당 노드를 제거했을 때 그래프가 나뉘는 경우 단절점이라 한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1203
    {

        static void Main1203(string[] args)
        {

            int n, m;
            List<int>[] edge;
            int[] order;
            bool[] cutting;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int num = 1;
                order = new int[n + 1];
                cutting = new bool[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    if (order[i] == 0) DFS(i);
                }

                int[] ret = new int[n + 1];
                int len = 0;
                for (int i = 1; i <= n; i++)
                {

                    if (cutting[i]) ret[len++] = i;
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                sw.Write($"{len}\n");

                for (int i = 0; i < len; i++)
                {

                    sw.Write($"{ret[i]} ");
                }

                int DFS(int _cur, bool _isRoot = true)
                {

                    order[_cur] = num++;
                    int degree = 0;
                    int ret = order[_cur];

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (order[next] > 0)
                        {

                            ret = Math.Min(ret, order[next]);
                            continue;
                        }

                        degree++;

                        int chk = DFS(next, false);

                        if (!_isRoot && chk >= order[_cur])
                        {

                            cutting[_cur] = true;
                        }

                        ret = Math.Min(ret, chk);
                    }

                    if (_isRoot && degree >= 2) cutting[_cur] = true;

                    return ret;
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                edge = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    edge[f].Add(t);
                    edge[t].Add(f);
                }

                sr.Close();

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
// #include<cstdio>
// #include<vector>
using namespace std;
int V, E, dfsn[10001], low[10001], ord, k;
bool ap[10001];
vector<int> adj[10001];
char buf[1<<17];
inline char read(){
	static int idx=1<<17;
	if(idx==1<<17){
		fread(buf, 1, 1<<17, stdin);
		idx=0;
	}
	return buf[idx++];
}
inline int readInt(){
	int sum=0;
	char cur=read();
	while(cur==10 || cur==32)	cur=read();
	while(cur>=48 && cur<=57){
		sum=sum*10+cur-48;
		cur=read();
	}
	return sum;
}
void dfs(int u, int v){
	dfsn[v]=low[v]=++ord;
	int child=0;
	for(int n:adj[v]){
		if(n==u)	continue;
		if(!dfsn[n]){
			child++;
			dfs(v, n);
			if(!u && child>1)	ap[v]=1;
			else if(u && low[n]>=dfsn[v])	ap[v]=1;
			low[v]=min(low[v], low[n]);
		}
		else	low[v]=min(low[v], dfsn[n]);
	}
	if(ap[v])	++k;
}
int main(){
	V=readInt();E=readInt();
	int A, B;
	while(E--){
		A=readInt();B=readInt();
		adj[A].push_back(B);
		adj[B].push_back(A);
	}
	for(int i=1;i<=V;++i)if(!dfsn[i])	dfs(0, i);
	printf("%d\n", k);
	for(int i=1;i<=V;++i)if(ap[i])	printf("%d ", i);
	return 0;
}
#endif
}
