using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 28
이름 : 배성훈
내용 : 돌연변이
    문제번호 : 10256번

    문자열, 트라이, 아호 코라식 문제다
    돌연변이들로 변경시켜 문자열들을 저장해야한다

    통과 시간 1.8초에 150MB나 써서 시간이 많이 걸린다
    
    앞에서 문자열 찾는 것과 초괴화를 자료구조밖에서 찾았는데,
    여기서는 트라이 자료구조 안에 메서드를 넣는게 더 분석하기 쉽다 판단해 
    트라이 자료구조안에서 초기화와 글자 찾기 메서드를 넣었다
*/

namespace BaekJoon._56
{
    internal class _56_10
    {

        static void Main10(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            Trie root;
            int n, m;
            string dna;
            string marker;
            char[] change;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                root = new();

                change = new char[100];
                int test = ReadInt();

                while(test-- > 0)
                {

                    Input();

                    ChangeMarker();

                    int ret = root.Find(dna);

                    sw.Write($"{ret}\n");
                    root.Clear();
                }

                sr.Close();
                sw.Close();
            }

            void Input()
            {

                ///
                /// 입력 받기
                ///
                n = ReadInt();
                m = ReadInt();

                dna = sr.ReadLine();
                marker = sr.ReadLine();
            }

            void ChangeMarker()
            {

                ///
                /// 기존 마커에서 변경된 마커들 찾고 트라이에 넣는다
                /// 
                for (int i = 0; i < m; i++)
                {

                    change[i] = marker[i];
                }

                root.Insert(change, m);

                for (int l = 0; l < m; l++)
                {

                    if (l > 0) change[l - 1] = marker[l - 1];

                    for (int r = l + 1; r < m; r++)
                    {

                        
                        for (int i = l, j = r; i <= r; i++, j--)
                        {

                            change[i] = marker[j];
                        }

                        for (int i = r + 1; i < m; i++)
                        {

                            change[i] = marker[i];
                        }

                        root.Insert(change, m);
                    }
                }

                ///
                /// 마커들 넣고 트라이 초기화?
                ///
                root.SetTrie();
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

        class Trie
        {

            private Trie[] go;
            private Trie fail;
            private bool isEnd;

            private static Dictionary<char, int> cTi = new(4) 
                            { { 'A', 0 }, { 'C', 1 }, { 'G', 2 }, { 'T', 3 } };

            private static Queue<Trie> q = new();

            public Trie()
            {

                go = new Trie[4];
                isEnd = false;
                fail = null;
            }

            public Trie this[int _idx]
            {

                set { go[_idx] = value; }
                get { return go[_idx]; }
            }

            public void Insert(char[] _str, int _len)
            {

                Trie next = this;
                for (int i = 0; i < _len; i++)
                {

                    int cur = cTi[_str[i]];
                    if (next[cur] == null) next[cur] = new();
                    next = next[cur];
                }

                next.isEnd = true;
            }

            public void SetTrie()
            {

                this.fail = this;
                q.Enqueue(this);

                while(q.Count > 0)
                {

                    Trie node = q.Dequeue();

                    for (int i = 0; i < 4; i++)
                    {

                        Trie next = node[i];
                        if (next == null) continue;

                        if (node == this) next.fail = this;
                        else
                        {

                            Trie fail = node.fail;
                            while(fail != this && fail[i] == null)
                            {

                                fail = fail.fail;
                            }

                            if (fail[i] != null) fail = fail[i];
                            next.fail = fail;
                        }

                        if (next.fail.isEnd) next.isEnd = true;
                        q.Enqueue(next);
                    }
                }
            }

            public int Find(string _str)
            {

                int ret = 0;

                Trie current = this;

                for (int i = 0; i < _str.Length; i++)
                {

                    int next = cTi[_str[i]];

                    while(current != this && current[next] == null)
                    {

                        current = current.fail;
                    }

                    if (current[next] != null) current = current[next];

                    if (current.isEnd) ret++;
                }

                return ret;
            }

            public void Clear()
            {

                for (int i = 0; i < 4; i++)
                {

                    go[i] = null;
                }
            }
        }
    }
#if other
// #include <bits/stdc++.h>
// #define endl '\n'
using namespace std;
const int PRECISION = 0;
using ll = long long;
using pi2 = pair<int, int>;
// #define fr first
// #define sc second
const int dy[4] = {-1, 0, +1, 0}, dx[4] = {0, +1, 0, -1};

inline int cvt(char c){
    if (c == 'A'){ return 0; } if (c == 'C'){ return 1; }
    if (c == 'G'){ return 2; } if (c == 'T'){ return 3; }
}
struct Node{ int jmp=0; int nxt[4]={0,0,0,0}; bool chk=0; };
vector<Node> trie;
void psh(int ni, const string& s, int si){ int sl = s.size();
    if (si == sl){ trie[ni].chk = 1; return; }
    int nxi = cvt(s[si]); if (trie[ni].nxt[nxi] == 0){
        trie[ni].nxt[nxi] = trie.size();
        trie.emplace_back();
    } return psh(trie[ni].nxt[nxi], s, si+1);
}
inline void psh(const string& s){ return psh(0, s, 0); }

void Main(){
    int t; cin >> t; while (t--){
        int sl, tl; string s, t; cin >> sl >> tl >> s >> t;
        trie.clear(); trie.emplace_back();
        psh(t);
        for (int i = 0; i < tl; i++){
            for (int j = i+1; j < tl; j++){
                reverse(t.begin()+i, t.begin()+j+1);
                psh(t);
                reverse(t.begin()+i, t.begin()+j+1);
            }
        }
        queue<int> q; q.push(0);
        while (!q.empty()){
            int now = q.front(); q.pop();
            for (int nxi : {0, 1, 2, 3}){
                int nxt = trie[now].nxt[nxi]; if (nxt == 0){ continue; }
                if (now == 0){ trie[nxt].jmp = now; }
                else{
                    int ptr = trie[now].jmp; while (ptr != 0){
                        if (trie[ptr].nxt[nxi] != 0){ break; }
                        else{ ptr = trie[ptr].jmp; }
                    }
                    if (trie[ptr].nxt[nxi] != 0){ ptr = trie[ptr].nxt[nxi]; }
                    trie[nxt].jmp = ptr;
                }
                q.push(nxt);
            }
        }
        int ans = 0;
        int ptr = 0; for (char c : s){
            int nxi = cvt(c);
            while (ptr != 0){
                if (trie[ptr].nxt[nxi] != 0){ break; }
                else{ ptr = trie[ptr].jmp; }
            }
            if (trie[ptr].nxt[nxi] != 0){ ptr = trie[ptr].nxt[nxi]; }
            if (trie[ptr].chk){ ans += 1; }
        } cout << ans << endl;
    }
}

int main(){
    ios_base::sync_with_stdio(0); cin.tie(0); cout.tie(0);
    cout.setf(ios::fixed); cout.precision(PRECISION); Main();
}
#elif other2
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.ArrayDeque;
import java.util.Queue;
import java.util.StringTokenizer;

public class Main {
	public static void main(String[] args) throws Exception {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		StringBuilder sb = new StringBuilder();

		Trie trie = new Trie();

		int TC = Integer.parseInt(br.readLine());
		for (int tc = 1; tc <= TC; tc++) {
			StringTokenizer st = new StringTokenizer(br.readLine(), " ");
			int N = Integer.parseInt(st.nextToken());
			int M = Integer.parseInt(st.nextToken());

			String line = br.readLine();
			char[] marker = br.readLine().toCharArray();

			trie.clear();
			trie.insert(marker);
			for (int i = 0; i < M; i++) {
				for (int j = i + 1; j < M; j++) {
					for (int left = i, right = j; left < right; left++, right--) {
						char temp = marker[left];
						marker[left] = marker[right];
						marker[right] = temp;
					}
					trie.insert(marker);
					for (int left = i, right = j; left < right; left++, right--) {
						char temp = marker[left];
						marker[left] = marker[right];
						marker[right] = temp;
					}
				}
			}
			trie.initFail();

			int ans = trie.parse(line);
			sb.append(ans).append("\n");
		}

		System.out.println(sb.toString());
	}

