using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 29
이름 : 배성훈
내용 : 키 순서
    문제번호 : 2458번

    플로이드 워셜 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1739
    {

        static void Main1739(string[] args)
        {

#if first
            int INF = 123_456;

            int n;
            int[][] fw1, fw2;

            Input();

            GetRet();

            void GetRet()
            {

                FloydWarshall(fw1);
                FloydWarshall(fw2);

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    int cnt = 1;
                    for (int j = 0; j < n; j++)
                    {

                        if (i == j) continue;
                        if (fw1[i][j] != INF || fw2[i][j] != INF) cnt++;
                    }

                    if (cnt == n) ret++;
                }

                Console.Write(ret);

                void FloydWarshall(int[][] _fw)
                {

                    for (int mid = 0; mid < n; mid++)
                    {

                        for (int start = 0; start < n; start++)
                        {

                            if (_fw[start][mid] == INF) continue;
                            for (int end = 0; end < n; end++)
                            {

                                if (_fw[mid][end] == INF) continue;
                                _fw[start][end] = Math.Min(_fw[start][end], _fw[start][mid] + _fw[mid][end]);
                            }
                        }
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                fw1 = new int[n][];
                fw2 = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    fw1[i] = new int[n];
                    fw2[i] = new int[n];

                    Array.Fill(fw1[i], INF);
                    Array.Fill(fw2[i], INF);
                }

                int m = ReadInt();
                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt() - 1;
                    int t = ReadInt() - 1;

                    fw1[f][t] = 1;
                    fw2[t][f] = 1;
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';
                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
#else

            int n;
            bool[][] fw;

            Input();

            GetRet();

            void GetRet()
            {

                FloydWarshall(fw);

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    int cnt = 1;
                    for (int j = 0; j < n; j++)
                    {

                        if (i == j) continue;
                        if (fw[i][j] || fw[j][i]) cnt++;
                    }

                    if (cnt == n) ret++;
                }

                Console.Write(ret);

                void FloydWarshall(bool[][] _fw)
                {

                    for (int mid = 0; mid < n; mid++)
                    {

                        for (int start = 0; start < n; start++)
                        {

                            if (!_fw[start][mid]) continue;
                            for (int end = 0; end < n; end++)
                            {

                                if (!_fw[mid][end]) continue;
                                _fw[start][end] = true;
                            }
                        }
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                fw = new bool[n][];
                for (int i = 0; i < n; i++)
                {

                    fw[i] = new bool[n];
                }

                int m = ReadInt();
                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt() - 1;
                    int t = ReadInt() - 1;

                    fw[f][t] = true;
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';
                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
#endif

        }
    }

#if other
// #pragma GCC optimize("Ofast")
// #pragma GCC target("arch=skylake")
// #include<stdio.h>
// #include<bitset>
using namespace std;
// #include <sys/stat.h>
// #include <sys/mman.h>
signed I[36];char*J=(char*)mmap(0,I[12],1,2,0,fstat(0,(struct stat*)I));
int getu(){int x=0;do x=x*10+*J-'0';while(*++J>='0');++J;return x;}
int geti(){bool e=*J=='-';J+=e;return(e?-1:1)*getu();}
int n,m,a,b,ans,done[501],cnt[501];
bitset<501> bit[501];
int v[2][501][501],c[2][501];
void dfs(int id,int k){
	bit[k][k]=1;
	done[k]=1;
	for(int i=0,t;i<c[id][k];i++){
        t=v[id][k][i];
		if(!done[t]) 
			dfs(id,t);
		bit[k]|=bit[t];	
	}
}
int main(){
	n=getu(); m=getu();
	while(m--){
		a=getu(); b=getu();
		v[0][a][c[0][a]++]=b;
		v[1][b][c[1][b]++]=a;
	}
	for(int i=1;i<=n;i++){
		if(!done[i])
			dfs(0,i);
		cnt[i]+=bit[i].count();
	}
	for(int i=1;i<=n;i++) bit[i].reset(),done[i]=0;
	for(int i=1;i<=n;i++){
		if(!done[i])
			dfs(1,i);
		cnt[i]+=bit[i].count();
		if(cnt[i]==n+1) ans++;
	}
	printf("%d",ans);
}

#elif other2
using System;
using System.Collections.Generic;
using System.Linq;

// CapitalLetters for class names and methods, camelCase for variable names.
// Write your code clearly enough so that it doesn't need to be commented, or at least, so that it rarely needs to be commented.

namespace Testpad
{
    public class BaekJoon2458
    {
        // 위상 정렬 문제가 아닌듯
        static void Main()
        {
            int[] inputs = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int n = inputs[0]; // 정점의 수
            int m = inputs[1]; // 간선의 수

            List<int>[] lines = new List<int>[n]; // 인접 리스트
            for (int i = 0; i < n; i++)
            {
                lines[i] = new List<int>();
            }

            // 간선들을 받음
            bool[,] connection = new bool[n, n]; // 두 정점의 연결 여부

            for (int i = 0; i < m; i++)
            {
                inputs = Console.ReadLine().Split().Select(int.Parse).ToArray();
                connection[inputs[0] - 1, inputs[1] - 1] = true;
                lines[inputs[0] - 1].Add(inputs[1] - 1);
            }

            // BFS로 연결들을 확인
            Queue<int> queue = new Queue<int>();

            for (int i = 0; i < n; i++)
            {
                bool[] used = new bool[n];
                queue.Clear();
                queue.Enqueue(i);
                used[i] = true;

                while (queue.Count > 0)
                {
                    foreach (var item in lines[queue.Peek()])
                    {
                        if (used[item] == false)
                        {
                            queue.Enqueue(item);
                            connection[i, item] = true;
                            used[item] = true;
                        }
                    }

                    queue.Dequeue();
                }
            }
            

            // 다른 모든 정점들과 연결되는 정점들의 개수를 확인해 출력
            int result = 0;

            for (int i = 0; i < n; i++)
            {
                bool connected = true;

                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        connection[i, j] = true;
                    }

                    if (connection[i, j] == false && connection[j, i] == false)
                    {
                        connected = false;
                        break;
                    }
                }

                if (connected == true)
                {
                    result++;
                }
            }

            Console.Write(result);
        }
    }
}
#endif
}
