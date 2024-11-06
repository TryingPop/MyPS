# C#

## 난이도 : 브론즈 1

## 알고리즘 분류
  - 구현
  - 문자열

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 1024 MB

## 문제
#### Oh no! Joanna just spilled some syrup on her keyboard and now some of the keys are sticky. This causes her considerable frustration, as every time she presses one of the sticky keys, the corresponding character gets entered twice on her computer.
#### This could not have happened at a more inconvenient time; it is the start of the contest and she was just about to type in the solution to the first problem! Joanna does not have time to remove and clean every key on her keyboard, so she wonders if there is a way to quickly identify the sticky keys. Starting to panic, she quickly types some text on her keyboard and stares at the resulting text displayed on her screen.
#### Given the line of text that Joanna typed on her keyboard and the resulting text displayed on her screen, help her determine which keys must be sticky.

## 입력
#### The input consists of:
  - One line containing a string s (1 ≤ length(s) ≤ 1,000), the text that Joanna typed on her keyboard.
  - One line containing a string t (1 ≤ length(t) ≤ 1,000), the text displayed on Joanna's screen as a result.
#### Both s and t consist only of lower-case letters (`a'--`z') and spaces (` '), and start and end with a letter.
#### It is guaranteed that t is the result of doubling each character in s that corresponds to a sticky key. At least one character in s corresponds to a sticky key (i.e. s ≠ t).

## 출력
#### Output all characters (letters and space) corresponding to keys that must be sticky, in any order.

## 예제 입력
this is very annoying<br/>
thiss iss veery annoying<br/>

## 예제 출력
se<br/>

## 문제 링크
https://www.acmicpc.net/problem/21347