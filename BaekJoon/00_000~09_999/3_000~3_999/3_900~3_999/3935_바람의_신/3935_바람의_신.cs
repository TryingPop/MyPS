using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 22
이름 : 배성훈
내용 : 바람의 신
    문제번호 : 3935번

    dp, 비트마스킹 문제다.
    어떻게 접근해도 시간초과날거 같아 챗 gpt의 힘을 빌렸다.
    그러니 백트래킹을 이용한 dp 방법을 제안했다.

    해당 방법으로 예제를 하니 생각보다 빠르게 통과했다.
    그래서 해당 코드를 나의 코드 작성법에 맞게 변형해 제출하니 통과한다.

    아이디어는 DFS + 백트래킹 + dp를 이용하는 것이다.
    dp는 방문한 곳을 재방지하는 의도로 쓴다.
    재방지를 확인하는건 현재 날짜와 현재 좌표, 그리고 각 좌표별 비온 경우이다.

    DFS 탐색에서는 현재 날이 가능한지 먼저 확인한다.
    이는 현재 비구름 위치에 축제가 열리는지 비가 안온지 7일째 되는 장소가 존재하는지로 판별한다.
    유효한 경우 다음 칸으로 이동한다.

    이렇게 목표 날짜를 넘길 수 있는지 확인한다.
    해당 경우 브루트포스면 매번 9방향으로 이동하고 최대 365일이다.
    그리고 마을이 맑은 날을 보면 7^16이고 이를 곱하면 9 x 365 x 7^16 > 10^17이다.
    문제 조건으로 가지치는 경우, 강력한 가지치기로 시간내에 해결된다고 한다.

    그래도 불안해서 일단 틀리면 뒤에 생각해보자는 식으로 메모리로 경우의 수를 최대한 줄여 제출했다.
    그러니 이상없이 통과했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1723
    {

#if CHAT_GPT
        // 바람의 신 해당 코드 변형해서 제출해보자!
        // gpt
        static readonly int[,] cloudOffsets = new int[,]
        {
            {0,0}, {0,1}, {1,0}, {1,1}
        };

        static readonly (int dx, int dy)[] directions = new (int, int)[]
        {
        (0, 0), (0, 1), (0, 2), (0, -1), (0, -2), (1, 0), (2, 0), (-1, 0), (-2, 0)
        };

        static bool DFS(int day, int x, int y, int N, int[][] festival, int[] rainless, Dictionary<string, bool> memo)
        {
            if (day > N) return true;

            string key = day + "," + x + "," + y + "," + string.Join("-", rainless);
            if (memo.ContainsKey(key)) return false;

            // 비 오는 마을 계산
            bool[] isRain = new bool[16];
            for (int i = 0; i < 4; i++)
            {
                int nx = x + cloudOffsets[i, 0];
                int ny = y + cloudOffsets[i, 1];
                if (nx >= 0 && ny >= 0 && nx < 4 && ny < 4)
                {
                    int idx = nx * 4 + ny;
                    isRain[idx] = true;
                }
            }

            // 조건 위반 체크
            for (int i = 0; i < 16; i++)
            {
                if (festival[day - 1][i] == 1 && isRain[i]) return false;
            }

            int[] nextRainless = new int[16];
            for (int i = 0; i < 16; i++)
            {
                nextRainless[i] = isRain[i] ? 0 : rainless[i] + 1;
                if (nextRainless[i] >= 7) return false;
            }

            foreach (var (dx, dy) in directions)
            {
                int nx = x + dx;
                int ny = y + dy;
                if (nx < 0 || ny < 0 || nx + 1 >= 4 || ny + 1 >= 4) continue;
                if (DFS(day + 1, nx, ny, N, festival, nextRainless, memo)) return true;
            }

            memo[key] = false;
            return false;
        }

        static void Main()
        {
            while (true)
            {
                string line = Console.ReadLine();
                if (line == null || line == "0") break;

                int N = int.Parse(line);
                int[][] festival = new int[N][];
                for (int i = 0; i < N; i++)
                {
                    festival[i] = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                }

                int[] rainless = new int[16]; // 처음엔 모두 0일 (전날 비 옴)
                var memo = new Dictionary<string, bool>();

                bool result = DFS(1, 1, 1, N, festival, rainless, memo);
                Console.WriteLine(result ? 1 : 0);
            }
        }
#else

        static void Main1723(string[] args)
        {

            long RAIN = 0b_111;
            int R = 2;
            int C = 2;
            int MOVE = 48;

            string Y = "1\n";
            string N = "0\n";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n;
            HashSet<long> set;      // 중복 방문 제거
            int[] festival;         // 축제가 일어나는 상태 festival[i] & (1 << j) != 0인 경우 i일에 j번 장소에는 축제가 일어난다.
            int[] rain;             // 연속된 맑은 날의 상태 = 비가 안온 상태라 rain으로 했다.
            int[] dirR, dirC;       // 다음 이동 방향
            int[][] chkPos;         // chkPos[r][c] : r, c에서 비오는 상태 확인할 값

            Init();

            while ((n = ReadInt()) != 0)
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                sw.Write(DFS(0, 1, 1, 0) ? Y : N);

                bool DFS(int _day, int _r, int _c, long _state)
                {

                    if (_day >= n) 
                        return true;

                    // 방문 여부 확인
                    // 날짜, 현재 좌표, 각 좌표의 연속된 맑은 날을 기준으로 방문 여부 확인한다.
                    long key = SetMyHash(_day, _r, _c, _state);
                    // 찾은 경우 바로 탈출하므로 이미 방문한 곳을 탐색한 것이면
                    // 불가능하다와 같다. 그래서 불가능하다고 탈출
                    if (set.Contains(key)) return false;
                    // 재방문 방지
                    // 맨 앞에 뒀으니 언제든지 탈출해도 된다!
                    set.Add(key);

                    // 현재 축제 장소에 비오는 지 확인
                    // 비오면 불가능하고 해당 경우 가지치기!
                    if ((chkPos[_r][_c] & festival[_day]) != 0) return false;

                    // 비내리기
                    SetRain(_state);

                    for (int i = 0; i < 16; i++)
                    {

                        // 현재 장소에 비가 온다.
                        if ((chkPos[_r][_c] & (1 << i)) != 0) rain[i] = 0;
                        else
                        {

                            // 맑은날 1일 추가
                            rain[i]++;

                            // 0 ~ 6일까지만이라했으므로 7일되면 탈출!
                            if (rain[i] == 7) return false;
                        }
                    }

                    // 비오는 상태 갱신
                    _state = GetRain();

                    // 문제 조건에 맞는 비구름 이동 시작
                    for (int i = 0; i < 9; i++)
                    {

                        int nR = _r + dirR[i];
                        int nC = _c + dirC[i];
                        
                        if (ChkInvalidPos(nR, nC)) continue;
                        // 가능한 경우 1개라도 찾으면 바로 TRUE로 탈출!
                        if (DFS(_day + 1, nR, nC, _state)) return true;
                    }

                    return false;
                }

                long SetMyHash(int _day, int _r, int _c, long _state)
                {

                    // 날짜를 가장 큰 비트에
                    long ret = _day;
                    // 이후 이어서 현재 좌표 r
                    ret = (ret << R) | (long)_r;
                    // 이후에 이어서 현재 좌표 c
                    ret = (ret << C) | (long)_c;
                    // 마지막으로 맑은 날의 상태, 비가 안온 상태를 이어 붙인다.
                    ret = (ret << MOVE) | _state;

                    return ret;
                }

                // 맵을 벗어난 경우 확인하는 함수
                bool ChkInvalidPos(int _r, int _c)
                    => _r < 0 || _c < 0 || _r >= 3 || _c >= 3;

                // 비트마스킹 한 것을 배열로 표현
                void SetRain(long _state)
                {

                    // 연속된 맑은 날의 비트마스킹을 배열에 표현
                    // 각각 1씩 더해야 하기에 +1 연산으로 했고 이를 위해 배열로 바꿔서 한다.
                    for (int i = 15; i >= 0; i--) 
                    {

                        rain[i] = (int)(_state & RAIN);
                        _state >>= 3;
                    }
                }

                // 배열을 비트마스킹으로 표현
                long GetRain()
                {

                    // 연속된 맑은 날은 7일이 되면 강제 탈출하므로 0 ~ 7의 값을 갖는다.
                    // 그래서 3비트에 담긴다.
                    // 
                    // 연속된 맑은 날의 상태를 비트마스킹으로 기록
                    // 0 ~ 2 번 비트 15번 좌표의 연속된 맑은 날
                    // 3 ~ 5 번 비트 14번 좌표의 연속된 맑은 날
                    // ... 
                    // 45 ~ 47 번 비트 0번 좌표의 연속된 맑은 날
                    long ret = 0;
                    for (int i = 0; i < 16; i++)
                    {

                        ret = (ret << 3) | (long)rain[i];
                    }

                    return ret;
                }
            }

            void Init()
            {

                festival = new int[365];
                set = new(1 << 16);
                rain = new int[16];

                dirR = new int[9] { -1, 0, 1, 0, -2, 0, 2, 0, 0 };
                dirC = new int[9] { 0, -1, 0, 1, 0, -2, 0, 2, 0 };

                chkPos = new int[3][];
                int[] chkR = { 0, 0, 1, 1 }, chkC = { 0, 1, 0, 1 };
                for (int i = 0; i < 3; i++)
                {

                    chkPos[i] = new int[3];
                    for (int j = 0; j < 3; j++)
                    {

                        for (int k = 0; k < 4; k++)
                        {

                            int idx = PosToIdx(i + chkR[k], j + chkC[k]);
                            chkPos[i][j] |= 1 << idx;
                        }
                    }
                }
            }

            // r, c의 좌표로 문제 표에 맞는 인덱스 찾기
            int PosToIdx(int _r, int _c)
                    => 4 * _r + _c;

            void Input()
            {

                for (int i = 0; i < n; i++)
                {

                    int r = 0;
                    for (int j = 0; j < 16; j++)
                    {

                        if (ReadInt() == 1) r |= 1 << j;
                    }

                    festival[i] = r;
                }

                for (int i = 0; i < 16; i++)
                {

                    rain[i] = 0;
                }

                set.Clear();
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
#endif
    }

#if other
// # include <bits/stdc++.h>

using namespace std;

// #define rep(i, a, b) for (int i = (int)(a); i < (int)(b); i++)
// #define rrep(i, a, b) for (int i = (int)(b)-1; i >= (int)(a); i--)
// #define ALL(v) (v).begin(), (v).end()
// #define UNIQUE(v) sort(ALL(v)), (v).erase(unique(ALL(v)), (v).end())
// #define SZ(v) (int)v.size()
// #define MIN(v) *min_element(ALL(v))
// #define MAX(v) *max_element(ALL(v))
// #define LB(v, x) int(lower_bound(ALL(v), (x)) - (v).begin())
// #define UB(v, x) int(upper_bound(ALL(v), (x)) - (v).begin())

using uint = unsigned int;
using ll = long long int;
using ull = unsigned long long;
using i128 = __int128_t;
using u128 = __uint128_t;
const int inf = 0x3fffffff;
const ll INF = 0x1fffffffffffffff;

template <typename T> inline bool chmax(T &a, T b) {
    if (a < b) {
        a = b;
        return 1;
    }
    return 0;
}
template <typename T> inline bool chmin(T &a, T b) {
    if (a > b) {
        a = b;
        return 1;
    }
    return 0;
}
template <typename T, typename U> T ceil(T x, U y) {
    assert(y != 0);
    if (y < 0)
        x = -x, y = -y;
    return (x > 0 ? (x + y - 1) / y : x / y);
}
template <typename T, typename U> T floor(T x, U y) {
    assert(y != 0);
    if (y < 0)
        x = -x, y = -y;
    return (x > 0 ? x / y : (x - y + 1) / y);
}
template <typename T> int popcnt(T x) {
    return __builtin_popcountll(x);
}
template <typename T> int topbit(T x) {
    return (x == 0 ? -1 : 63 - __builtin_clzll(x));
}
template <typename T> int lowbit(T x) {
    return (x == 0 ? -1 : __builtin_ctzll(x));
}

template <class T, class U>
ostream &operator<<(ostream &os, const pair<T, U> &p) {
    os << "P(" << p.first << ", " << p.second << ")";
    return os;
}
template <typename T> ostream &operator<<(ostream &os, const vector<T> &vec) {
    os << "{";
    for (int i = 0; i < vec.size(); i++) {
        os << vec[i] << (i + 1 == vec.size() ? "" : ", ");
    }
    os << "}";
    return os;
}
template <typename T, typename U>
ostream &operator<<(ostream &os, const map<T, U> &map_var) {
    os << "{";
    for (auto itr = map_var.begin(); itr != map_var.end(); itr++) {
        os << "(" << itr->first << ", " << itr->second << ")";
        itr++;
        if (itr != map_var.end())
            os << ", ";
        itr--;
    }
    os << "}";
    return os;
}
template <typename T> ostream &operator<<(ostream &os, const set<T> &set_var) {
    os << "{";
    for (auto itr = set_var.begin(); itr != set_var.end(); itr++) {
        os << *itr;
        ++itr;
        if (itr != set_var.end())
            os << ", ";
        itr--;
    }
    os << "}";
    return os;
}
// # ifdef LOCAL
// #define show(...) _show(0, #__VA_ARGS__, __VA_ARGS__)
// #else
// #define show(...) true
// #endif
    template<typename T> void _show(int i, T name)
    {
        cerr << '\n';
    }
    template<typename T1, typename T2, typename...T3>
void _show(int i, const T1 &a, const T2 &b, const T3 &...c)
    {
        for (; a[i] != ',' && a[i] != '\0'; i++)
            cerr << a[i];
        cerr << ":" << b << " ";
        _show(i + 1, a, c...);
    }

    /**
     * @brief template
     */

    int N;
    vector<int> A(N);
    vector<vector<int>> G(9);
    map<int, bool> mp;

    int encode(int K, int X, int Y, int D0, int D2, int D6, int D8)
    {
        return K * 1000000 + X * 100000 + Y * 10000 + D0 * 1000 + D2 * 100 + D6 * 10 + D8;
    }

    bool DFS(int K, int X, int Y, int D0, int D2, int D6, int D8)
    {
        if (K == N) return true;
        if (D0 >= 7 || D2 >= 7 || D6 >= 7 || D8 >= 7) return false;
        if ((A[K] & (1 << (X * 3 + Y))) == 0) return false;
        int E = encode(K, X, Y, D0, D2, D6, D8);
        if (mp.count(E)) return mp[E];
        for (int Next : G[X * 3 + Y])
        {
            int NX = Next / 3, NY = Next % 3;
            int N0 = D0 + 1, N2 = D2 + 1, N6 = D6 + 1, N8 = D8 + 1;
            if (Next == 0) N0 = 0;
            if (Next == 2) N2 = 0;
            if (Next == 6) N6 = 0;
            if (Next == 8) N8 = 0;
            if (DFS(K + 1, NX, NY, N0, N2, N6, N8)) return mp[E] = true;
        }
        return mp[E] = false;
    }

    int main()
    {
        rep(i, 0, 3) {
            rep(j, 0, 3) {
                G[i * 3 + j].push_back(i * 3 + j);
                rep(k, 0, 3) {
                    if (i != k) G[i * 3 + j].push_back(k * 3 + j);
                    if (j != k) G[i * 3 + j].push_back(i * 3 + k);
                }
            }
        }
        while (1)
        {
            mp.clear();
            cin >> N;
            if (N == 0) return 0;
            A.resize(N);
            rep(i, 0, N) {
                vector<vector<int>> X(4,vector<int>(4));
                rep(j, 0, 4) rep(k, 0, 4) cin >> X[j][k];
                int B = 0;
                rep(j, 0, 3) {
                    rep(k, 0, 3) {
                        if (X[j][k] == 0 && X[j + 1][k] == 0 && X[j][k + 1] == 0 && X[j + 1][k + 1] == 0) B |= (1 << (j * 3 + k));
                    }
                }
                A[i] = B;
            }
            cout << (DFS(0, 1, 1, 1, 1, 1, 1) ? 1 : 0) << endl;
        }
    }

#endif
}
