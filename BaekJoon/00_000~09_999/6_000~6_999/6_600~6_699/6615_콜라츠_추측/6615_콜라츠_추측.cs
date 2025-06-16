using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 14
이름 : 배성훈
내용 : 콜라츠 추측
    문제번호 : 6615번

    브루트포스, 트리, 최소 공통조상 문제다.
    2^58 이하는 1로 간다고 나와있다.
    그래서 트리로 보면 모두 부모 1로 감을 알 수 있다.
    먼저 브루트포스로 진행했다.
    이전 경로를 기억해야하는데, 이는 dictionary로 저장했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1336
    {

        static void Main1336(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            long a, b;
            Dictionary<long, int> visit = new(1_000_000);

            while (Input())
            {

                GetRet();
            }

            bool Input()
            {

                string[] temp = sr.ReadLine().Split();
                a = int.Parse(temp[0]);
                b = int.Parse(temp[1]);

                return a != 0 || b != 0;
            }

            void GetRet()
            {

                long A = a, B = b;
                visit.Clear();
                int cur = 0;
                visit[A] = cur++;

                while (A != 1)
                {

                    if (A % 2 == 0) A /= 2;
                    else A = A * 3 + 1;

                    visit[A] = cur++;
                }

                cur = 0;
                while (!visit.ContainsKey(B))
                {

                    cur++;
                    if (B % 2 == 0) B /= 2;
                    else B = B * 3 + 1;
                }

                sw.Write($"{a} needs {visit[B]} steps, {b} needs {cur} steps, they meet at {B}\n");
            }
        }
    }

#if other
// #include <iostream>
// #define MAX 530

typedef long long ll;
using namespace std;

ll arr1[MAX];
ll arr2[MAX];

int main() {
    while(1){
        ll n, m;
        int x=0, y=0;
        scanf("%lld %lld", &n, &m);

        ll dn=n, dm=m;

        if(n==0 && m==0) break;

        while(n!=1){
            arr1[x]=n;
            if(n%2==0) n/=2;
            else n=n*3+1;
            x++;
        }
        arr1[x]=1;

        while(m!=1){
            arr2[y]=m;
            if(m%2==0) m/=2;
            else m=m*3+1;
            y++;
        }
        arr2[y]=1;

        while(arr1[x]==arr2[y]){
            x--, y--;
            if(x==-1 || y==-1) break;
        }

        printf("%lld needs %d steps, %lld needs %d steps, they meet at %lld\n", dn, x+1, dm, y+1, arr1[x+1]);
    }

    return 0;
}
#endif
}
