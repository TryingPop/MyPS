using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 29
이름 : 배성훈
내용 : 스위치 켜고 끄기
    문제번호 : 1244번

    출력 형식만 조심하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0131
    {

        static void Main131(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int bulbLen = ReadInt(sr);

            bool[] bulb = new bool[bulbLen];

            for (int i = 0; i < bulbLen; i++)
            {

                int chk = ReadInt(sr);
                bulb[i] = chk == 1;
            }

            int len = ReadInt(sr);

            for (int i = 0; i < len; i++)
            {

                int rule = ReadInt(sr);

                if (rule == 1)
                {

                    // i 배수를 상태 변경
                    int mul = ReadInt(sr);

                    for (int j = mul - 1; j < bulbLen; j+= mul)
                    {

                        bulb[j] = !bulb[j];
                    }
                }
                else
                {

                    // 중심부터 좌우를 비교하며 같으면 상태 변경
                    int chk = ReadInt(sr) - 1;
                    bulb[chk] = !bulb[chk];

                    int add = 1;
                    while (true)
                    {

                        if (chk - add < 0 || chk + add >= bulbLen) break;

                        if (bulb[chk - add] == bulb[chk + add])
                        {

                            bulb[chk - add] = !bulb[chk - add];
                            bulb[chk + add] = !bulb[chk + add];
                            add++;
                        }
                        else break;
                    }
                }
            }

            sr.Close();

            StreamWriter sw = new(Console.OpenStandardOutput());
            for (int i = 0; i < bulbLen; i++)
            {

                sw.Write(bulb[i] ? '1' : '0');
                if (((i + 1) % 20) == 0) sw.Write('\n');
                else sw.Write(' ');
            }
            sw.Close();
        }


        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
