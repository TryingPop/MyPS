using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 5
이름 : 배성훈
내용 : 텀 프로젝트
    문제번호 : 9466번

    그래프 이론, 그래프 탐색, 깊이 우선 탐색 문제다.
    사이클을 찾고 길이를 찾아야 한다.

    각 노드마다 간선이 유일하다.
    간선을 따라가면 사이클이 있는 경우 노드가 반복된다.
    그래서 이미 지나온 곳을 방문한다면 
    해당 노드 번호를 반환하고 종료 시킨뒤 해당 노드에서 사이클을 찾아갔다.
    사이클 A를 찾은 경우 사이클 외에서 A로 오는 경우도 있다.
    이 경우 이미 탐색한 사이클을 탐색하기에 DFS탐색에 그룹 번호를 붙여 반복을 막았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1157
    {

        static void Main1157(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n;
            int[] arr, group;

            Solve();
            void Solve()
            {

                Init();

                int t = ReadInt();

                while (t-- > 0)
                {

                    Input();

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                arr = new int[100_001];
                group = new int[100_001];
            }

            void GetRet()
            {

                int g = 0;
                int ret = n;
                for (int i = 1; i <= n; i++)
                {

                    if (group[i] != 0) continue;
                    group[i] = ++g;
                    int find = DFS(i);

                    if (find == -1) continue;
                    ret -= GetCycleLength(find);
                }

                sw.Write($"{ret}\n");

                int GetCycleLength(int _n)
                {

                    int ret = 1;
                    int find = _n;
                    while (arr[_n] != find)
                    {

                        _n = arr[_n];
                        ret++;
                    }

                    return ret;
                }

                int DFS(int _n)
                {

                    int next = arr[_n];
                    if (group[next] == 0)
                    {

                        group[next] = g;
                        return DFS(next);
                    }
                    else if (group[next] != g) return -1;

                    return next;
                }
            }

            void Input()
            {

                n = ReadInt();
                
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                    group[i] = 0;
                }
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
// #include <cstdio>
// #include <cstring>
// #include <sys/stat.h>
// #include <sys/mman.h>
using namespace std;

int n, cnt, par[100001], visited[100001];

void dfs(int cur, int color) {
	visited[cur] = color;
	if (visited[par[cur]] == color) {
		int p = par[cur];
		do { cnt++, p = par[p]; } while (p ^ par[cur]);
	}
	else if (!visited[par[cur]]) dfs(par[cur], color);
}

int main() {
    struct stat st; fstat(0, &st);
	char* p = (char*)mmap(0, st.st_size, PROT_READ, MAP_SHARED, 0, 0);
	auto ReadInt = [&]() {
		int ret = 0;
		for (char c = *p++; c & 16; ret = 10 * ret + (c & 15), c = *p++);
		return ret;
	};
    
    for (int N = ReadInt(); N--;) {
		n = ReadInt(), cnt = 0;
		memset(visited + 1, 0, n << 2);
		for (int i = 1; i <= n; i++) par[i] = ReadInt();
		for (int i = 1, color = 1; i <= n; i++) if (!visited[i]) dfs(i, color++);
        printf("%d\n", n - cnt);
	}
}
#endif
}
