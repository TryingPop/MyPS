using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 13
이름 : 배성훈
내용 : 정답은 이수근이야!!
    문제번호 : 15888번

    수학, 브루트포스 문제다
    중근은 정수근에 포함 안된다, 이수근은 2이상인 정수에만 적용시켜야한다
    해당 조건으로 2번 틀렸다

    아이디어는 다음과 같다
    먼저 판별식으로 허수근, 중근이면 제외한다
    그리고 정수해 판별이므로, 정수는 곱셈과 나눗셈에 닫혀있으므로 
    정수해를 갖는다면 b, c 모두 a에 나눠떨어져야한다
    그래서 a를 나눠서 안나눠 떨어지는 경우도 제외했다
    그리고 화인한 뒤 b, c 를 a에 나눴다

    이후에 해당 수가 해가되는지 판별했다
    정수해이므로 절대값으로 봤을 때 0이아닌 두 정수를 곱하면 크거나 같아진다!
    그래서 정수해의 범위는 -100 ~ 100만 판별하면 된다
    그래서 해가되면 이수근 판별하고, 개수를 추가한다
    해를 2개 찾으면 정수근 or 이수근이고 2개가 안되면 둘다 틀렸근으로 했다

    이렇게 제출하니 이상없이 64ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0522
    {

        static void Main522(string[] args)
        {

            string RET1 = "이수근";
            string RET2 = "정수근";
            string RET3 = "둘다틀렸근";
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            int d = arr[1] * arr[1] - 4 * arr[0] * arr[2];
            if (d <= 0 || arr[1] % arr[0] != 0 || arr[2] % arr[0] != 0)
            {

                Console.WriteLine(RET3);
                return;
            }

            arr[1] /= arr[0];
            arr[2] /= arr[0];
            arr[0] = 1;

            int[] tp = new int[10];
            tp[0] = 1;
            for (int i = 1; i < 10; i++)
            {

                tp[i] = tp[i - 1] * 2;
            }

            int find = 0;
            bool isTwo = true;
            for (int i = -100; i <= 100; i++)
            {

                if (ChkInvalid(i)) continue;
                find++;

                if (!isTwo) continue;
                isTwo = false;
                for (int j = 1; j < 10; j++)
                {

                    if (tp[j] != i) continue;
                    isTwo = true;
                    break;
                }

                if (find == 2) break;
            }

            if (find == 2 && isTwo) Console.WriteLine(RET1);
            else if (find == 2) Console.WriteLine(RET2);
            else Console.WriteLine(RET3);

            bool ChkInvalid(int _chk)
            {

                int ret = 0;

                for (int i = 0; i < 3; i++)
                {

                    ret = ret * _chk + arr[i];
                }

                return ret != 0;
            }
        }
    }
}
