<template>
    <div class="chat">
        <div class="chat__header">
            <span class="chat__header__greetings">
                Cửa sổ trò chuyện
            </span>
        </div>
        <chat-list :msgs="messages"
                   :user_id="user_id"></chat-list>
        <chat-form @submitMessage="sendMessage"
                   :user_id="user_id"
                   :jwt_token="jwt_token"></chat-form>
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
        props: {
            user_id: {
                type: String,
                required: true,
                default: ''
            },
            jwt_token: {
                type: String,
                required: true,
                default: '',
            },
        },
        data() {
            return {
                messages: [],
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
        watch: {
            user_id: function () {
                console.log(this.user_id);
            },
            jwt_token: function () {
                console.log(this.jwt_token);
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
        width: 200px;
    }

    .chat__header {
        background: #ffffff;
        box-shadow: 0px 3px 10px rgba(0, 0, 0, 0.05);
        border-radius: 4px;
        padding: 12px;
        font-size: 16px;
        background-color: #006688;
    }

    .chat__header__greetings {
        color: #292929;
    }
</style>