using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 29
이름 : 배성훈
내용 : 화학식량
    문제번호 : 2257번

    스택, 문자열 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1302
    {

        static void Main1302(string[] args)
        {

            string input;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                input = sr.ReadLine();
            }

            void GetRet()
            {

                int[] stk = new int[input.Length];
                int len = 0;
                int prev = 0, cur = 0;

                for (int i = 0; i < input.Length; i++)
                {

                    if (input[i] == '(')
                    {

                        cur += prev;
                        Push(cur);
                        prev = 0;
                        cur = 0;
                    }
                    else if (input[i] == ')')
                    {

                        cur += prev;
                        prev = cur;
                        cur = Pop();
                    }
                    else
                    {

                        int val = ReadChar(input[i]);
                        if (val > 0)
                        {

                            cur += prev;
                            prev = val;
                        }
                        else
                            prev *= input[i] - '0';
                    }
                }

                Console.Write(cur + prev);

                void Push(int _val)
                {

                    stk[len++] = _val;
                }

                int Pop()
                {

                    if (len == 0) return -1; 
                    return stk[--len];
                }

                int ReadChar(char _)
                {

                    switch (_)
                    {

                        case 'H':
                            return 1;

                        case 'C':
                            return 12;

                        case 'O':
                            return 16;

                        default:
                            return -1;
                    }
                }
            }
        }
    }

#if other
// #include<stdio.h>
int arr[100], h, tmp;
int main() {
	char ch;
	while(scanf("%c ", &ch) != EOF) {
		if (ch > 'A' && ch < 'Z') {
			if (ch == 'H') 
				tmp = 1;
			else if (ch == 'C') 
				tmp = 12;
			else if (ch == 'O')
				tmp = 16;
			arr[h] += tmp;
		}
		else if (ch == '(')
			arr[++h] = 0;
		else if (ch == ')') {
			tmp = arr[h];
			arr[h - 1] += tmp;
			h--;
		}
		else if (ch >= '2' && ch <= '9')
			arr[h] += tmp*(ch - '0' - 1);
	}
	printf("%d\n", arr[0]);
	return 0;
}
#endif
}
