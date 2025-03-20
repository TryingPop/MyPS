using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 20
이름 : 배성훈
내용 : 등차수열의 합
    문제번호 : 1419번

    수학 문제다.
    아이디어는 단순하다.
    짝수인 경우, 1 + 2 + ... + k부터 k 간격으로 나온다.
    또한 1 + 3 + ... + 2k - 1 = k^2부터 k 간격으로 나온다.

    공차가 3인 경우는 1 + 4 + ... + 3k - 2 = 1 + 2 + ... + k + k^2 - k이므로 
    1 + 2 + ... k로 만들어진다.
    즉, 공차가 홀수인 경우 시작값을 적당히 변형해 공차 1로 만들어지고, 
    짝수인 경우 시작값을 적당히 변형해 공차 2로 변형할 수 있다.

    k가 홀수일 때, 1 + 2 + ... + k부터 k 범위로 존재한다.
    그래서 중요한건 a ~ b 범위중 x + nd의 값이 몇 개 존재하는지 카운팅하는 문제와 동형이 된다.
    그래서 0 ~ s개 k의 배수 세는 함수 Cnt를 만들어 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1425
    {

        static void Main1425(string[] args)
        {

            int l = int.Parse(Console.ReadLine());
            int r = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());

            Console.Write(GetRet());

            int GetRet()
            {

                if ((k & 1) == 1)
                {

                    // 홀수
                    int sub = (k * (k + 1)) / 2;

                    int left = Cnt(l - 1 - sub, k);
                    int right = Cnt(r - sub, k);

                    return right - left;
                }
                else
                {

                    int sub1 = (k * (k + 1)) / 2;
                    int sub2 = k * k;

                    int left = Cnt(l - sub1 - 1, k) + Cnt(l - sub2 - 1, k);
                    int right = Cnt(r - sub1, k) + Cnt(r - sub2, k);

                    return right - left;
                }
            }

            int Cnt(int _e, int _k) 
            {

                if (_e < 0) return 0;

                return 1 + _e / _k;
            }
        }
    }
}
