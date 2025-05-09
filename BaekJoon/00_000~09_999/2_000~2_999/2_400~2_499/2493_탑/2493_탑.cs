using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 28
이름 : 배성훈
내용 : 탑
    문제번호 : 2493번

    스택 문제다
    처음에 어떻게 접근해야할지 몰라서 힌트를 봤다
    힌트를 봐서 풀 수 있었다

    아이디어는 다음과 같다
    현재층에서 전파를 쐈을 때 현재층보다 낮은층은 수신이 안된다
    그리고 다음 연산에서 현재층이 현재층보다 낮은 것들을 다 수신하기 때문에
    현재 층보다 이전 낮은 층들은 현재 층 비교가 끝나면 제외한다

    그리고 현재 층보다 높은 가장 가까운 층을 찾아야한다 만약 없다면 0
    있다면 해당 순서를 기록

    그래서 저장할 데이터는 높이와 인덱스가 된다
    가장 가까운 것을 확인해야 하기에 자료구조는 스택임을 확인했다

    순차적으로 확인할 때, 스택에 현재층보다 낮은 층들은 다 빼버리고, 
    높은 층이 있으면 해당 층을 결과에 기록하고 높은 층이 없다면 탐색을 종료하고,
    현재 층의 높이와 인덱스를 스택에 넣는다
    
    이렇게 진행해서 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0369
    {

        static void Main369(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            Stack<(int height, int idx)> s = new Stack<(int height, int idx)>(n);
            // 번째 이므로, 번째에 맞춰 인덱스를 쓴다
            int[] ret = new int[n + 1];

            s.Push((ReadInt(), 1));

            for (int i = 2; i <= n; i++)
            {

                int cur = ReadInt();

                while(s.Count > 0)
                {

                    // 높이가 모두 달라 =을 생각할 필요가 없다
                    if (s.Peek().height > cur)
                    {

                        ret[i] = s.Peek().idx;
                        break;
                    }
                    else s.Pop();
                }

                s.Push((cur, i));
            }

            sr.Close();

            StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()));

            for (int i = 1; i <= n; i++)
            {

                sw.Write($"{ret[i]} ");
            }

            sw.Close();

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }

    }
}
