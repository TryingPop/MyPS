using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 22
이름 : 배성훈
내용 : 수학 공책
    문제번호 : 8973번

    dp, 브루트포스 문제다
    브루트포스로 풀었다

    아이디어는 다음과 같다
    위와 아래 간선을 이어보면서 합을 구해가면 \, / 이 교차된 x자형과 \, |, / 이 교차된 *형태가 있다
    중앙을 설정하고 확장해가며 최대값을 확인했다

    다른 사람 풀이를 보니 dp로 해결했는데, 이게 더 깔끔해 보인다
*/

namespace BaekJoon.etc
{
    internal class etc_0595
    {

        static void Main595(string[] args)
        {

            StreamReader sr;
            int n;
            int[] arr1;
            int[] arr2;

            Solve();

            void Solve()
            {

                Input();


                int max = -2_000_000;
                int maxF = -1;
                int maxB = -1;

                // * 형태
                for (int mid = 0; mid < n; mid++)
                {

                    int cur = arr1[mid] * arr2[mid]; 

                    int len = 0;
                    while (true)
                    {


                        if (max < cur)
                        {

                            max = cur;
                            maxF = mid - len;
                            maxB = n - 1 - mid - len;
                        }

                        len++;
                        if (mid + len >= n || mid - len < 0) break;
                        cur += arr1[mid + len] * arr2[mid - len];
                        cur += arr1[mid - len] * arr2[mid + len];
                    }
                }

                // X 형태
                for (int mid = 1; mid < n; mid++)
                {

                    int cur = arr1[mid - 1] * arr2[mid];
                    cur += arr1[mid] * arr2[mid - 1];

                    int len = 0;

                    while (true)
                    {

                        if (max < cur)
                        {

                            max = cur;
                            maxF = mid - 1 - len;
                            maxB = n - mid - 1 - len;
                        }

                        len++;
                        if (mid - len - 1 < 0 || mid + len >= n) break;
                        cur += arr1[mid - 1 - len] * arr2[mid + len];
                        cur += arr1[mid + len] * arr2[mid - 1 - len];
                    }
                }

                Console.Write($"{maxF} {maxB}\n{max}");
            }

            void Input()
            {

                sr = new(new BufferedStream(Console.OpenStandardInput()));
                n = ReadInt();
                arr1 = new int[n];
                arr2 = new int[n];

                for (int i = 0; i < n; i++)
                {

                    arr1[i] = ReadInt();
                }

                for (int i = 0; i < n; i++)
                {

                    arr2[i] = ReadInt();
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;
                while((c= sr.Read()) != -1 && c != ' ' && c != '\n') 
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }

                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
// #include <stdio.h>
// #include <algorithm>
// #include <vector>
using namespace std;

int n, a[2010], b[2010];

int main(){
    scanf("%d", &n);
    for(int i = 0; i < n; i++) scanf("%d", &a[i]);
    for(int i = 0; i < n; i++) scanf("%d", &b[i]);
    int ans = -1e9, ll, rr;
    for(int i = 0; i < n; i++){
        int sum = 0, l = i, r = i;
        while(1){
            if(l < 0 || r >= n) break;
            sum += a[l] * b[r];
            if(l != r) sum += a[r] * b[l];
            if(ans < sum){
                ans = sum;
                ll = l;
                rr = r;
            }
            l--; r++;
        }
    }
    for(int i = 0; i < n - 1; i++){
        int sum = 0, l = i, r = i + 1;
        while(1){
            if(l < 0 || r >= n) break;
            sum += a[l] * b[r];
            sum += a[r] * b[l];
            if(ans < sum){
                ans = sum;
                ll = l;
                rr = r;
            }
            l--; r++;
        }
    }
    printf("%d %d\n%d", ll, n - rr - 1, ans);
}

#elif other2
// #include <bits/stdc++.h>
using namespace std;

typedef long long ll;
typedef pair<int, int> pii;

// #define INF (INT_MAX / 2)
// #define endl '\n'
 
// #define MAX_N 2001

int n;
int arr1[MAX_N];
int arr2[MAX_N];
int dp[MAX_N][MAX_N];

int maxValue = -INF;
pii ans;

int getDp(int idx1, int idx2) {
	if (idx1 + idx2 >= n)
		return 0;
	int& ret = dp[idx1][idx2];
	if (ret != -1)
		return ret;
	
	ret = getDp(idx1 + 1, idx2 + 1);
	if (idx1 == n - 1 - idx2)
		ret += arr1[idx1] * arr2[idx1];
	else
		ret += arr1[idx1] * arr2[n - 1 - idx2] + arr1[n - 1 - idx2] * arr2[idx1];

	return ret;
}

int main() {
	ios_base::sync_with_stdio(false); cin.tie(NULL);

	cin >> n;
	for (int i = 0; i < n; i++)
		cin >> arr1[i];
	for (int i = 0; i < n; i++)
		cin >> arr2[i];

	memset(dp, -1, sizeof(dp));
	for (int b = 0; b < n; b++)
		for (int e = 0; e < n - b; e++) {
			int value = getDp(b, e);
			if (maxValue < value) {
				maxValue = value;
				ans = { b, e };
			}
		}

	cout << ans.first << ' ' << ans.second << endl;
	cout << maxValue << endl;

	return 0;
}
#elif other3
using System;

public class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int[] a = Array.ConvertAll(("0 " + Console.ReadLine()).Split(' '), int.Parse);
        int[] b = Array.ConvertAll(("0 " + Console.ReadLine()).Split(' '), int.Parse);
        int an = 0, sw = 0, er = int.MinValue;
        int[,] dp = new int[n + 1, n + 1];
        for (int i = 1; i <= n; i++)
        {
            dp[1, i] = a[i] * b[i];
            if (dp[1, i] > er)
            {
                an = i - 1;
                sw = n - i;
                er = dp[1, i];
            }
        }
        for (int i = 2; i <= n; i++)
        {
            for (int j = 1; j <= n - i + 1; j++)
            {
                dp[i, j] = dp[i - 2, j + 1] + a[j] * b[j + i - 1] + a[j + i - 1] * b[j];
                if (dp[i, j] > er)
                {
                    an = j - 1;
                    sw = n - (j + i - 1);
                    er = dp[i, j];
                }
            }
        }
        Console.Write($"{an} {sw}\n{er}");
    }
}
#endif
}
