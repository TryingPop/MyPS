using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 28
이름 : 배성훈
내용 : 암호문
    문제번호 : 2087번

    주된 아이디어는 중간에서 만나기와 비트마스킹이다
    반만 결과값을 저장하고, 반은 빼면서 결과를 찾는다
        2^n -> 2^(n/2) * 2

    결과를 키로, 선택 여부를 값으로 하는 Dictionary 자료구조를 이용했다
    그리고 결과가 존재하는 경우 선택 여부 값을 받아온다
    이후 이를 읽어서 출력했다

    그러니 176ms에 이상없이 풀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0123
    {

        static void Main123(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);

            int mid = len / 2;

            int[] left = new int[mid];
            int[] right = new int[len - mid];

            for (int i = 0; i < mid; i++)
            {

                left[i] = ReadInt(sr);
            }

            for (int i = 0; i < right.Length; i++)
            {

                right[i] = ReadInt(sr);
            }

            int find = ReadInt(sr);
            sr.Close();

            Dictionary<int, int> dic = new Dictionary<int, int>(1 << mid);

            // 왼쪽 합들 기록
            DFS(left, 0, 0, 0, dic);

            int[] ret = new int[2];
            // 오른쪽은 합하는데 기록이 있는지 확인하고, 있으면 해당 값 가져오기
            GetVal(right, 0, 0, 0, find, dic, ret);

            // 가져온 값으로 출력
            // 정답은 항상 존재!
            using (StreamWriter sw = new(Console.OpenStandardOutput()))
            {

                for (int i = 0; i < left.Length; i++)
                {

                    int calc = (1 << i) & ret[0];
                    if (calc == 0) sw.Write(0);
                    else sw.Write(1);
                }

                for (int i = 0; i < right.Length; i++)
                {

                    int calc = (1 << i) & ret[1];
                    if (calc == 0) sw.Write(0);
                    else sw.Write(1);
                }
            }
        }

        static void DFS(int[] _arr, int _curIdx, int _curVal, int _curSelect, Dictionary<int, int> _dic)
        {

            if (_curIdx == _arr.Length)
            {

                _dic[_curVal] = _curSelect;
                return;
            }

            DFS(_arr, _curIdx + 1, _curVal, _curSelect, _dic);
            DFS(_arr, _curIdx + 1, _curVal + _arr[_curIdx], _curSelect | (1 << _curIdx), _dic);
        }

        static void GetVal(int[] _arr, int _curIdx, int _curVal, int _curSelect, int _find, Dictionary<int, int> _dic, int[] _ret)
        {

            if (_curIdx == _arr.Length)
            {

                if (_dic.ContainsKey(_find - _curVal))
                {

                    _ret[0] = _dic[_find - _curVal];
                    _ret[1] = _curSelect;
                }

                return;
            }

            GetVal(_arr, _curIdx + 1, _curVal, _curSelect, _find, _dic, _ret);
            GetVal(_arr, _curIdx + 1, _curVal + _arr[_curIdx], _curSelect | (1 << _curIdx), _find, _dic, _ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
