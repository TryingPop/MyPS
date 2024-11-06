using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 17
이름 : 배성훈
내용 : 스택
    문제번호 : 10828번
*/

namespace BaekJoon._19
{
    internal class _19_01
    {

        static void Main1(string[] args)
        {

            Stack<int> stk = new Stack<int>();
            StringBuilder sb = new StringBuilder();


            int len = int.Parse(Console.ReadLine());

            for (int i = 0; i < len; i++)
            {

                string[] inputs = Console.ReadLine().Split(' ');

                if (inputs[0] == "push")
                {

                    stk.Push(int.Parse(inputs[1]));
                }
                else if (inputs[0] == "pop")
                {

                    if (stk.Count > 0)
                    {

                        sb.AppendLine(stk.Pop().ToString());
                    }
                    else
                    {

                        sb.AppendLine("-1");
                    }
                }
                else if (inputs[0] == "size")
                {

                    sb.AppendLine(stk.Count.ToString());
                }
                else if (inputs[0] == "empty")
                {

                    if (stk.Count > 0)
                    {

                        sb.AppendLine("0");
                    }
                    else
                    {

                        sb.AppendLine("1");
                    }
                }
                else if (inputs[0] == "top")
                {

                    if (stk.Count == 0)
                    {

                        sb.AppendLine("-1");
                    }
                    else
                    {

                        int top = stk.Pop();
                        stk.Push(top);
                        sb.AppendLine(top.ToString());
                    }
                }
            }

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            Console.WriteLine(sb);
            sw.Close();
            sb.Clear();
        }
    }
}
