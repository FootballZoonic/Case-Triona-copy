<template>
    <v-footer class="custom-footer pt-12 pb-12">
      <v-row class="d-flex w-100 align-center">
        <v-col class="d-flex ga-2 ms-auto">
          <img src="https://triona.se/assets/img/triona.png" alt="Triona Logo" style="height: 50px;">
        </v-col>
        <template v-if="isLgAndUp">
          <v-divider class="my-2 mr-6" thickness="2" vertical></v-divider>
          <v-col class="d-flex ga-2 ms-auto">
            <v-paragraph>
              This application was created by students at Högskolan Dalarna as a CASE project
            </v-paragraph>
          </v-col>
          <v-divider class="my-2 mr-6" thickness="2" vertical></v-divider>
          <v-col class="d-flex ga-2 ms-auto">
            <strong>Created by: <br></strong>
            <v-paragraph>
              Linus Bengtsson,
              Jimmy Hammarberg,<br>
              Victor Kajgård &
              William Olsson
            </v-paragraph>
          </v-col>
        </template>
        <v-divider class="my-2 mr-6" thickness="2" vertical></v-divider>
        <v-col class="d-flex justify-center align-center">
          <a href="https://www.triona.se/">© 2025 Triona AB</a>
        </v-col>
      </v-row>
    </v-footer>
  </template>
  
  <script>
  import { useDisplay } from 'vuetify';
  
  export default {
    name: 'Footer',
    setup() {
      const { lgAndUp } = useDisplay();
      const isLgAndUp = lgAndUp;
  
      return {
        isLgAndUp,
      };
    },
  };
  </script>
  
  <style scoped>
  .custom-footer {
    max-height: 100px;
    background-color: #4A4A4A;
    font-family: Roboto;
    font-size: 14px;
    line-height: 19px;
    color: rgba(255, 255, 255, 0.6);
    z-index: 10000;
  }
  
  a {
    text-decoration: none;
    color: inherit;
    padding: 0;
    margin: 0;
    border: none;
    background: none;
  }
  </style>