using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 27
이름 : 배성훈
내용 : 진짜 메시지
    문제번호 : 9324번

    힐링용 문제다
    해당 문자가 3번 반복하는 순간마다 해당 문자가 1번 더 나오는지 확인하는 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0105
    {

        static void Main105(string[] args)
        {

            string OK = "OK";
            string FAKE = "FAKE";
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new(Console.OpenStandardOutput());

            int[] alphabet = new int[26];

            int testCase = int.Parse(sr.ReadLine());

            while(testCase-- > 0)
            {

                string str = sr.ReadLine();

                bool ret = true;
                for (int i = 0; i < str.Length; i++)
                {

                    int idx = str[i] - 'A';
                    alphabet[idx]++;
                    // 3번 나왔다
                    if (alphabet[idx] == 3)
                    {

                        // 다음이 반복문자인지 확인
                        if (i + 1 < str.Length && str[i + 1] == str[i])
                        {

                            // 반복문자면 초기화하고 건너뛴다
                            alphabet[idx] = 0;
                            i++;
                        }
                        else 
                        { 
                            
                            // 반복문자가 아니면 거짓이고 탈출
                            ret = false;
                            break;
                        }
                    }
                }

                if (ret) sw.WriteLine(OK);
                else sw.WriteLine(FAKE);

                for (int i = 0; i < 26; i++)
                {

                    alphabet[i] = 0;
                }
            }

            sr.Close();
            sw.Close();
        }
    }
}
