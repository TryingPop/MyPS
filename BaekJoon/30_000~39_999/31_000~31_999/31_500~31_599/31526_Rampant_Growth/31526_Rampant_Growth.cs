using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 9
이름 : 배성훈
내용 : Rampant Growth
    문제번호 : 31526번

    수학, 조합론 문제다.
    열마다 식물을 1개씩 심는다.
    그리고 같은행 인접한 열에 식물이 있으면 안된다.
    그래서 맨 왼쪽의 열 부터 시작해 채워가는데
    초기에는 r이된다.
    
    이후 부터는 인접한 경우를 제외한 r - 1의 경우의 수가 모두 동일하게 적용된다.
    그리고 각경우는 현재 위치에서 자리가 다르므로 다른 경우가 된다.

    그래서 r x (r - 1)^(c - 1)이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1755
    {

        static void Main1755(string[] args)
        {

            int row, col;

            Input();

            GetRet();

            void GetRet()
            {

                long ret = row--;
                long MOD = 998_244_353;

                for (int c = 1; c < col; c++)
                {

                    ret = (ret * row) % MOD;
                }

                Console.Write(ret);
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                row = int.Parse(temp[0]);
                col = int.Parse(temp[1]);
            }
        }
    }
}
