# C#

## 난이도 : 브론즈 2

## 알고리즘 분류
  - 구현
  - 문자열


## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB


## 문제
The Society for Prevention of Profanity on the Internet has observed a growing number of chat lines on the World Wide Web. A chat line allows a Web user to type lines of text which are transmitted to all other users. The Society is concerned about the number of fourletter words being transmitted by these chat lines and has proposed the mandatory use of software to remove all four-letter words from every transmission. Your job is to write the software to do this removal.<br/>


## 입력
The input to your program consists of an integer, n, on a line by itself, followed by n lines of text. Each line of text contains words separated by spaces. Each word consists of letters of the alphabet and exactly one space separates adjacent words. There are no spaces before the first word or after the last word on each line. Lines do not exceed 80 characters in length.<br/>


## 출력
The output from your program should consist of the n lines of text, with each four-letter word replaced by four asterisks. The lines should be separated by one blank line.<br/>

## 예제 입력
2<br/>
The quick brown fox jumps over the lazy dog<br/>
Now is the time for all good people to come to the aid of the party<br/>


## 예제 출력
The quick brown fox jumps **** the **** dog<br/>
<br/>
Now is the **** for all **** people to **** to the aid of the party<br/>


## 풀이
ReadLine으로 줄 단위로 문자열을 입력 받는다. 그리고 Split으로 띄어쓰기로 문자열을 구분해 문자열의 길이가 4이면 ****처리한 문자열로 바꿔 출력하면 된다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/6965