# C#

## 난이도 : 브론즈 2

## 알고리즘 분류
  - 구현
  - 문자열

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 128 MB

## 문제
영어에서는 어떤 글자가 다른 글자보다 많이 쓰인다. 예를 들어, 긴 글에서 약 12.31% 글자는 e이다.<br/>
어떤 글이 주어졌을 때, 가장 많이 나온 글자를 출력하는 프로그램을 작성하시오.<br/>


## 입력
첫째 줄부터 글이 주어진다. 글은 최대 50개의 줄로 이루어져 있고, 각 줄은 최대 50개의 글자로 이루어져 있다. 각 줄에는 공백과 알파벳 소문자만 있다. 글에 알파벳은 적어도 하나 이상 있다.<br/>


## 출력
첫째 줄에 가장 많이 나온 문자를 출력한다. 여러 개일 경우에는 알파벳 순으로 앞서는 것부터 모두 공백없이 출력한다.<br/>


## 예제 입력
english is a west germanic<br/>
language originating in england<br/>
and is the first language for<br/>
most people in the united<br/>
kingdom the united states<br/>
canada australia new zealand<br/>
ireland and the anglophone<br/>
caribbean it is used<br/>
extensively as a second<br/>
language and as an official<br/>
language throughout the world<br/>
especially in common wealth<br/>
countries and in many<br/>
international organizations<br/>


## 예제 출력
a<br/>


## 풀이
단순히 문장에 들어간 알파벳의 갯수를 세면 된다.<br/>
알파벳이 하나도 없는 줄이 있을 수 있다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/1371