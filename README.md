# AkkaGameServer

AkkaGameServer is an experimental game server built using **[Akka.NET](https://getakka.net/)**. The goal is to explore the capabilities of Akka.NET and create a basic foundation for a game server.


## Network Layer

The network layer handles the underlying communication with clients. It supports the following libraries:

- [WebSocketSharp](https://github.com/sta/websocket-sharp)
- [DotNetty](https://github.com/Azure/DotNetty)

## Messaging

The messaging system is designed to handle incoming and outgoing packets efficiently.

#### Incoming Packets

Incoming packets implement the [`IIncomingPacket`](https://github.com/Gaz1994/AkkaGameServer/blob/main/AkkaGameServer.API/Communication/Incoming/IIncomingPacket.cs) interface. When a message is received via the network layer, the [`IncomingHandler`](https://github.com/Gaz1994/AkkaGameServer/blob/main/AkkaGameServer.API/Communication/Incoming/IncomingHandler.cs) class resolves the correct packet type based on the `Header` stored in a dictionary.

#### Outgoing Packets

Outgoing packets implement the [`IOutgoingPacket<MessageType>`](https://github.com/Gaz1994/AkkaGameServer/blob/main/AkkaGameServer.API/Communication/Outgoing/IOutgoingPacket.cs) interface. To send a packet, you need to provide the appropriate response message that the outgoing packet will use.

### Message Format

The message style is inspired by 2-byte header, 4-byte payload.

## Testing

The server has been tested with [`WebSocketSharp`](https://github.com/sta/websocket-sharp) messages using Postman Websockets.

### Example

- Sent: `1000|{"connectionId":"test123"}`
  - `1000` represents the header
  - `{}` represents the message payload

- Received: `1010|{"ConnectionId":"718304496c3f462ba6f62ede6580535d","Player":{"PlayerId":"647a7433-519a-4a44-ae95-15c4a4d90994","CurrentRoomId":"00000000-0000-0000-0000-000000000000","ConnectionId":"718304496c3f462ba6f62ede6580535d"}}`

This is an example response with test data

## Notes

- The `connectionId` from incoming messages is no longer used. Instead, it is set within the backend for better practice.
- This README file will be updated with more information as the project progresses.

## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvement, please open an issue or submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).
