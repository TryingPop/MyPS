using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 1
이름 : 배성훈
내용 : 삼각형 게임
    문제번호 : 4658번

    브루트포스, 백트래킹 문제다
    이전 삼각형 번호, 사용한 빗변 번호를 기억하고 DFS 돌렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0784
    {

        static void Main784(string[] args)
        {

            string NO = "none\n";
            StreamReader sr;
            StreamWriter sw;

            int[][] arr;
            int[][] before;
            bool[] use;

            Solve();

            void Solve()
            {

                Init();

                do
                {

                    Input();

                    int ret = DFS(0, 0);

                    if (ret == -1) sw.Write(NO);
                    else sw.Write($"{ret}\n");
                }
                while (ChkConti());

                sr.Close();
                sw.Close();
            }

            int DFS(int _depth = 0, int _score = 0)
            {

                if (_depth == 6)
                {

                    int chk1 = arr[before[0][0]][(before[0][1] + 2) % 3];
                    int chk2 = arr[before[5][0]][(before[5][1])];
                    if (chk1 == chk2) return _score;
                    else return -1;
                }

                int ret = -1;
                if (_depth == 0)
                {

                    before[0][0] = 0;
                    for (int i = 0; i < 3; i++)
                    {

                        before[0][1] = i;

                        int chk = DFS(1, _score + arr[0][(i + 1) % 3]);
                        ret = Math.Max(ret, chk);
                    }

                    return ret;
                }

                int beforeVal = arr[before[_depth - 1][0]][before[_depth - 1][1]];
                for (int i = 1; i < 6; i++)
                {

                    if (use[i]) continue;

                    for (int j = 0; j < 3; j++)
                    {

                        if (arr[i][j] != beforeVal) continue;
                        use[i] = true;
                        before[_depth][0] = i;
                        before[_depth][1] = (j + 1) % 3;

                        int chk = DFS(_depth + 1, _score + arr[i][(j + 2) % 3]);
                        ret = Math.Max(ret, chk);
                        use[i] = false;
                    }
                }

                return ret;
            }

            void Input()
            {

                for (int i = 0; i < 6; i++)
                {

                    for (int j = 0; j < 3; j++)
                    {

                        arr[i][j] = ReadInt();
                    }
                }
            }

            bool ChkConti()
            {

                if (sr.Read() == '$') return false;
                if (sr.Read() == '\r') sr.Read();

                return true;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                arr = new int[6][];
                before = new int[6][];
                for (int i = 0; i < 6; i++)
                {

                    arr[i] = new int[3];
                    before[i] = new int[2];
                }

                use = new bool[6];
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
// #include <stdio.h>
int max=0;
void dfs(int a[],int n,int b[][3]);
void dice(int a[],int b[][3]);
int main(void)
{
    char a='*';
    while(a!='$')
    {
        int num[6][3];
        for(int i=0;i<6;i++)
        {
            scanf("%d %d %d",&num[i][0],&num[i][1],&num[i][2]);
        }
        int arr[6];
        arr[0]=0;
        dfs(arr,1,num);
        scanf("%s",&a);
        if(max==0)
        {
            printf("none\n");
        }
        else
        {
            printf("%d\n",max);
        }
        max=0;

    }
}
void dfs(int a[],int n,int b[][3])
{
    if(n==6)
    {
        dice(a,b);
        return;
    }
    for(int i=0;i<6;i++)
    {
        int q=0;
        for(int j=0;j<n;j++)
        {
            if(i==a[j])
            {
                q=1;
                break;
            }
        }
        if(q==0)
        {
            a[n]=i;
            dfs(a,n+1,b);
        }
    }
}
void dice(int a[],int b[][3])
{
    for(int i=0;i<3;i++)
    {
        int sum=b[0][i];
        int before=b[0][(i+2)%3];
        for(int j=1;j<6;j++)
        {
            int q=0;
            for(int k=0;k<3;k++)
            {
                if(b[a[j]][k]==before)
                {
                    q=1;
                    before=b[a[j]][(k+1)%3];
                    sum+=b[a[j]][(k+2)%3];
                    break;
                }
            }
            if(q==0)
            {
                sum=-1;
                break;
            }
        }
        if(before!=b[0][(i+1)%3])
        {
            sum=-1;
        }
        if(max<sum)
        {
            max=sum;
        }
    }
}
#elif other2
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.StringTokenizer;

public class Main {
	static int max = 0;
	public static void main(String[] args) throws NumberFormatException, IOException {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		StringBuilder sb = new StringBuilder();
		do {
			int[][] triangles = new int[6][3];
			for (int i = 0; i < 6; i++) {
				StringTokenizer st = new StringTokenizer(br.readLine());
				triangles[i][0] = Integer.parseInt(st.nextToken());
				triangles[i][1] = Integer.parseInt(st.nextToken());
				triangles[i][2] = Integer.parseInt(st.nextToken());
			}
			for (int i = 0; i < 3; i++) {
				find(triangles, triangles[0][i], triangles[0][(i+2)%3], 1, triangles[0][(i+1)%3]);
			}
			if(max != 0) {
				sb.append(max);
				max = 0;
			}
			else {
				sb.append("none");
			}
			sb.append("\n");
		}while(br.readLine().equals("*"));
		System.out.println(sb.toString());
	}
	private static void find(int[][] triangles, int i, int j, int k, int triangles2) {
		if(k == (1 << 6) -1 && i == triangles2) {
			if(max < j) max = j;
			return;
		}
		for (int a = 1; a < triangles.length; a++) {
			if((k & 1 << a) != 0) continue;
			for (int b = 0; b < 3; b++) {
				if(triangles[a][b] == i) {
					find(triangles, triangles[a][(b+2) % 3], j+triangles[a][(b+1)%3],k|1<<a, triangles2);
				}
			}
		}
	}
}

#elif other3
import sys

input = sys.stdin.readline

def dfs(n,now,sum_v):
    global total
    
    if n == 6:
        if tris[0][cnt] == now:
            total = sum_v
        else:
            total = 0
        return
    
    for i in range(6):
        if visit[i] == 1:
            continue
        for j in range(3):
            if now == tris[i][j]:
                visit[i] = 1
                dfs(n+1,tris[i][(j+2)%3],sum_v+tris[i][(j+1)%3])
                visit[i] = 0

while 1:
    tris = []
    for _ in range(6):
        tris.append(list(map(int,input().split())))
    max_v = 0
    
    visit = [0] * 6
    
    for cnt in range(3):
        total = 0
        dfs(0,tris[0][cnt],0)
        if max_v < total:
            max_v = total
    if max_v:
        print(max_v)
    else:
        print('none')
    
    next = input().strip()
    if next == "*":
        pass
    elif next == "$":
        break
#endif
}
