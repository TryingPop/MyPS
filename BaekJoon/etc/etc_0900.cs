using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 22
이름 : 배성훈
내용 : 라면 사기 (Large)
    문제번호 : 18186번

    그리디 문제다
    앞의 방법을 이용하니 풀렸다..
    슬라이딩 윈도우가 더 느리다
*/

namespace BaekJoon.etc
{
    internal class etc_0900
    {

        static void Main900(string[] args)
        {

#if first

            StreamReader sr;

            long n, b, c;
            long[] arr;
            long ret;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                ret = 0;
                if (b <= c)
                {

                    for (int i = 0; i < n; i++)
                    {

                        ret += arr[i] * b;
                    }
                }
                else
                {

                    for (int i = 0; i < n; i++)
                    {

                        long cnt;
                        if (arr[i + 1] > arr[i + 2])
                        {

                            cnt = Math.Min(arr[i], arr[i + 1] - arr[i + 2]);
                            ret += Buy(i, 2, cnt);
                        }

                        cnt = Math.Min(arr[i], arr[i + 1]);
                        ret += Buy(i, 3, cnt);
                        ret += Buy(i, 1, arr[i]);
                    }
                }

                Console.Write(ret);
            }

            long Buy(int _s, int _set, long _cnt)
            {

                for (int i = 0; i < _set; i++)
                {

                    arr[_s + i] -= _cnt;
                }

                return (b + c * (_set - 1)) * _cnt;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                b = ReadInt();
                c = ReadInt();

                arr = new long[n + 2];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
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
#endif
            StreamReader sr;

            long n, b, c;
            int n1, n2, n3;
            long ret;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                ret = 0;
                if (b <= c)
                {

                    for (int i = 0; i < n; i++)
                    {

                        ret += ReadInt() * b;
                    }
                }
                else
                {

                    n1 = 0;
                    n2 = 0;
                    n3 = 0;

                    for (int i = 0; i < n; i++)
                    {

                        Calc(ReadInt());
                    }

                    Calc();
                    Calc();
                }

                sr.Close();
                Console.Write(ret);
            }

            void Calc(int _n = 0)
            {

                Read(_n);

                if (n1 == 0) return;

                int cnt;
                if (n2 > n3)
                {

                    cnt = Math.Min(n1, n2 - n3);
                    ret += Buy2(cnt);
                }

                cnt = Math.Min(n1, n2);
                ret += Buy3(cnt);
                ret += Buy1(n1);
            }

            void Read(int _n = 0)
            {

                n1 = n2;
                n2 = n3;
                n3 = _n;
            }

            long Buy3(int _cnt)
            {

                n1 -= _cnt;
                n2 -= _cnt;
                n3 -= _cnt;

                return (b + 2 * c) * _cnt;
            }

            long Buy2(int _cnt)
            {

                n1 -= _cnt;
                n2 -= _cnt;

                return (b + c) * _cnt;
            }
            
