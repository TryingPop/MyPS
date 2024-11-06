using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 25
이름 : 배성훈
내용 : 탭 vs 공백
    문제번호 : 16961번

    구현 문제다
    누적합으로 쉽게 찾을 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0341
    {

        static void Main341(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n = ReadInt();

            int ret1 = 0;
            int ret2 = 0;
            int ret3 = 0;
            int ret4 = 0;
            int ret5 = 0;

            (int t, int e)[] day = new (int t, int e)[368];
            for (int i = 0; i < n; i++)
            {

                int t = ReadInt();
                int s = ReadInt();
                int e = ReadInt();

                if (t == 'T' - '0')
                {

                    day[s].t++;
                    day[e + 1].t--;
                }
                else
                {

                    day[s].e++;
                    day[e + 1].e--;
                }

                int max = e - s + 1;
                if (ret5 < max) ret5 = max;
            }

            sr.Close();


            int cntT = 0;
            int cntE = 0;
            for (int i = 0; i < day.Length; i++)
            {

                cntT += day[i].t;
                cntE += day[i].e;

                int cur = cntT + cntE;

                if (cur != 0) 
                { 
                    
                    ret1++; 
                    if (ret2 < cur) ret2 = cur;
                }

                if (cntT != 0 && cntT == cntE) 
                { 
                    
                    ret3++;
                    if (ret4 < cur) ret4 = cur;
                }
            }

            Console.Write($"{ret1}\n{ret2}\n{ret3}\n{ret4}\n{ret5}");

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
