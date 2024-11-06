using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 17
이름 : 배성훈
내용 : 회의실 배정 2
    문제번호 : 19621번

    dp 문제다
    회의실 배정 4를 풀기 전에 경우의 수가 적은 회의실 배정 2를 먼저 풀었다
    범위가 25이다

    회의실 배정 3에서 써보니 시간 초과가 나서 해당 방법은 안좋은거 같다
    좌표 압축은 안해도될거 같다;
*/

namespace BaekJoon.etc
{
    internal class etc_0557
    {

        static void Main557(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            int n = ReadInt();
            (int s, int e, int cnt)[] arr = new (int s, int e, int cnt)[n];

            int[] dp = new int[n];
            Solve();
            sr.Close();

            void Solve()
            {

                for (int i = 0; i < n; i++)
                {

                    arr[i] = (ReadInt(), ReadInt(), ReadInt());
                }

                // Compact();

                for (int i = 0; i < n; i++)
                {

                    dp[i] = -1;
                }

                Array.Sort(arr, (x, y) => 
                {

                    int ret = x.e.CompareTo(y.e);
                    if (ret != 0) return ret;

                    return x.s.CompareTo(y.s);
                });

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    int calc = DFS(i);
                    ret = ret < calc ? calc : ret;
                }

                Console.WriteLine(ret);
            }

            int DFS(int _idx)
            {

                if (dp[_idx] != -1) return dp[_idx];
                dp[_idx] = 0;
                int end = arr[_idx].e;

                int ret = 0;
                for (int i = _idx + 1; i < n; i++)
                {

                    if (arr[i].s < end) continue;
                    int calc = DFS(i);
                    ret = calc < ret ? ret : calc;
                }

                dp[_idx] = ret + arr[_idx].cnt;
                return dp[_idx];
            }

            void Compact()
            {

                int[] calc = new int[2 * n];
                for (int i = 0; i < n; i++)
                {

                    calc[2 * i] = arr[i].s;
                    calc[2 * i + 1] = arr[i].e;
                }

                Array.Sort(calc);

                // 중복값 제거
                int len = RemoveSame(calc);

                // 이제 압축값 부여
                for (int i = 0; i < n; i++)
                {

                    arr[i].s = GetIdx(calc, len, arr[i].s);
                    arr[i].e = GetIdx(calc, len, arr[i].e);
                }
            }

            int GetIdx(int[] _arr, int _len, int _val)
            {

                int l = 0;
                int r = _len - 1;

                while (l <= r)
                {

                    int mid = (l + r) / 2;
                    if (_arr[mid] < _val) l = mid + 1;
                    else r = mid - 1;
                }

                return r + 1;
            }

            int RemoveSame(int[] _sortedArr)
            {

                int before = _sortedArr[0];
                int back = 0;
                for (int i = 1; i < _sortedArr.Length; i++)
                {

                    if (before == _sortedArr[i])
                    {

                        back++;
                        continue;
                    }

                    _sortedArr[i - back] = _sortedArr[i];
                    before = _sortedArr[i];
                }

                return _sortedArr.Length - back;
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
//19621 회의실 배정2
// #include <iostream>
// #include <vector>
// #include <algorithm>
using namespace std;
typedef long long ll;

typedef struct conference {
	ll start;
	ll end;
	int people;
}CONF;

int n,ans;
vector<CONF> conf;

void allocation(int cnt, int ending, int k) {
	if (k == n) {
		ans = max(ans, cnt);
		return;
	}

	if (conf[k].start >= ending) //k번째 회의 진행
		allocation(cnt + conf[k].people, conf[k].end, k+1);

	//k번째 회의 건너뛰기
	allocation(cnt, ending, k+1);
}


bool cmp(CONF a,CONF b) {
	return a.start < b.start;
}

int main() {
	ll start, end; int people;
	cin >> n;
	for (int i = 0; i < n; i++) {
		cin >> start >> end >> people;
		conf.push_back({ start,end,people });
	}
	sort(conf.begin(),conf.end(), cmp);
	allocation(0, 0, 0);
	cout << ans;
}
#endif
}
