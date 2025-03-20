<template>
  <v-card flat>
    <v-row class="d-flex justify-center align-center">
      <v-col cols="12" sm="6" md="3">
        <v-text-field v-model="caseParam" label="Case" prepend-inner-icon="mdi-magnify" variant="outlined" hide-details
          dense @keyup.enter="search" class="search-field"></v-text-field>
      </v-col>
      <v-col cols="12" sm="6" md="3">
        <v-text-field v-model="userParam" label="User" prepend-inner-icon="mdi-magnify" variant="outlined" hide-details
          dense @keyup.enter="search" class="search-field"></v-text-field>
      </v-col>
      <v-col cols="12" sm="6" md="3">
        <v-text-field v-model="actorParam" label="Actor" prepend-inner-icon="mdi-magnify" variant="outlined" hide-details
          dense @keyup.enter="search" class="search-field"></v-text-field>
      </v-col>
      <v-col cols="12" sm="6" md="3">
        <v-select v-model="typeParam" :items="typeOptions" label="Type" prepend-inner-icon="mdi-magnify"
          variant="outlined" hide-details dense class="search-field"></v-select>
      </v-col>
    </v-row>
    <v-row class="d-flex justify-center align-center">
      <v-col cols="12" sm="6" md="3">
        <v-btn class="button" @click="showStartDate" block>
          {{ startDateText }}
          <template v-slot:append>
            <v-icon v-if="startDate" small @click.stop="clearStartDate" color="#c62107">mdi-close</v-icon>
          </template>
        </v-btn>
      </v-col>
      <v-col cols="12" sm="6" md="3">
        <v-btn class="button" @click="showEndDate" block>
          {{ endDateText }}
          <template v-slot:append>
            <v-icon v-if="endDate" small @click.stop="clearEndDate" color="#c62107">mdi-close</v-icon>
          </template>
        </v-btn>
      </v-col>
      <v-col cols="12" sm="6" md="3">
        <v-btn class="button" @click="clear" block>Clear</v-btn>
      </v-col>
      <v-col cols="12" sm="6" md="3">
        <v-btn class="button" @click="search" block>Search</v-btn>
      </v-col>
    </v-row>
    <v-dialog v-model="showStartDatePicker" max-width="290">
      <v-date-picker v-model="startDate" @input="showStartDatePicker = false"></v-date-picker>
    </v-dialog>
    <v-dialog v-model="showEndDatePicker" max-width="290">
      <v-date-picker v-model="endDate" @input="showEndDatePicker = false"></v-date-picker>
    </v-dialog>
  </v-card>
</template>


<script lang="ts">
import { defineComponent, ref, computed } from 'vue';

export default defineComponent({
  name: 'SearchComponent',
  props: {
    searchParams: {
      type: Object,
    },
  },
  setup(props, { emit }) {
    const caseParam = ref('');
    const userParam = ref('');
    const actorParam = ref('');
    const typeParam = ref('');
    const startDate = ref<Date | null>(null);
    const endDate = ref<Date | null>(null);
    const showStartDatePicker = ref(false);
    const showEndDatePicker = ref(false);
    const typeOptions = ['', '[CALL]', '[RES]', '[INFO]', '[WARN]'];

    const startDateText = computed(() => {
      return startDate.value ? startDate.value.toLocaleDateString() : 'Start Date';
    });

    const endDateText = computed(() => {
      return endDate.value ? endDate.value.toLocaleDateString() : 'End Date';
    });

    const search = () => {
      const startDateDateTime = startDate.value ? new Date(startDate.value) : undefined;
      const endDateDateTime = endDate.value ? new Date(endDate.value) : undefined;

      if (endDateDateTime) {
        endDateDateTime.setHours(23, 59, 59, 999); // Set time to 23:59:59.999
      }

      emit('search', {
        caseParam: caseParam.value.toUpperCase(),
        userParam: userParam.value.toUpperCase(),
        actorParam: actorParam.value,
        typeParam: typeParam.value,
        startDateParam: startDateDateTime,
        endDateParam: endDateDateTime,
      });
    };

    const clear = () =>{
      caseParam.value = '';
      userParam.value = '';
      actorParam.value = '';
      typeParam.value = '';
      startDate.value = null;
      endDate.value = null;
    };

    const showStartDate = () => {
      showStartDatePicker.value = true;
    };

    const showEndDate = () => {
      showEndDatePicker.value = true;
      
    };

    const clearStartDate = () => {
      startDate.value = null;
    };

    const clearEndDate = () => {
      endDate.value = null;
    };

    return {
      caseParam,
      userParam,
      actorParam,
      typeParam,
      startDate,
      endDate,
      showStartDatePicker,
      showEndDatePicker,
      typeOptions,
      search,
      clear,
      showStartDate,
      showEndDate,
      startDateText,
      endDateText,
      clearStartDate,
      clearEndDate,
    };
  },
});
</script>

<style scoped>
.search-field {
  padding-top: 8px;
}

.button {
  height: 3.3rem;
  margin-top: 1.1rem;
  font-family: 'Montserrat', sans-serif;
  margin-bottom: 1rem;
  /* Ensures the icon is placed to the right */
}

.button:hover {
  background-color: #65c8e6; /* Background color on hover */
  color: #ffffff; /* Text color on hover */
}
</style>