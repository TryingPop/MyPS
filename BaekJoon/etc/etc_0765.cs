using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 21
이름 : 배성훈
내용 : 알파벳 블록
    문제번호 : 27497

    덱, 스택, 문자열 문제다
    투 포인터를 이용해서 덱처럼 활용했다
    따로 인덱스 조정을 하기 싫어 그냥 2배수 배열을 만들었다
*/

namespace BaekJoon.etc
{
    internal class etc_0765
    {

        static void Main765(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int[] order;
            int[] str;

            int n;
            int l, r, len;

            Solve();
            void Solve()
            {

                Init();

                for (int i = 0; i < n; i++)
                {

                    int op = ReadInt();

                    if (op == 1)
                    {

                        int ch = ReadChar();
                        str[++r] = ch;
                        order[len++] = r;
                    }
                    else if (op == 2)
                    {

                        int ch = ReadChar();
                        str[--l] = ch;
                        order[len++] = l;
                    }
                    else
                    {

                        if (len == 0) continue;
                        len--;
                        if (l == order[len]) l++;
                        else r--;
                    }
                }

                sr.Close();

                for (int i = l; i <= r; i++)
                {

                    sw.Write((char)str[i]);
                }

                if (r < l) sw.Write(0);

                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                n = ReadInt();

                str = new int[n * 2];
                order = new int[n];

                l = n;
                r = n - 1;
                len = 0;
            }

            int ReadChar()
            {

                int c, ret = 0;
                ret = sr.Read();
                if (sr.Read() == '\r') sr.Read();

                return ret;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// cs27497 - rby
// 2024-01-26 23:34:34
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace cs27497
{
    internal class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            int N = int.Parse(sr.ReadLine());
            string line;

            Stack<bool> ord = new Stack<bool>(N + 1);
            Stack<char> head = new Stack<char>(N + 1);
            Stack<char> tail = new Stack<char>(N + 1);

            for(int i = 0; i < N; i++)
            {
                line = sr.ReadLine();

                if (line[0] == '2')
                {
                    head.Push(line[2]);
                    ord.Push(true);
                }
                else if (line[0] == '1')
                {
                    tail.Push(line[2]);
                    ord.Push(false);
                }
                else
                {
                    if (ord.Count == 0) continue;

                    if (ord.Pop()) head.Pop();
                    else tail.Pop();
                }
            }

            if (ord.Count == 0)
            {
                sb.AppendLine("0");
            }
            else
            {
                char[] chars = new char[tail.Count];
                while (head.Count > 0)
                {
                    sb.Append(head.Pop());
                }

                int cur = tail.Count - 1;
                while (tail.Count > 0)
                {
                    chars[cur--] = tail.Pop();
                }
                sb.AppendLine(new string(chars));
            }
            sw.Write(sb);
            sw.Close();
            sr.Close();
        }
    }
}

#endif
}
