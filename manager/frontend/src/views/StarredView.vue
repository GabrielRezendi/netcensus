<template>
  <div v-if="showEmpty" class="mx-5">
    <v-icon size="128" color="orange">mdi-star</v-icon>
    <h1>You have yet to star a machine.</h1>
    <p>Go in 'Machines' and give a star to the ones that you are particularly interested and they will appear in this page.</p>
  </div>
  <div v-else class="mx-5">
    <h1 class="mt-5">
      <v-icon>mdi-desktop-classic</v-icon> Starred Machines
      <span style="font-weight: 350">visualization</span>
    </h1>
    <p>
      See below the machines that are active and were active and you have
      starred.
    </p>
    <br />
    <v-row>
      <v-col v-bind:key="machine" v-for="machine in machines" md="3">
        <machine-component v-if="machine" :content="machine" />
      </v-col>
    </v-row>
  </div>
</template>

<script>
import { defineComponent } from "vue";
// Components
import machineComponent from "../components/machineComponent.vue";
export default defineComponent({
  name: "StarredView",
  data: () => ({
    showEmpty: false,
    machines: [],
  }),
  components: {
    machineComponent,
  },
  methods: {
    getMachines: function () {
      var favorites = JSON.parse(this.$cookies.get("netcensus-favorites"));
      this.$emitter.emit("showLoading");
      this.$axios
        .get(process.env.VUE_APP_API_ADDRESS + "Machines", {
          params: {
            search: favorites,
          },
        })
        .then((response) => {
          this.machines = response.data;
          this.$emitter.emit("hideLoading");
        });
    },
  },
  mounted() {
    if (
      this.$cookies.get("netcensus-favorites") == "[]" ||
      this.$cookies.get("netcensus-favorites") == null
    ) {
      this.showEmpty = true;
    } else {
      this.getMachines();

      setInterval(() => {
        this.getMachines();
      }, 30000);
    }
  },
});
</script>
