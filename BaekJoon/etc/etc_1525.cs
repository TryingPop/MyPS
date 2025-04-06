using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 6
이름 : 배성훈
내용 : 팀 선발
    문제번호 : 1044번

    비트마스킹, 중간에서 만나기 문제다.
    팀 인원이 같아야 한다.
    해당 부분 설명이 없고 예제로 확인했다;

    아이디어는 다음과 같다.
    단순 브루트포스로 할 경우 2^36 > 64억이므로 시간 초과 난다.
    2^18 < 30만으로 구간을 반으로 나눠 모든 경우를 조사하는 것은 시도해볼만하다.
    
    먼저 절반의 구간으로 나눠서 인덱스가 작은 팀을 모든 경우를 조사하면서 
    Dictionary 1팀의 총합 - 2팀 총합을 저장한다.
    그런데 팀의 인원은 절반이 되어야 하므로 A팀의 인원에 따라 다른 dictionary에 저장한다.
    dictionary에 저장할 때 key를 점수 차이로, val를 팀 선택 상태 state를 저장한다.


    만약 이미 보유한 키라면 state값이 사전 순으로 앞서게 해줘야한다.
    state를 비트마스킹으로 하되 1팀이면 0, 2팀이면 1로 하고 앞의 인덱스가 비교적 큰 값을 나타내게 하면
    state값이 작은게 사전순으로 앞서게된다.
    그래서 state를 n - 1 부터 1자리씩 채워 넣는다.

    이후 뒷 팀을 조사하는데, b팀의 선택횟수를 확인한다.
    이후 A팀의 인원만큼 B팀이 정해지면 A, B를 같은 인원으로 갖는것과 마찬가지다.

    이후 dictionary에서 팀 차이가 최소로 하는 값을 찾아야 한다.
    이는 dictionary의 key에 차가 0에 가까운 것을 찾는것과 같다.
    dictionary 자체에서 해결하기 힘들어 따로 list를 만들어 값들으 다시 저장한 뒤 정렬했다.
    이후 점수차에 대한 이분 탐색으로 점수 합이 0 보다 작은 것 중 가장 큰 idx를 찾아 점수차를 계산하고
    점수 합이 0이상에 대해 가장 작은 인덱스를 찾아 조사했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1525
    {

        static void Main1525(string[] args)
        {

            int n;
            (long a, long b)[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int half = n >> 1;
                Dictionary<long, long>[] dic = new Dictionary<long, long>[half + 1];
                
                for (int i = 0; i <= half; i++)
                {

                    dic[i] = new();
                }

                DFS1();

                List<(long v, long s)>[] front = new List<(long v, long s)>[half + 1];
                for (int i = 0; i <= half; i++)
                {

                    front[i] = new(dic[i].Count);

                    foreach (var item in dic[i].OrderBy(x => x.Key))
                    {

                        front[i].Add((item.Key, item.Value));
                    }
                }

                long min = 1_000_000_000_000_000_000;
                long state = 0L;

                DFS2(half);

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = n - 1; i >= 0; i--)
                {

                    if ((state & (1L << i)) == 0L) sw.Write($"1 ");
                    else sw.Write($"2 ");
                }

                void DFS2(int _dep, int _selectA = 0, long _sub = 0L, long _state = 0L)
                {

                    if (_dep == n)
                    {

                        List<(long v, long s)> cur = front[_selectA];

                        int idx = BinarySearch(_sub);

                        if (0 <= idx)
                        {

                            long chk = cur[idx].v + _sub;
                            Chk(chk, idx);
                        }

                        if (idx + 1 < cur.Count)
                        {

                            long chk = cur[idx + 1].v + _sub;
                            Chk(chk, idx + 1);
                        }

                        return;

                        int BinarySearch(long _val)
                        {
                            
                            int l = 0;
                            int r = cur.Count - 1;

                            while (l <= r)
                            {

                                int mid = (l + r) >> 1;
                                if (_val + cur[mid].v < 0) l = mid + 1;
                                else r = mid - 1;
                            }

                            return r;
                        }

                        void Chk(long _chk, int _idx)
                        {

                            _chk = _chk < 0 ? -_chk : _chk;
                            if (min < _chk) return;
                            else if (min == _chk)
                            {

                                long chkState = _state | cur[_idx].s;
                                state = Math.Min(state, chkState);
                            }
                            else
                            {

                                min = _chk;
                                state = _state | cur[_idx].s;
                            }
                        }
                    }

                    if (_selectA < half) DFS2(_dep + 1, _selectA + 1, _sub + arr[_dep].a, _state);
                    DFS2(_dep + 1, _selectA, _sub - arr[_dep].b, _state | (1L << n - 1 - _dep));
                }

                void DFS1(int _dep = 0, int _selectB = 0, long _sub = 0L, long _state = 0L)
                {

                    if (_dep == half)
                    {

                        Dictionary<long, long> cur = dic[_selectB];

                        if (cur.ContainsKey(_sub))
                            cur[_sub] = Math.Min(cur[_sub], _state);
                        else
                            cur[_sub] = _state;

                        return;
                    }

                    DFS1(_dep + 1, _selectB, _sub + arr[_dep].a, _state);
                    if (_selectB < half) DFS1(_dep + 1, _selectB + 1, _sub - arr[_dep].b, _state | (1L << (n - 1 - _dep)));
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());

                arr = new (long a, long b)[n];
                string[] temp = sr.ReadLine().Split();
                for (int i = 0; i < n; i++)
                {

                    arr[i].a = long.Parse(temp[i]);
                }

                temp = sr.ReadLine().Split();
                for (int i = 0; i < n; i++)
                {

                    arr[i].b = long.Parse(temp[i]);
                }
            }
        }
    }

