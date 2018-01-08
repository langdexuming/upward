<template>
  <div v-title data-title="Car" class=" container">
    <ul>
        <li v-for="item in brands">
          {{ item.brandName }}
        </li>
      </ul>
  </div>
</template>

<script>
import { getBrands } from "../api/api";

export default {
  name: "Car",
  created() {
    this.loadBrands();
  },
  data() {
    return {
      brands: []
    };
  },
  methods: {
    loadBrands() {
      let _this = this;
      getBrands().then(res => {
        let { statusCode, message, data } = res.data;
        if (statusCode !== 200) {
          console.log("error");
        } else {
          for (var i = 0; i < data.length; i++) {
            console.log(data[i].brandName);
          }
          _this.brands = data;
        }
      });
    }
  }
};
</script>

<style scoped>

</style>



