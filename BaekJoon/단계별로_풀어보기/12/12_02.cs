using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 8
이름 : 배성훈
내용 : 인사성 밝은 곰곰이
    문제번호 : 25192번

    주어진 상황에서 일부 문자열을 제외한 서로 다른 문자열을 찾는 문제
*/

namespace BaekJoon._12
{
    internal class _12_02
    {

        static void Main2(string[] args)
        {

            int length = int.Parse(Console.ReadLine());
            int result = 0;

            HashSet<string> ids = new HashSet<string>();

            for (int i = 0; i < length; i++)
            {

                string temp = Console.ReadLine();

                if (temp == "ENTER")
                {

                    result += ids.Count;
                    ids.Clear();
                    continue;
                }

                ids.Add(temp);
            }

            result += ids.Count;
            Console.WriteLine(result);
        }
    }
}
