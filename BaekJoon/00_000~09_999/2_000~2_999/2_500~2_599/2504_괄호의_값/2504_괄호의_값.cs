using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 28
이름 : 배성훈
내용 : 괄호의 값
    문제번호 : 2504번

    스택 문제다
    숫자 처리부분을 어떻게 할지 몰라서 다른 사람 풀이를 참고했다

    아이디어는 다음과 같다
    괄호 성립 여부는 스택으로 판별하고 숫자 계산은 분배법칙으로 한다
    
    분배법칙은 a * (b + c) = a * b + a * c
    로 풀어씀을 의미한다

    그래서 ([()[()[]]]) 를 입력받았다고 하자 그러면 식으론

        2 * 3 * (2 + 3 * (2 + 3))
        = 2 * 3 * 2 + 2 * 3 * 3 * 2 + 2 * 3 * 3 * 3
    이되게 바꿔줘야한다

    그래서 isOp라는 임시변수를 둬서 괄호가 시작되는 (, [ 입력이 들어오는 순간 true로 해서
    ), ]게 나오면 임시변수를 정답에 담을 준비 하라고 알려준다
    임시변수를 담으면 바로 false로 바꾼다
    isOp를 써야하는 이유는 2 * 3 * 2가 되면 딱 한 번만 더해야하는데,
    없으면 2 * 3 * 2 + 2 * 3 + 2가 될 수 있어 별도 처리를 해준 것이다

    calc는 앞의 곱해줄 값이다 (, [이 입력되는 순간 곱해준다
    그리고 ), ] 이 나오는 순간 곱한 값을 나눠준다 
    그리고 isOp가 true인 순간만 값을 answer에 더해준다

    이렇게 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0370
    {

        static void Main370(string[] args)
        {

            string str = Console.ReadLine();

            Stack<char> s = new Stack<char>(str.Length);

            int ret = 0;
            int calc = 1;
            bool isOp = false;
            for (int i = 0; i < str.Length; i++)
            {

                if (s.Count > 0)
                {

                    if (s.Peek() == '(' && str[i] == ')') 
                    {

                        if (isOp)
                        {

                            isOp = false;
                            ret += calc;
                        }
                        calc /= 2;
                        s.Pop(); 
                    }
                    else if (s.Peek() == '[' && str[i] == ']')
                    {

                        if (isOp)
                        {

                            isOp = false;
                            ret += calc;
                        }
                        calc /= 3;
                        s.Pop();
                    }
                    else
                    {

                        isOp = true;
                        if (str[i] == '(') calc *= 2;
                        else calc *= 3;
                        s.Push(str[i]);
                    }
                }
                else 
                {

                    isOp = true;
                    if (str[i] == '(') calc *= 2;
                    else calc *= 3;
                    s.Push(str[i]); 
                }
            }

            if (s.Count > 0) ret *= 0;
            Console.WriteLine(ret);
        }
    }

#if other
var str = Console.ReadLine()!;
var stack = new ValueTuple<bool?, int>[31];
var cnt = 0;
Push(null, 0);
foreach (var c in str)
{
    switch (c)
    {
        case '(':
            Push(true, 0);
            break;
        case ')':
            if (!Close(true, 2))
                goto fail;
            break;
        case '[':
            Push(false, 0);
            break;
        default:
            if (!Close(false, 3))
                goto fail;
            break;
    }
}

if (cnt == 1)
{
    Console.Write(stack[0].Item2);
    return;
}
fail: Console.Write(0);

void Push(bool? value, int sum)
{
    stack[cnt++] = (value, sum);
}

bool TryPop(out bool? value, out int sum)
{
    (value, sum) = stack[--cnt];
    return cnt >= 0;
}

bool Close(bool compared, int weight)
{
    if (TryPop(out var value, out var sum) && value == compared)
    {
        if (sum == 0) sum = 1;
        stack[cnt - 1].Item2 += sum * weight;
        return true;
    }
    return false;
}
#endif
}
