using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 9
이름 : 배성훈
내용 : 직사각형
    문제번호 : 7626번

    주된 아이디어는 다음과 같다 구분구적법, 좌표압축, 스위핑 알고리즘, 세그먼트 트리이다

    구분구적법 아이디어이므로 시작과 끝 점을 모아둔 x좌표에 대한 정렬이 필요하다 (y로 잡아도 상관없다!)
    해당 부분에서 스위핑 알고리즘이 작용한다

    이제 각 x에 따른 높이 y값들을 구해야한다
    여기서도 그냥 스위핑으로할 수 있다 (y의 시작값 종료값으로 길이를 추가하면 된다)
    스위핑으로 해결할 시 O(N)의 시간이 소요된다(실상은 2N)

    해당 시간을 logN으로 줄이기 위해 세그먼트 트리로 이용해야한다
    여기까지는 왔는데 이게 맞나하는 의문가 해당 x에 맞는 높이를 세그먼트 트리로 어떻게 만들지 몰라서 한참을 고민하고 
    결국 다른 사람 아이디어를 찾아봤다

    찾아보니 다행히도 추론은 틀리지 않았다 
    높이를 만드는데 있어 y를 구간으로 나누어 주었다
    여기서 좌표압축 알고리즘이 사용된다

    활성화된 구간을 세그먼트 트리 형태로 기록하고,
    활성화된 구간을 통해 높이도 노드를 통해 얻어 내게 해야한다

    활성화된 구간 기록은 카운트로 기록했다
    해당 구간이 통으로 포함되면 카운트 기록하고 해당 노드의 자식노드들에게는 기록하지 않는다
    그리고 부모노드에게는 카운트를 기록하지 않는다
    해당 기록 방법에 의해 구간 기록과 높이 계산을 분리하면 높이 계산에서 카운트가 기록되었는지 모든 노드들을 조사해야한다 
    그래서 O(N)의 시간이 되어버린다 ( 2N <= seg.Length <= 4N)

    그래서 높이 계산도 기록과 동시에 함께 했다
    구간 기록은 부모에게 넘겨주지 않지만, 높이 계산값은 부모에게 계승하게 한다

    만약 카운트가 기록된 구간이면, 자식 상관없이 높이는 해당 구간의 전체 길이로 한다
    카운트가 안된 구간이면 자식 구간들의 합이 된다
    기록과 동시에 진행하기에 아무리 많아도 높이 계산은 8 * logN개의 노드를 넘지 못한다

    그래서 아래는 해당 방법으로 푼 코드이다
    시간은 784ms이다
*/

namespace BaekJoon._47
{
    internal class _47_05
    {

        static void Main5(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = PlusInt(sr);
            int[] orderY = new int[2 * len];
            // y구간 찾을 때 쓸 연산용
            Dictionary<int, int> idxs = new Dictionary<int, int>(2 * len);

            // 높이 값들 저장 min max로 찾는다
            Height[] heights = new Height[len];
            // x좌표와 높이 인덱스 저장
            // 높이 인덱스는 len보다 큰 경우 직사각형의 오른쪽 x값이므로 해당 높이 구간을 제거 해야함을 의미 한다
            // len보다 작은 경우는 직사각형의 왼쪽 x 값이므로 높이를 추가해야함을 의미한다
            PosX[] pos = new PosX[len * 2];
            
            for (int i = 0;  i < len; i++)
            {

                // 여기서 입력되는 좌표는 음이아닌 정수다!
                int x1 = PlusInt(sr);
                int x2 = PlusInt(sr);
                int y1 = PlusInt(sr);
                int y2 = PlusInt(sr);

                // min max 저장
                heights[i].Set(y1, y2);

                // 좌표 압축용!
                orderY[i] = y1;
                orderY[i + len] = y2;

                // x 좌표와 높이 가리키는 i저장
                pos[i].Set(x1, i);
                // len 보다 큰 것은 뺄 용도다!
                pos[i + len].Set(x2, i + len);
            }
            sr.Close();

            // 좌표압축
            Array.Sort(orderY);
            // 서로 다른 y의 최대 개수
            int maxIdxY = 0;
            for (int i = 0; i < orderY.Length; i++)
            {

                // 오름차순으로 넣는다
                int curY = orderY[i];
                // 이미 있는 경우 중복인덱스 방지!
                if (!idxs.ContainsKey(curY))
                {

                    // 기존 높이 값으로 정렬된 y의 인덱스 기록
                    // orderY의 값과 인덱스가 바꼈다고 보면 된다
                    // 범위가 10억이기에 배열로는 안되어서 딕셔너리를 썼다
                    idxs.Add(curY, maxIdxY);

                    // maxIdx에 맞게 orderY도 갱신해준다!
                    orderY[maxIdxY] = curY;
                    maxIdxY++;
                }
            }

            for (int i = 0; i < len; i++)
            {

                // 이제 처음 높이 값들을 orderY의 인덱스로 바꿔준다
                int minY = heights[i].minIdx;
                int maxY = heights[i].maxIdx;

                heights[i].minIdx = idxs[minY];
                heights[i].maxIdx = idxs[maxY];
            }

            // 안쓰니 클리어하고 null로 하려고 했는데
            // 그냥 클리어만 했다;
            idxs.Clear();
            
            // pos를 x축으로 정렬!
            Array.Sort(pos);

            // 세그먼트 트리 만들기!
            // y 구간이기에 1구간에 2점이 이용되므로
            // 정렬된 서로 다른 y의 개수 - 1개가 구간이 된다
            int log = (int)Math.Ceiling(Math.Log2(maxIdxY - 1)) + 1;
            (int value, int cnt)[] seg = new (int value, int cnt)[1 << log];

            // 해당 x값에 많은 직사각형 왼쪽 값이나 오른쪽값이 몰려 있는 경우
            // 높이 연산을 모두한 최종 결과가 문제에서 요구하는 높이가 될 것이다
            int preX = pos[0].x;
            // 가로 길이
            long width = 0;

            long result = 0;
            for (int i = 0; i < pos.Length; i++)
            {

                // 기록된 x값이 바뀌는 경우
                // 앞번에 높이 값이 최종 높이임을 의미!
                if (preX != pos[i].x) 
                {

                    // 가로 길이 설정!
                    width = pos[i].x - preX;
                    // 구분구적법 사각형 면적 추가
                    result += width * seg[0].value;
                    // 그리고 이전 높이 갱신
                    preX = pos[i].x;
                }

                // 높이 인덱스
                int idx = pos[i].heightIdx;

                // 추가하는 경우인지
                if (idx < len) Update(seg, orderY, 1, maxIdxY - 1, heights[idx].minIdx + 1, heights[idx].maxIdx, true);
                // 빼는 경우인지
                else Update(seg, orderY, 1, maxIdxY - 1, heights[idx - len].minIdx + 1, heights[idx - len].maxIdx, false);
            }

            Console.WriteLine(result);
        }

