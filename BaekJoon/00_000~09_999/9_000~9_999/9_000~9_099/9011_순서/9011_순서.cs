using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 30
이름 : 배성훈
내용 : 순서
    문제번호 : 9011번

    구현, 해구성하기 문제다
    우선 불가능한 경우는 다음과 같다
    i번째에 대해 자기보다 앞에 있는 작은 수의 개수는 
    i - 1 이하인 수여야한다 i - 1을 초과하면 impossible로 내렸다

    이제 가능한 경우 해 구현 아이디어는 그리디하게 접근했다
    자기보다 앞에 있는 작은 수의 개수가 a라하면
    맨 뒤에서부터 남아있는 수 중에서 a + 1 번째 수를 꺼냈다
    a + 1 번째 수를 찾기 위해서 앞에서부터 카운터 해서 찾아갔다( 탐색마다 O(N)의 시간이 걸린다 )
    그리고 해당 수를 기입하면 사용했다고 기록했다 총 O(N^2)의 시간이 걸린다
    해당 부분은 세그먼트 트리를 이용하면 (O (N log N) 으로 줄일 수 있다)

    이렇게 찾아서 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0398
    {

        static void Main398(string[] args)
        {

            string IMPO = "IMPOSSIBLE";
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt();
            int[] arr = new int[100];
            int[] order = new int[100];
            bool[] use = new bool[101];

            while(test-- > 0)
            {

                int len = ReadInt();

                bool impo = false;
                for (int i = 0; i < len; i++)
                {

                    // 입력 및 해가 존재하는지 판별
                    int cur = ReadInt();
                    arr[i] = cur;
                    if (i < cur) impo = true;
                }

                if (impo)
                {

                    // 불가능하면 불가능하다고 하고 바로 탈출
                    sw.WriteLine(IMPO);
                    continue;
                }

                for (int i = len - 1; i >= 0; i--)
                {

                    int cur = arr[i];
                    int chk = 0;
                    for (int j = 1; j <= len; j++)
                    {

                        if (use[j]) continue;
                        if (chk == cur)
                        {

                            use[j] = true;
                            order[i] = j;
                            break;
                        }

                        chk++;
                    }
                }

                for (int i = 0; i < len; i++)
                {

                    sw.Write(order[i]);
                    sw.Write(' ');

                    use[i + 1] = false;
                }

                sw.Write('\n');
            }

            sr.Close();
            sw.Close();

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
// #include<stdio.h>

int main()
{
	int T; scanf("%d", &T);


	while (T--) {
		int N, i, j; 
		int D[110], R[110];
		scanf("%d", &N);

		for (i = 0; i < N; i++) {
			scanf("%d", &R[i]);
		}

		for (i = 0; i < N; i++) {
			int x = R[i] + 1;
			D[i] = x;

			if (R[i] > i) break;
			for (j = 0; j < i; j++) {
				if (D[j] >= x) D[j]++;
			}
		}

		if (i < N) printf("IMPOSSIBLE\n");
		else {
			for (i = 0; i < N; i++) {
				printf("%d ", D[i]);
			}
			printf("\n");
		}

	}
	return 0;
}
#elif other2
// #include <cstdio>
// #include <cstring>
// #include <algorithm>
using namespace std;
int t, n,idx;
int arr[500];
int sol[110];
bool flag;
int main() {
	scanf("%d", &t);
	while (t--) {
		memset(arr, 0, sizeof(arr));
		flag = false;
		scanf("%d", &n);
		for (idx = 1; idx < n; idx <<= 1);
		for (int i = 0; i < n; i++) {
			scanf("%d", &sol[i]);
			if (sol[i] < 0) flag = true;
			arr[i + idx] = 1;
		}
		for (int i = idx - 1; i > 0; i--) arr[i] = arr[i * 2] + arr[i * 2 + 1];
		for (int i = n - 1; i >= 0; i--) {
			int temp = sol[i] + 1;
			int cidx = 1;
			while (cidx < idx) {
				cidx <<= 1;
				if (arr[cidx] < temp) 
					temp -= arr[cidx++];
			}
			if (arr[cidx] != 1||temp!=1) {
				flag = true;
				break;
			}
			sol[i] = cidx - idx + 1;
			for (; cidx; cidx >>= 1) arr[cidx] -= 1;
		}
		if (flag) {
			puts("IMPOSSIBLE");
			continue;
		}
		for (int i = 0; i < n; i++) printf("%d ", sol[i]);
		puts("");
	}
	return 0;
}
#endif
}
