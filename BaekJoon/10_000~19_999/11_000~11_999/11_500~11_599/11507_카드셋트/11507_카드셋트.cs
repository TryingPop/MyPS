using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 4
이름 : 배성훈
내용 : 카드셋트
    문제번호 : 11507번

    문자열, 파싱 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1746
    {

        static void Main1746(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            bool[][] cards = new bool[4][];
            for (int i = 0; i < 4; i++)
            {

                cards[i] = new bool[14];
            }

            int[] cnt = new int[4] { 13, 13, 13, 13 };

            string s = sr.ReadLine();
            bool flag = false;

            for (int i = 0; i < s.Length; i += 3)
            {

                int idx1 = Type(s[i]);
                int idx2 = (s[i + 1] - '0') * 10 + (s[i + 2] - '0');

                if (cards[idx1][idx2])
                {

                    flag = true;
                    break;
                }

                cards[idx1][idx2] = true;
                cnt[idx1]--;
            }

            if (flag) Console.Write("GRESKA");
            else Console.Write($"{cnt[0]} {cnt[1]} {cnt[2]} {cnt[3]}");

            int Type(char _i)
            {

                switch (_i)
                {

                    case 'P':
                        return 0;

                    case 'K':
                        return 1;

                    case 'H':
                        return 2;

                    case 'T':
                        return 3;

                    default:
                        return -1;
                }
            }
        }
    }
}
