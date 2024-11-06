using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 2
이름 : 배성훈
내용 : 울타리
    문제번호 : 1047번

    그리디, 브루트포스 문제다
    나무들로 만들 수 있는 모든 직사각형들을 조사했다
    그러니 N^4이되고, 

    여기서 길이가 만들어지는지 확인한다 여기서 나무들 확인한다고 
    N의 시간이 더 걸린다
    길이가 부족하면 추가적으로 나무를 베어 길이를 충당한다

    그래서 가장 적게 벤 나무 경우를 찾는다
    시간 복잡도는 O(N^5)이다
    N이 40이라 계산기를 돌리면 1억 240만이다
*/

namespace BaekJoon.etc
{
    internal class etc_0933
    {

        static void Main933(string[] args)
        {

            StreamReader sr;
            int n;
            (int x, int y, int w)[] wood;
            int[] x, y, w;
            bool[] use;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(x);
                Array.Sort(y);
                Array.Sort(wood, (x, y) => y.w.CompareTo(x.w));

                int ret = n;
                for (int l = 0; l < n; l++)
                {

                    for (int r = l; r < n; r++)
                    {

                        int w = 2 * (x[r] - x[l]);
                        for (int b = 0; b < n; b++)
                        {

                            for (int t = b; t < n; t++)
                            {

                                int need = 2 * (y[t] - y[b]) + w;

                                int cur = 0;
                                int pop = 0;
                                for (int i = 0; i < n; i++)
                                {

                                    if (wood[i].x < x[l] || wood[i].x > x[r] 
                                        || wood[i].y < y[b] || wood[i].y > y[t])
                                    {

                                        use[i] = true;
                                        cur += wood[i].w;
                                        pop++;
                                    }
                                }

                                if (cur < need)
                                {

                                    for (int i = 0; i < n; i++)
                                    {

                                        if (use[i])
                                        {

                                            use[i] = false;
                                            continue; 
                                        }
                                        
                                        pop++;
                                        cur += wood[i].w;

                                        if (need <= cur) break;
                                    }
                                }

                                if (need <= cur && pop < ret) ret = pop;
                            }
                        }
                    }
                }

                Console.WriteLine(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                wood = new (int x, int y, int w)[n];
                x = new int[n];
                y = new int[n];
                w = new int[n];
                use = new bool[n];

                for (int i = 0; i < n; i++)
                {

                    wood[i] = (ReadInt(), ReadInt(), ReadInt());
                    x[i] = wood[i].x;
                    y[i] = wood[i].y;
                }

                sr.Close();
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
// #include <functional>
// #include <numeric>
// #include <algorithm>
// #include <vector>

using namespace std;

int x[42],y[42],D[42];

int main(){
	int n;
	scanf("%d",&n);
	for (int i = 0; i < n;i ++) scanf("%d%d%d",&x[i],&y[i],&D[i]);
	int maxsum = accumulate(D, D+n, 0);
	int ans = n;
	vector<int> xsort(x,x+n), ysort(y,y+n);
	sort(xsort.begin(),xsort.end()); xsort.resize(unique(xsort.begin(), xsort.end()) - xsort.begin());
	sort(ysort.begin(),ysort.end()); ysort.resize(unique(ysort.begin(), ysort.end()) - ysort.begin());
	vector<int> inner;
	for (int a = 0; a < xsort.size(); a++) for (int b = a; b < xsort.size(); b++) {
		for (int c = 0; c < ysort.size(); c++) for (int d = (int)ysort.size()-1; d >= c; d--) {
			int need = 2*(xsort[b] - xsort[a]) + 2*(ysort[d] - ysort[c]);
			if (need >= maxsum) continue;

			int rem = 0, curcnt = 0;
			inner.clear();
			for (int i = 0; i < n; i++) {
				if (x[i] >= xsort[a] && x[i] <= xsort[b] && y[i] >= ysort[c] && y[i] <= ysort[d]){
					inner.push_back(D[i]);
				} else {
					curcnt++;
					rem += D[i];
				}
			}
			if (curcnt >= ans) break;
			if (rem >= need) {
				ans = min(ans, curcnt);
				continue;
			}
			sort(inner.begin(), inner.end(), greater<int>());
			for (auto val : inner) {
				curcnt++;
				rem += val;
				if (rem >= need) {
					ans = min(ans, curcnt);
					break;
				}
			}
		}
	}
	printf("%d\n", ans);
	return 0;
}
#elif other2
using System;
using System.Collections.Generic;

public class Program
{
    struct Tree
    {
        public int x, y, w;
        public Tree(int x, int y, int w)
        {
            this.x = x; this.y = y; this.w = w;
        }
    }
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        Tree[] trees = new Tree[n];
        for (int i = 0; i < n; i++)
        {
            int[] xyw = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int x = xyw[0], y = xyw[1], w = xyw[2];
            trees[i] = new(x, y, w);
        }
        Dictionary<long, byte> visited = new();
        Queue<long> queue = new();
        visited[(1L << n) - 1] = 0;
        queue.Enqueue((1L << n) - 1);
        while (queue.Count > 0)
        {
            long cur = queue.Dequeue();
            int[] index = new int[5];
            int minX = 0, maxX = 0, minY = 0, maxY = 0, maxW = 0;
            int wood = 0;
            void VarInit(long state)
            {
                minX = minY = int.MaxValue; 
                maxX = maxY = maxW = int.MinValue;
                wood = 0;
                for (int i = 0; i < n; i++)
                {
                    if ((state & 1L << i) == 0)
                    {
                        wood += trees[i].w;
                        continue;
                    }
                    int x = trees[i].x, y = trees[i].y, w = trees[i].w;
                    if (x < minX)
                    {
                        index[0] = i; minX = x;
                    }
                    if (x > maxX)
                    {
                        index[1] = i; maxX = x;
                    }
                    if (y < minY)
                    {
                        index[2] = i; minY = y;
                    }
                    if (y > maxY)
                    {
                        index[3] = i; maxY = y;
                    }
                    if (w > maxW)
                    {
                        index[4] = i; maxW = w;
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                VarInit(cur);
                long next = cur - (1L << index[i]);
                if (visited.ContainsKey(next))
                    continue;
                visited[next] = (byte)(visited[cur] + 1);
                queue.Enqueue(next);
                VarInit(next);
                if (wood >= (maxX - minX + maxY - minY) * 2)
                {
                    Console.Write(visited[next]);
                    return;
                }
            }
        }
    }
}
#endif
}
