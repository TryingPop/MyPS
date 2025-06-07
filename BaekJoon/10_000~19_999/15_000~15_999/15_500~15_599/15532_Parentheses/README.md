# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 해 구성하기

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
Dave loves strings consisting only of ‘(’ and ‘)’. Especially, he is interested in balanced strings. Any balanced strings can be constructed using the following rules:<br/>
  - A string “()” is balanced.
  - Concatenation of two balanced strings are balanced.
  - If T is a balanced string, concatenation of ‘(’, T, and ‘)’ in this order is balanced. For example, “()()” and “(()())” are balanced strings. “)(” and “)()(()” are not balanced strings.

Dave has a string consisting only of ‘(’ and ‘)’. It satisfies the followings:<br/>
  - You can make it balanced by swapping adjacent characters exactly A times.
  - For any non-negative integer B (B < A), you cannot make it balanced by B swaps of adjacent characters.
  - It is the shortest of all strings satisfying the above conditions.

Your task is to compute Dave’s string. If there are multiple candidates, output the minimum in lexicographic order. As is the case with ASCII, ‘(’ is less than ‘)’.<br/>

## 입력
The input consists of a single test case, which contains an integer A (1 ≤ A ≤ 10^9).<br/>

## 출력
Output Dave’s string in one line. If there are multiple candidates, output the minimum in lexicographic order.<br/>

## 예제 입력
4<br/>

## 예제 출력
)())((<br/>

## 힌트
String “))(()(” can be balanced by 4 swaps, but the output should be “)())((” because it is the minimum in lexicographic order.<br/>

## 풀이
괄호 3개 짜리로 규칙성을 찾아 풀었다.<br/>

	)))((( : 6번
	))()(( : 5번
	)())(( : 4번
	()))(( : 3번		// ))(( 로 더 짧게 줄일 수 있다
	... 이외에도 다른 경우가 있다.

그래서 보니 괄호별 최소 이동횟수는
괄호 1개인 경우 1 ~ 1번<br/>
괄호 2개인 경우 2 ~ 3번<br/>
괄호 3개인 경우 4 ~ 6번<br/>
...
괄호 n개인 경우 (n * (n - 1) / 2) + 1 ~ n * (n + 1) / 2번
을 찾을 수 있었고
'('의 시작위치로 횟수를 조절할 수 있었다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/15532