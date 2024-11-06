using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 다음 소수
    문제번호 : 4134번

    입력값에 0, 1이 있어서 여러 번 틀렸다
*/

namespace BaekJoon._22
{
    internal class _22_08
    {

        static void Main8(string[] args)
        {

            int len = int.Parse(Console.ReadLine());

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < len; i++)
            {

                uint input = uint.Parse(Console.ReadLine());

                // 입력값에 0, 1이 있고 아래는 0, 1 인 경우 그대로 반환해서 다음 소수가 아니다
                uint result = input < 2 ? 2 : input;
                bool chk;
                
                // 다음 소수찾기
                while (true)
                {

                    chk = true;
                    uint bound = (uint)(Math.Sqrt(result)) + 1;
                    for (uint j = 2; j < bound; j++)
                    {

                        if (result % j == 0)
                        {

                            chk = false;
                            break;
                        }
                    }

                    if (chk)
                    {

                        break;
                    }

                    result++;
                }

                sb.AppendLine(result.ToString());
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.Write(sb);
            }
        }
    }
}
