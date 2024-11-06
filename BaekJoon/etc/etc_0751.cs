using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 3
이름 : 배성훈
내용 : 8진수 2진수
    문제번호 : 1212번
*/

namespace BaekJoon.etc
{
    internal class etc_0751
    {

        static void Main751(string[] args)
        {

            string ZERO = "000";
            string ONE = "001";
            string TWO = "010";
            string THREE = "011";
            string FOUR = "100";
            string FIVE = "101";
            string SIX = "110";
            string SEVEN = "111";

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 16);

            string str = sr.ReadLine();
            WriteFirst(str[0]);
            for (int i = 1; i < str.Length; i++)
            {

                Write(str[i]);
            }

            sr.Close();
            sw.Close();

            void WriteFirst(char _n)
            {

                if (_n == '0') sw.Write('0');
                else if (_n == '1') sw.Write('1');
                else if (_n == '2') sw.Write("10");
                else if (_n == '3') sw.Write("11");
                else if (_n == '4') sw.Write("100");
                else if (_n == '5') sw.Write("101");
                else if (_n == '6') sw.Write("110");
                else if (_n == '7') sw.Write("111");
            }

            void Write(char _n)
            {

                if (_n == '0') sw.Write(ZERO);
                else if (_n == '1') sw.Write(ONE);
                else if (_n == '2') sw.Write(TWO);
                else if (_n == '3') sw.Write(THREE);
                else if (_n == '4') sw.Write(FOUR);
                else if (_n == '5') sw.Write(FIVE);
                else if (_n == '6') sw.Write(SIX);
                else if (_n == '7') sw.Write(SEVEN);
            }
        }
    }
}
