using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 25
이름 : 배성훈
내용 : 장미
    문제번호 : 3343번

    수학, 브루트포스, 정수론 문제다
    처음에는 그리디로 접근했다가 틀렸다

    이후 계속 그리디로만 생각하다가 질문게시판을 보게 되었고,
        199 100 99  99  89  --> 188
    입력 예제를 보고 수정할 필요가 있음을 느꼈다
    그리고, 최소공배수까지는 적어도 효율 좋은걸 사는게 좋은게 아닌가
    생각을 했고, 여전히 그리디로 접근해서 최소공배수 나눌 수 있는 만큼 나누고
    나머지는 1개씩 사면 되는거 아닌가 하면서 계속해서 틀렸다

    그리고 답이 안나와서 다른사람 풀이를 검색했다
    그리디가 오답임을 알았다

    풀이를 보니, 효율 좋은 것을 b라 하면, 효율 안좋은 것은 a가 된다
    gcd를 a, b의 최대 공약수라하면, a를 b / gcd 만큼 사는건
    b를 a / gcd 사는것보다 효율이 안좋다
    그래서 a를 산다면 0개 ~ (b / gcd) - 1 개까지 산다

    그래서 a를 0 ~ (b / gcd) - 1개 사는 경우를 조사해 이 중 최소값을 찾아
    반환하게 하니 통과된다

    해당 코드를 보면 lcm씩 사면서 2 * lcm 미만에서는 lcm만큼 사는게 최적이 아닌거 같이 보인다
*/

namespace BaekJoon.etc
{
    internal class etc_0352
    {

        static void Main352(string[] args)
        {

            long[] info = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            long calc1 = info[1] * info[4];
            long calc2 = info[2] * info[3];

            if (calc2 < calc1)
            {

                // 앞이 효율 좋다
                // 효율 좋은게 뒤로가게 한다
                long temp = info[1];
                info[1] = info[3];
                info[3] = temp;
                temp = info[2];
                info[2] = info[4];
                info[4] = temp;
            }

            long gcd = GetGCD(info[1], info[3]);
            long len = info[3] / gcd;
            // 정답으로 가질 수 없는 충분히 큰 값
            long ret = 1_000_000_000_000_000_000;

            // lcm이 되기 전까지 계속해서 산다
            for (int i = 0; i < len; i++)
            {

                long remain = info[0] - i * info[1];
                long calc = i * info[2];

                if (remain > 0) 
                {
                    long buyB = remain / info[3];
                    if (remain % info[3] > 0) buyB++;
                    calc += buyB * info[4];
                }

                if (calc < ret) ret = calc;
            }

            Console.WriteLine(ret);
        }

        static long GetGCD(long _a, long _b)
        {

            if (_a < _b)
            {

                long temp = _a;
                _a = _b;
                _b = temp;
            }

            while(_b > 0)
            {

                long temp = _a % _b;
                _a = _b;
                _b = temp;
            }

            return _a;
        }
    }
}
