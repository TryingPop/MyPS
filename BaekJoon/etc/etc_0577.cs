using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 20
이름 : 배성훈
내용 : 곱셈 게임
    문제번호 : 4370번

    dp, 게임 이론 문제다
    부를 수 있는 모든 경우를 탐색해서 푼다
    그러면 각 결과에 대해
    해당 구간에서
    (18^k, 9 * 18^k] -> Back 승리
    (9 * 18^k, 18^k + 1] -> Donghyuk 승리
    (는 개구간, ]는 폐구간이다 개구간 : 끝을 포함 X, 폐구간 : 끝을 포함

    아이디어는 다음과 같다
    탑다운 dp 저장을 해야한다
    해당 경우를 작은경우로 쪼개서 진행하는 것이다
    그래서 다른 경우로 확장을 못한다

    일단 숫자를 하나씩 부르면서 진행한다
    계속 해서 부르고 n이상이 되는 경우 승리로 간주한다
    그러면 승리 수의 바로 앞의 경우 해당 수를 부르면 상대가 승리하는 경우가 존재하므로 
    부르면 안되는 수 즉, 패배하는 수이다
    
    이렇게 승리하는 경우로 가는 수를 끝에서부터 하나씩 탐색해간다
    승리하는 경우가 존재? -> 패배하는 수, 승리하는 경우가 존재 안하면 상대가 모두 패배하는 경우이므로
    승리하는 경우가 된다!
*/

namespace BaekJoon.etc
{
    internal class etc_0577
    {

