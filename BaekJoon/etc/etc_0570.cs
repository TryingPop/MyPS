using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 18
이름 : 배성훈
내용 : 스네이크 그리기
    문제번호 : 27513번

    구현, 해구성하기 문제다
    경우의 수를 4개로 나눠서 해결했다

    아이디어는 다음과 같다
    맵이 짝 x 짝, 짝 x 홀, 홀 x 짝, 홀 x 홀 사이즈로 구분가능해 보였고
    4 x 6, 5 x 6, 5 x 7 을 예제로 직접 만들어보면서 일반화를 했다
    
    먼저 홀 x 짝 과 짝 x 홀은 90도 회전하면 되는데, 두 개의 경우로 쪼개서 해결했다
    짝 x 짝은 짝 x 홀의 일반화에 포함시킬 수 있어 함께 풀었다

    먼저 짝 x ? 인 경우로 보자
    ㄱ으로 먼저 지렁이를 이동시켰다
    그리고 ㄹ 의 형태로 이동하면서 순서를 부여했다
    예를들어 6 x 5 사이즈로 순서를 부여하면 다음과 같다
        1   2   3   4   5
       30  29  28  27   6
       23  24  25  26   7
       22  21  20  19   8        
       15  16  17  18   9
       14  13  12  11  10

    그리고 홀 x 짝의 경우는 전체를 90도 회전시켜서도 가능하나 ㄱ으로 시작하는 일관성 때문에
    ㄹ 경로만 90도 회전해서 해결했다
    예를들어 5 x 6 사이즈로 순서를 부여하면 다음과 같다
        1   2   3   4   5   6
       30  23  22  15  14   7
       29  24  21  16  13   8
       28  25  20  17  12   9
       27  26  19  18  11  10

    이제 홀 x 홀의 경우 3 x 3을 보면서 1개만 남지 않을까 추측했고
    1개만 남는 해를 찾아 제출하니 통과했다 -> 왜 한개만 남는지는 추후에 알아봐야겠다

    홀 x 홀도 ㄱ형태로 진행해서 해를 만들었다
    그리고 u -> n 순으로 하다가 ㄹ으로 진행하는 식으로 1칸을 비웠다 
    예를들어 5 x 7 사이즈로 순서를 부여하면 다음과 같다
        1   2   3   4   5   6   7
       34   X  24  23  16  15   8
       33  32  25  22  17  14   9
       30  31  26  21  18  13  10
       29  28  27  20  19  12  11

    이것을 코드로 작성한 것이 아래의 코드다
 */  

namespace BaekJoon.etc
{
    internal class etc_0570
    {

