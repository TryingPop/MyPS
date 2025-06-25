using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 25
이름 : 배성훈
내용 : ZOAC 4
    문제번호 : 23971번

    수학, 사칙연산 문제다
    아이디어는 다음과 같다
    그리디로 왼쪽 위 끝에서 시작해 놓을 수 있으면 놓고 거리를 재면서 놓는다
    이렇게 찾으면 수식을 
*/

namespace BaekJoon.etc
{
    internal class etc_1078
    {

        static void Main1078(string[] args)
        {

            int h, w, n, m;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }
            
            void GetRet()
            {

                long ret = 1L * (1 + (h - 1) / (n + 1)) * (1 + (w - 1) / (m + 1));
                Console.Write(ret);
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                h = int.Parse(temp[0]);
                w = int.Parse(temp[1]);
                n = int.Parse(temp[2]);
                m = int.Parse(temp[3]);
            }
        }
    }
}
