using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 22
이름 : 배성훈
내용 : 시그널
    문제번호 : 16113번

    구현, 문자열 문제다
    규칙대로 5줄로 나누고
    왼쪽에서 한칸씩 이동하면서 숫자를 읽었다
    1인 경우만 1줄로 검증되기에 1만 따로 빼서 먼저 검증했다
*/

namespace BaekJoon.etc
{
    internal class etc_0331
    {

        static void Main331(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n = int.Parse(sr.ReadLine());

            int len = n / 5;
            int[,] input = new int[5, len];


            for (int i = 0; i < 5; i++)
            {

                for (int j = 0; j < len; j++)
                {

                    input[i, j] = sr.Read();
                }
            }

            sr.Close();

            StringBuilder sb = new(len);
            for (int i = 0; i < len; )
            {

                if (input[0, i] == '.') 
                {

                    i++;
                    continue; 
                }

                if (i + 1 >= len || input[0, i + 1] == '.')
                {

                    bool isOne = true;
                    for (int j = 0; j < 5; j++)
                    {

                        if (input[j, i] == '#') continue;
                        isOne = false;
                    }

                    if (isOne)
                    {

                        i++;
                        sb.Append(1);
                        continue;
                    }
                }

                int add = ReadArr(i);
                i += 3;
                sb.Append(add);
            }

            Console.WriteLine(sb);

            int ReadArr(int _idx)
            {

                if (input[0, _idx + 1] == '.') return 4;

                if (input[1, _idx] == '.')
                {

                    if (input[2, _idx] == '.') return 7;
                    else if (input[3, _idx] == '.') return 3;
                    else return 2;
                }

                if (input[1, _idx + 2] == '.')
                {

                    if (input[3, _idx] == '.') return 5;
                    else return 6;
                }

                if (input[2, _idx + 1] == '.') return 0;

                if (input[3, _idx] == '.') return 9;
                return 8;
            }
        }
    }
}
