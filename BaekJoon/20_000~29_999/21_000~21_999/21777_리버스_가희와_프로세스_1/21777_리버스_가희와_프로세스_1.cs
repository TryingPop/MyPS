using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 5
이름 : 배성훈
내용 : 리버스 가희와 프로세스 1
    문제번호 : 21777번

    그리디, 해 구성하기 문제다
    그냥 단순히 생각하다가 19%에서 두 번 틀렸다
    그래서 가희와 프로세스 1부터(etc_1030) 풀고 검증하면서 풀었다
    몇 개 검증하니 이전 id보다 커지거나 같은 경우 
    우선순위를 1 낮춰주면 됨을 확인했다
*/

namespace BaekJoon.etc
{
    internal class etc_1027
    {

        static void Main1027(string[] args)
        {

            int MAX = 1_000_000;

            StreamReader sr;
            StreamWriter sw;

            int n;
            int[] time, priority;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                n = ReadInt();
                int before = MAX + 1;
                int idx = n;

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    int id = ReadInt();

                    if (before >= id) idx--;

                    if (priority[id] == 0) 
                    { 
                        
                        priority[id] = idx;
                        ret++;
                    }

                    time[id]++;

                    before = id;
                }

                sw.Write($"{ret}\n");
                for (int i = 1; i <= MAX; i++)
                {

                    if (priority[i] == 0) continue;
                    sw.Write($"{i} {time[i]} {priority[i]}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                time = new int[MAX + 1];
                priority = new int[MAX + 1];
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
// #pragma GCC optimize("O3")
// #pragma GCC optimize("Ofast")
// #pragma GCC optimize("unroll-loops")
// #pragma GCC target("avx,avx2,fma")
using namespace std;
const int MAX=1000000;
int T;
int L[MAX+1],R[MAX+1];
int main(){
    ios::sync_with_stdio(0);
    cin.tie(0);
    cout.tie(0);
    cin>>T;
    int cnt=0;
    for (int i=0,j=1,p=0; i<T; i++){
        int x;
        cin>>x;
        if (x<=p)
            j++;
        if (!L[x]){
            cnt++;
            L[x]=j;
        }
        R[x]=j;
        p=x;
    }
    cout<<cnt<<"\n";
    for (int i=1; i<=MAX; i++){
        if (L[i])
            cout<<i<<" "<<R[i]-L[i]+1<<" "<<MAX-L[i]<<"\n";
    }
    return 0;
}

#endif
}
