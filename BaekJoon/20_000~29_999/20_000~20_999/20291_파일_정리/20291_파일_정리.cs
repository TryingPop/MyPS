using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 27
이름 : 배성훈
내용 : 파일 정리
    문제번호 : 20291번

    확장자 앞부분은 그냥 read로 짤라버린다
    그리고 확장자 정렬은 Linq를 이용해서 했다
*/

namespace BaekJoon.etc
{
    internal class etc_0109
    {

        static void Main109(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int len = int.Parse(sr.ReadLine());

            Dictionary<string, int> dic = new Dictionary<string, int>();

            for (int i = 0; i < len; i++)
            {

                // . 앞부분은 무시!
                int c;
                while((c = sr.Read()) != '.') { }

                string str = sr.ReadLine();

                if (dic.ContainsKey(str)) dic[str]++;
                else dic[str] = 1;
            }

            sr.Close();

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                foreach (string item in dic.Keys.OrderBy(x => x))
                {

                    sw.Write($"{item} {dic[item]}\n");

                }
            }
        }
    }
}
