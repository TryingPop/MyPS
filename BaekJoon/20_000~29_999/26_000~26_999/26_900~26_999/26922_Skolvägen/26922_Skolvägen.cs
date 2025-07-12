using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 18
이름 : 배성훈
내용 : Skolvägen
    문제번호 : 26922번

    dp 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_1065
    {

        static void Main1065(string[] args)
        {

            string arr;
            int[] up, down;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                up = new int[arr.Length + 1];
                down = new int[arr.Length + 1];

                down[0] = 1;
                for (int i = 0; i < arr.Length; i++)
                {

                    if (arr[i] == 'S')
                    {

                        // 밑에만 다리가 있는경우
                        // 밑은 다리를 1개 지나거나 위에서 내려오는 경우 중 작은 경우
                        down[i + 1] = Math.Min(down[i] + 1, up[i] + 1);
                        // 위는 이전 땅에서 올 수 있으니 다리를 지나지 않는 경우와
                        // 밑에서 위로 오는 경우 중 작은 걸 택
                        up[i + 1] = Math.Min(down[i] + 1, up[i]);
                    }
                    else if (arr[i] == 'N')
                    {

                        // 다리가 위에만 있는 경우
                        // 위에서 가려면 다리를 위의 다리를 건너야한다
                        // 아래는 올라오는 다리를 건너야한다
                        up[i + 1] = Math.Min(up[i] + 1, down[i] + 1);
                        // 아래는 다리가 없으므로 이전 아래 경우와
                        // 위에서 내려오는 경우를 비교하면 된다
                        down[i + 1] = Math.Min(down[i], up[i] + 1);
                    }
                    else
                    {

                        // 위 아래 있는 경우
                        up[i + 1] = Math.Min(up[i] + 1, down[i] + 2);
                        down[i + 1] = Math.Min(down[i] + 1, up[i] + 2);
                    }
                }

                Console.Write(up[arr.Length]);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                arr = sr.ReadLine();

                sr.Close();
            }
        }
    }
}
