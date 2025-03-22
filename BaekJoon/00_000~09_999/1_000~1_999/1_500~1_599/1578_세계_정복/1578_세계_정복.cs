using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 4
이름 : 배성훈
내용 : 세계 정복
    문제번호 : 1578번

    매개변수 탐색 문제다.
    만약 A개의 그룹을 지을 수 있다면, 각 나라에서는 많아야 A명의 사람을 뽑을 수 있다.
    비둘기집 원리로 A + 1명이 뽑히면 그룹은 A개인데 적어도 1곳에 중복되고 이는 불가능하다.
    그래서 각 A 이하로 최대한 뽑으면서 그룹이 되는지 확인하면 된다.
    이렇게 A그룹을 만들 수 있는 인원이되면 해당 그룹을 만들 수 있다.

    단순히 0부터 2^63까지 순차적으로 찿기에는 많다.
    해당 그룹이 만들어지는 경우를 보면, P개의 그룹이 만들어지면 1개의 그룹을 제외해 P - 1개의 그룹도 만들 수 있다.
    반면 Q개의 그룹이 불가능하면 Q + 1개의 그룹 만드는 것도 불가능하다.
    그룹 갯수에 따라 가능한 경우를 1, 불가능한 경우를 0으로 하면 해집합은 정렬된 집합을 확인할 수 있다.

    만약 3개의 그룹까지 만들 수 있다면 다음과 같은 표가 된다.

    해 집합이 정렬된 집합이므로 정답을 찾을 때 매개변수탐색을 이용하면 된다.
    가능한 경우는 최대 10억명이 들어오고 많아야 50개의 그룹이므로 정답은 10^11보다는 작다.
    그래서 매개변수 탐색의 끝을 10^11로 잡아 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1093
    {

        static void Main1093(string[] args)
        {

            int n, k;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                long l = 0, r = 100_000_000_000;

                while (l <= r)
                {

                    long mid = (l + r) >> 1;

                    long ret = 0;
                    for (int i = 0; i < n; i++)
                    {

                        ret += Math.Min(arr[i], mid);
                    }

                    if (mid * k <= ret) l = mid + 1;
                    else r = mid - 1;
                }

                Console.Write(l - 1);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                k = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();

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

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
typedef long long ll;
ll a[51],i,k,n,lo=1,mi,hi,s;
int main(){
    scanf("%lld%lld",&n,&k);
    for(i=0;i<n;++i)scanf("%lld",a+i);
    hi=n*1000000000LL+3;
    while(lo<hi){
        mi=(lo+hi)>>1;
        s=0;
        for(i=0;i<n;++i)s+=a[i]<mi?a[i]:mi;
        if(s<mi*k)hi=mi;
        else lo=mi+1;
    }
    printf("%lld",lo-1);
    return 0;
}
#endif
}
