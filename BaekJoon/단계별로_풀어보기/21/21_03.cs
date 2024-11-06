using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 19
이름 : 배성훈
내용 : 회사에 있는 사람
    문제번호 : 7785번
*/

namespace BaekJoon._21
{
    internal class _21_03
    {

        static void Main3(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int len = int.Parse(sr.ReadLine());

            HashSet<string> set = new HashSet<string>();

            // 입력값 조건에 맞게 저장
            for (int i = 0; i < len; i++)
            {

                string[] inputs = sr.ReadLine().Split(' ');

                if (inputs[1] == "enter")
                {

                    set.Add(inputs[0]);
                }
                else
                {

                    set.Remove(inputs[0]);
                }
            }

            sr.Close();

            StringBuilder sb = new StringBuilder();

            /*
            // 다른 사람의 풀이를 보니 foreach문을 이용해서 풀었다
            // 메모리도 적게 들고 시간도 엄청 단축된다!
            List<string> list = set.OrderByDescending(item => item).ToList();

            for (int i = 0; i < list.Count; i++)
            {

                sb.AppendLine(list[i]);
            }
            */

            // 조건에 맞게 문자열 출력
            foreach (string name in set.OrderByDescending(item => item))
            {

                sb.AppendLine(name);
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
                sw.WriteLine(sb);

        }
    }
}
