<template>
  <v-card v-if="content" color="#222f3e" class="text-white">
    <v-card-header>
      <v-card-header-text>
        <v-icon
          v-if="currentStatus == 'up'"
          icon="mdi-arrow-up-bold"
          size="32"
          color="green"
          class="float-right"
        ></v-icon>
        <v-icon
          v-if="currentStatus == 'down'"
          icon="mdi-arrow-down-bold"
          size="32"
          color="red"
          class="float-right"
        ></v-icon>
        <v-card-title class="text-h5">{{ content.machine }}</v-card-title>
        <v-card-subtitle> {{ currentStatus }}</v-card-subtitle>
        <v-card-subtitle style="margin-top: -10px"
          ><small>{{ lastRecord.dateTimeRegistered }}</small></v-card-subtitle
        >
      </v-card-header-text>
    </v-card-header>

    <v-list-item
      v-bind:key="cpu"
      v-for="cpu in JSON.parse(lastRecord.cpuUsage)"
      density="compact"
      style="margin-top: -10px"
    >
      <v-list-item-avatar left>
        <v-icon icon="mdi-cpu-64-bit" color="white"></v-icon>
      </v-list-item-avatar>
      <v-list-item-subtitle>{{ cpu }}% - CPU Usage</v-list-item-subtitle>
    </v-list-item>

    <v-list-item density="compact" style="margin-top: -25px">
      <v-list-item-avatar left>
        <v-icon icon="mdi-memory" color="white"></v-icon>
      </v-list-item-avatar>
      <v-list-item-subtitle
        >{{ (Math.round(lastRecord.avaiableRam * 100) / 100).toFixed(2) }}/{{
          (Math.round(lastRecord.totalRam * 100) / 100).toFixed(2)
        }}GB - RAM Usage</v-list-item-subtitle
      >
    </v-list-item>

    <v-expand-transition>
      <div v-if="expand">
        <v-list style="background-color: #4b627d !important" class="text-white">
          <v-list-item
            v-bind:key="ip"
            v-for="ip in JSON.parse(lastRecord.ipAddresses)"
            :title="ip"
            append-icon="mdi-ip"
            subtitle="IP"
          >
          </v-list-item>
          <v-list-item
            :title="
              (Math.round(lastRecord.avaiableStorage * 100) / 100).toFixed(2) +
              ' of ' +
              (Math.round(lastRecord.totalStorage * 100) / 100).toFixed(2) +
              ' GB'
            "
            append-icon="mdi-server"
            subtitle="Storage"
          >
          </v-list-item>
        </v-list>
      </div>
    </v-expand-transition>

    <v-divider></v-divider>

    <v-card-actions>
      <v-btn @click="expand = !expand"> See more </v-btn>
      <v-spacer></v-spacer>
      <v-btn small @click="toggleFavorite(content.machine)">
        <v-icon v-if="favorites.includes(content.machine)" color="orange"
          >mdi-star</v-icon
        >
        <v-icon v-else color="orange">mdi-star-outline</v-icon>
      </v-btn>
    </v-card-actions>
  </v-card>
</template>
<style scoped>
.v-list-item-subtitle {
  opacity: 1;
}
</style>
<script>
export default {
  name: "machineComponent",
  props: ["content"],
  data: () => ({
    expand: false,
    favorites: [],
    currentStatus: "down",
    lastDate: "",
    lastRecord: "",
  }),
  methods: {
    removeItemOnce: function (arr, value) {
      return arr.filter(function (item) {
        return item !== value;
      });
    },
    toggleFavorite: function (nickname) {
      var favorites = this.$cookies.get("netcensus-favorites");
      console.log(favorites);
      if (favorites == null) {
        favorites = [];
        favorites.push(nickname);
        this.$cookies.set("netcensus-favorites", JSON.stringify(favorites), -1);
      } else {
        favorites = JSON.parse(favorites);
        var contained = favorites.includes(nickname);

        //If it includes, then the user wishes to remove it
        if (contained) {
          for (let index = 0; index < favorites.length; index++) {
            if (favorites[index] == nickname) {
              contained = true;
              favorites = this.removeItemOnce(favorites, favorites[index]);
            }
          }
        }

        //If it doesnt includes, then the user wishes to add to it
        if (!contained) {
          favorites.push(nickname);
        }

        this.$cookies.set("netcensus-favorites", JSON.stringify(favorites), -1);

        if (favorites.count == 0) {
          favorites = [];
        }

        this.favorites = favorites;
      }
    },
  },
  mounted() {
    this.favorites = this.$cookies.get("netcensus-favorites");
  },
  created() {
    this.lastRecord = this.content.stats[this.content.stats.length - 1];

    var dateTime = new Date(this.lastRecord.dateTimeRegistered);

    var dateNow = new Date();

    if (
      dateTime.getDate() == dateNow.getDate() &&
      dateTime.getMonth() == dateNow.getMonth() &&
      dateTime.getFullYear() == dateNow.getFullYear()
    ) {
      if (dateNow.getHours() - dateTime.getHours() == 0) {
        if (dateNow.getMinutes() - dateTime.getMinutes() <= 3) {
          this.currentStatus = "up";
        } else {
          this.currentStatus = "down";
        }
      } else {
        this.currentStatus = "down";
      }
    } else {
      this.currentStatus = "down";
    }
  },
};
</script>
