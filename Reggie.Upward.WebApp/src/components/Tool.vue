<template>
  <div class=" container text-center">
    <div class="row" style="max-width:960px;margin-bottom:12px;">
        <div>
            <label>替换表达式</label>
            <input type="text" v-model="inputTextRegex">
            <label>新值</label>
            <input type="text" v-model="outputTextRegex">
            <input type="checkbox" v-model="isGlobalReplace" style="margin-left:24px;">
            <label>全局匹配</label>
        </div>
    </div>
    <div class="row" style="max-width:960px;">
        <textarea id="textareaOrigin" class=" col-md-5" v-model="inputText"/>
        <button type="button" class="btn btn-primary col-md-2" style="max-width:96px;margin:0px 12px;vertical-align:middle;" @click="replaceText">替换</button>
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
    }
  }
};
</script>

<style scoped>
textarea {
  height: 240px;
}
</style>
