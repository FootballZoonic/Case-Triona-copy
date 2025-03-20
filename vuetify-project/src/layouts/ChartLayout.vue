<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <LogFileSearch @search="handleSearch" />
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" sm="6" md="3">
        <DropDownMenu :options="chartOptions" @update:selectedOption="handleChartOptionChange" />
      </v-col>
      <v-col cols="12" sm="6" md="3">
        <DropDownMenu :options="chartTypes" @update:selectedOption="handleChartTypeChange" />
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12">
        <Chart :chartType="selectedChartType" :data="chartData" :options="chartOptionsRef" />
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'; 
import LogFileSearch from '@/components/LogFileSearch.vue';
import DropDownMenu from '@/components/DropDownMenu.vue';
import Chart from '@/components/Chart.vue';
import { ApiClient, LogFileItemInputOutputActor } from '@/services/ApiClient';
import { countUsernameMatches, prepareUserCountChartData, prepareHourlyActionsData, ChartData, prepareMonthlyActionsData, prepareDailyActionsData } from '@/services/ChartUtils';

export default defineComponent({
  name: 'MainLayout',
  components: {
    LogFileSearch,
    DropDownMenu,
    Chart
  },
  setup() {

    const data = ref<LogFileItemInputOutputActor[]>([]);
    const chartOptions = ref(['Actions per Hour', 'Actions per Month', 'Actions per Day', 'Actions per User']);
    const chartTypes = ref(['Bar', 'Line', 'Pie', 'Doughnut', 'Radar', 'PolarArea']);
    const selectedChartOption = ref(chartOptions.value[0]);
    const selectedChartType = ref(chartTypes.value[0]);
    const chartData = ref<ChartData>({ labels: [], datasets: [] });
    const chartOptionsRef = ref({});
    const showCharts = ref(false);

    const apiClient = new ApiClient('http://localhost:5268');

    const searchParams = ref({
      caseParam: undefined,
      userParam: undefined,
      actorParam: undefined,
      typeParam: undefined,
      startDateParam: undefined,
      endDateParam: undefined,
    });

    const handleSearch = (params) => {
      searchParams.value = params;
    };

    const fetchLogFiles = async (params) => {
      try {
        const { caseParam, userParam, actorParam, typeParam, startDateParam, endDateParam } = params;
        const fetchedData = await apiClient.searchAll(caseParam, userParam, actorParam, typeParam, startDateParam, endDateParam);
        data.value = fetchedData;
        console.log('Fetched Data:', fetchedData);

        const filteredData = data.value.filter(item => {
          const matchesCase = caseParam ? item.case?.includes(caseParam) : true;
          const matchesUser = userParam ? item.userName?.toLowerCase().includes(userParam.toLowerCase()) : true;
          const matchesActor = actorParam ? item.actor?.toLowerCase().includes(actorParam.toLowerCase()) : true;
          const matchesType = typeParam ? item.type?.includes(typeParam) : true;
          const matchesStartDate = startDateParam ? new Date(item.timestamp) >= new Date(startDateParam) : true;
          const matchesEndDate = endDateParam ? new Date(item.timestamp) <= new Date(endDateParam) : true;
          return matchesCase && matchesUser && matchesActor && matchesType && matchesStartDate && matchesEndDate;
        });

        if (selectedChartOption.value == 'Actions per Hour') {
          chartData.value = prepareHourlyActionsData(filteredData, userParam);

        }
        else if (selectedChartOption.value == 'Actions per Month') {
          chartData.value = prepareMonthlyActionsData(filteredData, userParam);
        }
        else if (selectedChartOption.value == 'Actions per User') {
          const usernameCounts = countUsernameMatches(filteredData);
          chartData.value = prepareUserCountChartData(usernameCounts);
        }
        else if (selectedChartOption.value == 'Actions per Day') {
          chartData.value = prepareDailyActionsData(filteredData, userParam);
        }


        showCharts.value = true; // Show the charts after search
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

   

    const handleChartOptionChange = (option) => {
      selectedChartOption.value = option;
    };

    const handleChartTypeChange = (type) => {
      selectedChartType.value = type;
    };


    watch(searchParams, (newParams) => {
      fetchLogFiles(newParams); // Fetch data when searchParams change
    }, { deep: true });

    return {
      chartOptions,
      chartTypes,
      selectedChartOption,
      selectedChartType,
      chartData,
      chartOptionsRef,
      showCharts,
      handleSearch,
      handleChartOptionChange,
      handleChartTypeChange
    };
  }
});
</script>

<style scoped>
.v-container {
  padding: 16px;
}
</style>