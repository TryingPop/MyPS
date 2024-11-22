using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 22
이름 : 배성훈
내용 : 실 전화기
    문제번호 : 23039번

    그래프 이론, 평면 그래프, 많은 조건 분기 문제다
    아이디어는 다음과 같다.
    먼저 이동할 필요가 있는 경우와 이동할 필요가 없는 경우를 구분했다.
    토끼 수는 5마리로 고정되어 있고, 내부에 별표 선이 교차하는 경우에 토끼굴을 옮겨야 한다.
    내부 별표 선에 교차가 없으면 0회로 끝난다.

    이후 불가능한 경우를 찾아봤다.
    우선 예제 3의 경우에 모든 선분이 있는 경우 불가능하다고 나와있었다.
    선분을 하나씩 빼서 되는지 확인했다.
    해당 선분에서 교차가 해결되면 해당 선분 미만에서도 해당 횟수 이하로 해결되기 때문이다.

    외부 선분 1개를 뺀 경우를 보자.
    외부 선이 1개 빠진경우 외부선이 빠진 두점을 각각 A, B라 하자
    A의 경우 선분 3개와 연결되어져 있다.
    그러면 중앙에 있는 선과 연결된 점 C 방향으로 A와 C 거리보다 더 이동한 장소에 A를 이동시킨다.
    그리고 B는 A, B, C이외의 두 점을 D, E라 하면 CDE는 선분이 있고 삼각형이 된다.
    CDE내부에 B를 이동시키면 2회로 완성된다.

    이제 내부 선을 하나 빼보자.
    그러면 빠진 선분의 두 점을 A, B라 하고, 이외 점을 C, D, E라 하자.
    그러면 빠진 선분을 기준으로 C, D, E는 2개 1개로 구분된다.
    여기서 1개있는 점을 C라 지칭하자.
    이제 D에 대해 D와 A, B 중 거리가 먼 것을 B라보고,
    선분 BC의 중심 방향으로 D를 아주 멀리 이동시킨다.
    기존 BC와 D의 거리보다 더 멀리 면 된다.
    그러면 모든 선분이 교차하지 않는다.

    그래서 모두 있는 경우를 제외하고는 해결이 가능하다.
    이제 해결 되는 경우에서 최소값을 찾아봤다.

    우선 많아야 2번임을 확인할 수 있다.
    이제 내부에 별이 안되는 경우를 확인하니, 1회로 모두 가능했다.
    이외의 경우는 2회이지 않을까 추론했고, 제출하니 통과했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1128
    {

        static void Main1128(string[] args)
        {

            bool[][] conn;
            int n;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                if (n == 10) Console.Write(-1);
                else
                {

                    int cnt = Chk();
                    if (cnt == 0) Console.Write(0);
                    else if (cnt == 5) Console.Write(2);
                    else Console.Write(1);
                }

                int Chk()
                {

                    int ret = 0;
                    if (conn[0][2])
                    {

                        if (conn[1][3]) ret++;
                        if (conn[1][4]) ret++;
                    }

                    if (conn[0][3])
                    {

                        if (conn[4][2]) ret++;
                        if (conn[4][1]) ret++;
                    }

                    if (conn[1][3] && conn[2][4]) ret++;

                    return ret;
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput());

                conn = new bool[5][];
                for (int i = 0; i < 5; i++)
                {

                    conn[i] = new bool[5];
                }

                n = ReadInt();
                for (int i = 0; i < n; i++)
                {

                    int f = ReadInt() - 1;
                    int b = ReadInt() - 1;

                    conn[f][b] = true;
                    conn[b][f] = true;
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
    }

#if other
// #include<stdio.h>
int d[6][6],i,n,w,x,y;
int main(){
    scanf("%d",&n);
    for(i=0;i<n;++i){
        scanf("%d%d",&x,&y);
        d[x][y]=d[y][x]=1;
    }
    x=d[1][3]+d[2][4]+d[3][5]+d[4][1]+d[5][2];
    y=d[1][3]&&d[2][4];
    y|=d[2][4]&&d[3][5];
    y|=d[3][5]&&d[4][1];
    y|=d[4][1]&&d[5][2];
    y|=d[5][2]&&d[1][3];
    if(n==10)w=-1;
    else if(x==5)w=2;
    else w=y;
    printf("%d",w);
    return 0;
}
#endif
}
