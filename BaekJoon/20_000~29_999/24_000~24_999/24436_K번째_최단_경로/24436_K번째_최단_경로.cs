using System.Numerics;

/*
날짜 : 2024. 12. 22
이름 : 배성훈
내용 : K번째 최단 경로
    문제번호 : 24436번

    수학, 다이나믹 프로그래밍, BFS, 조합론 문제다.
    조합론으로 경우의 수를 확인하고,
    경우의 수를 초과하면 끊는 가지치기(백트래킹)를 하면서 경로를 탐색했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1213
    {

        static void Main1213(string[] args)
        {

            long MAX = 1_000_000_000_000_000_000;
            StreamReader sr;
            StreamWriter sw;

            int l, x, y;
            long k;
            int[] sub;
            int[] add;
            long[,,,] cnt;
            List<int> order, calc;
            Queue<int> q;
            
            Solve();
            void Solve()
            {

                Init();

                int t = int.Parse(sr.ReadLine());

                while(t-- > 0)
                {

                    Input();

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                order.Clear();
                q.Enqueue(x);
                while (q.Count > 0)
                {

                    var node = q.Dequeue();
                    if (node == y) break;
                    calc.Clear();
                    int curX = node;
                    int curY = y;
                    for (int i = 0; i < l; i++)
                    {

                        int chkX = curX % 10;
                        curX /= 10;
                        int chkY = curY % 10;
                        curY /= 10;

                        if (chkX == chkY) continue;
                        int a = chkX < chkY ? 1 : -1;
                        int next = add[i] * a + node;
                        calc.Add(next);
                    }

                    calc.Sort();
                    int n = -1;
                    for (int i = 0; i < calc.Count; i++)
                    {

                        long next = Cnt(calc[i], y);
                        if (next < k) k -= next;
                        else
                        {

                            n = i;
                            break;
                        }
                    }

                    if (n == -1)
                    {

                        sw.Write("NO\n");
                        return;
                    }

                    order.Add(calc[n]);
                    q.Enqueue(calc[n]);
                }

                Write();


                void Write()
                {

                    if (l == 2) Write1();
                    else if (l == 3) Write2();
                    else Write3();
                }

                void Write1()
                {

                    sw.Write($"{x:00} ");
                    for (int i = 0; i < order.Count; i++)
                    {

                        sw.Write($"{order[i]:00} ");
                    }
                    sw.Write('\n');
                }

                void Write2()
                {

                    sw.Write($"{x:000} ");
                    for (int i = 0; i < order.Count; i++)
                    {

                        sw.Write($"{order[i]:000} ");
                    }
                    sw.Write('\n');
                }

                void Write3()
                {

                    sw.Write($"{x:0000} ");
                    for (int i = 0; i < order.Count; i++)
                    {

                        sw.Write($"{order[i]:0000} ");
                    }
                    sw.Write('\n');
                }

                long Cnt(int _x, int _y)
                {

                    for (int i = 0; i < 4; i++)
                    {

                        sub[i] = 0;
                    }

                    for (int i = 0; i < l; i++)
                    {

                        int curX = _x % 10;
                        int curY = _y % 10;
                        _x /= 10;
                        _y /= 10;

                        sub[i] = Math.Abs(curX - curY);
                    }

                    return cnt[sub[0], sub[1], sub[2], sub[3]];
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                add = new int[4] { 1, 10, 100, 1000 };
                order = new(36);
                calc = new(4);
                sub = new int[4];

                cnt = new long[10, 10, 10, 10];
                q = new(10_000);
                // 경우의 수 계산
                BigInteger[] fac = new BigInteger[37];
                fac[0] = 1;

                for (int i = 1; i < fac.Length; i++)
                {

                    fac[i] = fac[i - 1] * i;
                }

                for (int n1 = 0; n1 < 10; n1++)
                {

                    for (int n2 = 0; n2 < 10; n2++)
                    {

                        for (int n3 = 0; n3 < 10; n3++)
                        {

                            for (int n4 = 0; n4 < 10; n4++)
                            {

                                cnt[n1, n2, n3, n4] = Calc(n1, n2, n3, n4);
                            }
                        }
                    }
                }

                long Calc(int _n1, int _n2, int _n3, int _n4)
                {

                    int sum = _n1 + _n2 + _n3 + _n4;
                    BigInteger ret = fac[sum] / fac[_n1];
                    ret /= fac[_n2];
                    ret /= fac[_n3];
                    ret /= fac[_n4];

                    if (ret > MAX) return MAX;
                    else return (long)ret;
                }
            }

            void Input()
            {

                l = ReadInt();
                k = ReadLong();

                x = ReadInt();
                y = ReadInt();

                long ReadLong()
                {

                    long ret = 0;

                    while (TryReadLong()) { }
                    return ret;

                    bool TryReadLong()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == ' ' || c == '\n') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
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
                        if (c == ' ' || c == '\n') return true;
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
// #include <bits/stdc++.h>

using namespace std;

// #define all(v) v.begin(), v.end()
typedef long long ll;
const int NMAX = 1e4 + 5;
ll tc, l, k, dp[10][10][10][10], d[4], p[4], cnt, x;
string s, t;

int main(void){
    ios::sync_with_stdio(0); cin.tie(0); cout.tie(0);
    
    dp[0][0][0][0] = 1;
    for(int i = 0; i < 10; i++)
        for(int j = 0; j < 10; j++)
            for(int k = 0; k < 10; k++)
                for(int l = 0; l < 10; l++){
                    if(i) dp[i][j][k][l] += dp[i - 1][j][k][l];
                    if(j) dp[i][j][k][l] += dp[i][j - 1][k][l];
                    if(k) dp[i][j][k][l] += dp[i][j][k - 1][l];
                    if(l) dp[i][j][k][l] += dp[i][j][k][l - 1];
                }

    cin >> tc;
    while(tc--){
        cin >> l >> k >> s >> t;
        cnt = 0;
        memset(d, 0, sizeof(d));
        for(int i = 0; i < l; i++) {
            d[i] = t[i] - s[i];
            if(d[i] < 0) d[i] = -d[i], p[i] = -1;
            else p[i] = 1;
            cnt += d[i];
        }
        if(dp[d[0]][d[1]][d[2]][d[3]] < k){
            cout << "NO\n"; continue;
        }

        cout << s << ' ';
        while(cnt--){
            int flag = 0;
            for(int i = 0; i < l; i++)
                if(d[i] && p[i] < 0){
                    d[i]--;
                    x = dp[d[0]][d[1]][d[2]][d[3]];
                    if(k <= x){
                        s[i]--;
                        cout << s << ' ';
                        flag = 1; break;
                    }
                    else d[i]++, k -= x;
                }
            if(flag) continue;
            for(int i = l - 1; i >= 0; i--)
                if(d[i] && p[i] > 0){
                    d[i]--;
                    x = dp[d[0]][d[1]][d[2]][d[3]];
                    if(k <= x){
                        s[i]++;
                        cout << s << ' ';
                        break;
                    }
                    else d[i]++, k -= x;
                }
        }
        cout << '\n';
    }
    return 0;
}
#elif other2
// #include <bits/stdc++.h>
// #include <iostream>
// #include <unordered_map>


using namespace std;

unsigned long long l;
unsigned long long k;
unsigned long long th;
string st;
string ed;

unsigned long long solve(unordered_map<string, unsigned long long> &v, string &cur)
{
    if (cur == ed) {
        //th += 1;
        return 1;
    }

    if (v.find(cur) != v.end()) {
        //th += v[cur];
        return v[cur];
    }

    v[cur] = 0;
    string ocur = cur;
    if (ed[0] < cur[0]) {
        cur[0] -= 1;
        v[ocur] += solve(v, cur);
        cur[0] += 1;

    }
    if (ed[1] < cur[1]) {
        cur[1] -= 1;
        v[ocur] += solve(v, cur);
        cur[1] += 1;
    }
    if (l >= 3 && ed[2] < cur[2]) {
        cur[2] -= 1;
        v[ocur] += solve(v, cur);
        cur[2] += 1;
    }
    if (l >= 4 && ed[3] < cur[3]) {
        cur[3] -= 1;
        v[ocur] += solve(v, cur);
        cur[3] += 1;
    }
    if (l >= 4 && ed[3] > cur[3]) {
        cur[3] += 1;
        v[ocur] += solve(v, cur);
        cur[3] -= 1;
    }
    if (l >= 3 && ed[2] > cur[2]) {
        cur[2] += 1;
        v[ocur] += solve(v, cur);
        cur[2] -= 1;
    }
    if (ed[1] > cur[1]) {
        cur[1] += 1;
        v[ocur] += solve(v, cur);
        cur[1] -= 1;
    }
    if (ed[0] > cur[0]) {
        cur[0] += 1;
        v[ocur] += solve(v, cur);
        cur[0] -= 1;
    }

    //th += v[ocur];
    return v[ocur];
}

void k_find(unordered_map<string, unsigned long long> &v, string cur, unsigned long long th, vector<string> &cand)
{
    string ocur = cur;

    cand.push_back(cur);

    if (cur == ed) {
        return;
    }

    if (ed[0] < cur[0]) {
        cur[0] -= 1;
        if (th + v[cur] < k)
            th += v[cur];
        else {
            return k_find(v, cur, th, cand);
        }
        cur[0] += 1;
    }
    if (ed[1] < cur[1]) {
        cur[1] -= 1;
        if (th + v[cur] < k)
            th += v[cur];
        else {
            return k_find(v, cur, th, cand);
        }

        cur[1] += 1;
    }
    if (l >= 3 && ed[2] < cur[2]) {
        cur[2] -= 1;
        if (th + v[cur] < k)
            th += v[cur];
        else {
            return k_find(v, cur, th, cand);
        }

        cur[2] += 1;
    }
    if (l >= 4 && ed[3] < cur[3]) {
        cur[3] -= 1;
        if (th + v[cur] < k)
            th += v[cur];
        else {
            return k_find(v, cur, th, cand);
        }

        cur[3] += 1;
    }
    if (l >= 4 && ed[3] > cur[3]) {
        cur[3] += 1;
        if (th + v[cur] < k)
            th += v[cur];
        else {
            return k_find(v, cur, th, cand);
        }

        cur[3] -= 1;
    }
    if (l >= 3 && ed[2] > cur[2]) {
        cur[2] += 1;
        if (th + v[cur] < k)
            th += v[cur];
        else {
            return k_find(v, cur, th, cand);
        }

        cur[2] -= 1;
    }
    if (ed[1] > cur[1]) {
        cur[1] += 1;
        if (th + v[cur] < k)
            th += v[cur];
        else {
            return k_find(v, cur, th, cand);
        }

        cur[1] -= 1;
    }
    if (ed[0] > cur[0]) {
        cur[0] += 1;
        if (th + v[cur] < k)
            th += v[cur];
        else {
            return k_find(v, cur, th, cand);
        }

        cur[0] -= 1;
    }
}

int main(void)
{
    int t;

    cin >> t;


    for (int i = 0; i < t; i++) {
        cin >> l >> k >> st >> ed;
        unordered_map<string, unsigned long long> v;
        unsigned long long nr = solve(v, st);
        if (nr >= k) {
            vector<string> cand;
            k_find(v, st, 0, cand);
            cand.push_back(ed);
            for (int j = 0; j < cand.size() - 1; j++) {
                cout << cand[j] << " ";
            }

            cout << cand[cand.size() - 1] << endl;
        } else {
            cout << "NO" << endl;
        }
    }

    return 0;
}

#elif other3
import sys


def main() -> None:
    rd = sys.stdin.readline

    mem = {(0, 0, 0, 0) : 1}

    def dp(a, b, c=0, d=0):
        key = (a, b, c, d)
        if key not in mem:
            val = 0
            if a > 0:
                val += dp(a - 1, b, c, d)
            if b > 0:
                val += dp(a, b - 1, c, d)
            if c > 0:
                val += dp(a, b, c - 1, d)
            if d > 0:
                val += dp(a, b, c, d - 1)
            mem[key] = val
        return mem[key]

    for _ in range(int(rd())):
        L, K, x, y = rd().split()
        L, K, x, y = int(L), int(K), list(map(int, x)), list(map(int, y))
        v = []
        for l in range(L):
            v.append(
                ((1 if x[l] < y[l] else -1) * (10 ** (L - 1 - l)),
                 abs(x[l] - y[l]))
            )
        v.sort()
        op = [z[0] for z in v]
        v = [z[1] for z in v]

        if dp(*v) < K:
            print("NO")
        else:
            x = int(''.join(map(str, x)))
            ans = [x]
            while any(v):
                ptr = 0
                for i in range(len(v)):
                    if v[i] == 0:
                        continue
                    w = v.copy()
                    w[i] -= 1
                    diff = dp(*w)
                    if ptr + diff >= K:
                        v = w
                        K -= ptr
                        x += op[i]
                        ans.append(x)
                        break
                    ptr += diff
            print(*[str(num).rjust(L, '0') for num in ans])


if __name__ == "__main__":
    main()

#endif
}
