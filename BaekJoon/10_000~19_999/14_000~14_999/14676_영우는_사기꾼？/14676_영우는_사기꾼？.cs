using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 3
이름 : 배성훈
내용 : 영우는 사기꾼?
    문제번호 : 14676번

    구현, 방향 비순환 그래프, 그래프 이론 문제다
    건설 조건 부분을 간과해 여러 번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_1020
    {

        static void Main1020(string[] args)
        {

            string YES = "King-God-Emperor";
            string NO = "Lier!";

            StreamReader sr;
            List<int>[] edge;
            int n, m, k;
            int[] degree;
            int[] build;
            bool ret;

            Solve();
            void Solve()
            {

                Input();

                GetRet();

                Console.Write(ret ? YES : NO);
            }

            void GetRet()
            {

                ret = true;

                for (int i = 0; i < k; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    if (f == 1)
                    {

                        if (degree[b] > 0)
                        {

                            ret = false;
                            break;
                        }

                        build[b]++;
                        // 이미 지어진 경우면 패스
                        if (build[b] > 1) continue;

                        // 이전에 지어지지 않았다면
                        // 테크트리 부분 수정
                        for (int j = 0; j < edge[b].Count; j++)
                        {

                            int next = edge[b][j];
                            degree[next]--;
                        }
                    }
                    else
                    {

                        if (build[b] == 0)
                        {

                            ret = false;
                            break;
                        }

                        build[b]--;
                        // 파괴해도 해당건물이 남아있으면 패스
                        if (build[b] > 0) continue;

                        // 해당 건물이 없으면
                        // 테크트리 부분도 수정
                        for (int j = 0; j < edge[b].Count; j++)
                        {

                            int next = edge[b][j];
                            degree[next]++;
                        }
                    }
                }

                sr.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
                k = ReadInt();

                edge = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                degree = new int[n + 1];
                build = new int[n + 1];
                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    edge[f].Add(b);
                    degree[b]++;
                }
            }

            int ReadInt()
            {

                int ret = 0;
                while (TryReadInt()) { }

                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n' || c == -1) return true;
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
// #include<bits/stdc++.h>

using namespace std;

// #define INF 2000000000

typedef long long ll;
typedef pair<int, int> pii;

class Factory {
	int cnt_need = 0;
	int cnt_create = 0;
	int check_need = 0;
	int node_idx = 0;
	Factory* effect_node[3] = { nullptr };

public:
	void add_check_need() {
		check_need++;
	}
	void add_need() {
		cnt_need++;
	}
	void del_need() {
		cnt_need--;
	}

	void add_node(Factory* node) {
		effect_node[node_idx++] = node;
		node->add_check_need();
	}

	bool create() {
		if (cnt_need == check_need) {
			if (cnt_create++ == 0) {
				for (int i = 0; i < node_idx; i++) {
					effect_node[i]->add_need();
				}
			}
			return true;
		}
		else
			return false;
	}

	bool destroy() {
		if (cnt_create > 0) {
			if (--cnt_create == 0) {
				for (int i = 0; i < node_idx; i++) {
					effect_node[i]->del_need();
				}
			}
			
			return true;
		}
		else
			return false;
	}
};

Factory v[100001];

int main(void) {
	ios::sync_with_stdio(false);
	cin.tie(NULL);
	cout.tie(NULL);

	int n, m, k; cin >> n >> m >> k;

	for (int i = 0; i < m; i++) {
		int x, y; cin >> x >> y;
		v[x].add_node(&v[y]);
	}

	bool flag = true;
	while (k--) {
		int cmd, x; cin >> cmd >> x;
		if (!flag) continue;
		if (cmd == 1) {
			flag = v[x].create();
		}
		else {
			flag = v[x].destroy();
		}
	}

	if (flag) {
		cout << "King-God-Emperor";
	}
	else {
		cout << "Lier!";
	}
}
#endif
}
