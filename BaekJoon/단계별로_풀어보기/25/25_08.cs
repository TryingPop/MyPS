using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

/*
날짜 : 2023. 10. 20
이름 : 배성훈
내용 : 스타트와 링크
    문제번호 : 14889번

    다른 사람껄 보니 visited로 백트래킹을 구현했다.
    그래서 따라 했다

    기존에는 for문안에 else로 
    break을 넣었다

    그리고 속도를 올리고 싶다면, list를 2개 쓰면 더 빨라진다

    먼저, 백준은 과정보다 정답에 초점을 맞추자
    속도는 해커랭크에서 챙기자!
 */

namespace BaekJoon._25
{
    internal class _25_08
    {

        static void Main8(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int len = int.Parse(sr.ReadLine());
            int[][] abilities = new int[len][];
            int total = 0;
            using (sr)
            {

                for (int i = 0; i < len; i++)
                {

                    abilities[i] = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
                    total += abilities[i].Sum();
                }
            }

            int dis = int.MaxValue;
            bool[] visited = new bool[len];
            List<int> top = new List<int>();

            Back(abilities, visited, top, 0, len / 2, ref dis);

            Console.WriteLine(dis);
        }

        /// <summary>
        /// 백트래킹
        /// </summary>
        public static void Back(int[][] arr, bool[] visited, List<int> top, int idx, int maxNum, ref int dis)
        {

            if (top.Count == maxNum)
            {

                int tempDis = Calc(arr, top);
                if (tempDis < 0) tempDis = -tempDis;
                if (dis > tempDis) dis = tempDis;
                return;
            }

            for (int i = idx; i < arr.Length; i++)
            {

                if (visited[i]) continue;
                visited[i] = true;
                top.Add(i);
                Back(arr, visited, top, i + 1, maxNum, ref dis);
                top.Remove(i);

                visited[i] = false;
            }
        }

        /// <summary>
        /// 총합 계산
        /// </summary>
        public static int Calc(int[][] arr, List<int> calc)
        {

            int num1 = 0;
            int num2 = 0;
            
            for (int i = 1; i < arr.Length; i++)
            {

                bool chk = false;

                if (calc.Contains(i))
                {

                    chk = true;
                }

                for (int j = 0; j < i; j++)
                {

                    if (calc.Contains(j) == chk)
                    {

                        if (chk)
                        {

                            num1 += arr[i][j] + arr[j][i];
                        }
                        else
                        {

                            num2 += arr[i][j] + arr[j][i];
                        }
                    }
                }
            }

            return num1 - num2;
        }
    }
}
