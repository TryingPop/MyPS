using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 9
이름 : 배성훈
내용 : 수학적인 최소 공통 조상
    문제번호 : 26092번

    수학? 문제다
    최소 공통 조상이 어떻게 이루어지는가만 알면 쉽게 풀린다

    N의 부모 조건이 p : N의 가장 작은 소인수라할 때,
    N의 부모 노드의 번호는 N/p

    그러면 최소 공통 조상은 큰 수 공약수부터 같은지 확인해서 
    같으면 해당 값은 최소공토조상의 소인수이고
    다르면 탐색을 종료하고 여태까지 찾은 소인수들의 곱이 정답이 된다

    처음부터 다르면 1이다!
*/

namespace BaekJoon.etc
{
    internal class etc_0166
    {

        static void Main166(string[] args)
        {

            // 2개 데이터만 입력받기에 StreamReader 사용 X
            long[] input = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            List<long> left = new List<long>(100);
            List<long> right = new List<long>(100);

            long calc1 = input[0];
            long calc2 = input[1];

            // input[0]의 소인수 찾기
            for (long i = 2; i < calc1; i++)
            {

                if (i * i > calc1) break;

                if (calc1 % i != 0) continue;

                while (true)
                {

                    calc1 /= i;
                    left.Add(i);
                    if (calc1 % i != 0) break;
                }
            }

            if (calc1 != 1) left.Add(calc1);

            // input[1]의 소인수 찾기
            for (long i = 2; i < calc2; i++)
            {

                if (i * i > calc2) break;

                if (calc2 % i != 0) continue;

                while (true)
                {

                    calc2 /= i;
                    right.Add(i);
                    if (calc2 % i != 0) break;
                }
            }

            if (calc2 != 1) right.Add(calc2);

            // 내림차순 정렬
            left.Sort((x, y) => y.CompareTo(x));
            right.Sort((x, y) => y.CompareTo(x));

            int minLen = left.Count < right.Count ? left.Count : right.Count;

            // 정답 도출
            long ret = 1;
            for (int i = 0; i < minLen; i++)
            {

                if (left[i] != right[i]) break;

                ret *= left[i];
            }

            Console.WriteLine(ret);
        }

    }
}
