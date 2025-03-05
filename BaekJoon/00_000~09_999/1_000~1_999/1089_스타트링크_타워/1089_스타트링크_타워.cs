using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 13
이름 : 배성훈
내용 : 스타트링크 타워
    문제번호 : 1089번

    구현, 수학, 확률론 문제다.
    아이디어는 다음과 같다. 먼저 전광판 켜지는 상황을 비트로 저장한다.
    그래서 비트로 가능한 숫자들을 모두 찾는다.

    이후 분배법칙으로 각 자릿수마다 몇 번 출현하는지 확인해도 된다.
    그래서 각 자리수마다 몇 번 출현하는지 확인하니 전체 경우의 수 / 현재 가능한 갯수 
    임을 계산을 통해 알 수 있다.

    예를 들어 100의 자리 2개, 10의 자리 3개, 1의 자리 2개라 하자\
    전체 경우의 수는 2 x 3 x 2 = 12이고
    100의 자리가 나타나는 경우는 10의 자리 3개 x 1의 자리 2개 = 6번씩 등장한다.
    10의 자리는 100의 자리 2개 x 1의 자리 2개 = 4번씩 등장한다.
    1의 자리는 100의자리 2개 x 10의 자리 3개 = 6번씩 등장한다.

    이렇게 바로 평균을 구해줬다.
    오차 10^-5까지 허용한다는데, 나올 수 있는 숫자의 자릿수는 10^9이다.
    그리고 double은 15 ~ 16자리까지 정밀도를 가지므로
    double로 이상없이 진행했다.

    만약 오차가 10^-9라면, sum을 구하고, epsilon을 둬서 연산하면 된다.
    혹은 C#에서는 decimal은 24자리의 정밀도를 보장하므로 decimal 자료형을 이용해도 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1327
    {

        static void Main1327(string[] args)
        {

            int n;
            int[][] bulb;
            int[] sumArr = new int[1 << 16];
            int[] cntArr = new int[1 << 16];
            int[] num = new int[10];

            SetArr();

            Input();

            GetRet();

            void GetRet()
            {

                double ret = 0;
                for (int i = 0; i < n; i++)
                {

                    int start = SetStart(i);

                    int chk = 0;
                    for (int r = 0; r < 5; r++)
                    {

                        for (int c = 0; c < 3; c++)
                        {

                            if (bulb[r][c + start] == 0) continue;
                            int idx = GetInt(r, c);
                            chk |= idx;
                        }
                    }

                    if (cntArr[chk] == 0)
                    {

                        ret = -1.0;
                        break;
                    }

                    ret = ret * 10 + sumArr[chk] / (double)(cntArr[chk]);
                }

                Console.Write($"{ret:0.#######}");

                int SetStart(int _i) => _i << 2;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());

                bulb = new int[5][];
                for (int r = 0; r < 5; r++)
                {

                    string temp = sr.ReadLine();
                    bulb[r] = new int[temp.Length];
                    for (int c = 0; c < temp.Length; c++)
                    {

                        if (temp[c] == '#') bulb[r][c] = 1;
                    }
                }
            }

            void SetArr()
            {

                SetNum();

                for (int i = 0; i < sumArr.Length; i++)
                {

                    for (int j = 0; j < 10; j++)
                    {

                        if ((i | num[j]) != num[j]) continue;
                        sumArr[i] += j;
                        cntArr[i]++;
                    }
                }

                void SetNum()
                {

                    num[0] = GetInt(0, 0) | GetInt(0, 1) | GetInt(0, 2)
                        | GetInt(1, 0) | GetInt(1, 2)
                        | GetInt(2, 0) | GetInt(2, 2)
                        | GetInt(3, 0) | GetInt(3, 2)
                        | GetInt(4, 0) | GetInt(4, 1) | GetInt(4, 2);

                    num[1] = GetInt(0, 2)
                        | GetInt(1, 2)
                        | GetInt(2, 2)
                        | GetInt(3, 2)
                        | GetInt(4, 2);

                    num[2] = GetInt(0, 0) | GetInt(0, 1) | GetInt(0, 2)
                        | GetInt(1, 2)
                        | GetInt(2, 0) | GetInt(2, 1) | GetInt(2, 2)
                        | GetInt(3, 0)
                        | GetInt(4, 0) | GetInt(4, 1) | GetInt(4, 2);

                    num[3] = GetInt(0, 0) | GetInt(0, 1) | GetInt(0, 2)
                        | GetInt(1, 2)
                        | GetInt(2, 0) | GetInt(2, 1) | GetInt(2, 2)
                        | GetInt(3, 2)
                        | GetInt(4, 0) | GetInt(4, 1) | GetInt(4, 2);

                    num[4] = GetInt(0, 0) | GetInt(0, 2)
                        | GetInt(1, 0) | GetInt(1, 2)
                        | GetInt(2, 0) | GetInt(2, 1) | GetInt(2, 2)
                        | GetInt(3, 2)
                        | GetInt(4, 2);

                    num[5] = GetInt(0, 0) | GetInt(0, 1) | GetInt(0, 2)
                        | GetInt(1, 0)
                        | GetInt(2, 0) | GetInt(2, 1) | GetInt(2, 2)
                        | GetInt(3, 2)
                        | GetInt(4, 0) | GetInt(4, 1) | GetInt(4, 2);

                    num[6] = GetInt(0, 0) | GetInt(0, 1) | GetInt(0, 2)
                        | GetInt(1, 0)
                        | GetInt(2, 0) | GetInt(2, 1) | GetInt(2, 2)
                        | GetInt(3, 0) | GetInt(3, 2)
                        | GetInt(4, 0) | GetInt(4, 1) | GetInt(4, 2);

                    num[7] = GetInt(0, 0) | GetInt(0, 1) | GetInt(0, 2)
                        | GetInt(1, 2)
                        | GetInt(2, 2)
                        | GetInt(3, 2)
                        | GetInt(4, 2);

                    num[8] = GetInt(0, 0) | GetInt(0, 1) | GetInt(0, 2)
                        | GetInt(1, 0) | GetInt(1, 2)
                        | GetInt(2, 0) | GetInt(2, 1) | GetInt(2, 2)
                        | GetInt(3, 0) | GetInt(3, 2)
                        | GetInt(4, 0) | GetInt(4, 1) | GetInt(4, 2);

                    num[9] = GetInt(0, 0) | GetInt(0, 1) | GetInt(0, 2)
                        | GetInt(1, 0) | GetInt(1, 2)
                        | GetInt(2, 0) | GetInt(2, 1) | GetInt(2, 2)
                        | GetInt(3, 2)
                        | GetInt(4, 0) | GetInt(4, 1) | GetInt(4, 2);
                }
            }

            int GetInt(int _r, int _c) => 1 << (_r * 3 + _c);
        }
    }

#if other
// #include<stdio.h>
int main(){
    int N,S,D[10]={31599,4681,29671,29647,23497,31183,31215,29257,31727,31695},n,e,i,j,k;
    double F=0;
    char E[5][36];
    for(scanf("%d\n",&N),i=0;i<5;i++)
        scanf("%s",E[i]);
    for(k=0;k<N;k++,F=F*10+(double)S/n){
        for(e=i=0;i<5;i++)
            for(j=0;j<3;j++)
                e+=(E[i][j+k*4]=='#')<<14-i*3-j;
        for(S=n=i=0;i<10;i++)
            if(e==(D[i]&e))
                S+=i,n++;
        for(i=0;i<5;i++)
            if(E[i][3+k*4]=='#')
                n=0;
        if(!n)break;
    }
    printf(n?"%lf":"-1",F);
}
#endif
}
