using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 11
이름 : 배성훈
내용 : 합
    문제번호 : 1081번

    수학 문제다
    아이디어는 다음과 같다

    해당 수가 주어지면 0 ~ 해당 수 범위안의 각자리수들의 합을 f(해당 수)라 하면
    그러면 처음 ~ 끝의 각 자리수들의 합은 f(끝) - f(처음 - 1)으로 표현 가능하다
    그리고 그리디하게 앞 자리부터 구했다

    이렇게 제출하니 68ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0503
    {

        static void Main503(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            long[] digit = new long[11];
            long[] pow = new long[11];
            pow[0] = 1;
            pow[1] = 10;
            digit[1] = 45;
            for (int i = 2; i < 11; i++)
            {

                digit[i] = 10 * digit[i - 1] + pow[i - 1] * digit[1];
                pow[i] = pow[i - 1] * 10;
            }

            int[] last = new int[10];
            for (int i = 1; i < 10; i++)
            {

                last[i] = last[i - 1] + i;
            }
            long calc1 = GetVal(input[1]);
            long calc2 = GetVal(input[0] - 1);

            Console.WriteLine(calc1 - calc2);
            long GetVal(int _n)
            {

                // 0, -1인 경우는 0 반환
                if (_n <= 0) return 0L;

                // 어차피 2번만 확인하기에 문자열로 변환해서 각자리수 조사한다
                string str = _n.ToString();
                int len = str.Length;
                long ret = 0;

                for (int i = 0; i < str.Length; i++)
                {

                    int cur = str[i] - '0';
                    len--;

                    if (cur > 0)
                    {

                        ret += last[cur - 1] * pow[len];
                        ret += cur * digit[len];
                        ret += cur * ((_n % pow[len]) + 1);
                    }

                }

                return ret;
            }
        }
    }

#if other
using System;

class Program
{
    static void Main()
    {
        string[] inp = Console.ReadLine().Split(' ');
        long L = long.Parse(inp[0]), U = long.Parse(inp[1]);

        long result = 0;
        long cur = L;
        while(cur <= U)
        {
            long next = cur;
            long m = 1;
            while(true)
            {
                next += (9 - (cur / m) % 10) * m;
                if ((cur / m) % 10 > 0 || next > U) break;
                m *= 10;
            }
            while (next > U) next -= m;

            long a = cur;
            long b = next;
            long accAdd = 0;

            while(b > 0)
            {
                accAdd += a % 10 + b % 10;
                a /= 10;
                b /= 10;
            }

            result += accAdd * (next - cur + 1) / 2;
            cur = next + 1;
        }

        Console.WriteLine(result);
    }

}
#elif other2
using System;
					
public class Program
{

	public static void Main()
	{
		//개어렵네
		//참고 https://www.slideshare.net/Baekjoon/baekjoon-online-judge-1019
		var str = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		
		long sum = 0;
		int place = 1;
		bool flag = false;
		
		while(true){		
			while(!flag){
				if(str[0]%10 != 0){
					int t = str[0];
					
					while(t>0){
						sum += t%10*place;
						t /= 10;
					}								
				}
				else{
					break;
				}
				
				if(str[0] == str[1]){
					flag = true; break;
				}
				
				str[0]++;
			}
			while(!flag){		
				if(str[1]%10 != 9){
					int t = str[1];
				
					while(t>0){
						sum += t%10*place;
						t /= 10;
					}			
				}
				else{
					break;
				}
				
				if(str[0] == str[1]){
					flag = true; break;
				}
				
				str[1]--;
			}
			
			if(flag)
				break;
			
			for(int i=1; i<=9; i++){
				sum += (str[1]/10 - str[0]/10 +1)*place*i;
			}
				
			str[0] /= 10;
			str[1] /= 10;
			place *= 10;
			
		}
		
		Console.WriteLine(sum);
			
	}
}
#elif other3
var reader = new Reader();
var (a, b) = ((reader.NextInt() - 1).ToString(), (reader.NextInt()).ToString());
var mem = new Dictionary<string, long> {
    {"-1", 0},
    {"0", 0L},
    {"1", 1L},
    {"2", 3L},
    {"3", 6L},
    {"4", 10L},
    {"5", 15L},
    {"6", 21L},
    {"7", 28L},
    {"8", 36L},
    {"9", 45L},
};

Console.WriteLine(DigitSums(b) - DigitSums(a));

long DigitSums(string n)
{
    if (mem.ContainsKey(n))
        return mem[n];

    int leftMost = n[0] - '0';
    long sum = 0;
    int pow10 = (int)Math.Pow(10, n.Length - 1);
    int border = leftMost * pow10;

    sum += (leftMost * (int.Parse(n) - border + 1)) + DigitSums(n[1..]);
    for (int i = 0; i < leftMost; i++)
        sum += (i * pow10) + DigitSums(new string('9', n.Length - 1));

    mem.Add(n, sum);

    return sum;
}

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
}
#endif
}
