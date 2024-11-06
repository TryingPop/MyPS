using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 10
이름 : 배성훈
내용 : Egg
    문제번호 : 11012번

    주된 아이디어는 세그먼트 트리와 스위핑이다
    다행히도 세그먼트 트리는 직사각형 문제 덕분인지 몰라도 쉽게 보였다

    세그먼트 트리는 포함된 직사각형 y구간의 개수를 저장했다
    그냥 세그먼트 트리와는 결이 조금 다르다
    기존에는 부모로 값을 전달하는게 있다면, 직사각형처럼 부모에게 값을 전달하지 않는다
    그리고 값을 찾을 때에 자신의 점의 위치까지 가면서 사각형 구간들을 합쳐가게 했다
    현재 GetValue는 점에 대해서만 사각형의 개수를 찾을 수 있게 만들었다

    만약 범위를 주고 사각형을 찾으라고 한다면,
    다른 형태의 세그먼트 트리를 짰을거 같다!

    그리고 스위핑으로 직사각형의 x좌표와 점들의 좌표를 함께 정렬했다
    그리고 직사각형 높이부터 먼저 키거나 끄고 이후에 점 확인을 들어갔다
*/

namespace BaekJoon._47
{
    internal class _47_06
    {

        static void Main6(string[] args)
        {

            int MAX_POS = 10_000;
            int MAX_INTERVAL = 50_000;

            // 높이에 따른 세그먼트 트리
            // 처음에만 초기화해주면된다
            // 케이스 조사가 끝나면 자동으로 초기화된다
            int[] seg;
            {

                int log = (int)Math.Ceiling(Math.Log2(100_000 + 1)) + 1;
                seg = new int[1 << log];
            }

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());

            // 문제 수
            int test = ReadInt(sr);

            // 2가지 기능을하는 구조체
            // 투 포인트 알고리즘으로 하나의 기능만 하는 구조체로 풀 수도 있다
            FuncPos[] pos = new FuncPos[MAX_POS + 2 * MAX_INTERVAL];
            // 높이 구조체
            Height[] heights = new Height[MAX_INTERVAL];

            while (test-- > 0)
            {

                // 점들의 개수
                int posNum = ReadInt(sr);
                // 높이의 개수
                int heightNum = ReadInt(sr);

                // 점들을 입력받는다
                for (int i = 0; i < posNum; i++)
                {

                    int x = ReadInt(sr);
                    int y = ReadInt(sr);

                    // 점들 입력, x : x위치 , y : y위치, true : 점임을 알린다
                    pos[i].Set(x, y, true);
                }

                // 직사각형 입력
                for (int i = 0; i < heightNum; i++)
                {

                    // x의 범위 [minX, maxX], y의 범위 [minY, maxY]입력
                    // 그런데 x1 = minX, y1 = minY, y2 = maxY이고
                    // x2는 maxX + 1이다 사각형 개수를 줄이는 역할로 썼따
                    int x1 = ReadInt(sr);
                    int x2 = ReadInt(sr) + 1;
                    int y1 = ReadInt(sr);
                    int y2 = ReadInt(sr);

                    // x1 : 직사각형이 시작되는 x의 위치, i : 활성화 시킬 높이 i의 인덱스, false : 높이 좌표를 나타내는 구조체
                    pos[2 * i + posNum].Set(x1, i, false);
                    // x2 : 직사각형이 종료되는 x의 위치, i : 비활성화 시킬 높이 i의 인덱스(비활성화의 의미로 height 이상이 되게 했다), false : 높이 좌표를 나타내는 구조체
                    pos[2 * i + posNum + 1].Set(x2, i + heightNum, false);

                    // 높이 구간 세팅
                    heights[i].Set(y1, y2);
                }

                // pos의 전체 길이
                int len = posNum + 2 * heightNum;

                // x 오름차순으로 정렬한다
                // 그리고 x가 같은 경우 점 기능을 하는 구조체보다 높이 기능을 하는 구조체가 앞으로 온다
                HeapSort(pos, len);

                int result = 0;
                for (int i = 0; i < len; i++)
                {

                    // 점인 경우 사각형 개수 계산
                    if (pos[i].isPos) result += GetValue(seg, 0, 100_000, pos[i].funcInt);
                    else
                    {

                        // 높이 좌표인 경우
                        int idx = pos[i].funcInt;
                        // 활성화여부 판별해서 실행
                        if (idx < heightNum) Update(seg, 0, 100_000, heights[idx].minY, heights[idx].maxY, true);
                        else Update(seg, 0, 100_000, heights[idx - heightNum].minY, heights[idx - heightNum].maxY, false);
                    }
                }

                sw.WriteLine(result);
            }

            sr.Close();
            sw.Close();
        }

