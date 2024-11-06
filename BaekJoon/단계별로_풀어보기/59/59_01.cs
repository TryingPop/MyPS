using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 26
이름 : 배성훈
내용 : 스택 2
    문제번호 : 28278번

    스택 문제다
    배열을 이용해 조건에 맞게 구현했다
*/

namespace BaekJoon._59
{
    internal class _59_01
    {

        static void Main1(string[] args)
        {

            string EMPTY = "-1\n";
            string ZERO = "1\n";
            string NOT_ZERO = "0\n";

            StreamReader sr;
            StreamWriter sw;

            int[] stack;
            int n;
            int len;

            Solve();
            void Solve()
            {

                Init();

                for (int i = 0; i < n; i++)
                {

                    int op = ReadInt();

                    Op(op);
                }

                sw.Close();
                sr.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
                stack = new int[n];
                len = 0;
            }

            void Op(int _op)
            {

                if (_op == 1)
                {

                    int add = ReadInt();
                    stack[len++] = add;
                    return;
                }

                if (_op == 2)
                {

                    if (len == 0) sw.Write(EMPTY);
                    else sw.Write($"{stack[--len]}\n");
                    return;
                }

                if (_op == 3)
                {

                    sw.Write($"{len}\n");
                    return;
                }

                if (_op == 4)
                {

                    if (len == 0) sw.Write(ZERO);
                    else sw.Write(NOT_ZERO);
                    return;
                }

                if (len > 0) sw.Write($"{stack[len - 1]}\n");
                else sw.Write(EMPTY);
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
}
