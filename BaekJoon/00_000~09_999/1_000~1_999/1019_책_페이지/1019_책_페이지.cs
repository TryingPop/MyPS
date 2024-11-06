using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 14
이름 : 배성훈
내용 : 책 페이지
    문제번호 : 1019번

    코드 구간 구분한다고 만든 테스트 Console.WriteLine() 때문에
    출력 형식으로 여러 번 틀렸다...;

    주된 아이디어는 다음과 같다
    23_451_232을 입력 받으면,
    1 ~ 9_999_999 까지 먼저 센다
    
    다음으로 숫자의 맨 앞자리 2 > 1에 대해 
    10_000_000인 경우를 전부 센다
    그리고 2000만인 경우를 조사한다
    2가 3_451_232 + 1개 있다 (20_000_000 ~ 23_451_232)

    그러면, 23_451_232는 3_451_232로 변할 수 있다
    그리고 최고 단위는 10_000_000만 단위이므로 앞에 0이 올 수도 있다
    그래서 맨앞의 3 보다 작은 0, 1, 2에 경우의 수를 더한다

    이후 3이 451_232 + 1개 있으니 3에 해당 수를 더하고 맨 앞자리 숫자를 뺀다
    23_451_232 -> 3_451_232 -> 451_232 -> 51_232 -> ...-> 2 -> 0으로 만들면서 푼다!
*/

namespace BaekJoon.etc
{
    internal class etc_0032
    {

        static void Main32(string[] args)
        {

            string str = Console.ReadLine();

            // 처음 자리수 채우기!
            int[] ret = new int[10];
            // 더하는 값
            int[] add = new int[10];
            // 10의 제곱 값
            int[] pow = new int[10];

            // 먼저 10의 제곱값 연산
            pow[0] = 1; 
            for (int i = 1; i < 10; i++)
            {

                pow[i] = pow[i - 1] * 10;
            }

            // 이후에 더하는 값 계산
            // 인덱스 2에는
            // 00 ~ 99 까지 들어가는 0 ~ 9의 갯수가 있다 -> 20개
            // 인덱스 3에는
            // 000 ~ 999 까지 들어가는 0 ~ 9의 개수가 있다 -> 300개
            // ...
            for (int i = 1; i < 10; i++)
            {

                add[i] = pow[i - 1];
                add[i] *= i;
            }

            // 만약 1333정도면?
            // 1 ~ 999까지 카운트! 한 것이다!
            for (int i = 0; i < str.Length - 1; i++)
            {

                ret[0] += 9 * add[i];
                for (int j = 1; j < 10; j++)
                {

                    ret[j] += pow[i] + 9 * add[i];
                }
            }

            // 이제 자릿수만큼 카운트 시작!
            int num = int.Parse(str);
            int strLen = str.Length - 1;
            {

                // 먼저 맨 앞자리 제거
                // 맨 앞자리는 0이 올 수 없고, 미리 세었다!
                int front = str[0] - '0';

                for (int i = 1; i < front; i++)
                {

                    ret[i] += pow[strLen];
                }

                for (int i = 0; i < 10; i++)
                {

                    ret[i] += add[strLen] * (front - 1);
                }

                num -= front * pow[strLen];
                // +1은 ?0_000_000도 세어준다는 의미다!
                ret[front] += num + 1;
                strLen--;
            }

            // 2번째 부터 끝자리까지이다
            // 이젠 맨 앞에 0이 올 수 있어 0도 같이 세줘야한다
            // 하나씩 제거하며 세어준다!
            for (int i = strLen; i >= 0; i--)
            {

                int front = str[strLen - i + 1] - '0';

                for (int j = 0; j < front; j++)
                {

                    ret[j] += pow[i];
                }

                for (int j = 0; j < 10; j++)
                {

                    ret[j] += add[i] * (front);
                }

                num -= front * pow[i];
                ret[front] += num + 1;
            }

            // 결과 출력!
            for (int i = 0; i < 10; i++)
            {

                Console.Write(ret[i]);
                Console.Write(' ');
            }
        }
    }

#if other
int n = int.Parse(Console.ReadLine());
int add=0;

long[] counting = new long[10];

void c(int t, int d)
{
    for (;t > 0;t/=10) counting[t % 10] += d;
}
void s(int a,int b,int d)
{
    while (a % 10 != 0 && a <= b) c(a++,d);
    if (a > b) return;
    while (b % 10 != 9 && a <= b) c(b--,d);
    for (int i = 0, tmp = b / 10 - a / 10 + 1; i < 10; i++) counting[i] += tmp * d;
    s(a/10,b/10,d*10);
}
s(1,n,1);

Console.Write(string.Join(' ',counting));
#endif
}
