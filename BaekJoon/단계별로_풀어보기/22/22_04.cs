using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 최소공배수
    문제번호 : 1934번

    A * B = GCD(A, B) * LCM(A, B)
    여기서 GCD는 최대 공약수(Greatest Common Divisior), LCM은 최소 공배수(Latest Common Multiple)
*/

namespace BaekJoon._22
{
    internal class _22_04
    {

        static void Main4(string[] args)
        {

            StringBuilder sb = new StringBuilder();


            int len = int.Parse(Console.ReadLine());
#if false

            for (int i = 0; i < len; i++)
            {

                int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                int gcd = 1;
                
                // 최대 공약수 찾기
                for (int j = inputs[0]; j >= 1; j--)
                {

                    if (inputs[1] % j == 0 && inputs[0] % j == 0 )
                    {

                        gcd = j;
                        break;
                    }
                }

                int result = (inputs[0] / gcd) * inputs[1];
                sb.AppendLine((result).ToString());
            }
#elif false
            Stack<int> factors = new Stack<int>();
            int chk;

            for (int i = 0; i < len; i++)

            {

                int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                for (int j = 1; j <= inputs[0]; j++)
                {

                    if (inputs[0] % j == 0)
                    {

                        factors.Push(j);
                    }
                }

                
                while (factors.Count > 0)
                {

                    chk = factors.Pop();
                    if (inputs[1] % chk == 0)
                    {

                        inputs[1] = inputs[1] / chk;
                        factors.Clear();
                    }
                }

                int result = inputs[0] * inputs[1];
                sb.AppendLine((result).ToString());
            }
#else
            // 유클리드 알고리즘 or 나눗셈 알고리즘
            for (int i = 0; i < len; i++)
            {

                int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                int gcd = 1;
                
                // 최대 공약수 찾기
                {

                    int a = inputs[0] > inputs[1] ? inputs[0] : inputs[1];
                    int b = inputs[0] > inputs[1] ? inputs[1] : inputs[0];
                    int r = 0;
                    while (true)
                    {

                        if (a % b == 0)
                        {

                            break;
                        }

                        r = a % b;
                        a = b;
                        b = r;
                    }
                    gcd = b;
                }

                int result = inputs[0] * (inputs[1] / gcd);
                sb.AppendLine(result.ToString());
            }
#endif

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(sb);
            }
        }
    }
}
