using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 3
이름 : 배성훈
내용 : 특별한 케이크 (easy)
    문제번호 : 31672번

    구현, 자료구조, 시뮬레이션 문제다
    조건대로 논리를 비교하며 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0435
    {

        static void Main435(string[] args)
        {

            string SWI = "swi";
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            sr.ReadLine();

            int n = ReadInt();
            int[][] talk = new int[n + 1][];
            for (int i = 1; i <= n; i++)
            {

                talk[i] = new int[n + 1];
                int len = ReadInt();

                for (int j = 0; j < len; j++)
                {

                    int c = ReadInt();
                    talk[i][c] = 1;
                }

                talk[i][0] = ReadInt();
            }

            sr.Close();

            bool[] ret = new bool[n + 1];
            int[] chk = new int[n + 1];

            for (int i = 1; i <= n; i++)
            {

                // i : 거짓말 쟁이
                if (talk[i][0] == 0)
                {

                    // 이 중에 없다고 했으므로
                    // 거짓말은 이 중에 있어야한다!
                    // 자신이 포함되어야 논리 성립!
                    if (talk[i][i] != 1) continue; 
                }
                else
                {

                    // 이 중에 있다고 했으므로
                    // 거짓말은 이 중에 없어야한다
                    // 자신이 포함 안되어야 논리 성립!
                    if (talk[i][i] == 1) continue;
                }

                bool find = true;
                for (int j = 1; j <= n; j++)
                {

                    if (i == j) continue;
                    
                    if (talk[j][0] == 0)
                    {

                        // 거짓말 쟁이 아니라고 했는데
                        // 거짓말 쟁이가 있으면 모순
                        if (talk[j][i] == 1) 
                        { 
                            
                            find = false;
                            break;
                        }
                    }
                    else
                    {

                        // 거짓말 쟁이 있다고 햇는데
                        // 진짜 거짓말 쟁이 없으면 모순
                        if (talk[j][i] != 1)
                        {

                            find = false;
                            break;
                        }
                    }
                }

                if (find) ret[i] = true;

                for (int j = 1; j <= n; j++)
                {

                    chk[j] = 0;
                }
            }

            bool isSwi = true;
            for (int i = 1; i <= n; i++)
            {

                if (ret[i]) 
                { 
                    
                    isSwi = false;
                    Console.Write($"{i} ");
                }
            }

            if (isSwi) Console.Write(SWI);

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
