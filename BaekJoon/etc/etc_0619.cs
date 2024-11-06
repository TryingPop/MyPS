using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 26
이름 : 배성훈
내용 : 고양이와 개
    문제번호 : 3683번

    이분 매칭 문제다
    아이디어는 다음과 같다
    상어의 저녁식사 문제처럼
    양립할 수 없는 사람끼리 최대한 매칭시킨다
    그러면 최대한 매칭시킨게 양립할 수 없는 사람 중 최소 경우가 된다

    예를들어 
        C1, D1
        C1, D1
        D1, C1
    이렇게 있다고 하면 양립 못하는 사람은 1 - 3, 2 - 3이다
    여기서 이분 매칭을 실행하면 1개 매칭될것이고 양립할 수 없는 최소 그룹의 사람들을 모두 찾는다
    그리고 전체 사람에서 양립할 수없는 최소 사람을 빼면 다음 시청자가 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0619
    {

        static void Main619(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int test;

            int[] match;
            bool[] visit;

            int len;

            List<int>[] line;
            (bool pD, int pN, bool nD, int nN)[] vote;

            Solve();

            void Solve()
            {

                Input();

                sw = new(Console.OpenStandardOutput());
                while(test-- > 0)
                {

                    Init();
                    int ret = len;
                    for (int i = 1; i <= len; i++)
                    {

                        Array.Fill(visit, false, 1, len);
                        if (DFS(i)) ret--;
                    }

                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            bool DFS(int _n)
            {

                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];
                    if (visit[next]) continue;
                    visit[next] = true;

                    if (match[next] == 0 || DFS(match[next]))
                    {

                        match[next] = _n;
                        return true;
                    }
                }

                return false;
            }

            void Init()
            {

                ReadInt();
                ReadInt();
                len = ReadInt();

                for (int i = 1; i <= len; i++)
                {

                    line[i].Clear();
                }

                for(int i = 1; i <= len; i++)
                {

                    ReadVote(out bool pD, out int pN);
                    ReadVote(out bool nD, out int nN);

                    vote[i] = (pD, pN, nD, nN);
                }

                for (int i = 1; i < len; i++)
                {

                    for (int j = i + 1; j <= len; j++)
                    {

                        if ((vote[i].pD == vote[j].nD && vote[i].pN == vote[j].nN)
                            || (vote[i].nD == vote[j].pD && vote[i].nN == vote[j].pN))
                        {

                            if (vote[i].pD) line[i].Add(j);
                            else line[j].Add(i);
                        }
                    }
                }

                Array.Fill(match, 0, 1, len);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 8);

                test = ReadInt();
                match = new int[501];
                visit = new bool[501];

                line = new List<int>[501];
                for (int i = 1; i <= 500; i++)
                {

                    line[i] = new();
                }

                vote = new (bool pD, int pN, bool nD, int nN)[501];
            }

            void ReadVote(out bool _isDog, out int _num)
            {

                int c = sr.Read();
                _isDog = c == 'D';

                _num = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    _num = _num * 10 + c - '0';
                }

                return;
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
// #include <iostream>
// #include <vector>
// #include <cmath>
// #include <algorithm>
// #include <climits>
// #include <set>
// #include <map>
// #include <queue>
// #include <deque>
// #include <stack>
// #include <string>

using namespace std;
using pii=pair<int,int>;
using ll=long long;

void bfs(vector<vector<int>> &adj, vector<int> &lv, vector<int> &parent, vector<bool> &visited)
{
    queue<int> q;
    for(int i=0; i<adj.size(); i++){
        if(!visited[i]){
            lv[i]=1;
            q.push(i);
        }
        else lv[i]=-1;
    }
    while(!q.empty()){
        int now=q.front();
        q.pop();
        for(int next : adj[now]){
            if(parent[next]!=-1 && lv[parent[next]]==-1){
                lv[parent[next]]=lv[now]+1;
                q.push(parent[next]);
            }
        }
    }
}

bool dfs(vector<vector<int>> &adj, vector<int> &lv, vector<int> &parent, vector<bool> &visited, int now)
{
    for(int next : adj[now]){
        if(parent[next]==-1 || (lv[parent[next]]==lv[now]+1 && dfs(adj,lv,parent,visited,parent[next]))){
            parent[next]=now;
            visited[now]=true;
            return true;
        }
    }
    return false;
}

int main()
{
    ios::sync_with_stdio(false);
	cin.tie(0);
    int t;
    cin>>t;
    while(t--){
        int c,d,v;
        cin>>c>>d>>v;
        vector<vector<int>> adj(v);
        vector<int> lv(v);
        vector<int> parent(v,-1);
        vector<bool> visited(v);

        int answer=0;

        vector<pii> part;

        for(int i=0; i<v; i++){
            char anitype;
            int aninum;
            pii p;
            cin>>anitype>>aninum;
            p.first=(anitype=='C')?aninum:-aninum;
            cin>>anitype>>aninum;
            p.second=(anitype=='C')?aninum:-aninum;
            part.push_back(p);
        }

        for(int i=0; i<v; i++){
            for(int j=i+1; j<v; j++){
                if(part[i].first==part[j].second || part[i].second==part[j].first){
                    if(part[i].first>0) adj[i].push_back(j);
                    else adj[j].push_back(i);
                }
            }
        }

        while(true){
            bfs(adj,lv,parent,visited);
            int nowanswer=0;
            for(int i=0; i<v; i++) if(!visited[i] && dfs(adj,lv,parent,visited,i)) nowanswer+=1;
            if(nowanswer==0) break;
            answer+=nowanswer;
        }

        cout<<v-answer<<"\n";
    }

    return 0;
}

#elif other2
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;
import java.util.StringTokenizer;

public class Main {
	private static StringBuffer ret = new StringBuffer();
	private static BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
	private static StringTokenizer st;
	private static int T = 1, C, D, V, left[];
	private static String V_item[][];
	private static List<Integer> cat[][]; // [0:up, 1:down][cat_num] = n;
	private static List<Integer> dog[][]; // [0:up, 1:down][dog_num] = n;
	private static List<Integer> edge[];
	private static boolean check[];

	@Override
	public void finalize() throws Exception {
		br.close();
	}

	public static void main(String[] args) throws Exception {
		T = Integer.parseInt(br.readLine());
		while (T-- > 0) {
			set_input();
			solve();
		}

		System.out.println(ret);
	}

	private static void set_input() throws Exception {
		st = new StringTokenizer(br.readLine());
		C = Integer.parseInt(st.nextToken());
		D = Integer.parseInt(st.nextToken());
		V = Integer.parseInt(st.nextToken());
		V_item = new String[V + 1][2];
		cat = new List[2][C + 1];
		dog = new List[2][D + 1];
		for (int i = 1; i <= C; i++) {
			cat[0][i] = new ArrayList<>();
			cat[1][i] = new ArrayList<>();
		}
		for (int i = 1; i <= D; i++) {
			dog[0][i] = new ArrayList<>();
			dog[1][i] = new ArrayList<>();
		}
		String up, down;
		for (int i = 1; i <= V; i++) {
			st = new StringTokenizer(br.readLine());
			up = st.nextToken();
			down = st.nextToken();

			V_item[i][0] = up;
			V_item[i][1] = down;
			if (up.charAt(0) == 'C') {
				cat[0][Integer.parseInt(up.substring(1))].add(i);
			} else {
				dog[0][Integer.parseInt(up.substring(1))].add(i);
			}
			if (down.charAt(0) == 'C') {
				cat[1][Integer.parseInt(down.substring(1))].add(i);
			} else {
				dog[1][Integer.parseInt(down.substring(1))].add(i);
			}
		}
	}

	private static void solve() throws Exception {
		edge = new List[V + 1];
		for (int i = 1; i <= V; i++) {
			edge[i] = new ArrayList<>();
		}
		for (int c = 1; c <= C; c++) {
			for (int i : cat[0][c]) {
				for (int j : cat[1][c]) {
					edge[i].add(j);
				}
			}
		}
		for (int d = 1; d <= D; d++) {
			for (int i : dog[1][d]) {
				for (int j : dog[0][d]) {
					edge[i].add(j);
				}
			}
		}

		int sol = V;
		left = new int[V + 1];
		for (int c = 1; c <= C; c++) {
			for (int i : cat[0][c]) {
				check = new boolean[V + 1];
				if (bi_matching(i)) {
					sol--;
				}
			}
		}
		ret.append(sol).append('\n');
	}

	private static boolean bi_matching(int v) {
		for (int nv : edge[v]) {
			if (check[nv])
				continue;
			check[nv] = true;

			if (left[nv] == 0 || bi_matching(left[nv])) {
				left[nv] = v;
				return true;
			}
		}
		return false;
	}
}
#endif
}
