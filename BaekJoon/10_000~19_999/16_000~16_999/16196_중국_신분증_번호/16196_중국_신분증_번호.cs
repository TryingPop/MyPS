using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 6
이름 : 배성훈
내용 : 중국 신분증 번호
    문제번호 : 16196번

    신분증 유효 검사 문제이다
*/

namespace BaekJoon.etc
{
    internal class etc_0155
    {

        static void Main155(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            string idCard = sr.ReadLine();
            
            int yyyy = SubStringToInt(idCard, 6, 9);
            int mm = SubStringToInt(idCard, 10, 11);
            int dd = SubStringToInt(idCard, 12, 13);
            
            if (!ChkDay(yyyy, mm, dd))
            {

                Console.Write('I');
                return;
            }

            if (!ChkNums(idCard))
            {

                Console.Write('I');
                return;
            }

            int nnn = SubStringToInt(idCard, 14, 16);
            char ret = GetOrder(nnn);
            if (ret == 'I')
            {

                Console.Write('I');
                return;
            }

            int region = SubStringToInt(idCard, 0, 5);
            int len = ReadInt(sr);
            bool regionPass = false;

            for (int i = 0; i < len; i++)
            {

                if (region == ReadInt(sr))
                {

                    regionPass = true;
                    break;
                }
            }

            if (regionPass)
            {

                Console.Write(ret);
            }
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

        static bool ChkNums(string _s)
        {

            int ret = 0;
            for (int i = 0; i < 17; i++)
            {

                ret += _s[i] - '0';
                ret *= 2;
                ret %= 11;
            }

            ret = 12 - ret;
            if (ret >= 11) ret -= 11;

            if (ret == 10 && _s[17] == 'X') return true;
            else if (_s[17] - '0' == ret) return true;
            else return false;
        }

        static int SubStringToInt(string _s, int _start, int _end)
        {

            int ret = 0;
            int idx = _start;
            while(idx <= _end)
            {

                ret = ret * 10 + _s[idx++] - '0';
            }

            return ret;
        }

        static char GetOrder(int nnn)
        {

            if (nnn == 0) return 'I';
            else if ((nnn & 1) == 1) return 'M';
            else return 'F';
        }

        static bool ChkDay(int yyyy, int mm, int dd)
        {

            if (yyyy >= 2012 || yyyy <= 1899 || mm < 1 || mm > 12) return false;
            int[] mTod = { 31, yyyy % 400 == 0 ? 29 : yyyy % 100 == 0 ? 28 : yyyy % 4 == 0 ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (dd < 1 || dd > mTod[mm - 1]) return false;

            return true;
        }
    }
}
