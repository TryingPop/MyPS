using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 9
이름 : 배성훈
내용 : 도청 장치
    문제번호 : 9319번

    수학, 구현 문제다
    ... 도청장치 번호를 출력해야 한다;
    여태까지 갯수로 하다가 엄청나게 틀렸다;
    이후 이 부분을 수정하니 이상없이 통과했다;
*/

namespace BaekJoon.etc
{
    internal class etc_0371
    {

        static void Main371(string[] args)
        {

            string NOISE = "NOISE\n";

            StreamReader sr;
            StreamWriter sw;

            (int x, int y) lis;
            (int x, int y)[] pos;

            decimal[] r;
            decimal[] s;
            decimal sum;
            int n, b;

            Solve();

            void Solve()
            {

                Init();

                int test = ReadInt();

                while(test-- > 0)
                {

                    n = ReadInt();
                    b = ReadInt();

                    lis = (ReadInt(), ReadInt());

                    for (int i = 0; i < n; i++)
                    {

                        pos[i] = (ReadInt(), ReadInt());
                        s[i] = ReadInt();
                    }

                    SetR();

                    int ret = 0;
                    for (int i = 0; i < n; i++)
                    {

                        if (ChkR(i)) 
                        {

                            sw.Write($"{i + 1}\n");
                            ret++;
                            break;
                        }
                    }

                    if (ret == 0) sw.Write(NOISE);
                }

                sr.Close();
                sw.Close();
            }

            void SetR()
            {

                sum = 0;
                for (int i = 0; i < n; i++)
                {

                    r[i] = s[i] / (GetEuclidDis(ref pos[i]));
                    sum += r[i];
                }
            }

            bool ChkR(int _idx)
            {

                return r[_idx] > 6 * (b + sum - r[_idx]);
            }

            int GetEuclidDis(ref (int x, int y) _pos1)
            {

                int x = _pos1.x - lis.x;
                x *= x;

                int y = _pos1.y - lis.y;
                y *= y;

                return x + y;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                pos = new (int x, int y)[100_000];
                s = new decimal[100_000];
                r = new decimal[100_000];
            }


            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// #define MAX 100000
// #include<iostream>
// #include<algorithm>
// #include<vector>
using namespace std;

int t, n, B, x, y;
int xi[MAX + 5];
int yi[MAX + 5];
int si[MAX + 5];

double dist(const double& x1, const double& y1,
	const double& x2, const double& y2) {
	double x_diff = abs(x2 - x1);
	double y_diff = abs(y2 - y1);
	return x_diff*x_diff + y_diff * y_diff; //제곱한거
}

void solve() {
	ios_base::sync_with_stdio(false);
	cin.tie(NULL); cout.tie(NULL);
	cin >> n >> B;
	cin >> x >> y;
	for (int i = 1; i <= n; i++) {
		cin >> xi[i] >> yi[i] >> si[i];
	}

	double totalSum = 0;
	for (int j = 1; j <= n; j++) {
		totalSum += (si[j] / dist(x, y, xi[j], yi[j]));
	}
	double sum;
	for (int i = 1; i <= n; i++) {
		double ri = si[i] / dist(x, y, xi[i], yi[i]);
		double sum = totalSum - ri;
		if ( ri > 6 * (B + sum)) {
			cout << i << "\n";
			return;
		}
	}
	cout << "NOISE" << "\n";
	return;
}
int main() {
	ios_base::sync_with_stdio(false);
	cin.tie(NULL); cout.tie(NULL);

	cin >> t;
	while (t--) {
		solve();
	}
	return 0;
}
#endif
}
