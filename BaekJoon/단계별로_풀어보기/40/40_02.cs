using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 21
이름 : 배성훈
내용 : CCW
    문제번호 : 11758번

    p1을 시작지점 p2를 끝점으로 하는 벡터를 dir1,
    p2을 시작지점 p3를 끝점으로 하는 벡터를 dir2로해서 (시작지점을 p2가 아닌 p1으로 해도 된다!)

    dir1에서 dir2로 이동하는 방향이 시계방향이 빠른지 반시계 방향이 빠른지 확인하는 문제로 해석했었다
    둘이 같은 경우면 직선으로 인정한다로 해석했다

    접근 자체는 맞았으나, 조건을 나누고 값을 넣는 과정에서 여러 번 틀렸다
    처음에는 float으로 연산했으나, 소수로 나눠버리는 경우 오차가 생겨 틀린줄 알았다
    혹시, 몰라 입력되는 좌표 값이 1만 이하의 수이므로 int로 서로 x 값을 곱해서 크기를 맞췄다
    
    그리고 기준이 되는 선을 긋고 위 구간인지 아래 구간인지 판별했다
    체감상 4번 정도면 충분할거 같은데, 4번으로 함축하는게 의미없어 보여 그냥 여러 케이스로 쪼개서 했다

    반환값 실수로, 여러 번 틀리고 결국 외적을 써서 제출했다; (이걸 CCW 알고리즘이라 하더라...) 
    이후 if문 조건 따져보니 잘못된 구간을 찾았고, _dir1[0] < 0 부분에서 반환해야할 부호가 바껴 틀렸었다;

    지금은 수정해서 이상없다!
*/

namespace BaekJoon._40
{
    internal class _40_02
    {

        static void Main2(string[] args)
        {

            int[] p1 = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int[] p2 = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int[] p3 = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

#if Wrong
            // 벡터로 비교하자
            int[] dir1 = GetDir(p1, p2);
            int[] dir2 = GetDir(p1, p3);

            Scale(dir1, dir2);

            // 계산
            Console.WriteLine(ChkDir(dir1, dir2));
#else

            // 걍 외적쓰자;
            int area = p1[0] * p2[1] + p2[0] * p3[1] + p3[0] * p1[1];
            area -= p1[0] * p3[1] + p2[0] * p1[1] + p3[0] * p2[1];

            if (area > 0) area = 1;
            else if (area < 0) area = -1;

            Console.WriteLine(area);
#endif
        }

#if Wrong
        static int[] GetDir(int[] _start, int[] _end)
        {

            // 벡터 찾기
            int[] result = new int[2];

            result[0] = _end[0] - _start[0];
            result[1] = _end[1] - _start[1];

            return result;
        }

        static void Scale(int[] _dir1, int[] _dir2)
        {
            
            // X크기 맞춰주는 스케일링
            int scale1 = _dir1[0] < 0 ? -_dir1[0] : _dir1[0];
            int scale2 = _dir2[0] < 0 ? -_dir2[0] : _dir2[0];

            _dir1[0] *= scale2;
            _dir1[1] *= scale2;
            _dir2[0] *= scale1;
            _dir2[1] *= scale1;
        }

        static int ChkDir(int[] _dir1, int[] _dir2)
        {

            int p = 1;

            if (_dir1[0] == 0)
            {

                if (_dir1[1] < 0) p = -1;

                if (_dir2[0] > 0) return p * 1;
                if (_dir2[0] < 0) return p * -1;

                return 0;
            }

            if (_dir1[0] > 0)
            {

                if (_dir2[0] < 0)
                {

                    p = -1;
                    _dir2[1] = -_dir2[1];
                }

                if (_dir1[1] > _dir2[1]) return p * -1;
                if (_dir1[1] < _dir2[1]) return p * 1;

                return 0;
            }


            p = -1;
            // _dir1[0] < 0
            if (_dir2[0] > 0)
            {

                p = 1;
                _dir2[1] = -_dir2[1];
            }

            if (_dir1[1] > _dir2[1]) return p * -1;
            if (_dir1[1] < _dir2[1]) return p * 1;

            return 0;
        }
#endif
    }
}
