using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 9
이름 : 배성훈
내용 : 비슷한 단어
    문제번호 : 2607번

    구현, 문자열 문제다.
    길이가 같은 경우 하나 바꾸는데 구현을 잘못해 여러 번 틀렸다.
    아이디어는 단순하다. 순서는 중요하지 않다.
    1개를 빼고 추가해서 만들 수 있는지 확인하고, 
    이외의 경우에 한해 1개를 바꿔서 만들 수 있는지 확인했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1323
    {

        static void Main1323(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = int.Parse(sr.ReadLine());

            int ret = 0;
            string dst, src;
            int[] dArr = new int[255], sArr = new int[255];
            dst = sr.ReadLine();
            FillArr(dst, dArr);

            for (int i = 1; i < n; i++)
            {

                src = sr.ReadLine();
                FillArr(src, sArr);

                if (Chk()) ret++;
            }

            Console.Write(ret);
            bool Chk()
            {

                int sub = 0;
                int one = 0;
                for (int i = 'A'; i <= 'Z'; i++)
                {

                    int chk = sArr[i] - dArr[i];
                    if (chk == 0) continue;
                    sub += Math.Abs(chk);
                    if (chk == 1) one++;
                }

                if (sub <= 1) return true;
                else if (sub == 2 && one == 1) return true;
                return false;
            }

            void FillArr(string _str, int[] _arr)
            {

                for (int i = 'A'; i <= 'Z'; i++)
                {

                    _arr[i] = 0;
                }

                for (int i = 0; i < _str.Length; i++)
                {

                    _arr[_str[i]]++;
                }
            }
        }
    }
}
