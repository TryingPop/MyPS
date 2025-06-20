using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 17
이름 : 배성훈
내용 : Lunacy
    문제번호 : 4714번

    수학, 구현, 사칙연산 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_1062
    {

        static void Main1062(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            double mul = 0.167;

            double weight;
            while ((weight = double.Parse(sr.ReadLine())) != -1.0)
            {
                
                sw.Write($"Objects weighing {weight:0.00} on Earth will weigh {mul * weight:0.00} on the moon.\n");
            }

            sr.Close();
            sw.Close();
        }
    }
}
