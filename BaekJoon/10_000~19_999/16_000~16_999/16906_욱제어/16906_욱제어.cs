using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 27
이름 : 배성훈
내용 : 욱제어
    문제번호 : 16906번

    트라이 문제다.
    길이가 짧은거부터 접두어가 안되게 만들어가면 된다.
    만드는게 불가능한 경우 불가능함을 그리디로 알 수 있다.

    그래서 길이로 정렬한 뒤 생성을 시도했다.
    이제 만드는 것은 트라이 아이디어를 이용했다.
    
*/

namespace BaekJoon.etc
{
    internal class etc_1647
    {

        static void Main1647(string[] args)
        {

            int n;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int MAX = 1_000;
                int[] cnt = new int[MAX + 1];
                for (int i = 0; i < n; i++)
                {

                    cnt[arr[i]]++;
                }

                int idx = 1;
                (int zero, int one, int prev, int len, bool block)[] node = new (int zero, int one, int prev, int len, bool block)[3 * MAX];
                int HEAD = 0;

                for (int i = 1; i <= MAX; i++)
                {

                    if (cnt[i] == 0) continue;
                    while (cnt[i]-- > 0)
                    {

                        if (DFS(i, HEAD)) continue;

                        sw.Write(-1);
                        return;
                    }
                }

                sw.Write("1\n");
                int len = 0;
                int[] stk = new int[MAX];


                for (int i = 0; i < n; i++)
                {

                    // 출력 시도
                    // 끝 길이를 
                    for (int j = 1; j < idx; j++)
                    {

                        if (node[j].len != arr[i]) continue;
                        node[j].len = 0;
                        len = 0;

                        int cur = j;
                        int prev = node[j].prev;
                        while (cur != HEAD)
                        {

                            if (cur == node[prev].zero) stk[len++] = 0;
                            else stk[len++] = 1;

                            cur = prev;
                            prev = node[cur].prev;
                        }

                        break;
                    }

                    while (len-- > 0)
                    {

                        sw.Write(stk[len]);
                    }

                    sw.Write('\n');
                }

                bool DFS(int _dep, int _cur, int _len = 0)
                {

                    if (_dep == 0)
                    {

                        // 만들어진 문자열의 끝인 경우
                        node[_cur].len = _len;
                        // 끝이라 표시
                        node[_cur].block = true;
                        return true;
                    }

                    bool ret = false;
                    if (node[_cur].zero == HEAD)
                    {

                        node[idx].prev = _cur;
                        node[_cur].zero = idx++;
                    }
                    
                    if (!node[node[_cur].zero].block)
                        // 해당 경우로 진입하는 경우면 두 자식이 아직 안막혔다는 증거다!
                        return DFS(_dep - 1, node[_cur].zero, _len + 1);

                    if (node[_cur].one == HEAD)
                    {

                        node[idx].prev = _cur;
                        node[_cur].one = idx++;
                    }

                    if (!node[node[_cur].one].block)
                    {

                        int next = node[_cur].one;
                        ret = DFS(_dep - 1, next, _len + 1);
                        // 두 자식이 막힌경우면 해당 노드를 막아 여기로 진입 못하게 한다.
                        if (node[next].block) node[_cur].block = true;
                    }

                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
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
    }
#if other
using System;

public class Program
{
    struct Pair
    {
        public int index, value;
        public Pair(int i, int v)
        {
            index = i; value = v;
        }
    }
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        Pair[] length = new Pair[n];
        string[] array = Console.ReadLine().Split(' ');
        for (int i = 0; i < n; i++)
        {
            length[i] = new(i, int.Parse(array[i]));
        }
        Array.Sort(length, (a, b) => a.value - b.value);
        Trie root = new();
        for (int i = 0; i < n; i++)
        {
            if (!root.Insert(length[i].value, 0, "", out string word))
            {
                Console.Write(-1);
                return;
            }
            array[length[i].index] = word;
        }
        Console.Write($"1\n{string.Join('\n', array)}");
    }
    class Trie
    {
        Trie[] child = new Trie[2];
        bool end;
        public bool Insert(int length, int depth, string result, out string word)
        {
            if (end)
            {
                word = "";
                return false;
            }
            if (depth >= length)
            {
                word = result;
                return end = true;
            }
            for (int i = 0; i < 2; i++)
            {
                if (child[i] == null)
                    child[i] = new();
            }
            if (child[0].Insert(length, depth + 1, result + '0', out word))
                return true;
            return child[1].Insert(length, depth + 1, result + '1', out word);
        }
    }
}
#elif other2
// #include <cstdio>
// #include <string>
// #include <algorithm>
using namespace std;

string res[1004];

struct Node {
	int val, idx;

	bool operator< (Node O) {
		return val < O.val;
	}
}node[1004];

struct Trie {
	Trie* go[2];
	bool f;

	Trie() {
		go[0] = go[1] = NULL;
		f = false;
	}

	~Trie() {
		if (go[0]) delete go[0];
		if (go[1]) delete go[1];
	}

	bool insert(int cnt, int idx) {
		if (f) return false;

		if (cnt == 0) {
			f = true;
			return true;
		}

		for (int n = 0; n < 2; n++) {
			if (!go[n]) go[n] = new Trie();
			res[idx] += n + '0';

			if (go[n]->insert(cnt - 1, idx)) return true;
			else res[idx].pop_back();
		}

		return false;
	}
};

int main()
{
	//freopen("input.txt", "r", stdin);
	Trie* trie = new Trie();
	int N, t;
	scanf("%d", &N);

	for (int i = 0; i < N; i++) {
		scanf("%d", &t);
		node[i] = { t, i };
	}

	sort(node, node + N);

	bool flag = true;
	for (int i = 0; i < N; i++) {
		if (!flag) continue;
		if (!trie->insert(node[i].val, node[i].idx)) flag = false;
	}

	if (!flag) printf("-1\n");
	else {
		printf("1\n");
		for (int i = 0; i < N; i++) {
			for (int k = 0; res[i][k]; k++)
				printf("%c", res[i][k]);
			printf("\n");
		}
	}

	delete trie;
}
#endif
}
