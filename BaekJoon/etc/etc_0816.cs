using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 16
이름 : 배성훈
내용 : 마지막 팩토리얼 수 2
    문제번호 : 2554번

    수학, 정수론, 임의 정밀도 / 큰 수 연산 문제다
    100자리까지 오기에 Numerics의 BigInteger를 이용했다
    
    풀이는 해당 영상을 참고했다
    https://www.youtube.com/watch?v=RmENut3ZmnM

    아이디어는 다음과 같다
    (n * 5)! 은 (2^n) * (n!) 과 값이 같다
    우선 5씩 끊으면

    1, 2, 3, 4, 5 / 6, 7, 8, 9, 10 / 11, 12, 13, 14, 15 / .... / 5n - 4, 5n - 3, 5n - 2, 5n - 1, 5n

    이고 5부분만 앞으로 빼고 곱해가면
    (5^n) * (n!)이된다

    그리고 나머지는 수는 다음과 같이 있다
    1, 2, 3, 4 / 6, 7, 8, 9 / 11, 12, 13, 14 / .... / 5n - 4, 5n - 3, 5n - 2, 5n - 1
    해당 4개의 수를 곱하고 끝자리만 보면
    10으로 나눠떨어질 수 없기에 일의 자리만 비교하면 된다
    그러면 
    4 / 4/ 4 / ... / 4
    모두 곱하면 끝자리는 4로 나온다
    4는 총 n개 있어 곱하면
    4^n = 2^(2n)
    이되고 앞과 곱해주면
    (5^n) * (n!) * 4^n = (5^n) * (2^n) * n! * (2^n) = (10)^n * n! * (2^n)
    여기서 끝자리만 찾으므로 10^n은 필요 없다
    n! * 2^n 만 남음을 알 수 있다
    즉, (5 * n)! = (2^n) * (n!)

    이렇게 n의 크기를 줄여가면서 곱해가면 결과를 찾을 수 있다
    아래는 이를 코드로 구현한 것 뿐이다
*/

namespace BaekJoon.etc
{
    internal class etc_0816
    {

        static void Main816(string[] args)
        {

            BigInteger ZERO = 0;
            BigInteger n;

            int ret;
            int[] add;
            int[] two;
            Solve();
            void Solve()
            {

                Read();
                Init();

                ret = 1;

                while(n != ZERO)
                {

                    Div();
                }

                Console.Write(ret);
            }

            void Init()
            {

                add = new int[10] { 1, 1, 2, 6, 4, 1, 6, 2, 6, 4 };
                two = new int[5] { 1, 2, 4, 8, 6 };
            }

            void Div()
            {

                int a = (int)(n % 10);
                n /= 5;
                int t = (int)((n - 1) % 4) + 1;
                Mul(a, t);
            }

            void Mul(int _a = 1, int _t = 1)
            {

                ret = (ret * add[_a] * two[_t]) % 10;
            }

            void Read()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = 0;
                int c;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    n = n * 10 + c - '0';
                }

                return;
            }
        }
    }

#if other
// #include <stdio.h>
// #include <string.h>

char ans[128];

char *d(char *num)
{
    int ansidx=0;
 
    int idx=0;
    int tmp=num[idx]-'0';
    if(tmp<5)
        tmp=tmp*10+(num[++idx]-'0');
    while(strlen(num)>idx) {
        ans[ansidx]=(tmp/5)+'0';
        ansidx++;
        ans[ansidx]='\0';
        tmp=(tmp%5)*10+num[++idx]-'0';
    }
    if(ansidx==0)
        return "0";
 
    return ans;
}

int main()
{
    char num[128];
    scanf("%s", num);
    if(strlen(num)==1 && num[0]==49){
        printf("1");
        return 0;
    }
    int rest=1, chk=0, finish=1;
    
    while(strlen(num)>=2){
        while((num[strlen(num)-1]-48)%5!=0){
            rest=rest*(num[strlen(num)-1]-48);
            rest=rest%10;
            num[strlen(num)-1]--;
        }
        strcpy(num, d(num));
        if(strlen(num)>=2)
            chk+=((num[strlen(num)-2]-48)*10+num[strlen(num)-1]-48)%4;
        else
            chk+=(num[strlen(num)-1]-48)%4;
    }
    
    while(num[0]-48!=0){
        finish*=num[0]-48;
        num[0]--;
    }

    while(finish%10==0){
        finish/=10;
    }
    
    if(chk%4==0) printf("%d", (rest*6*finish)%10);
    if(chk%4==1) printf("%d", (rest*2*finish)%10);
    if(chk%4==2) printf("%d", (rest*4*finish)%10);
    if(chk%4==3) printf("%d", (rest*8*finish)%10);
}
#elif other2

import java.math.BigInteger;
import java.util.Scanner;

public class Main {

	public static void main(String[] args) {
		Scanner sc = new Scanner(System.in);
		String str = sc.next();
		BigInteger bigNumber = new BigInteger(str);
		BigInteger bigNumberFour = new BigInteger("4");
		BigInteger bigNumberFive = new BigInteger("5");
		BigInteger bigNumberTen = new BigInteger("10");
		
		// 5,10 배수 제외하고 다 곱했을때, 첫번째 자리
		int arr[] = {6,6,2,6,4,4,4,8,4,6};
		int result = 1 ;
		int fiveNum = 0;
		// 5 곱한 갯수만큼 뺄때 사용
		int brr[] = {2,4,8,6};
		
		// 얘는 while 문 빠져 나갈때 사용하려고 
		BigInteger bigNumberZero = new BigInteger("0");
		
		
		// 5가 몇개 있는지 .... (4로나눈 나머지..) 
		while(true) {
			
			if(bigNumber.equals(bigNumberZero)) {
				
				break;
			}
			
			int a = arr[bigNumber.remainder(bigNumberTen).intValue()];
			result = (result*a)%10;
			
			int b = bigNumber.divide(bigNumberFive).remainder(bigNumberFour).intValue();
			fiveNum = (fiveNum+b)%4;
			bigNumber = bigNumber.divide(bigNumberFive);
		}
		int idx = -1;
		if(result == 2) {
			idx = 0;
		}else if(result == 4) {
			idx = 1;
		}else if(result == 8) {
			idx = 2;
		}else if(result == 6) {
			idx = 3;
		}else {}
		
		result = brr[(idx+4-fiveNum)%4];
		if(str.equals("1") || str.equals("0")) System.out.println(1);
		else System.out.println(result);
		
		
	}

}

#endif
}
