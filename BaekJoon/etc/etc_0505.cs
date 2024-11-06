using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 11
이름 : 배성훈
내용 : 락스타 락동호
    문제번호 : 1581번

    많은 조건분기 문제다
    그리디하게 접근해서 풀었다
    FF, FS가 0인 경우만 따로 구분하고
    나머지는 경우의 수가 4000밖에 안되므로 시뮬레이션 돌렸다
    이후에 조건식으로 풀어냈다 여기서 부등호 잘못 설정해서 한 번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0505
    {

        static void Main505(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            int ret;
            if (input[0] == 0 && input[1] == 0)
            {

                ret = input[3];
                ret += input[2] == 0 ? 0 : 1;
            }
            else
            {

                ret = input[0];
                if (input[1] > 0)
                {

                    ret += input[3];
                    ret += 2 * Math.Min(input[1], input[2]);
                    ret += input[1] > input[2] ? 1 : 0;
                }
            }

            Console.WriteLine(ret);
        }
    }

#if other
import java.util.*;
import java.io.*;
import java.math.*;
 
public class Main{
    static BufferedReader br = 
            new BufferedReader(new InputStreamReader(System.in));
    static BufferedWriter bw = 
            new BufferedWriter(new OutputStreamWriter(System.out));
    static StringTokenizer st;
    
    public static void main(String args[]) throws IOException{
    	int FF, FS, SF, SS, ans;
    	FF = getInt();
    	FS = getInt();
    	SF = getInt();
    	SS = getInt();
    	if(FF + FS == 0) ans = SS + Math.min(1, SF);
    	else if(FS == 0) ans = FF;
    	else ans = FF + SS + 2*Math.min(FS, SF) + ((FS > SF) ? 1 : 0);
    	bw.write(ans+"");
    	bw.close();
    }
    
	static void getToken() throws IOException{
    	if(st != null && st.hasMoreTokens()) return;
    	st = new StringTokenizer(br.readLine());
    }
    
	static int getInt() throws IOException{
    	getToken();
    	return Integer.parseInt(st.nextToken());
    }
}
#endif
}
