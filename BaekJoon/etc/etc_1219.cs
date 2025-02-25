using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 26
이름 : 배성훈
내용 : 이상한 트리 해싱
    문제번호 : 31928번

    애드 혹, 해 구성하기 문제다.
    문제를 분석하면 2^(2^k), k > 1의 경우만 해가 존재한다.

    그리고 부모를 위로 자식을 밑으로 하는 경우
    전체 노드의 수가 3인 경우 |(일직선 형태)로 이어진 트리와 
    ㅅ형태로 이어진 트리의 해싱 값이 2^2^2 = 2^(2 * 2)로 같다.

    그래서 루트 이외의 노드를 루트를 부모로하는 트리와
    루트와 2번 정점을 제외한 모든 노드를 루트를 부모로 하고
    2번 정점은 루트가 아닌 다른 노드로 이어주면 해싱 값이 같음을 알 수 있다.

    이렇게 찾으니 이상없이 통과한다.
    다만 1 << 32에서 오버플로우로 1번 틀렸다.
    1L << 32로 바꾸니 이상없이 통과한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1219
    {

        static void Main1219(string[] args)
        {

            Solve();
            void Solve()
            {

                long n = long.Parse(Console.ReadLine());

                int[] chk = { 4, 8, 16, 32 };
                int idx = -1;
                for (int i = 0; i < chk.Length; i++)
                {

                    if (1L << chk[i] != n) continue;
                    idx = i;
                    break;
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                if (idx == -1)
                {

                    sw.Write(-1);
                    return;
                }

                int len = idx + 3;
                sw.Write($"{len} {len}\n");
                for (int i = 1; i < len; i++)
                {

                    sw.Write("1 ");
                }
                sw.Write('\n');
                for (int i = 1; i < len - 1; i++)
                {

                    sw.Write("1 ");
                }

                sw.Write(2);
            }
        }
    }
#if other
// #include<stdio.h>
long long i,h;
int main(){
    scanf("%lld",&h);
    if(h==1LL<<4){
        printf("3 3\n1 2\n1 1");
    }
    else if(h==1LL<<8){
        printf("4 4\n1 1 2\n1 1 1");
    }
    else if(h==1LL<<16){
        printf("5 5\n1 1 1 2\n 1 1 1 1");
    }
    else if(h==1LL<<32){
        printf("6 6\n1 1 1 1 2\n1 1 1 1 1");
    }
    else printf("-1");
    return 0;
}
#endif
}
