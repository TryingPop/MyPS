# C#

## 난이도 : 실버 4

## 알고리즘 분류
  - 문자열
  - 브루트포스 알고리즘

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Dalia is the assistant director of the fundraising team in the ACPC. She is facing a difficult time this year, there’s a huge lack of sponsors! And now we are facing the danger of not being able to provide the teams with balloons, T-shirts or even name-tags.<br/>
Dalia knows it is too late to get a sponsor, actually too late to do anything. But she doesn’t simply give up; she decided that her only hope is to gamble. She will go to a casino where they just invented a new game; she thinks she might have a more promising chance if she plays that game.<br/>
The game is very simple, the dealer puts a long string of cards on the table in front of Dalia and she is required to point out palindromes longer than one character (words that are read backward the same as forward) of maximum length (a maximum length palindrome is a palindrome that no other palindrome exists in the string with greater length). So if the maximum length of palindrome in a string is X>1, print all palindromes of length X in the string.<br/>


## 입력
Input will start with T number of test cases. Each test case will consist of 1 line that contains a non- empty string S of lower case English letters no longer than 1000 characters.<br/>


## 출력
For each test case, print a line containing the case number as shown in the sample then print the palindromes each on a line by itself, in the order of their occurrence in S from right to left.<br/>


## 예제 입력
2
abcba
abba<br/>


## 예제 출력
Case #1:<br/>
abcba<br/>
Case #2:<br/>
abba<br/>


## 풀이
문자의 길이가 1000이고 테스트 케이스는 많아야 50개이다.<br/>
그래서 연산은 2500만으로 브루트포스 방법으로 풀었다.<br/>

팰린드롬인 문자열을 보면, 왼쪽 끝과 오른쪽 끝의 원소를 각각 1개씩 빼면 팰린드롬이 된다.<br/>
역으로 양끝에 같은 원소를 붙이면 팰린드롬이 됨은 자명하다.<br/>
그래서 기존 문자열의 각 문자를 중심으로 팰린드롬이 되는 가장 긴 길이를 찾아갔다.<br/>

그래서 가장 긴 팰린드롬을 찾으면 리스트에 왼쪽 끝의 인덱스를 저장했다.<br/>
가장 긴 길이를 찾으면 원소를 초기화 해줘야 하기에 List 클래스를 이용했다.<br/>

그리고 오른쪽에서부터 출력해야 하기에 출력전 인덱스가 큰걸로 정렬했다.<br/>
이렇게 문자열을 하나씩 출력했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/9351