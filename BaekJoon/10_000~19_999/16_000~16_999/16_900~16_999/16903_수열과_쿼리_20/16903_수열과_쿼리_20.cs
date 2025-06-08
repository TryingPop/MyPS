using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 29
이름 : 배성훈
내용 : 수열과 쿼리 20
    문제번호 : 16903번

    트라이 문제다.
    연결 리스트를 이용해 해결했다.
    풀의 크기를 적절히 안해서 2번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1653
    {

        static void Main1653(string[] args)
        {

            const int NULL = 0;

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int q = ReadInt();
            int HEAD, TAIL, POOL;
            (int n, int z, int o, int v)[] nodes;

            Init();

            QueryAdd(0);
            while (q-- > 0)
            {

                int op = ReadInt();

                int n = ReadInt();

                if (op == 1) QueryAdd(n);
                else if (op == 2) QueryRemove(n);
                else sw.Write($"{QueryFind(n)}\n");
            }

            int QueryFind(int _n)
            {

                int cur = HEAD;
                int other = 0;
                for (int i = 29; i >= 0; i--)
                {

                    bool isOne = ((1 << i) & _n) == 0;

                    if ((isOne && nodes[cur].o != NULL) || nodes[cur].z == NULL)
                    {

                        cur = nodes[cur].o;
                        other |= 1 << i;
                    }
                    else
                        cur = nodes[cur].z;
                }

                return other ^ _n;
            }

            void QueryRemove(int _n)
            {

                DFS(HEAD);

                void DFS(int _cur, int _dep = 29)
                {


                    bool isZero = ((1 << _dep) & _n) == 0;
                    int next = Move(_cur, isZero);

                    if (_dep > 0)
                    {

                        DFS(next, _dep - 1);
                        nodes[next].v--;
                    }
                    else
                        nodes[next].v++;

                    if (nodes[next].v == 0)
                    {

                        RemoveNode(next);
                        if (isZero) nodes[_cur].z = NULL;
                        else nodes[_cur].o = NULL;
                    }
                }
            }

            void RemoveNode(int _idx)
            {

                nodes[_idx].z = NULL;
                nodes[_idx].o = NULL;
                nodes[_idx].n = POOL;
                POOL = _idx;
            }

            void QueryAdd(int _n)
            {

                int cur = HEAD;

                for (int i = 29; i >= 1; i--)
                {

                    bool isZero = ((1 << i) & _n) == 0;
                    cur = Move(cur, isZero);
                    nodes[cur].v++;
                }

                {

                    bool isZero = (1 & _n) == 0;
                    cur = Move(cur, isZero);
                    nodes[cur].v--;
                }
            }

            int Move(int _cur, bool _isZero)
            {

                if (_isZero)
                {

                    if (nodes[_cur].z == NULL) nodes[_cur].z = NewNode();
                    return nodes[_cur].z;
                }
                else
                {

                    if (nodes[_cur].o == NULL) nodes[_cur].o = NewNode();
                    return nodes[_cur].o;
                }
            }

            int NewNode()
            {

                int ret = POOL;
                POOL = nodes[POOL].n;
                return ret;
            }

            void Init()
            {

                HEAD = 1;
                TAIL = 2;
                POOL = 3;
                nodes = new (int n, int z, int o, int v)[3_000_005];

                for (int i = 4; i < nodes.Length; i++)
                {

                    nodes[i - 1].n = i;
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
const int N = 2e5+1, L = 30;

struct node {
  int c[2], cnt;
} t[L*N];
int id = 1;

void upd(int x, int v, int i=1, int bit=L-1) {
  t[i].cnt += v;
  if (bit < 0) return;
  int d = (x >> bit) & 1;
  if (!t[i].c[d]) t[i].c[d] = ++id;
  upd(x, v, t[i].c[d], bit-1);
}

int query(int x, int i=1, int bit=L-1) {
  if (bit < 0) return 0;
  int d = (x >> bit) & 1;
  if (!t[i].c[!d] || t[t[i].c[!d]].cnt == 0) return query(x, t[i].c[d], bit-1);
  return query(x, t[i].c[!d], bit-1) + (1 << bit);
}

char get() {
  static char buf[100000], *S = buf, *T = buf;
  if (S == T) {
    T = (S = buf) + fread(buf, 1, 100000, stdin);
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
  upd(0, 1);
  int q; read(q);
  while (q--) {
    int tt, x; read(tt); read(x);
    if (tt == 1) {
      upd(x, 1);
    } else if (tt == 2) {
      upd(x, -1);
    } else {
      cout << query(x) << nl;
    }
  }
}
#endif
}
