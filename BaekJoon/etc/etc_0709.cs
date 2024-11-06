using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 20
이름 : 배성훈
내용 : 검색 엔진
    문제번호 : 1108번

    강한 연결 요소 문제다
    처음에는 같은 SCC에 있으면 점수가 같아지는 줄 알았다
    즉 간접적으로 갈 수 있는 길로도 모두 점수를 계산해서 한 번 틀렸다

    질문 게시판을 보니 나와 비슷하게 틀린 사람이 있었다
    알고보니 직접적으로 이어진 노드의 경우만 확인해야한다
    직접적으로 이어진 노드의 이동 경로 중에 자신으로 오는게 있다면 점수 계산을 하면 안된다

    아래는 예제에서 다른 결과를 나타낸다
        4
        A 2 B C
        C 1 D
        D 1 A
        E 2 C D
        C

        ans : 1     -> 문제에서 요구하는 정답
        wrong : 2   -> SCC에 동일한 점수를 부여했을 때, 도출하는 점수

    이후 이를 수정하니 76ms에 이상없이 통과했다

    아이디어는 다음과 같다
    먼저 SCC 로 노드의 그룹을 묶는다
    그리고 DFS 탐색으로 점수를 계산한다
    여기서 같은 그룹이면 점수 누적을 하지 않고, 
    다른 그룹인 경우만 점수를 누적한다
    SCC는 코사라주 알고리즘으로 구했다
*/

namespace BaekJoon.etc
{
    internal class etc_0709
    {

        static void Main709(string[] args)
        {

            StreamReader sr;

            Dictionary<string, int> sTi;

            List<List<int>> line;
            List<List<int>> revLine;

            long[] score;
            bool[] visit;

            int len;
            int gLen;

            int n;
            int find;

            int[] group;
            int[] cnt;

            Stack<int> s;

            long ret;

            Solve();

            void Solve()
            {

                Input();

                SCC();

                GetRet();

                Console.WriteLine(ret);
            }

            void GetRet()
            {

                score = new long[len];
                cnt = new int[gLen];

                for (int i = 0; i < len; i++)
                {

                    int f = group[i];
                    cnt[f]++;
                    score[i] = 1;

                }

                ret = DFS3(find);
            }

            long DFS3(int _n)
            {

                if (visit[_n]) return score[_n];
                visit[_n] = true;

                long ret = 1;

                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];
                    if (group[next] == group[_n]) continue;

                    ret += DFS3(next);
                }

                score[_n] = ret;
                return ret;
            }

            void SCC()
            {

                visit = new bool[len];
                s = new(len);

                for (int i = 0; i < len; i++)
                {

                    if (visit[i]) continue;
                    DFS1(i);
                }

                gLen = 0;
                group = new int[len];
                while(s.Count > 0)
                {

                    int node = s.Pop();
                    if (!visit[node]) continue;

                    DFS2(node);
                    gLen++;
                }
            }

            void DFS1(int _n)
            {

                visit[_n] = true;

                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];
                    if (visit[next]) continue;

                    DFS1(next);
                }

                s.Push(_n);
            }

            void DFS2(int _n)
            {

                visit[_n] = false;
                group[_n] = gLen;

                for (int i = 0; i < revLine[_n].Count; i++)
                {

                    int next = revLine[_n][i];

                    if (!visit[next]) continue;
                    DFS2(next);
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());

                sTi = new(n * 25);
                line = new(n * 25);
                revLine = new(n * 25);

                int idx = 0;
                for (int i = 0; i < n; i++)
                {

                    string[] temp = sr.ReadLine().Split();
                    if (!sTi.ContainsKey(temp[0])) 
                    { 
                        
                        line.Add(new List<int>());
                        revLine.Add(new List<int>());
                        sTi[temp[0]] = idx++;
                    }

                    int f = sTi[temp[0]];
                    for (int j = 2; j < temp.Length; j++)
                    {

                        if (!sTi.ContainsKey(temp[j])) 
                        {

                            line.Add(new List<int>());
                            revLine.Add(new List<int>());
                            sTi[temp[j]] = idx++; 
                        }

                        int b = sTi[temp[j]];
                        line[f].Add(b);
                        revLine[b].Add(f);
                    }
                }

                find = sTi[sr.ReadLine()];
                len = idx;
                sr.Close();
            }
        }
    }

#if other
// #define _CRT_SECURE_NO_WARNINGS

// #include<stdio.h>

