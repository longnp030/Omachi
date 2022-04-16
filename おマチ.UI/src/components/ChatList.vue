<template>
    <div class="chat__body" id="chat__body" ref="chat__body">
        <chat-message v-for="(msg, index) in msgs"
                      :key="index"
                      :msg="msg"
                      :jwtToken="jwtToken"
                      :prev="[index == 0 ? null : msgs[index - 1]]"
                      :userId="userId">
        </chat-message>
    </div>
</template>

<script>
    import ChatMessage from "@/components/ChatMessage";
    export default {
        components: {
            ChatMessage,
        },
        props: ["msgs", "userId", "jwtToken"],
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
        scroll-behavior: smooth;
        background-color: white;
        max-height: 200px;
    }

    .chat__body::-webkit-scrollbar {
        display: none;
    }
</style>