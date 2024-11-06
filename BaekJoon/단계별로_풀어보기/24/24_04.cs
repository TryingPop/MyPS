using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 22
이름 : 배성훈
내용 : 칸토어 집합
    문제번호 : 4779번
*/

namespace BaekJoon._24
{
    internal class _24_04
    {

        static void Main4(string[] args)
        {

            string str = "";
            int input;

            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());

            while ((str = Console.ReadLine()) != null) 
            {
                input = int.Parse(str);

                CantorSet(input, sw);
                sw.WriteLine();
            }
            sw.Close();
        }

        static void CantorSet(int n ,StreamWriter sw, bool empty = false)
        {

            if (n <= 0)
            {

                sw.Write(empty ? ' ' : '-');
                return;
            }
            
            CantorSet(n - 1, sw, empty);
            CantorSet(n - 1, sw, true);
            CantorSet(n - 1, sw, empty);
        }
    }
}
