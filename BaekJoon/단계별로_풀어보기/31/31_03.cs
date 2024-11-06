using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 24
이름 : 배성훈
내용 : 오등큰수
    문제번호 : 17299번

    앞의 문제를 그대로 하니 쉽게 완성!
*/

namespace BaekJoon._31
{
    internal class _31_03
    {

        static void Main3(string[] args)
        {

            const int MAX = 1_000_000;

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            int[] arr = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            sr.Close();

            int[] cnts = new int[MAX + 1];

            for (int i= 0; i < len; i++)
            {

                cnts[arr[i]]++;
            }

            // 이제 연산 시작!
            int[] result = new int[len];

            Stack<int> temp = new Stack<int>();

            cnts[0] = -1;
            result[len - 1] = cnts[0];
            temp.Push(0);

            for (int i = len - 2; i >= 0; i--)
            {

                int before = arr[i];
                int after = arr[i + 1];

                if (cnts[before] < cnts[after])
                {

                    temp.Push(after);
                    result[i] = after;
                }
                else if (after == 0)
                {

                    result[i] = -1;
                }
                else
                {

                    while(temp.Count > 0)
                    {

                        int calc = cnts[temp.Peek()];

                        if (calc > cnts[before])
                        {

                            result[i] = temp.Peek();
                            break;
                        }
                        else if (calc == -1)
                        {

                            result[i] = -1;
                            break;
                        }
                        else
                        {

                            temp.Pop();
                        }
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < len; i++)
                {

                    sw.Write(result[i]);
                    sw.Write(' ');
                }
            }
        }
    }
}
