using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 6
이름 : 배성훈
내용 : 후위 표기식
    문제번호 : 1918번

    스택 문제다
    조건에 맞춰 구분해서 연산을 진행했다
*/

namespace BaekJoon.etc
{
    internal class etc_0470
    {

        static void Main470(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());
            string str = sr.ReadLine();
            sr.Close();

            Stack<char> alpha = new(100);
            Stack<char> op = new(100);
            char[] ret = new char[str.Length];
            int cur = 0;

            for (int i = 0; i < str.Length; i++)
            {

                if (str[i] <= 'Z' && 'A' <= str[i])
                {

                    // 알파벳은 그냥 순서대로 넣으면 된다
                    ret[cur++] = str[i];
                }
                else if (str[i] == '*' || str[i] == '/')
                {

                    // 곱셈 연산 -> 앞번의 곱셈들 순차적으로 처리
                    while(op.Count > 0 && (op.Peek() == '*' || op.Peek() == '/'))
                    {

                        ret[cur++] = op.Pop();
                    }
                    op.Push(str[i]);
                }
                else if (str[i] == '(')
                {

                    op.Push(str[i]);
                }
                else if (str[i] == ')')
                {

                    // 괄호안 남은 부분 연산처리
                    while(op.Peek() != '(')
                    {

                        ret[cur++] = op.Pop();
                    }

                    op.Pop();
                }
                else
                {

                    // +, -
                    while(op.Count > 0 && op.Peek() != '(')
                    {

                        ret[cur++] = op.Pop();
                    }

                    op.Push(str[i]);
                }
            }

            // 남은 연산 처리
            while(op.Count > 0)
            {

                ret[cur++] = op.Pop();
            }

            using (StreamWriter sw = new(Console.OpenStandardOutput()))
            {

                for (int i = 0; i < cur; i++)
                {

                    sw.Write(ret[i]);
                }
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.Text;

namespace boj1918_후위표기식
{
    
    class Program
    {
        static public Stack<char> stack = new Stack<char>();
        static public int level = 0;
        static public int bracketLevel = 0;

        static StringBuilder strb = new StringBuilder();

        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            for (int i = 0; i < input.Length; i++)
            {
                char current = input[i];
                int currentLevel = 0;
                int topLevel = 0;
                if (isOperator(current))
                {
                    if (current == '(')
                    {
                        bracketLevel++;
                    }
                    else if (current == ')')
                    {
                        bracketLevel--;
                        char next = '&';
                        while (next != '(')
                        {
                            next = stack.Pop();
                            if (!isBracket(next))
                                Record(next);
                        }
                    }
                    else if (!isBracket(current))
                    {
                        topLevel = -1;
                        if (stack.Count > 0)
                            topLevel = getPriority(stack.Peek());
                        currentLevel = getPriority(current);
                        if (currentLevel <= topLevel)
                        {
                            Emit(currentLevel);
                        }
                    }
                    if(current != ')')
                        stack.Push(current);
                }
                else
                {
                    Record(current);
                }
            }
            Emit(-1);
            Console.WriteLine(strb.ToString());
        }

        static void Record(char c)
        {
            if (!isBracket(c))
                strb.Append(c.ToString());
        }

        static void Emit(int level)
        {
            while (stack.Count > 0)
            {
                if (stack.Peek() == '(')
                    break;
                if (!(getPriority(stack.Peek()) >= level))
                    break;
                Record(stack.Pop());
            }
        }

        static bool isBracket(char c)
        {
            return c == '(' || c == ')';
        }

        static bool isOperator(char c)
        {
            return c == '-' || c == '+' || c == '*' || c == '/' || isBracket(c);
        }

        static int getPriority(char c)
        {
            if (c == '-' || c == '+')
                return 0;
            else if (c == '*' || c == '/')
                return 1;
            return 0;
        }
    }
}

#elif other2
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Baekjoon
{
    class Program
    {
        static void Main()
        {
            var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            var sb = new StringBuilder();

            /*
            for (int test = 1; test <= 50; test++)
            {
                string[] firstLine = sr.ReadLine().Split();
                string input = firstLine[1];
                string[] secondLine = sr.ReadLine().Split();
                string testAnswer = secondLine[1];
            */
            string input = sr.ReadLine();
            string last(int start, int end)
            {
                if ((end - start) == 1)
                    return input[start].ToString();

                int bracket = 0, minPriority = int.MaxValue, operatorCount = 0;
                int? mpIndex = null;
                for (int i = start; i < end; i++)
                {
                    int currentPriority = int.MaxValue;
                    if (input[i] == '+' || input[i] == '-')
                    {
                        currentPriority = bracket * 10000 + (100 - i);
                        operatorCount++;
                    }
                    if (input[i] == '*' || input[i] == '/')
                    {
                        currentPriority = bracket * 10000 + 1000 + (100 - i);
                        operatorCount++;
                    }
                    if (input[i] == '(')
                        bracket++;
                    if (input[i] == ')')
                        bracket--;

                    if (currentPriority < minPriority)
                    {
                        minPriority = currentPriority;
                        mpIndex = i;
                    }
                }
                if (mpIndex == null)
                {
                    while (input[start] == '(')
                        start++;
                    while (input[end - 1] == ')')
                        end--;
                    return last(start, end);
                }

                int minPriorityIndex = (int)mpIndex;

                if (operatorCount == 1)
                    return input[minPriorityIndex - 1].ToString() + input[minPriorityIndex + 1].ToString() + input[minPriorityIndex].ToString();
                else
                    return last(start, minPriorityIndex) + last(minPriorityIndex + 1, end) + input[minPriorityIndex].ToString();
            }

            sw.WriteLine(last(0, input.Length));
            /*
            string answer = last(0, input.Length);
            if (answer == testAnswer)
                Console.WriteLine($"Test {test} Completed");
            else
                Console.WriteLine($"Test {test} inCompleted : wrong answer - {answer}");
            }
            */

            sr.Close();
            sw.Close();
        }
    }
}
#elif other3
public static class PS
{
    public static void Main()
    {
        StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()));
        Stack<char> stack = new();

        string s = Console.ReadLine();
        char c;

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] >= 'A')
            {
                sw.Write(s[i]);
            }
            else
            {
                if (s[i] == '(')
                {
                    stack.Push('(');
                }
                else if (s[i] == ')')
                {
                    while ((c = stack.Pop()) != '(')
                    {
                        sw.Write(c);
                    }
                }
                else
                {
                    while (stack.TryPop(out c))
                    {
                        if (s[i].ComparePriority(c) <= 0)
                        {
                            sw.Write(c);
                        }
                        else
                        {
                            stack.Push(c);
                            break;
                        }
                    }

                    stack.Push(s[i]);
                }
            }
        }

        while (stack.TryPop(out c))
        {
            sw.Write(c);
        }

        sw.Close();
    }

    public static int ComparePriority(this char c, char other)
    {
        if (other == '(')
        {
            return 1;
        }
        else
        {
            switch (c)
            {
                case '*':
                case '/':
                    if (other == '*' || other == '/')
                        return 0;
                    else
                        return 1;

                default:
                    if (other == '+' || other == '-')
                        return 0;
                    else
                        return -1;
            }
        }
    }
}
#endif
}
