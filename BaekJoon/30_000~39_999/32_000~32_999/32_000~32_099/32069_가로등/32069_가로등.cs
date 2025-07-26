using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 2
이름 : 배성훈
내용 : 가로등
    문제번호 : 32069번

    그리디, BFS 문제다
    그리디와 누적합으로 해결했다

    사이값을 조사해 거리의 개수를 찾아 누적해주면 된다
    다만 0에서 L까지 임을 놓쳐 1번 틀렸고
    이후 범위가 50만까지 가서 25만으로 작게 잡아 1번 더 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_1018
    {

        static void Main1018(string[] args)
        {

            int MAX = 500_000;
            StreamReader sr;
            StreamWriter sw;

            long l;
            int n, k;

            long[] arr;
            int[] cnt;

            Solve();
            void Solve()
            {

                Input();

                SetCnt();

                GetRet();
            }

            void SetCnt()
            {

                cnt[0] = n;
                cnt[1] = 2;
                if (arr[0] <= MAX)
                    cnt[arr[0] + 1]--;

                if (l - arr[n - 1] <= MAX)
                {

                    cnt[l - arr[n - 1] + 1]--;

                }

                for (int i = 1; i < n; i++)
                {

                    long sub = arr[i] - arr[i - 1] - 1;
                    long m = sub >> 1;

                    cnt[1] += 2;
                    if (m <= MAX)
                    {

                        if ((sub & 1L) == 1L) 
                        {

                            cnt[m + 1]--; 
                            cnt[m + 2]--;
                        }
                        else cnt[m + 1] -= 2;
                    }
                }

                for (int i = 2; i <= MAX; i++)
                {

                    cnt[i] += cnt[i - 1];
                }
            }

            void GetRet()
            {

                for (int i = 0; i <= MAX; i++)
                {

                    for (int j = 0; j < cnt[i]; j++)
                    {

                        k--;
                        sw.Write($"{i}\n");

                        if (k == 0) break;
                    }

                    if (k == 0) break;
                }

                sw.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                l = ReadLong();
                n = ReadInt();
                k = ReadInt();

                arr = new long[n];
                cnt = new int[MAX + 3];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadLong();
                }

                sr.Close();
            }

            long ReadLong()
            {

                int c;
                long ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
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
using System.Collections.Generic;
using System.IO;

public class Program
{
    struct Pair
    {
        public long x, d;
        public Pair(long x, long d)
        {
            this.x = x; this.d = d;
        }
    }
    static void Main()
    {
        string[] lnk = Console.ReadLine().Split(' ');
        long l = long.Parse(lnk[0]); int n = int.Parse(lnk[1]), k = int.Parse(lnk[2]);
        long[] array = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);
        HashSet<long> visited = new();
        Queue<Pair> queue = new();
        foreach (long x in array)
        {
            visited.Add(x);
            queue.Enqueue(new(x, 0));
        }
        StreamWriter sw = new(Console.OpenStandardOutput());
        while (queue.Count > 0 && k > 0)
        {
            Pair cur = queue.Dequeue();
            sw.WriteLine(cur.d);
            if (--k == 0)
                break;
            if (cur.x - 1 >= 0 && !visited.Contains(cur.x - 1))
            {
                visited.Add(cur.x - 1);
                queue.Enqueue(new(cur.x - 1, cur.d + 1));
            }
            if (cur.x + 1 <= l && !visited.Contains(cur.x + 1))
            {
                visited.Add(cur.x + 1);
                queue.Enqueue(new(cur.x + 1, cur.d + 1));
            }
        }
        sw.Close();
    }
}
#elif other2
// #include <iostream>
// #include <algorithm>
// #include <cmath>
// #include <queue>
// #include <set>
// #include <unordered_map>
// #include <vector>

using namespace std;
using ll = long long;

int main()
{
    cin.tie(NULL);
    cout.tie(NULL);
    ios_base::sync_with_stdio(false);

    // 32069번
    int maxVal = 500000;
    ll l, n, k;
    cin >> l >> n >> k;

    vector<ll> lights;
    if (l >= maxVal)
        lights = vector<ll>(maxVal + 1, 0);
    else
        lights = vector<ll>(l + 1, 0);
    
    vector<ll> arr(n, 0);
    for (auto& val : arr) cin >> val;
    sort(arr.begin(), arr.end());

    // 어두움 0초기화
    lights[0] = n;
    
    ll curVal = arr[0];

    // 처음값
    if (curVal > l)
    {
        if (curVal >= maxVal)
            lights[maxVal]++;
        else
            lights[l]++;
    }
    else if (curVal > 0)
    {
        if (curVal >= maxVal)
            lights[maxVal]++;
        else
            lights[curVal]++;
    }

    // 사이값
    for (int i = 1; i < n; i++)
    {
        ll nextVal = arr[i];

        ll delVal = nextVal - curVal - 1;
        ll halfVal = delVal / 2;
        curVal = nextVal;

        // 1 ~ halfVal까지 1 더한다.
        if (delVal > 0)
        {
            if (delVal % 2 == 1)
            {
                if (halfVal + 1 >= maxVal)
                    lights[maxVal]++;
                else
                    lights[halfVal + 1]++;
            }
            else if (halfVal > 0)
            {
                if (halfVal >= maxVal)
                    lights[maxVal]++;
                else
                    lights[halfVal]++;
            }
            if (halfVal > 0)
            {
                if (halfVal >= maxVal)
                    lights[maxVal]++;
                else
                    lights[halfVal]++;
            }
        }
    }

    // 끝값 더한다.
    ll lastVal = l - curVal;
    if (lastVal > l)
    {
        if (l >= maxVal)
            lights[maxVal]++;
        else
            lights[l]++;
    }
    else if (lastVal > 0)
    {
        if (lastVal >= maxVal)
            lights[maxVal]++;
        else
            lights[lastVal]++;
    }

    ll sum = 0;
    for (int i = 1; i < lights.size(); i++)
    {
        sum += lights[i];   
    }

    // 먼저 0번 부터 출력
    while (k > 0 && lights[0] > 0)
    {
        cout << '0' << '\n';
        k--;
        lights[0]--;
    }

    // 그다움
    int curLights = 1;
    while (k > 0)
    {
        if (sum > k)
        {
            for (int i = 0; i < k; i++)
            {
                cout << curLights << '\n';
            }
            k = 0;
        }
        else
        {
            for (int i = 0; i < sum; i++)
            {
                cout << curLights << '\n';
            }
            k -= sum;
        }
        sum -= lights[curLights];
        curLights++;
    }

    return 0;
}
#endif
}
