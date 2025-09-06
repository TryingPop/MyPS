using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 19
이름 : 배성훈
내용 : Custom table sorter
    문제번호 : 2202번

    구현, 문자열, 정렬, 파싱 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1830
    {

        static void Main1830(string[] args)
        {

#if first
            string END_INPUT = "#";
            string END_QUERY = "0#";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, m;
            (string[] strs, int idx)[] arr = new (string[] strs, int idx)[20];
            int[] key = new int[20];
            bool[] desc = new bool[20];

            while (Input())
            {

                string temp;
                while ((temp = sr.ReadLine()) != END_QUERY)
                {

                    SetArr(temp);

                    Query();
                }
            }

            void SetArr(string _str)
            {

                string[] temp = _str.Split(',');
                m = temp.Length;
                for (int i = 0; i < m; i++)
                {

                    string cur = temp[i];
                    int num = 0;
                    for (int j = 0; j < cur.Length - 1; j++)
                    {

                        num = num * 10 + cur[j] - '0';
                    }

                    key[i] = num - 1;
                    desc[i] = cur[cur.Length - 1] == 'D';
                }
            }

            void Query()
            {

                Comparer<(string[] strs, int idx)> comp = Comparer<(string[] strs, int idx)>.Create((x, y) => MyComp(x, y));
                Array.Sort(arr, 0, n, comp);

                for (int i = 0; i < n; i++)
                {

                    sw.Write("  ");
                    for (int j = 0; j < arr[i].strs.Length; j++)
                    {

                        if (j != 0) sw.Write(',');
                        sw.Write(arr[i].strs[j]);
                    }

                    sw.Write('\n');
                }

                sw.Write('\n');
            }

            bool Input()
            {

                string title = sr.ReadLine();
                n = 0;
                if (title == END_INPUT) return false;

                sw.WriteLine(title);

                string temp;

                while ((temp = sr.ReadLine())[0] != '#')
                {

                    arr[n] = (temp.Split(','), n);
                    n++;
                }

                return true;
            }

            int MyComp((string[] strs, int idx) _f, (string[] strs, int idx) _t, int _idx = 0)
            {

                if (_idx == m) return _f.idx.CompareTo(_t.idx);

                int ret = _f.strs[key[_idx]].CompareTo(_t.strs[key[_idx]]);
                if (desc[_idx]) ret = -ret;

                if (ret == 0) ret = MyComp(_f, _t, _idx + 1);
                return ret;
            }

#else

            string END_INPUT = "#";
            string END_QUERY = "0#";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, m;
            (string[] strs, int idx)[] arr = new (string[] strs, int idx)[20];
            int[] key = new int[20];
            bool[] desc = new bool[20];

            while (Input())
            {

                string temp;
                while ((temp = sr.ReadLine()) != END_QUERY)
                {

                    SetArr(temp);

                    Query();
                }
            }

            void SetArr(string _str)
            {

                string[] temp = _str.Split(',');
                m = temp.Length;
                for (int i = 0; i < m; i++)
                {

                    string cur = temp[i];
                    int num = 0;
                    for (int j = 0; j < cur.Length - 1; j++)
                    {

                        num = num * 10 + cur[j] - '0';
                    }

                    key[i] = num - 1;
                    desc[i] = cur[cur.Length - 1] == 'D';
                }
            }

            void Query()
            {

                MySort();

                for (int i = 0; i < n; i++)
                {

                    sw.Write("  ");
                    for (int j = 0; j < arr[i].strs.Length; j++)
                    {

                        if (j != 0) sw.Write(',');
                        sw.Write(arr[i].strs[j]);
                    }

                    sw.Write('\n');
                }

                sw.Write('\n');
            }

            bool Input()
            {

                string title = sr.ReadLine();
                n = 0;
                if (title == END_INPUT) return false;

                sw.WriteLine(title);

                string temp;

                while ((temp = sr.ReadLine())[0] != '#')
                {

                    arr[n] = (temp.Split(','), n);
                    n++;
                }

                return true;
            }

            void MySort()
            {

                for (int i = 1; i < n; i++)
                {

                    Up(i);
                }

                for (int i = n - 1; i > 0; i--)
                {

                    Swap(0, i);
                    Down(i);
                }

                void Down(int _end)
                {

                    int p = 0;
                    while (p < _end)
                    {

                        int l = p * 2 + 1;
                        int r = p * 2 + 2;

                        if (_end <= l) break;

                        int max;

                        if (r == _end) max = l;
                        else max = MyComp(arr[l], arr[r]) > 0 ? l : r;

                        int ret = MyComp(arr[p], arr[max]);
                        if (ret > 0) break;

                        Swap(p, max);
                        p = max;
                    }
                }

                void Up(int _idx)
                {

                    while (_idx > 0)
                    {

                        int p = (_idx - 1) / 2;
                        int ret = MyComp(arr[p], arr[_idx]);
                        if (ret > 0) break;
                        Swap(p, _idx);
                        _idx = p;
                    }
                }

                void Swap(int _f, int _t)
                {

                    var temp = arr[_f];
                    arr[_f] = arr[_t];
                    arr[_t] = temp;
                }
            }

            int MyComp((string[] strs, int idx) _f, (string[] strs, int idx) _t, int _idx = 0)
            {

                if (_idx == m) return _f.idx.CompareTo(_t.idx);

                int ret = _f.strs[key[_idx]].CompareTo(_t.strs[key[_idx]]);
                if (desc[_idx]) ret = -ret;

                if (ret == 0) ret = MyComp(_f, _t, _idx + 1);
                return ret;
            }
#endif
        }
    }
}
