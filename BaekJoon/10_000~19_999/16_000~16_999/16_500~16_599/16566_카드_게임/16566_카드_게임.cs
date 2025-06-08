using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 8
이름 : 배성훈
내용 : 카드 게임
    문제번호 : 16566번

    분리 집합, 이분 탐색 문제다.
    아이디어는 다음과 같다.

    입력된 수 보다 크면서 가장 작은 수를 찾아야 한다.
    그래서 이분탐색으로 찾고, 사용되었는지 판별 여부는 
    유니온 파인드 알고리즘으로 기록해 사용안된 노드를 가리키게 했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1168
    {

        static void Main1168(string[] args)
        {

            StreamReader sr;

            int n, m, k;
            int[] arr, group;
            int[] stk;

            Solve();
            void Solve()
            {

                Input();

                Set();

                GetRet();
            }

            void GetRet()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < k; i++)
                {

                    int chk = ReadInt();
                    int idx = BinarySearch(chk);

                    int f = Find(idx);
                    sw.Write($"{arr[f]}\n");
                    group[f] = f + 1;
                }

                sw.Close();

                int BinarySearch(int _n)
                {

                    int l = 0;
                    int r = m - 1;

                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;

                        if (arr[mid] <= _n) l = mid + 1;
                        else r = mid - 1;
                    }

                    return l;
                }

                int Find(int _chk)
                {

                    int len = 0;
                    while(_chk != group[_chk])
                    {

                        stk[len++] = _chk;
                        _chk = group[_chk];
                    }

                    while (len-- > 0)
                    {

                        group[stk[len]] = _chk;
                    }

                    return _chk;
                }
            }

            void Set()
            {

                Array.Sort(arr);

                group = new int[m + 2];
                for (int i = 1; i <= m + 1; i++)
                {

                    group[i] = i;
                }

                stk = new int[m + 1];
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
                k = ReadInt();
                arr = new int[m];

                for (int i = 0; i < m; i++)
                {

                    arr[i] = ReadInt();
                }
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class Program
{
    static void Main()
    {
        Reader reader = new(Console.OpenStandardInput());
        int n = reader.ReadInt(), m = reader.ReadInt(), k = reader.ReadInt();
        bool[] check = new bool[n + 1];
        for (int i = 0; i < m; i++)
        {
            check[reader.ReadInt()] = true;
        }
        List<int> cards = new();
        for (int i = 1; i <= n; i++)
        {
            if (check[i])
                cards.Add(i);
        }
        Init(m + 1);
        StringBuilder sb = new();
        for (int i = 0; i < k; i++)
        {
            int j = UpperBound(cards, reader.ReadInt());
            sb.Append(cards[Find(j)]);
            if (i + 1 < k)
                sb.Append('\n');
            Union(j, Find(j) + 1);
        }
        Console.Write(sb.ToString());
    }
    static int[] parent;
    static void Init(int n)
    {
        parent = new int[n];
        for (int i = 0; i < n; i++)
        {
            parent[i] = i;
        }
    }
    static int Find(int x)
    {
        if (parent[x] == x)
            return x;
        return parent[x] = Find(parent[x]);
    }
    static void Union(int x, int y)
    {
        x = Find(x); y = Find(y);
        if (x != y)
            parent[x] = y;
    }
    static int UpperBound(List<int> list, int item)
    {
        int left = 0, right = list.Count - 1;
        while (left <= right)
        {
            int mid = (left + right) / 2;
            if (list[mid] <= item)
                left = mid + 1;
            else
                right = mid - 1;
        }
        return left;
    }
    class Reader
    {
        private Stream _inStream;
        private readonly int _bufferSize;
        private byte[] _buffer;
        private int _index, _readCount;
        private bool _isEof;
        public Reader(Stream inStream, int size = 65536)
        {
            _inStream = inStream;
            this._bufferSize = size;
            _buffer = new byte[size];
        }
        public byte ReadByte()
        {
            if (_index >= _readCount)
            {
                _readCount = _inStream.Read(_buffer, 0, _bufferSize);
                _index = 0;
                if (_readCount == 0)
                    _isEof = true;
            }
            return _buffer[_index++];
        }
        public int ReadInt()
        {
            byte b;
            while ((b = ReadByte()) != 45 && b < 48) { }
            bool neg = b == 45;
            int result = !neg ? b - 48 : 0;
            while (true)
            {
                b = ReadByte();
                if (b < 48)
                    break;
                result = result * 10 + b - 48;
            }
            return neg ? -result : result;
        }
    }
}
#elif other2
// #include <bits/stdc++.h>
// #include <sys/mman.h>
// #include <sys/stat.h>

using namespace std;

int parent[40'000'0'1];
int sz[4'0'0'0'00'1];
bool bucket[4000001];

int stk[4000001];

inline int find(int u) {
  int i = 0;
  while (parent[u] > 0) {
    stk[i++] = u;
    u = parent[u];
  }
  while (i > 0) {
    parent[stk[--i]] = u;
  }
  return u;
  // if (parent[u] < 0)
  //   return u;
  // else
  //   return parent[u] = find(parent[u]);
}

inline void merge(int u, int v) {
  u = find(u);
  v = find(v);

  if (sz[u] > sz[v]) swap(u, v);
  parent[v] = min(parent[u], parent[v]);
  parent[u] = v;
  sz[v] += sz[u];
}

int main() {
  struct stat st;
  fstat(0, &st);
  char* p = (char*)mmap(0, st.st_size, PROT_READ, MAP_SHARED, 0, 0);
  auto ReadInt = [&]() {
    int ret = 0;
    for (char c = *p++; c & 16; ret = 10 * ret + (c & 15), c = *p++)
      ;
    return ret;
  };
  // ios::sync_with_stdio(false);
  // cin.tie(nullptr);
  // cout.tie(nullptr);

  int n = ReadInt(), m = ReadInt(), k = ReadInt();

  // cin >> n >> m >> k;

  for (int i = 0; i < m; i++) {
    int card = ReadInt();
    // cin >> card;

    bucket[card] = true;
  }

  int ssz = 1;
  for (int ptr = 1; ptr <= n; ++ptr) {
    if (bucket[ptr]) {
      parent[ptr] = -ptr;
      sz[ptr] = ssz;
      ssz = 1;
    } else {
      parent[ptr] = ptr + 1;
      ssz += 1;
    }

    // parent[card] = -card;
    // sz[card] = card - lower;
    // for (int j = lower + 1; j < card; j++) {
    //   parent[j] = card;
    // }
    // lower = card;
  }

  for (int i = 0; i < k; i++) {
    int card = ReadInt();
    int ans;

    ans = -parent[find(card + 1)];
    cout << ans << "\n";

    merge(ans, ans + 1);
  }
}

#endif
}
