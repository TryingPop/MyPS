# C#

## 난이도 : 실버 1

## 알고리즘 분류
  - 문자열
  - 브루트포스 알고리즘

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 256 MB

## 문제
Returning back to problem solving, Gildong is now studying about palindromes. He learned that a palindrome is a string that is the same as its reverse. For example, strings "pop", "noon", "x", and "kkkkkk" are palindromes, while strings "moon", "tv", and "abab" are not. An empty string is also a palindrome.<br/>
Gildong loves this concept so much, so he wants to play with it. He has n distinct strings of equal length m. He wants to discard some of the strings (possibly none or all) and reorder the remaining strings so that the concatenation becomes a palindrome. He also wants the palindrome to be as long as possible. Please help him find one.<br/>

## 입력
The first line contains two integers n and m (1 ≤ n ≤ 100, 1 ≤ m ≤ 50) — the number of strings and the length of each string.<br/>
Next n lines contain a string of length m each, consisting of lowercase Latin letters only. All strings are distinct.<br/>

## 출력
In the first line, print the length of the longest palindrome string you made.<br/>
In the second line, print that palindrome. If there are multiple answers, print any one of them. If the palindrome is empty, print an empty line or don't print this line at all.<br/>

## 예제 입력
3 3<br/>
tab<br/>
one<br/>
bat<br/>

## 예제 출력
6<br/>
tabbat<br/>

## 노트
"battab" is also a valid answer.

## 풀이
입력되는 문자열이 100개이므로 각 원소에대해 나머지 원소 중 뒤집어진 문자열이 있는지 확인했다.<br/>
그리고 중앙에 단 하나만 올 수 있는 자신이 팰린드롬인지 확인했다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/24832