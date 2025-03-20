<template>
    <v-card flat>
      <v-row>
        <v-col cols="6">
          <v-select v-model="selectedChartOption" :items="chartOptions" label="Select Chart Option" @change="emitChartOption"></v-select>
        </v-col>
        <v-col cols="6">
          <v-select v-model="selectedChartType" :items="chartTypes" label="Select Chart Type" @change="emitChartType"></v-select>
        </v-col>
      </v-row>
    </v-card>
  </template>
  
  <script lang="ts">
  import { defineComponent, ref, watch, computed } from 'vue';
  import { Line, Bar, Pie, Doughnut, Radar, PolarArea } from 'vue-chartjs';
  import { Chart, registerables } from "chart.js";
  
  Chart.register(...registerables);
  
  export default defineComponent({
    props: {
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
    },
    setup(props, { emit }) {
      const chartTypes = ref(['Bar', 'Line', 'Pie', 'Doughnut', 'Radar', 'PolarArea']);
      const chartOptions = ref(['Actions per Hour', 'Actions per Month', 'Actions per Day', 'Actions per User']);
      const selectedChartOption = ref(chartOptions.value[0]);
      const selectedChartType = ref(chartTypes.value[0]);
  
      const { chartType, data, options } = toRefs(props);
      const chartData = ref(data.value);
      const chartOptionsRef = ref(options.value || {});
      const singleColor = '#c62107';
      const multipleColor = ['#c62107', '#65c8e6', '#4d4d4d', '#dfd5bb'];
  
      watch(data, (newData) => {
      chartData.value = newData;
      updateColorsBasedOnChartType();
    });
  
      watch(options, (newOptions) => {
        chartOptionsRef.value = newOptions;
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
  
      const emitChartOption = () => {
        emit('update:chartOption', selectedChartOption.value);
      };
  
      const emitChartType = () => {
        emit('update:chartType', selectedChartType.value);
      };
  
      return {
        chartTypes,
        chartOptions,
        selectedChartOption,
        selectedChartType,
        currentChartComponent,
        chartData,
        chartOptionsRef,
        emitChartOption,
        emitChartType
      };
    }
  });
  </script>
  
  <style scoped>
  .v-card {
    padding: 16px;
  }
  
  .v-select {
    margin-bottom: 16px;
  }
  </style>