#if other
// #include <iostream>
// #include <algorithm>
// #define MAX 1000000000000000000
using namespace std;

int n, mid;
pair<long long, long long> arr[36];
pair<long long, int> values_front[262144];
int fid;
pair<long long, int> values_back[262144];
int bid;

void front_dfs(int cursor, int count, int mask, long long value){
    if(cursor == mid)
    {
        values_front[fid++] = {value, mask};
        return;
    }

    if(mid - cursor > count)
        front_dfs(cursor + 1, count,  mask, value + arr[cursor].first);
    if(count)
        front_dfs(cursor + 1, count - 1, mask | 1 << mid - 1 - cursor, value - arr[cursor].second);
}

void back_dfs(int cursor, int count, int mask, long long value){
    if(n - cursor == mid){
        values_back[bid++] = {value, mask};
        return;
    }
    if(n - mid - cursor > count)
        back_dfs(cursor + 1, count,  mask, value + arr[n - 1 - cursor].first);
    if(count)
        back_dfs(cursor + 1, count - 1, mask | 1 << cursor, value - arr[n - 1 - cursor].second);
}

struct ret {
    long long diff;
    int front, back;
};
int operator<(const ret& a, const ret& b){
    return a.diff < b.diff ||
    a.diff == b.diff && a.front < b.front ||
    a.diff == b.diff && a.front == b.front && a.back < b.back;
}

ret search(int front_cnt){
    fid = 0, bid = 0;
    front_dfs(0, front_cnt, 0, 0);
    back_dfs(0, mid - front_cnt, 0, 0);

    sort(values_front, values_front + fid);
    sort(values_back, values_back + bid);

    int fc = 0, bc = bid - 1;

    ret r = {MAX, 0, 0};

    while (fc < fid && bc >= 0){
        long long v = abs(values_front[fc].first + values_back[bc].first);
        ret target = {v, values_front[fc].second, values_back[bc].second};
        if(target < r)
            r = target;
        if(bc > 0 && values_back[bc].first == values_back[bc - 1].first
        || values_front[fc].first + values_back[bc].first >= 0)
            --bc;
        else
            ++fc;
    }

    return r;
}

