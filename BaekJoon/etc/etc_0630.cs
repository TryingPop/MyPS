using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 26
이름 : 배성훈
내용 : 목장 건설하기
    문제번호 : 14925번

    dp, 누적합 문제다
    아이디어는 다음과 같다
    정사각형을 확인할 때 -, | 순서로 탐색하기에
    해당 지점과 확장할 3방향 -, |, \ 방향의 부분을 확장 할 수 있는지 확인하고 
    가장 작은 값 + 1을 담는다
    그러면, 정사각형이 확장될 때, 확장값이 담긴다
*/

namespace BaekJoon.etc
{
    internal class etc_0630
    {

        static void Main630(string[] args)
        {

            StreamReader sr;
            int[][] board;
            int[][] dp;
            int row, col;

            Solve();

            void Solve()
            {

                Input();

                int[] calc = new int[col];
                int ret = 0;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] != 0) calc[c] = 0;
                        else calc[c] = 1;

                        ret = Math.Max(calc[c], ret);
                    }

                    var temp = calc;
                    calc = dp[r];
                    dp[r] = temp;
                }

                for (int r = 1; r < row; r++)
                {

                    for (int c = 1; c < col; c++)
                    {

                        if (board[r][c] != 0) continue;
                        int cur = Math.Min(dp[r - 1][c], dp[r][c - 1]);
                        cur = Math.Min(cur, dp[r - 1][c - 1]);
                        cur++;

                        dp[r][c] = cur;
                        ret = Math.Max(ret, cur);
                    }
                }

                Console.WriteLine(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                dp = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    dp[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = ReadInt();
                    }
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
        }
    }

#if other
//
//  BOJ
//  ver.C++
//
//  Created by GGlifer
//
//  Open Source

// #include <iostream>
// #include <cstring>
// #include <unistd.h>

using namespace std;

// #define endl '\n'

// Set up : Global Variables
/* None */

// Set up : Functions Declaration
/* None */


int main()
{
    // Set up : I/O
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    // Set up : Input
    int M, N;
    cin >> M >> N;
    char A[M+1][N+1];
    for (int i=1; i<=M; i++)
        for (int j=1; j<=N; j++)
            cin >> A[i][j];

    // Process
    int dp[M+1][N+1];
    memset(dp, 0, sizeof(dp));

    int L = 0;
    for (int i=1; i<=M; i++) {
        for (int j=1; j<=N; j++) {
            if (A[i][j] == '0') {
                dp[i][j] = min({dp[i-1][j], dp[i-1][j-1], dp[i][j-1]})+1;
                L = max(L, dp[i][j]);
            }
        }
    }

    // Control : Output
    cout << L << endl;
}

// Helper Functions
/* None */

#elif other2
// #include <bits/stdc++.h>
// #include <sys/stat.h>
// #include <sys/mman.h>
using namespace std;

/////////////////////////////////////////////////////////////////////////////////////////////
/*
 * Author : jinhan814
 * Date : 2021-03-22
 * Description : FastIO implementation for cin, cout. (mmap ver.)
 */
const int INPUT_SZ = 2000000;
const int OUTPUT_SZ = 1 << 12;

