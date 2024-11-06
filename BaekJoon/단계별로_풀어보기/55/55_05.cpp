/*
날짜 : 2024. 6. 18
이름 : 배성훈
내용 : 큰 수 곱셈 (2)
	문제번호 : 15576번

	수학, 고속 푸리에 변환 문제다

	2자리? 혹은 3자리 이상으로 끊어 읽으면 성능향상이될거 같다
	그리고, FFT가 아닌 NTT로 바꿔도 빠르다고 한다
*/

#include <complex>
#include <vector>
#include <cmath>
#include <iostream>
#include <algorithm>

#define MAX_LEN 300'000

using namespace std;

typedef complex<double> cpx;

const double PI = 3.14159265358979;

void FillArr(string& _str, vector<cpx>& _v)
{

	reverse(_str.begin(), _str.end());

	int len = _str.length();
	_v.reserve(len);
	

	for (const char c: _str)
	{

		_v.push_back(c - '0');
	}
}

void FFT(vector<cpx>& _v, bool _inv)
{

	int n = _v.size();

	for (int i = 1, j = 0; i < n; i++)
	{

		int bit = (n >> 1);

		while (!((j ^= bit) & bit))
		{

			bit >>= 1;
		}

		if (i < j) swap(_v[i], _v[j]);
	}

	for (int i = 1; i < n; i <<= 1)
	{

		double x = _inv ? PI / i : -PI / i;

		cpx w(cos(x), sin(x));

		for (int j = 0; j < n; j += (i << 1))
		{

			cpx p(1, 0);

			for (int k = 0; k < i; k++)
			{

				cpx temp = _v[i + j + k] * p;
				_v[i + j + k] = _v[j + k] - temp;
				_v[j + k] += temp;
				p *= w;
			}
		}
	}

	if (_inv)
	{

		for (int i = 0; i < n; i++)
		{

			_v[i] /= n;
		}
	}
}

void Multiple(vector<cpx>& _f, vector<cpx>& _b, vector<cpx>& _ret)
{

	int newSize = 2;

	while (newSize < _f.size() + _b.size())
	{

		newSize <<= 1;
	}

	_f.resize(newSize);
	_b.resize(newSize);

	FFT(_f, 0);
	FFT(_b, 0);

	for (int i = 0; i < _ret.size(); i++)
	{

		_ret[i] = _f[i] * _b[i];
	}

	FFT(_ret, 1);
}

void GetRet(string& _ret, vector<cpx>& _v)
{

	int max = 0;
	int add = 0;

	for (int i = 0; i < _v.size(); i++)
	{

		int val = round(_v[i].real());
		if (val <= 0) continue;

		max = i;
		if (val < 10) continue;

		_v[i + 1] += val / 10;
		val %= 10;
		_v[i] = val;
	}

	_ret.clear();
	for (int i = max; i >= 0; i--)
	{

		int val = round(_v[i].real());
		_ret.push_back(val + '0');
	}
}

int main(void)
{

	ios_base::sync_with_stdio(false);
	cin.tie(nullptr);
	cout.tie(nullptr);

	string str;
	str.reserve(MAX_LEN * 2);

	vector<cpx> f, b;
	cin >> str;
	FillArr(str, f);

	cin >> str;
	FillArr(str, b);

	Multiple(f, b, f);

	GetRet(str, f);
	cout << str;
}

#if other
#include <bits/stdc++.h>

using namespace std;

using ll = long long;
using cd = complex<double>;

const double pi = acos(-1);

void fft(vector<cd>& a, bool inv = false) {
	int n = a.size();
	for (int k = 0; k < n; ++k) {
		int b{};
		for (int z = 1; z < n; z *= 2) {
			b *= 2;
			if (k & z) {
				++b;
			}
		}
		if (k < b) {
			swap(a[k], a[b]);
		}
	}
	static vector<cd> r, ir;
	if (r.empty()) {
		r.resize(n / 2);
		ir.resize(n / 2);
		for (int i = 0; i < r.size(); ++i) {
			r[i] = cd(cos(2 * pi / n * i), sin(2 * pi / n * i));
			ir[i] = conj(r[i]);
		}
	}
	for (int m = 2; m <= n; m *= 2) {
		for (int k = 0; k < n; k += m) {
			for (int j = 0; j < m / 2; ++j) {
				cd u = a[k + j];
				cd t = a[k + j + m / 2] * (inv ? ir[n / m * j] : r[n / m * j]);
				a[k + j] = u + t;
				a[k + j + m / 2] = u - t;
			}
		}
	}
	if (inv) {
		for (int i = 0; i < n; ++i) {
			a[i] /= n;
		}
	}
}

int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	vector<cd> f(1 << 17);
	vector<cd> g(1 << 17);
	string s;
	cin >> s;
	for (int i = 0, j = 0; i < s.size(); ++i) {
		int k = s.size() - i - 1;
		j = 10 * j + s[i] - '0';
		if (k % 5 == 0) {
			f[k / 5] = j;
			j = 0;
		}
	}
	cin >> s;
	for (int i = 0, j = 0; i < s.size(); ++i) {
		int k = s.size() - i - 1;
		j = 10 * j + s[i] - '0';
		if (k % 5 == 0) {
			g[k / 5] = j;
			j = 0;
		}
	}
	fft(f);
	fft(g);
	for (int i = 0; i < f.size(); ++i) {
		f[i] *= g[i];
	}
	fft(f, true);
	vector<ll> A(f.size());
	for (int i = 0; i < f.size() - 1; ++i) {
		A[i] += llround(f[i].real());
		A[i + 1] += A[i] / 100000;
		A[i] %= 100000;
	}
	bool flag{};
	for (int i = A.size() - 1; i >= 0; --i) {
		if (flag) {
			cout << setw(5) << setfill('0') << A[i];
		}
		else if (A[i] > 0 || i == 0) {
			cout << A[i];
			flag = true;
		}
	}
	cout << "\n";
	return 0;
}

#endif