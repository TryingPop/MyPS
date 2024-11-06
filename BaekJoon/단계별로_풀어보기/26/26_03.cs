using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 10. 22
이름 : 배성훈
내용 : 인간 - 컴퓨터 상호작용
    문제번호 : 16139번

    동적 계획법으로 풀어야 100점이 나온다
    이때 정답을 저장하는 자료형을 ushort로 할 경우 시간 초과가 뜬다
    ushort가 메모리 사용량은 적으나 int 연산보다 느리다
*/
namespace BaekJoon._26
{
    internal class _26_03
    {

        static void Main3(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            string word = sr.ReadLine();

            int len = int.Parse(sr.ReadLine());

            StringBuilder sb = new StringBuilder();

            // 저장하는 방식으로 !


            /*
            // 개수에 맞는 idx를 저장..
            // 0 번 인덱스는 개수!
            ushort[,] sol = new ushort[26, word.Length + 1];
            for (ushort i = 0; i < word.Length; i++)
            {

                int idx1 = (word[i]) - 'a';
                int idx2 = ++sol[idx1, 0];
                sol[idx1, idx2] = i;
            }

            for (int i = 0; i < len; i++)
            {

                string[] input = sr.ReadLine().Split(' ');

                int findWord = char.Parse(input[0]) - 'a';
                int startIdx = int.Parse(input[1]);
                int endIdx = int.Parse(input[2]);

                int wordNum = sol[findWord, 0];
                if (wordNum == 0) 
                {

                    sb.AppendLine("0");
                    continue; 
                }
                int min = 0;
                int max = 0;

                for (int j = 1; j <= wordNum; j++)
                {

                    if (startIdx > sol[findWord, j])
                    {

                        min++;
                    }

                    if (endIdx >= sol[findWord, j])
                    {

                        max++;
                    }
                }

                sb.AppendLine((max - min).ToString());
            }
            */

            int[,] sol = new int[26, word.Length + 1];

            for (int i = 0; i < word.Length; i++)
            {

                int idx1 = (word[i]) - 'a';
                sol[idx1, i + 1] = 1;

                for (int j = 0; j < 26; j++)
                {

                    sol[j, i + 1] += sol[j, i];
                }
            }

            for (int i = 0; i < len; i++)
            {

                string[] input = sr.ReadLine().Split(' ');
                int findWord = char.Parse(input[0]) - 'a';
                int startIdx = int.Parse(input[1]);
                int endIdx = int.Parse(input[2]);

                sb.AppendLine((sol[findWord, endIdx + 1] - sol[findWord, startIdx]).ToString());
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(sb);
            }

            sr.Close();
        }
#if first
            // 정답 찾아가는 로직은 맞으나, 순서 문제가 있다

            for (int i = 0; i < len; i++)
            {

                string[] input = sr.ReadLine().Split(' ');

                char findWord = char.Parse(input[0]);
                int startIdx = int.Parse(input[1]);
                int endIdx = int.Parse(input[2]);

                int result = 0;
                for (int j = startIdx; j <= endIdx; j++)
                {

                    if (word[j] == findWord)
                    {

                        result++;
                    }
                }

                sb.AppendLine(result.ToString());
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(sb);
            }
        }
#endif
    }
}
