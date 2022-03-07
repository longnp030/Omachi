import {
    HubConnectionBuilder,
    LogLevel,
} from '@microsoft/signalr';
import Vue from 'vue';

export default {
    install() {
        const connection = new HubConnectionBuilder()
            .withUrl('https://localhost:5001/chatsocket')
            .configureLogging(LogLevel.Information)
            .build();

        const chatHub = new Vue();
        Vue.prototype.$chatHub = chatHub;
        connection.on("SendMessage", (userId, message) => {
            chatHub.$emit('message-received', { userId, message })
        });

        let startedPromise = null
        function start() {
            startedPromise = connection.start().catch(err => {
                console.error('Failed to connect with hub', err)
                return new Promise((resolve, reject) =>
                    setTimeout(() => start().then(resolve).catch(reject), 5000))
            })
            return startedPromise
        }
        connection.onclose(() => start());

        start();
    }
};