// #define MAX_NAME_LEN 52
// #define MAX_NODE_SIZE 2502
// #define MAX_TABLE_SIZE 5011

struct NODE {
	int idx;
	char name[MAX_NAME_LEN];
	struct NODE* nextPtr;
}typedef node;

struct EDGE {
	int st, ed;
}typedef edge;

int cnt[MAX_NODE_SIZE], dfsn[MAX_NODE_SIZE], seq, main_stack[MAX_NODE_SIZE], main_stack_cursor;
int group_num, groups[MAX_NODE_SIZE], edge_index, scc_edge_index, node_list_idx;
int scc_indegress[MAX_NODE_SIZE], scc_cnt[MAX_NODE_SIZE], node_topological_list[MAX_NODE_SIZE], new_node_idx;
bool finished[MAX_NODE_SIZE];
long long ans[MAX_NODE_SIZE];
edge edges[MAX_NODE_SIZE];
node nodes[MAX_NODE_SIZE];
node* table[MAX_TABLE_SIZE];
edge scc_edges[MAX_NODE_SIZE];

bool compare_name(const char* a, const char* b);
node* gen_node(const char* name);
int DFS(int vertex);
int link_to_table(const char* name);
int make_hash(const char* name, int mul, int mod);
void quickSort(int start, int end, edge* arr);
void SCC_topological_sort();
void topological_sort_convert_to_node(int* scc_q, int scc_input);
void solve();

int main(void) {
	int n, i, m, dest_val, depart_val;
	char ans_dest[MAX_NAME_LEN] = { 0 };
	scanf("%d", &n);

	node_list_idx = scc_edge_index = edge_index = -1;

	while (n--) {
		char dest[MAX_NAME_LEN] = { 0 };
		scanf("%s", &dest);
		dest_val = link_to_table(dest);
		ans[dest_val] = 1;

		scanf("%d", &m);
		while (m--) {
			char depart[MAX_NAME_LEN] = { 0 };
			scanf("%s", &depart);
			depart_val = link_to_table(depart);
			ans[depart_val] = 1;

			edges[++edge_index].st = depart_val;
			edges[edge_index].ed = dest_val;
			cnt[depart_val]++;
		}
	}

	for (i = 0; i < MAX_NODE_SIZE; i++)
		cnt[i] += cnt[i - 1];
	quickSort(0, edge_index, edges);

	for (i = 1; i <= new_node_idx; i++)
		if (dfsn[i] == 0)
			DFS(i);

	SCC_topological_sort();

	solve();

	scanf("%s", &ans_dest);
	printf("%lld\n", ans[link_to_table(ans_dest)]);

	return 0;
}

void solve() {
	int i, z, curr, new_vertex;

	for (z = 0; z <= node_list_idx; z++) {
		curr = node_topological_list[z];

		for (i = cnt[curr - 1]; i < cnt[curr]; i++) {
			new_vertex = edges[i].ed;

			if (groups[curr] == groups[new_vertex]);
			//ans[new_vertex] = ans[curr];
			else ans[new_vertex] += ans[curr];
		}
	}
}

void topological_sort_convert_to_node(int* scc_q, int scc_input) {
	int i, j, num;

	for (i = 0; i <= scc_input; i++) {
		num = scc_q[i];
		for (j = 1; j <= new_node_idx; j++)
			if (groups[j] == num)
				node_topological_list[++node_list_idx] = j;
	}
}

void SCC_topological_sort() {
	int i, st, ed, input = -1, output = -1, current_vertex, new_vertex;
	int q[MAX_NODE_SIZE];

	for (i = 0; i <= edge_index; i++) {
		st = groups[edges[i].st];
		ed = groups[edges[i].ed];

		if (st == ed)
			continue;

		scc_edges[++scc_edge_index].st = st;
		scc_edges[scc_edge_index].ed = ed;
		scc_cnt[st]++;
		scc_indegress[ed]++;
	}

	for (i = 1; i <= group_num; i++) {
		scc_cnt[i] += scc_cnt[i - 1];
		if (scc_indegress[i] == 0)
			q[++input] = i;
	}
	quickSort(0, scc_edge_index, scc_edges);

	while (input != output) {
		current_vertex = q[++output];

		for (i = scc_cnt[current_vertex - 1]; i < scc_cnt[current_vertex]; i++) {
			new_vertex = scc_edges[i].ed;

			if (--scc_indegress[new_vertex] == 0)
				q[++input] = new_vertex;
		}
	}

	topological_sort_convert_to_node(q, input);
}

