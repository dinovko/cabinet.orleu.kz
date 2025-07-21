class NCALayerService {
    constructor(cbFunction) {
        this.socket = null;
        this.isConnected = false;
        this.heartbeatTimeout = null;
        this.autoCloseTimeout = null;
        this.callback = cbFunction;

        window.addEventListener("beforeunload", () => this.disconnect());
    }

    connect() {
        return new Promise((resolve, reject) => {
            if (this.socket && this.socket.readyState === WebSocket.OPEN) {
                console.info(`WebSocker подключен`);
                return resolve(true);
            }

            this.socket = new WebSocket("wss://127.0.0.1:13579/");

            this.socket.onopen = () => {
                console.info('Соединение открыто');

                setInterval(() => {
                    if (this.socket && this.socket.readyState === WebSocket.OPEN) {
                        this.socket.send("--heartbeat--");
                    }
                }, 1000);

                this.autoCloseTimeout = setTimeout(() => {
                    console.info("Автоматическое закрытие соединения через 2 минуты");
                    this.disconnect();
                }, 2 * 60 * 1000);
            };

            this.socket.onmessage = (event) => {

                let hasHeartbeat = false;
                let data = null;
                if (event.data == '--heartbeat--') {
                    hasHeartbeat = true;
                    resolve(true);
                } else {
                    data = JSON.parse(event.data);
                }

                const hasStatus = typeof data?.status !== "undefined";
                const hasBody = typeof data?.body === "object" && data.body !== null;
                const hasResult = hasBody && Array.isArray(data.body.result);
                const hasVersion = typeof data?.result !== "undefined";

                //console.info(hasStatus, hasBody, hasResult, hasVersion);

                if (hasVersion || hasHeartbeat) {
                    this.isConnected = true;
                    resolve(true);
                }
                else
                    if (hasStatus && hasBody && hasResult && !hasVersion) {
                        console.log("✅ Все поля есть");
                        //console.log("status:", data.status);
                        //console.log("result[0]:", data.body.result[0]);
                        //console.info(this.callback);
                        this.callback({ success: true, content: data.body.result[0] });
                        this.disconnect();
                       resolve({ success:true, content: data.body.result[0] });
                    }
                    else {
                        console.info(event)
                        this.disconnect();
                        reject("no validated response from service");
                    }
            };

            this.socket.onerror = (err) => {
                console.error("Ошибка WebSocket:", err);
                this.disconnect();
                reject("Ошибка WebSocket");
            };

            this.socket.onclose = () => {
                this.isConnected = false;
                clearTimeout(this.autoCloseTimeout);
                console.log("WebSocket закрыт.");
            };

            // Таймаут, если NCALayer не ответит за 1.5 секунды
            //this.heartbeatTimeout = setTimeout(() => {
            //    console.info('this.heartbeatTimeout')
            //    this.disconnect();
            //    reject("NCALayer не отвечает.");
            //}, 1500);
        })
    }

    disconnect() {
        if (this.socket) {
            this.socket.close();
            this.socket = null;
            this.isConnected = false;
            clearTimeout(this.autoCloseTimeout);
            clearTimeout(this.heartbeatTimeout);
            console.log("WebSocket вручную закрыт.");
        }
    }

    isAlive() {
        return this.isConnected;
    }

    openDialogWindow() {
        return new Promise(async (resolve, reject) => {

            let isOk = await this.connect();
            if (!isOk) {
                reject('connection error');
            }

            const timestamp = Date.now();
            const sessionid = this.generateGuid();

            const request = {
                "module": "kz.gov.pki.knca.basics",
                "method": "sign",
                "args": {
                    "allowedStorages": [
                        "PKCS12",
                        "AKKaztokenStore",
                        "AKKZIDCardStore",
                        "AKEToken5110Store"
                    ],
                    "format": "xml",
                    "data": `<?xml version=\"1.0\" encoding=\"UTF-8\"?><login><timeTicket>${timestamp}</timeTicket><sessionid>${sessionid}</sessionid></login>`,
                    "signingParams": {},
                    "signerParams": {
                        "extKeyUsageOids": [
                            "1.3.6.1.5.5.7.3.2"
                        ]
                    },
                    "locale": "ru"
                }
            };

            if (this.socket && this.socket.readyState === WebSocket.OPEN) {
                this.socket.send(JSON.stringify(request));
                //resolve('--heartbeat--');
            }
        })
    }

    generateGuid() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            const r = Math.random() * 16 | 0;
            const v = c === 'x' ? r : (r & 0x3 | 0x8); // UUID v4
            return v.toString(16);
        });
    }
}
function checkNCALayerHeartbeat(callback) {
    const socket = new WebSocket("wss://127.0.0.1:13579/");

    const timeout = setTimeout(function () {
        socket.close();
        callback(false);
    }, 1500); // 1.5 секунды таймаут

    socket.onopen = function () {
        console.info('on open');
        socket.send("--heartbeat--");
    };

    socket.onmessage = function (event) {
        console.info('event', event.data);
        clearTimeout(timeout);
        socket.close();

        if (event.data.includes("version") || event.data.includes("--heartbeat--")) {
            callback(true);
        } else {
            callback(false);
        }
    };

    socket.onerror = function () {
        console.info('on error');
        clearTimeout(timeout);
        callback(false);
    };
}
