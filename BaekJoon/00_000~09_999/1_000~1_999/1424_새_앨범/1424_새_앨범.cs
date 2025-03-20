using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 20
이름 : 배성훈
내용 : 새 앨범
    문제번호 : 1424번

    수학 문제다.
    13으로 나눠떨어지지 않게 한다가 엄청난 영향을 끼치는 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1428
    {

        static void Main1428(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            int l = int.Parse(Console.ReadLine());
            int c = int.Parse(Console.ReadLine());

            // 1개 시디에 들어갈 수 있는 최댓값
            int cnt = 1;
            c -= l;
            l++;
            cnt += c / l;
            cnt = Math.Min(cnt, n);
            // 13으로 나눠떨어지면 안된다.
            if (cnt % 13 == 0) cnt--;

            // 시디 갯수
            int ret = n / cnt;
            n %= cnt;
            if (n > 0) 
            { 
                
                // 전체 못채우는 부분 새로운 시디에 채우기
                ret++; 
                if (n % 13 == 0)
                {

                    // 남은게 13으로 나눠떨어지면 다른쪽에서 가져온다.
                    int r = cnt - n;
                    // (n % cnt) % 13 == 0이므로,
                    // cnt > 13이 보장된다.
                    // cnt = Math.Min(cnt, n)이므로 꽉찬 곳이 1개 이상이 보장된다!
                    // 1개 빼올 수 있는지 확인
                    // 꽉찬 곳에서 1개 뺐을 때 13으로 나눠떨어지면
                    // 2개를 빼온다. 아니면 1개 빼온다.
                    int a = cnt % 13 == 1 ? 2 : 1;

                    // 남은 공간이 뺴온 것을 넣을 수 있으면 넣어 13으로 나눠떨어지지 않게한다.
                    if (r < a) ret++;
                }
            }

            Console.Write(ret);
        }
    }

#if other
// #include <cstdio>
int main(){
	int N, Length, CD;
	scanf("%d\n%d\n%d\n",&N,&Length,&CD);
	int num =(CD+1)/(Length+1);
	if(num%13==0)
		num--;
	int cnt = (N + num - 1) / num;
	int remain = N - num * (cnt - 1);
	if(remain % 13 == 0)
		if(cnt == 1 || remain == num - 1)
			cnt++;
	printf("%d\n",cnt);
	return 0;
}
#endif
}
