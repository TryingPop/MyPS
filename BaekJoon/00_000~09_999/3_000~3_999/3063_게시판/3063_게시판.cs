using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 28
이름 : 배성훈
내용 : 게시판
    문제번호 : 3063번

    수학, 기하학 문제다
    덮어쓰는 부분의 구간을 기존에 배치된 범위로 수정하고
    넓이를 구해서 빼줬다
*/

namespace BaekJoon.etc
{
    internal class etc_0367
    {

        static void Main367(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt();

            while(test-- > 0)
            {

                int minX = ReadInt();
                int minY = ReadInt();

                int maxX = ReadInt();
                int maxY = ReadInt();

                int chkMinX = ReadInt();
                int chkMinY = ReadInt();
                int chkMaxX = ReadInt();
                int chkMaxY = ReadInt();

                chkMinX = ChkVal(minX, maxX, chkMinX);
                chkMaxX = ChkVal(minX, maxX, chkMaxX);

                chkMinY = ChkVal(minY, maxY, chkMinY);
                chkMaxY = ChkVal(minY, maxY, chkMaxY);

                int ret = GetArea(minX, maxX, minY, maxY);
                ret -= GetArea(chkMinX, chkMaxX, chkMinY, chkMaxY);

                sw.WriteLine(ret);
            }

            sr.Close();
            sw.Close();

            int GetArea(int _minX, int _maxX, int _minY, int _maxY)
            {

                int w = _maxX - _minX;
                int h = _maxY - _minY;
                return w * h;
            }

            int ChkVal(int _min, int _max, int _chk)
            {

                if (_chk < _min) return _min;
                if (_max < _chk) return _max;

                return _chk;
            }

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
