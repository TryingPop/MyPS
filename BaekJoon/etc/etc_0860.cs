using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 3
이름 : 배성훈
내용 : 돌의 정렬 줄세우기
    문제번호 : 24025번

    그리디, 해 구성하기 문제다
    음수면 절대값 이하가 되어야하고, 양수면 절대값 이상이 되어야한다
    음수면 0개가 되게 조절하고 양수면 10^9이되게 했다
*/

namespace BaekJoon.etc
{
    internal class etc_0860
    {

        static void Main860(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int[] arr, ret;
            int n;
            Solve();
            void Solve()
            {

                Input();

                GetRet();

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                int cur = 1;

                if (arr[n - 1] < 0)
                {

                    sw.Write(-1);
                    return;
                }

                ret = new int[n];

                for (int i = 0; i < n; i++)
                {

                    if (arr[i] >= 0) continue;
                    ret[i] = cur++;
                }

                for (int i = n - 1; i >= 0; i--)
                {

                    if (arr[i] < 0) continue;
                    ret[i] = cur++;
                }

                for (int i = 0; i < n; i++)
                {

                    sw.Write($"{ret[i]} ");
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool positive = c != '-';
                int ret = positive ? c - '0' : 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return positive ? ret : -ret;
            }
        }
    }

#if other
// #include <iostream>
// #include <algorithm>
// #include <set>
// #include <map>
// #include <vector>
// #include <queue>
// #include <list>
// #include <cstdio>
// #include <cstring>
// #include <iomanip>
// #include <cmath>
// #include <bitset>
// #include <unordered_set>
using namespace std;
typedef long long ll;

const int MAX_N=100005;
const int INF=1e9;
const int MOD=1'000'000;


int main(){
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int n, arr[MAX_N], ans[MAX_N], low, high;
    cin>>n;
    low=1; high=n;
    for(int i=0;i<n;i++){
        cin>>arr[i];

    }
    if(arr[n-1]<0){cout<<-1<<"\n";}
    else{
        for(int i=0;i<n;i++){
            if(arr[i]>0){
                cout<<high<<" ";
                high--;
            }
            else{
                cout<<low<<" ";
                low++;
            }
        }
        cout<<"\n";
    }
    return 0;
}
#endif
}
