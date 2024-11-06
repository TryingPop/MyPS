using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 4
이름 : 배성훈
내용 : 수열 만들기
    문제번호 : 19565번

    그래프 이론, 해 구성하기, 오일러 경로 문제다
    전체 경우의 수는 n^2 + 1로 쉽게 찾아졌다

    해 구성하는데 시간이 걸렸는데,
    숫자 하나씩 빼면 어떨까 가정을 세웠고,

        1 1 2 1 3 1 4 ... 1 n
        n 2 2 3 2 4 2 ... 2 n
        n 3 3 4 3 5 3 ... 3 n
        ...
        n n
        n 1

    위처럼 진행하니 ? 1 부분만 뒤에 빼면 이상없이 작동함을 알았다
    이렇게 제출하니 이상없이 통과했다

    문자열 포멧으로 출력하니 메모리도 많이 먹고,
    시간이 더 걸리는거 같아 stringbuilder를 이용해 제출하니
    15% 시간과 줄였다 70% 이상 메모리를 줄였다
*/

namespace BaekJoon.etc
{
    internal class etc_0861
    {

        static void Main861(string[] args)
        {

            StreamWriter sw;
            StringBuilder sb;

            int n;

            Solve();
            void Solve()
            {

                Read();

                sb = new(4_005);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                sw.Write($"{n * n + 1}\n");

                for (int i = 1; i <= n; i++)
                {

                    sb.Append(i);
                    sb.Append(' ');
                    for (int j = i + 1; j <= n; j++)
                    {

                        sb.Append(i);
                        sb.Append(' ');
                        sb.Append(j);
                        sb.Append(' ');
                    }

                    sw.Write(sb);
                    sb.Clear();
                    sw.Flush();
                }

                sw.Write(1);

                sw.Close();
            }

            void Read()
            {

                n = int.Parse(Console.ReadLine());
            }
        }
    }

#if other
// #include <iostream>
using namespace std;

int N;

int main() {
	ios::sync_with_stdio(0);
	cin.tie(0);

	cin >> N;

	if (N == 2) {
		cout << 5 << '\n';
		cout << 1 << ' ' << 1 << ' ' << 2 << ' ' << 2 << ' ' << 1;
		return 0;
	}
	
	cout << N * N + 1 << '\n';
	cout << 1;
	for (int i = 3; i < N; i++) cout << ' ' << i << ' ' << 1;
	for (int k = 2; k <= N; k++) {
		cout << ' ' << k;
		for (int i = k + 2; i <= N; i++) cout << ' ' << i << ' ' << k;
	}
	cout << ' ' << 1 << ' ' << 1;
	for (int k = N; k > 1; k--) cout << ' ' << k << ' ' << k;
	cout << ' ' << 1;
}
#endif
}
