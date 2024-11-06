using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 22
이름 : 배성훈
내용 : 2연산
    문제번호 : 14399번

    정수론, 애드혹 문제다
    아이디어는 다음과 같다
    연산을 보면 유클리드 호제법에서 몫의 값과 같다
    다만 초기에 1의 값이 담겨 있으므로 1인 경우는 1개 빼주는 연산을 해야한다
    마지막에 X + Y 연산을 하기에 사전순으로 앞서는 X를 추가해준다
    그리고 값이 1인 경우는 연산을 진행할 필요가 없기에 따로 반례처리했다

    BOJ 17431번 게임 문제를 풀기 전에 로직을 알기 위해 먼저 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0988
    {

        static void Main988(string[] args)
        {

            int n, arrLen, strLen;  // 입력값 숫자, arr의 길이와, ret의 길이
            char[] ret, temp;       // 결과출력용과 최소값 찾기 위한 배열
            int[] arr;              // gcd(a, n - a) = 1인 a를 모아놓은 배열

            Solve();
            void Solve()
            {

                Input();

                SetArr();

                GetRet();
            }

            void GetRet()
            {

                if (n == 1) return;
                ret = new char[strLen];
                temp = new char[strLen];
                Array.Fill(ret, 'Y');

                for (int i = 0; i < arrLen; i++)
                {

                    int b = arr[i];
                    int a = n - b;
                    int idx = 0;

                    while (a != 1 || b != 1)
                    {

                        if (a > b)
                        {

                            a -= b;
                            temp[idx++] = 'X';
                        }
                        else
                        {

                            b -= a;
                            temp[idx++] = 'Y';
                        }
                    }

                    if (TempBigStr()) continue;
                    Copy();
                }

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    for (int i = 0; i < strLen; i++)
                    {

                        sw.Write(ret[i]);
                    }

                    sw.Write('X');
                }
            }

            void Copy()
            {

                for (int i = 0; i < strLen; i++)
                {

                    ret[i] = temp[i];
                }
            }

            bool TempBigStr()
            {

                // 두 문자열 사전식 비교
                // temp가 크면 true 아니면 false
                for (int i = 0; i < strLen; i++)
                {

                    if (ret[i] == temp[i]) continue;
                    return ret[i] < temp[i];
                }

                return true;
            }

            void SetArr()
            {

                arrLen = 0;
                strLen = n;
                int e = n >> 1;

                for (int b = 1; b <= e; b++)
                {

                    int a = n - b;
                    int q;
                    int gcd = GetGCD(a, b, out q);
                    if (gcd != 1) continue;

                    if (q > strLen) continue;
                    else if (q < strLen)
                    {

                        strLen = q;
                        arrLen = 1;
                        arr[0] = b;
                    }
                    else arr[arrLen++] = b;
                }
            }

            void Input()
            {

                n = int.Parse(Console.ReadLine());
                arr = new int[(n >> 1) + 1];
            }

            int GetGCD(int _a, int _b, out int _q)
            {

                // q는 덧셈연산 횟수다
                // 초기에 1이 담겨 있으므로
                // 1인 경우는 1을 빼준다
                _q = 0;
                while (_b > 0)
                {

                    int temp = _a % _b;
                    _q += _a / _b;
                    if (_b == 1) _q--;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }
        }
    }

#if other
// #include<iostream>
// #include<string>
using namespace std;
bool compare(string a,string b){
    if(a=="0") return 1;
    return a>b;
}
int gcdl(int r1,int r2){
    int l=0;
    if(r1<r2) swap(r1,r2);
    while(r2){
        l+=r1/r2;
        r1%=r2;
        swap(r1,r2);
    }
    if(r1>1) return 1000001;
    return l;
}
int main(void){
    ios_base :: sync_with_stdio(false);
    cin.tie(NULL);
    int n,minl=1000000;
    string str,Min="0";
    cin>>n;
    if(n==1){
        cout<<"";
        return 0;
    }
    for(int i=1;i<=n/2;i++){
        if(gcdl(i,n)<minl) minl=gcdl(i,n);
    }
    for(int i=1;i<=n/2;i++){
        if(gcdl(i,n)!=minl) continue;
        str.clear();
        int x=n,y=i;
        while(x!=1 || y!=1){
            if(x>=y){
                x-=y;
                str+="X";
            }
            else{
                y-=x;
                str+="Y";
            }
        }
        int l=str.length();
        for(int i=0;i<=(l-1)/2;i++){
            swap(str[i],str[l-1-i]);
        }
        if(str[0]=='Y'){
            for(int i=0;i<l-1;i++){
                if(str[i]=='X') str[i]='Y';
                else str[i]='X';
            }
        }
        str[l-1]='X';
        if(compare(Min,str)) Min=str;
    }
    cout<<Min;
    return 0;
}
#elif other2
// #include <stdio.h>
// #include <vector>
// #include <queue>
// #include <algorithm>
// #include <iostream>
// #include <string>
// #include <bitset>
// #include <map>
// #include <set>
// #include <tuple>
// #include <string.h>
// #include <math.h>
// #include <random>
// #include <functional>
// #include <assert.h>
// #include <math.h>
// #define all(x) (x).begin(), (x).end()
// #define xx first
// #define yy second

using namespace std;

template<typename T, typename Pr = less<T>>
using pq = priority_queue<T, vector<T>, Pr>;
using i64 = long long int;
using ii = pair<int, int>;
using ii64 = pair<i64, i64>;

int check(int n, int a, int cut)
{
	int res = 0;

	while (n != 1 || a != 1)
	{
		if (a == 0 || (n != 1 && n == a))
			return 987654321; // 불가능한 케이스

		// n < a 가 될 때까지 감소
		int reduce = (n - 1) / a;
		res += reduce;
		if (res > cut)
			return res;

		n -= reduce * a;
		swap(a, n);
	}

	return res;
}

string seq(int n, int a)
{
	string res;

	char c = 'X';

	while (n != 1 || a != 1)
	{
		if (n > a)
		{
			n -= a;
			res.push_back(c);
		}
		else
		{
			swap(n, a);
			if (c == 'X')
				c = 'Y';
			else
				c = 'X';
		}
	}

	reverse(all(res));

	string changed = res;
	for (auto& c : changed)
	{
		if (c == 'X')
			c = 'Y';
		else
			c = 'X';
	}

	return min(changed, res);
}

int main()
{
	ios::sync_with_stdio(false);
	cin.tie(nullptr);

	int n;
	cin >> n;

	int ans = n - 1;
	string s = seq(n, 1);

	for (int a = 2; a < n; a++)
	{
		int c = check(n, a, ans);

		if (c < ans)
		{
			s = seq(n, a);
			ans = c;
		}
		else if (c == ans)
		{
			s = min(s, seq(n, a));
		}
	}

	cout << s << '\n';

	return 0;
}
#endif
}
