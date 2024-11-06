using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 2
이름 : 배성훈
내용 : 쇠막대기
    문제번호 : 10799번

    스택 문제다
    아이디어는 다음과 같다

    우선, 파이프를 자르면 현재 파이프만큼 추가된다
    파이프가 추가되면 결과에 카운팅하고, 현재 파이프 개수를 센다
    레이저 나오면 파이프 갯수만큼 카운팅한다

    그렇게 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0430
    {

        static void Main430(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 4);

            string str = sr.ReadLine();
            sr.Close();
            int pipe = 0;
            long ret = 0;

            for (int i = 0; i < str.Length; i++)
            {

                if (str[i] == '(')
                {

                    // 파이프 세기
                    ret++;
                    pipe++;
                }
                else if (str[i] == ')')
                {

                    pipe--;
                    if (str[i - 1] == '(')
                    {

                        // 레이저이므로 파이프가 아니라서 1개 뺀다
                        ret--;
                        // 자르는 경우 현재 파이프 만큼 들어온다
                        ret += pipe;
                    }
                }
            }

            Console.WriteLine(ret);
        }
    }
}
