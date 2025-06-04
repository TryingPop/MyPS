using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 3
이름 : 배성훈
내용 : 수열과 쿼리 15
    문제번호 : 14427번

    우선순위 큐, 세그먼트 트리 문제다.
    우선순위 큐를 이용해 해결했다.
    세그먼트 트리를 썼다면 더 쉽게 해결되었을 것이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1668
    {

        static void Main1668(string[] args)
        {

            int MAX = 100_000;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();
            var comp = Comparer<(int val, int idx)>.Create((x, y) =>
            {

                int ret = x.val.CompareTo(y.val);
                if (ret == 0) ret = x.idx.CompareTo(y.idx);
                return ret;
            });
            PriorityQueue<(int val, int idx), (int val, int idx)> min = new(MAX * 2, comp);
            int[] arr = new int[n + 1];

            for (int i = 1; i <= n; i++)
            {

                int val = ReadInt();
                arr[i] = val;
                min.Enqueue((val, i), (val, i));
            }

            int m = ReadInt();

            while (m-- > 0)
            {

                int op = ReadInt();

                if (op == 2)
                {

                    sw.Write($"{min.Peek().idx}\n");
                    continue;
                }

                int idx = ReadInt();
                int val = ReadInt();
                arr[idx] = val;

                min.Enqueue((val, idx), (val, idx));
                while (arr[min.Peek().idx] != min.Peek().val)
                {

                    min.Dequeue();
                }
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
const char nl = '\n';
using namespace std;
using ll = long long;
using ld = long double;
const int N = 1e5+1, INF = 0x3f3f3f3f;

pair<int, int> t[2*N];
void upd(int x, int v) {
  t[x+N] = {v, x};
  for (x += N, x /= 2; x; x /= 2) t[x] = min(t[2*x], t[2*x+1]);
}

char get() {
  static char buf[1000000], *S = buf, *T = buf;
  if (S == T) {
    T = (S = buf) + fread(buf, 1, 1000000, stdin);
    if (S == T) return EOF;
  }
  return *S++;
}
void read(int& x) {
  static char c; x = 0;
  for (c = get(); c < '0'; c = get());
  for (; c >= '0'; c = get()) x = x * 10 + c - '0';
}

int main() {
  cin.tie(0)->sync_with_stdio(0);
  memset(t, INF, sizeof t);
  int n; read(n);
  for (int i = 1; i <= n; i++) {
    read(t[N+i].first);
    t[N+i].second = i;
  }
  for (int i = N-1; i; i--) t[i] = min(t[2*i], t[2*i+1]);
  int q; read(q);
  while (q--) {
    int tt; read(tt);
    if (tt == 1) {
      int i, v; read(i); read(v);
      upd(i, v);
    } else {
      cout << t[1].second << nl;
    }
  }
}

#endif
}
