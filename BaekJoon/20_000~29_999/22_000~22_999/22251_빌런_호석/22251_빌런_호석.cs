using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. ?
이름 : 배성훈
내용 : 빌런 호석
    문제번호 : 22251번

    
    풀이 아이디어는 다음과 같다
    먼저 숫자 변환 할때 들어가는 수를 수작업으로 찾았다;
    그리고 수 변경을 하면 표를 보고 참고하게 했다(O(1))

    현재 층을 K(info[1])자리수로 표현한다 
        만약 현재층이 3층이고 자리수가 3인 경우 0 0 3으로
        또는 현재층이 1층이고 자리수가 2인 경우 0 1으로 했다
    그리고 각 자리수 숫자를 변형 시킨다
    변형 시킨 횟수가 조건에서 주어진 P(info[2]) 이하에 N(info[0])이하인 경우 카운팅했다
    (0층 경우를 세어서 1번 틀렸다)
    
    카운팅 방법으로는 DFS를 이용했다

    다른 사람의 풀이를 보니
    숫자를 변환할 때, 들어가는 표를 수작업이 아닌 코드로 찾는 방법이 있었다;
    9개 자리를 이용해 전광판이 켜진 경우 1, 꺼진경우 0으로 해서 표현했다;
        예를들어 
                 
              1 -
            2  | |  3
              4 -
            5  | |  6
              7 -
            이렇게 전광판에 번호를 부여하자
            그러면 0은 0x_1110111 로 나타낼 수 있다
            그리고 비트 연산으로 다르면 1씩 추가하면 된다
            간단하게 표를 만들 수 있다!
*/

namespace BaekJoon.etc
{
    internal class etc_0060
    {

        static void Main60(string[] args)
        {
            
            int[] info = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int[][] change = new int[10][];
            
            // Change 채우기;
            {

                change[0] = new int[10] { 0, 4, 3, 3, 4, 3, 2, 3, 1, 2 };
                change[1] = new int[10] { 4, 0, 5, 3, 2, 5, 6, 1, 5, 4 };
                change[2] = new int[10] { 3, 5, 0, 2, 5, 4, 3, 4, 2, 3 };
                change[3] = new int[10] { 3, 3, 2, 0, 3, 2, 3, 2, 2, 1 };
                change[4] = new int[10] { 4, 2, 5, 3, 0, 3, 4, 3, 3, 2 };
                change[5] = new int[10] { 3, 5, 4, 2, 3, 0, 1, 4, 2, 1 };
                change[6] = new int[10] { 2, 6, 3, 3, 4, 1, 0, 5, 1, 2 };
                change[7] = new int[10] { 3, 1, 4, 2, 3, 4, 5, 0, 4, 3 };
                change[8] = new int[10] { 1, 5, 2, 2, 3, 2, 1, 4, 0, 1 };
                change[9] = new int[10] { 2, 4, 3, 1, 2, 1, 2, 3, 1, 0 };
            }

            // find, 연산용 nums 채우기
            int[] find = new int[info[1]];
            {

                string temp = info[3].ToString();
                int calc = info[1] - temp.Length;
                for (int i = 0; i < temp.Length; i++)
                {

                    find[i + calc] = temp[i] - '0';
                }
            }

            // DFS 탐색!으로 채우자?
            // 완전 탐색
            int ret = DFS(change, find, info, 0);
            Console.WriteLine(ret - 1);
        }
        
        static int ArrToInt(int[] _arr)
        {

            int ret = 0;
            for (int i = 0; i < _arr.Length; i++)
            {

                ret = ret * 10 + _arr[i];
            }

            return ret;
        }

        static int DFS(int[][] _change, int[] _find, int[] _info, int _depth)
        {

            if (_info[1] == _depth) 
            {

                int f = ArrToInt(_find);
                if (f <= _info[0] && f > 0) return 1;
                return 0; 
            }

            int ret = 0;
            int save = _find[_depth];
            for (int i = 0; i < 10; i++)
            {

                int calc = _change[save][i];
                if (_info[2] - calc < 0) continue;

                _info[2] -= calc;
                _find[_depth] = i;
                ret += DFS(_change, _find, _info, _depth + 1);
                _info[2] += calc;
            }

            _find[_depth] = save;
            return ret;
        }
    }
}
