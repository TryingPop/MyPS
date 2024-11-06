using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 6
이름 : 배성훈
내용 : 후위 표기식 2
    문제번호 : 1935번

    스택 문제다
    후위 표기식에 대한 설명이 없어 왜 이렇게 나오는지 의미없는 고민을 했다
    이후 다른 문제에서 후위 표기식을 검색해 해당 규칙을 확인했다
    후위 표기식은 다음과 같다

    연산이 뒤에 오는 표기법이다
    A + B -> AB+
    A + B * C -> ABC*+ 
    A * (B + C) -> ABC+*

    연산을 보면 문자열 뒤쪽부터 꺼내어 연산을 해서 스택을 이용해 풀었다
    부동소수점 오차가 있을거 같아 decimal 로 연산을 했다
*/

namespace BaekJoon.etc
{
    internal class etc_0467
    {

        static void Main467(string[] args)
        {

            char PLUS = '+';
            char MINUS = '-';
            char MUL = '*';
            char DIV = '/';

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n = int.Parse(sr.ReadLine());

            string str = sr.ReadLine();

            decimal[] arr = new decimal[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = decimal.Parse(sr.ReadLine());
            }
            sr.Close();

            Stack<decimal> s = new(100);

            for (int i = 0; i < str.Length; i++)
            {

                if (IsOP(str[i]))
                {

                    decimal n1 = s.Pop();
                    decimal n2 = s.Pop();

                    if (str[i] == PLUS) s.Push(n1 + n2);
                    else if (str[i] == MINUS) s.Push(n2 - n1);
                    else if (str[i] == MUL) s.Push(n1 * n2);
                    else s.Push(n2 / n1);
                }
                else s.Push(arr[str[i] - 'A']);
            }

            decimal ret = s.Pop();
            Console.WriteLine($"{ret:0.00}");

            bool IsOP(char _c)
            {

                if (_c == PLUS || _c == MINUS || _c == MUL || _c == DIV) return true;
                return false;
            }
        }
    }

#if other
using System;
using System.Collections.Generic;

namespace Baek1935
{
    class Program
    {
        static class DEFINE
        {
            public const int MAX = 26;

        }

        static int N;

        static void Main(string[] args)
        {
            int[] nums = new int[DEFINE.MAX];


            N = int.Parse(Console.ReadLine());

            string inputs = Console.ReadLine();

            for (int i = 0; i < N; i++)
            {
                nums[i] = int.Parse(Console.ReadLine());
            }
            Stack<double> operand = new Stack<double>();
            Stack<char> operators = new Stack<char>();
            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i] - 'A' >= 0)
                {
                    operand.Push(nums[inputs[i] - 'A']);
                }
                else
                {
                    double x = operand.Pop();
                    double y = operand.Pop();
                    switch (inputs[i])
                    {
                        case '/':
                            operand.Push(y /x);
                            break;
                        case '+':
                            operand.Push(y + x);
                            break;
                        case '*':
                            operand.Push(y * x);
                            break;
                        case '-':
                            operand.Push(y - x);
                            break;
                    }
                }
            }
            Console.WriteLine(operand.Pop().ToString("F2"));

        }
    }
}

#endif
}