	private static class Trie {
		private Node origin, root;

		private int[] map;

		public Trie() {
			root = new Node();

			map = new int[256];
			map['A'] = 0;
			map['G'] = 1;
			map['T'] = 2;
			map['C'] = 3;
		}

		public void clear() {
			root = new Node();
		}

		public void insert(char[] arr) {
			Node curr = root;
			Node next = null;
			for (int i = 0, lim = arr.length; i < lim; i++) {
				int idx = map[arr[i]];
				next = curr.go[idx];
				if (next == null) {
					next = new Node();
					curr.go[idx] = next;
				}
				curr = next;
			}
			curr.key = 1; // no need to assign unique or multiple keys
		}

		public void initFail() {
			origin = new Node();
			for (int idx = 0; idx < 4; idx++) {
				origin.go[idx] = root;
			}
			root.fail = origin;

			Queue<Node> queue = new ArrayDeque<Node>();
			queue.offer(root);
			while (!queue.isEmpty()) {
				Node curr = queue.poll();
				for (int idx = 0; idx < 4; idx++) {
					Node next = curr.go[idx];
					if (next == null) continue;

					Node dest = curr.fail;
					while (dest != origin && dest.go[idx] == null) {
						dest = dest.fail;
					}
					dest = dest.go[idx];

					next.fail = dest;
					// no need to update key since all sequences have same length

					queue.offer(next);
				}
			}
		}

