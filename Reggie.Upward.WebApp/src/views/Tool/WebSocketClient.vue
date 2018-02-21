<template>
      <div v-title data-title="WebSocket客户端">
          <input type="text" v-model="WSUrl" placeholder="输入服务端地址">
          <input type="text" v-model="SendText" placeholder="输入要发送的文本">
          <button class="btn btn-primary" @click="websocketsend()">发送</button>
          <p>{{ReceiveText}}</p>
      </div>
</template>

<script>
export default {
  data() {
    return {
      websocket: null,
      SendText: "",
      ReceiveText: "",
      WSUrl: "ws://localhost:5001"
    };
  },
  methods: {
    initWebSocket() {
      //初始化weosocket
      //ws地址
      var wsuri = this.WSUrl;
      this.websocket = new WebSocket(wsuri);
      this.websocket.onmessage = this.websocketonmessage;
      this.websocket.onclose = this.websocketclose;
    },
    websocketsend() {
      //数据发送
      this.websocket.send(this.SendText);
    },
    websocketonmessage(e) {
      //数据接收
      this.ReceiveText = "接收文本:" + e.data;
      console.log(e.data);
    },
    websocketclose(e) {
      //关闭
      console.log("connection closed (" + e.code + ")");
    }
  },
  created() {
    this.initWebSocket();
  }
};
</script>

<style scoped>

</style>