        static int PlusInt(StreamReader _sr)
        {

            ///
            /// 양수 입력값 하나씩 받아오는 함수
            ///
            int n = 0;
            int c;

            while ((c = _sr.Read()) != '\n' && c != -1 && c != ' ') 
            {

                if (c == '\r') continue;
                n *= 10;
                n += c - '0';
            }

            return n;
        }

        struct Height
        {

            // 높이 구조체 
            // 초기에는 minY값, maxY값이 담기다가
            // orderY가 완성되면 orderY의 인덱스로 쓰인다
            public int minIdx;
            public int maxIdx;

            public void Set(int _min, int _max)
            {

                minIdx = _min;
                maxIdx = _max;
            }
        }

        struct PosX : IComparable<PosX>
        {

            // 현재 x 위치
            public int x;
            // 가리키는 높이 인덱스
            // orderY가 아닌 Height 구조체 배열의 인덱스이다!
            public int heightIdx;

            public void Set(int _x, int _heightIdx)
            {

                x = _x;
                heightIdx = _heightIdx;
            }

            // 세그먼트 트리 계산에 사용하는 정렬 규칙
            // x오름차순
            public int CompareTo(PosX other)
            {

                return x.CompareTo(other.x);
            }
        }


        static void Update((int value, int cnt)[] _seg, int[] _orderY, int _start, int _end, int _chkStart, int _chkEnd, bool _active, int _idx = 1)
        {

            if (_start >= _chkStart && _end <= _chkEnd)
            {

                // 활성화 시 해당 구간만 카운트 1 추가
                // 비활성화는 해당 구간만 카운트 1감소
                _seg[_idx - 1].cnt += _active ? 1 : -1;
                
                if (_seg[_idx - 1].cnt > 0)
                {

                    // 카운트가 양수면 구간 전체 길이
                    _seg[_idx - 1].value = _orderY[_end] - _orderY[_start - 1];
                }
                else
                {

                    // 카운트가 0이면 자식 존재하는지 판별하고, 자식의 높이 값계승
                    if (2 * _idx < _seg.Length)
                    {

                        // 오른쪽 왼쪽 둘 다 존재
                        _seg[_idx - 1].value = _seg[_idx * 2 - 1].value + _seg[_idx * 2].value;
                    }
                    else if (2 * _idx - 1 < _seg.Length)
                    {

                        // 왼쪽만 존재
                        _seg[_idx - 1].value = _seg[_idx * 2 - 1].value;
                    }
                    // 자식 없음
                    else _seg[_idx - 1].value = 0;
                    
                }

                return;
            }
            // 범위 벗어나면 탈출
            else if (_end < _chkStart || _chkEnd < _start) return; 

            int mid = (_start + _end) / 2;
            Update(_seg, _orderY, mid + 1, _end, _chkStart, _chkEnd, _active, _idx * 2 + 1);
            Update(_seg, _orderY, _start, mid, _chkStart, _chkEnd, _active, _idx * 2);

            // 카운트가 1 이상이면 자식 연산안한다
            if (_seg[_idx - 1].cnt > 0) return;
            // 카운트가 0이면 자식값을 계승하게 한다!
            _seg[_idx - 1].value = _seg[2 * _idx - 1].value + _seg[2 * _idx].value;
        }

    }
}
