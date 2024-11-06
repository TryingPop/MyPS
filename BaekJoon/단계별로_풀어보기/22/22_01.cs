using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 배수와 약수
    문제번호 : 5086번
*/

namespace BaekJoon._22
{
    internal class _22_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StringBuilder sb = new StringBuilder();

            int[] inputs;

            while (true)
            {

                inputs = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                // 탈출인지 확인 같은 경우는 없다
                if (inputs[0] == 0 && inputs[1] == 0)
                {

                    break;
                } 

                // inputs[1]이 inputs[0]으로 나누어 떨어지므로
                // input[0]은 input[1]의 약수
                if (inputs[1] % inputs[0] == 0)
                {

                    sb.AppendLine("factor");
                }

                // inputs[0]이 inputs[1]로 나누어 떨어지므로
                // input[0]은 inputs[1]의 배수
                else if (inputs[0] % inputs[1] == 0)
                {

                    sb.AppendLine("multiple");
                }

                // 약수도 배수도 아니다
                else
                {

                    sb.AppendLine("neither");
                }
                
            }

            sr.Close();

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(sb);
            }
        }
    }
}
