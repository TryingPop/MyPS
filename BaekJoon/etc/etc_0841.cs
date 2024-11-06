using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 26
이름 : 배성훈
내용 : 출근 기록
    문제번호 : 14238번

    dp, dfs 문제다
    처음에 만들어야 하는 줄 알아 접근하려니
    규칙이 많이 복잡해 포기했다

    그래서 고민하다가 안떠올라 힌트를 보고
    다차원 dp를 활용해 하나씩 만들어가며 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0841
    {

        static void Main841(string[] args)
        {

            string str;
            int a, b, c, n;
            bool[,,,,] dp;
            char[] ret;

            Solve();

            void Solve()
            {

                Init();

                if (DFS()) Console.Write(new string(ret));
                else Console.Write(-1);
            }

            void Init()
            {

                str = Console.ReadLine();
                n = str.Length;
                a = 0;
                b = 0;
                c = 0;

                for (int i = 0; i < str.Length; i++)
                {

                    if (str[i] == 'A') a++;
                    else if (str[i] == 'B') b++;
                    else c++;
                }

                ret = new char[n];

                dp = new bool[a + 1, b + 1, c + 1, 4, 4];
            }

            bool DFS(int _a = 0, int _b = 0, int _c = 0, int _prev1 = 0, int _prev2 = 0)
            {

                if (_a == a && _b == b && _c == c) return true;

                if (dp[_a, _b, _c, _prev1, _prev2]) return false;
                dp[_a, _b, _c, _prev1, _prev2] = true;

                if (_a < a)
                {

                    ret[_a + _b + _c] = 'A';
                    if (DFS(_a + 1, _b, _c, 1, _prev1)) return true;
                }

                if (_b < b)
                {

                    ret[_a + _b + _c] = 'B';
                    if (_prev1 != 2 && DFS(_a, _b + 1, _c, 2, _prev1)) return true;
                }

                if (_c < c)
                {

                    ret[_a + _b + _c] = 'C';
                    if (_prev1 != 3 && _prev2 != 3 && DFS(_a, _b, _c + 1, 3, _prev1)) return true;
                }

                return false;
            }

        }
    }

#if other
// #include<stdio.h>
// #include<string.h>
int main(void)
{
    int length,count[3]={0},yes=1,min,remain;
    char input[55]={0},answer[55]={0};
    scanf("%s",&input);//출근 기록 입력
    length=strlen(input);
    for(int i=0; i<length; i++)
    {//A,B,C의 개수를 센다.
        count[input[i]-'A']++;
    }
    min=(count[2]+(count[2]-1)*2);//C부터 먼저 채운다. 최소 C의 개수 및 각 C 사이에 최소 2칸 이상의 공간이 필요하다.
    remain=length-min;//C를 모두 채우고 남는 여분의 칸.
    if(remain<0)
    {//만일 자리가 부족하면 불가능이다.
        yes=0;
        goto skip;
    }
    for(int i=0; i<length; i+=3)
    {//C부터 채운다. C 사이에 최소 두 칸의 공간을 띄운다.
        if(count[2]==0)
        {//C를 모두 채웠으면 종료한다.
            break;
        }
        if(remain>0)
        {//여분의 칸이 있다면, 한 칸을 띄워서 넣어준다.(이러면 나중에 B가 하나 더 들어간다)
            remain--;
            i++;
        }
        count[2]--;//카운트를 줄이고 현 자리에 C를 채운다.
        answer[i]='C';
    }
    for(int i=0; i<length; i+=2)
    {//다음 B를 채운다. B 사이에 최소 한 칸의 공간이 있어야 한다.
        if(count[1]==0)
        {//모든 B를 채웠으면 종료한다.
            break;
        }
        if(answer[i]!=0)
        {//C가 놓인 칸은 건너뛴다.
            i++;
        }
        if(i<length)
        {//B가 칸을 넘어가지 않았다면 채운다.
            count[1]--;
            answer[i]='B';
        }
    }
    if(count[1]>0)
    {//B를 다 못 채웠을 경우 불가능이다.
        yes=0;
        goto skip;
    }
    for(int i=0; i<length; i++)
    {//남은 자리를 A로 채운다.
        if(answer[i]==0)
        {
            answer[i]='A';
        }
    }
    skip:
    yes==1 ? printf("%s",answer) : printf("-1");//최종 정답을 출력한다.
    return 0;
}

#endif
}
