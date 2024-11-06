using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. -
이름 : 배성훈
내용 : 수열 나누기
    문제번호 : 10067번

    볼록껍질을 이용한 최적화 문제다

    컨벡스 헐 트릭에 대한 이해가 많이 부족하다;
    https://conankuns.tistory.com/15
    해당 사이트 참고해서 작성 했다

    Leftmost Segment로 컨벡스 헐 트릭이 대충 어떻게 구현되는지 알겠으나
    덱이 섞이니 다시 원점으로 돌아왔다;
    현재는 이러한 문제에서 사용할 수 있구나 느끼고, 
    조건을 하나씩 다시 따지고 왜 이렇게 썼는지 확인해봐야 겠다
*/

namespace BaekJoon._58
{
    internal class _58_04
    {

        static void Main4(string[] args)
        {

            StreamReader sr;
            int n, k;
            long[] sum;

            int[][] val;
            long ret1;
            int[] ret2;
            long[][] dp;

            Solve();
            void Solve()
            {

                Input();

                SetLine();

                GetRet();
            }

            void GetRet()
            {

                int last = 1;
                ret1 = 0;

                for (int i = 1; i <= n; i++)
                {

                    if (ret1 < dp[k % 2][i])
                    {

                        ret1 = dp[k % 2][i];
                        last = i;
                    }
                }

                ret2 = new int[k + 1];

                for (int i = k; i >= 1; i--)
                {

                    ret2[i] = last;
                    last = val[i][last];
                }

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                sw.Write($"{ret1}\n");


                for (int i = 1; i <= k; i++)
                {

                    sw.Write($"{ret2[i]} ");
                }

                sw.Close();
            }

            bool Chk1(int _k, int _i, int _j, long _x)
            {

                return dp[_k % 2][_i] - dp[_k % 2][_j] + sum[n] * (sum[_j] - sum[_i]) 
                    <= _x * (sum[_j] - sum[_i]);
            }

            bool Chk2(int _k, int _i, int _j, int _l)
            {

                return (dp[_k % 2][_i] - dp[_k % 2][_j] + sum[n] * (sum[_j] - sum[_i])) * (sum[_l] - sum[_j])
                    <= (dp[_k % 2][_j] - dp[_k % 2][_l] + sum[n] * (sum[_l] - sum[_j])) * (sum[_j] - sum[_i]);
            }

            void SetLine()
            {

                val = new int[k + 1][];

                val[0] = new int[n + 1];

                dp = new long[2][];
                dp[0] = new long[n + 1];
                dp[1] = new long[n + 1];

                int[] dq = new int[n + 1];

                for (int i = 1; i <= k; i++)
                {

                    int s = 0, e = 0;
                    dq[e] = i - 1;

                    Array.Fill(dp[i % 2], 0);
                    val[i] = new int[n + 1];

                    for (int j = i; j <= n; j++)
                    {

                        while (s < e && Chk1(i - 1, dq[s], dq[s + 1], sum[j])) 
                        {

                            s++;
                        }

                        int idx = dq[s];

                        val[i][j] = idx;
                        dp[i % 2][j] = dp[(i - 1) % 2][idx] + sum[j] * sum[idx] - sum[n] * sum[idx] + sum[n] * sum[j] - sum[j] * sum[j];

                        while(s < e && Chk2(i - 1, j, dq[e], dq[e - 1]))
                        {

                            e--;
                        }

                        dq[++e] = j;
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();

                sum = new long[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    sum[i] = sum[i - 1] + ReadInt();
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
// #pragma GCC optimize("O3,fast-math")
// #include <bits/stdc++.h>
using namespace std;
using ll=long long;

struct line { ll b; int a, c, i; };
int main() {
	ios::sync_with_stdio(0);cin.tie(0);
	int N, K;
	cin>>N>>K; K++;
	vector<int> A(N+1), P(N+1);
	for(int i=1; i<=N; i++) cin>>A[i], A[i]+=A[i-1];

	vector<line> X(N+1);
	auto f=[&](ll dt) {
		int n=0, p=0;
		auto add=[&](line t) {
			if(p<n && X[n-1].a==t.a) {
				if(X[n-1].b>t.b) t=X[n-1];
				n--;
			}
			while(p+1<n && double(X[n-2].b-X[n-1].b)*(t.a-X[n-1].a)>=float(X[n-1].b-t.b)*(X[n-1].a-X[n-2].a)) n--;
			X[n++]=t;
		};
		auto get=[&](ll x) {
			while(p+1<n && X[p].b-X[p+1].b<=x*(X[p+1].a-X[p].a)) p++;
			return X[p];
		};
		add({0, 0, 0, 0});
		for(int i=1; i<=N; i++) {
			auto[b,a,c,k]=get(2*(A[i]-A[N]));
			P[i]=k;
			add({2LL*(A[i]-A[N])*a + 2LL*(A[N]-A[i])*A[i] + b - dt, A[i], c+1, i});
		}
		return X[n-1].c;
	};
	auto go=[&](const vector<int>& Q) {
		ll x=0;
		for(int i=1; i<K+1; i++) x+=ll(A[Q[i]]-A[Q[i-1]])*A[Q[i-1]];
		cout<<x<<'\n';
		for(int i=1; i<K; i++) cout<<Q[i]<<' ';
		exit(0);
	};
	auto ga=[&](int k) {
		vector<int> Q(max(k+1, K+1));
		for(int i=N, j=k; i; i=P[i]) Q[j--]=i;
		if(k==K) go(Q);
		return Q;
	};

	ll l=0, r=5e16;
	while(l<r) {
		ll m=(l+r)/2;
		auto k=f(m*2+1);
		if(k>K) l=m+1;
		else if(k<K) r=m;
		else ga(k);
	}

	auto lk=f(2*r-1);
	auto L=ga(lk);
	auto rk=f(2*r+1);
	auto R=ga(rk);
	for(int i=1, p=0; i<=lk; i++) {
		while(R[p]<=L[i-1]) p++;
		if(R[p]>=L[i] && i+rk-p==K) {
			while(p<=rk) L[i++]=R[p++];
			go(L);
			break;
		}
	}
}
#elif other2
import java.io.BufferedReader;

import java.io.BufferedWriter;

import java.io.IOException;

import java.io.InputStreamReader;

import java.io.OutputStreamWriter;

import java.util.*;

//import java.math.*;

//import java.text.DecimalFormat;

public class Main {

		static BufferedReader rr = new BufferedReader(new InputStreamReader(System.in));

	static BufferedWriter ww = new BufferedWriter(new OutputStreamWriter(System.out));

	static int n, k;

	//static int[] in;

	static int[] A;

	static int[][] pos;

	//static long[][] po2;

	//static final int dn=32000, rn=31;

	

public static void main(String[] args) throws IOException { 		

  StringTokenizer st=new StringTokenizer(rr.readLine(), " ");

  //int[] ii=ltii();

  n=Integer.parseInt(st.nextToken()); 

  k=Integer.parseInt(st.nextToken());

  st=new StringTokenizer(rr.readLine(), " ");

  A=new int[n+1];

  for(int i=0;i<n;i++) A[i+1]=Integer.parseInt(st.nextToken());

  rr=null; st=null;

  

  for(int i=1;i<=n;i++){

		A[i]=A[i-1]+A[i];

	}

	

	long r=cdp();

	A=null;

	StringBuilder sb = new StringBuilder();

	sb.append(r); sb.append("\n");

	

	//for(int i=0;i<k;i++){

//	 for(int j=1;j<=n;j++) 

//	 System.out.print(pos[i][j]+" ");

//	 System.out.println();

//	}

	

	ArrayList<Integer> ra=new ArrayList<>();

	

  int fp=n;

  for(int i=k-1;i>=0;i--){

  	int c=pos[i][fp];

  	ra.add(c);

  	fp=c;

  }

  pos=null;

  int cu=0, si=ra.size();

  for(int i=si-1;i>=0;i--) {

  	cu=ra.get(i);

  	//ww.write(cu+" ");

  	if(cu==0) cu=1;

  	if(i<si-1){

  		int ncu=ra.get(i+1);

  		if(cu<=ncu) {

  			cu=ncu+1;

  		}

  	}

  	ra.set(i, cu);

  }

  for(int i=0;i<si;i++){

  	cu=ra.get(i);

  	cu=cu%(n-1);
    if(cu==0) cu=n-1;

  	ra.set(i, cu);

  }

  //ww.write("\n");

  Collections.sort(ra);

  for(int i=0;i<si;i++) {

  	cu=ra.get(i);

  	sb.append(cu);

  	if(i!=si-1) sb.append(" ");

  }

  ww.write(sb.toString());

	ww.flush();

	ww.close();

}

static long cdp() {

	double[][] s=new double[2][n+1];

	int[][] l1=new int[2][n+1], l3=new int[2][n+1];

	long[][] l2=new long[2][n+1];

	pos=new int[k][n+1];

	//po2=new long[k][n/31+1];

	long dp=0, in2=0;

	int ni=0, nj=0, x=0;

	double is=0;

	for(int i1=0;i1<=k;i1++){

		ni=0; nj=0;

	// inital condition needs

	for(int i=1;i<=n;i++){

		x=A[i];

		while(ni<=n && s[i1&1][ni]<x) ni++;

		ni--;

		if(ni<0) ni=0;

		dp=(long)x * l1[i1&1][ni] + l2[i1&1][ni];

		if(i1>0){

			pos[i1-1][i]=l3[i1&1][ni];

		}

		in2=dp-(long)x*x;

		while(nj>=0) {

			is=(double)(in2-l2[i1+1&1][nj])/(l1[i1+1&1][nj]-x);

			if(s[i1+1&1][nj]<is) break ;

			nj--;

		}

		nj++;

		s[i1+1&1][nj]=is;

		l1[i1+1&1][nj]=x; l2[i1+1&1][nj]=in2; 

		l3[i1+1&1][nj]=i;

	}

	

	}

	return dp;

}

}
#endif
}
