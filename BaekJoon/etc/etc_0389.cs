using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 29
이름 : 배성훈
내용 : iSharp
    문제번호 : 3568번

    문자열, 파싱 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0389
    {

        static void Main389(string[] args)
        {

            string ARR = "[]";
            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new(Console.OpenStandardOutput());
            StringBuilder sb = new StringBuilder(120);

            string[] str = sr.ReadLine().Split();
            sr.Close();
            for (int i = 1; i < str.Length; i++)
            {

                sb.Append(str[0]);
                int end = str[i].Length - 1;
                for (int j = str[i].Length - 2; j >= 0; j--)
                {

                    // 연산자 기호 처리
                    if (str[i][j] == '*') sb.Append('*');
                    else if (str[i][j] == '&') sb.Append('&');
                    else if (str[i][j] == ']')
                    {

                        sb.Append(ARR);
                        j--;
                    }
                    else 
                    { 
                        
                        end = j + 1;
                        break;
                    }
                }

                // 변수명 처리
                sb.Append(' ');
                sb.Append(str[i].Substring(0, end));
                // 끝문자 처리
                sb.Append(';');

                // 출력 및 sb 초기화
                sw.WriteLine(sb);
                sb.Clear();
            }

            sw.Close();
        }
    }

#if other
using System;
using System.Text;

public class Program
{
    static void Main()
    {
        string[] array = Console.ReadLine().Split(' ');
        Console.Write(Parse(array[0], array[1..]));
    }
    static string Parse(string type, string[] variables)
    {
        StringBuilder sb = new();
        foreach (string var in variables)
        {
            string n = string.Empty, t = string.Empty;
            for (int i = 0; i < var.Length; i++)
            {
                if (var[i] == ',' || var[i] == ';')
                    break;
                if (char.IsLetter(var[i]))
                    n += var[i];
                else
                {
                    if (var[i] == '[')
                    {
                        i++;
                        t = "[]" + t;
                    }
                    else
                        t = var[i] + t;
                }
            }
            sb.AppendLine($"{type}{t} {n};");
        }
        return sb.ToString();
    }
}
#endif
}
