using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 17
이름 : 배성훈
내용 : 트ㅏㅊ;
    문제번호 : 4378번

    구현 문제다.
    조건대로 왼쪽으로 1칸씩 이동하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1941
    {

        static void Main1941(string[] args)
        {


            char[] change = new char[255];
            for (char i = (char)0; i < change.Length; i++)
            {

                change[i] = i;
            }

            change['1'] = '`';
            change['2'] = '1';
            change['3'] = '2';
            change['4'] = '3';
            change['5'] = '4';
            change['6'] = '5';
            change['7'] = '6';
            change['8'] = '7';
            change['9'] = '8';
            change['0'] = '9';
            change['-'] = '0';
            change['='] = '-';

            change['W'] = 'Q';
            change['E'] = 'W';
            change['R'] = 'E';
            change['T'] = 'R';
            change['Y'] = 'T';
            change['U'] = 'Y';
            change['I'] = 'U';
            change['O'] = 'I';
            change['P'] = 'O';
            change['['] = 'P';
            change[']'] = '[';
            change['\\'] = ']';

            change['S'] = 'A';
            change['D'] = 'S';
            change['F'] = 'D';
            change['G'] = 'F';
            change['H'] = 'G';
            change['J'] = 'H';
            change['K'] = 'J';
            change['L'] = 'K';
            change[';'] = 'L';
            change['\''] = ';';

            change['X'] = 'Z';
            change['C'] = 'X';
            change['V'] = 'C';
            change['B'] = 'V';
            change['N'] = 'B';
            change['M'] = 'N';
            change[','] = 'M';
            change['.'] = ',';
            change['/'] = '.';

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            string str = null;
            while ((str = sr.ReadLine())!= null)
            {

                for (int i = 0; i < str.Length; i++)
                {

                    sw.Write(change[str[i]]);
                }
                sw.Write('\n');

                sw.Flush();
            }
        }
    }
}