int link_to_table(const char* name) {
	int hash_val = make_hash(name, 17, MAX_TABLE_SIZE);

	node* cursor = table[hash_val];

	while (cursor != nullptr) {
		if (compare_name(cursor->name, name))
			return cursor->idx;

		cursor = cursor->nextPtr;
	}

	node* new_node = gen_node(name);
	new_node->nextPtr = table[hash_val];
	table[hash_val] = new_node;
	return new_node->idx;
}

node* gen_node(const char* name) {
	node* result = &(nodes[new_node_idx++]);

	result->idx = new_node_idx;
	result->nextPtr = nullptr;
	for (int i = 0; name[i]; i++)
		result->name[i] = name[i];

	return result;
}

bool compare_name(const char* a, const char* b) {
	for (int i = 0; i < MAX_NAME_LEN; i++)
		if (a[i] != b[i])
			return false;

	return true;
}

int DFS(int vertex) {
	dfsn[vertex] = ++seq;
	main_stack[main_stack_cursor++] = vertex;

	int i, new_vertex, result = dfsn[vertex], temp;
	for (i = cnt[vertex - 1]; i < cnt[vertex]; i++) {
		new_vertex = edges[i].ed;
		if (dfsn[new_vertex] == 0) {
			temp = DFS(new_vertex);
			if (result > temp)
				result = temp;
		}
		else if (!finished[new_vertex]) {
			if (result > dfsn[new_vertex])
				result = dfsn[new_vertex];
		}
	}

	if (result == dfsn[vertex]) {
		group_num++;
		while (1) {
			temp = main_stack[--main_stack_cursor];
			finished[temp] = true;
			groups[temp] = group_num;

			if (temp == vertex)
				break;
		}
	}

	return result;
}

int make_hash(const char* name, int mul, int mod) {
	int i, result = 0;

	for (i = 0; name[i]; i++) {
		result = (result + name[i] * mul) % mod;
		result *= mul;
	}

	return result % mod;
}

void quickSort(int start, int end, edge* arr) {
	if (start < end) {
		int left = start - 1, right = end + 1, pivot = arr[(start + end) / 2].st;
		edge temp;
		while (1) {
			while (arr[++left].st < pivot);
			while (arr[--right].st > pivot);
			if (left >= right)
				break;
			temp = arr[left], arr[left] = arr[right], arr[right] = temp;
		}
		quickSort(start, left - 1, arr);
		quickSort(right + 1, end, arr);
	}
}
#elif other2
// #include <map>
// #include <cstdio>
// #include <string>
// #include <vector>
// #include <algorithm>
using namespace std;

int n, t, cnt;
char tmp[2506];
map<string, int> hs;
vector<int> adj[2506], rev_adj[2506];
long long score[2506];

bool visited[2506];
vector<int> st;
int scc[2506], cnt_scc;

void dfs_scc(int x) {
    visited[x] = true;
    for (auto &i: adj[x]) if (!visited[i]) dfs_scc(i);
    st.push_back(x);
}

void dfs_scc(int x, int v) {
    visited[x] = true;
    for (auto &i: rev_adj[x]) if (!visited[i]) dfs_scc(i, v);
    scc[x] = v;
}

int main() {
    scanf("%d", &n);
    for (int i = 0; i < n; i++) {
        scanf("%s%d", tmp, &t);
        if (!hs[tmp]) hs[tmp] = ++cnt;
        int x = hs[tmp];
        for (int j = 0; j < t; j++) {
            scanf("%s", tmp);
            if (!hs[tmp]) hs[tmp] = ++cnt;
            int y = hs[tmp];
            rev_adj[x - 1].push_back(y - 1);
            adj[y - 1].push_back(x - 1);
        }
    }
    fill(visited, visited + cnt, false);
    for (int i = 0; i < cnt; i++) if (!visited[i]) dfs_scc(i);
    fill(visited, visited + cnt, false);
    for (int i = (int)st.size() - 1; i >= 0; i--) if (!visited[st[i]]) dfs_scc(st[i], cnt_scc++);
    for (int i = 0; i < cnt; i++) score[i] = 1;
    for (int i = (int)st.size() - 1; i >= 0; i--) for (auto &j: rev_adj[st[i]]) if (scc[st[i]] != scc[j]) score[st[i]] += score[j];

    scanf("%s", tmp);
    if (!hs[tmp]) return puts("0"), 0;
    int q = hs[tmp] - 1;
    printf("%lld", score[q]);
}

