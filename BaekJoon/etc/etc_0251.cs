using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 16
이름 : 배성훈
내용 : 응애 (Easy)
    문제번호 : 28088번

    구현, 시뮬레이션, 비트마스킹 문제다
    100만 * 200이라 불안하긴 했는데,
    상황대로 구현하니 300ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0251
    {

        static void Main251(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);
            int r = ReadInt(sr);

            int[] state = new int[n];
            Array.Fill(state, 1);
            for (int i = 0; i < m; i++)
            {

                int idx = ReadInt(sr);
                state[idx] = -1;
            }

            sr.Close();
            int[] next = new int[n];
            while(r-- > 0)
            {

                next[0] = state[1] * state[n - 1];
                next[n - 1] = state[0] * state[n - 2];
                for (int i = 1; i < n - 1; i++)
                {

                    next[i] = state[i - 1] * state[i + 1];
                }

                var temp = state;
                state = next;
                next = temp;
            }

            int ret = 0;
            for (int i = 0; i < n; i++)
            {

                if (state[i] == -1) ret++;
            }

            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
// #include <bits/stdc++.h>
// using namespace std;

// #define ll long long

int N, M;
ll K;

int count(int N, ll K, vector<int> v) {
    int ret = 0;
    for (ll k = 1; k <= K; k <<= 1) {
        if (K & k) {
            vector<int> nv = v;
            for (int n = 0; n < N; n++) {
                if (v[n]) nv[(n+k*2)%N] ^= 1;
            }
            v = nv;
        }
    }
    for (int n = 0; n < N; n++) {
        if (v[n]) ret++;
    }
    return ret;
}

int main() {
    cin.tie(0)->sync_with_stdio(0);
    cin >> N >> M >> K;
    vector<int> ans(N, 0);
    for (int m = 0; m < M; m++) {
        int x; cin >> x;
        ans[x] = 1;
    }
    cout << count(N, K, ans);
}
#elif other2
var l = Array.ConvertAll(Console.ReadLine().Split(), Int32.Parse);
var q = new Queue<int>();
var s = new HashSet<int>();
int num;
for (int i = 0; i < l[1]; i++, s.Add(Int32.Parse(Console.ReadLine())));
for (int i = 0; i < l[2]; i++) {
    if (s.Count == 0) break;
    foreach(var n in s)
    {
        q.Enqueue((l[0] + (n - 1)) % l[0]);
        q.Enqueue((n + 1) % l[0]);
    }
    s.Clear();
    while(q.Count != 0)
    {
        num = q.Dequeue();
        if (s.Contains(num)) s.Remove(num);
        else s.Add(num);
    }
}

Console.WriteLine(s.Count);
#endif
}
