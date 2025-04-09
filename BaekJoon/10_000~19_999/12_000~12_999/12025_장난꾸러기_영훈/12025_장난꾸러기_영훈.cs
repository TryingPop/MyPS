using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 15
이름 : 배성훈
내용 : 장난꾸러기 영훈
    문제번호 : 12025번

    수학, 비트마스킹 문제다
    
    아이디어는 다음과 같다 
    1, 2, 6, 7의 개수 cnt를 찾고
    사전순에서 n번째 값을 찾아야한다

    먼저 n번째가 존재하는지 판별해야한다
    n이 유효한건 2^cnt개 만큼 다른 경우가 가능하다
    그래서 n > 2^cnt면 불가능하다

    n번째 값을 찾아야한다
    1번째의 경우 1 - 6, 2 - 7에서 6, 7 모두가 6 -> 1, 7 -> 2가 되어야한다
    2번째의 경우 맨 오른쪽에 있는 1,2,6,7의 원소만 1 -> 6이, 2 -> 7이 되어야하고
        6 -> 1, 7 -> 2가 되어야한다

    이를 10까지 하다보면 비트마스킹과 비슷한 규칙임을 알 수 있다
    1번인 경우 1 - 1 = 0으로 취급하면 000...00 << 모두 최소값
    2번인 경우 2 - 1 = 1으로 취급하면 000...01 << 맨 뒤에만 최대값 이외는 최소값
    3번인 경우 3 - 1 = 2으로 취급하면 000...10 << 맨 뒤의 바로앞 원소만 최대값, 이외는 최소값

    ... 이렇게 하면 비트마스킹으로 풀 수 있게된다

    이를 아래 코드는 위 논리를 표현한 것이다
    이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0240
    {

        static void Main240(string[] args)
        {

            string str = Console.ReadLine();
            long n = long.Parse(Console.ReadLine());

            char[] ret = new char[str.Length];
            int[] findIdx = new int[str.Length];
            int cnt = 0;

            for (int i = str.Length - 1; i >= 0; i--)
            {

                if (str[i] == '1' || str[i] == '6' || str[i] == '2' || str[i] == '7') 
                {

                    findIdx[cnt++] = i;
                }

                ret[i] = str[i];
            }

            long chk = 1;
            for (int i = 0; i < cnt; i++)
            {

                chk *= 2;
            }

            if (chk < n)
            {

                Console.WriteLine(-1);
            }
            else
            {

                long calc = n - 1;
                for (int i = 0; i < cnt; i++)
                {

                    bool isMin = true;
                    if (((long)(1 << i) & calc) != 0)
                    {

                        isMin = false;
                    }

                    ret[findIdx[i]] = GetVal(ret[findIdx[i]], isMin);
                }

                for (int i = 0; i < ret.Length; i++)
                {

                    Console.Write(ret[i]);
                }
            }
        }

        static char GetVal(char _input, bool _isMin)
        {

            if (_isMin)
            {

                if (_input == '6')
                {

                    return '1';
                }
                else if (_input == '7')
                {

                    return '2';
                }
            }
            else
            {

                if (_input == '1')
                {

                    return '6';
                }
                else if(_input == '2')
                {

                    return '7';
                }
            }

            return _input;
        }
    }
}
