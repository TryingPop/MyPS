using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 9
이름 : 배성훈
내용 : 조개 줍기
    문제번호 : 14870번

    dp, 세그먼트 트리 문제다.

    코드는 다음 사이트를 참고했다.
    https://justicehui.github.io/koi/2019/07/13/BOJ14870/

    풀이는 다음 사이트에 잘 수록되어져 있다.
    https://daisylum.tistory.com/26

    다른 참고한 코드인데, 세그먼트 트리 부분에서 시간초과 날거 같았다.
    https://stonejjun.tistory.com/101

    초기 접근한 사이트
    https://blog.naver.com/PostView.naver?blogId=pasdfq&logNo=222242568352&parentCategoryNo=&categoryNo=7&viewDate=&isShowPopularPosts=false&from=postList

    해당 좌표의 값이 변화할 때, 보드에 어떻게 영향을 끼치는지 확인하는게 중요한 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1463
    {

        static void Main1463(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n;
            int[][] arr;
            // arr의 조건에 맞는 누적합 dp
            long[][] dp;

            // 팬윅 트리 = 바이너리 인덱스 트리다.
            // seg[i][j] 는 i는 board의 i행을 뜻한다.
            // j는 바이너리 인덱스 트리의 인덱스를 뜻한다.
            long[][] seg;

            // 쿼리에 쓰이는 dp이다.
            int[] s, e;
            long sum;

            Input();

            SetDp();

            SetSeg();

            GetRet();

            void GetRet()
            {

                s = new int[n + 1];
                e = new int[n + 1];
                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                sw.Write($"{sum}\n");

                int U = 'U' - '0';
                for (int i = 0; i < n; i++)
                {

                    int op = ReadInt();
                    int f = ReadInt();
                    int t = ReadInt();

                    Query(f, t, op == U ? 1 : -1);
                    sw.Write($"{sum}\n");
                }
            }

            void Query(int _f, int _t, int _val)
            {

                // 변경된 곳을 의미
                // f행의 시작지점
                s[_f] = _t;
                // f행의 끝지점
                e[_f] = _t;

                for (int i = _f + 1; i <= n; i++)
                {

                    s[i] = n + 1;
                    e[i] = 0;
                }

                for (int i = _f, j = _t; ;)
                {

                    // 확장되는 끝지점
                    if (j < n
                        // 열이 확장 가능한지 확인
                        && Math.Max(GetBoard(i - 1, j + 1), GetBoard(i, j)) + _val
                        == Math.Max(GetBoard(i - 1, j + 1), GetBoard(i, j) + _val)) j++;
                    // 열이 안되면 행이 되는지 확인한다.
                    else i++;

                    if (i > n) break;
                    // 끝 부분 갱신
                    e[i] = j;
                }

                for (int i = _f, j = _t; ;)
                {

                    // 시작지점을 찾는다.
                    if (i < n
                        && Math.Max(GetBoard(i + 1, j - 1), GetBoard(i, j)) + _val
                        == Math.Max(GetBoard(i + 1, j - 1), GetBoard(i, j) + _val)) i++;
                    else j++;

                    if (j > n || e[i] < j) break;
                    s[i] = Math.Min(s[i], j);
                }

                // 이제 영향을 주는 행과 열에 변형 시작
                for (int i = _f; i <= n; i++)
                {

                    // 영향 없으면 넘긴다.
                    if (s[i] > e[i]) continue;

                    Update(i, s[i], e[i], _val);
                    sum += _val * (e[i] - s[i] + 1);
                }
            }

            long GetBoard(int _i, int _j)
            {

                // 변경된 누적합 찾기
                return dp[_i][_j] + GetVal(_i, _j);
            }

            void Update(int _idx, int _l, int _r, int _val)
            {

                // 누적함 아이디어다.
                // l을 담당하는 모든 구간에 val를 추가
                for (; _l <= n; _l += _l & -_l)
                {

                    seg[_idx][_l] += _val;
                }

                // r + 1을 담당하는 모든 구간에 val를 뺀다.
                for (_r++; _r <= n; _r += _r & -_r)
                {

                    seg[_idx][_r] -= _val;
                }
            }

            long GetVal(int _idx, int _chk)
            {

                long ret = 0;

                for(;_chk > 0; _chk ^= _chk & -_chk)
                {

                    ret += seg[_idx][_chk];
                }

                return ret;
            }

            void SetSeg()
            {

                seg = new long[n + 1][];
                seg[0] = new long[n + 1];
                for (int i = 0; i <= n; i++)
                {

                    seg[i] = new long[n + 1];
                }
            }

            void SetDp()
            {

                sum = 0;
                dp = new long[n + 1][];
                dp[0] = new long[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    dp[i] = new long[n + 1];
                    for (int j = 1; j <= n; j++)
                    {

                        dp[i][j] = Math.Max(dp[i - 1][j], dp[i][j - 1]) + arr[i][j];
                        sum += dp[i][j];
                    }
                }
            }

            void Input()
            {

                n = ReadInt();
                arr = new int[n + 1][];
                arr[0] = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = new int[n + 1];
                    for (int j = 1; j <= n; j++)
                    {

                        arr[i][j] = ReadInt();
                    }
                }
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }

#if other
// #include <iostream>

using namespace std;

int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);

    int n; cin >> n;
    int arr[n + 1][n + 1]{};
    for(int i = 1; i <= n; ++i) for(int j = 1; j <= n; ++j) cin >> arr[i][j];
    long long ans = 0;
    for(int i = 1; i <= n; ++i) for(int j = 1; j <= n; ++j) ans += arr[i][j] += max(arr[i - 1][j], arr[i][j - 1]);
    for(int i = 0; i < n; ++i) for(int j = 1; j <= n; ++j) arr[i][j] -= arr[i + 1][j - 1];
    cout << ans << '\n';
    for(int cnt = 0; cnt < n; ++cnt)
    {
        char c; int x, y; cin >> c >> x >> y;
        int del = (c == 'U' ? 1 : -1);
        int s = y, e = y + 1, t = x + y;
        while(s != e)
        {
            arr[t - s][s] += del;
            if(t - e <= n && e <= n) arr[t - e][e] -= del;
            ans += del * (e - s);

            ++t;
            if(del == 1)
            {
                if(t - s <= n && arr[t - s - 1][s] > 0) s = s;
                else s = s + 1;
                if(e <= n && arr[t - e - 1][e] < 0) e = e + 1;
                else e = e;
            }
            else
            {
                if(t - s <= n && arr[t - s - 1][s] >= 0) s = s;
                else s = s + 1;
                if(e <= n && arr[t - e - 1][e] <= 0) e = e + 1;
                else e = e;
            }
        }
        cout << ans << '\n';
    }
}

#endif
}
