using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 별 찍기 7
    문제번호 : 2444번
*/

namespace BaekJoon._23
{
    internal class _23_06
    {

        static void Main6(string[] args)
        {

            int num = int.Parse(Console.ReadLine());

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 2 * num - 1; i++)
            {

                // 위 삼각
                if (i < num)
                {

                    // 공백
                    for (int j = 0; j < num - i - 1; j++)
                    {

                        sb.Append(' ');
                    }

                    // 별
                    for (int j = 0; j < 2 * i + 1; j++)
                    {

                        sb.Append('*');
                    }
                }
                // 아래 삼각
                else
                {

                    // 공백
                    for (int j = 0; j < i - num + 1; j++)
                    {

                        sb.Append(' ');
                    }

                    // 별
                    for (int j = 0; j < 2 * (2 * num - i) - 3; j++)
                    {

                        sb.Append('*');
                    }
                }

                sb.Append("\n");
            }

            Console.WriteLine(sb);
        }
    }
}
