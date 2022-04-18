<template>
  <v-app>
    <v-main>
      <v-card>
        <v-layout>
          <v-navigation-drawer
            color="#273C75"
            class="bg-netcensus"
            expand-on-hover
            rail
          >
            <v-list class="bg-netcensus">
              <v-list-item
                :prepend-avatar="require('./assets/icon.png')"
                title="NetCensus"
                :subtitle="version"
              ></v-list-item>
            </v-list>

            <v-divider></v-divider>

            <v-list class="bg-netcensus ml-2" nav>
              <v-list-item
                href="/"
                prepend-icon="mdi-desktop-classic"
                title="Machines"
                value="machines"
              ></v-list-item>
              <v-list-item
                prepend-icon="mdi-star"
                href="/Starred"
                title="Starred"
                value="starred"
              ></v-list-item>
              <v-list-item
                href="/About"
                prepend-icon="mdi-information"
                title="About"
                value="about"
              ></v-list-item>
            </v-list>
          </v-navigation-drawer>
          <v-main style="height: 100vh">
            <img
              :class="'float-right mr-2 ' + logoCurrentClass"
              :src="logoCurrentImage"
            />
            <router-view />
          </v-main>
        </v-layout>
      </v-card>
    </v-main>
  </v-app>
</template>
<style scoped>
.logo-netcensus-loading {
  width: 150px;
  position: absolute;
  right: 0;
  bottom: 0;
  margin-bottom: 15px;
  opacity: 1;
}
.logo-netcensus {
  width: 150px;
  position: absolute;
  right: 0;
  bottom: 0;
  margin-bottom: 15px;
  opacity: 0.5;
}
.bg-netcensus {
  background-color: #273c75 !important;
  color: #fff !important;
}
</style>
<script>
export default {
  name: "App",
  data: () => ({
    logoCurrentImage: require("./assets/logo.png"),
    logoCurrentClass: "logo-netcensus",
    version: "1.0.0-alpha",
  }),
  methods: {
    showLoading: function () {
      this.logoCurrentClass = "logo-netcensus-loading";
      this.logoCurrentImage = require("./assets/loading.gif");
    },
    hideLoading: function () {
      this.logoCurrentClass = "logo-netcensus";
      this.logoCurrentImage = require("./assets/logo.png");
    },
  },
  mounted() {
    this.$emitter.on("showLoading", () => {
      this.showLoading();
    });

    this.$emitter.on("hideLoading", () => {
      this.hideLoading();
    });
  },
};
</script>
