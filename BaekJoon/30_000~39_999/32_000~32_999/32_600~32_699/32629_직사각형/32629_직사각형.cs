using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 21
이름 : 배성훈
내용 : 직사각형
    문제번호 : 32629번

    수학 문제다.
    산술 기하 평균으로 x + y >= 2 sqrt(xy)이다.
    x와 y의 차이가 작은 경우 가장 작은 둘레임이 보장된다.
    그래서 w와 h의 합을 이분 탐색으로 찾아줬다.
*/

namespace BaekJoon.etc
{
    internal class etc_1782
    {

        static void Main1782(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = int.Parse(sr.ReadLine());

            while (t-- > 0)
            {

                int n = int.Parse(sr.ReadLine());
                int ret = BinarySearch(n);
                sw.Write($"{ret * 2}\n");
            }

            int BinarySearch(int _val)
            {

                int l = 1;
                int r = 63_246;

                while (l <= r)
                {

                    int mid = (l + r) >> 1;
                    int w = mid / 2;
                    int h = mid - w;

                    if (w * h < _val) l = mid + 1;
                    else r = mid - 1;
                }

                return l;
            }
        }
    }

#if other
// #include <iostream>
// #include <vector>
// #include <queue>
// #include <algorithm>
// #include <climits>
// #include <cmath>
// #include <string>
// #include <map>
// #include <set>
// #include <unordered_map>
// #define endl "\n"

using namespace std;
using ll = long long;

int main(void) {
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);
    cout.tie(NULL);
    cout << fixed; cout.precision(6);

    int T; cin >> T;
    while(T--) {
        ll x, sq;
        cin >> x;
        sq = sqrt(x);   
        cout << sq*4+(sq*sq==x?0:(sq*sq+sq<x?4:2)) << "\n";
    }

    return 0;
}
#endif
}
