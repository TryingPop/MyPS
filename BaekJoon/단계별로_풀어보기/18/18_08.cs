using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 21
이름 : 배성훈
내용 : 단어 정렬
    문제번호 : 1181번

    아래가 메모리가 더 적게 든다
*/

namespace BaekJoon._18
{
    internal class _18_08
    {

        static void Main8(string[] args)
        {


            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            int len = int.Parse(sr.ReadLine());

#if false
            HashSet<string> set = new HashSet<string>(len);
            for (int i = 0; i < len; i++)
            {

                set.Add(sr.ReadLine());
            }
            sr.Close();

            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());

            foreach (var item in set.OrderBy(x => x.Length).ThenBy(x => x))
            {

                sw.WriteLine(item);
            }
#else
            string[] inputs = new string[len];

            for (int i = 0; i < len; i++)
            {

                inputs[i] = sr.ReadLine();
            }
            sr.Close();

            Array.Sort(inputs, (string x, string y) =>
            {

                int res1 = x.Length.CompareTo(y.Length);
                int res2 = x.CompareTo(y);

                return res1 == 0 ? res2 : res1;
            });


            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            string pre = "";

            for (int i = 0; i < inputs.Length; i++)
            {

                if (pre != inputs[i])
                {

                    sw.WriteLine(inputs[i]);
                    pre = inputs[i];
                }
            }

#endif
            sw.Close();
        }
    }
}
