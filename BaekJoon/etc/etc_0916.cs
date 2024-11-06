using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 27
이름 : 배성훈
내용 : 🎵니가 싫어 싫어 너무 싫어 싫어 오지 마 내게 찝쩍대지마🎵 - 1
    문제번호 : 20440번

    정렬, 누적합, 좌표 압축 문제다
    정렬하고 투 포인터로 시뮬레이션 돌려 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0916
    {

        static void Main916(string[] args)
        {

            StreamReader sr;
            int n;

            int[] s, e;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(s);
                Array.Sort(e);

                int idx1 = 0, idx2 = 0;
                int cnt = 0;

                int ret1 = 0;
                int ret2 = 0, ret3 = 0;
                bool flag = false;
                int curTime = 0;

                while(idx1 <= n && idx2 <= n)
                {

                    // 시간 확인
                    int time = s[idx1] < e[idx2] ? s[idx1] : e[idx2];

                    // 시간 변동이 있으면 앞번 모기수 확인
                    if (curTime != time)
                    {

                        if (ret1 < cnt)
                        {

                            // 최대값 갱신된 경우
                            ret1 = cnt;
                            ret2 = curTime;

                            // 끝시간 확인용
                            flag = true;
                        }

                        if (cnt < ret1 && flag)
                        {

                            // 최대값의 끝 지점
                            ret3 = curTime;
                            flag = false;
                        }

                        // 시간 갱신
                        curTime = time;
                    }

                    // 인덱스 조절
                    if (s[idx1] <= e[idx2])
                    {

                        // 모기 추가
                        idx1++;
                        cnt++;
                    }
                    else
                    {

                        // 모기 제거
                        idx2++;
                        cnt--;
                    }
                }

                Console.Write($"{ret1}\n{ret2} {ret3}");
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                n = ReadInt();
                s = new int[n + 1];
                e = new int[n + 1];

                for (int i = 0; i < n; i++)
                {

                    s[i] = ReadInt();
                    e[i] = ReadInt();
                }

                s[n] = int.MaxValue;
                e[n] = int.MaxValue - 1;
                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// #include <iostream>
// #include <vector>
// #include <algorithm>
// #include <queue>

using namespace std;

int main()
{
	ios_base::sync_with_stdio(false);
	cin.tie(NULL);
	cout.tie(NULL);

	int iN;
	cin >> iN;

	vector<pair<int, int>> vecTime(iN, { 0,0 });
	for (int i = 0;i < iN;++i)
	{
		cin >> vecTime[i].first >> vecTime[i].second;
	}

	sort(vecTime.begin(), vecTime.end(), [](const pair<int, int>& o1, const pair<int, int>& o2)->bool
		{
			if (o1.first == o2.first) return o1.second > o2.second;
			return o1.first < o2.first;
		});

	int iAns = 0;
	int iAnsS = 0;
	int iAnsE = 0;

	priority_queue<int, vector<int>, greater<>> pq;

	for (int i = 0;i < iN;++i)
	{
		int iS = vecTime[i].first;
		int iE = vecTime[i].second;
		while (!pq.empty())
		{
			if (pq.top() > iS) break;
			pq.pop();
		}
		pq.push(iE);

		if (pq.size() > iAns)
		{
			iAns = pq.size();
			iAnsS = iS;
			iAnsE = pq.top();
		}
		else if (pq.size() == iAns)
		{
			if (iAnsE >= iS)
			{
				iAnsE = max(iAnsE, iE);
			}
		}

	}

	cout << iAns << "\n";
	cout << iAnsS << " " << iAnsE << "\n";
	return 0;
}
#endif
}
