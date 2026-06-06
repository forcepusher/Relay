# WebSocketRelay
Networking as simple as it gets (for programmers though).  
  
Key priorities:  
1. Readable Debug - JSON data stream for developing, binary for shipping (with JSOD debug output on demand).
2. Protable - Relay server runtime embedded in the Unity package, double-click-ready.
3. Testing - Integration/unit tests using the portable runtime for quick QA. Especially valuable for AI slop.
  
Yes, it actually serializes your game state to JSON so you can easily spot bugs.  
That is the core difference for improving your debugging experience.  
  
Library boilerplate code abd tests were AI-assisted, LLMs can't write OOP code like this anyway.  
And as always - beware it's all code. No drag and drop crap planned.  
