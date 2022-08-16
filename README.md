# UnoNet
**UnoNet** is a **simple**, **TCP-based** networking solution that's aimed toward beginners.
While making UnoNet, I focused on making it simple and easy to understand and tried to look in the eyes of a beginner.

## Project status
**.NET Version:** 4.8.1

As of now, the project is being tested in the unity game engine.
Since Unity supports only 4. X, the project, for now, will stay at 4.8.1 and may be upgraded to support other platforms in the future

**Features & Plans**
- [✅] Supports multiple clients
- [✅] Based on TCP
- [✅] Clients connecting & disconnecting events in server and client
- [❌] Disconnect reasons (For now it's just "DisconnectReasons.Disconnected")
- [✅] Client & Server sending packets between each other
- [✅] Client can send packets to all the connected clients
- [❌] Client-to-client packet sending
- [:x:] Add custom json serialization solution
- [:x:] Add documentation

## Setup
**Currently UnoNet requires Newtonsoft's Json.NET to work. Download here [Newtonsoft Json.NET](https://www.newtonsoft.com/json)**

Download the latest release of UnoNet

Add the wanted .dlls (For Client - UnoNet.Client, Server - UnoNet.Server, **UnoNet.Core is required for both of them to work!**) to your project (For Unity add it in Assets/Plugins)

Start coding! :tada:
