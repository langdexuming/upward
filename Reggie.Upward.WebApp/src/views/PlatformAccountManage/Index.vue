<template>
  <div v-title data-title="平台账号管理">
    <div class="row container">
      <nav class="navbar navbar-nav navbar-fixed-top">
        <div class=" navbar-brand">平台账号管理</div>
        <div id="user-info" class=" navbar-text pull-right">{{currentUser.name}}</div>
      </nav>

    <div class="content-wrapper">
    <ul id="platform-list" class="list-group col-md-3">
      <li class="list-group-item" v-for="item in platforms">
        <router-link :to="item.name">{{item.name}}</router-link>
        </li>
    </ul>
    <div class="main-content">
      <div class="poster">
        <!-- <img > -->
        <h2>多平台，加密，安全</h2>
        <p>支持多平台账户储存，采用XX加密技术</p>
      </div>
    </div>
    <div >

    </div>
  </div>

    </div>
  </div>
</template>

<script>
import { getPlatforms } from "../../api/api";

export default {
  created() {
    this.loadPlatforms();
  },
  data() {
    return {
      platforms: [{ id: 0, name: "平台1" }, { id: 1, name: "平台2" }],
      currentUser: { name: "tiky" }
    };
  },
  methods: {
    loadPlatforms() {
      let _this = this;
      getPlatforms().then(res => {
        let { status, statusText, data } = res;
        if (status !== 200) {
          console.log(status);
        } else {
          for (var i = 0; i < data.length; i++) {
            console.log(data[i].name);
          }
          _this.platforms = data;
        }
      });
    }
  }
};
</script>

<style scoped>
.navbar {
  background: #a0a0a0;
}
.navbar .navbar-brand {
  margin-left: 48px;
}
.content-wrapper {
  margin-top: 50px;
}

#platform-list {
}

.main-content {
  height: 100%;
  width: 100%;
}
.main-content .poster {
  padding-top: 12px;
}
</style>
