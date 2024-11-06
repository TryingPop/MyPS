using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 22
이름 : 배성훈
내용 : 거북이
    문제번호 : 8911번

    구현, 시뮬레이션 문제다
    조건대로 구현했다

    직사각형 넓이는 그리디하게 x, y의 상한과 하한으로 가로와 세로를 구했다
*/

namespace BaekJoon.etc
{
    internal class etc_0328
    {

        static void Main328(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int x = 0;
            int y = 0;

            int dir = 0;
            int[] dirX = { 1, 0, -1, 0 };
            int[] dirY = { 0, 1, 0, -1 };

            int infX = 0;
            int infY = 0;
            int supX = 0;
            int supY = 0;

            int test = int.Parse(sr.ReadLine());
            while(test-- > 0)
            {

                x = 0;
                y = 0;
                dir = 0;
                
                infX = 0;
                infY = 0;
                supX = 0;
                supY = 0;

                string temp = sr.ReadLine();

                for (int i = 0; i < temp.Length; i++)
                {

                    Move(temp[i]);
                    SetBound();
                }

                int ret = GetArea();
                sw.WriteLine(ret);
            }

            sr.Close();
            sw.Close();

            int GetArea()
            {

                // 면적 계산
                int area = supX - infX;
                area *= supY - infY;

                return area;
            }

            void Move(char c)
            {

                // 이동
                if (c == 'F')
                {

                    x += dirX[dir];
                    y += dirY[dir];
                }
                else if (c == 'B')
                {

                    x -= dirX[dir];
                    y -= dirY[dir];
                }
                else if (c == 'R') dir = dir < 3 ? dir + 1 : 0;
                else dir = dir == 0 ? 3 : dir - 1;
            }

            void SetBound()
            {

                // 상하한 계산
                if (infX > x) infX = x;
                if (supX < x) supX = x;

                if (infY > y) infY = y;
                if (supY < y) supY = y;
            }
        }
    }
}
