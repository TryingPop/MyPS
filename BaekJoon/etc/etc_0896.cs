using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 21
이름 : 배성훈
내용 : PABEHCTBO
    문제번호 : 24309번

    수학, 사칙연산, 큰 수 연산 문제다
    곱셈을 직접 구현하던지 Numerics의 BigInteger를 이용하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0896
    {

        static void Main896(string[] args)
        {

            BigInteger a, b, c;

            Solve();
            void Solve()
            {

                Input();

                Console.Write((b - c) / a);
            }

            void Input()
            {

                a = BigInteger.Parse(Console.ReadLine());
                b = BigInteger.Parse(Console.ReadLine());
                c = BigInteger.Parse(Console.ReadLine());
            }
        }
    }

#if other
// #include <iostream>
// #include <string>
// #include <algorithm>
using namespace std;
typedef long long ll;
ll a; string b, c;
string bc;
string ans;

int main() {
	ios::sync_with_stdio(0);
	cin.tie(0);

	cin >> a >> b >> c;
	reverse(b.begin(), b.end());
	reverse(c.begin(), c.end());

	int len = b.length();
	while (c.length() < len) c += '0';
	bool carry = 0;
	
	for (int i = 0; i < len; i++) {
		int cur = (int)(b[i] - '0') - (int)(c[i] - '0');
		if (carry) cur--;
		if (cur < 0) cur += 10, carry = 1;
		else carry = 0;
		bc += (char)(cur + '0');
	}
	reverse(bc.begin(), bc.end());
	
	ll tmp = 0;
	for (int i = 0; i < len; i++) {
		tmp = tmp * 10 + (int)(bc[i] - '0');
		char nxt = (tmp / a) + '0'; tmp %= a;
		if (nxt == '0' && ans == "") continue;
		ans += nxt;
	}

	if (ans == "") cout << 0;
	else cout << ans;
}
#elif other2
using System.Numerics;

namespace BOJ;
class P24309
{
    static void Main() => new P24309().Solve();
    StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 102400);
    StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 102400);
    int[] dx = { 0, 0, -1, 1, -1, -1, 1, 1, 0 };
    int[] dy = { -1, 1, 0, 0, -1, 1, 1, -1, 0 };
    bool Step(int x, int y, int r, int c) 
        => x < 0 || x >= r || y < 0 || y >= c;
    string ReadLineUntil() 
    { 
    
        string s; 
        do 
        { 
        
            s = sr.ReadLine(); 
        } while (s.Length <= 0); 
        return s; 
    }

    string[] seps = { " ", "\t", };
    string[] ReadSplit() 
        => ReadLineUntil().Split(seps, StringSplitOptions.RemoveEmptyEntries);
    T[] ReadArray<T>(Func<string, T> f) 
        => ReadSplit().Select(f).ToArray();
    T Read1<T>(Func<string, T> f) 
        => f(ReadLineUntil());
    (T, T) Read2<T>(Func<string, T> f) 
    { 
        var s = ReadArray(f); 
        return (s[0], s[1]); 
    }
    
    (T, T, T) Read3<T>(Func<string, T> f) 
    { 
        
        var s = ReadArray(f); 
        return (s[0], s[1], s[2]); 
    }

    void Solve()
    {
        //ax=b-c
        //x=(b-c)/a
        var a = Read1(BigInteger.Parse);
        var b = Read1(BigInteger.Parse);
        var c = Read1(BigInteger.Parse);
        sw.WriteLine((b - c) / a);
        sw.Flush();
    }
}
#endif
}
