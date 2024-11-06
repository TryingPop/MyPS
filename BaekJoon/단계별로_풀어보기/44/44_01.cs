using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 28
이름 : 배성훈
내용 : 가장 가까운 공통 조상
    문제번호 : 3584번

    로직은 맞았다.. 그런데 입력 문제로 자꾸 Format에러가 떴다
    StreamReader를 선언해놓고 Console.ReadLine으로 불러오니 Format에러가 떴다

    전에 사용하던 위에서부터 찾아가는게 아닌 아래서부터 찾아가게 바꿨다
    이 문제에서는 아래에서부터 찾아가는게 느렸다

    만약 위에서부터 찾아간다면, 이진탐색으로 탐색시간을 N을 logN으로 줄일 수 있다!
*/

namespace BaekJoon._44
{


    internal class _44_01
    {

        static void Main1(string[] args)
        {


            int MAX = 10_000;
            int[] parent = new int[MAX + 1];
            // int[,] parent = new int[MAX + 1,(int)Math.Log2(MAX) + 1];

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = int.Parse(sr.ReadLine());

            for (int t = 0; t < test; t++)
            {

                int len = int.Parse(sr.ReadLine());

                for (int i = 1; i <= len; i++)
                {

                    parent[i] = 0;
                }

                for (int i = 0; i < len - 1; i++)
                {

                    int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                    parent[temp[1]] = temp[0];
                }

                int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                FindParent(parent, info[0], out int depth1);
                FindParent(parent, info[1], out int depth2);

                while(depth1 != depth2)
                {

                    if (depth1 > depth2)
                    {

                        info[0] = parent[info[0]];
                        depth1--;
                    }
                    else
                    {

                        info[1] = parent[info[1]];
                        depth2--;
                    }
                }

                while (info[0] != info[1])
                {

                    info[0] = parent[info[0]];
                    info[1] = parent[info[1]];
                }

                sw.Write(info[0]);
                sw.Write('\n');
            }

            sw.Close();
            sr.Close();
        }

        // 부모 연결!
        
        static void FindParent(int[] _parents, int _find, out int _depth)
        {

            _depth = 0;
            while(_find != 0)
            {

                _depth++;
                _find = _parents[_find];
            }
        }
    }
}
