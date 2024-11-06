using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 15
이름 : 배성훈
내용 : 최대 힙
    문제번호 : 11279번

    조건에 맞춰 직접 클래스 구현해서 만들어본 경우
    검색해보니 힙이라는 클래스는 완전 이진 트리라고 한다

    Pop 부분에서 바꿔주고 다시 부모랑 비교하는 연산을 안해줘서 많이 틀렸다
*/

namespace BaekJoon._29
{
    internal class _29_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StringBuilder sb = new StringBuilder();
            int len = int.Parse(sr.ReadLine());
            CustomData data = new CustomData(len);

            for (int i = 0; i < len; i++)
            {

                // 입력 받기
                int input = int.Parse(sr.ReadLine());

                if (input != 0) data.Push(input);
                else sb.AppendLine(data.Pop().ToString());
            }

            sr.Close();

            Console.Write(sb.ToString());
        }

        public class CustomData
        {

            int[] _array;

            int _count;

            public CustomData(int _size = 0)
            {

                if (_size < 0) _size = 0;

                // 사이즈를 2^n + 1의 형태로 만들자!
                int exp = (int)Math.Ceiling(Math.Log2(_size));
                _size = 1 << exp;
                // 계산 편의상 + 1 추가
                _array = new int[_size + 1];
                _count = 0;
            }

            // 값 추가
            public void Push(int _value)
            {

                // i번 째에 값 추가
                _array[++_count] = _value;

                // 부모노드와 값을 비교하면서 자기 위치 찾아간다
                int calc = _count;
                ChkUp(calc);
            }

            public int Pop()
            {

                // 없는 경우 조건대로 0 출력
                if (_count < 1) return 0;

                // 맨 위에 가장 큰 값이 있으므로 반환
                int result = _array[1];
                // 맨 위의 값을 0으로 하고 내려보낸다!
                _array[1] = 0;
                int calc = 1;

                // 가장 큰 값을 1번 인덱스로 가게 한다
                ChkDown(ref calc);
                // 0으로 설정한 값이 밑으로 도착했으므로 이것을 끝으로 옮긴다
                Swap(calc, _count--);
                // 바꾸는 과정에서 큰게 자식 노드에 있을 수 있으므로 올리는 연산을 한다
                ChkUp(calc);

                return result;
            }

            private void ChkUp(int _chk)
            {

                // 부모노드가 존재할 때까지
                while (_chk > 1)
                {

                    // 부모 노드와 비교
                    int parent = _chk / 2;

                    // 자식이 부모보다 큰 경우
                    if (_array[_chk] > _array[parent])
                    {

                        Swap(_chk, parent);
                        _chk = parent;
                    }
                    // 아닌 경우 종료
                    else break;
                }
            }

            /// <summary>
            /// 끝 값이 필요하기에 ref로 받아온다
            /// </summary>
            private void ChkDown(ref int _chk)
            {

                // 자식 노드가 존재할 때 까지
                while (_chk * 2 <= _count)
                {

                    // 자식 노드 중에 더 큰 애를 쓴다
                    // 여기서는 자연수를 받고 0보다 크기에 _count를 초과해도 이상 없다
                    int child = _array[_chk * 2] > _array[_chk * 2 + 1] ? _chk * 2 : _chk * 2 + 1;
                    // 해당 메서드는 내려 보낼 때만 쓰므로 비교할 필요가 없다
                    Swap(_chk, child);
                    _chk = child;
                }
            }

            private void Swap(int _idx1, int _idx2)
            {

                int temp = _array[_idx1];
                _array[_idx1] = _array[_idx2];
                _array[_idx2] = temp;
            }
        }
    }
}
