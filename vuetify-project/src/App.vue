<template>
  <v-app>
    <top-bar></top-bar>
    <v-main>
      <v-container fluid>
        <side-bar></side-bar>
        <router-view></router-view>
        <router-view name="log"></router-view>
      </v-container>
    </v-main> 
    <Footer></Footer>
  </v-app>
  
</template>

<script>
import LogComponent from './components/LogComponent.vue';
import TopBar from './components/TopBar.vue';
import SideBar from './components/SideBar.vue';
import Chart from './components/Chart.vue';
import Footer from './components/Footer.vue'

export default {
  name: 'App',
  components: {
    LogComponent,
    TopBar,
    SideBar,
    Chart,
    Footer
  },
};
</script>

<style>
.v-main {
  padding-top: 64px; /* Adjust this value based on the height of your app bar */
}
/* Add any global styles here */
</style>
