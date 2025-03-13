using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 14
이름 : 배성훈
내용 : 굉장한 학생
    문제번호 : 2336번

    세그먼트 트리 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1405
    {

        static void Main1405(string[] args)
        {

            // 2336번
            int INF = 1_234_567;
            int n;
            (int r1, int r2, int r3)[] student;
            int[] seg;

            Input();

            SetSeg();

            GetRet();

            void GetRet()
            {

                // 첫 시험 점수로 랭킹을 정렬
                // 두 번째 점수나, 세 번째 점수로 정렬해도 상관없다.
                // 이렇게 되면 i < j일 때 i보다 대단한 j 번째 사람은 존재할 수 없다.
                // 이로 앞에 있는 인덱스로만 확인하면 된다.
                Array.Sort(student, (x, y) => x.r1.CompareTo(y.r1));
                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    // seg[두 번째 랭킹] = 세 번째 랭킹의 최솟값
                    // 자신앞에 있는 사람 중 대단한 사람이 있는지 확인한다.
                    // 자신보다, r2 점수가 높은 사람 중 r3의 가장 높은 점수가 GetVal의 결과가 된다.
                    // 그 랭킹이 자신보다 낮은 경우 대단한 사람이 존재한다는 말과 같다.
                    // 이외의 경우는 없다와 동치이다.
                    // 없는 경우 ret++이다.
                    if (GetVal(1, n, 1, student[i].r2) > student[i].r3)
                        ret++;

                    // 이제 자신 점수 추가
                    Update(1, n, student[i].r2, student[i].r3);
                }

                Console.Write(ret);
            }

            int GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                if (_e < _chkS || _chkE < _s) return INF;
                else if (_chkS <= _s && _e <= _chkE) return seg[_idx];

                int mid = (_s + _e) >> 1;

                return Math.Min(GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1), GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2));
            }

            void Update(int _s, int _e, int _chk, int _val, int _idx = 0)
            {

                if (_s == _e)
                {

                    seg[_idx] = _val;
                    return;
                }

                int mid = (_s + _e) >> 1;
                if (_chk <= mid) Update(_s, mid, _chk, _val, _idx * 2 + 1);
                else Update(mid + 1, _e, _chk, _val, _idx * 2 + 2);

                seg[_idx] = Math.Min(seg[_idx * 2 + 1], seg[_idx * 2 + 2]);
            }

            void SetSeg()
            {

                int log = n == 1 ? 1 : (int)(Math.Log2(n - 1) + 1e-9) + 2;
                seg = new int[1 << log];
                Array.Fill(seg, INF);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                student = new (int r1, int r2, int r3)[n];
                for (int i = 1; i <= n; i++)
                {

                    int idx = ReadInt() - 1;
                    student[idx].r1 = i;
                }

                for (int i = 1; i <= n; i++)
                {

                    int idx = ReadInt() - 1;
                    student[idx].r2 = i;
                }

                for (int i = 1; i <= n; i++)
                {

                    int idx = ReadInt() - 1;
                    student[idx].r3 = i;
                }

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

                        while((c = sr.Read()) != -1 && c!= ' ' && c != '\n')
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
// #include <vector>
// #include <algorithm>
// #include <limits>
using namespace std;

const int INF = 1e9;

struct Fenw {
    int n;
    vector<int> tree;
    Fenw(int n): n(n), tree(n+1, INF) {}
    
    // index는 1-indexed
    void update(int idx, int val) {
        for(; idx <= n; idx += idx & -idx)
            tree[idx] = min(tree[idx], val);
    }
    
    int query(int idx) {
        int ret = INF;
        for(; idx > 0; idx -= idx & -idx)
            ret = min(ret, tree[idx]);
        return ret;
    }
};

int main(){
    ios::sync_with_stdio(false);
    cin.tie(nullptr);
    
    int N;
    cin >> N;
    
    // 각 시험에서의 순위를 저장할 배열 (1-indexed)
    vector<int> pos1(N+1), pos2(N+1), pos3(N+1);
    vector<int> exam1_order(N+1);
    
    // 첫 번째 시험: 순서대로 입력받음 (1등부터)
    for (int i = 1; i <= N; i++){
        int student;
        cin >> student;
        exam1_order[i] = student;
        pos1[student] = i;
    }
    
    // 두 번째 시험: 순위 i에 해당하는 학생 번호 입력 → pos2[student] = i;
    for (int i = 1; i <= N; i++){
        int student;
        cin >> student;
        pos2[student] = i;
    }
    
    // 세 번째 시험
    for (int i = 1; i <= N; i++){
        int student;
        cin >> student;
        pos3[student] = i;
    }
    
    // Fenwick Tree: 인덱스는 두 번째 시험 순위, 저장값은 최소 세 번째 시험 순위
    Fenw fenw(N);
    int count = 0;
    
    // 첫 번째 시험 순서대로 처리
    for (int i = 1; i <= N; i++){
        int student = exam1_order[i];
        int rank2 = pos2[student];
        int rank3 = pos3[student];
        // 두 번째 시험에서 rank2보다 좋은 학생들 중 최소 세 번째 시험 순위
        int minRank3 = fenw.query(rank2 - 1);
        
        // 만약 minRank3이 현재 학생의 세 번째 순위보다 크거나 INF면,
        // 이미 처리된 학생 중 세 시험 모두에서 더 좋은 학생이 없음 → 굉장한 학생!
        if(minRank3 > rank3)
            count++;
        
        // Fenw Tree 업데이트: 두 번째 시험 순위가 rank2인 위치에 현재 학생의 세 번째 시험 순위 반영
        fenw.update(rank2, rank3);
    }
    
    cout << count << "\n";
    return 0;
}

#endif
}
