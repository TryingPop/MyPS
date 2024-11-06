using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 5
이름 : 배성훈
내용 : 점 숫자
    문제번호 : 8891번

    수학, 구현, 브루트포스 문제다

    아이디어는 다음과 같다
    해당 값이 주어지면 좌표를 찾고, 좌표연산을 한 뒤에 다시 값을 찾게 했다

    값으로 좌표를 찾는건 몇 번째 대각선에 포함되고 x의 좌표만 확인했다
    대각선 시작지점을 보면 
        1, 2, 4, 7, 
         +1 +2 +3 +4...
    계차수열이고, 누적합이 필요하게되었다
    값의 범위가 0에서 1만까지이므로 1만은 142번째 대각선에 포함된걸 확인했다
    그래서 나올 수 있는 연산은 많아야 284번째 대각선인다
    넉넉하게 300번째 대각선까지 구하고, 몇 번째 대각선에 포함되는지는 이분탐색을 이용했다

    그리고 좌표로 대각선 변환은 대각선 찾는 방법은 대각선의 번호는 x좌표 + y좌표 -1이므로
    금방 찾아진다
    해당 코드를 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0452
    {

        static void Main452(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int[] sum = new int[301];
            sum[1] = 1;
            for (int i = 1; i < sum.Length - 1; i++)
            {

                sum[i + 1] = i + sum[i];
            }

            int test = ReadInt();

            while (test-- > 0)
            {

                int n1 = ReadInt();
                int n2 = ReadInt();

                IntToPos(n1, out int x1, out int y1);
                IntToPos(n2, out int x2, out int y2);

                int ret = PosToInt(x1 + x2, y1 + y2);
                sw.WriteLine(ret);
            }

            sr.Close();
            sw.Close();

            void IntToPos(int _find, out int _x, out int _y)
            {

                // 좌표 찾기
                int l = 1;
                int r = 300;

                while (l <= r)
                {

                    int mid = (l + r) / 2;

                    if (sum[mid] <= _find) l = mid + 1;
                    else r = mid - 1;
                }

                int idx = r;
                _x = _find - sum[idx] + 1;
                _y = idx - _x + 1;
            }

            int PosToInt(int _x, int _y)
            {

                int ret = sum[_x + _y - 1];
                ret += _x - 1;
                return ret;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
#if other
import java.io.*;
import java.util.*;
class Main{
	public static int[] xy(int a){
		int k=(int)((Math.sqrt(8*a)+3)/2);
		int[] arr=new int[2];
		arr[1]=(a-((k-2)*(k-1)/2));
		arr[0]=k-arr[1];
		return arr;
	}
	
    public static void main(String[] args)throws Exception{
    	BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
    	BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(System.out));
    	int n=Integer.parseInt(br.readLine());
    	int a=0;
    	int b=0;
    	int[][] where=new int[2][2];
    	for(int i=0;i<n;i++){
    		String[] reader=br.readLine().split(" ");
    		a=Integer.parseInt(reader[0]);
    		b=Integer.parseInt(reader[1]);
    		where[0]=xy(a);
    		where[1]=xy(b);
    		bw.write(((where[0][0]+where[0][1]+where[1][0]+where[1][1]-1)*(where[0][0]+where[0][1]+where[1][0]+where[1][1]-2)/2+where[0][1]+where[1][1])+"\n");
    	}
        bw.close();
    }
}
#endif
}
