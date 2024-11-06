using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 18
이름 : 배성훈
내용 : AC
    문제번호 : 5430번

    기징 빠르게 푼 사람과 10배 이상 느리다
    빠르게 푼 사람을 보니
    숫자 읽어오는걸 하나씩 읽어왔다
    '['와 ',' ']'인 경우는 넘겨버렸다

    그리고 RemoveAt을 하는게 아닌
    startIdx와 lastIdx를 이용해 처음과 끝을 표현했다
    그리고 방향을 int로 할당하는게 아닌 bool로 할당했다!
*/

namespace BaekJoon._20
{
    internal class _20_07
    {

        static void Main7(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < len; i++)
            {

                string cmd = sr.ReadLine();
                sr.ReadLine();
                string str = sr.ReadLine();
                str = str.Substring(1, str.Length - 2);

                List<int> nums = new List<int>();

                if (str != string.Empty)
                {

                     nums = Array.ConvertAll(str.Split(','), int.Parse).ToList();
                }

                bool chk = false;

                int dir = 1;
                int ptr = 0;
                for (int j = 0; j < cmd.Length; j++)
                {

                    if (cmd[j] == 'R')
                    {

                        dir *= -1;
                        ptr = nums.Count - 1 - ptr;
                    }
                    else if (cmd[j] == 'D')
                    {

                        if (nums.Count > 0)
                        {

                            nums.RemoveAt(ptr);
                            if (dir < 0)
                            {

                                ptr--;
                            }
                        }
                        else
                        {

                            chk = true;
                            break;
                        }
                    }
                }

                if (chk)
                {

                    sb.AppendLine("error");
                    continue;
                }

                
                sb.Append("[");

                if (nums.Count > 0)
                {

                    sb.Append($"{nums[ptr]}");

                    if (dir > 0)
                    {
                        for (int j = 1; j < nums.Count; j++)
                        {

                            sb.Append($",{nums[j]}");
                        }
                    }
                    else
                    {

                        for (int j = ptr - 1; j >= 0; j--)
                        {

                            sb.Append($",{nums[j]}");
                        }
                    }
                }

                sb.AppendLine("]"); //  sb.Append("]").Append("\n");
            }
            sr.Close();

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
                Console.WriteLine(sb);
        }
    }
}
