using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaekJoon.etc
{
    internal class etc_1324
    {

        static void Main1324(string[] args)
        {

            int[] rom;

            SetRom();

            Input();

            void Input()
            {

                string input = Console.ReadLine();
                int ret = rTa(input);
                input = Console.ReadLine();

                ret += rTa(input);

                Console.WriteLine(ret);
                Console.Write(aTr(ret));
            }

            void SetRom()
            {

                rom = new int['Z' + 1];
                rom['I'] = 1;
                rom['V'] = 5;
                rom['X'] = 10;
                rom['L'] = 50;
                rom['C'] = 100;
                rom['D'] = 500;
                rom['M'] = 1000;
            }

            string aTr(int _num)
            {

                return null;
            }

            int rTa(string _str)
            {

                int num = 0;
                return -1;
            }
        }
    }
}