class INPUT {
private:
	char* p;
	bool __END_FLAG__, __GETLINE_FLAG__;
public:
	explicit operator bool() { return !__END_FLAG__; }
    INPUT() { p = (char*)mmap(0, INPUT_SZ, PROT_READ, MAP_SHARED, 0, 0); }
	bool is_blank(char c) { return c == ' ' || c == '\n'; }
	bool is_end(char c) { return c == '\0'; }
	char _readChar() { return *p++; }
	char readChar() {
		char ret = _readChar();
		while (is_blank(ret)) ret = _readChar();
		return ret;
	}
	template<typename T>
	T _readInt() {
		T ret = 0;
		char cur = _readChar();
		bool flag = 0;
		while (is_blank(cur)) cur = _readChar();
		if (cur == '-') flag = 1, cur = _readChar();
		while (!is_blank(cur) && !is_end(cur)) ret = 10 * ret + cur - '0', cur = _readChar();
		if (is_end(cur)) __END_FLAG__ = 1;
		return flag ? -ret : ret;
	}
	int readInt() { return _readInt<int>(); }
	long long readLL() { return _readInt<long long>(); }
	string readString() {
		string ret;
		char cur = _readChar();
		while (is_blank(cur)) cur = _readChar();
		while (!is_blank(cur) && !is_end(cur)) ret.push_back(cur), cur = _readChar();
		if (is_end(cur)) __END_FLAG__ = 1;
		return ret;
	}
	double readDouble() {
		string ret = readString();
		return stod(ret);
	}
	string getline() {
		string ret;
		char cur = _readChar();
		while (cur != '\n' && !is_end(cur)) ret.push_back(cur), cur = _readChar();
        if (__GETLINE_FLAG__) __END_FLAG__ = 1;
		if (is_end(cur)) __GETLINE_FLAG__ = 1;
		return ret;
	}
	friend INPUT& getline(INPUT& in, string& s) { s = in.getline(); return in; }
} _in;

class OUTPUT {
private:
	char write_buf[OUTPUT_SZ];
	int write_idx;
public:
	~OUTPUT() { bflush(); }
	template<typename T>
	int getSize(T n) {
		int ret = 1;
		if (n < 0) n = -n;
		while (n >= 10) ret++, n /= 10;
		return ret;
	}
	void bflush() {
		fwrite(write_buf, sizeof(char), write_idx, stdout);
		write_idx = 0;
	}
	void writeChar(char c) {
		if (write_idx == OUTPUT_SZ) bflush();
		write_buf[write_idx++] = c;
	}
	void newLine() { writeChar('\n'); }
	template<typename T>
	void _writeInt(T n) {
		int sz = getSize(n);
		if (write_idx + sz >= OUTPUT_SZ) bflush();
		if (n < 0) write_buf[write_idx++] = '-', n = -n;
		for (int i = sz - 1; i >= 0; i--) write_buf[write_idx + i] = n % 10 + '0', n /= 10;
		write_idx += sz;
	}
	void writeInt(int n) { _writeInt<int>(n); }
	void writeLL(long long n) { _writeInt<long long>(n); }
	void writeString(string s) { for (auto& c : s) writeChar(c); }
	void writeDouble(double d) { writeString(to_string(d)); }
} _out;

/* operators */
INPUT& operator>> (INPUT& in, char& i) { i = in.readChar(); return in; }
INPUT& operator>> (INPUT& in, int& i) { i = in.readInt(); return in; }
INPUT& operator>> (INPUT& in, long long& i) { i = in.readLL(); return in; }
INPUT& operator>> (INPUT& in, string& i) { i = in.readString(); return in; }
INPUT& operator>> (INPUT& in, double& i) { i = in.readDouble(); return in; }

OUTPUT& operator<< (OUTPUT& out, char i) { out.writeChar(i); return out; }
OUTPUT& operator<< (OUTPUT& out, int i) { out.writeInt(i); return out; }
OUTPUT& operator<< (OUTPUT& out, long long i) { out.writeLL(i); return out; }
OUTPUT& operator<< (OUTPUT& out, size_t i) { out.writeInt(i); return out; }
OUTPUT& operator<< (OUTPUT& out, string i) { out.writeString(i); return out; }
OUTPUT& operator<< (OUTPUT& out, double i) { out.writeDouble(i); return out; }

/* macros */
// #define fastio 1
// #define cin _in
// #define cout _out
// #define istream INPUT
// #define ostream OUTPUT
/////////////////////////////////////////////////////////////////////////////////////////////

int n, m, mx, dp[1001][1001];

int main() {
	fastio;
	cin >> n >> m;
	for (int i = 1; i <= n; i++) for (int j = 1; j <= m; j++) {
		int t; cin >> t;
		if (t) continue;
		dp[i][j] = min({ dp[i - 1][j - 1], dp[i - 1][j], dp[i][j - 1] }) + 1;
		mx = max(mx, dp[i][j]);
	}
	cout << mx << '\n';
}
#elif other3
#endif
}
