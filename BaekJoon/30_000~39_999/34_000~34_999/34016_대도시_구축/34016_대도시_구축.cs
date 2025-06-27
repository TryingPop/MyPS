using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 27
이름 : 배성훈
내용 : 대도시 구축
    문제번호 : 34016번

    그리디, 애드 혹 문제다.
    인덱스 입력 실수만으로 5번 가까이 틀렸다.
    강이 2개인데 0번 해야할 것을 1번으로 하고,
    1번할 것은 0번으로 했었다;

    크루스칼 알고리즘 방식으로 1 - N 형태로 그룹을 만드는게 최소임을 알 수 있다.
    이제 강이 있는 경우 케이스를 본다.
    낮은 쪽이 2번 이상으로만 이루어진 경우
    1번과연결하기에 무시해도 된다.

    이후 1번만 이루어진 경우를 모두 확인했고, 
    1 - 2번 함께 잇는 경우를 확인했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1733
    {

        static void Main1733(string[] args)
        {

            // 34016
            int n, m;
            (int f, int t)[] river;

            Input();

            GetRet();

            void GetRet()
            {

                long ret = ((n + 4L) * (n - 1L)) / 2;
                if (m == 1)
                {

                    if (river[0].f == 1)
                    {

                        if (river[0].t == 2) ret += 2;
                        else ret++;
                    }
                }
                else if (m == 2)
                {

                    Array.Sort(river, (x, y) =>
                    {

                        int ret = x.f.CompareTo(y.f);
                        if (ret == 0) ret = x.t.CompareTo(y.t);
                        return ret;
                    });

                    if (river[0].f == 1)
                    {

                        // 1 - 2 연결인 경우
                        // 2 - 3으로 바꿔야 한다
                        if (river[0].t == 2) ret += 2;
                        // 1 - N 인경우
                        // 2 - N으로 바꾼다.
                        else ret++;
                    }

                    // 1 - b1, 1 - b2인 연결임
                    if (river[1].f == 1)
                    {

                        // 앞에서 1 - 3이 3 - 4로 바껴야 한다.
                        // 현재 1 - 3인경우 정렬 상태 상 river[0]번은 1 - 2임이 자명하다.
                        // 
                        if (river[1].t == 3) ret += 2;
                        else ret++;
                    }
                    else if (river[1].f == 2)
                    {

                        if (river[0].t == river[1].t)
                        {

                            if (river[1].t == 3) ret += 2;
                            else ret++;
                        }
                        else if (river[1].t == 3 && river[0].t == 2) ret++;
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                river = new (int f, int t)[m];
                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    river[i] = (f, t);
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
// #include<stdio.h>
// #include<vector>
// #include<algorithm>
using namespace std;
vector<int> cnt[5];
main(){
	long long n,m;scanf("%lld %lld",&n,&m);
	long long ans = (n+2)*(n-1)/2 + n-1;
	while(m--){
		int u,v;scanf("%d %d",&u,&v);
		if(u<5)cnt[u].push_back(v);
		if(v<5)cnt[v].push_back(u);
		if(u==1)ans-=v+1;
	}
	sort(cnt[1].begin(),cnt[1].end(),[](auto a, auto b){return a>b;});
	for(auto j:cnt[1]){
		auto f = [](int k, int t){
			for(auto i:cnt[t])if(i==k)return 0;
			return 1;
		};
		for(int i = 2;;i++)if(j!=i&&f(j,i)){
			ans+=i+j;
			if(i<5)cnt[i].push_back(j);
			if(j<5)cnt[j].push_back(i);
			break;
		}
	}
	
	printf("%lld",ans);
}

#endif
}
