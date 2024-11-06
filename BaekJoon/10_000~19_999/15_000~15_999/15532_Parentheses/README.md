# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 해 구성하기

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
#### Dave loves strings consisting only of ‘(’ and ‘)’. Especially, he is interested in balanced strings. Any balanced strings can be constructed using the following rules:
  - A string “()” is balanced.
  - Concatenation of two balanced strings are balanced.
  - If T is a balanced string, concatenation of ‘(’, T, and ‘)’ in this order is balanced. For example, “()()” and “(()())” are balanced strings. “)(” and “)()(()” are not balanced strings.
#### Dave has a string consisting only of ‘(’ and ‘)’. It satisfies the followings:
  - You can make it balanced by swapping adjacent characters exactly A times.
  - For any non-negative integer B (B < A), you cannot make it balanced by B swaps of adjacent characters.
  - It is the shortest of all strings satisfying the above conditions.
#### Your task is to compute Dave’s string. If there are multiple candidates, output the minimum in lexicographic order. As is the case with ASCII, ‘(’ is less than ‘)’.

## 입력
#### The input consists of a single test case, which contains an integer A (1 ≤ A ≤ 10^9).

## 출력
#### Output Dave’s string in one line. If there are multiple candidates, output the minimum in lexicographic order.

## 예제 입력
4<br/>

## 예제 출력
)())((<br/>

## 힌트
#### String “))(()(” can be balanced by 4 swaps, but the output should be “)())((” because it is the minimum in lexicographic order.

## 문제 링크
https://www.acmicpc.net/problem/15532