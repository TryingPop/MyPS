using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 13
이름 : 배성훈
내용 : 영화 수집
    문제번호 : 3653번

    세그먼트 트리 문제다.
    자기보다 위쪽에 몇 개 있는지 확인해야 한다.
    처음에는 연결리스트로 접근하려고 했다.
    그래서 몇 개 진행해보니 앞에 있는 구간들에 값을 1씩 빼주어야 했다.
    구간 관리를 해야하니 세그먼트 트리로 되지 않을까 생각했다.
    단순히 빼기로는 아이디어가 떠오르지 않았고,
    고민하니 큰 값의 갯수를 세는 세그먼트트리를 잡고,
    따로 idx 배열을 둬서 idx보다 큰 것의 개수를 찾는 식으로 했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1183
    {

        static void Main1183(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int N = 100_000, M = 100_000;
            int S, E;
            int n, m;
            int[] idx, seg;
            int next;

            Solve();
            void Solve()
            {

                Init();

                int t = ReadInt();

                while (t-- > 0)
                {

                    Input();

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                for (int i = 0; i < m; i++)
                {

                    int num = ReadInt();
                    int cur = idx[num];
                    idx[num] = next;
                    int ret = GetVal(S, E, cur + 1, E);
                    sw.Write($"{ret} ");

                    SetVal(S, E, cur, -1);
                    SetVal(S, E, next++, 1);
                }
                sw.Write('\n');
                sw.Flush();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                idx = new int[N + 1];
                int len = 2 + (int)(Math.Log2(N + M + 1) - 1e-9);
                seg = new int[1 << len];
                S = 0;
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();

                next = n + 1;
                Array.Fill(seg, 0);
                E = n + m + 1;

                for (int i = 1; i <= n; i++)
                {

                    SetVal(S, E, i, 1);
                    idx[i] = n + 1 - i;
                }
            }

            int GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                if (_e < _chkS || _chkE < _s) return 0;
                else if (_chkS <= _s && _e <= _chkE) return seg[_idx];

                int mid = (_s + _e) >> 1;
                return GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1)
                    + GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);
            }

            void SetVal(int _s, int _e, int _chk, int _add, int _idx = 0)
            {

                if (_s == _e)
                {

                    seg[_idx] += _add;
                    return;
                }

                int mid = (_s + _e) >> 1;
                if (_chk <= mid) SetVal(_s, mid, _chk, _add, _idx * 2 + 1);
                else SetVal(mid + 1, _e, _chk, _add, _idx * 2 + 2);

                seg[_idx] = seg[_idx * 2 + 1] + seg[_idx * 2 + 2];
            }

            int ReadInt()
            {

                int ret = 0;

                while(TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
// #define ll long long
// #define pii pair<int,int>
// #define pll pair<ll,ll>

// #define N (1<<18)

int ft[N+1];
int query(int e){
    int ans=0;
    for(;e;e&=e-1)ans+=ft[e];
    return ans;
}
void update(int e, int delta){
    for(e--;e<N;e|=e+1)ft[e+1]+=delta;
}

int main(){
    ios::sync_with_stdio(0);
    cin.tie(0);

    int t;
    cin>>t;
    while(t--){
        int n,m;
        cin>>n>>m;
        int d[m];
        for(int i=0;i<m;i++)cin>>d[i];
        int pos[n+2];
        int npos=n+m+7;
        for(int i=n;i>0;i--){
            pos[i]=npos--;
            update(pos[i],1);
        }
        string ans;
        for(int i:d){
            ans+=" "+to_string(query(pos[i])-1);
            update(pos[i],-1);
            pos[i]=npos--;
            update(pos[i],1);
        }
        cout<<ans.substr(1)<<"\n";
        memset(ft,0,sizeof(int)*(N+1));
    }
}

#endif
}
