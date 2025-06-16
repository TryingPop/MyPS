# C#

## 난이도 : 브론즈 1

## 알고리즘 분류
  - 구현
  - 문자열
  - 정렬

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
두 단어 A와 B가 주어졌을 때, A에 속하는 알파벳의 순서를 바꾸어서 B를 만들 수 있다면, A와 B를 애너그램이라고 한다.<br/>
두 단어가 애너그램인지 아닌지 구하는 프로그램을 작성하시오.<br/>


## 입력
첫째 줄에 테스트 케이스의 개수(<100)가 주어진다. 각 테스트 케이스는 한 줄로 이루어져 있고, 길이가 100을 넘지 않는 단어가 공백으로 구분되어서 주어진다. 단어는 알파벳 소문자로만 이루어져 있다.<br/>


## 출력
각 테스트 케이스마다 애너그램인지 아닌지를 예체 출력과 같은 형식으로 출력한다.<br/>


## 출력 형식
정확한 출력 형식은 제출에서 언어를 Java로 설정하면 확인할 수 있다.<br/>


	import java.io.File;
	import java.io.FileNotFoundException;
	import java.util.Scanner;
	
	public class Main {
	
	    private static boolean solveAnagrams(String first, String second ) {
	        /* ------------------- INSERT CODE HERE --------------------
	         *
	         * Your code should return true if the two strings are anagrams of each other.
	         *
	         * */
	
	        return false;
	
	        /* -------------------- END OF INSERTION --------------------*/
	    }
	
	    public static void main(String[] args) {
	        Scanner sc = new Scanner(System.in);
	
	        int numTests = sc.nextInt();
	
	        for (int i = 0; i < numTests; i++) {
	            String first = sc.next().toLowerCase();
	            String second = sc.next().toLowerCase();
	
	            System.out.println(first + " & " + second + " are " + (solveAnagrams(first, second) ? "anagrams." : "NOT anagrams."));
	        }
	    }
	}


## 예제 입력
3<br/>
blather reblath<br/>
maryland landam<br/>
bizarre brazier<br/>

## 예제 출력
blather & reblath are anagrams.<br/>
maryland & landam are NOT anagrams.<br/>
bizarre & brazier are anagrams.<br/>


## 풀이
출력형식은 문자 위 예제 출력을 보면 알 수 있다.<br/>
두 문자가 애너그램인지 확인하는 방법은 알파벳의 갯수가 같은지 확인하면 된다.<br/>
그래서 알파벳 갯수를 비교해 풀었다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/6996