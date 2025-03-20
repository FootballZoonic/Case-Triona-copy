<template>
  <v-container>
    <v-row>
      <v-col cols="12" md="12">
        <v-card flat>
          <LogFileSearch @search="handleSearch"></LogFileSearch>
          <LogComponent :searchParams="searchParams"></LogComponent>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { ref } from 'vue';
import LogFileSearch from '@/components/LogFileSearch.vue';
import LogComponent from '@/components/LogComponent.vue';

export default {
  components: {
    LogFileSearch,
    LogComponent,
  },
  setup() {
    const searchParams = ref({
      caseParam: undefined,
      userParam: undefined,
      actorParam: undefined,
      typeParam: undefined,
      startDateParam: undefined,
      endDateParam: undefined,
    });

    const handleSearch = ({ caseParam, userParam, typeParam, actorParam, startDateParam, endDateParam }) => {
      searchParams.value = { caseParam, userParam, typeParam, actorParam, startDateParam, endDateParam };
    };

    return {
      searchParams,
      handleSearch,
    };
  },
};
</script>
<style></style>