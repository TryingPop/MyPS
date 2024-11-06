using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 17
이름 : 배성훈
내용 : 괄호
    문제번호 : 9012번
*/

namespace BaekJoon._19
{
    internal class _19_03
    {

        static void Main3(string[] args)
        {

            StringBuilder sb = new StringBuilder();

            int len = int.Parse(Console.ReadLine());

            Stack<char> stk = new Stack<char>();

            for (int i = 0; i < len; i++)
            {

                string input = Console.ReadLine();

                for (int j = 0; j < input.Length; j++)
                {

                    if (input[j] == '(')
                    {

                        stk.Push(input[j]);
                    }
                    else
                    {

                        if (stk.Count <= 0)
                        {

                            stk.Push('(');
                            break;
                        }

                        stk.Pop();
                    }
                }

                if (stk.Count > 0)
                {

                    stk.Clear();
                    sb.AppendLine("NO");
                }
                else
                {

                    sb.AppendLine("YES");
                }
            }

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            sw.WriteLine(sb);
            sw.Close();
        }
    }
}
