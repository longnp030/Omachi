<template>
    <div class="chat">
        <div class="chat__header">
            <span class="chat__header__greetings">
                Cửa sổ trò chuyện
            </span>

            <b-icon icon="dash" class="minimize-chat-btn" @click="minimize=!minimize" v-if="!minimize"></b-icon>
            <b-icon icon="plus" class="minimize-chat-btn" @click="minimize=!minimize" v-else></b-icon>
        </div>
        <chat-list :msgs="messages"
                   :userId="userId"
                   :jwtToken="jwtToken"
                   v-if="!minimize"></chat-list>
        <chat-form @submitMessage="sendMessage"
                   :userId="userId"
                   :jwtToken="jwtToken"
                   v-if="!minimize"></chat-form>
    </div>
</template>

<script>
    import ChatList from "@/components/ChatList";
    import ChatForm from "@/components/ChatForm";

    export default {
        components: {
            ChatList,
            ChatForm,
        },
        props: ["userId", "jwtToken"],
        data() {
            return {
                messages: [],
                minimize: true,
            };
        },
        created() {
            this.$chatHub.$on('message-received', this.onMessageReceived);
        },
        methods: {
            sendMessage(msg) {
                this.messages.push(msg);
            },

            onMessageReceived({ userId, message }) {
                var newMsg = { userId, message };
                this.messages.push(newMsg);
            },
        },
    };
</script>

<style scoped>
    .chat {
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        border-radius: 4px;
        z-index: 800;
        margin: 0 10px 0px 0;
        align-self: end;
        width: 210px;
    }

    .chat__header {
        color: #ffffff;
        box-shadow: 0px 3px 10px rgba(0, 0, 0, 0.05);
        border-radius: 4px 4px 0 0;
        padding: 4px 12px;
        font-size: 16px;
        background-color: #006688;
        display: flex;
        justify-content: space-between;
    }

    .minimize-chat-btn {
        cursor: pointer;
        margin-top: 4px;
    }
</style>