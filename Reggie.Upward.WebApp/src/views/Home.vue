<template>
<header v-title data-title="Home">
    <!-- <h1>定位我的车</h1> -->
    <h2>发现世界</h2>
  <section class="search col-md-4 col-md-offset-4">
      <form role="search" action="get">
          <div class="input-group">
              <input type="text" class="form-control" aria-label="输入关键词查找" placeholder="输入关键词查找">
              <div class="input-group-btn">
                  <button class="btn">
                      <span class="search__icon glyphicon glyphicon-search"></span>
                  </button>
                  </div>
                  </div>
      </form>
        <ul class="search__tags">
           <li v-for="item in searchTags">
                  <router-link :to="item.link">
{{item.title}}
        </router-link></li>
            </ul>
  </section>
</header>

</template>

<script>
export default {
  data() {
    return {
      searchTags: [
        { link: "/tool", title: "工具" },
        { link: "/PlatformAccountManage", title: "平台  账号管理" },
        { link: "/tool", title: "数据" },
        { link: "/tool", title: "定位" },
        { link: "/tool", title: "监测" },
        { link: "/tool", title: "更多" }
      ]
    };
  }
};
</script>

<style scoped>
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
</style>
