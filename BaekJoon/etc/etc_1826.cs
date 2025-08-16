using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 15
이름 : 배성훈
내용 : 동전 뒤집기
    문제번호 : 1285번

    그리디, 브루트포스 문제다.
    먼저 행간 뒤집기는 서로 독립적이다.
    
    그래서 열과 행을 나눠서 생각했다.
    동전의 개수를 size라 하면
    열의 뒤집기는 2^size개 존재 가능하다.

    그래서 모든 열 상태에 따라 행을 뒤집는지 안뒤집는지 하면서
    H가 최소가 되는 경우와 T가 최소가 되는 경우를 뒤집어가면서 찾아갔다.
    H, T 뿐이므로 H가 최소, 최대로 찾아가면 된다.
    
    그렇게 진행했을 때 H들의 최소 중의 최솟값이나
    H의 최댓값 중 최댓값을 찾은 뒤 T의 최소값을 찾아 비교해 정답을 구했다.

    연산 순서를 조절하면 더 적은 메모리를 사용해 풀 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1826
    {

        static void Main1826(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int size = int.Parse(sr.ReadLine());

            int REV = (1 << size) - 1;

            int[] min = new int[1 << size];
            int[] max = new int[1 << size];
            int[] cnt = new int[1 << size];
            for (int i = 0; i < cnt.Length; i++)
            {

                for (int j = 0; j < size; j++)
                {

                    int chk = 1 << j;
                    if ((i & chk) != 0) continue;
                    cnt[i | chk] = cnt[i] + 1;
                }
            }

            for (int i = 0; i < size; i++)
            {

                string str = sr.ReadLine();
                int curState = 0;
                for (int j = 0; j < size; j++)
                {

                    if (str[j] == 'T') continue;
                    curState |= 1 << j;
                }

                for (int j = 0; j < min.Length; j++)
                {

                    int chk1 = j ^ curState;
                    int chk2 = j ^ REV ^ curState;
                    min[j] += Math.Min(cnt[chk1], cnt[chk2]);
                    max[j] += Math.Max(cnt[chk1], cnt[chk2]);
                }
            }

            int ret = size * size;
            int mul = size * size;
            for (int i = 0; i < min.Length; i++)
            {

                ret = Math.Min(ret, min[i]);
                ret = Math.Min(ret, mul - max[i]);
            }

            Console.Write(ret);
        }
    }

#if other
using System;

public class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        string[] board = new string[n];
        for (int i = 0; i < n; i++)
        {
            board[i] = Console.ReadLine();
        }
        int[] column = new int[n];
        int answer = int.MaxValue;
        void Recursion(int depth)
        {
            if (depth == n)
            {
                int t = 0;
                for (int i = 0; i < n; i++)
                {
                    t += Math.Min(column[i], n - column[i]);
                }
                answer = Math.Min(answer, t);
                return;
            }
            for (int i = 0; i < n; i++)
            {
                if (board[depth][i] == 'T')
                    column[i]++;
            }
            Recursion(depth + 1);
            for (int i = 0; i < n; i++)
            {
                if (board[depth][i] == 'T')
                    column[i]--;
                else
                    column[i]++;
            }
            Recursion(depth + 1);
            for (int i = 0; i < n; i++)
            {
                if (board[depth][i] == 'H')
                    column[i]--;
            }
        }
        Recursion(0);
        Console.Write(answer);
    }
}
#elif other2
// #include<iostream>
// #include<cstdlib>
using namespace std;
int N, sum=0, mn, Q=15;
bool arr[20][20];
void g(int r){
	for(int i=0;i<N;i++){
		arr[r][i]=!arr[r][i];
		if(arr[r][i])	sum++;
		else sum--;
	}
}
void f(int c){
	for(int i=0;i<N;i++){
		arr[i][c]=!arr[i][c];
		if(arr[i][c])	sum++;
		else	sum--;
	}
}
int main(){
	ios::sync_with_stdio(false);
	cin.tie(0);
	cout.tie(0);
	cin>>N;
	char t;
	for(int i=0;i<N;i++){
		for(int j=0;j<N;j++){
			cin>>t;
			if(t=='T'){
				arr[i][j]=true;
				sum++;
			}
		}
	}
	mn=sum;
	while(Q--){
		for(int i=0;i<70;i++){
			int x=rand()%(N*2);
			int y=sum;
			if(x<N){
				g(x);
				if(sum<=y)	i=-1;
				else	g(x);
			}
			else{
				f(x-N);
				if(sum<=y)	i=-1;
				else	f(x-N);
			}
		}
		mn=min(mn, sum);
		for(int i=0;i<N;i++)	if(rand()%2)	g(i);
		for(int i=0;i<N;i++)	if(rand()%2)	f(i);
	}
	cout<<mn<<"\n";
	return 0;
}
#endif
}
