using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 20
이름 : 배성훈
내용 : 다각형의 면적
    문제번호 : 2166번

    현재 오답이다!
    질문 게시판의 반례들을 찾아보니 교과서에서 명시한 도형이 아닌 경우도 입력될 수 있다고 한다
    오목한 경우가 끼여있다!

    즉, 외적써서 풀라는 소리이다
    그런데 무턱대고 외적을 쓰면 또 틀린다
    면적이라고 했으므로 음수가 나올 수 없다
*/

namespace BaekJoon._40
{
    internal class _40_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            double areas = 0.0;

            // 초기 세팅
            Pos initPos;
            {

                string[] temp = sr.ReadLine().Split(' ');

                initPos.x = long.Parse(temp[0]);
                initPos.y = long.Parse(temp[1]);
            }

            Pos beforePos;
            { 
                string[] temp = sr.ReadLine().Split(' ');

                beforePos.x = long.Parse(temp[0]);
                beforePos.y = long.Parse(temp[1]);
            }

            Pos curPos;
            double result = 0;
            for (int i = 2; i < len; i++)
            {

                string[] temp = sr.ReadLine().Split(' ');

                curPos.x = long.Parse(temp[0]);
                curPos.y = long.Parse(temp[1]);

                // 삼각형 연산
                result += GetTriangleArea(initPos, beforePos, curPos);
                beforePos = curPos;
            }

            sr.Close();
            if (result < 0) result = -result;
            Console.WriteLine($"{result:0.0}");
        }

        struct Pos
        {

            public long x;
            public long y;
        }
        
        static double GetTriangleArea(Pos _pos1, Pos _pos2, Pos _pos3)
        {

            /*
            // 교과서에서 명시한 다각형의 경우 오목한 경우는 다각형이라 명명하지 않기에
            // 삼각형을 이어붙이는 방식을 처음에 사용했따
            // 아래는 외적이 아닌 해당 세 점을 포함하는 직 사각형을 만들어 
            // 바깥부분을 제거하는 형식으로 했다
            GetMinMax(_pos1.x, _pos2.x, _pos3.x, out int left, out int right);
            GetMinMax(_pos1.y, _pos2.y, _pos3.y, out int bottom, out int top);

            // 사각형 면적
            double rectArea = GetRectArea(right - left, top - bottom);
            var chk = rectArea;
            rectArea -= GetRectArea(_pos1.x - _pos2.x, _pos1.y - _pos2.y, true);
            rectArea -= GetRectArea(_pos2.x - _pos3.x, _pos2.y - _pos3.y, true);
            rectArea -= GetRectArea(_pos3.x - _pos1.x, _pos3.y - _pos1.y, true);
            return rectArea;
            */

            // 외적
            double result = 0.0;

            result += _pos1.x * _pos2.y + _pos2.x * _pos3.y + _pos3.x * _pos1.y;
            result -= _pos1.x * _pos3.y + _pos2.x * _pos1.y + _pos3.x * _pos2.y;

            result *= 0.5;
            return result;

        }

        /*
        static void GetMinMax(int _a, int _b, int _c, out int _min, out int _max)
        {

            if (_a < _b)
            {

                _min = _a;
                _max = _b;
            }
            else
            {

                _min = _b;
                _max = _a;
            }

            _min = _min < _c ? _min : _c;
            _max = _max > _c ? _max : _c;
        }

        static double GetRectArea(long _width, long _height, bool _half = false)
        {
            
            _width = _width > 0 ? _width : -_width;
            _height = _height > 0 ? _height : -_height;
            if (_half) return _width * _height * 0.5;
            else return _width * _height;
        }
        */
    }
}
