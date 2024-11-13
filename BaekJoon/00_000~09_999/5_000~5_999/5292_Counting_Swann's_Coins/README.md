# C#

## 난이도 : 브론즈 3

## 알고리즘 분류
  - 구현

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Governor Weatherby Swann orders you to count the number of coins in the government treasury. To make the job more interesting you decide to say ”Dead” for all numbers that are a multiple of 3 and ”Man” for all numbers that are a multiple of 5. For numbers that are multiples of both 3 and 5 you say ”DeadMan”.<br/>

## 입력
The input will be the number of coins you need to count.<br/>

## 출력
The output will be the numbers and words in sequence, separated by spaces. Start a new line after each word.<br/>

## 예제 입력
18<br/>

## 예제 출력
1 2 Dead<br/>
4 Man<br/>
Dead<br/>
7 8 Dead<br/>
Man<br/>
11 Dead<br/>
13 14 DeadMan<br/>
16 17 Dead<br/>

## 풀이
3의 배수이면 Dead, 5의 배수이면 Man, 15의 배수이면 DeadMan을 출력하고 줄을 바꾸면 된다.<br/>
이외는 숫자를 출력하게 구현하면 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/5292