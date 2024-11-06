using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 9
이름 : 배성훈
내용 : 원숭이 스포츠
    문제번호 : 16438번

    해 구성하기, 분할 정복 문제다
    원숭이들끼리 적어도 한 번 적으로 만나야한다
    이전 팀에서 절반씩 A, B로 나누면 모두가 적이 적어도 한 번은 될 수 있다
    7인 경우를 보자
    먼저 전체의 절반으로 나눈다
        AAAABBB

    그러면 1234 - A, 567 - B은 서로 한 번씩 적이되었다
    그래서 각 그룹 내에서 적으로 한 번씩 만나면 된다

    짝수인 경우 절반으로 나눠 떨어지므로 이상이 없다
        12 - A, 34 - B
        13 - A, 24 - B
    그래서 1, 2, 3, 4 모두 적으로 만난다

    이제 567을 보면 절반에 올림연산
        56 - A, 7 - B
    그러면 B는 5, 6과 적이되었고 빼도 된다

    3번의 팀나누기로 모두가 적이 적어도 한 번은 되었다
    이렇게 진행하면 log2(n) 번 시행을 해야 팀으로 나눌 수 있다
    6 < log2(99) < 7 이므로 7번이면 충분히 나눌 수 있다

    다른 사람을 보니 이를 비트마스킹으로 분할정복을 했다
*/

namespace BaekJoon.etc
{
    internal class etc_0872
    {

        static void Main872(string[] args)
        {

            StreamWriter sw;

            int n;
            char[][] arr;

            Solve();
            void Solve()
            {

                n = int.Parse(Console.ReadLine());
                arr = new char[7][];
                for (int i = 0; i < 7; i++)
                {

                    arr[i] = new char[n];
                    Array.Fill(arr[i], 'A');
                    arr[i][0] = 'B';
                }

                DNC(0, n - 1, 6);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);


                for (int i = 0; i < 7; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        sw.Write(arr[i][j]);
                    }

                    sw.Write('\n');
                }


                sw.Close();
            }

            void DNC(int _s, int _e, int _depth)
            {

                int mid = (_s + _e) >> 1;
                if (_e - _s > 1)
                {

                    DNC(_s, mid, _depth - 1);
                    DNC(mid + 1, _e, _depth - 1);
                }

                for (int i = _s; i <= mid; i++)
                {

                    arr[_depth][i] = 'A';
                }

                for (int i = mid + 1; i <= _e; i++)
                {

                    arr[_depth][i] = 'B';
                }
            }
        }
    }

#if other
// #include<stdio.h>
using namespace std;
int main(){
    int N;
    scanf("%i",&N);
    for(int x=0;x<7;x++){
        for(int y=0;y<N;y++){
            if((1<<x)&y || (N<=(1<<x) && y==0)) printf("B");
            else printf("A");
        }
        printf("\n");
    }
}
#elif other2
// #include <stdio.h>
// #include <algorithm>
using namespace std;

int main(){
	int n;

	scanf("%d", &n);

	for(int i = 0; i < 7; i++){
		if((1<<i) >= n){
			for(int j = 1; j < n; j++) printf("A");
			printf("B"); 
		}
		else{
			for(int j = 0; j < n; j++) printf("%c", (1<<i)&j ? 'B' : 'A');
		}
		printf("\n");
	}

	return 0;
}
#endif
}