#elif other3
import java.io.*;
import java.util.*;

public class Main {

    static int N;
    static int nodeId, nodeCount;

    static int[] nodeIds, groupIds;
    static boolean[] finish;

    static List<List<Integer>> graph, sccGraph, sccList;
    static ArrayDeque<Integer> stack;
    static HashMap<String, Integer> indexMap;

    public static void init(BufferedReader br) throws IOException {
        StringTokenizer st;

        N = Integer.parseInt(br.readLine());
        graph = new ArrayList<>();
        sccGraph = new ArrayList<>();
        sccList = new ArrayList<>();
        stack = new ArrayDeque<>();
        indexMap = new HashMap<>();

        for (int i = 0; i < N; i++) {
            st = new StringTokenizer(br.readLine());
            String dest = st.nextToken();
            if (!indexMap.containsKey(dest)) {
                indexMap.put(dest, nodeCount++);
                graph.add(new ArrayList<>());
            }

            int destIdx = indexMap.get(dest);
            int size = Integer.parseInt(st.nextToken());
            for (int j = 0; j < size; j++) {
                String src = st.nextToken();
                if (!indexMap.containsKey(src)) {
                    indexMap.put(src, nodeCount++);
                    graph.add(new ArrayList<>());
                }

                int srcIdx = indexMap.get(src);
                graph.get(srcIdx).add(destIdx);
            }
        }

        nodeIds = new int[nodeCount];
        groupIds = new int[nodeCount];
        finish = new boolean[nodeCount];
        Arrays.fill(nodeIds, -1);
        Arrays.fill(groupIds, -1);
    }

    public static void getScc() {
        for (int i = 0; i < nodeCount; i++) {
            if (nodeIds[i] == -1) {
                dfs(i);
            }
        }
    }

    public static int dfs(int currIdx) {
        nodeIds[currIdx] = nodeId++;
        stack.addLast(currIdx);

        int parentId = nodeIds[currIdx];
        for (int adjIdx : graph.get(currIdx)) {
            if (nodeIds[adjIdx] == -1) {
                parentId = Math.min(parentId, dfs(adjIdx));
            } else if (!finish[adjIdx]) {
                parentId = Math.min(parentId, nodeIds[adjIdx]);
            }
        }

        if (parentId == nodeIds[currIdx]) {
            int nodeIdx = -1;
            List<Integer> scc = new ArrayList<>();
            while (nodeIdx != currIdx) {
                nodeIdx = stack.removeLast();
                groupIds[nodeIdx] = sccList.size();
                finish[nodeIdx] = true;
                scc.add(nodeIdx);
            }
            sccList.add(scc);
        }

        return parentId;
    }

    public static int[] initSccGraph() {
        int[] inDegrees = new int[sccList.size()];
        for (int i = 0; i < sccList.size(); i++) {
            sccGraph.add(new ArrayList<>());
        }

        for (int i = 0; i < nodeCount; i++) {
            for (int adjIdx : graph.get(i)) {
                if (groupIds[i] != groupIds[adjIdx]) {
                    inDegrees[groupIds[adjIdx]]++;
                    sccGraph.get(groupIds[i]).add(groupIds[adjIdx]);
                }
            }
        }
        return inDegrees;
    }

    public static long getScore(int[] inDegrees, String target) {
        int targetIdx = indexMap.get(target);
        long[] scores = new long[nodeCount];
        Arrays.fill(scores, 1);

        Queue<Integer> queue = new LinkedList<>();
        for (int i = 0; i < sccList.size(); i++) {
            if (inDegrees[i] == 0) {
                queue.add(i);
            }
        }

        while (!queue.isEmpty()) {
            int curr = queue.poll();
            for (int member : sccList.get(curr)) {
                for (int adj : graph.get(member)) {
                    if (groupIds[member] != groupIds[adj]) {
                        scores[adj] += scores[member];
                    }
                }
            }

            for (int adj : sccGraph.get(curr)) {
                if (--inDegrees[adj] == 0) {
                    queue.add(adj);
                }
            }
        }

        return scores[targetIdx];
    }

    public static void main(String[] args) throws Exception {
        // Input & Output stream
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(System.out));
        StringTokenizer st = null;

        init(br);
        getScc();

        String target = br.readLine();
        long score = getScore(initSccGraph(), target);
        bw.write(score + "");

        // close the buffer
        br.close();
        bw.close();
    }
}
#endif
}
