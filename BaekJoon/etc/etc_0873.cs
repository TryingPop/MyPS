using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 10
이름 : 배성훈
내용 : 풀자
    문제번호 : 1332번

    브루트포스 문제다
    처음에 그냥 dp로 해결하면 되지않을까 생각했고
    잘못 구현해서 한 번 틀렸다
    그리고 고민하니 그냥 브루트포스로 해결될거 같았다

    아이디어는 다음과 같다
    v가 자연수이므로 꼭 지나야하는 임의의 두 점 l, r을 정한다 
    편의상 번호가 l < r로 잡는다

    두 점의 차가 v가 될 때 해당 두 점 l, r을 지나고 r에서 끝나는
    최소 횟수를 확인한다
    
    이렇게 찾은 최소 횟수가 정답이랑 일치한다
    다른 사람의 풀이를 보니 dp로 해결했는데, N^3 메모리나 
    n x min x max범위만큼 메모리를 할당해풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0873
    {

        static void Main873(string[] args)
        {

            StreamReader sr;
            int n, v;
            int[] arr;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret = n;
                for (int l = 0; l < n; l++)
                {

                    for (int r = l + 1; r < n; r++)
                    {

                        if (Math.Abs(arr[l] - arr[r]) < v) continue;

                        int chk = 1 + (l + 1) / 2 + (r - l + 1) / 2;
                        if (chk < ret) ret = chk;
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                v = ReadInt();

                arr = new int[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
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
using System;
using System.IO;

namespace Baekjoon_1332
{
    class Program
    {
        static void Main()
        {
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int[] nv = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            int n = nv[0], v = nv[1], answer = n;
            string[] s = sr.ReadLine().Split(' ');
            int[] min0 = { 1001 }, min1 = { 1001 }, max0 = { -1 }, max1 = { -1 };
            for (int i = 0; i < n; i++)
            {
                int a = int.Parse(s[i]);
                int[] curr = new int[] { a, i };
                bool z = i % 2 == 0;
                if (z)
                {
                    if (a < min0[0])
                    {
                        min0 = curr;
                    }
                    if (a > max0[0])
                    {
                        max0 = curr;
                    }
                }
                if (a - min0[0] >= v)
                {
                    answer = Math.Min(answer, 1 + min0[1] / 2 + (i + 1 - min0[1]) / 2);
                }
                if (max0[0] - a >= v)
                {
                    answer = Math.Min(answer, 1 + max0[1] / 2 + (i + 1 - max0[1]) / 2);
                }
                if (!z)
                {
                    if (a < min1[0])
                    {
                        min1 = curr;
                    }
                    if (a > max1[0])
                    {
                        max1 = curr;
                    }
                }
                if (a - min1[0] >= v)
                {
                    answer = Math.Min(answer, 2 + min1[1] / 2 + (i + 1 - min1[1]) / 2);
                }
                if (max1[0] - a >= v)
                {
                    answer = Math.Min(answer, 2 + max1[1] / 2 + (i + 1 - max1[1]) / 2);
                }
            }
            Console.WriteLine(answer);
        }
    }
}
#elif other2
// #include <bits/stdc++.h>
using namespace std;

int main() {
    ios_base::sync_with_stdio(0); cin.tie(0);
    
    int n, v;
    cin >> n >> v;
    vector<int> a(n);
    for (int i=0; i<n; i++) cin >> a[i];
    
    vector<vector<vector<char>>> cache(n, vector<vector<char>>(1001, vector<char>(1001, -1)));
    function<char(int, int, int)> sol=[&](int last, int mn, int mx) {
        if (mx-mn>=v) {
            return char(0);
        }

        char& ret=cache[last][mn][mx];
        if (ret!=-1) return ret;

        ret=51;
        if (last<n-1) ret=min(ret, char(sol(last+1, min(mn, a[last+1]), max(mx, a[last+1]))+1));
        if (last<n-2) ret=min(ret, char(sol(last+2, min(mn, a[last+2]), max(mx, a[last+2]))+1));

        return ret;
    };  
    cout << min(n, int(sol(0, a[0], a[0])+1));

    return 0;
}
#elif other3
// #include<bits/stdc++.h>
using namespace std;
int dp[55][51][51];
int main(){
    int n,v;scanf("%d%d",&n,&v);
    memset(dp,0x3f,sizeof(dp));
    vector<int> a(n);for(int i=0;i<n;i++)scanf("%d",&a[i]);
    dp[0][0][0]=1;
    for(int k=0;k<n;k++)for(int i=0;i<n;i++)for(int j=0;j<n;j++){
        if(k+1<n)dp[k+1][a[i]<a[k+1]?i:k+1][a[j]>a[k+1]?j:k+1]=min(dp[k+1][a[i]<a[k+1]?i:k+1][a[j]>a[k+1]?j:k+1],dp[k][i][j]+1);
        if(k+2<n)dp[k+2][a[i]<a[k+2]?i:k+2][a[j]>a[k+2]?j:k+2]=min(dp[k+2][a[i]<a[k+2]?i:k+2][a[j]>a[k+2]?j:k+2],dp[k][i][j]+1);
    }

    int ans=n;
    for(int k=0;k<n;k++)for(int i=0;i<n;i++)for(int j=0;j<n;j++)
        if(a[i]+v<=a[j])ans=min(ans,dp[k][i][j]);
    printf("%d",ans);
}
#endif
}
