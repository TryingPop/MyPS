using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 27
이름 : 배성훈
내용 : 오영식의 보물
    문제번호 : 1631번

    수학, 구현, 재귀, 조합론 문제다
    아이디어는 다음과 같다
    아래 링을 a에서 b로 옮길러면 a의 위에 모든 링들을
    c로 옮겨야한다

    그리고 맨 아래 링을 b에 옮긴다
    초기 상태에서 최소 경우의 수는 2^n이다
    여기서 n은 위에 있는 링의 개수다

    만약 찾는 횟수가 m번째가 2^n보다 작다면
    해당 링은 고정시키고
    위에 링들은 a -> c로 옮기는 연산을 진행하는 상태를 출력해야한다

    반면 찾는 횟수가 m이 2^n보다 크다면
    밑에 링을 이동시키고 위에 시행횟수를 빼준다
    그리고 위에 링이 있는 곳 c에서 원하는 위치로 이동할 수 있는지 확인했다
    이렇게 아래 층부터 위층으로 한개씩 찾아갔다

    to의 값을 제대로 설정안해 
    zero division 에러로 7번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0915
    {

        static void Main915(string[] args)
        {

            StreamReader sr;
            int n, m;
            int[] ret;
            int from, to;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                from = 1;
                to = 3;
                int i;
                for (i = n - 1; i >= 0; i--)
                {

                    int cur = 1 << i;

                    if (m < cur)
                    {

                        if (ret[i] == from) continue;
                        else
                        {

                            to = ret[i];
                            break;
                        }
                    }

                    m -= cur;
                    from = GetEmpty(from, ret[i]);
                }

                for (; i >= 0; i--)
                {

                    int cur = 1 << i;

                    if (m < cur)
                    {

                        ret[i] = from;
                        to = GetEmpty(from, to);
                        continue;
                    }

                    ret[i] = to;
                    from = GetEmpty(from, to);
                    m -= cur;
                }

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    for (i = 0; i < n; i++)
                    {

                        if (ret[i] == 1) sw.Write('A');
                        else if (ret[i] == 2) sw.Write('B');
                        else if (ret[i] == 3) sw.Write('C');
                    }
                }
            }

            int GetEmpty(int _from, int _to)
            {

                return 6 / (_from * _to);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                ret = new int[n];
                for (int i = 0; i < n; i++)
                {

                    ret[i] = ReadPos();
                }

                sr.Close();
            }

            int ReadPos()
            {

                int c = sr.Read();
                switch (c)
                {

                    case 'A':
                        return 1;

                    case 'B':
                        return 2;

                    case 'C':
                        return 3;

                    default:
                        return -1;
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// #include<stdio.h>
using lld=long long int;
int n;
lld m;
lld f[109];
int a[109];
int b[109];
char xx[109];
void moving(int dep, int z){
    if(m==0)return;
    if(dep<0)return;
    if(f[dep]<=m){
        m-=f[dep];
        for(int i=0;i<=dep;i++){
            a[i]=z;
        }
    }
    else{
        int zi = 3 - z - a[dep];
        moving(dep-1, zi);
        if(m==0)return;
        m--;
        a[dep]=z;
        moving(dep-1,z);
    }
}
void process(int dep){
    if(dep<0)return;
    if(a[dep]==b[dep]){
        process(dep-1);
        return;
    }
    if(m==0)return;
    int si = a[dep];
    int ti = b[dep];
    int zi = 3-si-ti;
    moving(dep-1, zi);
    if(m==0)return;
    a[dep]=ti;
    m--;
    process(dep-1);
}
int main(){
    int i,j,k;
    scanf("%d %lld",&n,&m);
    scanf("%s",xx);
    for(i=0;i<n;i++){
        b[i]=xx[i]-'A';
        a[i]=0;
        f[i]=(2LL<<i)-1;
    }
    process(n-1);
    for(i=0;i<n;i++){
        printf("%c",a[i]+'A');
    }
}
#endif
}
