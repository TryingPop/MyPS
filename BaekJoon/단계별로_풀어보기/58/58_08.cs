using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 25
이름 : 배성훈
내용 : 파일 합치기 2
    문제번호 : 13974번

    크눅스 최적화, dp 문제다
    이전에 풀었던
    크눅스 최적화로 안풀린다;
    
    찾아보니 Garsia-Wachs 알고리즘을 써야한다;
    https://tistory.joonhyung.xyz/15
    다른 사람 코드를 참고해 제출했다;

    크눅스 알고리즘은 원래라면 중간에 길이만큼 비교해야하나
    이전 길이가 다음 길이에 영향을 미치니 이부분을 저장하고 
    다음 연산에 사용해 시간 복잡도를 줄이는 아이디어다
*/

namespace BaekJoon._58
{
    internal class _58_08
    {

        static void Main8(string[] args)
        {

#if first

            int MAX_SIZE = 5_000;
            StreamReader sr;
            StreamWriter sw;

            int n;
            int[] arr;
            int ret;

            Solve();
            void Solve()
            {

                Init();

                int test = ReadInt();

                while(test-- > 0)
                {

                    Input();

                    GetRet();
                }

                sw.Close();
                sr.Close();
            }

            void Input()
            {

                n = ReadInt();
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                }
            }

            int Find(int _n)
            {

                for (int i = 1; i < _n - 1; i++)
                {

                    if (arr[i] <= arr[i + 2]) return i;
                }

                return _n - 1;
            }

            void Delete(int _n, int _idx)
            {

                for (int i = _idx; i < _n; i++)
                {

                    arr[i] = arr[i + 1];
                }
            }

            void Swap(int _idx1, int _idx2)
            {

                arr[_idx1] ^= arr[_idx2];
                arr[_idx2] ^= arr[_idx1];
                arr[_idx1] ^= arr[_idx2];
            }

            void Left(int _idx)
            {

                while(_idx > 1)
                {

                    if (arr[_idx] <= arr[_idx - 1]) break;
                    Swap(_idx - 1, _idx);
                    _idx--;
                }
            }

            void GetRet()
            {

                ret = 0;

                while (n > 1)
                {

                    int idx = Find(n);

                    arr[idx] += arr[idx + 1];

                    ret += arr[idx];
                    Delete(n, idx + 1);
                    Left(idx);

                    n--;
                }

                sw.Write($"{ret}\n");
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                arr = new int[MAX_SIZE + 1];
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c= sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
#else
            StreamReader sr;
            StreamWriter sw;

            int n;
            int[] sum;
            int[][] dp;
            int[][] knuth;

            Solve();
            void Solve()
            {

                Init();

                int test = ReadInt();

                while (test-- > 0)
                {

                    Input();

                    KnuthOpt();

                    sw.Write($"{dp[0][n]}\n");
                }

                sw.Close();
                sr.Close();
            }

            void Input()
            {

                n = ReadInt();
                for (int i = 1; i <= n; i++)
                {

                    sum[i] = sum[i - 1] + ReadInt();
                }
            }

            void KnuthOpt()
            {

                int INF = 100_000_000;
                for (int i = 1; i <= n; i++)
                {

                    dp[i - 1][i] = 0;
                    knuth[i - 1][i] = i;
                }

                for (int length = 2; length <= n; length++)
                {

                    for (int start = 0; length + start <= n; start++)
                    {

                        int end = length + start;
                        dp[start][end] = INF;
                        for (int mid = knuth[start][end - 1]; mid <= knuth[start + 1][end]; mid++)
                        {

                            int now = dp[start][mid] + dp[mid][end] + sum[end] - sum[start];
                            if (dp[start][end] > now)
                            {

                                dp[start][end] = now;
                                knuth[start][end] = mid;
                            }
                        }
                    }
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                sum = new int[5_001];

                dp = new int[5_001][];
                knuth = new int[5_001][];

                for (int i = 0; i <= 5_000; i++)
                {

                    dp[i] = new int[5_001];
                    knuth[i] = new int[5_001];
                }
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
#endif
        }
    }

#if other
// #include <cstdio>

const int SIZE = 504;
int arr[SIZE];

int find(int N) {
	for (int i = 1; i < N - 1; i++) {
		if (arr[i] <= arr[i + 2]) return i;
	}

	return N - 1;
}

void del(int N, int idx) {
	for (int i = idx; i < N; i++) {
		arr[i] = arr[i + 1];
	}
}

void swap(int& a, int& b) {
	a ^= b;
	b ^= a;
	a ^= b;
}

void left(int idx) {
	while (idx > 1) {
		if (arr[idx - 1] >= arr[idx]) break;
		swap(arr[idx - 1], arr[idx]);
		--idx;
	}
}

int solve(int N) {
	int ret = 0;
	while (N > 1) {
		int idx = find(N);
		arr[idx] += arr[idx + 1];

		ret += arr[idx];
		del(N, idx + 1);
		left(idx);

		--N;
	}
	return ret;
}

int main()
{
	int T;
	scanf("%d", &T);
	while (T--) {
		int N;
		scanf("%d", &N);
		for (int i = 1; i <= N; i++) scanf("%d", &arr[i]);
		printf("%d\n", solve(N));
	}
}
#endif
}
