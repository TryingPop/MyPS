using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

/*
날짜 : 2023. 7. 18
이름 : 배성훈
내용 : 덱(Deque)
    문제번호 : 10866번
*/

namespace BaekJoon._20
{
    internal class _20_05
    {

        public class Dequeue<T>
        {

            private T[] _array;
            private int _size = 0;      // _array안의 원소 갯수
            

            public Dequeue(int _capacity = 1) 
            {

                _array = new T[_capacity];
            }

            public void PushBack(T item)
            {

                if (_size < _array.Length)
                {

                    _array[_size++] = item;
                }
                else
                {

                    T[] temp = new T[_size + 1];
                    Array.Copy(_array, 0, temp, 0, _size);
                    _array = temp;
                    _array[_size] = item;
                    _size++;
                }
            }

            public void PushFront(T item)
            {

                if (_size < _array.Length)
                {

                    for (int i = _size; i > 0; i--)
                    {

                        _array[i] = _array[i - 1];
                    }

                    _array[0] = item;
                    _size++;
                }
                else
                {

                    T[] temp = new T[_size + 1];
                    Array.Copy(_array, 0, temp, 1, _size);
                    
                    _array[0] = item;
                    _size++;
                }
            }

            public T PopBack()
            {

                return _array[--_size];
            }

            public T PopFront()
            {

                T _temp = _array[0];
                for (int i = 0; i < _size; i++)
                {

                    _array[i] = _array[i + 1];
                }

                _size--;

                return _temp;
            }

            public T Front()
            {

                return _array[0];
            }

            public T Back()
            {

               
                return _array[_size - 1];
            }

            public int Count { get { return _size; } }

            public void Print()
            {

                for (int i = 0; i < _size; i++)
                {

                    Console.WriteLine(_array[i]);
                }
            }
        }

        static void Main5(string[] args)
        {

            int len = int.Parse(Console.ReadLine());

            Dequeue<int> deque = new Dequeue<int>(len);

            StringBuilder sb = new StringBuilder();


            for (int i = 0; i < len; i++)
            {

                string[] inputs = Console.ReadLine().Split(' ');

                if (inputs[0] == "push_back")
                {

                    deque.PushBack(int.Parse(inputs[1]));
                }
                else if (inputs[0] == "push_front")
                {

                    deque.PushFront(int.Parse(inputs[1]));
                }
                else if (inputs[0] == "pop_front")
                {

                    if (deque.Count > 0)
                    {

                        sb.AppendLine(deque.PopFront().ToString());
                    }
                    else
                    {

                        sb.AppendLine("-1");
                    }
                }
                else if (inputs[0] == "pop_back")
                {

                    if (deque.Count > 0)
                    {

                        sb.AppendLine(deque.PopBack().ToString());
                    }
                    else
                    {

                        sb.AppendLine("-1");
                    }
                }
                else if (inputs[0] == "size")
                {

                    sb.AppendLine(deque.Count.ToString());
                }
                else if (inputs[0] == "empty")
                {

                    if (deque.Count > 0)
                    {

                        sb.AppendLine("0");
                    }
                    else
                    {

                        sb.AppendLine("1");
                    }
                }
                else if (inputs[0] == "front")
                {

                    if (deque.Count > 0)
                    {

                        sb.AppendLine(deque.Front().ToString());
                    }
                    else
                    {

                        sb.AppendLine("-1");
                    }
                }
                else if (inputs[0] == "back")
                {

                    if (deque.Count > 0)
                    {

                        sb.AppendLine(deque.Back().ToString());
                    }
                    else
                    {

                        sb.AppendLine("-1");
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
                sw.WriteLine(sb);
        }
    }
}
