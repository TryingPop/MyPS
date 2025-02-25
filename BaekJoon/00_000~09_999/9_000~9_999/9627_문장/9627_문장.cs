using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 25
이름 : 배성훈
내용 : 문장
    문제번호 : 9627번

    문자열, 브루트포스 문제다.
    편지 문자와 숫자 문자를 합쳤을 때 해당 숫자가 되는
    가장 작은 숫자를 찾는 문제다.

    0 ~ 20이하는 특수 단어가 존재한다.
    그리고 30, 40, ..., 90이랑 100, 200, 300, ..., 900이 특수 단어가 된다.
    이외의 숫자는 앞의 숫자들로 합쳐서 만들어지는 문자다.

    그래서 채워진 숫자들로 조건대ㅗㄹ 나머지 숫자들을 만들어가며 길이가 가능한지 확인했다.
    불가능한 경우는 없다기에 없을 때 코드를 만들지 않았다.
    그리고 찾은 숫자로 해당 문자를 만들어 출력했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1361
    {

        static void Main1361(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());
            string[] input = new string[n];

            string[] numEng = new string[1_000];

            numEng[0] = string.Empty;
            numEng[1] = "one";
            numEng[2] = "two";
            numEng[3] = "three";
            numEng[4] = "four";
            numEng[5] = "five";
            numEng[6] = "six";
            numEng[7] = "seven";
            numEng[8] = "eight";
            numEng[9] = "nine";
            numEng[10] = "ten";
            numEng[11] = "eleven";
            numEng[12] = "twelve";
            numEng[13] = "thirteen";
            numEng[14] = "fourteen";
            numEng[15] = "fifteen";
            numEng[16] = "sixteen";
            numEng[17] = "seventeen";
            numEng[18] = "eighteen";
            numEng[19] = "nineteen";
            numEng[20] = "twenty";
            numEng[30] = "thirty";
            numEng[40] = "forty";
            numEng[50] = "fifty";
            numEng[60] = "sixty";
            numEng[70] = "seventy";
            numEng[80] = "eighty";
            numEng[90] = "ninety";
            numEng[100] = "onehundred";
            numEng[200] = "twohundred";
            numEng[300] = "threehundred";
            numEng[400] = "fourhundred";
            numEng[500] = "fivehundred";
            numEng[600] = "sixhundred";
            numEng[700] = "sevenhundred";
            numEng[800] = "eighthundred";
            numEng[900] = "ninehundred";

            int chk = 0;
            string NONE = "$";
            for (int i = 0; i < n; i++)
            {

                input[i] = sr.ReadLine();
                if (input[i] == NONE) continue;

                chk += input[i].Length;
            }

            int ret = 0;
            for (int i = 1; i < 1_000; i++)
            {

                int add = 0;

                if (numEng[i] == null)
                {

                    int curNum = i;
                    int h = (curNum / 100) * 100;
                    curNum = curNum - h;

                    add = numEng[h].Length;

                    if (curNum <= 20)
                        add += numEng[curNum].Length;
                    else
                    {

                        int t = (curNum / 10) * 10;
                        curNum = curNum - t;
                        add += numEng[t].Length + numEng[curNum].Length;
                    }
                }
                else
                    add = numEng[i].Length;

                if (chk + add == i) 
                { 
                    
                    ret = i;
                    break;
                }
            }

            for (int i = 0; i < n; i++)
            {

                if (input[i] != NONE) sw.Write($"{input[i]} ");
                else
                {

                    int h = (ret / 100) * 100;
                    ret -= h;

                    sw.Write(numEng[h]);
                    if (ret <= 20)
                        sw.Write(numEng[ret]);
                    else
                    {

                        int t = (ret / 10) * 10;
                        ret -= t;
                        sw.Write(numEng[t]);
                        sw.Write(numEng[ret]);
                    }

                    sw.Write(' ');
                }
            }
        }
    }
}
