const PORT = 8080;
const sockets = new Set<Bun.ServerWebSocket>();

const server = Bun.serve({
    port: PORT,
    fetch(req, server) {
        if (server.upgrade(req)) {
            return undefined;
        }
        return new Response("WebSocket Relay Server");
    },
    websocket: {
        open(ws) {
            sockets.add(ws);
            console.log(`Client connected. Total clients: ${sockets.size}`);
        },
        close(ws) {
            sockets.delete(ws);
            console.log(`Client disconnected. Total clients: ${sockets.size}`);
        },
        message(ws, message) {
            // Broadcast the message to all other connected clients
            for (const client of sockets) {
                if (client !== ws) {
                    client.send(message);
                }
            }
        },
    },
});

console.log(`Relay Server listening on port ${PORT}`);
