using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 18
이름 : 배성훈
내용 : 테트리스
    문제번호 : 3019번

    구현, 브루트포스 알고리즘 문제다
    블록의 길이가 1 ~ 4 사이라 한칸씩 이동하며 확인했다
    만약 블록의 길이가 1000 이상이면 kmp 알고리즘의 패턴을 찾아 풀 것이다
*/

namespace BaekJoon.etc
{
    internal class etc_0827
    {

        static void Main827(string[] args)
        {


            StreamReader sr;
            int[] board;
            int n;
            int type;

            int[][] block;

            Solve();
            void Solve()
            {

                Input();

                SetBlock();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;
                
                
                for (int i = 0; i < block.Length; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        int chk = board[j];
                        bool flag = true;
                        for (int k = 1; k < block[i].Length; k++)
                        {

                            if (j + k < n && chk == board[j + k] + block[i][k]) continue;
                            flag = false;
                            break;
                        }

                        if (flag) ret++;
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                board = new int[n];

                type = ReadInt();
                for (int i = 0; i < n; i++)
                {

                    board[i] = ReadInt();
                }

                sr.Close();
            }

            void SetBlock()
            {

                block = null;
                if (type == 1)
                {

                    block = new int[2][];
                    block[0] = new int[1];
                    block[1] = new int[4];
                    return;
                }
                
                if (type == 2)
                {

                    block = new int[1][];
                    block[0] = new int[2];
                    return;
                }

                if (type == 3)
                {

                    block = new int[2][];
                    block[0] = new int[3] { 0, 0, -1 };
                    block[1] = new int[2] { 0, 1 };
                    return;
                }

                if (type == 4)
                {

                    block = new int[2][];
                    block[0] = new int[3] { 0, 1, 1 };
                    block[1] = new int[2] { 0, -1 };
                    return;
                }

                if (type == 5)
                {

                    block = new int[4][];
                    block[0] = new int[3];
                    block[1] = new int[2] { 0, -1 };
                    block[2] = new int[3] { 0, 1, 0 };
                    block[3] = new int[2] { 0, 1 };
                    return;
                }

                if (type == 6)
                {

                    block = new int[4][];
                    block[0] = new int[3];
                    block[1] = new int[2];
                    block[2] = new int[3] { 0, -1, -1 };
                    block[3] = new int[2] { 0, 2 };
                    return;
                }

                if (type == 7)
                {

                    block = new int[4][];
                    block[0] = new int[3];
                    block[1] = new int[2] { 0, -2 };
                    block[2] = new int[3] { 0, 0, 1 };
                    block[3] = new int[2];
                    return;
                }
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
// #include<stdio.h>
// #include<stdarg.h>
int a[200];
int n,c,r;
int d(int m, ...)
{
	va_list v;
	int d[5];
	int i, j, k=0;
	va_start(v,m);
	for(i=0;i<m;i++)
		d[i]=va_arg(v,int);
	va_end(v);

	for(i=0; i<=n-m;i++)
	{
		for(j=1;j<m;j++)
			if(a[i]-d[0]!=a[i+j]-d[j])
				break;
		if(j==m)
			k++;
	}
	return k;
}
int main()
{
	scanf("%d %d",&n,&c);
	for(int i=0; i<n; i++)
		scanf("%d",&a[i]);
	r=(c==1)*(d(1,0)+d(4,0,0,0,0));
	r+=(c==2)*(d(2,0,0));
	r+=(c==3)*(d(3,0,0,1)+d(2,1,0));
	r+=(c==4)*(d(3,1,0,0)+d(2,0,1));
	r+=(c==5)*(d(3,0,0,0)+d(3,1,0,1)+d(2,1,0)+d(2,0,1));
	r+=(c==6)*(d(2,2,0)+d(3,0,0,0)+d(2,0,0)+d(3,0,1,1));
	r+=(c==7)*(d(2,0,2)+d(3,0,0,0)+d(3,1,1,0)+d(2,0,0));
	printf("%d",r);
}
#endif
}
