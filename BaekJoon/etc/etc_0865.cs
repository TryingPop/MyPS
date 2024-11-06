using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 6
이름 : 배성훈
내용 : 시철이가 사랑한 GCD
    문제번호 : 21870번

    수학, 브루트포스, 정수론, 분할 정복, 재귀, 유클리드 호제법 문제다
    분할 정복으로 풀지만 나머지 연산을 세그먼트 트리를 이용하면 되지 않을까 생각했다
    구간마다 GCD 연산은 많아야 3개를 넘지 않는다 다만 처음에 n번 gcd 연산을 한다

    그래서 해당 방법으로 제출하니 204ms에 통과했고,
    다른 사람의 풀이를 보니 더 효율적인 방법이 있었다

    반환을 gcd와 결과를 같이 반환하면 N번 gcd연산으로 끝난다
    해당 방법으로 푸니 72ms로 2배 이상 빠르다
*/

namespace BaekJoon.etc
{
    internal class etc_0865
    {

        static void Main865(string[] args)
        {

            StreamReader sr;

            int n;

#if first

            int[] seg;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            int DNC(int _s, int _n)
            {

                if (_n == 1) return GetVal(0, n - 1, _s, _s);
                int mid = _n / 2;

                int chk1 = GetVal(0, n - 1, _s, _s + mid - 1) + DNC(_s + mid, _n - mid);
                int chk2 = GetVal(0, n - 1, _s + mid, _s + _n - 1) + DNC(_s, mid);

                return chk1 < chk2 ? chk2 : chk1;
            }

            void GetRet()
            {

                int ret = DNC(0, n);

                Console.Write(ret);
            }

            int GetGCD(int _a, int _b)
            {

                while(_b > 0)
                {

                    int temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            int GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                if (_chkS <= _s && _e <= _chkE) return seg[_idx];

                int mid = (_s + _e) >> 1;

                if (mid < _chkS) return GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);
                else if (_chkE <= mid) return GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1);
                else return GetGCD(GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1), 
                    GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2));
            }

            void Update(int _s, int _e, int _chk, int _val, int _idx = 0)
            {

                if (_s == _e)
                {

                    seg[_idx] = _val;
                    return;
                }

                int mid = (_s + _e) >> 1;
                if (mid < _chk) Update(mid + 1, _e, _chk, _val, _idx * 2 + 2);
                else Update(_s, mid, _chk, _val, _idx * 2 + 1);

                seg[_idx] = GetGCD(seg[_idx * 2 + 1], seg[_idx * 2 + 2]);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                int log = (int)Math.Ceiling(Math.Log2(n)) + 1;
                seg = new int[1 << log];

                Array.Fill(seg, 1);
                for (int i = 0; i < n; i++)
                {

                    int val = ReadInt();
                    Update(0, n - 1, i, val);
                }

                sr.Close();
            }
#else

            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
            }

            int GetGCD(int _a, int _b)
            {

                while (_b > 0) 
                {

                    int temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            void GetRet()
            {

                int ret = DNC(0, n).ret;

                Console.Write(ret);
            }

            (int gcd, int ret) DNC(int _s, int _len)
            {

                if (_len == 1) return (arr[_s], arr[_s]);

                int mid = _len / 2;
                (int gcd, int ret) l = DNC(_s, mid);
                (int gcd, int ret) r = DNC(_s + mid, _len - mid);

                return (GetGCD(l.gcd, r.gcd), Math.Max(l.ret + r.gcd, r.ret + l.gcd));
            }
#endif
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
// #include <bits/stdc++.h>
using namespace std;

int arr[200001];

// gcd, sum
pair<int,int> go(int l, int r) {
    if (l == r)
        return make_pair(arr[l], arr[l]);
    int mid = l + ((r - l + 1) >> 1) - 1;
    auto lp = go(l, mid);
    auto rp = go(mid+1, r);
    return make_pair(gcd(lp.first, rp.first), max(lp.first + rp.second, rp.first + lp.second));
}

int main() {
    ios_base::sync_with_stdio(false);
    cin.tie(0); cout.tie(0);
    int n; cin >> n;
    for (int i = 0; i < n; i++) cin >> arr[i];
    cout << go(0, n-1).second << "\n";
    return 0;
}
#endif
}
