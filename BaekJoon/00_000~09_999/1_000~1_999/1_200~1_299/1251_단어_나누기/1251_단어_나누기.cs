using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 29
이름 : 배성훈
내용 : 단어 나누기
    문제번호 : 1251번

    구현, 문자열, 브루트포스 알고리즘 문제다
    초기값 설정을 잘못해서 한 번 틀렸다
        abcdefg 인 경우 문제 조건대로 어떻게 변환해도 abcdefg가 나올 수 없다!
        그래서 초기값을 입력값으로 하면 안된다!

    그래서 무조건 큰 'z'로만 도배했다
    이후에는 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0382
    {

        static void Main382(string[] args)
        {

            char[] str = Console.ReadLine().ToCharArray();

            char[] calc = new char[str.Length];
            char[] ret = new char[str.Length];

            for (int i = 0; i < str.Length; i++)
            {

                ret[i] = 'z';
            }

            for (int i = 1; i < str.Length - 1; i++)
            {

                for (int j = i + 1; j < str.Length; j++)
                {

                    Reverse(0, i);
                    Reverse(i, j);
                    Reverse(j, str.Length);

                    int comp = Comp(calc, ret);
                    if (comp < 0) Copy(calc, ret);
                }
            }

            Console.WriteLine(new string(ret));

            void Reverse(int _startIdx, int _endIdx)
            {

                // 순서 바꾼 것을 calc에 넣는다
                int l = _startIdx;
                int r = _endIdx - 1;

                while(l <= r)
                {

                    calc[l] = str[r];
                    calc[r] = str[l];
                    l++;
                    r--;
                }
            }

            void Copy(char[] _copy, char[] _dest)
            {

                // 덮어쓰기
                for (int i = 0; i < _copy.Length; i++)
                {

                    _dest[i] = _copy[i];
                }
            }

            int Comp(char[] _f, char[] _b)
            {

                // char 배열 비교
                for (int i = 0; i < _f.Length; i++)
                {

                    int cur = _f[i] - _b[i];
                    if (cur == 0) continue;
                    if (cur < 0) return -1;
                    return 1;
                }

                return 0;
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BackJ
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());

            char[] charArray = sr.ReadLine().ToArray();
            char[] temp = new char[charArray.Length];
            Array.Copy(charArray, temp, charArray.Length);
            int N = charArray.Length;

            List<string> lst = new List<string>();

            for (int a = 1; a <= N - 2; a++)
            {
                for (int b = 1; b <= N - a - 1; b++)
                {
                    Array.Reverse(temp, 0, a);
                    Array.Reverse(temp, a, b);
                    Array.Reverse(temp, a + b, N - a - b);
                    lst.Add(new string(temp));
                    Array.Copy(charArray, temp, charArray.Length);
                }
            }

            lst.Sort();
            sw.WriteLine(lst.First());

            sw.Flush();
            sw.Close();
            sr.Close();
        }
    }
}

#endif
}
