<template>
  <header v-title data-title="Home">
    <!-- <h1>定位我的车</h1> -->
    <h2>发现世界</h2>
    <section class="search col-md-4 col-md-offset-4">
      <form role="search" action="get">
        <div class="input-group">
          <el-input v-model="input" placeholder="输入关键词查找"></el-input>
          <div class="input-group-btn">
            <el-button type="primary normal__margin_left" icon="el-icon-search">搜索</el-button>
          </div>
        </div>
      </form>

      <!-- <ul id="example-1">
        <li v-for="item in searchTags">{{ item.title }}</li>
      </ul>

      <el-row>
        <el-button type="primary" valid-v-for="item in searchTags">{{item.title}}</el-button>
      </el-row>-->

      <el-menu
        :default-active="activeIndex"
        class="el-menu-demo"
        mode="horizontal"
        @select="handleSelect"
      >
        <el-menu-item index="1">
          <router-link tag="tool" :to="searchTags[0].link">工具</router-link>
        </el-menu-item>
        <el-menu-item index="2">
          <router-link tag="blockchain" :to="searchTags[1].link">区块链</router-link>
        </el-menu-item>
        <!-- <el-submenu index="2">
          <template slot="title">区块链</template>
          <el-menu-item index="2-1">节点A</el-menu-item>
          <el-menu-item index="2-2">节点B</el-menu-item>
          <el-menu-item index="2-3">更多</el-menu-item>
          <el-submenu index="2-4">
            <template slot="title">节点管理</template>
            <el-menu-item index="2-4-1">添加</el-menu-item>
            <el-menu-item index="2-4-2">修改</el-menu-item>
            <el-menu-item index="2-4-3">删除</el-menu-item>
          </el-submenu>
        </el-submenu>-->
        <el-menu-item index="3" disabled>车联网</el-menu-item>

        <el-menu-item index="4">
          <a href="#" target="_blank">更多</a>
        </el-menu-item>
      </el-menu>

      <div class="line"></div>
    </section>
  </header>
</template>

<script>
export default {
  data() {
    return {
      input: "",
      searchTags: [
        { link: "/tool", title: "工具" },
        { link: "/BlockChain", title: "区块链" },
        // { link: "/PlatformAccountManage", title: "平台  账号管理" },
        { link: "/iov", title: "车联网" },
        // { link: "/ResourceManage", title: "资源管理" },
        // { link: "/Samples", title: "作品" },
        // { link: "/tool", title: "数据" },
        // { link: "/tool", title: "定位" },
        // { link: "/tool", title: "监测" },
        { link: "/more", title: "更多" }
      ],
      activeIndex: "1",
      activeIndex2: "1"
    };
  },
  methods: {
    handleSelect(key, keyPath) {
      console.log(key, keyPath);
    },
    linkToUrl(url) {}
  }
};
</script>

<style scoped>
.search {
  margin-top: 1em;
}
.search .search__icon {
  font-size: 16px;
  vertical-align: middle;
}

.search__tags {
  display: flex;
  overflow: hidden;
  border-radius: 0 0 3px 3px;
  list-style: none;
  padding: 0px;
}

.search__tags li {
  color: #fff;
  background-color: #555;
  background-size: cover;
  background-position: 50% 50%;
  -webkit-box-flex: 1;
  -ms-flex-positive: 1;
  flex-grow: 1;
  white-space: nowrap;
  padding: 0;
  line-height: 1.3;
}

.search__tags a {
  display: inline-block;
  width: 100%;
  font-size: 13px;
  background: rgba(255, 255, 255, 0.7);
  border-right: 1px solid rgba(0, 0, 0, 0.1);
  padding: 5px 0;
  color: #333;
  text-decoration: none;
  cursor: pointer;
}

.search__tags a:hover {
  background: rgba(255, 255, 255, 0.8);
}

.normal__margin_left {
  margin-left: 0.6em;
}

.normal__margin_right {
  margin-right: 0.6em;
}

.normal__margin_top {
  margin-top: 0.6em;
}

.normal__margin_bottom {
  margin-bottom: 0.6em;
}
</style>
