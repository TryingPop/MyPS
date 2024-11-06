using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 28
이름 : 배성훈
내용 : 대통령 선거
    문제번호 : 9547번

    구현 문제다
    문제 조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0373
    {

        static void Main373(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt();
            int[,] user = new int[101, 101];
            int[] vote = new int[101];

            while(test-- > 0)
            {

                int c = ReadInt();
                int v = ReadInt();

                for (int i = 1; i <= c; i++)
                {

                    vote[i] = 0;
                }

                for (int i = 1; i <= v; i++)
                {

                    for (int j = 1; j <= c; j++)
                    {

                        user[i, j] = ReadInt();
                    }
                }

                int fir = 0;
                int sec = 1;

                int max = 0;
                for (int i = 1; i <= v; i++)
                {

                    int idx = user[i, 1];
                    vote[idx]++;

                    if (max < vote[idx]) max = vote[idx];
                }

                if (max >= (v + 1) / 2)
                {

                    for (int i = 1; i <= c; i++)
                    {

                        if (vote[i] == max)
                        {

                            fir = i;
                            break;
                        }
                    }

                    sw.Write($"{fir} {sec}\n");
                    continue;
                }

                sec++;

                int c1 = -1;

                for (int i = 1; i <= c; i++)
                {

                    if (vote[i] == max)
                    {

                        c1 = i;
                        vote[i] = 0;
                        break;
                    }
                }

                int c2 = -1;
                max = 0;
                for (int i = 1; i <= c; i++)
                {

                    if (max < vote[i])
                    {

                        max = vote[i];
                        c2 = i;
                    }

                    vote[i] = 0;
                }

                max = 0;
                for (int i = 1; i <= v; i++)
                {

                    for (int j = 1; j <= c; j++)
                    {

                        if (user[i, j] == c1)
                        {

                            max++;
                            break;
                        }
                        else if (user[i, j] == c2)
                        { 
                            
                            max--;
                            break;
                        }
                    }
                }

                if (max > 0) fir = c1;
                else fir = c2;

                sw.Write($"{fir} {sec}\n");
            }

            sr.Close();
            sw.Close();

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
