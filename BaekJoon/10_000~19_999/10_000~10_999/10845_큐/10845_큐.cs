using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 12
이름 : 배성훈
내용 : 큐
    문제번호 : 10845번

    큐 문제다
    라빈 - 카프 알고리즘을 이용해 명령어를 해석했다
*/

namespace BaekJoon.etc
{
    internal class etc_1053
    {

        static void Main1053(string[] args)
        {

            int PUSH = 466650;
            int POP = 14864;
            int SIZE = 544705;
            int EMPTY = 4066604;
            int FRONT = 5137928;
            int BACK = 29863;

            string E = "-1\n";
            StreamReader sr;
            StreamWriter sw;

            int n;
            int[] deck;
            int length;
            int head, tail;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {


                for (int i = 0; i < n; i++)
                {

                    int op = ReadOp();
                    if (op == PUSH)
                    {

                        int num = ReadInt();
                        deck[++tail] = num;
                        length++;
                    }
                    else if (op == POP)
                    {

                        if (length == 0) sw.Write(E);
                        else
                        {

                            length--;
                            sw.Write($"{deck[head++]}\n");
                        }
                    }
                    else if (op == SIZE) sw.Write($"{length}\n");
                    else if (op == EMPTY)
                    {

                        if (length == 0) sw.Write("1\n");
                        else sw.Write("0\n");
                    }
                    else if (op == FRONT)
                    {

                        if (length == 0) sw.Write(E);
                        else sw.Write($"{deck[head]}\n");
                    }
                    else
                    {

                        if (length == 0) sw.Write(E);
                        else sw.Write($"{deck[tail]}\n");
                    }
                }

                sr.Close();
                sw.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
                deck = new int[n << 1];

                head = n;
                tail = n - 1;
                length = 0;
            }

            int ReadOp()
            {

                // Rabin - Karp
                int ret = 0;
                int c;
                while ((c = sr.Read()) != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 31 + c - 'a';
                }

                return ret;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
