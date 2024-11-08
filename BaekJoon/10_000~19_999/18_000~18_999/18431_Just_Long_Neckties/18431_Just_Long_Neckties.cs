using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 8
이름 : 배성훈
내용 : Just Long Neckties
    문제번호 : 18431번

    정렬, 그리디 문제다.
    아이디어는 다음과 같다.
    먼저 이상함이 최소가 되려면 현재 입을 넥타이와 n개의 선택할 넥타이들을 정렬하고
    자기 순서에 맞는 넥타이를 고르면 이상함이 최소가 된다.

    만약 ai를 현재 입은 넥타이 중 i번째로 작은거, bi를 선택할 넥타이 중 i번째로 작은거라 하자
    그러면 a1 - b1, a2 - b2가 매칭되어야 하는데 a1 - b2, a2 - b1으로 매칭되었다면
    a2 - b1은 a2 - b2보다 작으므로 고려할 대상이 아니다.
    
    반면 a1 - b2는 대소관계와 a1 <= a2, b1 <= b2이므로 a1 - b1 <= a1 - b2이고 동시에 a2 - b2 <= a1 - b2가 성립한다.
    그래서 이상함이 증가한다.

    이에 정렬한 뒤 순서대로 매칭해주는게 최대가 된다.
    이제 정렬한 인덱스에 대해 상사가 i번째를 택하면 현재 사람들은 넥타이가 작은 순서대로 
    자신의 선택에서 가장 작은 넥타이를 골라가면 된다.

    해당 과정을 보면 왼쪽과 오른쪽으로 분할하면 반복됨을 알 수 있다.
    만약 상사가 i번쨰를 고르면 1 ~ i - 2 번째는 바로 앞에서 사용한 결과와 같다.
    오른쪽은 n + 1 ~ i 번째에서 i 경우만 뺀것과 같다.
    이는 누적하는 아이디어를 이용하면 매번 범위를 탐색하는게 아닌 1칸씩 이동하면서 찾을 수 있다.
    1 ~ i - 1번째의 최대값을 알고 잇을 때 1 ~ i 번째 최대값은 1 ~ i - 1의 최대값과 i번째 값을 비교하는 아이디어 말이다.

    이렇게 누적합을 이용해 찾으니 이상없이 통과한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1099
    {

        static void Main1099(string[] args)
        {

            int n;
            (int val, int idx)[] select;
            int[] cur, ret;

            Solve();
            void Solve()
            {

                Input();

                GetRet();

                Output();
            }

            void GetRet()
            {

                // 그리디로 i번째 넥타이를 택한 사람이 i번째로 넥타이를 선택하는게 이상함의 최소가 된다.
                Array.Sort(select, (x, y) => x.val.CompareTo(y.val));
                Array.Sort(cur);

                int[] left = new int[n];
                int[] right = new int[n];

                for (int i = 0; i < n; i++)
                {

                    // i번쨰 짧은 넥타이를 입은 사람이 i번째로 짧은 넥타이를 선택했을 때 이상함
                    if (cur[i] < select[i].val) left[i] = select[i].val - cur[i];

                    // i번째 짧은 넥타이를 입은 사람이 i + 1로 짧은 넥타이를 선택했을 때의 이상함
                    if (cur[i] < select[i + 1].val) right[i] = select[i + 1].val - cur[i];
                }

                for (int i = 1; i < n; i++)
                {
                    
                    // 왼쪽에 0 ~ i번째까지 이상함의 가장 큰 값을 찾아 저장
                    left[i] = Math.Max(left[i - 1], left[i]);
                    int j = n - 1 - i;
                    // 오른쪽에 i ~ n 번째까지 이상함의 가장 큰 값을 찾아 저장
                    right[j] = Math.Max(right[j], right[j + 1]);
                }

                for (int i = 0; i <= n; i++)
                {

                    // 상사가 선택하는 idx
                    int idx = select[i].idx;
                    // 왼쪽 이상함의 최대값
                    int l = i == 0 ? 0 : left[i - 1];
                    // 오른쪽 이상함의 최대값
                    int r = i == n ? 0 : right[i];

                    // 나머지 사원들의 이상함의 최대값
                    ret[idx] = Math.Max(l, r);
                }
            }

            void Output()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                for (int i = 0; i <= n; i++)
                {

                    sw.Write($"{ret[i]} ");
                }

                sw.Close();
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                select = new (int val, int idx)[n + 1];
                cur = new int[n];
                ret = new int[n + 1];
                for (int i = 0; i <= n; i++)
                {

                    select[i] = (ReadInt(), i);
                }

                for (int i = 0; i < n; i++)
                {

                    cur[i] = ReadInt();
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();

                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == -1) return true;

                        ret = c - '0';
                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }

#if other
// #include <iostream>
// #include <vector>
// #include <algorithm>
using namespace std;
typedef long long ll;

int N;
pair<int, int> P[200001];
ll B[200002];
ll L[200002], R[200002];
ll ans[200001];

int main() {
	ios::sync_with_stdio(0);
	cin.tie(0);

	cin >> N;
	for (int i = 0; i <= N; i++) cin >> P[i].first, P[i].second = i;
	sort(P, P + N + 1);

	for (int i = 1; i <= N; i++) cin >> B[i];
	sort(B + 1, B + N + 1);
	for (int i = 1; i <= N; i++) {
		L[i] = max((ll)P[i - 1].first - B[i], L[i - 1]);
	}
	for (int i = N; i > 0; i--) {
		R[i] = max((ll)P[i].first - B[i], R[i + 1]);
	}
	for (int i = 0; i <= N; i++) {
		ans[P[i].second] = max(L[i], R[i + 1]);
	}

	for (int i = 0; i <= N; i++) cout << ans[i] << ' ';
}
#endif
}
