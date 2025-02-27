# C#

## 난이도 : 브론즈 2

## 알고리즘 분류
  - 구현
  - 문자열

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
어린 제다이들은 요다와 대화하는 법을 배워야 한다. 요다는 모든 문장에서 가장 앞 단어 두 개를 제일 마지막에 말한다.<br/>
어떤 문장이 주어졌을 때, 요다의 말로 바꾸는 프로그램을 작성하시오.<br/>


## 입력
첫째 줄에 문장의 수 N이 주어진다. 둘째 줄부터 N개의 줄에는 각 문장이 주어진다. 문장의 길이는 100글자 이내이다. 단어의 개수는 3개 이상이다.<br/>


## 출력
각 문장을 요다의 말로 바꾼 뒤 출력한다.<br/>


## 예제 입력
4<br/>
I will go now to find the Wookiee<br/>
Solo found the death star near planet Kessel<br/>
I'll fight Darth Maul here and now<br/>
Vader will find Luke before he can escape<br/>


## 예제 출력
go now to find the Wookiee I will<br/>
the death star near planet Kessel Solo found<br/>
Darth Maul here and now I'll fight<br/>
find Luke before he can escape Vader will<br/>


## 풀이
ReadLine 함수를 이용해 줄 단위로 문자열을 입력받고, Split 메소드를 이용해 띄움으로 문자열을 구분한다.<br/>
그리고 문제대로 출력하면 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/5363