using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 27
이름 : 배성훈
내용 : 이상한 격자
    문제번호 : 32955번

    삼분 탐색으로 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1646
    {

        static void Main1646(string[] args)
        {

            int n;
            int a, b, c, d;
            int[] x, y;

            Input();

            GetRet();

            void GetRet()
            {

                long ret = TernarySearch(x, a, b) + TernarySearch(y, c, d);
                Console.Write(ret);

                long TernarySearch(int[] _arr, int _L, int _R)
                {

                    int l = 0;
                    int r = 2_000_000;
                    while (r - l >= 3)
                    {

                        int p = (l * 2 + r) / 3, q = (l + r * 2) / 3;
                        if (GetDis(p) <= GetDis(q))
                            r = q;
                        else
                            l = p;
                    }

                    long ret = 123_456_789_123_456_789;
                    for (int i = l; i <= r; i++)
                    {

                        ret = Math.Min(ret, GetDis(i));
                    }

                    return ret;

                    long GetDis(int _cen)
                    {

                        long ret = 0;
                        for (int i = 0; i < n; i++)
                        {

                            if (_cen > _arr[i])
                                ret += 1L * _R * (_cen - _arr[i]);
                            else
                                ret += 1L * _L * (_arr[i] - _cen);
                        }

                        return ret;
                    }
                }
            }

            void Input()
            {

                int ADD = 1_000_000;
                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                a = ReadInt();
                b = ReadInt();
                c = ReadInt();
                d = ReadInt();

                x = new int[n];
                y = new int[n];

                for (int i = 0; i < n; i++)
                {

                    x[i] = ReadInt() + ADD;
                    y[i] = ReadInt() + ADD;
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
                        bool positive = c != '-';

                        ret = positive ? c - '0' : 0;

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        ret = positive ? ret : -ret;
                        return false;
                    }
                }
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;
using ll=long long;

int main() {
	ios::sync_with_stdio(0);cin.tie(0);
	ll N; int L, R, U, D;
	cin>>N>>R>>L>>D>>U;
	auto go=[&](int* A, int a, int b) {
		ll sum=0;
		if(!a || !b) return sum;
		ll m=(b*N)/(a+b);
		nth_element(A, A+m, A+N);
		int x=A[m];
		for(ll i=0; i<m; i++) sum+=a*ll(x-A[i]);
		for(ll i=m+1; i<N; i++) sum+=b*ll(A[i]-x);
		return sum;
	};
	int X[N], Y[N];
	for(ll i=0; i<N; i++) cin>>X[i]>>Y[i];
	cout<<go(X, L, R)+go(Y, U, D)<<'\n';
}

#endif
}
