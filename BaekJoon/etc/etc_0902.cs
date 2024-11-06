using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 23
이름 : 배성훈
내용 : 전구
    문제번호 : 2550번

    dp, 가장 긴 증가하는 부분수열(LIS) 문제다
    스위치 부분에서 위에 있는 순서대로 새로운 인덱스를 부여한다
    그리고 전구에 해당 인덱스로 바꾼 뒤 가장 긴 증가하는 부분수열의 길이는
    선이 엮이지 않고 가장 많이 전구를 켤 수 있는 경우와 일치한다

    스위치의 개수가 1만개라, N^2은 불안해
    이분탐색으로 찾아 n log n 시간으로 제출했다
*/

namespace BaekJoon.etc
{
    internal class etc_0902
    {

        static void Main902(string[] args)
        {

            StreamReader sr;
            int n, len, end;
            int[] newIdx, arr, oldIdx;
            int[] lis, before, ret;
            Solve();
            void Solve()
            {

                Input();

                LIS();

                GetRet();
            }

            void GetRet()
            {

                ret = new int[len];
                len = 0;
                while (end != 0)
                {

                    ret[len++] = oldIdx[end];
                    end = before[end];
                }

                Array.Sort(ret);

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    sw.Write($"{len}\n");
                    for (int i = 0; i < len; i++)
                    {

                        sw.Write($"{ret[i]} ");
                    }
                }
            }

            void LIS()
            {

                before = new int[n + 1];

                lis = new int[n];
                lis[0] = arr[1];
                len = 1;

                end = lis[0];
                before[lis[0]] = 0;
                for (int i = 2; i <= n; i++)
                {

                    int idx = BinarySearch(arr[i]);

                    lis[idx] = arr[i];
                    if (idx > 0) before[lis[idx]] = lis[idx - 1];

                    if (idx < len) continue;

                    len = idx + 1;
                    end = lis[idx];
                }
            }

            int BinarySearch(int _n)
            {

                int l = 0;
                int r = len - 1;

                while(l <= r)
                {

                    int mid = (l + r) >> 1;

                    if (lis[mid] < _n) l = mid + 1;
                    else r = mid - 1;
                }

                return l;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                newIdx = new int[n + 1];
                oldIdx = new int[n + 1];
                arr = new int[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    int idx = ReadInt();
                    newIdx[idx] = i;
                    oldIdx[i] = idx;
                }

                for (int i = 1; i <= n; i++)
                {

                    int idx = ReadInt();
                    arr[i] = newIdx[idx];
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
// #include <bits/stdc++.h>
// #define fastio cin.tie(0)->sync_with_stdio(0)
// #define MAX 10001

using namespace std;

int N;
int arr[MAX], arr1[MAX], arr2[MAX], arr3[MAX];

int main() {
    fastio;

    cin >> N;    
    for (int i = 1; i <= N; ++i) {
        cin >> arr[i];
        arr1[arr[i]] = i;
    }
    for (int i = 1; i <= N; ++i) {
        int n; cin >> n;
        arr2[i] = arr1[n];
    }
    int len = 0, idx[MAX];
    for (int i = 1; i <= N; ++i) {
        if (len == 0 || arr3[len - 1] < arr2[i]) {
            arr3[len++] = arr2[i];
            idx[i] = len;
            continue;
        }
        auto it = lower_bound(begin(arr3), begin(arr3) + len, arr2[i]);
        idx[i] = distance(begin(arr3), it) + 1;
        *it = arr2[i];
    }
    int tmp = len, ans[MAX];
    for (int i = N; i > 0 && tmp > 0; --i) {
        if (idx[i] == tmp) ans[tmp--] = arr[arr2[i]];
    }
    cout << len << '\n';
    sort(begin(ans) + 1, begin(ans) + 1 + len);
    for (int i = 1; i <= len; ++i) cout << ans[i] << ' ';
}
#endif
}