        static void Main577(string[] args)
        {

            string YES = "Baekjoon wins.\n";
            string NO = "Donghyuk wins.\n";

            StreamReader sr = new(Console.OpenStandardInput());
            StreamWriter sw = new(Console.OpenStandardOutput());

#if first
            Solve();
            sr.Close();
            sw.Close();

            void Solve()
            {

                while (true)
                {

                    string str = sr.ReadLine();
                    if (str == null || str == string.Empty) break;

                    long n = long.Parse(str);

                    bool isSearch = true;
                    bool ret = false;
                    long calc1 = 9;
                    long calc2 = 18;

                    while (isSearch)
                    {

                        if (n <= calc1)
                        {

                            ret = true;
                            isSearch = false;
                        }
                        else if (n <= calc2)
                        {

                            ret = false;
                            isSearch = false;
                        }

                        calc1 *= 18;
                        calc2 *= 18;
                    }

                    sw.Write(ret ? YES : NO);
                }
            }

#elif second

            // 1 ~ 10까지 2, 3, 5, 7 인수의 개수
            (int n1, int n2, int n3, int n4)[] cnt = new (int n1, int n2, int n3, int n4)[10];
            int[,,,] dp = new int[33, 33, 33, 33];

            // 제곱
            long[,] pow = new long[10, 33];

            // 인수 채우기
            for (int i = 2; i <= 9; i++)
            {

                int t = i;
                int n2 = 0, n3 = 0, n5 = 0, n7 = 0;

                while(t % 2 == 0)
                {

                    t /= 2;
                    n2++;
                }
                while(t % 3 == 0)
                {

                    t /= 3;
                    n3++;
                }
                while(t % 5 == 0)
                {

                    t /= 5;
                    n5++;
                }
                while(t % 7 == 0)
                {

                    t /= 7;
                    n7++;
                }

                cnt[i] = (n2, n3, n5, n7);
            }

            int[] a = { 2, 3, 5, 7 };
            for (int i = 0; i < 4; i++)
            {

                pow[a[i], 0] = 1;
                for (int j = 1; j <= 32; j++)
                {

                    pow[a[i], j] = pow[a[i], j - 1] * a[i];
                }
            }

            long n;

            while (true)
            {

                string str = sr.ReadLine();
                if (str == null || str == string.Empty) break;

                n = long.Parse(str);

                int ret = Find(0, 0, 0, 0);
                sw.Write(ret == 1 ? YES : NO);

                for (int i1 = 0; i1 < 33; i1++)
                {

                    for (int i2 = 0; i2 < 33; i2++)
                    {

                        for (int i3 = 0; i3 < 33; i3++)
                        {

                            for (int i4 = 0; i4 < 33; i4++)
                            {

                                dp[i1, i2, i3, i4] = 0;
                            }
                        }
                    }
                }
            }

            sr.Close();
            sw.Close();
            int Find(int _2, int _3, int _5, int _7)
            {

                int ret = dp[_2, _3, _5, _7];
                if (pow[2, _2] * pow[3, _3] * pow[5, _5] * pow[7, _7] >= n)
                {
                    dp[_2, _3, _5, _7] = -1;
                    return -1; 
                }
                if (ret != 0) return ret;

                ret = 1;

                for (int i = 2; i <= 9; i++)
                {

                    ret = Math.Min(ret, Find(_2 + cnt[i].n1, _3 + cnt[i].n2, _5 + cnt[i].n3, _7 + cnt[i].n4));
                }

                ret *= -1;
                dp[_2, _3, _5, _7] = ret;
                return ret;
            }
#else

            Dictionary<long, bool> memo = new(33 * 33 * 33 * 33);
            long n;
            while (true)
            {

                string str = sr.ReadLine();
                if (str == null || str == string.Empty) break;

                n = long.Parse(str);

                bool ret = DFS(1L);

                sw.Write(ret ? NO : YES);
                memo.Clear();
            }

            sr.Close();
            sw.Close();

            bool DFS(long cur)
            {

                if (memo.ContainsKey(cur)) return memo[cur];
                if (cur >= n) return true;

                for (int i = 2; i < 10; i++)
                {

                    if (DFS(cur * i)) 
                    { 
                        
                        memo[cur] = false;
                        return false;
                    }
                }

                memo[cur] = true;
                return true;
            }
#endif
        }
    }

#if other
// #include<stdio.h>
// #include<stdlib.h>

int main()
{
	long long n;
	long long p;

	while (scanf("%lld", &n)!=EOF)
	{
		p = 1;

		while (1)
		{
			p = p * 9;
			if (p >= n)
			{
				printf("Baekjoon wins.\n");
				break;
			}

			p = p * 2;
			if (p >= n)
			{
				printf("Donghyuk wins.\n");
				break;
			}
		}
	}

	return 0;
}
#elif other2
// #include <bits/stdc++.h>
using namespace std;
typedef pair<int,int>pii;
typedef pair<pii,int>piii;
typedef pair<pii,pii>piiii;
typedef long long ll;
typedef unsigned long long ull;
int dx[] = {1,-1,0,0,1,1,-1,-1};
int dy[] = {0,0,1,-1,1,-1,1,-1};
// #define endl '\n'
inline int abs(int x){
  return x>0 ? x : -x;
}
int gcd(int a,int b){
  while(b){
    int t = b;
    b = a%b;
    a=t;
  }
  return a;
}
void io(){
  ios_base::sync_with_stdio(0);
  cin.tie(0);
}
const int INF = 1e9;
const ll LINF = 1e18;
const int MOD = 987654321;
const int MXN = 1000005;

ll N;
ll pows[10][33];
int dp[33][33][33][33];
piiii cnt[10];
int rec(int _2,int _3,int _5,int _7) {
  int& ret = dp[_2][_3][_5][_7];
  if((ll)(pows[2][_2]*pows[3][_3]*pows[5][_5]*pows[7][_7])>=N)return ret = -1;
  if(ret!=0)return ret;
  ret = 1;
  for(int nxt = 2; nxt<=9; ++nxt){
    ret = min(ret,rec(_2+cnt[nxt].first.first,_3+cnt[nxt].first.second,_5+cnt[nxt].second.first,_7+cnt[nxt].second.second));
  }
  ret*=-1;
  return ret;
}
int main() {
  for(int nxt=2;nxt<=9;++nxt){
    int t = nxt;
    int __2=0,__3=0,__5=0,__7=0;
    while(t%2==0){
        t/=2;
        __2++;
      }
      while(t%3==0){
        t/=3;
        __3++;
      }
      while(t%5==0){
        t/=5;
        __5++;
      }
      while(t%7==0){
        t/=7;
        __7++;
      }
    cnt[nxt] = {{__2,__3},{__5,__7}};

  }
  vector<int>a={2,3,5,7};
  for(int i : a){
    pows[i][0]=1;
    for(int k =1;k<=32;++k){
      pows[i][k]=(ll)pows[i][k-1]*i;
    }

  }
  while(cin>>N) {
    memset(dp,0,sizeof(dp));
    if(rec(0,0,0,0)==1)cout<<"Baekjoon wins."<<endl;
    else cout<<"Donghyuk wins."<<endl;
  }
}

#elif other3
fun main() = with(System.`in`.bufferedReader()) {

    while (true) {
        var s = readLine() ?: break
        var n = s.toLong()
        fun searching(num: Long, turn: Int): Int {
            if (n <= num * 9) return turn
            for (i in 2 .. 9) {
                var next = searching(num * i, turn xor 1)
                if (next == turn) return turn
            }
            return turn xor 1
        }
        if (searching(1, 0) == 0) println("Baekjoon wins.") else println("Donghyuk wins.")
    }
}
#elif other4
def canWin(state,n, memo):
	if state in memo:
		return memo[state]
	if state >= n:
		return True
	for move in range(9,1,-1):
		if canWin(state*move,n, memo):
			memo[state] = False
			return False
	memo[state] = True
	return True

while True:
	try:
		n = int(input())
	except:
		break
	d = {}
	if not canWin(1,n, d):
		print("Baekjoon wins.")
	else:
		print("Donghyuk wins.")
#endif
}
