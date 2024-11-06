using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 28
이름 : 배성훈
내용 : 비 오는 날
    문제번호 : 31066번

    구현, 그리디, 시뮬레이션 문제다
    n, m, k 범위가 1 ~ 10이므로 시뮬레이션 돌려 해결했다
    
*/

namespace BaekJoon.etc
{
    internal class etc_0643
    {

        static void Main643(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int test = ReadInt();
                int n, m, k;
                while(test-- > 0)
                {

                    n = ReadInt();
                    m = ReadInt();
                    k = ReadInt();

                    if (m == k && k == 1 && n > 1) sw.Write($"{-1}\n");
                    else
                    {

                        int ret = 0;
                        while (true)
                        {


                            n -= m * k;
                            ret++;
                            if (n <= 0) break;
                            n++;
                            ret++;
                        }

                        sw.Write($"{ret}\n");
                    }
                }

                sr.Close();
                sw.Close();
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

int main(){
    int t;scanf("%d",&t);
    while(t--){
        int a,b,c;scanf("%d%d%d",&a,&b,&c);
        if(a==1)printf("1\n");
        else if(b*c==1)printf("-1\n");
        else printf("%d\n",(a-2)/(b*c-1)*2+1);
    }
    return 0;
}
#endif
}
