using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 29
이름 : 배성훈
내용 : Milking Time
    문제번호 : 6136번

    dp문제다.
    M의 크기가 1000으로 작다.
    M^2의 방법이 유효하다.
    이전 최대 시간을 모두 저장한다.

    아이디어는 다음과 같다.
    먼저 시작 시간을 기준으로 정렬한다.

    순차적으로 일을 시도하는데 가능한 것 중 가장 효율이 좋은 경우를 찾는다.
    첫 일의 경우 0초에 0효율이다.
    그러면 해당 일을 하는 경우 arr[0].e + r에 일이 가능하고, 효율을 arr[i].v이다.
    이를 chk 배열에 시간과 효율을 넣는다.
    
    두 번째 일의 경우 chk[0]의 시간에 현재 일을 가능한 경우
    해당 시간으로 일을한 효율을 택한다.
    반면 불가능하다면 0, 0초에서 한다고 본다.
    그리고 arr[1].e + r시간과 찾은 효율 + arr[i].v를 해서 chk 배열에 넣는다.
    이렇게 계속해서 일을해간다.

    그리고 마지막에 chk배열 중 가장 높은 효율을 찾으면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1589
    {

        static void Main1589(string[] args)
        {

            int n, m, r;
            (int s, int e, int v)[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                // 시작 시간으로 정렬한다.
                Array.Sort(arr, (x, y) => x.s.CompareTo(y.s));

                // time : 이전에 일을 마치고 쉬었던 시간
                // val : 해당 일을 할 때 최고 효율
                (int time, int val)[] chk = new (int time, int val)[m];
                int len = 0;

                for (int i = 0; i < m; i++)
                {

                    int curStart = arr[i].s;

                    // 초기 0효율
                    int v = 0;
                    for (int j = 0; j < len; j++)
                    {

                        // 이전 마치고 휴식한 시간이 현재일을 못하는 경우 패스
                        if (curStart < chk[j].time) continue;

                        // 일을 할 수 있는 경우 최대 효율을 찾는다.
                        if (v < chk[j].val)
                            v = chk[j].val;
                    }

                    // 찾은 최대 효율에서 현재 일을 한 효율을 chk에 넣는다.
                    chk[len++] = (arr[i].e + r, v + arr[i].v);
                }

                int ret = 0;
                for (int i = 0; i < len; i++)
                {

                    // 최고 효율 찾기
                    ret = Math.Max(chk[i].val, ret);
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();
                r = ReadInt();

                arr = new (int s, int e, int v)[m];
                for (int i = 0; i < m; i++)
                {

                    arr[i] = (ReadInt(), ReadInt(), ReadInt());
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;

                        ret = c - '0';

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }
#if other
// #include<stdio.h>
int time[1001][3], Tcnt[1001];
int push(int n, int end);
int main(){
	int n, m, r, i, j, k, a, b, c, cnt=0, max=-999, allMax=-999, lastTime;
	scanf("%d %d %d", &n, &m, &r);
	for(i=1; i<=m; i++){
		scanf("%d %d %d", &a, &b, &c);
		j = i;
		do{
			j--;
			if(j == 0)
				break;
		}while(time[j][0] > a);
		push(j+1, i);
		time[j+1][0] = a;
		time[j+1][1] = b;
		time[j+1][2] = c;
	}
	for(i=m; i>=1; i--){
		cnt=0;
		max = 0;
		for(j=i+1; j<=m; j++){
			if(time[j][0] < time[i][1] + r)
				continue;
			if(Tcnt[j] > max)
				max = Tcnt[j];
			if(Tcnt[j] == allMax)
				break;
		}
		Tcnt[i] = max+time[i][2];
		if(allMax < Tcnt[i])
			allMax = Tcnt[i];
	}
	printf("%d", allMax);
	return 0;
}
int push(int n, int end){
	if(n < end)
		push(n+1, end);
	time[n+1][0] = time[n][0];
	time[n+1][1] = time[n][1];
	time[n+1][2] = time[n][2];
	return 0;
}
#endif
}