        static void Main570(string[] args)
        {

            int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            if (arr[0] == 1 && arr[1] == 1)
            {

                Console.Write($"{1}\n{1} {1}");
                return;
            }

            int ret = arr[0] * arr[1];
            StringBuilder sb = new(ret * 10 + 10);
            if ((arr[0] & 1) == 0)
            {

                // 짝 x 홀 or 짝 x 짝 사이즈
                sb.Append(ret);
                sb.Append('\n');

                for (int i = 1; i <= arr[1]; i++)
                {

                    sb.Append(1);
                    sb.Append(' ');
                    sb.Append(i);
                    sb.Append('\n');
                }

                for (int i = 2; i <= arr[0]; i++)
                {

                    sb.Append(i);
                    sb.Append(' ');
                    sb.Append(arr[1]);
                    sb.Append('\n');
                }

                int row = arr[0];
                while(row > 1)
                {

                    if ((row & 1) == 0)
                    {

                        for (int i = arr[1] - 1; i >= 1; i--)
                        {

                            sb.Append(row);
                            sb.Append(' ');
                            sb.Append(i);
                            sb.Append('\n');
                        }
                    }
                    else
                    {

                        for (int i = 1; i <= arr[1] - 1; i++)
                        {

                            sb.Append(row);
                            sb.Append(' ');
                            sb.Append(i);
                            sb.Append('\n');
                        }
                    }

                    row--;
                }
            }
            else if ((arr[1] & 1) == 0)
            {

                // 홀 x 짝 사이즈
                sb.Append(ret);
                sb.Append('\n');

                for (int i = 1; i <= arr[1]; i++)
                {

                    sb.Append(1);
                    sb.Append(' ');
                    sb.Append(i);
                    sb.Append('\n');
                }

                for (int i = 2; i <= arr[0]; i++)
                {

                    sb.Append(i);
                    sb.Append(' ');
                    sb.Append(arr[1]);
                    sb.Append('\n');
                }

                int col = arr[1] - 1;
                while(col > 0)
                {

                    if ((col & 1) == 1)
                    {

                        for (int i = arr[0]; i >= 2; i--)
                        {

                            sb.Append(i);
                            sb.Append(' ');
                            sb.Append(col);
                            sb.Append('\n');
                        }
                    }
                    else
                    {

                        for (int i = 2; i <= arr[0]; i++)
                        {

                            sb.Append(i);
                            sb.Append(' ');
                            sb.Append(col);
                            sb.Append('\n');
                        }
                    }

                    col--;
                }
            }
            else
            {

                // 홀 x 홀 사이즈
                sb.Append(ret - 1);
                sb.Append('\n');

                for (int i = 1; i <= arr[1]; i++)
                {

                    sb.Append(1);
                    sb.Append(' ');
                    sb.Append(i);
                    sb.Append('\n');
                }

                for (int i = 2; i <= arr[0]; i++)
                {

                    sb.Append(i);
                    sb.Append(' ');
                    sb.Append(arr[1]);
                    sb.Append('\n');
                }


                int col = arr[1] - 1;
                while(col > 2)
                {

                    if ((col & 1) == 0)
                    {

                        for (int i = arr[0]; i >= 2; i--)
                        {

                            sb.Append(i);
                            sb.Append(' ');
                            sb.Append(col);
                            sb.Append('\n');
                        }
                    }
                    else
                    {

                        for (int i = 2; i <= arr[0]; i++)
                        {

                            sb.Append(i);
                            sb.Append(' ');
                            sb.Append(col);
                            sb.Append('\n');
                        }
                    }

                    col--;
                }

                
                int row = arr[0];
                while (row > 2)
                {

                    if ((row & 1) == 1)
                    {

                        for (int i = 2; i >= 1; i--)
                        {

                            sb.Append(row);
                            sb.Append(' ');
                            sb.Append(i);
                            sb.Append('\n');
                        }
                    }
                    else
                    {

                        for (int i = 1; i <= 2; i++)
                        {

                            sb.Append(row);
                            sb.Append(' ');
                            sb.Append(i);
                            sb.Append('\n');
                        }
                    }

                    row--;
                }

                sb.Append("2 1\n");
            }

            Console.WriteLine(sb);
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structure
{
    /// <summary>
    /// 현재 만들어놓은 
    /// </summary>
    class HurozA2
    {
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        //public void Solve()
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            // 만약 segfault오류나면 쓰지 말것.
            // 만약 Argumentnull오류가 난다면 이놈은 쓰지말것.

            int[] input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);

            if ((input[0] * input[1]) % 2 != 0)
            {
                Console.WriteLine((input[0] * input[1]) - 1);
            }
            else
            {
                Console.WriteLine(input[0] * input[1]);
            }

            DoWork(input[0], input[1]);

            sr.Close();
            sw.Close();
        }

        private static void DoWork(int x, int y)
        {
            bool isOdd = false;
            bool isXOdd = false;
            bool isYOdd = false;
            if(x*y % 2 == 0)
            {
                isOdd = false;
            }
            else
            {
                isOdd = true;
            }

            if(x % 2 == 0)
            {
                isXOdd = false;
            }
            else
            {
                isXOdd = true;
            }
            if(y % 2 == 0)
            {
                isYOdd = false;
            }
            else
            {
                isYOdd = true;
            }


            int curx = 1;
            int cury = y;
            // 일단 1자로 쭉 내리기.
            // (1,y)에서 , (1,1까지)
            for(cury = y; cury > 1; cury--)
            {
                sw.WriteLine(curx.ToString() + " " +  cury.ToString());
            }
            // 반복.
            for(; cury <= y; cury++)
            {
                if (isOdd && cury > y - 3 && y > 2)
                {
                    //sw.WriteLine(1);
                    for (; curx <= x; curx++)
                    {
                        sw.WriteLine(curx.ToString() + " " + cury.ToString());
                    }
                    curx--;
                    cury++;  //한칸 올림.
                    int count = 0;
                    for(; curx > 1; curx--)
                    {
                        if(count == 0)
                        {
                            sw.WriteLine(curx.ToString() + " " + cury.ToString());
                        }
                        else
                        {
                            if (count % 2 == 1)
                            {
                                sw.WriteLine(curx.ToString() + " " + cury.ToString());
                                cury++;
                                sw.WriteLine(curx.ToString() + " " + cury.ToString());
                            }
                            else
                            {
                                sw.WriteLine(curx.ToString() + " " + cury.ToString());
                                cury--;
                                sw.WriteLine(curx.ToString() + " " + cury.ToString());
                            }
                            
                        }
                        count++;
                    }

                    break;
                }
                else if(!isOdd && cury > y - 3 && !isXOdd && isYOdd && y > 2)
                {
                    //sw.WriteLine(2);
                    for (; curx <= x; curx++)
                    {
                        sw.WriteLine(curx.ToString() + " " + cury.ToString());
                    }
                    curx--;
                    cury++;  //한칸 올림.
                    int count = 0;
                    for (; curx > 1; curx--)
                    {
                            if (count % 2 == 0)
                            {
                                sw.WriteLine(curx.ToString() + " " + cury.ToString());
                                cury++;
                                sw.WriteLine(curx.ToString() + " " + cury.ToString());
                            }
                            else
                            {
                                sw.WriteLine(curx.ToString() + " " + cury.ToString());
                                cury--;
                                sw.WriteLine(curx.ToString() + " " + cury.ToString());
                            }
                        count++;
                    }

                    break;
                }
                else
                {
                    //sw.WriteLine(3);
                    for (; curx <= x; curx++)
                    {
                        sw.WriteLine(curx.ToString() + " " + cury.ToString());
                    }
                    curx--;
                    cury++;

                    for (; curx > 2; curx--)
                    {
                        sw.WriteLine(curx.ToString() + " " + cury.ToString());
                    }
                    sw.WriteLine(curx.ToString() + " " + cury.ToString());
                }
            }
            
        }
    }


}

#elif other2
using System;
using System.Text;

namespace MyTestingField
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            string[] input = Console.ReadLine().Split(' ');
            int n = int.Parse(input[0]);
            int m = int.Parse(input[1]);
            int length = n * m;
            length = length % 2 == 0 ? length : length - 1;
            Console.WriteLine(length);

