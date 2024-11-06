# C#

## 난이도 : 실버 1

## 알고리즘 분류
  - 문자열
  - 브루트포스 알고리즘

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 256 MB

## 문제
#### Returning back to problem solving, Gildong is now studying about palindromes. He learned that a palindrome is a string that is the same as its reverse. For example, strings "pop", "noon", "x", and "kkkkkk" are palindromes, while strings "moon", "tv", and "abab" are not. An empty string is also a palindrome.
#### Gildong loves this concept so much, so he wants to play with it. He has n distinct strings of equal length m. He wants to discard some of the strings (possibly none or all) and reorder the remaining strings so that the concatenation becomes a palindrome. He also wants the palindrome to be as long as possible. Please help him find one.

## 입력
#### The first line contains two integers n and m (1 ≤ n ≤ 100, 1 ≤ m ≤ 50) — the number of strings and the length of each string.
#### Next n lines contain a string of length m each, consisting of lowercase Latin letters only. All strings are distinct.

## 출력
#### In the first line, print the length of the longest palindrome string you made.
#### In the second line, print that palindrome. If there are multiple answers, print any one of them. If the palindrome is empty, print an empty line or don't print this line at all.

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

## 문제 링크
https://www.acmicpc.net/problem/24832