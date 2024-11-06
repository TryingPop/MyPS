using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 5
이름 : 배성훈
내용 : Yunny's Trip
    문제번호 : 31885번

    자료구조, 애드 혹, 해시 문제다
    아이템은 많아야 2번 사용할 수 있다
    아이템의 최대 범위가 20만이므로 완전탐색 O(N^2)으로는 해결이 안된다

    그래서 해시를 이용했다
    x + y = z를 찾아야 하는 것이니
    y = z - x이 성립한다

    그래서 1개를 확인하면서 z - x 를 모두 저장했다
    이후에 그리고 y인 해당원소가 있는식으로 확인했다

    해설을 보니 이분탐색도 가능하다던데,
    이게 왜 되지 의문이 든다
*/

namespace BaekJoon.etc
{
    internal class etc_0796
    {

        static void Main796(string[] args)
        {

            StreamReader sr;

            int n, k;
            (long x, long y)[] items;
            (long x, long y) goal;
            
            Solve();
            void Solve()
            {

                Input();

                long ret = GetRet();

                Console.Write(ret);
            }

            long GetRet()
            {

                HashSet<(long x, long y)> goals = new(n);
                HashSet<(long x, long y)> meet = new(4 * n);

                // 0번
                (long x, long y) zero = (0, 0);
                long chk = GoalTaxiDis(ref zero);
                long ret = -1;
                if (chk <= k) ret = chk;

                // 1번
                for (int i = 0; i < n; i++)
                {

                    chk = GoalTaxiDis(ref items[i]) + 2;
                    if (chk <= k && (ret == -1 || chk < ret)) ret = chk;

                    long x = goal.x - items[i].x;
                    long y = goal.y - items[i].y;
                    goals.Add((x, y));
                    meet.Add((x - 1, y));
                    meet.Add((x + 1, y));
                    meet.Add((x, y - 1));
                    meet.Add((x, y + 1));
                }

                // 2번
                for (int i = 0; i < n; i++)
                {

                    if (goals.Contains(items[i]))
                    {

                        chk = 4;
                        if (chk <= k && (ret == -1 || chk < ret)) ret = chk;
                    }
                    
                    if (meet.Contains(items[i]))
                    {

                        chk = 5;
                        if (chk <= k && ret == -1) ret = chk;
                    }
                }

                return ret;
            }

            long GoalTaxiDis(ref (long x, long y) _pos)
            {

                long ret = Math.Abs(_pos.x - goal.x) + Math.Abs(_pos.y - goal.y);
                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();

                items = new (long x, long y)[n];
                for (int i = 0; i < n; i++)
                {

                    items[i] = (ReadLong(), ReadLong());
                }

                goal = (ReadLong(), ReadLong());

                sr.Close();
            }

            long ReadLong()
            {

                int c = sr.Read();
                bool plus = c != '-';

                long ret = plus ? c - '0' : 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
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
// #include <iostream>
// #include <vector>
// #include <algorithm>
using namespace std;

int main(){
    ios::sync_with_stdio(false), cin.tie(NULL), cout.tie(NULL);
    constexpr auto dist{[](const pair<long long, long long> a, const pair<long long, long long> b) -> long long {
        long long dx = b.first - a.first, dy = b.second - a.second;
        return (dx < 0? -dx : dx) + (dy < 0? -dy : dy);
    }};
    constexpr pair<int, int> dir[4]{{-1, 0}, {0, 1}, {1, 0}, {0, -1}};
    int N;
    long long K;
    cin >> N >> K;
    long long ans = 6;
    vector<pair<long long, long long>> arr(N);
    for(auto& [dx, dy] : arr)
        cin >> dx >> dy;
    long long ex, ey;
    cin >> ex >> ey;
    pair<long long, long long> e{ex, ey};
    if(dist(pair<long long, long long>{0, 0}, e) <= K)
        ans = dist(pair<long long, long long>{0, 0}, e);
    for(pair<long long, long long> v : arr){
        long long d = dist(v, e);
        if(d > K - 2)
            continue;
        ans = min(ans, 2LL + d);
    }
    sort(arr.begin(), arr.end());
    if(K >= 4){
        for(auto [vx, vy] : arr){
            if(binary_search(arr.begin(), arr.end(), pair<long long, long long>{ex - vx, ey - vy})){
                ans = min(ans, 4LL);
                continue;
            }
            if(K == 4)
                continue;
            for(auto [dx, dy] : dir){
                pair<long long, long long> tar{ex + dx - vx, ey + dy - vy};
                if(!binary_search(arr.begin(), arr.end(), tar))
                    continue;
                ans = min(ans, 5LL);
            }
        }
    }
    cout << (ans == 6? -1 : ans);
    return 0;
}
#endif
}
