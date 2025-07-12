using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 12
이름 : 배성훈
내용 : LIGNJA
    문제번호 : 3263번

    dp 문제다.
    쉬는 시간에 일이 있으면 일을 무조건 해야 한다.
    그리고 같은 시간에 일이 여러 개라면 원하는 것을 선택할 수 있다.

    그리디와 dp 알고리즘으로 해결했다.
    시작 시간에 따라 일들을 정렬한다.

    dp[i] = val를 정렬된 i번째 일을 했을 때 최소 시간이 담기게 한다.
    그렇게 시작해서 dp[0]에는 최소 시간이 담기게 하면 일하는 시간은 양수뿐이므로 
    그리디로 전체에서 최소로 일하는시간이 된다.
    
    먼저 i번째 일을 하면 일을 시도한다.
    그리고 일하고 난 뒤 다음 일의 최소시간 nVal을 찾는다.
    그러면 nVal의 시간의 일들을 모두 시도해본다.
    이렇게 DFS탐색으로 dp의 값을 채워갔다.

    그러면 탐색하는 노드는 k개이고, 각 경우 이분 탐색으로 찾기에 log k의 시간이 걸린다.
    그래서 전체 시간 복잡도는 O(k log k)
*/

namespace BaekJoon.etc
{
    internal class etc_1761
    {

        static void Main1761(string[] args)
        {

            int n, k;
            (int p, int t)[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                Array.Sort(arr, (x, y) => x.p.CompareTo(y.p));
                int[] dp = new int[k + 1];
                Array.Fill(dp, -1);

                Console.Write(n - DFS());

                int DFS(int _dep = 0)
                {

                    ref int ret = ref dp[_dep];
                    if (ret != -1) return ret;
                    ret = arr[_dep].t;
                    int val = arr[_dep].p + arr[_dep].t;
                    int next = BinarySearch(val == 0 ? 1 : val);
                    if (next > k) return ret;
                    int nVal = arr[next].p;

                    int min = n + 1;
                    for (int i = next; i <= k; i++)
                    {

                        if (arr[i].p != nVal) break;
                        min = Math.Min(min, DFS(i));
                    }

                    if (min == n + 1) min = 0;
                    ret += min;
                    return ret;
                }

                int BinarySearch(int _val)
                {

                    int l = 0;
                    int r = k;

                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;

                        if (arr[mid].p < _val) l = mid + 1;
                        else r = mid - 1;
                    }

                    return l;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                k = ReadInt();

                arr = new (int p, int t)[k + 1];

                for (int i = 1; i <= k; i++)
                {

                    arr[i] = (ReadInt(), ReadInt());
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
    }

#if other
// #include <cstring>
// #include <iostream>
// #include <algorithm>
// #define MAX 10002

using namespace std;

int main(){
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);
    int n, k;
    cin >> n >> k;
    int l[MAX] = {};
    int a[MAX];
    memset(a, -1, sizeof(a));
    int v[MAX];
    int next[MAX];
    for(int j = 1; j <= k; ++j){
        int x, y;
        cin >> x >> y;
        v[j] = x + y;
        next[j] = a[x];
        a[x] = j;
    }
    l[n + 1] = 0;
    for(int i = n; i >= 1; --i){
        int m = -1;
        if(a[i] == -1){
            l[i] = l[i + 1] + 1;
            continue;
        }
        int k = a[i];
        while(k != -1){
            m = max(m, l[v[k]]);
            k = next[k];
        }
        l[i] = m;
    }
    cout << l[1] << "\n";
    return 0;
}
#endif
}
