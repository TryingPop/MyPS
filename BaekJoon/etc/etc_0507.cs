using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 11
이름 : 배성훈
내용 : 2023 아주머학교 프로그래딩 정시머힌
    문제번호 : 28125번

    구현 문자열 문제다
    ^ 처리를 n으로 안해서 한 번 틀렸다
    이외는 하나씩 바꿔주는 처리를 하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0507
    {

        static void Main507(string[] args)
        {

            string NO = "I don't understand\n";
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = int.Parse(sr.ReadLine());
            string str = null;
            char[] ret = new char[100];
            int len = 0;
            while (test-- > 0)
            {

                str = sr.ReadLine();

                int origin = GetChar();
                
                if (origin <= len / 2)
                {

                    sw.Write(NO);
                }
                else
                {

                    for (int i = 0; i < len; i++)
                    {

                        sw.Write(ret[i]);
                    }
                    sw.Write('\n');
                }

                len = 0;
            }

            sw.Close();
            sr.Close();

            int GetChar()
            {

                int origin = 0;
                for (int i = 0; i < str.Length; i++)
                {

                    if (str[i] == '@') ret[len] = 'a';
                    else if (str[i] == '[') ret[len] = 'c';
                    else if (str[i] == '!') ret[len] = 'i';
                    else if (str[i] == ';') ret[len] = 'j';
                    else if (str[i] == '^') ret[len] = 'n';
                    else if (str[i] == '0') ret[len] = 'o';
                    else if (str[i] == '7') ret[len] = 't';
                    else if (str[i] == '\\')
                    {

                        i++;
                        if (str[i] == '\'') ret[len] = 'v';
                        else 
                        {

                            i++;
                            ret[len] = 'w'; 
                        }
                    }
                    else
                    {

                        origin++;
                        ret[len] = str[i];
                    }

                    len++;
                }

                return origin;
            }
        }
    }

#if other
using System;
using System.Text;

public class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        StringBuilder sb = new();
        for (int i = 0; i < n; i++)
        {
            string word = Console.ReadLine(), origin = string.Empty;
            int count = 0;
            for (int j = 0; j < word.Length; j++)
            {
                if (!char.IsLetter(word[j]))
                {
                    origin += Original(word, ref j);
                    count++;
                }
                else
                    origin += word[j];
            }
            if (count >= (origin.Length + 1) / 2)
                sb.AppendLine("I don't understand");
            else
                sb.AppendLine(origin);
        }
        Console.Write(sb.ToString());
    }
    static char Original(string word, ref int start)
    {
        if (word[start] == '\\')
        {
            if (word[++start] == '\\')
            {
                start++;
                return 'w';
            }
            return 'v';
        }
        return word[start] switch
        {
            '@' => 'a',
            '[' => 'c',
            '!' => 'i',
            ';' => 'j',
            '^' => 'n',
            '0' => 'o',
            '7' => 't'
        };
    }
}
#elif othter2
using System.IO;
using System.Text;
using System;
using System.Collections.Generic;

class Programs
{

    static StreamReader sr = new StreamReader(Console.OpenStandardInput(), Encoding.Default);
    static StreamWriter sw = new StreamWriter(Console.OpenStandardOutput(), Encoding.Default);
    static Dictionary<char,char> dic = new Dictionary<char, char>();
    static void Main(String[] args)
    {
        int n = int.Parse(sr.ReadLine());
        dic.Add('@', 'a');
        dic.Add('[', 'c');
        dic.Add('!', 'i');
        dic.Add(';', 'j');
        dic.Add('^', 'n');
        dic.Add('0', 'o');
        dic.Add('7', 't');
       // dic.Add('\\', new KeyValuePair<string, string>("","v"));
       //dic.Add('\\', new KeyValuePair<string, string>("\'","w"));
        StringBuilder sb = new StringBuilder();
       
        for (int i = 0; i < n; i++)
        {
            sb.Append( sr.ReadLine());
            int size = sb.Length;
            int count = 0;
            for (int j = 0; j < sb.Length; j++)
            {
                if (dic.ContainsKey(sb[j]))
                {
                    sb[j] = dic[sb[j]];
                    count++;
                }
                else if(sb[j]=='\\')
                {
                    if(  j + 2 < sb.Length&& sb[j + 1] == '\\'&&sb[j+2]=='\'')
                    {
                        sb[j] = 'w';
                        sb.Remove(j + 1,2);
                        count++;
                    }
                    else if (j + 1 < sb.Length && sb[j + 1] == '\'')
                    {
                        sb[j] = 'v';
                        sb.Remove(j + 1, 1);
                        count++;
                    }
                }
            }
            if(count>=sb.Length/2.0)
            {
                sw.WriteLine("I don't understand");
            }
            else
            {
                sw.WriteLine(sb.ToString());
            }
            sb.Clear();
        }
        sw.Dispose();
    }
}
#endif
}