            if(m % 2 == 0)
            {
                sb.AppendLine("1 1");
                bool isDown = true;
                for(int i = 1; i <= m; i++)
                {
                    if (isDown)
                    {
                        for (int j = 2; j <= n; j++)
                        {
                            sb.Append(j);
                            sb.Append(' ');
                            sb.AppendLine(i.ToString());
                        }
                        isDown = false;
                    }
                    else
                    {
                        for(int j = n; j >= 2; j--)
                        {
                            sb.Append(j);
                            sb.Append(' ');
                            sb.AppendLine(i.ToString());
                        }
                        isDown = true;
                    }
                }
                for(int i = m; i >= 2; i--)
                {
                    sb.Append("1 ");
                    sb.AppendLine(i.ToString());
                }
            }
            else if (n % 2 == 0)
            {
                sb.AppendLine("1 1");
                bool isDown = true;
                for (int i = 1; i <= n; i++)
                {
                    if (isDown)
                    {
                        for (int j = 2; j <= m; j++)
                        {
                            sb.Append(i);
                            sb.Append(' ');
                            sb.AppendLine(j.ToString());
                        }
                        isDown = false;
                    }
                    else
                    {
                        for (int j = m; j >= 2; j--)
                        {
                            sb.Append(i);
                            sb.Append(' ');
                            sb.AppendLine(j.ToString());
                        }
                        isDown = true;
                    }
                }
                for (int i = n; i >= 2; i--)
                {
                    sb.Append(i);
                    sb.AppendLine(" 1");
                }
            }
            else
            {
                sb.AppendLine("1 1");
                bool isDown = true;
                for (int i = 1; i <= m - 2; i++)
                {
                    if (isDown)
                    {
                        for (int j = 2; j <= n; j++)
                        {
                            sb.Append(j);
                            sb.Append(' ');
                            sb.AppendLine(i.ToString());
                        }
                        isDown = false;
                    }
                    else
                    {
                        for (int j = n; j >= 2; j--)
                        {
                            sb.Append(j);
                            sb.Append(' ');
                            sb.AppendLine(i.ToString());
                        }
                        isDown = true;
                    }
                }
                sb.Append(n);
                sb.Append(" ");
                sb.AppendLine((m - 1).ToString());
                for(int i = n - 1; i > 1; i--)
                {
                    if(i % 2 == 0)
                    {
                        sb.Append(i);
                        sb.Append(" ");
                        sb.AppendLine((m - 1).ToString());
                        sb.Append(i);
                        sb.Append(" ");
                        sb.AppendLine(m.ToString());
                    }
                    else
                    {
                        sb.Append(i);
                        sb.Append(" ");
                        sb.AppendLine(m.ToString());
                        sb.Append(i);
                        sb.Append(" ");
                        sb.AppendLine((m - 1).ToString());
                    }
                }
                for(int i = m; i > 1; i--)
                {
                    sb.Append("1 ");
                    sb.AppendLine(i.ToString());
                }
            }
            Console.WriteLine(sb);
        }
    }
}

#endif
}
