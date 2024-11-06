using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 30
이름 : 배성훈
내용 : 점프왕 최준민
    문제번호 : 11564번

    수학, 정수론, 많은 조건 분기 문제다
    음양 이 겹치는 구간이 있는 경우와 그렇지 않은 경우로 구분해서 풀었다
    음음인 경우 양양으로 바꿔도 이상 없기에 양양으로 바꿔 풀었다
    다만, 처음에 0이 아닌 점에서 시작할 수 있는 줄 알고 틀렸고
    이후에는 조건 빠뜨려 1번 더 틀렸다 (총 2번;)
*/

namespace BaekJoon.etc
{
    internal class etc_0661
    {

        static void Main661(string[] args)
        {

            long[] info = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);

            long ret = 0;

            if (info[1] <= 0 && info[2] >= 0)
            {

                info[1] = -info[1];
                ret += info[1] / info[0];
                ret += info[2] / info[0];
                ret++;
                Console.WriteLine(ret);
                return;
            }

            if (info[1] <= 0 && info[2] <= 0)
            {

                long temp = -info[1];
                info[1] = -info[2];
                info[2] = temp;
            }

            ret += info[2] / info[0];
            ret -= info[1] / info[0];
            if (info[1] % info[0] == 0) ret++;

            Console.WriteLine(ret);
        }
    }

#if other
import java.io.BufferedReader;
import java.io.InputStreamReader;

public class Main {

	public static void main(String[] args) throws Exception {

		BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
		String[] line = reader.readLine().split(" ");
		long k = Long.parseLong(line[0]);
		long a = Long.parseLong(line[1]);
		long b = Long.parseLong(line[2]);

		long start = 0;
		//System.out.println("a/k=" + a / k);
		long ak = a / k;
		if (a % k != 0 && a > 0) {
			ak++;
		}
		start = ak * k;

		long end = 0;
		//System.out.println("b/k=" + b / k);
		long bk = b / k;
		if (b % k != 0 && b < 0) {
			bk--;
		}
		end = bk * k;

	//	System.out.println(start);
	//	System.out.println(end);
		long ret = 0;
		if (end >= start) {
			ret = (end - start) / k + 1;
		}
		System.out.println(ret);

	}
}

#endif
}
