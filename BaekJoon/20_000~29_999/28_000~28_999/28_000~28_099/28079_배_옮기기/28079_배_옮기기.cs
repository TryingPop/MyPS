using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 2
이름 : 배성훈
내용 : 배 옮기기
    문제번호 : 28079번

    dp, 비트마스킹 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1801
    {

        static void Main1801(string[] args)
        {

            // 28079 배 옮기기

            int n;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                if (n == 1)
                {

                    Console.Write(arr[0]);
                    return;
                }

                int INF = 123_456_789;

                int[] dp1 = new int[1 << n];
                int[] dp2 = new int[1 << n];

                Array.Fill(dp1, INF);
                Array.Fill(dp2, INF);

                dp2[dp2.Length - 1] = 0;

                DFS1(0);
                if (dp1[0] == INF) dp1[0] = -1;
                Console.Write(dp1[0]);

                void DFS1(int _state)
                {

                    if (dp1[_state] != INF) return;
                    dp1[_state] = 0;

                    int min = INF;

                    for (int i = 1; i < n; i++)
                    {

                        if ((_state & (1 << i)) != 0) continue;

                        for (int j = 0; j < i; j++)
                        {

                            if (arr[i] == arr[j]) break;
                            else if ((_state & (1 << j)) != 0) continue;

                            int nextState = _state | (1 << i) | (1 << j);
                            DFS2(nextState);
                            min = Math.Min(min, dp2[nextState] + arr[i]);
                        }
                    }

                    dp1[_state] = min;
                }

                void DFS2(int _state)
                {

                    if (dp2[_state] != INF) return;
                    dp2[_state] = 0;
                    int min = INF;
                    for (int i = 0; i < n; i++)
                    {

                        if ((_state & (1 << i)) == 0) continue;
                        int nextState = _state ^ (1 << i);
                        DFS1(nextState);
                        min = Math.Min(min, dp1[nextState] + arr[i]);
                    }

                    dp2[_state] = min;
                }
            }

            void Input()
            {

                n = int.Parse(Console.ReadLine());
                arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                Array.Sort(arr);
            }
        }
    }

#if other
// #include <iostream>
// #include <vector>
// #include <vector>
// #include <set>
// #include <array>
// #include <algorithm>
// #include <random>
// #include "map"
// #include <bitset>
// #include "array"
// #include <chrono>
//#include <ext/pb_ds/tree_policy.hpp>
//#include <ext/pb_ds/assoc_container.hpp>
using namespace std;
//using namespace __gnu_pbds;
//typedef tree<int, null_type, less<int>, rb_tree_tag,
//        tree_order_statistics_node_update> pds;
// #define all(x)x.begin(),x.end()
// #define pack(x)sort(all(x));x.erase(unique(all(x)),x.end())
// #define gi(x, v)lower_bound(all(x),v)-x.begin()
using ll = long long;
using ld = long double;
using tu = array<int, 3>;
vector<pair<int,int>> hv;
map<vector<pair<int,int>>,int> mp;
int dfs();
int sets(int x){
    vector<pair<int,int>> v;
    for(int i=0;i<x;i++){
        v.push_back(hv.back());
        hv.pop_back();
    }
    if(v[x-1].second!=1)hv.push_back({v[x-1].first,v[x-1].second-1});
    for(int i=x-2;i>=1;i--){
        hv.push_back(v[i]);
    }if(v[0].second!=1)hv.push_back({v[0].first,v[0].second-1});
    int ret=(v[0].first+hv[1].first*2+hv[0].first)+dfs();
    if(v[0].second!=1)hv.pop_back();
    for(int i=1;i+1<x;i++){
        hv.pop_back();
    }if(v[x-1].second!=1)hv.pop_back();
    for(int i=x-1;i>=0;i--){
        hv.push_back(v[i]);
    }return ret;
}
int dfs(){
    if(hv.size()==1){
        if(hv[0].second==1)return hv[0].first;
        return 1e9;
    }if(hv.size()==2){
        return (hv[0].second-1)*hv[1].first*2 + (hv[1].second-1)*(hv[1].first+hv[0].first) + hv[1].first;
    }
    if(mp[hv])return mp[hv];
    if(hv.size()==3){
        auto w=hv.back();
        hv.pop_back();
        int p=min((int)1e9,dfs()+w.second*(w.first+hv[0].first));
        hv.push_back(w);
        return mp[hv]=p;
    }
    int ans=1e9;
    auto w=hv.back();
    hv.pop_back();
    if(w.second!=1)hv.push_back({w.first,w.second-1});
    ans=min(ans,dfs()+w.first+hv[0].first);
    if(w.second!=1)hv.pop_back();
    hv.push_back(w);
    bool flag=true;
    for(int i=2;i<20;i++){
        if(hv.size()>i+1)ans=min(ans,sets(i));
    }
    return mp[hv]=ans;
}
int main(){
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    int n,a;
    cin>>n;
    vector<int> v;
    for(int i=1;i<=n;i++){
        cin>>a;
        v.push_back(a);
    }sort(all(v));
    for(auto i:v){
        if(hv.empty()||hv.back().first!=i)hv.push_back({i,1});
        else hv.back().second++;
    }int w=dfs();
    if(w==1e9)cout<<-1;
    else cout<<w;
}
#endif
}
