using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 22
이름 : 배성훈
내용 : 모든 스택 수열
    문제번호 : 23284번

    브루트포스, 스택 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1724
    {

        static void Main1724(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            int[][] stk = new int[n + 1][];
            int[][] arr = new int[n + 1][];
            for (int i = 0; i <= n; i++)
            {

                stk[i] = new int[n + 1];
                arr[i] = new int[n + 1];
            }

            List<int[]> ret = new();
            DFS(1, 0);

            ret.Sort((x, y) =>
            {

                for (int i = 0; i < n; i++) 
                {

                    if (x[i] != y[i]) return x[i].CompareTo(y[i]);
                }

                return 0;
            });

            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            for (int i = 0; i < ret.Count; i++)
            {

                for (int j = 0; j < n; j++)
                {

                    sw.Write($"{ret[i][j]} ");
                }

                sw.Write('\n');
            }

            void DFS(int _dep, int _len)
            {

                if (_dep > n)
                {

                    int idx = stk[n][0];
                    int[] newArr = New(arr[n], _len);

                    while (idx > 0)
                    {

                        newArr[_len++] = stk[n][idx--];
                    }

                    ret.Add(newArr);
                    return;
                }

                SetPrevStk(_dep);
                SetPrevArr(_dep, _len);

                Push(_dep, _dep);
                DFS(_dep + 1, _len);

                int len = stk[_dep][0] - 1;
                for (int i = len; i > 0; i--)
                {

                    arr[_dep][_len++] = stk[_dep][i];
                    stk[_dep][i] = _dep;
                    stk[_dep][0] = i;

                    int[] newArr = New(arr[_dep], _len);

                    DFS(_dep + 1, _len);
                }
            }

            int[] New(int[] _arr, int _len)
            {

                int[] ret = new int[n];
                for (int i = 0; i < _len; i++)
                {

                    ret[i] = _arr[i];
                }

                return ret;
            }

            void SetPrevArr(int _idx, int _len)
            {

                for (int i = 0; i < _len; i++)
                {

                    arr[_idx][i] = arr[_idx - 1][i];
                }
            }

            void SetPrevStk(int _idx)
            {

                int len = stk[_idx - 1][0];
                for (int i = 0; i <= len; i++)
                {

                    stk[_idx][i] = stk[_idx - 1][i];
                }
            }

            void Push(int _idx, int _val)
            {

                int push = ++stk[_idx][0];
                stk[_idx][push] = _val;
            }
        }
    }

#if other
// #include <iostream>
// #include <string>
// #include <stack>
using namespace std;

int n;
stack<int> RBS_simulator;
string Seq, Ans;
void gen_RBS(int i, int num_pushed) {
	if (i == 2 * n) {
		Ans += Seq;
		Ans.back() = '\n';
	}
	else {
		if (!RBS_simulator.empty()) {
			int len_memo = Seq.length(), num_top = RBS_simulator.top();
			RBS_simulator.pop();
			Seq += to_string(num_top) + ' ';
			gen_RBS(i + 1, num_pushed);
			RBS_simulator.push(num_top);
			Seq.resize(len_memo);
		}
		if (RBS_simulator.size() < 2 * n - i) {
			RBS_simulator.push(++num_pushed);
			gen_RBS(i + 1, num_pushed);
			RBS_simulator.pop();
		}
	}
}

int main()
{
	cin >> n;
	gen_RBS(0, 0);
	cout << Ans;
}
#endif
}
