<template>
  <div v-title data-title="资源管理" class="container">
    <h1>资源管理</h1>
    <form id="myform">
      <label>上传文件：</label>
      <input type="file" name="fileup" id="fileup" v-on:change="fileChange($event)">
    </form>
    <label>{{filePath}}</label>
  </div>
</template>

<script>
export default {
  data() {
    return {
      filePath: ""
    };
  },
  methods: {
    fileChange: function(el) {
      if (!el.target.files[0].size) return;

      var obj = new FormData(document.getElementById("myform"));
      obj.append("name", "wzh");
      var _this = this;
      $.ajax({
        type: "post",
        url: "http://localhost:5001/api/FileModels",
        data: obj,
        cache: false,
        processData: false, // 不处理发送的数据，因为data值是Formdata对象，不需要对数据做处理
        contentType: false, // 不设置Content-type请求头
        success: function(res) {
          var arr = res.split(":");
          if (arr[0] == "ok") {
            _this.img = arr[1];
          } else {
            alert(arr[1]);
          }
        }
      });
    }
  }
};
</script>

<style scoped>
</style>
