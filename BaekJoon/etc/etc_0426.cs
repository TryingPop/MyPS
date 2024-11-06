using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 2
이름 : 배성훈
내용 : 생일 선물
    문제번호 : 3661번

    그리디, 정렬 문제다
    문제 설명이 이상하다
    설명 중 각 사람이 낼 수 있는 최소 금액은 1원 이라는 설명이 있는데
    해당 검증 코드를 작성하면 틀린다

    아이디어는 다음과 같다 이분 탐색으로 미만인 최대 금액을 찾는다
    그리고 돈 많은 애들과 인덱스가 앞서는 애들로 정렬을 해서 앞에서부터 부족한 돈을 더 내게 한다
    그리고 해당 결과를 인덱스에 맞게 다시 출력하면 이상없이 통과한다

    시간은 다음과 같다
        O( T * N * log (N * M) ) 
    여기서 T : 테스트 케이스, N : 입력 수, M : 값의 범위
*/

namespace BaekJoon.etc
{
    internal class etc_0426
    {

        static void Main426(string[] args)
        {

            string IMPO = "IMPOSSIBLE";

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt();
            MyData[] arr = new MyData[100];

            while(test-- > 0)
            {

                int g = ReadInt();

                int n = ReadInt();

                int sum = 0;

                for (int i = 0; i < n; i++)
                {

                    int val = ReadInt();
                    arr[i].Set(val, i);

                    if (sum < g) sum += val;
                }

                // if (g < n || sum < g)
                if (sum < g)
                {

                    sw.WriteLine(IMPO);
                    continue;
                }

                Array.Sort(arr, 0, n);

                int l = 1;
                int r = g;

                while(l <= r)
                {

                    int mid = (l + r) / 2;

                    int calc = 0;
                    for (int i = 0; i < n; i++)
                    {

                        if (arr[i].val < mid) calc += arr[i].val;
                        else calc += mid;
                    }

                    if (g <= calc) r = mid - 1;
                    else l = mid + 1;
                }

                sum = 0;
                for (int i = 0; i < n; i++)
                {

                    int add = r;
                    if (arr[i].val < r) add = arr[i].val;

                    arr[arr[i].idx].ret = add;
                    sum += add;
                }

                int remain = g - sum;
                for (int i = 0; i < n; i++)
                {

                    if (arr[i].val <= r) continue;
                    arr[arr[i].idx].ret++;
                    remain--;

                    if (remain == 0) break;
                }

                for (int i = 0; i < n; i++)
                {

                    sw.Write(arr[i].ret);
                    sw.Write(' ');
                }

                sw.Write('\n');
            }

            sw.Close();
            sr.Close();



            int Comp((int val, int idx) _f, (int val, int idx) _b)
            {

                int ret = _f.val.CompareTo(_b.val);
                if (ret != 0) return ret;

                ret = _b.idx.CompareTo(_f.idx);
                return ret;
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

        struct MyData : IComparable<MyData>
        {

            public int val;
            public int idx;
            public int ret;

            public void Set(int _val, int _idx)
            {

                val = _val;
                idx = _idx;
            }

            public int CompareTo(MyData other)
            {

                int ret = other.val.CompareTo(val);
                if (ret != 0) return ret;

                return idx.CompareTo(other.idx);
            }
        }
    }

#if other
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.*;

public class Main {

    public static void main(String[] args) throws Exception {

        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        StringTokenizer st;
        int T = Integer.parseInt(br.readLine());    // 테케수
        StringBuilder sb = new StringBuilder();

        for (int t = 0; t < T; t++) {
            st = new StringTokenizer(br.readLine());
            int p = Integer.parseInt(st.nextToken());   // 선물의 가격
            int n = Integer.parseInt(st.nextToken());   // 선영이 친구의 수

            List<Data> list = new ArrayList<>();   // 각 사람이 낼 수 있는 금액
            List<Data> res = new ArrayList<>(); // 최종 리스트

            int total = 0;  // 친구들이 가진 총 금액

            st = new StringTokenizer(br.readLine());
            for (int i = 0; i < n; i++) {
                list.add(new Data(i, Integer.parseInt(st.nextToken())));
                res.add(new Data(i, 0));
                total += list.get(i).money;
            }

            // 선물 금액보다 친구들의 총 금액이 더 적으면 IMPOSSIBLE 출력
            if (total < p) {
                sb.append("IMPOSSIBLE\n");
                continue;
            }

            // 선물 금액과 친구들의 총 금액이 같을 경우
            if (total == p) {
                for (int i = 0; i < n; i++) {
                    sb.append(list.get(i).money).append(" ");
                }
                sb.append("\n");
                continue;
            }

            Collections.sort(list); // 오름차순 정렬

            int nbbang = p / n;    // 선물 금액의 1/n
            int n_copy = n;
            for (int i = 0; i < n; i++) {
                Data cur = list.get(i);
                n_copy--;
                if (cur.money < nbbang) {
                    res.get(cur.num).money += cur.money;
                    p -= cur.money;
                    nbbang = p / n_copy;
                } else {
                    res.get(cur.num).money += nbbang;
                    p -= nbbang;
                }
                if (n_copy == 0) break;
            }

            if(p != 0) {
                for (int i = n - 1; i >= 0; i--) {
                    Data cur = list.get(i);
                    if (res.get(cur.num).money + 1 <= cur.money) {
                        res.get(cur.num).money++;
                        p--;
                    }
                    if (p == 0) break;
                    if (i == 0) i = n;
                }
            }

            for (int i = 0; i < n; i++) {
                sb.append(res.get(i).money).append(" ");
            }
            sb.append("\n");
        }

        System.out.print(sb);
    }

    static class Data implements Comparable<Data> {
        int num, money;

        public Data(int num, int money) {
            this.num = num;
            this.money = money;
        }

        @Override
        public int compareTo(Data o) {
            if (this.money == o.money)  // 같은 돈이면 인덱스 내림차순
                return Integer.compare(o.num, this.num);
            return Integer.compare(this.money, o.money);
        }
    }
}
#endif
}
