using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 10
이름 : 배성훈
내용 : 단축키 지정
    문제번호 : 1283번

    if문 안의 조건을 잘못해서 여러 번 틀렸다;
    문제 조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0173
    {

        static void Main173(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            bool[] usedKey = new bool[26];

            int test = int.Parse(sr.ReadLine());


            while(test-- > 0)
            {

                string str = sr.ReadLine();

                string[] chk = str.Split(' ');
                int idx = 0;
                bool find = false;

                // 먼저 문자 맨 앞의 글자 사용가능한지 확인
                for (int i = 0; i < chk.Length; i++)
                {

                    char c = chk[i][0];
                    if (c <= 'Z' && c >= 'A') c = (char)(c - 'A' + 'a');


                    if (usedKey[c - 'a']) 
                    {

                        idx += chk[i].Length;
                        continue; 
                    }

                    usedKey[c - 'a'] = true;

                    idx += i;
                    find = true;
                    break;
                }

                if (!find)
                {

                    // 문자 맨 앞을 다 사용한 경우 중앙 문자 검색
                    idx = -1;
                    for (int i = 0; i < str.Length; i++)
                    {

                        char c = str[i];
                        if (c >= 'A' && c <= 'Z') c = (char)(c - 'A' + 'a');

                        if (c >= 'a' && c <= 'z')
                        {

                            if (usedKey[c - 'a']) continue;
                            usedKey[c - 'a'] = true;

                            find = true;
                            idx = i;
                            break;
                        }
                    }
                }

                for (int i = 0; i < str.Length; i++)
                {

                    if (idx == i) sw.Write('[');

                    sw.Write(str[i]);
                    if (idx == i) sw.Write(']');
                }

                if (test != 0) sw.Write('\n');
            }

            sw.Close();
            sr.Close();
        }
    }
}
