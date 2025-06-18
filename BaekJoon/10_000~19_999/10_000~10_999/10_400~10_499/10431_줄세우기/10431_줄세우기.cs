using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 25
이름 : 배성훈
내용 : 줄세우기
    문제번호 : 10431번

    구현, 정렬, 시뮬레이션 문제다
    조건대로 시뮬레이션 돌렸다
*/

namespace BaekJoon.etc
{
    internal class etc_1080
    {

        static void Main1080(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int[] line;
            int T, ret;

            Solve();
            void Solve()
            {

                Init();

                T = ReadInt();

                for (int t = 1; t <= T; t++)
                {

                    GetRet();
                    sw.Write($"{t} {ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                ReadInt();
                ret = 0;
                for (int i = 0; i < 20; i++)
                {

                    line[i] = ReadInt();
                    int myPos = i;
                    for (int j = 0; j < i; j++)
                    {

                        if (line[j] <= line[i]) continue;
                        myPos = j;
                        break;
                    }

                    if (i == myPos) continue;
                    int temp = line[i];
                    for (int j = i; j > myPos; j--)
                    {

                        line[j] = line[j - 1];
                        ret++;
                    }

                    line[myPos] = temp;
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                line = new int[20];
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
