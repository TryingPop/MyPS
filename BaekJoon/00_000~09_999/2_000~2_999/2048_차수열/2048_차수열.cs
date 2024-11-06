using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 5
이름 : 배성훈
내용 : 차수열
    문제번호 : 2084번

    그래프 이론, 그리디 알고리즘, 해 구성하기, 차수열 문제다
    먼저 간선은 서로 연결되므로 간선의 총합은 항상 짝수여야 한다.
    그래서 짝수 검증을 먼저했다.

    이후 남은 간선이 많은거끼리 이으면 되지 그래프가 되지 않을까 추론했고 
    중간에 이을 수 없는 경우 불가능하다고 판별하면서 제출하니 통과했다.
    
    해당 사이트에 차수열의 존재성 증명 내용이 있다. 
    https://gazelle-and-cs.tistory.com/42
*/

namespace BaekJoon.etc
{
    internal class etc_1094
    {

        static void Main1094(string[] args)
        {

            StreamReader sr;
            int n;
            (int cnt, int idx)[] arr;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int sum = 0;
                for (int i = 0; i < n; i++)
                {

                    sum += arr[i].cnt;
                }

                if ((sum & 1) == 1)
                {

                    Impossible();
                    return;
                }

                bool[][] ret = new bool[n][];
                for (int i = 0; i < n; i++)
                {

                    ret[i] = new bool[n];
                }

                int front = 0;
                while (front < n)
                {

                    Array.Sort(arr, front, n - front);
                    if (arr[n - 1].cnt == 0) break;
                    while (arr[front].cnt == 0) front++;

                    int idx1 = arr[n - 1].idx;
                    for (int j = n - 2; j >= front; j--)
                    {

                        int idx2 = arr[j].idx;
                        if (ret[idx1][idx2]) continue;
                        else if (arr[j].cnt == 0) break;
                        ret[idx1][idx2] = true;
                        ret[idx2][idx1] = true;

                        arr[n - 1].cnt--;
                        arr[j].cnt--;
                        if (arr[n - 1].cnt == 0) break;
                    }

                    if (arr[n - 1].cnt > 0)
                    {

                        Impossible();
                        return;
                    }
                }

                Possible();

                void Possible()
                {

                    using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                    {

                        for (int i = 0; i < n; i++)
                        {

                            for (int j = 0; j < n; j++)
                            {

                                if (j != 0) sw.Write(' ');
                                if (ret[i][j]) sw.Write(1);
                                else sw.Write(0);
                            }

                            sw.Write('\n');
                        }
                    }
                }

                void Impossible()
                {

                    Console.Write(-1);
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new (int cnt, int idx)[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = (ReadInt(), i);
                }

                sr.Close();
                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == ' ' || c == '\n') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }

#if other
// #include <iostream>
// #include <algorithm>
// #include <vector>


using namespace std;

bool comparator(pair<int, int> a, pair<int, int> b)
{
	return (a.first > b.first);
}

void solve(vector<pair<int, int>> &seq)
{
	int n = seq.size();
	vector<vector<bool>> graph(n, vector<bool>(n, false));

	while(true)
	{
		sort(seq.begin(), seq.end(), comparator);
		while(seq.size() > 0 && seq.back().first == 0)
			seq.erase(seq.end() - 1);

		if(seq.size() == 0)
			break;

		if(seq[0].first <= seq.size() - 1)
		{
			int s = seq[0].second;
			for(int i = 1; i <= seq[0].first; i++)
			{
				int t = seq[i].second;
				graph[s][t] = true;
				graph[t][s] = true;
				seq[i].first--;
			}
			seq[0].first = 0;
		}
		else
		{
			cout << -1 << endl;
			return;
		}
	}

	for(int r = 0; r < n; r++)
	{
		for(int c = 0; c < n; c++)
		{
			if(graph[r][c] == true)
				cout << "1 ";
			else
				cout << "0 ";
		}
		cout << endl;
	}
}

int main()
{
	std::ios::sync_with_stdio(false);

	int n, k;
	vector<pair<int, int>> seq;

	cin >> n;
	for(int i = 0; i < n; i++)
	{
		cin >> k;
		seq.push_back(make_pair(k, i));
	}

	solve(seq);

	return 0;
}

#endif
}
