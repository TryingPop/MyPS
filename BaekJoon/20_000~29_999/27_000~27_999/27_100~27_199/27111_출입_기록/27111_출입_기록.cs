using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 2
이름 : 배성훈
내용 : 출입 기록
    문제번호 : 27111번

    구현, 시뮬레이션 문제다
    해시셋을 이용해 구현했다

    이후 번호 범위를 보니 bool 배열을 이용해도 될거 같다
*/

namespace BaekJoon.etc
{
    internal class etc_1015
    {

        static void Main1015(string[] args)
        {

            StreamReader sr;
            int n;
            HashSet<int> ent;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    int num = ReadInt();
                    int op = ReadInt();

                    if (op == 0)
                    {

                        if (ent.Contains(num)) ent.Remove(num);
                        else ret++;
                    }
                    else 
                    {

                        if (ent.Contains(num)) ret++;
                        else ent.Add(num); 
                    }
                }

                sr.Close();

                ret += ent.Count;
                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                ent = new(n);
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret= ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
