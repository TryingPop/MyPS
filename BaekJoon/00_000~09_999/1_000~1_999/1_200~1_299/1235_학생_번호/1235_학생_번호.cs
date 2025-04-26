using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 27
이름 : 배성훈
내용 : 학생 번호
    문제내용 : 1235번

    구현, 문자열 문제다
    처음에 문자열 길이가 7로 고정된줄 알고 7로 시작했다가 1번 틀렸다
    이후에는 이상없이 통과했다

    아이디어는 다음과 같다
    해시셋으로 중복 문자열을 판별했다
    문자열 자르는건 Substring 내장 메서드를 이용했다
*/

namespace BaekJoon.etc
{
    internal class etc_0363
    {

        static void Main363(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));

            int n = int.Parse(sr.ReadLine());
            HashSet<string> s = new(n);

            string[] ids = new string[n];

            for (int i = 0; i < n; i++)
            {

                ids[i] = sr.ReadLine();
            }

            sr.Close();

            int ret = ids[0].Length;
            for (int i = 1; i < ids[0].Length; i++)
            {

                for (int j = 0; j < n; j++)
                {

                    s.Add(ids[j].Substring(i));
                }

                if (s.Count == n) ret = ids[0].Length - i;
                else break;

                s.Clear();
            }

            Console.WriteLine(ret);
        }
    }
}
