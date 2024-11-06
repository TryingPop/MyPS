using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 2
이름 : 배성훈
내용 : 2진수 8진수
    문제번호 : 1373번

    수학, 문자열 문제다
    앞에서부터 읽었다
    처음에 0인 경우 작성해 101 -> 05로 출력해 1번 틀렸고
    이후 0을 생각안해 97%에서 0 ->  아무것도 출력안해 1번 더 틀렸다
    
*/

namespace BaekJoon.etc
{
    internal class etc_0749
    {

        static void Main749(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);

            string str = sr.ReadLine();
            sr.Close();


            StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 8);
            int s = str.Length % 3;

            bool cnt = false;
            int n = 0;
            for (int i =0; i < s; i++)
            {

                cnt = true;
                n = n * 2 + str[i] - '0';
            }

            if (cnt) sw.Write(n);
            n = 0;

            for (int i = s; i < str.Length; i += 3)
            {

                cnt = true;
                n = (str[i] - '0') * 4 + (str[i + 1] - '0') * 2 + (str[i + 2] - '0');
                sw.Write(n);
            }

            if (!cnt) sw.Write(0);
            sw.Close();
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

        var str = sr.ReadLine();
        Stack<int> stack = new Stack<int>();

        for(int i = str.Length - 1; i >= 0; i -= 3)
        {
            int a = 0;

            a += str[i] - '0';
            if(i - 1 >= 0)
            {
                a += (str[i - 1] - '0') * 2;
            }
            if(i - 2 >= 0)
            {
                a += (str[i - 2] - '0') * 4;
            }

            stack.Push(a);
        }

        while(stack.Count > 0)
        {
            sw.Write(stack.Pop());
        }

        sw.Close();
        sr.Close();
    }
}
#endif
}
