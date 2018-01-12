<template>
  <div class=" container text-center">
    <div class="row" style="margin-bottom:12px;">
        <div>
            <label>替换表达式</label>
            <input type="text" v-model="inputTextRegex">
            <label>新值</label>
            <input type="text" v-model="outputTextRegex">
            <input type="checkbox" v-model="isGlobalReplace" style="margin-left:24px;">
            <label>全局匹配</label>
        </div>
    </div>
    <div class="row">
        <textarea id="textareaOrigin" class=" col-md-5" v-model="inputText"
        @keyup.enter="replaceText"/>
    <div class=" col-md-2 btn-group-vertical">
          <button class="btn btn-primary" @click="replaceText">替换</button>
          <button class="btn btn-danger" @click="resetText">重置</button>
    </div>
        <textarea id="textareaNew" class=" col-md-5" v-model="outputText"/>
    </div>
</div>
</template>

<script>
export default {
  name: "Tool",
  created() {
    console.log("Tool created");
  },
  data() {
    return {
      inputTextRegex: "//",
      outputTextRegex: "",
      isGlobalReplace: true,
      inputText: "",
      outputText: ""
    };
  },
  methods: {
    replaceText() {
      if (this.isGlobalReplace) {
        this.outputText = this.inputText.replace(
          new RegExp(this.inputTextRegex, "gm"),
          this.outputTextRegex
        );
      } else {
        this.outputText = this.inputText.replace(
          this.inputTextRegex,
          this.outputTextRegex
        );
      }
    },
    resetText() {
      this.inputText = "";
      this.outputText = "";
    }
  }
};
</script>

<style scoped>
textarea {
  height: 240px;
}

.btn-group-vertical {
  padding: 0px;
}

.btn-group-vertical .btn {
  border-radius: 0 !important;
  margin-bottom: 20px;
}
</style>
