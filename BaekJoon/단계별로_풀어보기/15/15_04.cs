using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 14
이름 : 배성훈
내용 : 색종이
    문제번호 : 2563번
*/

namespace BaekJoon._15
{
    internal class _15_04
    {

        static void Main4(string[] args)
        {

            const int width = 10;
            const int height = 10;
            int len = int.Parse(Console.ReadLine());

            int[,] pos = new int[len, 2];

            for (int i = 0; i < len; i++)
            {

                int[] input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                pos[i, 0] = input[0];
                pos[i, 1] = input[1];
            }


            int result = 0;
            for (int i = 0; i < len; i++)
            {
                
                // 겹치는 구간 판별용 좌표 생성
                // 겹치면 true, 안겹치면 false
                bool[,] tile = new bool[width, height];

                // 좌표가 겹치는지 판별
                // 중복을 피하기 위해 먼저 배치된 것만 확인한다
                for (int j = 0; j < i; j++)
                {

                    int lenX = pos[j, 0] - pos[i, 0];
                    int lenY = pos[j, 1] - pos[i, 1];
                    // 겹치는 구간이 있는지 판별
                    if (Math.Abs(lenX) < width && Math.Abs(lenY) < height)
                    {

                        // 좌표 설정
                        int minX, minY, maxX, maxY;
                        if (lenX < 0)
                        {
                            
                            // 겹치는 부분이 시작 왼쪽에 닿을 때 
                            minX = 0;
                            maxX = width + lenX;
                        }
                        else
                        {

                            // 겹치는 부분이 오른쪽에 닿을 때
                            minX = lenX;
                            maxX = width;
                        }

                        if (lenY < 0)
                        {

                            // 겹치는 부분이 위에 닿을 때 
                            minY = 0;
                            maxY = height + lenY;
                        }
                        else
                        {

                            // 겹치는 부분이 아래에 닿을 때
                            minY = lenY;
                            maxY = height;
                        }

                        for (int x = minX; x < maxX; x++)
                        {

                            for (int y = minY; y < maxY; y++)
                            {

                                // 겹치는 구간이므로 true
                                tile[x, y] = true;
                            }
                        }
                    }
                }

                // 겹치지 않는 너비 값 추가
                for (int x = 0; x < width; x++)
                {

                    for (int y = 0; y < height; y++)
                    {

                        if (!tile[x, y])
                        {

                            
                            result++;
                        }
                    }
                }
            }
            Console.WriteLine(result);
        }
    }
}
