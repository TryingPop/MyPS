using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 26
이름 : 배성훈
내용 : 복붙의 달인
    문제번호 : 11008번

    구현, 문자열 문제다
    kmp 알고리즘을 변형해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0622
    {

        static void Main622(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int[] jump;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput());
                int test = int.Parse(sr.ReadLine());

                jump = new int[100];
                while (test-- > 0)
                {

                    string[] input = sr.ReadLine().Split();

                    SetJump(input[1]);
                    int pLen = input[1].Length;

                    int ptr = 0;
                    int ret = 0;
                    for (int i = 0; i < input[0].Length; i++)
                    {

                        if (input[0][i] == input[1][ptr]) ptr++;
                        else
                        {

                            while (ptr != 0)
                            {

                                if (input[0][i] == input[1][jump[ptr - 1]])
                                {

                                    ptr = jump[ptr - 1];
                                    ptr++;
                                    break;
                                }

                                ptr = jump[ptr - 1];
                            }
                        }

                        if (ptr == pLen)
                        {

                            ptr = 0;
                            ret -= pLen;
                            ret++;
                        }

                        ret++;
                    }

                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            void SetJump(string _str)
            {

                int ptr = 1;
                int len = _str.Length;
                int next = 0;
                
                while(ptr < len)
                {

                    while (true)
                    {

                        if (_str[next] == _str[ptr])
                        {

                            next++;
                            break;
                        }

                        if (next == 0) break;
                        next = jump[next - 1];
                    }

                    jump[ptr++] = next;
                }
            }
        }
    }

#if other
for(int t=int.Parse(Console.ReadLine());t-->0;)
{
    var s=Console.ReadLine().Split();
    Console.WriteLine(s[0].Replace(s[1],"").Length+s[0].Replace(s[1]," ").Count(e=>e==' '));
}
#elif other2
using System;
using System.Text;

namespace 중급
{
    class 복붙의달인
    {
        static void Main()
        {
            StringBuilder answer = new StringBuilder();
            int t = int.Parse(Console.ReadLine());
            while(t-- > 0)
            {
                string[] str = Console.ReadLine().Split();
                string p = str[1];
                string s = str[0];
                
                int cnt = 0;
                for (int i = 0; i < s.Length - p.Length + 1; i++)
                {
                    if (s.Substring(i, p.Length) == p)
                    {
                        cnt++;
                        i += p.Length - 1;
                    }
                }
                int time = s.Length - cnt * p.Length + cnt;
                answer.AppendLine(time.ToString());
            }
            Console.WriteLine(answer);
        }
    }
}
#endif
}
