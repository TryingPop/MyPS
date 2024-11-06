using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 31
이름 : 배성훈
내용 : 챔피언 (Easy)
    문제번호 : 21319번

    그리디, 이분 탐색 문제다
    처음에 시간제한을 걸고 접근해봤다
    그런데 처음 설정한 시간 내에 통과 못했고 5시간 이상 고민한 문제다;
    (결국 문제를 하나하나 분석해서(확인하며) 풀었다;)

    우선 O(N)의 방법이 가능해 보였지만, 시간 내에 안될거 같아 3번 틀리고
    이분 탐색 방법으로 방향을 틀었다

    우선 비내림차순으로 전투력이 주어지기에 앞의 사람과 전투력이 같지 않다면
    뒤에 사람과 앞에 사람이 붙었을 경우 뒤에 사람이 항상 이긴다!
    그리고 1승이 가능한 경우는 맨 앞에 있지 않고 바로 앞 사람이 항상 자기보다 작은 경우다
    이에 바로 앞 사람이 자기보다 작다면 앞의 모든 사람을 이길 수 있다
    아니라면 자신은 비기거나 지는 승부밖에 없다
    그래서 순 증가하게 수열을 먼저 변형했다

    이후에 앞의 사람에서 챔피언이 나오는 경우가 있다면, 그 사람이 자신을 제외한 모든 사람을 이기는 경우도
    나올 수 있고 해당 경우에도 챔피언이 된다

    그래서 뒤에 사람 중 챔피언을 이길 수 있다면 해당 사람은 순서만 적절히 조율하면 자신을 제외한 모든 사람을 이길 수 있다
    이에 가장 작은 전투력을 가진 챔피언을 찾으면 뒤에 1승하는 사람들을 출력하면 정답이 된다

    당연히 뒤의 사람을 이기면 앞의 사람을 이기겠지 생각을 하고
    처음 이분 탐색에서 모든 사람을 이기는지 확인하는데 이분탐색을 썼다
    그런데 잘못된 생각이었다!

    우선 사람 간격 즉, 두 사람 사이에 있는 사람의 수를 x, 두 사람간 전투력 차이를 y
    그러면 기울기의 변화량이 극적으로 감소하는 경우면 반례가 나올 수 있었다;
    다음 예제를 보자
        1 2 100 100 100 ... 100 ... 100 101
            100이 10만개 있다고 가정
    그러면 2 -> 100은 못이기지만 101은 이긴다!
    이를 찾아내는데 시간이 꽤 걸렸다;

    이후 최소 사람의 위치를 찾는 것으로 이분 탐색을 바꿨다;
*/

namespace BaekJoon.etc
{
    internal class etc_0747
    {

        static void Main747(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n;
            int[] arr;
            int[] idx;
            int len;
            int s;

            Solve();

            void Solve()
            {

                Input();

                Find();

                Output();
            }

            void Find()
            {

                s = 0;
                if (n == 1) return;
                if (len == 0)
                {

                    idx[0] = -1;
                    return;
                }

                Chk();
            }

            void Chk()
            {

                int l = 1;
                int r = len;

                while (l <= r)
                {

                    int mid = (l + r) / 2;

                    bool can = true;
                    for (int i = mid + 1; i <= len; i++)
                    {

                        if (arr[i] < arr[mid] + idx[i] - 2) continue;
                        can = false;
                        break;
                    }

                    if (can) r = mid - 1;
                    else l = mid + 1;
                }

                s = r + 1;
                if (len < s)
                {

                    s = 0;
                    len = 0;
                    idx[0] = -1;
                }
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 655536);

                for (int i = s; i <= len; i++)
                {

                    sw.Write($"{idx[i]} ");
                }

                sw.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n];
                idx = new int[n];

                len = 0;
                arr[len] = ReadInt();
                idx[len] = 1;

                for (int i = 2; i <= n; i++)
                {

                    int cur = ReadInt();
                    if (arr[len] == cur) continue;
                    arr[++len] = cur;
                    idx[len] = i;
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
// #include <bits/stdc++.h>
using namespace std;
using ll = long long;

int n,m;
int a[200'010];

int main() {
	ios_base::sync_with_stdio(false); cin.tie(nullptr); cout.tie(nullptr);
	cin>>n;
	for(int i=0;i<n;i++) cin>>a[i];
	if(n==1) {cout<<1; return 0;}
	for(int i=1,k=0;i<n;i++) if(a[i-1]<a[i]) {
		int t=a[i]+i;
		k=max(i,k);
		t+=k-i;
		for(; k<n-1; k++) {
			if(t>a[k+1]) t++;
			else break;
		}
		if(k==n-1) cout<<i+1<<' ', m=1;
	}
	if(!m) cout<<-1;
}
#endif
}