		public int parse(String line) {
			int cnt = 0;

			Node curr = root;
			for (int i = 0, lim = line.length(); i < lim; i++) {
				int idx = map[line.charAt(i)];
				while (curr != origin && curr.go[idx] == null) {
					curr = curr.fail;
				}
				curr = curr.go[idx];
				if (curr.key > 0) cnt++;
			}

			return cnt;
		}
	}

	private static class Node {
		public int key;
		public Node fail;
		public Node[] go;

		public Node() {
			this.key = 0;
			this.fail = null;
			this.go = new Node[4];
		}
	}
}
#elif other3
// # 백준
// # 10256
// # 돌연변이
// # pypy3 언어


// # 함수를 정의하여 마커와 돌연변이를 생성하는 부분

def generate_mutations(marker):
    mutations = set()  # 중복된 돌연변이를 방지하기 위해 세트(set)를 사용합니다.
    n = len(marker)
    
// # 두 번째 부분을 뒤집는 모든 경우의 수를 생성
    for i in range(n + 1):
        for j in range(n + 1):
// # 마커의 일부를 뒤집어 돌연변이를 생성합니다.
            mutated_marker = marker[:i] + marker[i:j][::-1] + marker[j:]
            mutations.add(mutated_marker)  # 생성된 돌연변이를 세트에 추가합니다.
    
    return mutations

// # 주어진 DNA 구조에서 마커와 돌연변이가 몇 번 출현하는지 세는 함수

def count_mutations(dna, marker):
    mutations = generate_mutations(marker)  # 모든 돌연변이를 생성합니다.
    count = 0  # 출현 횟수를 저장할 변수를 초기화합니다.
    
    for i in range(len(dna) - len(marker) + 1):
        substring = dna[i:i + len(marker)]  # DNA 문자열에서 마커와 같은 길이의 부분 문자열을 추출합니다.
        if substring in mutations:  # 추출한 부분 문자열이 돌연변이 중에 있다면,
            count += 1  # 출현 횟수를 증가시킵니다.
    
    return count

// # 테스트 케이스 수 입력
T = int(input())

for _ in range(T):
// # DNA 구조와 마커 입력
    n, m = map(int, input().split())  # DNA 길이와 마커 길이를 입력받습니다.
    dna = input()  # DNA 문자열을 입력받습니다.
    marker = input()  # 마커를 입력받습니다.
    
// # 결과 출력
    result = count_mutations(dna, marker)  # DNA에서 마커와 돌연변이가 몇 번 출현하는지 계산합니다.
    print(result)  # 결과를 출력합니다.

#endif
}
