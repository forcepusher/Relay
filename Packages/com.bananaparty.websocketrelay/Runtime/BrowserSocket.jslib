const browserSocketLibrary = {
    // Class definition

    $browserSocket: {
        sockets: [],

        connect: function (serverAddress) {
            const webSocket = new WebSocket(serverAddress);
            webSocket.binaryType = "arraybuffer";

            const payloadQueue = [];

            webSocket.onmessage = function (messageEvent) {
                if (!(messageEvent.data instanceof ArrayBuffer)) {
                    throw new Error(
                        "Unsupported message type. Only binary (ArrayBuffer) messages are supported.",
                    );
                }
                payloadQueue.push(new Uint8Array(messageEvent.data));
            };

            this.sockets.push({ webSocket, payloadQueue });
            return this.sockets.length - 1;
        },

        send: function (socketIndex, payloadBytes) {
            this.sockets[socketIndex].webSocket.send(payloadBytes);
        },

        disconnect: function (socketIndex) {
            this.sockets[socketIndex].webSocket.close();
        },

        isConnected: function (socketIndex) {
            return (
                this.sockets[socketIndex].webSocket.readyState ===
                WebSocket.OPEN
            );
        },

        hasUnreadPayloadQueue: function (socketIndex) {
            return this.sockets[socketIndex].payloadQueue.length > 0;
        },

        readPayloadQueue: function (
            socketIndex,
            payloadBytesBufferPtr,
            payloadBytesBufferLength,
        ) {
            const socket = this.sockets[socketIndex];
            const payloadBytes = socket.payloadQueue[0];

            if (
                !payloadBytes ||
                payloadBytesBufferLength < payloadBytes.length
            ) {
                return payloadBytes ? payloadBytes.length : 0;
            }

            HEAPU8.set(socket.payloadQueue.shift(), payloadBytesBufferPtr);
            return payloadBytes.length;
        },
    },

    // External C# calls

    GetBrowserSocketIsConnected: function (socketIndex) {
        return this.$browserSocket.isConnected(socketIndex);
    },

    GetBrowserSocketHasUnreadPayloadQueue: function (socketIndex) {
        return this.$browserSocket.hasUnreadPayloadQueue(socketIndex);
    },

    BrowserSocketReadPayloadQueue: function (
        socketIndex,
        payloadBytesBufferPtr,
        payloadBytesBufferLength,
    ) {
        return this.$browserSocket.readPayloadQueue(
            socketIndex,
            payloadBytesBufferPtr,
            payloadBytesBufferLength,
        );
    },

    BrowserSocketConnect: function (serverAddressPtr) {
        return this.$browserSocket.connect(UTF8ToString(serverAddressPtr));
    },

    BrowserSocketSend: function (
        socketIndex,
        payloadBytesPtr,
        payloadBytesCount,
    ) {
        const bytesToSend = HEAPU8.buffer.slice(
            payloadBytesPtr,
            payloadBytesPtr + payloadBytesCount,
        );
        this.$browserSocket.send(socketIndex, bytesToSend);
    },

    BrowserSocketDisconnect: function (socketIndex) {
        this.$browserSocket.disconnect(socketIndex);
    },
};

autoAddDeps(browserSocketLibrary, "$browserSocket");
mergeInto(LibraryManager.library, browserSocketLibrary);
