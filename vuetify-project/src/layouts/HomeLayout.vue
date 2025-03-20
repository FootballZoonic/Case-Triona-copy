<template>
  <v-container>
    <v-row>
      <v-col cols="12" md="6" >
        <ProjectDescription :style="{ maxWidth: '550px', minHeight: '450px'}"></ProjectDescription>
      </v-col>
      <v-col cols="12" md="6">
        <Weather location="Borlänge" latitude="60.485344" longitude="15.432495" :style="{ maxWidth: '550px', minHeight: '450px'}"></Weather>
      </v-col>
      </v-row>
      <v-row>
      <v-col cols="12" md="6">
        <ProjectOverview :style="{ maxWidth: '550px', minHeight: '450px'}"></ProjectOverview>
      </v-col>
      <v-col cols="12" md="6">
        <AboutCLoad :style="{ maxWidth: '550px', minHeight: '450px'}"></AboutCLoad>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>

import '../styles/cardStyle.css';

</script>