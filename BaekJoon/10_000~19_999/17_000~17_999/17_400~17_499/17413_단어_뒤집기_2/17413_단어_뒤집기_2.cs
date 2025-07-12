using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 1
이름 : 배성훈
내용 : 단어 뒤집기 2
    문제번호 : 17413번

    구현, 문자열, 스택 문제다
    뒤집는게 필요할 때 스택을 이용해 뒤집었다
    그리고 뒤집는 때는, 태그가 시작되는 '<' 거나 혹은 띄어쓰기가 오는 순간이다
*/

namespace BaekJoon.etc
{
    internal class etc_0417
    {

        static void Main417(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            string str = sr.ReadLine();
            sr.Close();

            StringBuilder sb = new(str.Length);
            Stack<char> s = new(str.Length);

            int interval = 0;
            for (int i = 0; i < str.Length; i++)
            {

                if (str[i] == '<') interval++;

                if (interval > 0) 
                {

                    while (s.Count > 0)
                    {

                        sb.Append(s.Pop());
                    }

                    sb.Append(str[i]); 
                }
                else if (str[i] == ' ')
                {

                    while (s.Count > 0)
                    {

                        sb.Append(s.Pop());
                    }

                    sb.Append(str[i]);
                }
                else s.Push(str[i]);

                if (str[i] == '>') interval--;
            }

            while (s.Count > 0)
            {

                sb.Append(s.Pop());
            }

            sw.WriteLine(sb);
            sw.Close();
        }
    }
#if other
using System;
using System.IO;
using System.Text;

using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
using var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
var buffer = new StringBuilder();
int c;
char ch;
var inTag = false;
while (!((c = sr.Read()) is -1 or '\n' or '\r'))
{
    ch = (char)c;

    if (!inTag)
    {
        if (ch is ' ' or '<')
        {
            if (ch == '<')
                inTag = true;

            for (int i = buffer.Length - 1; i >= 0; i--)
                sw.Write(buffer[i]);
            buffer.Clear();
            sw.Write(ch);
            continue;
        }
        buffer.Append(ch);
        continue;
    }

    if (ch == '>')
        inTag = false;
    sw.Write(ch);
}
for (int i = buffer.Length - 1; i >= 0; i--)
    sw.Write(buffer[i]);
#endif
}
