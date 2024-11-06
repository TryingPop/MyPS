using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 19
이름 : 배성훈
내용 : 재홍의 사다리
    문제번호 : 14842번

    수학, 기하학 문제다
    비례식을 세우면 된다
    부동소수점 오차를 줄이기 위해 decimal 변수를 이용했다
    그런데 다른 사람 풀이를 보고 double로 해보니 더블로도 통과된다
*/

namespace BaekJoon.etc
{
    internal class etc_0572
    {

        static void Main572(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            // decimal ret;
            double ret;

            if ((input[2] & 1) == 0)
            {

                ret = (input[2] - 1) / 2;
                ret *= input[1];
            }
            else
            {

                ret = input[2] / 2;
                ret *= ret / (input[2]);
                ret *= 2 * input[1];
            }
            Console.WriteLine($"{ret:0.000000}");
        }
    }
#if other
import java.util.*;

public class Main {
	static Scanner scan;
	
	public static void main (String[] args) {
		scan = new Scanner(System.in);
		
		int H = scan.nextInt();
		int W = scan.nextInt();
		int N = scan.nextInt();
		double NN = (double) N;
		double result = 0;
		
		if(N%2==0) {
			result = (NN-2)/2 * W;
		}
		else {
			result = (NN-1)/NN * (NN-1)/2.0 * W;
		}
		System.out.printf("%.6f", result);
	}
}

#endif
}
