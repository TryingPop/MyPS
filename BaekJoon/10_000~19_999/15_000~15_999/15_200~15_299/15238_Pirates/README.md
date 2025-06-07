# C#

## 난이도 : 브론즈 3

## 알고리즘 분류
  - 구현
  - 문자열

## 제한조건
  - 시간 제한 : 5초
  - 메모리 제한 : 512 MB

## 문제
Pirates talk a funny way. They say word where the letters are repeated more than they need to be. We would like know which letter appears the most frequently in a Pirate word.<br/>
For example: In the word “arrrrrghh”, the letter “r” appears 5 times while “h” appears twice.<br/>
Write a program that reads a pirate word from the terminal and writes the letter that occurs most frequently.<br/>
It is guaranteed that only one letter is the most repeated. That is, words like “aipo” won’t appear because no single letter wins.<br/>


## 입력
The input will consist of two lines.<br/>
The first line will contain the size of the word and the second the word to process.<br/>
The word will only contain lowercase letters from a to z. The size of the word will be at most 1000 characters. No spaces or other symbols will appear.<br/>


## 출력
Print a single line with the character that appears the most and the number of occurrences.<br/>


## 예제 입력
9<br/>
shhhiiver<br/>


## 예제 출력
h 3<br/>


## 풀이
문자열을 하나씩 읽으며 등장한 알파벳의 갯수를 세면 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/15238