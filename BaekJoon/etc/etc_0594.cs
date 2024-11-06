using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 22
이름 : 배성훈
내용 : 회문은 회문아니야!!
    문제번호 : 15927번

    애드 혹, 문자열 문제다
    팰린드롬의 성질로 풀었다

    전체가 팰린드롬이 아닌 문자열 -> 전체 길이 반환

    전체가 팰린드롬인데, 하나의 문자로만 이루어진 문자열은 
    어떻게 쪼개도 팰린드롬이 된다 -> -1 반환

    이제 전체가 팰린드롬인데 두 개 이상의 문자로만 이루어진 문자열 
    맨 앞이나 맨 뒤에 하나를 빼면, 팰린드롬이 보장안된다 -> 전체 길이 - 1 반환

    이렇게 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0594
    {

        static void Main594(string[] args)
        {

            StreamReader sr;

            Solve();
            void Solve()
            {

                sr = new(new BufferedStream(Console.OpenStandardInput(), bufferSize: 65536 * 8));
                string str = sr.ReadLine();
                sr.Close();

                int useAlpha = 0;
                char a = ' ';
                bool flag = true;
                for (int i = 0; i < str.Length; i++)
                {

                    int comp = str.Length - i - 1;
                    if (comp < i) break;

                    if (str[i] == str[comp]) 
                    {

                        if (a != str[i] && useAlpha < 2) 
                        {

                            a = str[i];
                            useAlpha++;
                        }
                        continue; 
                    }

                    flag = false;
                    break;
                }

                if (flag && useAlpha == 1) Console.WriteLine(-1);
                else if (flag) Console.WriteLine(str.Length - 1);
                else Console.WriteLine(str.Length);
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.Text;

namespace C_Project
{
    class Program

    {

        static void Main(string[] args)
        {

            StringBuilder sb = new StringBuilder();

            String S = Console.ReadLine();

            int idx, length = S.Length;
            for (idx = 0; idx < length / 2; ++idx)
            {
                if (S[idx] != S[length - 1 - idx])
                {
                    Console.Write(length);
                    return;
                }
            }

            char c = S[0];
            bool flag = true;

            for (idx = 0; idx < length; ++idx)
            {
                if (S[idx] != c)
                {
                    flag = false;
                    break;
                }
            }

            if (!flag) Console.Write(length - 1);
            else Console.Write(-1);

            Console.WriteLine(sb);
        }
    }
}

#elif other2
string input = Console.ReadLine();

// 1. 팰린드롬이 아니면
if(!Palindrome(ref input, 0, input.Length))
{
    Console.WriteLine(input.Length);
    return;
}

// 2. 팰린드롬일 때;
// 2.1. 전부 같은 문자
if(OnlyOne(ref input))
{
    Console.WriteLine(-1);
    return;
}

// 2.2. 다른문자 있음
for(int i = 1; i < input.Length; i++)
{
    if (!Palindrome(ref input, 0, input.Length - i))
    {
        Console.WriteLine(input.Length - i);
        return;
    }
    if (!Palindrome(ref input, i, input.Length - i))
    {
        Console.WriteLine(input.Length - i);
        return;
    }
}
Console.WriteLine(-1);
return;

bool Palindrome(ref string line, int startidx, int length)
{
    int endidx = startidx + length - 1;
    for(int i = length / 2; i >=0; i--)
    {
        if (line[startidx + i] != line[endidx - i])
            return false;
    }
    return true;
}

bool OnlyOne(ref string line)
{
    char c = line[0];
    for (int i = 0; i < line.Length; i++)
        if (line[i] != c)
            return false;
    return true;
}
#endif
}
