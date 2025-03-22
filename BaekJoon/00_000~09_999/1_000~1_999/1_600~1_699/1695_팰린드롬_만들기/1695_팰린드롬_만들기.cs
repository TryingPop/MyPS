using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 11
이름 : 배성훈
내용 : 팰린드롬 만들기
    문제번호 : 1695번

    ... 고민해도 잘 안나온다
    이거 같은데 싶은 아이디어를 해서 통과했다
    시간은 1860ms -> 448ms

    처음 아이디어는 다음과 같다
    왼쪽끝과 오른쪽 끝이 같은지 판별한다
        같은 경우 양쪽 다 뺀다
            현재 값을 넣는다
        다른 경우 왼쪽 뺀 경우, 오른쪽 뺀 경우로 나눈다
        그리고 왼쪽 뺀 경우와 오른쪽 뺀 경우에 
            현재 값에 + 1한 값을 넣는다

    이 경우 현재 값이 최소임을 보장해줘야한다!
    그래서 우선순위 큐로 진행 했다

    그리고 왼쪽과 오른쪽이 빠지면서 진행하기에, 오른쪽 인덱스가 왼쪽 인덱스보다 작거나 같은 경우 탈출하고 이때 최소값 체크를 한다
    5000 * 5000의 메모리를 사용하기에 메모리가 아슬아슬하다

    거기에 우선순위 큐까지 사용하니, 처음에 1860ms 시간이 걸렸다
    제한 시간이 2초이기에 통과하긴 통과했다;
    
    조금 더 빠르게 할 수 없을까 생각하고 이걸 DFS로 바꾸니 448ms로 처음꺼보단 4배 이상 빨라졌다
    그래도 여전히 메모리를 많이 잡아먹는다;

    C# 은 다른 푼 사람이 없다;
    그래서 C++을 보니 메모리가 엄청 감축한 코드가 있었다
    n^2이 아닌 n을 이용해서 풀었다

    코드는 아래 Cpp_other 구간에 남겨 놓는다

    마지막으로 해당 코드를 풀이해서 넣으니 448 ms -> 100ms로 줄어들었다...
    메모리도 20배 이상 절약된다

    코드의 주석을 달고보니 아이디어가 대단하다!
*/

namespace BaekJoon.etc
{
    internal class etc_0015
    {

#if DFS
        const int MAX = 10_000;
        static int len;
        static int[,] dp;
        static int[] nums;
#endif

