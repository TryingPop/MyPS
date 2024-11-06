using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 17
이름 : 배성훈
내용 : 균형잡힌 세상
    문제번호 : 4949번
*/

namespace BaekJoon._19
{
    internal class _19_04
    {

        static void Main4(string[] args)
        {

            Stack<char> stk = new Stack<char>();

            StringBuilder sb = new StringBuilder();

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            while (true)
            {
                string input = sr.ReadLine();

                if (input == ".")
                {

                    break;
                }


                for (int i = 0; i < input.Length; i++)
                {

                    if (input[i] == '(')
                    {

                        stk.Push(input[i]);
                    }
                    else if (input[i] == '[')
                    {

                        stk.Push(input[i]);
                    }
                    else if (input[i] == ')')
                    {

                        if (stk.Count > 0)
                        {

                            if (stk.Pop() == '(')
                            {

                                continue;
                            }
                        }

                        stk.Push('(');
                        break;
                    }
                    else if (input[i] == ']')
                    {

                        if (stk.Count > 0)
                        {

                            if (stk.Pop() == '[')
                            {

                                continue;
                            }
                        }

                        stk.Push('[');
                        break;
                    }
                }

                if (stk.Count != 0)
                {

                    sb.AppendLine("no");
                    stk.Clear();
                }
                else
                {

                    sb.AppendLine("yes");
                }
            }
            sr.Close();

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            sw.WriteLine(sb);
            sw.Close();
        }
    }
}
