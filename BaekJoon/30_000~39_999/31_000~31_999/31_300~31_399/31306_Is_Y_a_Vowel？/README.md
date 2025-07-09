# C#

## 난이도 : 브론즈 4

## 알고리즘 분류
  - 구현
  - 문자열

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 2048 MB

## 문제
The Vowels are a, e, i, o and u, and possibly y. People disagree on whether y is a vowel or not. Unfortunately for you, you have been tasked with counting the number of vowels in a word. You'll have to count how many vowels there are assuming y is a vowel, and assuming y is not.<br/>


## 입력
The single line of input contains a string of at least one and at most 50 lowercase letters.<br/>


## 출력
Output two space-separated integers. The first is the number of vowels assuming y is not a vowel, the second is the number of vowels assuming y is a vowel.<br/>


## 예제 입력
asdfiy<br/>


## 예제 출력
2 3<br/>


## 풀이
입력된 문자열에 a, e, i, o, u의 갯수를 세고, y의 갯수를 따로 센다.<br/>
그리고 a, e, i, o, u의 갯수를 합친걸 ret1이라 하면 ret1과 ret1 + y를 출력하면 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/31306