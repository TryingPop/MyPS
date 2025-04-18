using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 25
이름 : 배성훈
내용 : 디지털 티비
    문제번호 : 2816번

    구현, 해 구성하기 문제다
    포인터를 옮기며 KBS1을 찾으면 1번으로 가게 하는 연산과
    포인터를 옮기며 KBS2를 찾으면 2번으로 가게 하는 연산으로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_1079
    {


        static void Main1079(string[] args)
        {

            string KBS1 = "KBS1";
            string KBS2 = "KBS2";

            string[] str;
            int n;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int to = Down(KBS1);
                Swap(0, to);
                to = Down(KBS2);
                Swap(1, to);

                sw.Close();

                void Swap(int _from, int _to)
                {

                    string temp = str[_to];
                    for (int i = _to; i > _from; i--)
                    {

                        str[i] = str[i - 1];
                        sw.Write('4');
                    }

                    str[_from] = temp;
                }

                int Down(string _find)
                {

                    for (int i = 0; i < n; i++)
                    {

                        if (str[i] == _find) return i;
                        sw.Write('1');
                    }

                    return -1;
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());

                str = new string[n];
                for (int i = 0; i < n; i++)
                {

                    str[i] = sr.ReadLine();
                }

                sr.Close();
            }
        }
    }
}
