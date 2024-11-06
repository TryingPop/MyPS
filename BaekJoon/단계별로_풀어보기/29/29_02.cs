using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 15
이름 : 배성훈
내용 : 최소 힙
    문제번호 : 1927번
*/

namespace BaekJoon._29
{
    internal class _29_02
    {

        static void Main2(string[] args)
        {

            StringBuilder sb = new StringBuilder();

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int len = int.Parse(sr.ReadLine());

            CustomData data = new CustomData(len);

            for (int i = 0; i < len; i++)
            {

                int num = int.Parse(sr.ReadLine());

                if (num != 0) data.Push(num);
                else sb.AppendLine(data.Pop().ToString());
            }

            sr.Close();

            // 출력
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())); ;

            sw.Write(sb);
            sw.Close();
        }

        public class CustomData
        {

            int[] _array;
            int _count;

            public CustomData(int _size)
            {

                if (_size < 0) _size = 0;

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

                    if (_array[parent] > _array[_chk])
                    {

                        Swap(parent, _chk);
                        _chk = parent;
                    }
                    else break;
                }
            }

            private void ChkDown(ref int _chk)
            {

                while(_chk * 2 <= _count)
                {

                    int child = 2 * _chk;
                    // 0인 경우는 _count 보다 큰 경우와 같다!
                    if (_array[child + 1] != 0) child = _array[child] < _array[child + 1] ? child : child + 1;

                    Swap(child, _chk);
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
