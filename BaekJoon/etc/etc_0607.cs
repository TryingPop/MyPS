using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 24
이름 : 배성훈
내용 : 슬슬 가지를 먹지 않으면 죽는다
    문제번호 : 27945번

    그리디, 분리집합, 최소스패닝 트리 문제다
    아이디어는 다음과 같다

    최소 신장트리로 만들었을 때, 간선이 1, 2, 3, ..., d - 1인 경우 생존일 d가 보장된다
    그래서 살리는 간선 순위를, t가 낮은 순으로 노드들을 잇는데 쓴다
    t를 기준으로 정렬은 우선순위 큐를 써서 했다(위상정렬 - 크루스칼 알고리즘)

    이미 이어진 노드인 경우 해당 간선은 잇는데 사용하지 않는다
    이미 이어진 것을 확인하는 방법은 유니온 파인드 알고리즘을 썼다

    여기서 t는 간선마다 모두 다름이 보장되므로 t순서대로 이으면서 생존 날짜와 비교하며
    최대 생존날짜를 찾았다
    이는 그디리하게 현재 날짜와 같은 t인 경우 생존일 1을 늘리고 아니면 t가 같은 경우는 없으므로 종료시켰다

    이렇게 제출하니 144ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0607
    {

        static void Main607(string[] args)
        {

            StreamReader sr;

            PriorityQueue<(int f, int b, int t), int> q;
            int n, m;
            int[] group;

            Stack<int> s;
            Solve();

            void Solve()
            {

                Input();

                s = new(100);
                int ret = 1;

                while(q.Count > 0)
                {

                    var node = q.Dequeue();

                    if (ret != node.t) break;

                    int f = Find(node.f);
                    int b = Find(node.b);

                    if (f == b) continue;
                    Union(f, b);
                    ret++;
                }

                Console.WriteLine(ret);
            }

            void Union(int _f, int _b)
            {

                if (_b < _f)
                {

                    int temp = _f;
                    _f = _b;
                    _b = temp;
                }

                group[_b] = _f;
            }

            int Find(int _chk)
            {

                while(_chk != group[_chk])
                {

                    s.Push(_chk);
                    _chk = group[_chk];
                }

                while(s.Count > 0)
                {

                    group[s.Pop()] = _chk;
                }

                return _chk;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 8);
                n = ReadInt();
                m = ReadInt();

                group = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    group[i] = i;
                }

                q = new(m);
                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();
                    int t = ReadInt();

                    q.Enqueue((f, b, t), t);
                }

                sr.Close();
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
import java.io.*;
import java.util.*;
public class Main {
	static int[] root;
	public static void main(String[] args) throws Exception{
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		StringTokenizer st = new StringTokenizer(br.readLine());
		int n = Integer.parseInt(st.nextToken());
		int m = Integer.parseInt(st.nextToken());
		int[][] route = new int[n+1][2];
		
		int s, e, a, cnt=0;
		boolean flag = false;
		for (int i=0; i<m; i++) {
			st = new StringTokenizer(br.readLine());
			if (flag) continue;
			s = Integer.parseInt(st.nextToken());
			e = Integer.parseInt(st.nextToken());
			a = Integer.parseInt(st.nextToken());
			if (a>n) continue;
			route[a][0]=s; route[a][1]=e;
			cnt++;
			if (cnt==n) flag=true;
		}
		init(n);
		
		int x, y;
		for (int i=1; i<=n; i++) {
			x = route[i][0]; y = route[i][1];
			x = find(x); y = find(y);
			if (x==y) {System.out.println(i);break;}
			root[x] = y;
		}
	}

	static void init(int n) {
		root = new int[n+1];
		for (int i=1; i<=n; i++) root[i] = i;
	}
	static int find(int x) {
		return root[x]==x ? x:(root[x]=find(root[x]));
	}
}
#endif
}
