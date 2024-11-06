using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 23
이름 : 배성훈
내용 : Keyboardd
    문제번호 : 21347번

    구현 문자열 문제다
    문제 예제를 잘못 이해해 한 번 틀렸다;
    처음에는 모두 다른 경우 문장처럼 보이는 its yock를 반환해야하는 줄 알았다
    ... 그런데 알파벳을 비교하니 so ticky 를 재배치하여 얻은 문자였다
    그래서 그냥 찾는 순서대로 제출하니 이상없이 통과한다;
*/

namespace BaekJoon.etc
{
    internal class etc_1070
    {

        static void Main1070(string[] args)
        {

            StreamReader sr;
            string str1, str2;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                bool[] use = new bool[128];
                int idx1 = 0;
                int idx2 = 0;
                while(idx1 < str1.Length && idx2 < str2.Length)
                {

                    if (str1[idx1] == str2[idx2])
                    {

                        idx1++;
                        idx2++;
                        continue;
                    }

                    if (!use[str2[idx2]]) 
                    { 
                        
                        use[str2[idx2]] = true;
                        sw.Write(str2[idx2]);
                    }
                    idx2++;
                }

                while (idx2 < str2.Length) 
                {

                    if (!use[str2[idx2]])
                    {

                        use[str2[idx2]] = true;
                        sw.Write(str2[idx2]);
                    }
                    idx2++;
                }

                sw.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                str1 = sr.ReadLine();
                str2 = sr.ReadLine();

                sr.Close();
            }
        }
    }
}
