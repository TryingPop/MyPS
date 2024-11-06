using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 3
이름 : 배성훈
내용 : 가장 작은 직사각형
    문제번호 : 1438번

    브루트포스, 기하학 문제다
    두 포인터알고리즘과 정렬을 이용해
    N^3 log N 시간으로 풀었다

    아이디어는 다음과 같다
    y를 고정시키고 포함되는 x들을 찾는다
    y 설정하는데 N^2의 시간이 걸린다

    y에 따른 해당 x를 정렬한 뒤 x의 개수가 
    전체의 절반이 안되면 y를 재설정한다
    전체의 절반이 되는 경우면 x를 정렬한다
    그리고 두 포인터로 절반이 되는 최소 길이를 찾는다
    x 정렬에 N log N이 쓰이고, 두 포인터는 N에 해결된다

    그래서 총 O((N^3) log N)의 시간이 걸린다
*/

namespace BaekJoon.etc
{
    internal class etc_0937
    {

        static void Main937(string[] args)
        {

            int INF = 1_000_000_000;
            StreamReader sr;
            int n;
            (int x, int y)[] rects;
            int[] x, y;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(y);

                int ret = INF;
                int chk = n / 2;
                // y 값을 고정
                for (int b = 0; b < n; b++)
                {

                    for (int t = b; t < n; t++)
                    {

                        int top = y[t] + 1;
                        int bot = y[b] - 1;

                        int len = 0;

                        // 범위에 해당하는 x를 모두 저장
                        for (int i = 0; i < n; i++)
                        {

                            if (rects[i].y <= bot || top <= rects[i].y) continue;
                            x[len++] = rects[i].x;
                        }

                        if (len < chk) continue;

                        // x 정렬하고 두 포인터로 최소 사각형 찾는다
                        Array.Sort(x, 0, len);

                        // 길이 chk 보정
                        int l = 0;
                        int r = chk - 1;

                        int h = top - bot;

                        while(r < len)
                        {

                            int cnt = r - l + 1;
                            if (cnt < chk)
                            {

                                r++;
                                continue;
                            }

                            ret = Math.Min(ret, h * (x[r] - x[l] + 2));
                            l++;
                        }
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                rects = new (int x, int y)[n];
                x = new int[n];
                y = new int[n];

                for (int i = 0; i < n; i++)
                {

                    rects[i] = (ReadInt(), ReadInt());
                    y[i] = rects[i].y;
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
// #include <cstdio>
// #include <algorithm>
using namespace std;

int n, x[101] = { -1 }, y[101] = { -1 }, szx = 1, szy = 1, s[101][101], r = 1e9;
pair<int, int> p[100];
int main() {
    scanf("%d", &n);
    for (int i = 0; i < n; i++) {
        scanf("%d%d", &p[i].first, &p[i].second);
        x[szx++] = p[i].first;
        y[szy++] = p[i].second;
    }
    sort(x, x + szx);
    sort(y, y + szy);
    szx = unique(x, x + szx) - x;
    szy = unique(y, y + szy) - y;
    for (int i = 0; i < n; i++) {
        int tx = lower_bound(x, x + szx, p[i].first) - x, ty = lower_bound(y, y + szy, p[i].second) - y;
        s[tx][ty]++;
    }
    for (int i = 1; i < szx; i++) {
        for (int j = 1; j < szy; j++) s[i][j] += s[i - 1][j] + s[i][j - 1] - s[i - 1][j - 1];
    }
    for (int i = 1; i < szx; i++) {
        for (int j = 1; j < szy; j++) {
            int k = 0, l = j;
            while (l >= 0) {
                if (s[i][j] - s[k][j] - s[i][l] + s[k][l] < n / 2) l--;
                else {
                    r = min(r, (x[i] - x[k + 1] + 2) * (y[j] - y[l + 1] + 2));
                    k++;
                }
            }
        }
    }
    printf("%d", r);
    return 0;
}
#endif
}
