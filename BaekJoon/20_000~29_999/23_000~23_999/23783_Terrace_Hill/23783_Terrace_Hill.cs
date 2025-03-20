using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 20
이름 : 배성훈
내용 : Terrace Hill
    문제번호 : 23783번

    스택 문제다.
    이전보다 높은 값을 스택에 저장한다.
    이하 값이 있는 경우, 다리를 놓을 수 있는지 확인하고 뺀다.
*/

namespace BaekJoon.etc
{
    internal class etc_1424
    {

        static void Main1424(string[] args)
        {

            int n, len;
            int[] arr, stk;

            Input();

            GetRet();

            void GetRet()
            {

                stk = new int[n];
                len = 0;

                long ret = 0;
                for (int i = 0; i < n; i++)
                {

                    while (len > 0)
                    {

                        if (arr[stk[len - 1]] > arr[i]) break;
                        else
                        {

                            len--;
                            if (arr[stk[len]] == arr[i])
                                ret += i - stk[len] - 1;
                        }
                    }

                    stk[len++] = i;
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }


                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;

                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }
}
