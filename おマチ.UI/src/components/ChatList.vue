<template>
    <div class="chat__body" id="chat__body" ref="chat__body">
        <chat-message v-for="(msg, index) in msgs"
                      :key="index"
                      :msg="msg"
                      :prev="[index == 0 ? null : msgs[index - 1]]"
                      :user_id="user_id">
        </chat-message>
    </div>
</template>

<script>
    import ChatMessage from "@/components/ChatMessage";
    export default {
        components: {
            ChatMessage,
        },
        props: ["msgs", "user_id"],
        watch: {
            msgs() {
                var chat_body = this.$refs.chat__body;
                chat_body.scrollTop = chat_body.scrollHeight;
                console.log(this.msgs);
            }
        }
    };
</script>

<style scoped>
    .chat__body {
        padding: 12px;
        overflow: auto;
        max-height: 300px;
        scroll-behavior: smooth;
        background-color: white;
    }

    .chat__body::-webkit-scrollbar {
        display: none;
    }
</style>