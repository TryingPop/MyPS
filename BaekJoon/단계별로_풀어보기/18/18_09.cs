using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 21
이름 : 배성훈
내용 : 나이순 정렬
    문제번호 : 10814번
*/

namespace BaekJoon._18
{
    internal class _18_09
    {

        static void Main9(string[] args)
        {


            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            int len = int.Parse(sr.ReadLine());

            (int age, string name)[] inputs = new (int age, string name)[len];

            for (int i = 0; i < len; i++)
            {

                int age = 0;
                int c;
                while((c = sr.Read())!= ' ')
                {

                    age = 10 * age + c - '0';
                }

                inputs[i] = (age, sr.ReadLine());
            }

            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());

            foreach(var s in inputs.OrderBy(x => x.age))
            {

                sw.WriteLine($"{s.age} {s.name}");
            }
            sw.Close();
        }
    }
}
