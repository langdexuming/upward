<template>
    <div v-title data-title="工具">
    <ul id="v-for-tools">
        <router-link v-for="item in tools" :to="item.link" class="btn btn-default">
{{item.title}}
        </router-link>
    </ul>
      <!-- 路由匹配到的组件将渲染在这里 -->
            <router-view></router-view>
  </div>
</template>

<script>
export default {
  data() {
    return {
      tools: [
        { link: "/tool/RegEx", title: "正则表达式" },
        { link: "/tool/LetterShift", title: "字母转换" }
      ]
    };
  }
};
</script>

<style scoped>
#v-for-tools {
  font-size: 18;
  background: #ccc;
  padding: 20px 10px;
  margin-bottom: 20px;
}

#v-for-tools .btn {
  margin-right: 10px;
}
</style>
