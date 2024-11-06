using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 11
이름 : 배성훈
내용 : LongestPalindrome
    문제번호 : 24832번

    또린드롬 문제이다!
    여러 문자열이 주어지는데, 해당 문자열들을 조합해서 가장 긴 팰린드롬을 만드는 문제이다
    여러 개가 존재하면 그 중에 아무거나 하나 출력하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0018
    {

        static void Main18(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int len = ReadInt(sr);              // 문자열 수
            int strLen = ReadInt(sr);           // 문자의 길이

            // 문자열 입력 받기
            string[] inputs = new string[len];
            for (int i = 0; i < len; i++)
            {

                inputs[i] = sr.ReadLine();
            }
            sr.Close();

            // 쌍이 있는지 확인
            bool[] pair = new bool[len];

            // 출력용
            Queue<int> start = new Queue<int>(50);
            Stack<int> end = new Stack<int>(50);

            int ret = 0;
            for (int i = 0; i < len - 1; i++)
            {

                for (int j = i + 1; j < len; j++)
                {

                    // 이미 쌍이 있으면 사용된 것이므로 넘긴다
                    if (pair[i]) break;
                    // 해당 조건은 없어도된다!
                    // 모든 문자열은 다르기 때문이다
                    // 그렇지만 연산속도를 위해 추가한다
                    // 실제로 72ms -> 68ms
                    else if (pair[j]) continue;

                    // 이어 붙이면 팰린드롬이 되는지 확인
                    bool chk = true;
                    for (int k = 0; k < strLen; k++)
                    {

                        if (inputs[i][k] != inputs[j][strLen - 1 - k])
                        {

                            chk = false;
                            break;
                        }
                    }

                    // 쌍이 되면 탐색에 안쓰이게 하고, 결과에 등록한다
                    if (chk)
                    {

                        pair[i] = true;
                        pair[j] = true;
                        start.Enqueue(i);
                        end.Push(j);

                        ret += strLen * 2;
                    }
                }
            }

            // 중앙에 넣을껄 설정한다!
            for (int i = 0; i < len; i++)
            {

                // 쌍이 있는건 확인 X
                if (pair[i]) continue;

                // 팰린드롬인지 확인!
                bool chk = true;
                for (int k = 0; k < strLen; k++)
                {

                    if (inputs[i][k] != inputs[i][strLen - 1 - k])
                    {

                        chk = false;
                        break;
                    }
                }

                if (chk)
                {

                    // 혼자서 팰린드롬이 되는 문자열 발견!
                    // 그러므로 넣고 for문 탈출
                    // 중앙에 다른 혼자서 팰린드롬이 되는 문자열은 두 개이상 이어붙일 수 없다
                    // 예제 2를 보면 이해할 수 있다!
                    start.Enqueue(i);
                    ret += strLen;
                    break;
                }
            }

            // 결과 출력한다!
            // 먼저 가장 긴 팰린드롬의 길이
            sw.Write(ret);
            sw.Write('\n');
            // 이제 문자열 출력
            while(start.Count > 0)
            {

                sw.Write(inputs[start.Dequeue()]);
            }

            while(end.Count > 0)
            {

                sw.Write(inputs[end.Pop()]);
            }
            sw.Close();
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret *= 10;
                ret += c - '0';
            }

            return ret;
        }
    }
}
