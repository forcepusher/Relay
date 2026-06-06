# WebSocketRelay
Networking as simple as it gets (for programmers though).  
  
Key priorities:  
1. Readable Debug - JSON data stream for developing, binary for shipping (with JSON debug output on demand).
2. Portable - Relay server runtime embedded in the Unity package, double-click-ready.
3. Testing - Integration/unit tests using the portable runtime for quick QA. Especially valuable for AI slop.
  
Yes, it actually serializes your game state to JSON so you can easily spot bugs.  
That is the core difference for improving your debugging experience.  
  
Future plans:
1. Sample projects to use as a template for kickstarting development of your games.
2. Unity Dedicated Server, where Unity instance server is just a connected client to its own relay server.
3. UDP support via HTTP/3 QUIC.
  
Not planned:
1. Chasing performance brownie points. Not doing anything unless it's spiking in a profiler.
2. Drag and drop garbage. Too much hassle and bloat to get right.
3. Deterministic prediction-rollback. Not financially viable for most studios and game types.
  
---
  
Library boilerplate code and tests were AI-assisted, LLMs can't design OOP code like this anyway.  
And as always - beware it's all code.  
