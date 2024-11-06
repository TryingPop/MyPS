using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 20
이름 : 배성훈
내용 : 수열의 합
    문제번호 : 1024번

    수학 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_1048
    {

        static void Main1048(string[] args)
        {

            int n, l;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                l = int.Parse(temp[1]);
            }

            void Output(int _s = -1, int _e = -1)
            {

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    for (int i = _s; i <= _e; i++)
                    {

                        sw.Write($"{i} ");
                    }
                }
            }

            void GetRet()
            {

                for (int len = l; len <= 100; len++)
                {

                    // 짝수인 경우
                    if ((len & 1) == 0)
                    {

                        int l = len >> 1;
                        // 중앙지점 찾기
                        if (n % l != 0) continue;
                        int chk = n / l;
                        // 연속한 자연수 짝수개의 합은 무조건 홀수이다!
                        if ((chk & 1) == 0) continue;
                        chk >>= 1;

                        if (chk + 1 < l)
                        {

                            Output();
                            return;
                        }

                        Output(chk - l + 1, chk + l);
                        return;
                    }
                    // 홀수인 경우
                    else
                    {

                        // 중앙의 왼쪽지점 찾기
                        if (n % len != 0) continue;
                        int chk = n / len;
                        int l = len >> 1;
                        if (chk < l)
                        {

                            Output();
                            return;
                        }

                        Output(chk - l, chk + l);
                        return;
                    }
                }

                Output();


            }
        }
    }

#if other
using System;

namespace Boj
{
    class _1024
    {
        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split();
            int N = int.Parse(s[0]);
            int L = int.Parse(s[1]); 
            bool exist = false;
            for (int i = L; i < 101; i++)
            {   
                double initial = ((double)(2*N/i) - (i -1))/2;
                double sum = 0;
                for (int k = 0; k < i; k++)
                {
                    sum += initial + k;
                }
                if ((initial >= 0 && initial%1 == 0 && sum == N) || (initial == 0 && sum == N) ) {
                    exist = true;
                    for (int j = 0; j < i ; j++)
                    {   
                        Console.Write("{0} ", initial + j);
                    }
                    break;
                }
            }
            if(exist != true) {
                Console.Write(-1);
            }
        }
    }
}
#elif other2
using System;

namespace InputTest
{
	class Program
	{
			static void Main(string[] args)
			{
		    	string[] firstLine = Console.ReadLine().Split(' ');
                int N = int.Parse(firstLine[0]);
                int L = int.Parse(firstLine[1]);
				
				var (r, l) = calc(N, L);
				
				if (r == -1){
				    Console.WriteLine(r);
				}else{
				    for(int i = 0; i < l; i++){
                       Console.Write((r+i)+" ");
				    }
				}
			}
			
			static (int, int) calc(int n, int l){
				if(l > 100){
				    return (-1, 0);
				}else if(l%2 != 0 && n%l == 0 && (n/l)>=(l/2)){
				    return ((n/l)-(l/2), l);
				}else if(l%2 == 0 && (n%l) == (l/2) && (n/l)>=(l/2 - 1)){
				    return ((n/l)-(l/2)+1, l);
				}else{
				    return calc(n, l+1);
				}
			}
	}
}
#endif
}