int main(){
    ios::sync_with_stdio(false);
    cin.tie(0);
    cin >> n;
    mid = n / 2;
    for (int i = 0; i < n; ++i)
        cin >> arr[i].first;
    for (int i = 0; i < n; ++i)
        cin >> arr[i].second;

    ret r = {MAX, 0, 0};
    for (int i = 0; i <= mid; ++i) {
        ret v = search(i);
        if(v < r)
            r = v;
    }

    for (int i = 0; i < mid; ++i) {
        cout << 1 + (r.front >> mid - i - 1 & 1) << ' ';
    }

    for (int i = 0; i < mid; ++i) {
        cout << 1 + (r.back >> mid - i - 1& 1) << ' ';
    }
}
#elif other2
// #include <bits/stdc++.h>
// #define fastio ios_base::sync_with_stdio(0), cin.tie(0)
using namespace std;
typedef long long lon;
typedef struct info
{
	lon sum, bit;
	bool operator<(const struct info& t)
	{
		if (sum == t.sum)	return bit > t.bit;
		return sum < t.sum;
	}
}info;

int n, half, cnt[2];
lon res[2] = { 0xFFFFFFFFFFFFFFF }, seq[2][36];
info diff[2][50000];

void rightback(int time, int req, info v)
{
	if (time == n)
	{
		diff[1][cnt[1]++] = v;
		return;
	}
	if (req + time < n)	rightback(time + 1, req, { v.sum - seq[1][time], v.bit });
	if (req)	rightback(time + 1, req - 1, { v.sum + seq[0][time], v.bit | (0x1LL << (n - time - 1)) });
	return;
}

void leftback(int time, int req, info v)
{
	if (time == half)
	{
		diff[0][cnt[0]++] = v;
		return;
	}
	if (req + time < half)	leftback(time + 1, req, { v.sum - seq[1][time], v.bit });
	if (req)	leftback(time + 1, req - 1, { v.sum + seq[0][time], v.bit | (0x1LL << (n - time - 1)) });
	return;
}

int main()
{
	fastio;
	cin >> n;
	for (int t = 0; t < 2; ++t)
	{
		for (int a = 0; a < n; ++a)	cin >> seq[t][a];
	}
	half = n / 2;
	for (int t = 0; t <= half; ++t)
	{
		cnt[0] = 0;
		cnt[1] = 0;
		leftback(0, t, { 0, 0 });
		rightback(half, half - t, { 0, 0 });
		sort(diff[1], diff[1] + cnt[1]);
		int ptr[3] = { 0 };
		while (ptr[1] < cnt[1])
		{
			while (ptr[1] < cnt[1] && diff[1][ptr[0]].sum == diff[1][ptr[1]].sum)	++ptr[1];
			diff[1][ptr[2]++] = diff[1][ptr[0]];
			ptr[0] = ptr[1];
		}
		cnt[1] = ptr[2];
		for (int a = 0; a < cnt[0]; ++a)
		{
			int s = 0, e = cnt[1] - 1;
			lon sol[2] = { abs(diff[0][a].sum + diff[1][0].sum), diff[1][0].bit };
			while (s <= e)
			{
				int mid = (s + e) / 2;
				lon cal = diff[0][a].sum + diff[1][mid].sum, acal = abs(cal);
				if (acal < sol[0] || (acal == sol[0] && sol[1] < diff[1][mid].bit))
				{
					sol[0] = acal;
					sol[1] = diff[1][mid].bit;
				}
				if (cal < 0)	s = mid + 1;
				else
				{
					e = mid - 1;
				}
			}
			sol[1] |= diff[0][a].bit;
			if (sol[0] < res[0] || (sol[0] == res[0] && res[1] < sol[1]))
			{
				res[0] = sol[0];
				res[1] = sol[1];
			}
		}
	}
	lon exp2 = 0x1LL << (n - 1);
	for (int t = 0; t < n; ++t)
	{
		cout << (res[1] & exp2 ? 1 : 2) << ' ';
		exp2 >>= 1;
	}
	return 0;
}
#endif
}
