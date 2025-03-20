<template>
  <v-card flat>
    <v-data-table
      :headers="headers"
      :items="filteredLogFiles"
      :search="search"
    ></v-data-table>
  </v-card>
</template>

<script lang="ts">
import { defineComponent, ref, computed, watch, toRefs } from 'vue';
import { fetchLogFiles } from '@/services/logFileService'; // Import the service function
import { LogFileItemInputOutputActor } from '@/services/ApiClient'; // Adjust the path if necessary

export default defineComponent({
  name: 'LogFileTable',
  props: {
    searchParams: {
      type: Object,
      required: true,
    },
  },
  setup(props) {
    const { searchParams } = toRefs(props);
    const search = ref('');
    const logFiles = ref<LogFileItemInputOutputActor[]>([]); // Initialize as an empty array

    const fetchLogFilesData = async (params: any) => {
      logFiles.value = await fetchLogFiles(params);
    };

    const filteredLogFiles = computed(() => {
      if (!search.value) {
        return logFiles.value;
      }
      return logFiles.value.filter(item =>
        Object.values(item).some(val =>
          String(val).toLowerCase().includes(search.value.toLowerCase())
        )
      );
    });

    watch(searchParams, (newParams) => {
      fetchLogFilesData(newParams); // Fetch data when searchParams change
    }, { deep: true });

    return {
      search,
      logFiles,
      filteredLogFiles,
      fetchLogFilesData, // Expose the fetchLogFilesData method
    };
  },
  data() {
    return {
      headers: [
        { key: 'timestamp', title: 'Timestamp' },
        { key: 'case', title: 'Case' },
        { key: 'userName', title: 'User' },
        { key: 'actor', title: 'Actor' },
        { key: 'type', title: 'Type' },
        { key: 'endpoint', title: 'Endpoint' },
      ],
    };
  },
});
</script>

<style scoped>
/* Lägg till din styling här */
</style>