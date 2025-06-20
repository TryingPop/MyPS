using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 15 
이름 : 배성훈
내용 : 비밀번호 발음하기
    문제번호 : 4659번

    문제 조건에 맞춰 조건문 구현하는 문제이다
*/

namespace BaekJoon.etc
{
    internal class etc_0036
    {

        static void Main36(string[] args)
        {

            string END = "end";
            string ACCEPT = " is acceptable.";
            string REJECT = " is not acceptable.";

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            // 26으로 잡고 싶었으나, 그냥 'z'로 했다
            bool[] vowel = new bool['z' + 1];           // 모음
            bool[] acceptDouble = new bool['z' + 1];    // 연속해서 2번 나오는거 허용하는 문자

            vowel['a'] = true;
            vowel['e'] = true;
            vowel['i'] = true;
            vowel['o'] = true;
            vowel['u'] = true;

            acceptDouble['e'] = true;
            acceptDouble['o'] = true;

            while (true)
            {

                string str = sr.ReadLine();

                if (str == END) break;

                char before = str[0];                   // 첫 문자

                bool chk1 = vowel[before];              // 모음 있는지?
                bool chk2 = true;                       // 모음이나 자음이 연속 3개
                bool chk3 = true;                       // 같은게 2개 연속?

                int conti = 1;

                for (int i = 1; i < str.Length; i++)
                {

                    char cur = str[i];
                    if (!chk1) chk1 = vowel[cur];

                    // 모음 자음 같은지?
                    if (vowel[before] == vowel[cur])
                    {

                        conti++;
                        // 모음 3개, 자음 3개 확인
                        if (conti >= 3)
                        {

                            chk2 = false;
                            break;
                        }
                        // 이전과 같은지 그리고 허용된 중복인지 확인
                        else if (before == cur && !acceptDouble[cur])
                        {
                            
                            chk3 = false;
                            break;
                        }

                    }
                    // 자음 모음 다르다
                    else conti = 1;

                    before = cur;
                }

                // 출력
                sw.Write('<');
                sw.Write(str);
                sw.Write('>');

                if (chk1 && chk2 && chk3) sw.WriteLine(ACCEPT);
                else sw.WriteLine(REJECT);
            }

            sw.Close();
            sr.Close();
        }
    }
}
