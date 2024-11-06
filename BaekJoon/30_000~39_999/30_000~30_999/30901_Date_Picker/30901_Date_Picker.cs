using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 31
이름 : 배성훈
내용 : Date Picker
    문제번호 : 30901번

    수학, 그리디, 브루트포스, 정렬, 확률론 문제다
    문제해석에 많은 시간을 썼다
    문제에서 요구하는건 다음과 같다
    .은 회의가능한 시간, x는 회의불가능한 시간을 나타낸다
    그리고 d는 날짜, h는 시간이다

    7일 중 d일을 골라서 모든 24시간 중 h를 구한다
    그리고 이중 가능한 회의 시간을 찾는다
    p의 최대값이 정답이 된다
    그러면 7Cd * 24Ch * h * d 의 시간이 걸리고 최악의 경우 d = 3 or 4, h = 12인 경우 
    20억을 초과하므로 시간초과 날 수 있다

    그런데 그리디로 생각하면, 7일중 d일을 고르고, 24시간 중 가능한 시간을 모두 조사한다
    그리고 가능한 시간이 많은 걸로 정렬하고, 이중 h개 고르면 24시간 중 h시간을 고르는 것을 안해도 된다
    이 경우 7Cd * d * h 을 조사하면 회의 가능한 시간을 찾을 수 있다
    이중 최대가 최대 회의시간이되고 전체 시간에서 나누면 정답이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_1089
    {

        static void Main1089(string[] args)
        {

            StreamReader sr;
            string[] date;
            int[] times;
            int d, h;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                times = new int[24];
                int[] select = new int[d];
                int max = 0;

                DFS();

                Console.WriteLine($"{max / (1.0 * d * h):0.0000000}");
                void DFS(int _depth = 0, int _cnt = 0)
                {

                    if (_cnt == d)
                    {

                        for (int day = 0; day < d; day++)
                        {

                            for (int hour = 0; hour < 24; hour++)
                            {

                                if (date[select[day]][hour] == '.') times[hour]++;
                            }
                        }

                        Array.Sort(times, (x, y) => y.CompareTo(x));
                        int chk = 0;
                        for (int i = 0; i < h; i++)
                        {

                            chk += times[i];
                            times[i] = 0;
                        }

                        for (int i = h; i < 24; i++)
                        {

                            times[i] = 0;
                        }

                        max = Math.Max(chk, max);
                        return;
                    }
                    else if (_depth == 7) return;
                    int prev = select[_cnt];
                    select[_cnt] = _depth;

                    DFS(_depth + 1, _cnt + 1);

                    select[_cnt] = prev;
                    DFS(_depth + 1, _cnt);
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                date = new string[7];
                for (int i = 0; i < 7; i++)
                {

                    date[i] = sr.ReadLine();
                }

                string[] temp = sr.ReadLine().Split();
                d = int.Parse(temp[0]);
                h = int.Parse(temp[1]);
                sr.Close();
            }
        }
    }

#if other
// #include <algorithm>
// #include <cstdio>
// #include <functional>
// #include <vector>

using namespace std;

int main(){
    char schedule[8][30];
    for(int i = 0; i < 7; ++i){
        scanf("%s", schedule[i]);
    }
    int d, h;
    scanf("%d %d", &d, &h);
    vector<int> bitmask(d, 1);
    bitmask.resize(7);
    int answer = 0;
    do{
        vector<int> v;
        for(int j = 0; j < 24; ++j){
            int cnt = 0;
            for(int i = 0; i < 7; ++i){
                if(bitmask[i] && schedule[i][j] == '.'){
                    ++cnt;
                }
            }
            v.push_back(cnt);
        }
        sort(v.begin(), v.end(), greater<int>());
        int sum = 0;
        for(int j = 0; j < h; ++j){
            sum += v[j];
        }
        answer = max(answer, sum);
    }while(prev_permutation(bitmask.begin(), bitmask.end()));
    printf("%.12f\n", answer / (double)(d * h));
    return 0;
}
#endif
}
