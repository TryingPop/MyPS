using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 1
이름 : 배성훈
내용 : 만능 오라클
    문제번호 : 7656번

    문자열, 파싱 문제다
    Split을 이용해 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_1010
    {

        static void Main1010(string[] args)
        {

            StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            string PREV = "Forty-two is";
            string END = ".\n";
            string[] str = Console.ReadLine().Split("What is");

            for (int i = 1; i < str.Length; i++)
            {

                int len = -1;
                for (int j = 0; j < str[i].Length; j++)
                {

                    if (str[i][j] == '.') break;

                    else if (str[i][j] == '?')
                    {

                        len = j;
                        break;
                    }
                }

                if (len == -1) continue;

                sw.Write(PREV);
                sw.Write(str[i].Substring(0, len));
                sw.Write(".\n");
            }

            sw.Close();
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace no7656try1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 원하는 버퍼의 사이즈를 매개변수로 입력할 것
            int bufferSize = 2000;
            StreamReader reader = new StreamReader(Console.OpenStandardInput(bufferSize));

            // 입력을 받을때마다 호출합니다. 해당 함수의 리턴값은 String입니다.
            string recvLine = reader.ReadLine();


            // 코드가 종료되기 전에 미리 호출하여 리소스를 회수합니다.
            reader.Close();



            bool isFoundWhat = false;
            string what = "What is";
            int whatIndex = 0;
            StringBuilder result = new StringBuilder();
            StringBuilder line = new StringBuilder();

            for (int index = 0; index < recvLine.Length; index++)
            {
                if (whatIndex > 0)
                {
                    if (recvLine[index] == what[whatIndex])
                    {
                        whatIndex++;
                    }
                    else
                    {
                        whatIndex = 0;
                        // whaWhat도 판단할것. 이것은 아래 코드로 내려가서 체크할 것.
                    }
                    if (whatIndex == 7)
                    {
                        whatIndex = 0;
                        line.Append("Forty-two is");
                        isFoundWhat = true;
                        index++;
                    }
                }
                if (whatIndex == 0)
                {
                    // what의 w 판정
                    if (index == recvLine.Length) break;
                    if ((recvLine[index] == 'W' || recvLine[index] == 'w') && (isFoundWhat == false))
                    {
                        whatIndex++;
                        continue;
                    }


                    if (isFoundWhat)
                    {
                        if (recvLine[index] == '.')
                        {
                            line.Clear();
                            isFoundWhat = false;
                            continue;
                        }
                        if (recvLine[index] == '?')
                        {
                            result.AppendLine($"{line}.");
                            line.Clear();
                            isFoundWhat = false;
                        }
                        else
                        {
                            line.Append(recvLine[index]);
                        }
                    }
                }
            }
            Console.Write(result);

        }
    }
}

#endif
}
