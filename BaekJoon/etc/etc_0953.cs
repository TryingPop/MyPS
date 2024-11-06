using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 8
이름 : 배성훈
내용 : 최대 유량
    문제번호 : 6086번

    최대 유량, 시뮬레이션, 구현 문제다
    에드먼드 카프 알고리즘으로 해결했다

    A -> Z로 가는 경로를 찾으면 우선 최대한 유량을 흘린다
    여기서는 직렬과 병렬의 방법으로 합칠 수 있는 경로만 주어지기에
    A 에서 Z 경로를 찾은 뒤에 최대 유량을 흘리면서 값을 찾으면 된다

    A -> B -> C -> Z 일수도,
    A -> C -> B -> Z 일수도 있어
    양방향에 c의 용량을 주고 유량을 흘릴 때 
    양방향에 추가시키며 시뮬레이션 돌려 찾았다

    방법은 에드먼드 카프를 약간 변형시켜 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0953
    {

        static void Main953(string[] args)
        {

            StreamReader sr;
            List<int>[] edge;
            (int dst, int c, int f, int rev)[] e;
            int[] prev, path;
            Queue<int> q;

            Solve();
            void Solve()
            {

                Input();

                Console.Write(GetRet());
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                edge = new List<int>[52];
                
                for (int i = 0; i < 52; i++)
                {

                    edge[i] = new();
                }

                int len = ReadInt();
                e = new (int dst, int c, int f, int rev)[2 * len];

                for (int i = 0, idx = 0; i < len; i++, idx += 2)
                {

                    int f = ReadPipe();
                    int b = ReadPipe();
                    int c = ReadInt();

                    e[idx] = (b, c, 0, idx + 1);
                    e[idx + 1] = (f, c, 0, idx);

                    edge[f].Add(idx);
                    edge[b].Add(idx + 1);
                }

                sr.Close();
            }

            int ReadPipe()
            {

                int ret = sr.Read();
                sr.Read();

                if ('a' <= ret) ret -= 'a' - 26;
                else ret -= 'A';

                return ret;
            }

            int GetRet()
            {

                int ret = 0;
                q = new(52);
                prev = new int[52];
                path = new int[52];

                while (true)
                {

                    q.Clear();
                    Array.Fill(prev, -1);
                    q.Enqueue(0);

                    while(q.Count > 0)
                    {

                        int node = q.Dequeue();

                        for (int i = 0; i < edge[node].Count; i++)
                        {

                            int idx = edge[node][i];
                            int next = e[idx].dst;

                            if (e[idx].f < e[idx].c && prev[next] == -1)
                            {

                                prev[next] = node;
                                path[next] = idx;
                                q.Enqueue(next);
                                if (next == 25) break;
                            }
                        }
                    }

                    if (prev[25] == -1) break;
                    int flow = 1_000_000_000;
                    for (int i = 25; i != 0; i = prev[i])
                    {

                        flow = Math.Min(flow, e[path[i]].c - e[path[i]].f);
                    }

                    for (int i = 25; i != 0; i = prev[i])
                    {

                        int idx1 = path[i];
                        int idx2 = e[path[i]].rev;

                        e[idx1].f += flow;
                        e[idx2].f += flow;
                    }

                    ret += flow;
                }

                return ret;
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
// #include<stdio.h>

int m, a[52][52], b[52], z, t;
char x, y;
int f(int d, int e) {
	if (d == 25) return e;
	b[d] = 1;
	for (int i = 0; i < 52; i++) {
		if (a[d][i] && !b[i]) {
			int q = f(i, e < a[d][i] ? e : a[d][i]);
			if (q) {
				a[d][i] -= q;
				a[i][d] += q;
				if (!d) t += q;
				b[d] = 0;
				return q;
			}
		}
	}
	b[d] = 0;
	return 0;
}
int main() {
	scanf("%d", &m);
	for (int i = 0; i < m; i++) {
		scanf(" %c %c %d", &x, &y, &z);
		if (x > 96) x -= 6;
		if (y > 96) y -= 6;
		x -= 65, y -= 65;
		a[x][y] += z;
		a[y][x] += z;
	}
	while (f(0, 1000));
	printf("%d", t);
}
#elif other2
// #include <stdio.h>
int min;
int sum=0;
int pipe[52][52]={0,};
int Ford_Fulkerson(int visited[],int way[],int a){
    if(way[a]==25)return 1;
    for(int i=0;i<52;i++){
        if(pipe[way[a]][i]!=0&&visited[i]==0){
            visited[i]=1;
            way[a+1]=i;
            if(pipe[way[a]][i]<min)min=pipe[way[a]][i];
            if(Ford_Fulkerson(visited,way,a+1)==1){
                pipe[way[a]][i]-=min;
                return 1;
            }
        }
    }
    return 0;
}
int Char_To_Index(char k){
    if(k>='a'&&k<='z')return k-'a'+26;
    else return k-'A';
}
int main(void){
    int n;
    scanf("%d",&n);
    for(int i=0;i<n;i++){
        char ch1, ch2;
        int num;
        scanf(" %c %c %d", &ch1, &ch2, &num);
        int in1= Char_To_Index(ch1);
        int in2= Char_To_Index(ch2);
        if(in1==in2)continue;
        pipe[in1][in2]+=num;
        pipe[in2][in1]+=num;
    }
    for(int i=0;i<52;i++){
        if(pipe[0][i]!=0){
            int visited[52]={1,0,};
            visited[i]=1;
            int way[52]={0,i,};
            min=pipe[0][i];
            if(Ford_Fulkerson(visited,way,1)==1){
                pipe[0][i]-=min;
                sum+=min;
                i--;
            }
        }
    }
    printf("%d",sum);
}
#endif
}
