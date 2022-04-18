<template>
  <div class="mx-5">
    <h1 class="mt-5">
      <v-icon>mdi-desktop-classic</v-icon> Machines
      <span style="font-weight: 350">visualization</span>
    </h1>
    <p>See below the machines that are active and were active.</p>
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
  name: "HomeView",
  data: () => ({
    machines: [],
  }),
  components: {
    machineComponent,
  },
  methods: {
    getMachines: function () {
      this.$emitter.emit("showLoading");
      this.$axios
        .get(process.env.VUE_APP_API_ADDRESS + "Machines")
        .then((response) => {
          this.machines = response.data;
          this.$emitter.emit("hideLoading");
        });
    },
  },
  mounted() {
    this.getMachines();

    setInterval(() => {
      this.getMachines();
    }, 30000);
  },
});
</script>
