<template>
    <v-select v-model="selectedOption" :items="options" label="Select Option" @change="emitSelectedOption"></v-select>
  </template>
  
  <script lang="ts">
  import { defineComponent, ref, watch } from 'vue';
  
  export default defineComponent({
    name: 'DropDownMenu',
    props: {
      options: {
        type: Array,
        required: true
      }
    },
    setup(props, { emit }) {
      const selectedOption = ref(props.options[0]);
  
      const emitSelectedOption = () => {
        emit('update:selectedOption', selectedOption.value);
      };
  
      watch(selectedOption, emitSelectedOption);
  
      return {
        selectedOption
      };
    }
  });
  </script>
  
  <style scoped>
  .v-select {
    margin-bottom: 16px;
  }
  </style>