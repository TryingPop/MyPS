using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 17
이름 : 배성훈
내용 : Jumping Yoshi
    문제번호 : 11460번

    그래프 문제다.
    우선 각 i에 대해 arr[i] + arr[j] = j - i 이거나 arr[i] + arr[j] = i - j인 j를 찾아야 한다.
    각 i, j에 대해 조사하는 방법밖에 떠오르지 않았다.
    즉, N^2의 방법만 떠올랐다.

    N이 100만까지 오므로 사용이 불가능하다.    
    결국 풀이를 봤다.

    그러니, 다음과 같이 값을 저장한다.
    edge[0][i + arr[i]]좌표에 i의 값을 저장한다.
    edge[1][i - arr[i]]좌표에 i의 값을 저장한다.
    
    그러면 이제 방문한 j에대해 j - arr[j]에 대해 edge[0][j - arr[j]]의 원소 k를 보면
    edge[0]의 정의로 k + arr[k] = j - arr[j]가 성립함을 알 수 있다.
    그래서 arr[j] + arr[k] = j - k 이므로 k로 이동할 수 있다.
    
    이제 j + arr[j]에 대해 edge[1][j + arr[j]]의 원소 k를 보자.
    edge[1]의 정의로 j + arr[j] = k - arr[k]이고, arr[j] + arr[k] = k - j이다.
    이는 j에서 k로 이동 가능함을 뜻한다.

    물론 arr[i] >= 0이므로 k - j >= 0 이 보장된다!
    이렇게 조사하는 경우 i + arr[i]는 많아야 N개 조사하고, 마찬가지로 i - arr[i]도 N개 조사한다.
    즉, 전체 방문 조사는 O(N x 3)이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1891
    {

        static void Main1891(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int MAX = 1_000_000;
            int n;
            int[] arr = new int[MAX];
            bool[] visit = new bool[MAX];
            List<int>[][] edge = new List<int>[2][];
            Queue<int> q = new(MAX);
            for (int i = 0; i < 2; i++)
            {

                edge[i] = new List<int>[MAX];
                for (int j = 0; j < MAX; j++)
                {

                    edge[i][j] = new();
                }
            }

            while (Input())
            {

                GetRet();
            }

            void GetRet()
            {

                q.Clear();
                visit[0] = true;
                q.Enqueue(0);

                // O(3N)
                while (q.Count > 0)
                {

                    int node = q.Dequeue();

                    int chk = node + arr[node];
                    if (chk < n)
                    {

                        // arr[node] + arr[next] = next - node인 next 찾기
                        for (int i = 0; i < edge[1][chk].Count; i++)
                        {

                            int next = edge[1][chk][i];
                            if (visit[next]) continue;
                            visit[next] = true;
                            q.Enqueue(next);
                        }
                    }

                    chk = node - arr[node];
                    if (chk >= 0)
                    {

                        // arr[node] + arr[next] = node - next인 next 찾기
                        for (int i = 0; i < edge[0][chk].Count; i++)
                        {

                            int next = edge[0][chk][i];
                            if (visit[next]) continue;
                            visit[next] = true;
                            q.Enqueue(next);
                        }
                    }
                }

                int ret = 0;
                for (int i = n - 1; i >= 0; i--)
                {

                    if (!visit[i]) continue;
                    ret = i;
                    break;
                }

                sw.Write(ret);
                sw.Write('\n');
            }

            bool Input()
            {

                n = ReadInt();
                if (n == 0) return false;
                for (int i = 0; i < n; i++)
                {

                    edge[0][i].Clear();
                    edge[1][i].Clear();
                    visit[i] = false;
                }

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                    // arr[i] + arr[j] = j - i 이거나 arr[i] + arr[j] = i - j인 좌표 찾기
                    // 이는 arr[i] + i = j - arr[j] 이거나 i - arr[i] = j + arr[j] 가 성립
                    // 그래서 arr[k] + k는 edge[0] 에 저장
                    // k - arr[k] 는 edge[1]에 저장
                    if (i + arr[i] < n) edge[0][i + arr[i]].Add(i);
                    if (i - arr[i] >= 0) edge[1][i - arr[i]].Add(i);
                }

                return true;
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
    }

#if other
// #include <bits/stdc++.h>

using namespace std;

int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);
    cout.tie(NULL);

    while(true)
    {
        int N;
        cin>>N;
        if(N==0) return 0;

        vector<int> adj[N];
        int pebbles[N];
        bool visited[N];
        memset(visited,false,sizeof(visited));

        for(int i=0;i<N;i++)
        {
            int ai;
            cin>>ai;
            pebbles[i] = ai;
            if(ai == 0)
            {
                adj[i].emplace_back(i);
                continue;
            }
            if(i+ai<N) adj[i+ai].emplace_back(i);
            if(i-ai>=0) adj[i-ai].emplace_back(i);
        }

        /*for(int i=0;i<N;i++)
        {
            cout<<"i : "<<i<<"  ";
            for(int j=0;j<adj[i].size();j++)
            {
                cout<<adj[i][j]<<' ';
            }
            cout<<'\n';
        }*/

        queue<int> q;
        q.push(0);
        visited[0] = true;
        int maxidx = 0;
        while(!q.empty())
        {
            int here = q.front();
            q.pop();
            maxidx = max(maxidx,here);

            if(here+pebbles[here]<N)
            {
                for(auto there : adj[here+pebbles[here]])
                {
                    if(!visited[there]&&here+pebbles[here]<=there)
                    {
                        visited[there] = true;
                        q.push(there);
                    }
                }
            }
            if(here-pebbles[here]>=0)
            {
                for(auto there : adj[here-pebbles[here]])
                {
                    if(!visited[there]&&here-pebbles[here]>=there)
                    {
                        visited[there] = true;
                        q.push(there);
                    }
                }
            }
        }
        cout<<maxidx<<'\n';
    }
}

#endif
}
