using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 10
이름 : 배성훈
내용 : 한 줄로 서기
    문제번호 : 1138번

    구현, 그리디 문제다.
    그리디로 큰 사람부터 자리를 배치할 시 앞에 배치하면 된다.
    그리고 이어주는건 연결리스트로 해서 N^2에 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1173
    {

        static void Main1173(string[] args)
        {

            int n;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                (int val, int next)[] node = new (int val, int next)[n + 2];
                int HEAD = 0;
                int TAIL = 1;
                node[0].next = TAIL;
                int cur = 2;
                for (int i = n - 1; i >= 0; i--)
                {

                    int idx = HEAD;
                    for (int j = 0; j < arr[i]; j++)
                    {

                        idx = node[idx].next;
                    }

                    node[cur].val = i + 1;
                    ConnNext(idx, cur++);
                }

                for (int i = node[HEAD].next; i != TAIL; i = node[i].next)
                {

                    sw.Write($"{node[i].val} ");
                }

                sw.Close();

                void ConnNext(int _cur, int _next)
                {

                    int nn = node[_cur].next;
                    node[_cur].next = _next;
                    node[_next].next = nn;
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();

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
    }

#if other
// #include <stdio.h>
int n,a[11],b[11];
int main()
{
    int i,j;
    scanf("%d",&n);
    for(i=1;i<=n;i++)
        scanf("%d",&a[i]);
    for(i=n;i>=1;i--)
    {
        for(j=n-1;j>=a[i]+1;j--)
            b[j+1]=b[j];
        b[a[i]+1]=i;
    }
    for(i=1;i<=n;i++)
        printf("%d ",b[i]);
    return 0;
}
#endif
}
