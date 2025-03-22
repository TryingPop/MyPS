using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 14
이름 : 배성훈
내용 : 2로 몇 번 나누어질까
    문제번호 : 1407번

    1 ~ 64까지 f함수를 돌렸다
    그러니, 홀수는 1, 짝수는 2의 배수였다

    그리고, 짝수부분만 보니
    1 ~ 32에 2배를 한 형태였다

    4배수 부분만 보면
    1 ~ 16에 4를 곱한 형태이다

    8배수 부분만 보면
    1 ~ 8에 8을 곱한 형태이다

    이렇게 반복되는 규칙을 찾으니, 홀수만 카운팅하고,
    2로 나눈 뒤 다시 홀수 카운팅에 2를 곱하고,
    또 2로 나눈 뒤 홀수만 카운팅하고 2를 곱하고.... 반복하는 방법을 떠올리게 되었다

    그래서 개수가 1개 남을때까지 홀수만 카운팅하면서 했다
    1개 인 경우, 홀수가 될때까지 2로 나눴고
    홀수가 된 순간에 2를 나눈만큼의 값을 더해줬다

    아래는 해당 아이디어를 코드로 나타낸 것이다
*/

namespace BaekJoon.etc
{
    internal class etc_0034
    {

        static void Main34(string[] args)
        {


            long[] info = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            // 2로 나눠야한다;
            // 2배수만 따로 처리하자!
            // 1, 2, 1, 4, 1, 2, 1, 8, 1, 2, 1, 4, 1, 2, 1, 16          <- 1 ~ 16
            // ?, 1, ?, 2, ?, 1, ?, 4, ?, 1, ?, 2, ?, 1, ?, 8           <- 2로나눔
            // ?, ?, ?, 1, ?, ?, ?, 2, ?, ?, ?, 1, ?, ?, ?, 4           <- 4로 나눔
            // ?를 제외하면 반복되는 형상이 보인다!

            // 홀수부분만 세어주면 된다!
            long ret = 0;
            long curValue = 1;

            // 값이 같아질때까지 홀수만 센다!
            while (info[0] != info[1])
            {

                if ((info[0] & 1) == 1)
                {

                    // 맨 앞이 홀수인 경우
                    long calc = ((info[1] - info[0]) / 2 + 1);
                    calc *= curValue;
                    ret += calc;
                    
                    info[0]++;
                    info[0] /= 2;
                    info[1] /= 2;
                }
                else
                {

                    // 맨 앞이 짝수인 경우
                    info[0]++;

                    long calc = ((info[1] - info[0]) / 2 + 1);
                    calc *= curValue;
                    ret += calc;

                    info[0] /= 2;
                    info[1] /= 2;
                }

                // 2로 나눴으니 더해줄 값은 2를 곱하게된다
                curValue *= 2;
            }

            // 같아지는 경우다!
            // 짝수면 홀수만들기!
            while ((info[0] & 1) == 0)
            {

                curValue *= 2;
                info[0] /= 2;
            }

            // info[0]이 홀수가 되었다!
            // 이때 나눈 값을 더해준다!
            ret += curValue;

            // 결과 출력
            Console.WriteLine(ret);
        }
    }
}
