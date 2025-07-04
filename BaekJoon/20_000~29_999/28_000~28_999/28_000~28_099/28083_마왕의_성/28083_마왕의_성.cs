using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 30
이름 : 배성훈
내용 : 마왕의 성
    문제번호 : 28083번

    분리집합 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1743
    {

        static void Main1743(string[] args)
        {

            int row, col;
            int[][] high, cost;
            long[] val, ret;
            PriorityQueue<int, int> pq;

            Input();

            GetRet();

            Output();

            void Output()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0, j = 0; i < ret.Length; i++, j++)
                {

                    if (j == col)
                    {

                        sw.Write('\n');
                        j = 0;
                    }

                    sw.Write($"{ret[i]} ");
                }
            }

            void GetRet()
            {

                int[] group = new int[row * col];
                int[] stk = new int[row * col];
                int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };

                for (int i = 1; i < group.Length; i++)
                {

                    group[i] = i;
                }

                Queue<int> q = new(row * col);
                ret = new long[row * col];
                int prev = -1;

                while (pq.Count > 0)
                {

                    int idx = pq.Dequeue();
                    (int r, int c) = IdxToPos(idx);
                    int curH = high[r][c];

                    if (prev != curH)
                    {

                        // 높이 변경이 일어나는 경우다.
                        // 값 기록이 필요하다!
                        prev = curH;
                        while (q.Count > 0)
                        {

                            int i = q.Dequeue();
                            ret[i] = val[Find(i)];
                        }
                    }

                    q.Enqueue(idx);
                    for (int i = 0; i < 4; i++)
                    {

                        int nR = r + dirR[i];
                        int nC = c + dirC[i];
                        if (ChkInvalidPos(nR, nC)) continue;

                        int nextH = high[nR][nC];
                        if (curH < nextH) continue;

                        Union(idx, PosToIdx(nR, nC), curH == nextH);
                    }
                }

                // 마지막 갱신 안되거 있는지 확인
                while (q.Count > 0)
                {

                    int i = q.Dequeue();
                    ret[i] = val[Find(i)];
                }

                bool ChkInvalidPos(int _r, int _c)
                    => _r < 0 || _c < 0 || _r >= row || _c >= col;

                int Find(int _chk)
                {

                    int len = 0;
                    while (_chk != group[_chk])
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

                void Union(int _f, int _t, bool _flag)
                {

                    int f = Find(_f);
                    int t = Find(_t);

                    if (f == t) return;

                    int max, min;
                    if (_flag && t < f)
                    {

                        min = t;
                        max = f;
                    }
                    else
                    {

                        min = f;
                        max = t;
                    }

                    group[max] = min;
                    val[min] += val[max];
                }
            }

            int PosToIdx(int _r, int _c)
                    => _r * col + _c;

            (int r, int c) IdxToPos(int _idx)
                => (_idx / col, _idx % col);

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();

                pq = new(row * col);
                val = new long[row * col];

                high = new int[row][];
                cost = new int[row][];


                for (int r = 0; r < row; r++)
                {

                    high[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        int cur = ReadInt();
                        high[r][c] = cur;
                        pq.Enqueue(PosToIdx(r, c), cur);
                    }
                }

                for (int r = 0; r < row; r++)
                {

                    cost[r] = new int[col];
                    for (int c = 0; c< col; c++)
                    {

                        int cur = ReadInt();
                        cost[r][c] = cur;
                        val[PosToIdx(r, c)] = cur;
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
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    static void Main()
    {
        Reader reader = new(Console.OpenStandardInput());
        int n = reader.ReadInt(), m = reader.ReadInt();
        int[,] height = new int[n, m], cost = new int[n, m];
        List<(int r, int c)> list = new();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                height[i, j] = reader.ReadInt();
                list.Add((i, j));
            }
        }
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                cost[i, j] = reader.ReadInt();
            }
        }
        long[,] answer = new long[n, m];
        Init(n, m, cost);
        list.Sort((x, y) => height[x.r, x.c] - height[y.r, y.c]);
        for (int i = 0; i < n * m;)
        {
            int start = i, h = height[list[i].r, list[i].c];
            while (i < n * m && height[list[i].r, list[i].c] == h)
            {
                int r = list[i].r, c = list[i].c, cur = list[i].r * m + list[i].c;
                if (r > 0 && height[r - 1, c] <= h)
                    Union((r - 1) * m + c, cur);
                if (r < n - 1 && height[r + 1, c] <= h)
                    Union((r + 1) * m + c, cur);
                if (c > 0 && height[r, c - 1] <= h)
                    Union(r * m + c - 1, cur);
                if (c < m - 1 && height[r, c + 1] <= h)
                    Union(r * m + c + 1, cur);
                i++;
            }
            for (int j = start; j < i; j++)
            {
                answer[list[j].r, list[j].c] = -parent[Find(list[j].r * m + list[j].c)];
            }
        }
        StreamWriter sw = new(Console.OpenStandardOutput());
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                sw.Write(answer[i, j]);
                if (j + 1 < m)
                    sw.Write(' ');
            }
            if (i + 1 < n)
                sw.Write('\n');
        }
        sw.Close();
    }
    static long[] parent;
    static void Init(int n, int m, int[,] array)
    {
        parent = new long[n * m];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                parent[i * m + j] = -array[i, j];
            }
        }
    }
    static long Find(long x)
    {
        if (parent[x] < 0)
            return x;
        return parent[x] = Find(parent[x]);
    }
    static void Union(long x, long y)
    {
        x = Find(x); y = Find(y);
        if (x != y)
        {
            parent[x] += parent[y];
            parent[y] = x;
        }
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
using namespace std;
typedef long long ll;
typedef pair<ll,ll> pll;


ll parent[1001000]; /// 부모
ll nodes[1001000]; /// 자식 수
ll height[1001000]; /// 높이
ll tax[1001000]; /// 세금
ll N,M;

pll by_h[1001000];
stack<ll> conc; /// 결정내릴 애들 
ll out[1010][1010]; /// 결과
bool abled[1001000]; /// 연결 가능?



ll Find(ll x) {
    if(parent[x] == x) return x;
    else return parent[x] = Find(parent[x]);
}

void Union(ll x, ll y) {
    x = Find(x);
    y = Find(y);
    if(x > y) swap(x, y);
    if(x == y) return;
    
    /// 큰 애를 작은 애에 Union
    nodes[x] += nodes[y];
    nodes[y] = 0;
    
    parent[y] = x;
}




int main() {
    ios::sync_with_stdio(false);
    cin.tie(NULL);
    cout.tie(NULL);
    
    /// 초기화
    ll t = 1001000;
    for(int t=0;t<1001000; t++) {
        parent[t] = t;
    }
    fill(abled, abled+1001000, 0);
    
    /// 입력받기
    cin >> N >> M;
    
    for(int i=0;i<N;i++) { /// 행
        for(int j=0;j<M;j++) { /// 열
            cin >> height[j + i*M];
            by_h[j + i*M] = {height[j + i*M], j + i*M};
        }
    }
    
    for(int i=0;i<N;i++) { /// 행
        for(int j=0;j<M;j++) { /// 열
            cin >> tax[j + i*M];
        }
    }
    
    /// 초기화 (tax 를 node 수로 치환)
    for(int t=0;t<1001000; t++) {
        nodes[t] = tax[t];
    }
    
    
    /// 높이에 따라 부지 정렬 (높이 낮은 순서대로)
    sort(by_h, by_h+(N*M));
    
    /// by_h 정렬 순서대로 union
    int h_bef = 0; /// 이전 높이
    
    for(int i = 0; i<N*M; i++) {
        pll x = by_h[i];    
        
        int h = x.first; /// 높이
        int n = x.second; /// 현재 노드
        
        ///cout << h << " " << n << " !!\n";
        
        abled[n] = 1;
        
        if(h_bef != h) {
            ///cout << h_bef << " " << n << " ***\n";
            while(!conc.empty()) {
                int A = conc.top(); 
                
                /// 높이 업데이트되어야 함
                int y = Find(A); /// 부모
                out[A/M][A%M] = nodes[y];
                
                
                conc.pop();
            }
        }
        h_bef = h;
        conc.push(n);
        
        /// 탐색 진행
        /// 1. 상
        if(n/M > 0  &&  abled[n-M]) {
            Union(n-M, n);
            //cout << "@@\n\n";
        }
        
        /// 2. 하
        if(n/M < N-1  &&  abled[n+M]) {
            Union(n+M, n);
            //cout << "##\n\n";
        }
        
        /// 3. 좌
        if(n%M > 0  &&  abled[n-1]) {
            Union(n-1, n);
            //cout << "!!\n\n";
        }
            
        /// 4. 우
        if(n%M < M-1  &&  abled[n+1]) {
            Union(n+1, n);
            //cout << "$$\n\n";
        }
    }
    
    
    while(!conc.empty()) {
        int A = conc.top(); 
                
        /// 높이 업데이트되어야 함
        int y = Find(A); /// 부모
        out[A/M][A%M] = nodes[y];
                
                
        conc.pop();
    }
    
    for(int i=0;i<N;i++) { /// 행
        for(int j=0;j<M;j++) { /// 열
            cout << out[i][j] << " ";
        }
        cout << "\n";
    }
    return 0;
}

#endif
}