        static void Main15(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
#if !best
            int len = GetInt(sr);

            int[] nums = new int[len];
            for (int i = 0; i < len; i++)
            {

                nums[i] = GetInt(sr);
            }
#endif

#if Heavy
            int MAX = 10_000;
            int len = GetInt(sr);
            int[,] dp = new int[len, len];
#elif DFS
            len = GetInt(sr);
            dp = new int[len + 1, len + 1];
#endif
            // 앞에서짜른거, 뒤에서 짜른거

#if Heavy || DFS
            for (int i = 0; i <= len; i++)
            {

                for (int j = 0; j <= len; j++)
                {

                    dp[i, j] = MAX;
                }
            }
#endif
#if Heavy
            int[] nums = new int[len];
#elif DFS
            nums = new int[len];
#endif

#if Heavy || DFS
            for (int i = 0; i < len; i++)
            {

                nums[i] = GetInt(sr);
            }
#endif
            sr.Close();
#if !Best

            int[] dp = new int[len];

            // nums
            //      0 1 2 3 4 5 6 7 8       <- idx
            //      1 2 3 4 2 3 5 2 9       <- value
            // 인경우
            // idx를 기준으로 봤을 때
            // dp
            //      0 1 2 3 4 5 6 7 8
            //      1 0 0 0 0 0 0 0 0       <- idx 0 == idx 0
            //      1 0 0 0 0 0 0 1 0       <- idx 1 == idx 7
            //      1 0 0 0 1 0 0 1 0       <- idx 1 == idx 4
            //      1 1 0 0 1 0 0 1 0       <- idx 1 == idx 1
            //      1 1 0 0 0 2 0 1 0       <- idx 2 == idx 5
            //                                  idx 7에서 1의 값을 받아 max = 1
            //      1 1 2 0 0 2 0 1 0       <- idx 2 == idx 2
            //      1 1 2 0 0 2 0 1 0           idx 5에서 2의 값을 받아 max = 2
            //      1 1 2 3 0 2 0 1 0       <- idx 3 == idx 3
            //      1 1 2 3 0 2 0 1 0       <- idx 4 == idx 7 이지만
            //                                  현재 max = 0이고, max + 1 == dp[7]이므로 값 갱신 X
            //      1 1 2 3 3 2 0 1 0       <- idx 4 == idx 4
            //                                  idx 5에서 max = 2로 갱신
            //      이후는 자기자신에서 값이 채워진다
            //      1 1 2 3 3 2 2 1 1
            for (int left = 0; left < len; left++)
            {

                int max = 0;
                for (int right = len - 1; right >= left; right--)
                {

                    int cur = dp[right];
                    
                    if (nums[left] == nums[right]) dp[right] = dp[right] < max + 1 ? max + 1 : dp[right];
                    max = max < cur ? cur : max;
                }
            }

            // 현재 문자에서 순서를 유지하면서 문자를 빼낼 때,
            // 가장 긴 팰린드롬의 시작 지점에서 중앙까지의 개수를 찾는다
            int m = 0;
            for (int i = 0; i < len; i++) m = m < dp[i] ? dp[i] : m;

            int c = 0;
            for (int i = 0; i < len - 1; i++)
            {

                // 짝수인지 확인
                // 만들어진 팰린드롬이 abba꼴인지 확인한다!
                if (dp[i] == m && dp[i + 1] == m && nums[i] == nums[i + 1]) c = 1;
            }

            // 1 2 3 4 2 3 5 2 9 에서 만들어진 팰림드롬의 최대 길이는 5
            // 아닌 것들을 이어붙여야하므로 4
            // 9 - (6 - 1) = 4
            // 4개를 이어 붙여야한다
            Console.WriteLine(len - m * 2 + 1 - c);
#endif

#if Heavy
            int min = MAX;
            dp[0, len - 1] = 0;
                        // BFS 식 탐색;
            // 이걸 DFS 로 해서 해보자!
            // 이걸 크기 줄이니깐 시간이 더 걸린다
            PriorityQueue<(int l, int r, int value), int> q = new PriorityQueue<(int l, int r, int value), int>(len * len);
            q.Enqueue((0, len - 1, 0), 0);
            while(q.Count > 0)
            {

                var node = q.Dequeue();

                if (dp[node.l, node.r] < node.value) continue;
                else if (node.l >= node.r)
                {

                    if (min > node.value) min = node.value;
                    continue;
                }


                if (nums[node.l] == nums[node.r])
                {

                    if (node.l + 1 < len && node.r - 1 >= 0 && node.value < dp[node.l + 1, node.r - 1])
                    {

                        dp[node.l + 1, node.r - 1] = node.value;
                        q.Enqueue((node.l + 1, node.r - 1, node.value), node.value);
                    }
                }
                else
                {

                    int val = node.value + 1;
                    if (node.l + 1 < len && node.r >= 0 && val < dp[node.l + 1, node.r])
                    {

                        dp[node.l + 1, node.r] = val;
                        q.Enqueue((node.l + 1, node.r, val), val);
                    }

                    if (node.l < len && node.r - 1 >= 0 && val < dp[node.l, node.r - 1])
                    {

                        dp[node.l, node.r - 1] = val;
                        q.Enqueue((node.l, node.r - 1, val), val);
                    }
                }
            }
#elif DFS
            int min = DFS(0, len - 1);
#endif
#if Wrong
            /*
            // 해당 코드 반례 1, 1, 2
            for (int right = len - 1; right >= 0; right--)
            {

                // left 연산부터 하자!
                for (int left = 0; left < len; left++)
                {

                    if (left >= right) 
                    {

                        if (dp[left, right] < min) min = dp[left, right];
                        break; 
                    }

                    if (nums[left] == nums[right])
                    {

                        int temp = dp[left, right];
                        if (dp[left + 1, right - 1] > temp) dp[left + 1, right - 1] = temp;
                    }
                    else
                    {

                        int temp = dp[left, right] + 1;
                        if (dp[left + 1, right] > temp) dp[left + 1, right] = temp;
                        if (dp[left, right - 1] > temp) dp[left, right - 1] = temp;
                    }
                }
            }
            */
#endif
#if Heavy || DFS
            Console.WriteLine(min);
#endif
        }

#if DFS
        static int DFS(int _l, int _r)
        // static int DFS(int[,] dp, int[] nums, int _l, int _r)
        {

            if (_r < 0 || _l >= len) return MAX;

            if (_r <= _l)
            {

                dp[_l, _r] = 0;
                return 0;
            }

            if (dp[_l, _r] != MAX) return dp[_l, _r];
            int ret = MAX;

            if (nums[_l] == nums[_r])
            {

                // ret = DFS(dp, nums, _l + 1, _r - 1);
                ret = DFS(_l + 1, _r - 1);
                // dp[_l, _r] = ret;
                if (dp[_l, _r] > ret) dp[_l, _r] = ret;
            }
            else
            {

                // int l = DFS(dp, nums, _l + 1, _r);
                // int r = DFS(dp, nums, _l, _r - 1);
                int l = DFS(_l + 1, _r);
                int r = DFS(_l, _r - 1);

                ret = l < r ? l + 1: r + 1;

                if (dp[_l, _r] > ret) dp[_l, _r] = ret;
            }

            return dp[_l, _r];
        }
#endif
        static int GetInt(StreamReader _sr)
        {

            bool minus = false;
            int ret = 0;
            int c;

            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                else if (c == '-')
                {

                    minus = true;
                    continue;
                }

                ret *= 10;
                ret += c - '0';
            }

            ret = minus ? -ret : ret;
            return ret;
        }
    }
#if Cpp_other
// # include<iostream>
// # include<vector>
// # include<algorithm>
// #define X first
// #define Y second
using namespace std;
int main(){
	ios_base::sync_with_stdio(false), cin.tie(0);
	int n;cin>>n;
	vector<int>v(n);
	for(int i=0;i<n;i++) cin>>v[i];
	vector<int>d(n);
	for(int i=0;i<n;i++){
		int mx=0;
		for(int j=n-1;j>=i;j--){
			int t=d[j];
			if(v[i]==v[j]) d[j]=max(d[j],mx+1);
			mx=max(mx,t);
		}
	}
	int mx=0;
	for(int i=0;i<n;i++) mx=max(d[i],mx);
	int c=0;
	for(int i=0;i<n-1;i++){
		if(d[i]==mx&&d[i+1]==mx&&v[i]==v[i+1]) c=1;
	}
	cout<<n-mx*2+1-c;
}
#endif
}
