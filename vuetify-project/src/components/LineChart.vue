<template>
  <v-card class="pa-6 mb-2 elevation-4" style="width: 100%">
    <Line :data="chartData" />
  </v-card>
</template>

<script setup>
import { ref, watch } from "vue";
import { Line } from "vue-chartjs";
import { Chart, registerables } from "chart.js";

Chart.register(...registerables);

const props = defineProps({
  data: {
    type: Object,
    required: true
  }
});

const chartData = ref(props.data);

// Watch for changes in the props data and update chartData accordingly
watch(() => props.data, (newData) => {
  chartData.value = newData;
});
</script>

<style scoped>
/* Add any custom styles here */
</style>