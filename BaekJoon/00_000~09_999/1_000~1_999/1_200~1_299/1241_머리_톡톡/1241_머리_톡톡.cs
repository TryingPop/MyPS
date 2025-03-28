using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 27
이름 : 배성훈
내용 : 머리 톡톡
    문제번호 : 1241번
    
    정수론 문제다.
    같은 숫자를 가진 사람은 톡톡 치는 횟수가 같다.
    그래서 갯수로 관리한다.
    그리고 약수로 있는 인원의 누적합이 정답이 된다.
    해당 방법으로 접근하는 경우 100만 x (1 / 1 + 1 / 2 + ... + 1 / 10만) = 100만 x log 10만으로 시도해볼만하다 느꼈다.
*/

namespace BaekJoon.etc
{
    internal class etc_1481
    {

        static void Main1481(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());
            int[] arr = new int[n];
            int[] cnt = new int[1_000_001];
            int[] ret = new int[1_000_001];

            for (int i = 0; i < n; i++)
            {

                int cur = int.Parse(sr.ReadLine());
                cnt[cur]++;
                arr[i] = cur;
            }

            for (int i = 1; i <= 1_000_000; i++)
            {

                if (cnt[i] == 0) continue;
                for (int j = i; j <= 1_000_000; j += i) 
                {

                    ret[j] += cnt[i];
                }
            }

            for (int i = 0; i < n; i++)
            {

                sw.Write($"{ret[arr[i]] - 1}\n");
            }
        }
    }

#if other
// #include<bits/stdc++.h>
using namespace std;

int n;
int a[100010];
int c[1000010];
int ans[1000010];

int main()
{
	ios::sync_with_stdio(0);
	cin.tie(0);
	cin>>n;
	for(int i=1;i<=n;i++)
	{
		cin>>a[i];
		c[a[i]]++;
	}
	for(int i=1;i<=1000000;i++)
	{
		for(int j=i;j<=1000000;j+=i)
		{
			ans[j]+=c[i];
		}
	}
	for(int i=1;i<=n;i++)
	{
		cout<<ans[a[i]]-1<<"\n";
	}
}
#endif
}
