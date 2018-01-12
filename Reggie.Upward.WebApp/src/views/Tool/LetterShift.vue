<template>
    <div class=" container text-center" v-title data-title="字母转换">
        <div id="convertTypes">
          <template v-for="item in convertTypes" >
                   <input type="radio" :id="item.id" :value="item.id" v-model="selectedConvertTypes" class="sr-only">
                                              <label :for="item.id" class="radio-inline text-info">
                                                                    {{item.name}}
              </label>
          </template>
        </div>
        <div class="row text-center" style="height:40px;vertical-align:middle;">
              <label>分隔符：</label>
                <select v-model="inputWordSeparator">
                  <option v-for="item in wordSeparators" :value="item.value">{{item.name}}</option>
               </select>
               ->
               <select v-model="outputWordSeparator">
                 <option v-for="item in wordSeparators" :value="item.value">{{item.name}}</option>
               </select>
        </div>
        <div class="row">
            <textarea id="textareaOrigin" class=" col-md-5" v-model="inputText" placeholder="请输入要转换的单词或句子"
            @keyup.enter="translateText"/>
            <button class="btn btn-primary col-md-2" @click="translateText">转换</button>
            <textarea id="textareaNew" class=" col-md-5" v-model="outputText"/>
        </div>
    </div>
</template>

<script>
export default {
  data() {
    return {
      inputText: "",
      outputText: "",
      inputWordSeparator: " ",
      outputWordSeparator: "",
      wordSeparators: [
        { name: "空格", value: " " },
        { name: ",", value: "," },
        { name: "_", value: "_" },
        { name: "无", value: "" }
      ],
      convertTypes: [
        { id: 0, typename: "ToUpper", name: "转大写" },
        { id: 1, typename: "ToLower", name: "转小写" },
        { id: 2, typename: "ToPascal", name: "Pascal命名" },
        { id: 3, typename: "ToCamelCase", name: "Camel-Case命名" }
      ],
      selectedConvertTypes: 0,
      isGeneratedResult: false
    };
  },
  watch: {
    inputText: function(val) {
      this.isGeneratedResult = false;
    },
    selectedConvertTypes: function(val) {
      if (this.isGeneratedResult) {
        this.translateText();
      }
    }
  },
  methods: {
    translateText() {
      let result = "";
      var input = this.inputText;
      var inputWS = this.inputWordSeparator;
      var outputWS = this.outputWordSeparator;
      var inputRegex = new RegExp(inputWS, "gm");
      var outputRegex = outputWS;
      switch (this.selectedConvertTypes) {
        case 0:
          result = input.toLocaleUpperCase().replace(inputRegex, outputRegex);
          break;
        case 1:
          result = input.toLocaleLowerCase().replace(inputRegex, outputRegex);
          break;
        case 2:
          var array = input.split(inputWS);
          array.forEach((value, number, array) => {
            result +=
              value.substring(0, 1).toUpperCase() +
              value.substring(1).toLocaleLowerCase();
            if (number !== array.length - 1) {
              result += outputWS;
            }
          });
          break;
        case 3:
          var array = input.split(inputWS);
          array.forEach((value, number, array) => {
            if (number === 0) {
              result += value.toLocaleLowerCase();
            } else {
              result +=
                value.substring(0, 1).toUpperCase() +
                value.substring(1).toLocaleLowerCase();
            }

            if (number !== array.length - 1) {
              result += outputWS;
            }
          });
          break;
        default:
      }

      this.outputText = result;
      this.isGeneratedResult = true;
    }
  }
};
</script>

<style scoped>
textarea {
  height: 240px;
}

#convertTypes {
  font-size: 16px;
  margin-bottom: 10px;
}

#convertTypes label {
  padding: 5px 10px;
  margin-right: 10px;
  border: 2px solid transparent;
}

#convertTypes label:hover {
  border: 2px solid #555;
}

#convertTypes label:active {
  background: #eee;
}

input[type="radio"]:checked + label {
  background: #777;
  color: #222;
}

.btn {
  border-radius: 0;
}
</style>
