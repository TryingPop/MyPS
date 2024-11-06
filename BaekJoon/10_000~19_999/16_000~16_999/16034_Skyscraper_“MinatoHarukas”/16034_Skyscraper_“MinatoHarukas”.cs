using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 30
이름 : 배성훈
내용 : Skyscraper “MinatoHarukas”
    문제번호 : 16034번

    수학, 브루트포스 문제다
    홀수 짝수 나눠서 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_1088
    {

        static void Main1088(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;
            int n;

            Solve();
            void Solve()
            {

                Init();

                while (Input())
                {

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                int ret1 = n;
                int ret2 = 1;

                Div();

                sw.Write($"{ret1} {ret2}\n");

                void Div()
                {

                    // 합과 짝수 찾는다
                    int chk = n << 1;
                    for (int i = 2; i * i <= chk; i++)
                    {

                        if (chk % i != 0) continue;
                        int j = chk / i;

                        ChkSubSum(i, j);
                        ChkSubSum(j, i);
                    }

                    void ChkSubSum(int _sum, int _len)
                    {

                        // 길이가 짝수인데 합이 짝수는 불가능하다!
                        if (((_len & 1) == 0 && (_sum & 1) == 0) || _len < ret2) return;
                        int center = (_sum + 1) >> 1;
                        int start = center - (_len >> 1);
                        if (start < 1) return;
                        ret1 = start;
                        ret2 = _len;
                    }
                }
            }

            bool Input()
            {

                n = ReadInt();
                return n > 0;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
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
                    if (c == ' ' || c == '\n') return true;
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
// #include<bits/stdc++.h>
using namespace std;
int main()
{
	int maxh,st;
	while(1)
	{
		int x;
		scanf("%d",&x);
		if(x==0){return 0;}
		maxh=1,st=x;
		for(int i=1;i*i<=x;i++)
		{
			if(x%i==0)
			{
				int a=i,b=x/i;
				if(a%2==1&&b%2==1)
				{
					int ST=a-b/2;
					int H=b;
					if(H>maxh&&ST>0) maxh=H,st=ST;
					ST=b-a/2;
					H=a;
					if(H>maxh&&ST>0) maxh=H,st=ST;
					ST=a/2-b+1;
					H=2*b;
					if(H>maxh&&ST>0) maxh=H,st=ST;
					ST=b/2-a+1;
					H=2*a;
					if(H>maxh&&ST>0) maxh=H,st=ST;
				}
				if(a%2==1&&b%2==0)
				{
					int ST=b-a/2;
					int H=a;
					if(H>maxh&&ST>0) maxh=H,st=ST;
					ST=a/2-b+1;
					H=b*2;
					if(H>maxh&&ST>0) maxh=H,st=ST;
				}
				if(b%2==1&&a%2==0)
				{
					int ST=a-b/2;
					int H=b;
					if(H>maxh&&ST>0) maxh=H,st=ST;
					ST=b/2-a+1;
					H=a*2;
					if(H>maxh&&ST>0) maxh=H,st=ST;
				}
			}
		}
		printf("%d %d\n",st,maxh);
	}
}
#endif
}
