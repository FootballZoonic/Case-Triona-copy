<template>
  <v-row >
    <v-col>
      <v-card class="justify-center align-center d-flex" style="height: 30rem;">
        <component :is="currentChartComponent" :data="chartData" :options="chartOptions" />
      </v-card>
    </v-col>
  </v-row>
</template>

<script setup>
import { ref, watch, toRefs, computed } from "vue";
import { Line, Bar, Pie, Doughnut, Radar, PolarArea } from 'vue-chartjs';
import { Chart, registerables } from "chart.js";

Chart.register(...registerables);

const props = defineProps({
  chartType: {
    type: String,
    default: 'Line',
    validator: (value) => ['Line', 'Bar', 'Pie', 'Doughnut', 'Radar', 'PolarArea'].includes(value)
  },
  data: {
    type: Object,
    required: true
  },
  options: {
    type: Object,
    required: false
  }
});

const { chartType, data, options } = toRefs(props);
const chartData = ref(data.value);
const chartOptions = ref(options.value || {});

const singleColor = '#c62107';
const multipleColor = ['#c62107', '#65c8e6', '#4d4d4d', '#dfd5bb'];

// Watch for changes in the props data and update chartData accordingly
watch(data, (newData) => {
  chartData.value = newData;
});

watch(options, (newOptions) => {
  chartOptions.value = newOptions;
});

watch(chartType, (newType) => {
  const updateColors = (backgroundColor, borderColor, borderWidth = 2, fill = false) => {
    chartData.value.datasets.forEach(dataset => {
      dataset.backgroundColor = backgroundColor;
      dataset.borderColor = borderColor;
      dataset.borderWidth = borderWidth;
      dataset.fill = fill;
    });
  };

  switch (newType) {
    case 'Bar':
      updateColors(singleColor, singleColor);
      break;
    case 'Pie':
    case 'Doughnut':
      updateColors(multipleColor, undefined, 0);
      break;
    case 'Radar':
      updateColors(singleColor, singleColor, 2, true);
      break;
    case 'PolarArea':
      updateColors(multipleColor);
      break;
    case 'Line':
      updateColors(singleColor, singleColor, 2, false);
      break;
  }
});

const currentChartComponent = computed(() => {
  switch (chartType.value) {
    case 'Bar':
      return Bar;
    case 'Pie':
      return Pie;
    case 'Doughnut':
      return Doughnut;
    case 'Radar':
      return Radar;
    case 'PolarArea':
      return PolarArea;
    default:
      return Line;
  }
});
</script>

<style scoped>
.v-card {
  padding: 16px;
}
</style>