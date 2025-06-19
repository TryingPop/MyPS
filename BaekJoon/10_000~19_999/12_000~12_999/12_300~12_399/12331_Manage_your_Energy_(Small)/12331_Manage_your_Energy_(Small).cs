using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 2
이름 : 배성훈
내용 : Manage your Energy (Small)
    문제번호 : 12331번

    dp, 브루트포스 문제다.
    문제를 잘못 분석해 4번 틀렸다.
    가장 큰 값에 모든 에너지를 쓰고 나머지는 회복 에너지 쓰면 되는거 아니야 ? 
    생각해서 쓰는 방식으로 진행했다.

    그런데 5 4 5 1이고 최대 에너지가 5, 충전량이 2라 하자.
    그러면 최대한 사용한다면 처음 5에서 풀 에너지를 사용하고,
    나머지는 충전량만큼 사용할 것이다.
    5 x 5 + 4 x 2 + 5 x 2 + 1 x 2 = 45가 나온다.
    그런데 4에서 에너지를 쓰지않고 다음 5에서 다 사용한다면
    5 x 5 + 4 x 0 + 5 x 4 + 1 x 2 = 47으로 반례가 바로 나온다;

    그래서 모든 경우를 확인하는데 배낭 형식으로 진행해 풀었다;
*/

namespace BaekJoon.etc
{
    internal class etc_1239
    {

        static void Main1239(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int e, r, n, ret;
            int[] cur, next;
            int[] val;

            Solve();
            void Solve()
            {

                Init();

                int t = ReadInt();

                for (int i = 1; i <= t; i++)
                {

                    Input();

                    GetRet();
                    sw.Write($"Case #{i}: {ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                Array.Fill(cur, -1);
                Array.Fill(next, -1);

                cur[e] = 0;

                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j <= e; j++)
                    {

                        if (cur[e] == -1) continue;
                        
                        for (int k = 0; k <= j; k++)
                        {

                            int use = j - k;
                            int nIdx = j - use + r;
                            if (nIdx > e) nIdx = e;
                            next[nIdx] = Math.Max(next[nIdx], cur[j] + use * val[i]);
                        }
                    }

                    for (int j = 0; j <= e; j++)
                    {

                        cur[j] = next[j];
                        next[j] = -1;
                    }
                }

                ret = 0;
                for (int i = 0; i <= e; i++)
                {

                    ret = Math.Max(ret, cur[i]);
                }
            }

            void Input()
            {

                e = ReadInt();
                r = ReadInt();
                n = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    val[i] = ReadInt();
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                val = new int[10];
                cur = new int[6];
                next = new int[6];
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
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

#if other
// #include<cstdio>
// #include<vector>
int T,N;
long long E,R;
std::vector<long long> v;
int gnm(int id){
	for(int i=id+1;i<N and i<=id+(E+R-1)/R;i++)if(v[id]<=v[i])return i;
	return 0;
}
long long gmg(){
	long long c=E,ans=0;
	for(int i=0;i<N;i++){
		int m=gnm(i);
		if(m){
			long long x=c+(m-i)*R-E;
			if(x<0)x=0;
			else if(x>c)x=c;
			ans+=x*v[i];
			c+=R-x;
			if(c>E)c=E;
		}
		else{
			ans+=c*v[i];
			c=R;
		}
	}
	return ans;
}
int main(){
	scanf("%d",&T);
	for(int cas=1;cas<=T;cas++){
		scanf("%lld%lld%d",&E,&R,&N);
		if(E<R)R=E;v.resize(N);
		for(int i=0;i<N;i++)scanf("%lld",&v[i]);
		printf("Case #%d: %lld\n",cas,gmg());
	}
	return 0;
}
#endif
}