            long Buy1(int _cnt)
            {

                n1 -= _cnt;

                return b * _cnt;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                b = ReadInt();
                c = ReadInt();
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
using System.Linq;

class Program
{
    static void Main()
    {
        //bj18186 /d4 /라면 사기 (Large) /240502
        //240429의 응용문제, 달라진 건 거의 없고, 추가로 구매할 때 드는 비용만 생각하면 끝
        //1000000

        //n은 공장의 수, b는 한 개를 구매할 때의 가격, c는 추가 구매할 때의 가격
        int n, b, c;
        //answer는 최소 비용을 계산하여 저장
        long answer = 0;

        //입력: 첫 줄에는 공장의 수 n과 가격 b, c를 입력받는다. 
        var inputs = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        n = inputs[0]; //공장의 개수
        b = inputs[1]; //개별가
        c = inputs[2]; //추가가격
        //ㄴ라면을 살 수 있는 공장의 개수가 (1 < n)이라면 라면의 개가격은 개별가+추가가격*(n-1)이다.
        //공장의 개수, 아래 연산에서 +2까지 접근하고, 인덱스 1부터 시작하기에 +3
        var v = new long[n + 3];

        //입력: 두 번째 줄에서 각 공장별 라면의 수를 입력받아 배열에 저장한다. 
        var ramens = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        for (int i = 0; i < n; i++) v[i + 1] = ramens[i];

        //연산
        //c가 b보다 작지 않다면 모든 라면을 개별적으로 구매하는 게 더 유리하다.
        if (b <= c)
        {
            for (int i = 1; i <= n; i++) answer += v[i] * b;
            Console.WriteLine(answer); //정답 출력 후
            return; // 프로그램을 조기 종료합니다.
        }
        //그렇지 않다면, 패키지 구매를 최대한 활용해야 유리하다. (낱개로 사지 않고 묶음으로 사기)
        for (int i = 1; i <= n; i++)
        {
            //만약 살 수 있는 라면이 없다면 Min연산에 의해 0을 곱하기 때문에 신경쓰지 않아도 됨 
            //2개 패키지가 3개 패키지보다 많은 라면을 필요로 할 때
            if (v[i + 1] > v[i + 2])
            {
                //2개 패키지 구매를 먼저 계산
                long count = Math.Min(v[i], v[i + 1] - v[i + 2]); //살 수 있는 개수
                answer += (b + c) * count; //2개 패키지 비용을 적용한다
                v[i] -= count;
                v[i + 1] -= count;

                //이후 3개 패키지 구매를 계산
                count = Math.Min(v[i], Math.Min(v[i + 1], v[i + 2]));
                answer += (b + 2 * c) * count; //3개 패키지 비용을 적용한다
                v[i] -= count;
                v[i + 1] -= count;
                v[i + 2] -= count;
            }
            else //반대의 경우
            {
                //3개 패키지 구매가 우선적으로 가능
                long count = Math.Min(v[i], Math.Min(v[i + 1], v[i + 2]));
                answer += (b + 2 * c) * count;
                v[i] -= count;
                v[i + 1] -= count;
                v[i + 2] -= count;

                //남은 라면은 2개 패키지로 구매
                count = Math.Min(v[i], v[i + 1]);
                answer += (b + c) * count;
                v[i] -= count;
                v[i + 1] -= count;
            }
            //마지막으로 남은 라면은 개별적으로 구매
            answer += b * v[i];
        }
        //최종 계산된 최소 비용을 출력
        Console.WriteLine(answer);
    }
}
#elif other2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;
using System.Data;


namespace Algorithm
{
    class Program
    {
        static void Main()
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            StringBuilder sb = new StringBuilder();

            string[] input2 = sr.ReadLine().Split();
            long n = long.Parse(input2[0]);
            long b = long.Parse(input2[1]);
            long c = long.Parse(input2[2]);
            
            string[] input = sr.ReadLine().Split();
            List<long> ramen = Array.ConvertAll(input, long.Parse).ToList();
            if(c >= b)
            {
                Console.WriteLine((long)ramen.Sum() * b);
                return;
            }
            BigInteger res = 0;
            ramen.Add(0);
            ramen.Add(0);
            for(int i = 0; i < n; i++)
            {
                if(ramen[i] == 0)
                    continue;
                
                if(ramen[i+1] > ramen[i+2])
                {
                    
                    long min = Math.Min(ramen[i], (ramen[i+1]-ramen[i+2]));
                    ramen[i]-= min;
                    ramen[i+1]-=min;
                    res+= min* (b+c);

                    min = Math.Min(ramen[i], ramen[i+2]);
                    ramen[i]-= min;
                    ramen[i+1]-=min;
                    ramen[i+2]-=min;
                    res+= min * (b+c+c);
                }
                else
                {
                    long min = Math.Min(ramen[i+1], ramen[i]);
                    ramen[i]-= min;
                    ramen[i+1]-=min;
                    ramen[i+2]-=min;
                    res+= min * (b+c+c);
                }
                res+= ramen[i]*b;

            }
            sb.Append(res);
            sw.WriteLine(sb);

            sr.Close();
            sw.Close(); 
        }

    }

}
#elif other3
// #include <stdio.h>
// #define rep(i, a, b) for(int i = a; i < (b); ++i)
typedef long long ll;

static inline char gc() { // like getchar()
	static char buf[1 << 18];
	static size_t bc, be;
	if (bc >= be) {
		buf[0] = 0, bc = 0;
		be = fread_unlocked(buf, 1, sizeof(buf), stdin);
	}
	return buf[bc++]; // returns 0 on EOF
}

int readInt() {
	int a, c;
	while ((a = gc()) < 40);
	// if (a == '-') return -readInt();
	while ((c = gc()) >= 48) a = a * 10 + c - 480;
	return a - 48;
}

int main() {
    int n;
    ll b, c;
    n = readInt();
    b = readInt();
    c = readInt();
    if (c > b) c = b;
    ll res = 0;
    int one = 0;
    int two = 0;
    int x;
    rep(i, 0, n) {
        x = readInt();
        int use_one = one < x ? one : x;
        x -= use_one;
        int use_two = two < x ? two : x;
        x -= use_two;
        res += c * (use_one + use_two) + b * x;
        one = x;
        two = use_one;
    }
    printf("%lld\n", res);
    return 0;
}
#elif other4
// #include <bits/stdc++.h>
using namespace std;
// #define fastio cin.tie(0)->ios::sync_with_stdio(0); cout.tie(0); setvbuf(stdout, nullptr, _IOFBF, BUFSIZ);
#pragma warning(disable:4996)

using ll = long long;
int arr[1000003];

// 1. 라면사는 순서를 바꿔도 결과는 똑같다. 그러니까 맨 앞부분부터 사도 괜찮다. 
// 2. b <= c 의 경우 낱개로 사면 되는데 이 경우 Trivial 하다.
// 3. i 번째 라면가게까지 최적으로 사서 0으로 만들었다 하자. 
//    i번째부터 세가게 x1, x2, x3 가 있을 때
//		x2 < x1 이면 x1 은 (x1-x2) 만큼은 혼자서 처리해야한다. 그러면 x1 <= x2 로 미리 만들어 아래 조건에 맞추자.
//		x3 <= x1 <= x2 식이라면 x1 은 (x1-x3) 만큼은 혼자서 처리해야한다. 이왕하는거 x2 도 같이 처리하자..
//		x1 <= x3 <= x2 식이라면 x2 의 (x2-x3) 만큼은 혼자서 처리해야한다. 이왕 하는거 x1 이랑 같이 처리해주자.
//		x1 <= x2 <= x3 에서는 Greedy 전략이 통한다.
//      

// Fast Input
inline char ReadChar() {
	static char readbuffer[200024];
	static int buf_p, buf_l;

	if (buf_p == buf_l) {
		buf_l = (int)fread(readbuffer, 1, 200024, stdin);
		buf_p = 0;
	}
	return readbuffer[buf_p++];
}

inline void ReadInt(int& r) {
	r = 0;
	bool bMinus = false;
	char now = ReadChar();
	while (!(now >= '0' && now <= '9') && now != '-')
		now = ReadChar();

	if (now == '-') {
		bMinus = true;
		now = ReadChar();
	}

	while (now != '\n' && now != ' ') {
		r *= 10;
		r += now - '0';
		now = ReadChar();
	}
	if (bMinus) r *= -1;
}
//

int main()
{
	fastio;

	int n; ReadInt(n);
	int b, c; ReadInt(b); ReadInt(c);
	ll ans = 0;
	for (int i = 0; i < n; i++) ReadInt(arr[i]);

	if (b <= c) {
		for (int i = 0; i < n; i++) ans += arr[i];
		ans *= b;
	}
	else {
		ll bc = b + c, bcc = bc + c;
		for (int i = 0; i < n; i++)
		{
			if (arr[i] == 0) continue;
			if (arr[i] > arr[i + 1])
			{
				ll d = arr[i] - arr[i + 1];
				arr[i] -= d;
				ans += d * b;
			}
			if (arr[i] > arr[i + 2])
			{
				ll d = arr[i] - arr[i + 2];
				arr[i] -= d; arr[i + 1] -= d;
				ans += d * bc;
			}
			if (arr[i + 1] > arr[i + 2])
			{
				ll d = min(arr[i + 1] - arr[i + 2], arr[i]);
				arr[i] -= d; arr[i + 1] -= d;
				ans += d * bc;
			}
			if (arr[i + 1] <= arr[i + 2])
			{
				ll d = min(arr[i], min(arr[i + 1], arr[i + 1]));
				arr[i] -= d; arr[i + 1] -= d; arr[i + 2] -= d;
				ans += d * bcc;
			}
		}
	}
	cout << ans;
}
#endif
}
