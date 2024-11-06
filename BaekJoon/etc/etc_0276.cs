using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 20
이름 : 배성훈
내용 : 퍼즐 자르기
    문제번호 : 14727번

    스택, 분할정복, 세그먼트 트리 문제다
    스택을 이용해서 했으나, 3번 탐색하는 매우 비효율적으로 풀었다

    수정하는데, 로직이 잘못되어 그런지 엄청나게 틀렸다;
    그런데 거리 함수를 따로 정의하니 구현하는 난이도가 낮아지고
    바로 통과했다

    아이디어는 다음과 같다
    해당 블록보다 낮아지는 왼쪽끝과 오른쪽 끝을 알아야 한다
    이상인 값만 남기면서 기록해간다
    만약 스택에 낮은 값이 있다면 해당 블록 -1이 오른쪽 끝이된다
    왼쪽끝은 스택의 저장방법에 의해 스택에 저장된 바로 앞 인덱스의 +1이 왼쪽끝이 된다
    이렇게 가로를 찾고 세로를 곱해주면 정답이 된다

    마지막까지 진행하면 빠지지 않는 원소가 있는데, 
    이는 끝에 가질 수 없는 가장 작은 값을 넣으면 해결된다
*/

namespace BaekJoon.etc
{
    internal class etc_0276
    {

        static void Main276(string[] args)
        {

            StreamReader sr;
            int n;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }
#if first
            void GetRet()
            {

                long ret = 0;
                int[] stack = new int[n + 2];
                int len = 0;

                long[] width = new long[n + 2];

                for (int i = 1; i <= n + 1; i++)
                {

                    while(len > 0)
                    {

                        int idx = stack[len - 1];
                        if (arr[i] < arr[idx])
                        {

                            width[idx] = i - 1 - idx;
                            len--;
                        }
                        else break;
                    }

                    stack[len++] = i;
                }

                len = 0;

                for (int i = n; i >= 0; i--)
                {

                    while(len > 0)
                    {

                        int idx = stack[len - 1];
                        if (arr[i] < arr[idx])
                        {

                            width[idx] += idx - i;
                            len--;
                        }
                        else break;
                    }

                    stack[len++] = i;
                }

                for (int i = 1; i <= n; i++)
                {

                    long area = width[i] * arr[i];
                    ret = Math.Max(ret, area);
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                arr = new int[n + 2];

                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
            }

#else
            void GetRet()
            {

                long ret = 0;
                int[] stack = new int[n];
                int len = 0;

                for (int i = 0; i <= n; i++)
                {

                    while (len > 0)
                    {

                        int idx = stack[len - 1];
                        if (arr[i] < arr[idx])
                        {

                            long width;
                            if (len > 1) width = GetDis(stack[len - 2] + 1, i - 1);
                            else width = GetDis(0, i - 1);
                            ret = Math.Max(ret, width * arr[idx]);
                            len--;
                        }
                        else break;
                    }

                    stack[len++] = i;
                }

                Console.Write(ret);
            }

            long GetDis(int _l, int _r)
            {

                return _r - _l + 1;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                arr = new int[n + 1];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
            }

#endif


            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// #include <cstdio>
// #include <iostream>
// #include <stack>
// #include <queue>
// #include <algorithm>
// #include <utility>
// #include <string>
// #include <cmath>
// #include <vector>
// #include <functional>
// #include <ctime>
// #include <map>
// #include <set>
// #include <list>
// #include <unordered_set>
// #include <unordered_map>
//#include <bits/stdc++.h>
using namespace std;

typedef unsigned long long ull;
typedef long long ll;
typedef pair<int, int> pint;
typedef pair<ull, ull> pull;
typedef vector<int> vint;
typedef vector<ll> vll;
typedef struct node
{
	int a;
	int b;
}node;
// #define SWAP(a, b, type) do{type x;x=b, b=a, a=x;}while(0)

typedef unsigned long long ull;

unsigned int arr[100000];

ull get_max(int left, int right);
ull merged(int left, int right);

int main()
{
	ios::sync_with_stdio(false), cin.tie(0);
	int N, i;
	cin >> N;
	for (i = 0;i < N;i++)
		cin >> arr[i];
	cout << get_max(0, N - 1) << endl;
	return 0;
}

ull get_max(int left, int right)
{
	if (left == right)
		return arr[left];

	ull lmax = get_max(left, (left + right) / 2), rmax = get_max((left + right) / 2 + 1, right);
	ull mmax = merged(left, right), submax = max(lmax, rmax);
	return max(mmax, submax);
}

ull merged(int left, int right)
{
	unsigned int l = (left + right) / 2, r, height, num = 2;
	ull M;
	r = l + 1;
	height = min(arr[l], arr[r]);
	M = (ull)height*num;

	while (l > left && r < right)
	{
		if (arr[l - 1] > arr[r + 1])
		{
			l--, num++;
			height = min(height, arr[l]);
			M = max(M, (ull)height*num);
		}
		else
		{
			r++, num++;
			height = min(height, arr[r]);
			M = max(M, (ull)height*num);
		}
	}

	while (l > left)
	{
		l--, num++;
		height = min(height, arr[l]);
		M = max(M, (ull)height*num);
	}

	while (r < right)
	{
		r++, num++;
		height = min(height, arr[r]);
		M = max(M, (ull)height*num);
	}

	return M;
}
#elif other2
// #include <bits/stdc++.h>
using namespace std;
typedef long long ll;

int n;
int arr[100005];
stack<int> st;
ll res;

int main() {
	ios_base::sync_with_stdio(false);
	cin.tie(NULL); cout.tie(NULL);

	cin >> n;

	for (int i = 1; i <= n; i++) {
		cin >> arr[i];
	}

	st.push(0);
	for (int i = 1; i <= n + 1; i++) {
		while (!st.empty() && arr[st.top()] > arr[i]) {
			ll tmp = arr[st.top()];
			st.pop();
			ll tmp2 = i - st.top() - 1;
			res = max(res, tmp * tmp2);
		}
		st.push(i);
	}

	cout << res;

	return 0;
}
#endif
}
