# AkkaGameServer
Goal is to experiment with Akka.NET & create a basic game server. 
Also want to implement DotNetty, WebSocketSharp & Kafka. 

This is far from complete.

only tested ```WebSocketSharp``` messages so far. 


Trigger message example via Postman Websockets: 

Sent: ```1000|{"connectionId":"test123"}``` (Where '1000' is the header, '{}' is the message)

Recieved: ```1010|{"ConnectionId":"718304496c3f462ba6f62ede6580535d","Player":{"PlayerId":"647a7433-519a-4a44-ae95-15c4a4d90994","CurrentRoomId":"00000000-0000-0000-0000-000000000000","ConnectionId":"718304496c3f462ba6f62ede6580535d"}}```  (This is just example data). 


