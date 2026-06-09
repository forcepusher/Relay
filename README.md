# WebSocketRelay
This is a development repository for com.bananaparty.websocketrelay  
See [package Readme file here](https://github.com/forcepusher/WebSocketRelay/tree/master/Packages/com.bananaparty.websocketrelay)

Networking as simple as it gets (for programmers though).  
The goal is to provide bare minimum code to get things done.  
  
Key priorities:  
1. Developer Experience - JSON data stream for developing. Binary for shipping (JSON debug output on demand).
2. Portable & Cheap - Relay server runtime embedded in Unity package. No expensive setups, doubleclick-ready.
3. Tests & Stability - Integration tests using the portable runtime for quick QA. Especially valuable for AI slop.
  
Yes, it actually serializes your game state to JSON so you can easily spot bugs.  
That is the core difference for improving your debugging experience.  
  
Future plans:
1. Sample projects to use as a template for kickstarting development of your games.
2. Unity Instance Dedicated Server. Unity spins up a relay server and connects to it as a client to act as a server.
3. UDP support via HTTP/3 QUIC. At this point it's going to be just as efficient as any other non-web network library.
  
Not planned:
1. Chasing performance brownie points. If it's not spiking in a profiler, then I'm not doing anything.
2. Drag and drop garbage. Too much hassle and bloat just to get right. If you're not a programmer - don't touch it.
3. Deterministic prediction-rollback. Very CPU-intensive, expensive to develop, and horrible developer experience.
  
---
  
Library boilerplate code and tests were AI-assisted, LLMs can't design OOP code like this anyway.  
And as always - beware it's all code.  
