using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 세탁소 사장 동혁
    문제번호 : 2720번
*/

namespace BaekJoon._23
{
    internal class _23_03
    {

        static void Main3(string[] args)
        {

            int len = int.Parse(Console.ReadLine());

            StringBuilder sb = new StringBuilder();

            {

                int input;
                int calc;
                
                // 동전 단위 내림차순
                int[] n = new int[4] { 25, 10, 5, 1 };

                // 거스름돈 계산
                // 그리디 알고리즘 적용
                for (int i = 0; i < len; i++)
                {

                    input = int.Parse(Console.ReadLine());

                    for (int j = 0; j < n.Length; j++)
                    {

                        calc = input / n[j];
                        input = input % n[j];
                        sb.Append(calc.ToString());
                        if (j != n.Length - 1)
                        {

                            sb.Append(' ');
                        }
                        else
                        {

                            sb.Append('\n');
                        }
                    }
                }
            }

            // 정답 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.Write(sb);
            }
        }
    }
}
