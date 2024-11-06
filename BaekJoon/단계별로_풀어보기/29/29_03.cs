using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 15
이름 : 배성훈
내용 : 절댓값 힙
    문제번호 : 11286번

    29_01 ~ 29_03 까지 비교하는 방법만 다를 뿐 전부다 매우 유사한 로직
*/

namespace BaekJoon._29
{
    internal class _29_03
    {

        static void Main3(string[] args)
        {

            StringBuilder sb = new StringBuilder();

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());
            CustomData data = new CustomData(len);

            for (int i = 0; i < len; i++)
            {

                int num = int.Parse(sr.ReadLine());

                if (num == 0) sb.AppendLine(data.Pop().ToString());
                else data.Push(num);
            }

            sr.Close();

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.Write(sb);
            }
        }

        public class CustomData
        {

            int[] _array;
            int _count;

            public CustomData(int _size)
            {

                int exp = (int)Math.Ceiling(Math.Log2(_size));
                int size = 1 << exp;

                _array = new int[size + 1];
                _count = 0;
            }

            public void Push(int _value)
            {

                _array[++_count] = _value;

                ChkUp(_count);
            }

            public int Pop()
            {

                if (_count < 1) return 0;

                int result = _array[1];
                _array[1] = 0;
                int calc = 1;
                ChkDown(ref calc);
                Swap(calc, _count--);
                if (_array[calc] != 0) ChkUp(calc);

                return result;
            }

            private void ChkUp(int _chk)
            {

                while (_chk > 1)
                {

                    int parent = _chk / 2;

                    if (AisSmallerThanB(_array[_chk], _array[parent]))
                    {

                        Swap(_chk, parent);
                        _chk = parent;
                    }
                    else break;
                }
            }

            private void ChkDown(ref int _chk)
            {

                while (_chk * 2 <= _count)
                {

                    int child = _chk * 2;

                    if (_array[child + 1] != 0) child = AisSmallerThanB(_array[child], _array[child + 1]) ? child : child + 1;

                    Swap(_chk, child);
                    _chk = child;
                }
            }

            private bool AisSmallerThanB(int _a, int _b)
            {

                // 29_02번에서 비교 방식만 추가!
                int absA = _a < 0 ? -_a : _a;
                int absB = _b < 0 ? -_b : _b;

                if (absA < absB) return true;
                if (absB < absA) return false;

                return _a < _b;
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