        static int ReadInt(StreamReader sr)
        {

            // 정수 1개 읽기
            // 양수만 읽는다
            int ret = 0;
            int c;

            while((c = sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret *= 10;
                ret += c - '0';
            }

            return ret;
        }

        static void HeapSort(FuncPos[] _arr, int _len)
        {

            // _arr 힙정렬
            for (int i = 1; i < _len; i++)
            {

                int chk = i;
                while (chk > 0)
                {

                    int p = (chk - 1) / 2;

                    if (_arr[chk].CompareTo(_arr[p]) > 0)
                    {

                        Swap(_arr, chk, p);
                        chk = p;
                    }
                    else break;
                }
            }

            Swap(_arr, 0, _len - 1);

            for (int i = _len - 2; i >= 1; i--)
            {

                int chk = 0;
                while (true)
                {

                    int l = chk * 2 + 1;
                    int r = chk * 2 + 2;

                    if (r <= i)
                    {

                        if (_arr[l].CompareTo(_arr[r]) > 0)
                        {

                            if (_arr[l].CompareTo(_arr[chk]) > 0)
                            {

                                Swap(_arr, chk, l);
                                chk = l;
                                continue;
                            }
                        }
                        else if (_arr[r].CompareTo(_arr[chk]) > 0)
                        { 
                            
                            Swap(_arr, chk, r);
                            chk = r;
                            continue;
                        }
                    }
                    else if (l <= i && _arr[l].CompareTo(_arr[chk]) > 0)
                    {

                        Swap(_arr, chk, l);
                        chk = l;
                        continue;
                    }

                    break;
                }

                Swap(_arr, 0, i);
            }
        }

        static void Swap(FuncPos[] _arr, int _idx1, int _idx2)
        {

            var temp = _arr[_idx1];
            _arr[_idx1] = _arr[_idx2];
            _arr[_idx2] = temp;
        }

        static void Update(int[] _seg, int _start, int _end, int _chkStart, int _chkEnd, bool _active, int _idx = 1)
        {

            // 활성화된 직사각형 개수 담기
            // 높이 구간이 포함되면 카운트 1 추가
            if (_start >= _chkStart && _end <= _chkEnd)
            {

                _seg[_idx - 1] += _active ? 1 : -1;
                return;
            }
            else if (_start > _chkEnd || _end < _chkStart) return;

            int mid = (_start + _end) / 2;

            Update(_seg, _start, mid, _chkStart, _chkEnd, _active, _idx * 2);
            Update(_seg, mid + 1, _end, _chkStart, _chkEnd, _active, _idx * 2 + 1);
        }

        static int GetValue(int[] _seg, int _start, int _end, int _chkIdx, int _idx = 1)
        {

            // 활성화된 직사각형 개수 찾기
            // 점으로만 활성화된 직사각형 개수를 찾을 수 있다
            // 구간으로 찾으려면 활성화된 직사각형을 찾으려면 다른 형태의 세그먼트 트리가 필요하다!
            if (_start == _end)
            {

                return _seg[_idx - 1];
            }

            int mid = (_start + _end) / 2;
            int result;
            if (_chkIdx > mid) result = GetValue(_seg, mid + 1, _end, _chkIdx, _idx * 2 + 1);
            else result = GetValue(_seg, _start, mid, _chkIdx, _idx * 2);

            result += _seg[_idx - 1];
            return result;
        }

        struct FuncPos : IComparable<FuncPos>
        {

            /// 
            /// 지양해야할 구조체
            /// 구조체를 2개로 나누는게 의미가 더 좋아보인다(해당 경우 투 포인트 알고리즘을 써야한다)
            /// (클래스면 상속으로 해결 가능하지만, 배열에서 계속 new를 하면서 채워야한다)
            /// 
            /// 구조체를 나누면 정렬을 2번해야하는데, 1번되게 했다
            /// 기능을 합칠 시 결과 코드가 간략하게? 보인다
            /// 투 포인트면 l(좌표의 x), r(직사각형의 x)매번 비교해줘야한다
            ///

            // x 좌표
            public int x;
            // 해당 bool 변수에 따라
            public bool isPos;
            // y좌표가 될수도 있고, height을 가리키는 idx가 될 수도 있다
            public int funcInt;

            public void Set(int _x, int _funcInt, bool _isPos)
            {

                x = _x;
                funcInt = _funcInt;
                isPos = _isPos;
            }

            public int CompareTo(FuncPos _other)
            {

                // x 오름차순을 최우선으로
                int ret = x.CompareTo(_other.x);
                if (ret == 0)
                {

                    // x가 같으면 좌표들을 뒤로가게 한다
                    if (isPos == _other.isPos) ret = 0;
                    else ret = isPos ? 1 : -1;
                }

                return ret;
            }
        }

        struct Height
        {

            // 높이 y의 min과 max를 보유
            public int minY;
            public int maxY;

            public void Set(int _minY, int _maxY)
            {

                minY = _minY;
                maxY = _maxY;
            }
        }
    }
}
