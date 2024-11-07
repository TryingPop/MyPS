# C#

## 난이도 : 실버 5

## 알고리즘 분류
  - 문자열
  - 파싱

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
컴퓨터 과학에서 오라클은 어떤 질문에든 대답할 수 있는 대단한 것이다. 이 문제에서 당신은 모든 것에 답변할 수 있는 오라클을 프로그램으로 작성해야 한다. 이것은, 삶, 우주, 그리고 모든 것에 대한 답변이 42라는 것을 알고 있다면, 아주 쉬운 일이다.<br/>

## 입력
입력은 한 줄로 된 1000자 이내의 단락이 주어진다. 포함된 문자는 대소문자, 띄어쓰기, 하이픈(hyphen), 어퍼스트로피(apostrophe), 반점(comma), 세미콜론(semicolon), 온점(period)과 물음표(question mark)이다. 문장의 처음은 대문자로 시작하며, 이외에는 어디에도 대문자가 나타나지 않는다. 문장은 마침표 또는 물음표로 끝난다. 모든 질문은 "What is"로 시작해서 물음표로 끝난다.<br/>

## 출력
각각의 질문에 대해, "What"은 "Forty-two"로, 끝의 "?"는 "."으로 대치하여 답변을 내어라.<br/>

## 예제 입력
Let me ask you two questions. What is the answer to life? What is the answer to the universe?<br/>

## 예제 출력
Forty-two is the answer to life.<br/>
Forty-two is the answer to the universe.<br/>

## 풀이
Split 메소드를 이용해 What is 를 기준으로 분할했다.<br/>
그러면 1번부터 What is ~~~ 의 ~~~가 담긴다.<br/>
이후 ?가 나올때까지 길이를 잰 뒤 Substring 메소드로 부분 문자열을 구해 출력해 풀었다.<br/>
문장은 .이나 ?로 끝난다고 했으므로 .이 나오면 질문이 아니므로 끊었다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/7656